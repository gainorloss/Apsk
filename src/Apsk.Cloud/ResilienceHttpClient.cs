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

        public Task<HttpResponseMessage> PostAsync<T>(string url, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return DoPostPutAsync(HttpMethod.Post, url, () =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
                return requestMessage;
            }, authorizationToken, requestId, authorizationMethod);
        }
        public Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> dic, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return DoPostPutAsync(HttpMethod.Post, url, () =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new FormUrlEncodedContent(dic);
                return requestMessage;
            }, authorizationToken, requestId, authorizationMethod);
        }

        public Task<string> GetStringAsync(string url, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            return HttpInvoker(url, async (ctx) =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                SetAuthorizationHeader(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                var response = await _client.SendAsync(requestMessage);

                // raise exception if HttpResponseCode 500 
                // needed for circuit breaker to track fails

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return await response.Content.ReadAsStringAsync();
            });
        }

        public Task<HttpResponseMessage> DeleteAsync(string url, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return HttpInvoker(url, async (ctx) =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);

                SetAuthorizationHeader(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                if (requestId != null)
                {
                    requestMessage.Headers.Add("x-requestid", requestId);
                }

                return await _client.SendAsync(requestMessage);
            });
        }

        public Task<HttpResponseMessage> PutAsync<T>(string url, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return DoPostPutAsync(HttpMethod.Put, url, item, authorizationToken, requestId, authorizationMethod);
        }

        #region Private internal methods.
        private Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry 
            // as it is disposed after each call
            return HttpInvoker(uri, async (ctx) =>
            {
                var requestMessage = new HttpRequestMessage(method, uri);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
                SetAuthorizationHeader(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                if (requestId != null)
                {
                    requestMessage.Headers.Add("x-requestid", requestId);
                }

                var response = await _client.SendAsync(requestMessage);

                // raise exception if HttpResponseCode 500 
                // needed for circuit breaker to track fails

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }
        private Task<HttpResponseMessage> DoPostPutAsync(HttpMethod method, string uri, Func<HttpRequestMessage> func, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry 
            // as it is disposed after each call
            return HttpInvoker(uri, async (ctx) =>
            {
                var requestMessage = func.Invoke();

                SetAuthorizationHeader(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                if (requestId != null)
                {
                    requestMessage.Headers.Add("x-requestid", requestId);
                }

                var response = await _client.SendAsync(requestMessage);

                // raise exception if HttpResponseCode 500 
                // needed for circuit breaker to track fails

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }

        private async Task<T> HttpInvoker<T>(string url, Func<Context, Task<T>> action)
        {
             url = url?.Trim()?.ToLower();

            if (!_policyWrappers.TryGetValue(url, out PolicyWrap policyWrap))
            {
                policyWrap = Policy.Wrap(_policyCreator((string)url).ToArray());
                _policyWrappers.TryAdd((string)url, policyWrap);
            }

            // Executes the action applying all 
            // the policies defined in the wrapper
            return await policyWrap.Execute(action, new Context(url));
        }

        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }

        #endregion
    }
}
