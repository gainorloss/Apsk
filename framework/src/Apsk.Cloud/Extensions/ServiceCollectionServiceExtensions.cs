// <copyright file="ServiceCollectionServiceExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Cloud.Extensions
{
    using Apsk.Abstractions;
    using Apsk.AppSettings;
    using Apsk.Cloud.Abstractions;
    using DnsClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net;

    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddApskServiceDiscoveryClient(this IServiceCollection services, IConfiguration config)
        {
            var serviceDiscovery = new ServiceDiscoverySetting();
            config.GetSection(nameof(ServiceDiscoverySetting)).Bind(serviceDiscovery);

            // dnsQuery.
            var dnsQuery = new LookupClient(IPAddress.Parse(serviceDiscovery.DnsEndpoint.Address), serviceDiscovery.DnsEndpoint.Port);
            services.AddSingleton<IDnsQuery>(sp => dnsQuery);

            // http context accessor.
            services.AddHttpContextAccessor();

            // resilience http client.
            services.AddSingleton<IDiscoveryClient,DiscoveryClient>();

            return services;
        }
    }
}
