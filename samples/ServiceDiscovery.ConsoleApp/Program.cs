using DnsClient;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceDiscovery.ConsoleApp
{
    class Program
    {
        private static Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        static async Task Main(string[] args)
        {
            try
            {
                var timeOut = Policy.Timeout(2, (ctx, span, task) => Console.WriteLine($"{DateTime.Now} - 超时"));
                var retry = Policy.Handle<Exception>()
                    .Retry(2, (ex, i) => Console.WriteLine($"{DateTime.Now} - 重试第{i}次:{ex.Message}"));
                var breaker = Policy.Handle<Exception>()
                    .CircuitBreaker(2, TimeSpan.FromSeconds(2),(ex, span) => Console.WriteLine($"{DateTime.Now} - 中断:{span.TotalSeconds}s"),()=> Console.WriteLine($"{DateTime.Now} - 中断:恢复"));

                var fallback = Policy.Handle<Exception>().Fallback(()=> Console.WriteLine($"{DateTime.Now} - 降级"));

                var services = new[] { "CatalogItemsAPI","UsersAPI" };
                var dnsQuery = new LookupClient(IPAddress.Parse("127.0.0.1"),8600);

                while (true)
                {
                    Thread.Sleep(3000);
                    foreach (var service in services)
                    {
                        var entries = await dnsQuery.ResolveServiceAsync("service.consul", service);
                        var entry = entries.FirstOrDefault();
                        if (entry != null)
                        {
                            if (!keyValuePairs.TryGetValue(entry.HostName,out var count))
                            {
                                keyValuePairs.Add(entry.HostName,1);
                            }
                            else
                            {
                                keyValuePairs[entry.HostName] = count + 1;
                            }
                        }

                        Console.WriteLine(string.Join(",",keyValuePairs.Select(pair=>$"{pair.Key}:{pair.Value}")));
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
