using AspectCore.Extensions.DependencyInjection;
using BootSharp.ConsoleApp.AggregatesModel;
using BootSharp.ConsoleApp.EventHandlers;
using Infrastructure.Bus.Abstractions;
using Infrastructure.Bus.Extensions;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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
                //logger.LogDebug("debug");
                //logger.LogInformation("information");
                //logger.LogWarning("warning");
                //logger.LogError("error");
                //logger.LogCritical("critical");
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

            #region 事件总线 单元测试
            {
                var eventBus = sp.GetRequiredService<IEventBus>();
                //eventBus.Subscribe<PersonCreatedEvent, PersonCreatedEventHandler>();
                eventBus.Subscribe();

                //Thread.Sleep(3000);

                var eh = sp.GetRequiredService<PersonCreatedEventHandler>();
                var mediator = sp.GetRequiredService<IMediator>();
                //Performance("普通调用", async () => await eh.HandleAsync(new PersonCreatedEvent()), 1);
                //Performance("MediatR", () => mediator.Publish(new PersonCreatedNotification()), 1);
                //Performance("事件总线", () => eventBus.Publish(new PersonCreatedEvent()), 1);

                //Performance("普通调用", () => eh.HandleAsync(new PersonCreatedEvent()), 100);
                //Performance("MediatR调用", () => mediator.Publish(new PersonCreatedNotification()), 100);
                //Performance("事件总线", () => eventBus.Publish(new PersonCreatedEvent()), 100);

                //Performance("普通调用", () => eh.HandleAsync(new PersonCreatedEvent()), 1000);
                //Performance("MediatR调用", () => mediator.Publish(new PersonCreatedNotification()), 1000);
                //Performance("事件总线", () => eventBus.Publish(new PersonCreatedEvent()), 1000);

                //Performance2("普通调用", () => eh.HandleAsync(new PersonCreatedEvent()), 1000);
                //Performance2("事件总线", () => eventBus.Publish(new PersonCreatedEvent()), 1000);

                //Performance("普通调用", () => eh.HandleAsync(new PersonCreatedEvent()), 10000);
                //Performance("MediatR调用", () => mediator.Publish(new PersonCreatedNotification()), 10000);
                //Performance("事件总线", () => eventBus.Publish(new PersonCreatedEvent()), 10000);

                //Performance("普通调用", () => eh.HandleAsync(new PersonCreatedEvent()), 1000000);
                //Performance("MediatR调用", () => mediator.Publish(new PersonCreatedNotification()), 1000000);
                //Performance("事件总线", () => eventBus.Publish(new PersonCreatedEvent()), 1000000);

                //Performance2("普通调用", () => eh.HandleAsync(new PersonCreatedEvent()), 10000000);
                //Performance2("MediatR调用", () => mediator.Publish(new PersonCreatedNotification()), 10000000);
                //Performance2("事件总线", () => eventBus.Publish(new PersonCreatedEvent()), 10000000);
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
