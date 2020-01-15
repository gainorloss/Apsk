using Apsk.Cloud.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Net.Http;

namespace Apsk.Cloud
{
    public class ResilienceHttpClientFactory
    {
        private readonly ILogger<ResilienceHttpClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int _retryCount;
        private readonly int _exceptionCountAllowedBeforeBreaking;
        public ResilienceHttpClientFactory(ILogger<ResilienceHttpClient> logger,
            IHttpContextAccessor httpContextAccessor,
            int retryCount,
            int exceptionCountAllowedBeforeBreaking)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _retryCount = retryCount;
            _exceptionCountAllowedBeforeBreaking = exceptionCountAllowedBeforeBreaking;
        }
        public IHttpClient GetResilienceHttpClient() =>
            new ResilienceHttpClient(_logger, origin => CreatePolicies(origin), _httpContextAccessor);

        public Policy[] CreatePolicies(string origin)
        {
            return new Policy[] {
                Policy.Handle<HttpRequestException>()
                .WaitAndRetry(
                    // number of retries
                    _retryCount,
                    // exponential backofff
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    // on retry
                    (exception, timeSpan, retryCount, context) =>
                    {
                        var msg = $"Retry {retryCount} implemented with Polly's RetryPolicy " +
                            $"of {context.PolicyKey} " +
                            //$"at {context.ExecutionKey}, " +
                            $"due to: {exception}.";
                        _logger.LogWarning(msg);
                        _logger.LogDebug(msg);
                    }),
                  Policy.Handle<HttpRequestException>()
                .CircuitBreaker( 
                   // number of exceptions before breaking circuit
                   _exceptionCountAllowedBeforeBreaking,
                   // time circuit opened before retry
                   TimeSpan.FromMinutes(1),
                   (exception, duration) =>
                   {
                        // on circuit opened
                        _logger.LogTrace("Circuit breaker opened");
                   },
                   () =>
                   {
                        // on circuit closed
                        _logger.LogTrace("Circuit breaker reset");
                   })
            };
        }
    }
}
