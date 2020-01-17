using Apsk.Abstractions;
using Apsk.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceDiscovery.ConsoleApp
{
    class Program
    {
        private static Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            try
            {
                #region Obsolete.
                //var timeOut = Policy.Timeout(2, (ctx, span, task) => Console.WriteLine($"{DateTime.Now} - 超时"));
                //var retry = Policy.Handle<Exception>()
                //    .Retry(2, (ex, i) => Console.WriteLine($"{DateTime.Now} - 重试第{i}次:{ex.Message}"));
                //var breaker = Policy.Handle<Exception>()
                //    .CircuitBreaker(2, TimeSpan.FromSeconds(2), (ex, span) => Console.WriteLine($"{DateTime.Now} - 中断:{span.TotalSeconds}s"), () => Console.WriteLine($"{DateTime.Now} - 中断:恢复"));

                //var fallback = Policy.Handle<Exception>().Fallback(() => Console.WriteLine($"{DateTime.Now} - 降级"));


                //var services = new[] { "CatalogItemsAPI", "UsersAPI" };
                //var dnsQuery = new LookupClient(IPAddress.Parse("127.0.0.1"), 8600);

                //for (int i = 0; i < 1000; i++)
                //{
                //    Policy.Wrap(fallback, breaker, retry, timeOut)
                //        .Execute(() =>
                //{

                //    var entries = dnsQuery.ResolveServiceAsync("service.consul", "UsersAPI").Result;
                //    var entry = entries.FirstOrDefault();
                //    if (entry == null)
                //        throw new Exception();

                //    var ip = $"http://{entry.HostName.Substring(0, entry.HostName.Length - 1)}:{entry.Port}";
                //    using (var httpClient = new HttpClient())
                //    {
                //        var ret = httpClient.GetAsync($"{ip}/api.user.getname/v1.0").Result.Content.ReadAsStringAsync().Result;
                //        Console.WriteLine($"{DateTime.Now} - 请求{ip}成功:{ret}");
                //    }
                //});
                //    Task.Delay(1000).Wait();
                //} 
                #endregion

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                var sp = new ServiceCollection()
                    .AddApskComponents(config)
                    .AddApskServiceDiscovery(config)
                    .BuildServiceProvider();

                var serviceDiscovery = sp.GetRequiredService<IServiceDiscoveryClient>();

                for (int i = 0; i < 1000; i++)
                {
                    var rsp = await serviceDiscovery.SendAsync("UsersAPI", "api.user.getname/v1.0", HttpMethod.Get);
                    if (rsp.IsSuccessStatusCode)
                        Console.WriteLine($"{DateTime.Now} - 请求UsersAPI：成功");

                    Task.Delay(1000).Wait();
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
