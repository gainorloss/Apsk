using AspectCore.Extensions.DependencyInjection;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .Build();

            var sp = new ServiceCollection()
                .AddComponents(configuration)
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddBus()
                .BuildAspectInjectorProvider();
            var logger = sp.GetRequiredService<ILogger<Program>>();

            #region serilog 功能测试

            {
                logger.LogDebug("debug");
                logger.LogInformation("information");
                logger.LogWarning("warning");
                logger.LogError("error");
                logger.LogCritical("critical");
            }
            #endregion

            var integrationTest = sp.GetRequiredService<IIntegrationTests>();
            var functionTest = sp.GetRequiredService<IFunctionTests>();

            #region Orleans 性能测试
            {
                var url = "http://localhost:5000/api/account/login";
                //var url = "http://localhost:5000/api/weatherforecast/login";
                {
                    //var times = 1000;
                    //var sw = new Stopwatch();

                    //sw.Start();
                    //Parallel.For(0, times, async i => await new HttpClient().GetAsync(url));
                    //sw.Stop();

                    //System.Console.WriteLine($@"【+$ConcurrencyTest】:{times}并发{sw.ElapsedMilliseconds}ms))(无线电)");
                    //await Task.CompletedTask;
                }
                //await integrationTest.TestAccountLoginAsync();
            }
            #endregion

            Console.ReadLine();
        }

        static void Performance(string scene, Action act, int times = 1000)
        {
            var sw = Stopwatch.StartNew();
            sw.Start();
            Parallel.For(0, times, i =>
            {
                act.Invoke();
            });
            sw.Stop();
            Console.WriteLine($"【${scene}】:{times}并发,{sw.ElapsedMilliseconds}ms");
        }

        static void Performance2(string scene, Action act, long times = 1000)
        {
            var sw = Stopwatch.StartNew();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                act.Invoke();
            }
            sw.Stop();
            Console.WriteLine($"【${scene}】:{times},{sw.ElapsedMilliseconds}ms");
        }
    }
}
