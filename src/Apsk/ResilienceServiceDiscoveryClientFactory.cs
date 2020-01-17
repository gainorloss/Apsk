// <copyright file="ResilienceServiceDiscoveryClientFactory.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Apsk.Abstractions;
    using DnsClient;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Polly;

    public class ResilienceServiceDiscoveryClientFactory
    {
        private readonly ILogger<ResilienceServiceDiscoveryClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int _retryCount;
        private readonly int _exceptionCountAllowedBeforeBreaking;
        private readonly IDnsQuery _dnsQuery;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResilienceServiceDiscoveryClientFactory"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="retryCount"></param>
        /// <param name="exceptionCountAllowedBeforeBreaking"></param>
        /// <param name="dnsQuery"></param>
        public ResilienceServiceDiscoveryClientFactory(
            ILogger<ResilienceServiceDiscoveryClient> logger,
            IHttpContextAccessor httpContextAccessor,
            int retryCount,
            int exceptionCountAllowedBeforeBreaking,
            IDnsQuery dnsQuery)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _retryCount = retryCount;
            _exceptionCountAllowedBeforeBreaking = exceptionCountAllowedBeforeBreaking;
            _dnsQuery = dnsQuery;
        }

        public IServiceDiscoveryClient GetResilienceHttpClient() =>
            new ResilienceServiceDiscoveryClient(_logger, origin => CreatePolicies(origin), _httpContextAccessor, _dnsQuery);

        public Policy[] CreatePolicies(string origin)
        {
            var timeout = Policy.Timeout(2, (ctx, span, task) => Console.WriteLine($"{DateTime.Now} - 超时"));
            var retry = Policy.Handle<Exception>()
                .Retry(2, (ex, i) => Console.WriteLine($"{DateTime.Now} - 重试第{i}次:{ex.Message}"));
            var breaker = Policy.Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(2), (ex, span) => Console.WriteLine($"{DateTime.Now} - 中断:{span.TotalSeconds}s"), () => Console.WriteLine($"{DateTime.Now} - 中断:恢复"));

            var fallback = Policy.Handle<Exception>().Fallback(() => Console.WriteLine($"{DateTime.Now} - 降级"));
            return new Policy[] { fallback, breaker, retry, timeout };
        }
    }
}
