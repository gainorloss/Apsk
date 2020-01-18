// <copyright file="HystrixCommandInterceptorAttribute.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System;
    using System.Threading.Tasks;
    using AspectCore.DynamicProxy;
    using Microsoft.Extensions.Caching.Memory;
    using Polly;

    /// <summary>
    /// @HistrixCommand.
    /// </summary>
    public class HystrixCommandAttribute
        : AbstractInterceptorAttribute
    {
        public string FallbackMethod { get; set; }

        public int ExceptionsAllowedBeforeBreaking { get; set; } = 2;

        public double IntervalSeconds { get; set; } = 5;

        public int TimeoutSeconds { get; set; } = 2;

        public int RetryTimes { get; set; } = 2;

        public int CacheTTLMilliseconds { get; set; }

        private Policy _policy;
        private static object _lock = new object();
        private IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

        /// <inheritdoc/>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            lock (_lock)
            {
                if (_policy == null)
                    _policy = Policy.NoOp();

                if (!string.IsNullOrEmpty(FallbackMethod))
                {
                    _policy = _policy.Wrap(Policy.Handle<Exception>().Fallback(
                         (ctx, token) =>
                    {
                        AspectContext aspectContext = ctx["aspectContext"] as AspectContext;
                        var fallbackMethod = aspectContext.ServiceMethod.DeclaringType.GetMethod(FallbackMethod);
                        var fallbackResult = fallbackMethod.Invoke(aspectContext.Implementation, aspectContext.Parameters);
                        aspectContext.ReturnValue = fallbackResult;
                    }, (ex, token) => { }));
                }

                if (ExceptionsAllowedBeforeBreaking > 0)
                {
                    _policy = _policy.Wrap(Policy.Handle<Exception>().CircuitBreaker(
                        ExceptionsAllowedBeforeBreaking,
                        TimeSpan.FromSeconds(IntervalSeconds),
                        (ex, span) => Console.WriteLine($"{DateTime.Now} - 断路器:开启"),
                        () => Console.WriteLine($"{DateTime.Now} - 断路器:重置")));
                }

                if (RetryTimes > 0)
                    _policy = _policy.Wrap(Policy.Handle<Exception>().WaitAndRetry(RetryTimes, retryCount => TimeSpan.FromSeconds(IntervalSeconds), (ex, span) => Console.WriteLine($"{DateTime.Now} - 重试")));

                if (TimeoutSeconds > 0)
                    _policy = _policy.Wrap(Policy.Timeout(TimeoutSeconds));
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
                    await _policy.Execute(async ctx => await next(context), pollyContext);
                    using (var entry = _memoryCache.CreateEntry(cacheKey))
                    {
                        entry.Value = context.ReturnValue;
                        entry.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMilliseconds(CacheTTLMilliseconds);
                    }
                }
            }
            else
            {
                await _policy.Execute(async ctx => await next(context), pollyContext);
            }
        }
    }
}
