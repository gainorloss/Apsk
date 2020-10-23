// <copyright file="TestAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using AspectCore.DynamicProxy;

    /// <summary>
    /// @Test 注解单元测试.
    /// </summary>
    public class TestAttribute
        : AbstractInterceptorAttribute
    {
        /// <summary>
        /// Gets or sets times.
        /// </summary>
        public int[] Times { get; set; } = new[] { 100 };

        /// <inheritdoc/>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var sw = new Stopwatch();

            var result = new List<long>();
            foreach (var times in Times)
            {
                sw.Restart();
                for (int i = 0; i < times; i++)
                    await next(context);
                sw.Stop();
                result.Add(sw.ElapsedMilliseconds);
            }
#if DEBUG
            System.Console.WriteLine($@"【+$Test】:
{string.Join(Environment.NewLine, Times.Select((times, i) => $"执行{times}次{result[i]}ms"))}(无线电)");
#endif

        }
    }
}
