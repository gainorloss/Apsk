// <copyright file="ResilienceServiceDiscoveryClient.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using System;
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

    public class DiscoveryClient
        : IDiscoveryClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<DiscoveryClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDnsQuery _dnsQuery;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryClient"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="policyCreator"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dnsQuery"></param>
        public DiscoveryClient(ILogger<DiscoveryClient> logger, IHttpContextAccessor httpContextAccessor,
            IDnsQuery dnsQuery)
        {
            _client = new HttpClient();
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _dnsQuery = dnsQuery;
        }

        public Task<HttpResponseMessage> FallbackAsync(string service, string api, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            Console.WriteLine("fallback");
            return Task.FromResult(default(HttpResponseMessage));
        }

        public async Task<HttpResponseMessage> SendAsync(string service, string api, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            var entries = await _dnsQuery.ResolveServiceAsync("service.consul", service);
            if (entries == null || !entries.Any())
                throw new ArgumentException();

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
