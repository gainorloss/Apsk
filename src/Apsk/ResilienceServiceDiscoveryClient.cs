// <copyright file="ResilienceServiceDiscoveryClient.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Apsk.Abstractions;
    using DnsClient;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Polly;
    using Polly.Wrap;

    public class ResilienceServiceDiscoveryClient
        : IServiceDiscoveryClient
    {
        private readonly HttpClient _client;
        private readonly Func<string, IEnumerable<Policy>> _policyCreator;
        private ConcurrentDictionary<string, PolicyWrap> _policyWrappers;
        private readonly ILogger<ResilienceServiceDiscoveryClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDnsQuery _dnsQuery;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResilienceServiceDiscoveryClient"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="policyCreator"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dnsQuery"></param>
        public ResilienceServiceDiscoveryClient(ILogger<ResilienceServiceDiscoveryClient> logger, Func<string, IEnumerable<Policy>> policyCreator, IHttpContextAccessor httpContextAccessor,
            IDnsQuery dnsQuery)
        {
            _client = new HttpClient();
            _policyWrappers = new ConcurrentDictionary<string, PolicyWrap>();
            _policyCreator = policyCreator;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _dnsQuery = dnsQuery;
        }

        public async Task<HttpResponseMessage> SendAsync(string service, string api, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            var url = $"{service}_{api}"?.Trim()?.ToLower();

            if (!_policyWrappers.TryGetValue(url, out PolicyWrap policyWrap))
            {
                policyWrap = Policy.Wrap(_policyCreator(url).ToArray());
                _policyWrappers.TryAdd(url, policyWrap);
            }

            // Executes the action applying all 
            // the policies defined in the wrapper
            return await policyWrap.Execute(
                async ctx =>
            {
                var entries = await _dnsQuery.ResolveServiceAsync("service.consul", service);
                if (entries == null || !entries.Any())
                    throw new Exception();

                var entry = entries.FirstOrDefault();

                var ip = $"http://{entry.HostName.Substring(0, entry.HostName.Length - 1)}:{entry.Port}/{api}";
                var requestMessage = new HttpRequestMessage(method, ip);

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
            //var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            //if (!string.IsNullOrEmpty(authorizationHeader))
            //{
            //    requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            //}
        }
    }
}
