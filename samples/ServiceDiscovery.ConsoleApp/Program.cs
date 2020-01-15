using DnsClient;
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
