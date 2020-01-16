using DnsClient;
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
        private static Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            try
            {
                var timeOut = Policy.Timeout(2, (ctx, span, task) => Console.WriteLine($"{DateTime.Now} - 超时"));
                var retry = Policy.Handle<Exception>()
                    .Retry(2, (ex, i) => Console.WriteLine($"{DateTime.Now} - 重试第{i}次:{ex.Message}"));
                var breaker = Policy.Handle<Exception>()
                    .CircuitBreaker(2, TimeSpan.FromSeconds(2), (ex, span) => Console.WriteLine($"{DateTime.Now} - 中断:{span.TotalSeconds}s"), () => Console.WriteLine($"{DateTime.Now} - 中断:恢复"));

                var fallback = Policy.Handle<Exception>().Fallback(() => Console.WriteLine($"{DateTime.Now} - 降级"));


                var services = new[] { "CatalogItemsAPI", "UsersAPI" };
                var dnsQuery = new LookupClient(IPAddress.Parse("127.0.0.1"), 8600);

                for (int i = 0; i < 1000; i++)
                {
                    Policy.Wrap(fallback, breaker, retry, timeOut)
                        .Execute(() =>
                {

                    var entries = dnsQuery.ResolveServiceAsync("service.consul", "UsersAPI").Result;
                    var entry = entries.FirstOrDefault();
                    if (entry == null)
                        throw new Exception();

                    var ip = $"http://{entry.HostName.Substring(0, entry.HostName.Length - 1)}:{entry.Port}";
                    using (var httpClient = new HttpClient())
                    {
                        var ret = httpClient.GetAsync($"{ip}/api.user.getname/v1.0").Result.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"{DateTime.Now} - 请求{ip}成功:{ret}");
                    }
                });
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
