using Apsk.Cloud.AppSettings;
using DnsClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Apsk.Cloud.Extensions
{
    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddApskServiceDiscovery(this IServiceCollection services,IConfiguration config)
        {
            var serviceDiscovery = new ServiceDiscoverySetting();
            config.GetSection(nameof(ServiceDiscoverySetting)).Bind(serviceDiscovery);

            // dnsQuery.
            var dnsQuery = new LookupClient(IPAddress.Parse(serviceDiscovery.DnsEndpoint.Address), serviceDiscovery.DnsEndpoint.Port);
            services.AddSingleton<IDnsQuery>(sp => dnsQuery);

            // http context accessor.
            services.AddHttpContextAccessor();
            // resilience http client.
            services.AddSingleton(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ResilienceHttpClient>>();
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                return new ResilienceHttpClientFactory(logger, httpContextAccessor, 5, 5);
            });
            services.AddSingleton(sp =>sp.GetRequiredService<ResilienceHttpClientFactory>().GetResilienceHttpClient());

            return services;
        }
    }
}
