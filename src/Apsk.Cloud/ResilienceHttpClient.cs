using Apsk.Cloud.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Wrap;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Apsk.Cloud
{
    public class ResilienceHttpClient
        : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly Func<string, IEnumerable<Policy>> _policyCreator;
        private ConcurrentDictionary<string, PolicyWrap> _policyWrappers;
        private readonly ILogger<ResilienceHttpClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ResilienceHttpClient(ILogger<ResilienceHttpClient> logger, Func<string, IEnumerable<Policy>> policyCreator, IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _policyWrappers = new ConcurrentDictionary<string, PolicyWrap>();
            _policyCreator = policyCreator;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> SendAsync<T>(string url, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            url = url?.Trim()?.ToLower();

            if (!_policyWrappers.TryGetValue(url, out PolicyWrap policyWrap))
            {
                policyWrap = Policy.Wrap(_policyCreator((string)url).ToArray());
                _policyWrappers.TryAdd((string)url, policyWrap);
            }

            // Executes the action applying all 
            // the policies defined in the wrapper
            return await policyWrap.Execute(async ctx =>
            {
                var requestMessage = new HttpRequestMessage(method, url);

                if (data != null)
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");

                SetAuthorizationHeader(requestMessage);

                if (authorizationToken != null)
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

                if (requestId != null)
                    requestMessage.Headers.Add("x-requestid", requestId);

                var rsp = await _client.SendAsync(requestMessage);

                if (rsp.StatusCode == HttpStatusCode.InternalServerError)
                    throw new HttpRequestException();
                return rsp;
            }, new Context(url));
        }

        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }
    }
}
