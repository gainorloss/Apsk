using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Apsk.AOP
{
    /// <summary>
    /// @Test 注解单元测试
    /// </summary>
    public class TestAttribute
        : AbstractInterceptorAttribute
    {
        public int[] Times { get; set; } = new[] { 100 };
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
