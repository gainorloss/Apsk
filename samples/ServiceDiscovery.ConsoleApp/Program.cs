using Apsk;
using Apsk.Abstractions;
using Apsk.Extensions;
using AspectCore.Extensions.DependencyInjection;
using DnsClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceDiscovery.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var config = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", false, true)
                      .Build();

                var sp = new ServiceCollection()
                    .AddApskComponents(config)
                    .AddApskServiceDiscoveryClient(config)
                    .BuildDynamicProxyProvider();

                var executor = sp.GetRequiredService<IHistrixCommandExecutor>();

                {
                    var serviceDiscovery = sp.GetRequiredService<IDiscoveryClient>();

                    for (int i = 0; i < 1000; i++)
                    {
                        await executor.ExecuteAsync(async () =>
                         {
                             var rsp = await serviceDiscovery.SendAsync("UsersAPI", "api.user.getname/v1.0", HttpMethod.Get);
                             if (rsp.IsSuccessStatusCode)
                                 Console.WriteLine($"{DateTime.Now} - 请求UsersAPI：成功");
                         });
                        Task.Delay(1000).Wait();
                    }
                }

                {
                    //var serviceDiscovery = sp.GetRequiredService<IDiscoveryClient>();

                    //for (int i = 0; i < 1000; i++)
                    //{
                    //    var rsp = await serviceDiscovery.SendAsync("UsersAPI", "api.user.getname/v1.0", HttpMethod.Get);
                    //    if (rsp.IsSuccessStatusCode)
                    //        Console.WriteLine($"{DateTime.Now} - 请求UsersAPI：成功");
                    //    Task.Delay(1000).Wait();
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
