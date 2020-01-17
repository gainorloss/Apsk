using Apsk.AppSettings;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace Apsk.Cloud.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApskServiceDiscovery(this IApplicationBuilder app, IConfiguration config, IHostApplicationLifetime applicationLifetime)
        {
            var serviceDiscovery = new ServiceDiscoverySetting();
            config.GetSection(nameof(ServiceDiscoverySetting)).Bind(serviceDiscovery);

            var serviceName = Assembly.GetEntryAssembly().GetName().Name.Replace(".",string.Empty);

            var features = app.Properties["server.Features"] as FeatureCollection;
            var addresses = features.Get<IServerAddressesFeature>()
                .Addresses
                .Select(p => new Uri(p));

            var consul = new ConsulClient(config =>
            {
                config.Datacenter = "dc1";
                config.Address = new Uri(serviceDiscovery.HttpEndpoint);
            });

            foreach (var address in addresses)
            {
                var serviceId = $"{serviceName}_{address.Host}:{address.Port}";

                var registration = new AgentServiceRegistration()
                {
                    Address = address.Host,
                    Port = address.Port,
                    Check = new AgentServiceCheck()
                    {
                        HTTP = $"{address.OriginalString}/healthCheck",
                        Interval = TimeSpan.FromSeconds(30),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                    },
                    ID = serviceId,
                    Name =serviceName
                };
                applicationLifetime.ApplicationStarted.Register(() => consul.Agent.ServiceRegister(registration).Wait());
                applicationLifetime.ApplicationStopping.Register(() => consul.Agent.ServiceDeregister(registration.ID).Wait());
            }
        }
    }
}
