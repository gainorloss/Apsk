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

                #region Obsolete.
                {
                    //var services = new[] { "CatalogItemsAPI", "UsersAPI" };
                    //var dnsQuery = new LookupClient(IPAddress.Parse("127.0.0.1"), 8600);

                    //for (int i = 0; i < 1000; i++)
                    //{
                    //    executor.Execute(() =>
                    //    {
                    //        var entries = dnsQuery.ResolveServiceAsync("service.consul", "UsersAPI").Result;
                    //        var entry = entries.FirstOrDefault();
                    //        if (entry == null)
                    //            throw new Exception();

                    //        var ip = $"http://{entry.HostName.Substring(0, entry.HostName.Length - 1)}:{entry.Port}";
                    //        using (var httpClient = new HttpClient())
                    //        {
                    //            var ret = httpClient.GetAsync($"{ip}/api.user.getname/v1.0").Result.Content.ReadAsStringAsync().Result;
                    //            Console.WriteLine($"{DateTime.Now} - 请求{ip}成功:{ret}");
                    //        }

                    //    });
                    //    Task.Delay(1000).Wait();
                    //}
                }
                #endregion

                {
                    var serviceDiscovery = sp.GetRequiredService<IDiscoveryClient>();

                    for (int i = 0; i < 1000; i++)
                    {
                        executor.Execute(async () =>
                        {
                            var rsp = await serviceDiscovery.SendAsync("UsersAPI", "api.user.getname/v1.0", HttpMethod.Get);
                            if (rsp.IsSuccessStatusCode)
                                Console.WriteLine($"{DateTime.Now} - 请求UsersAPI：成功");

                        });
                        Task.Delay(1000).Wait();
                    }
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
