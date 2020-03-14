using Apsk.Abstractions;
using Apsk.Cloud.Abstractions;
using Apsk.Cloud.Extensions;
using Apsk.Extensions;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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
                        var ret = await serviceDiscovery.SendAsync("UsersAPI", "api.user.getname/v1.0", HttpMethod.Get);
                        if (ret.Success)
                            Console.WriteLine($"{DateTime.Now} - 请求UsersAPI：成功");
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
