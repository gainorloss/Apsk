// <copyright file="HystrixCommandInterceptorAttribute.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Threading.Tasks;
    using Apsk.Abstractions;
    using AspectCore.DependencyInjection;
    using AspectCore.DynamicProxy;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Polly;

    /// <summary>
    /// @HistrixCommand.
    /// </summary>
    public class HystrixCommandAttribute
        : AbstractInterceptorAttribute
    {
        public string FallbackMethod { get; set; } = "FallbackAsync";

        public int ExceptionsAllowedBeforeBreaking { get; set; } = 2;

        public double IntervalSeconds { get; set; } = 5;

        public int TimeoutSeconds { get; set; } = 2;

        public int RetryTimes { get; set; } = 2;

        public int CacheTTLMilliseconds { get; set; }

        private static ConcurrentDictionary<MethodInfo, AsyncPolicy> policies
               = new ConcurrentDictionary<MethodInfo, AsyncPolicy>();

        private IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

        [FromServiceContext]
        public ILogger<HystrixCommandAttribute> Logger { get; set; }

        /// <inheritdoc/>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            policies.TryGetValue(context.ServiceMethod, out AsyncPolicy policy);
            lock (policies)
            {
                if (policy == null)
                    policy = Policy.NoOpAsync();

                if (!string.IsNullOrEmpty(FallbackMethod))
                {
                    policy = policy.WrapAsync(Policy.Handle<Exception>().FallbackAsync(
                        async (ctx, token) =>
                    {
                        AspectContext aspectContext = ctx["aspectContext"] as AspectContext;
                        var fallbackMethod = aspectContext.ServiceMethod.DeclaringType.GetMethod(FallbackMethod);
                        var fallbackResult = fallbackMethod.Invoke(aspectContext.Implementation, aspectContext.Parameters);
                        aspectContext.ReturnValue = fallbackResult;
                        Logger.LogDebug($"{DateTime.Now} - 降级");
                    }, async (ex, token) => { }));
                }

                if (ExceptionsAllowedBeforeBreaking > 0)
                {
                    policy = policy.WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(
                        ExceptionsAllowedBeforeBreaking,
                        TimeSpan.FromSeconds(IntervalSeconds),
                        (ex, span) => Logger.LogDebug($"{DateTime.Now} - 断路器:开启"),
                        () => Logger.LogDebug($"{DateTime.Now} - 断路器:重置")));
                }

                if (RetryTimes > 0)
                    policy = policy.WrapAsync(Policy.Handle<Exception>().WaitAndRetryAsync(RetryTimes, retryCount => TimeSpan.FromSeconds(IntervalSeconds), (ex, span) => Logger.LogDebug($"{DateTime.Now} - 重试")));

                if (TimeoutSeconds > 0)
                    policy = policy.WrapAsync(Policy.TimeoutAsync(TimeoutSeconds));
            }

            Context pollyContext = new Context();
            pollyContext["aspectContext"] = context;
            if (CacheTTLMilliseconds > 0)
            {
                var cacheKey = $"hystrixCommand_key_{context.ServiceMethod.DeclaringType}.{context.ServiceMethod}{string.Join("_", context.Parameters)}";
                if (_memoryCache.TryGetValue(cacheKey, out var charValue))
                {
                    context.ReturnValue = charValue;
                }
                else
                {
                    await policy.ExecuteAsync(ctx => next(context), pollyContext);
                    using (var entry = _memoryCache.CreateEntry(cacheKey))
                    {
                        entry.Value = context.ReturnValue;
                        entry.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMilliseconds(CacheTTLMilliseconds);
                    }
                }
            }
            else
            {
                await policy.ExecuteAsync(ctx => next(context), pollyContext);
            }
        }
    }
}
