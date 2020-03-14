// <copyright file="HystrixCommandExecutor.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using Apsk.Abstractions;
    using Apsk.Annotations;
    using Polly;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    [Component(ComponentLifeTimeScope.Singleton)]
    public class HystrixCommandExecutor
         : IHistrixCommandExecutor
    {
        public void Execute(Action action)
        {
            var timeOut = Policy.Timeout(2, (ctx, span, task) => Console.WriteLine($"{DateTime.Now} - 超时"));
            var retry = Policy.Handle<Exception>()
                .Retry(2, (ex, i) => Console.WriteLine($"{DateTime.Now} - 重试第{i}次:{ex.Message}"));
            var breaker = Policy.Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(2), (ex, span) => Console.WriteLine($"{DateTime.Now} - 中断:{span.TotalSeconds}s"), () => Console.WriteLine($"{DateTime.Now} - 中断:恢复"));

            var fallback = Policy.Handle<Exception>().Fallback(() => Console.WriteLine($"{DateTime.Now} - 降级"));

            Policy.Wrap(fallback, breaker, retry, timeOut)
                .Execute(action);
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            var timeOut = Policy.TimeoutAsync(2, (ctx, span, task) =>
            {
                Console.WriteLine($"{DateTime.Now} - 超时");
                return Task.CompletedTask;
            });
            var retry = Policy.Handle<Exception>()
                .RetryAsync(2, (ex, i) => Console.WriteLine($"{DateTime.Now} - 重试第{i}次:{ex.Message}"));
            var breaker = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(2), (ex, span) => Console.WriteLine($"{DateTime.Now} - 中断:{span.TotalSeconds}s"), () => Console.WriteLine($"{DateTime.Now} - 中断:恢复"));

            var fallback = Policy.Handle<Exception>().FallbackAsync(ct =>
            {
                Console.WriteLine($"{DateTime.Now} - 降级");
                return Task.CompletedTask;
            });

            await Policy.WrapAsync(fallback, breaker, retry, timeOut)
                  .ExecuteAsync(action);
        }
    }
}
