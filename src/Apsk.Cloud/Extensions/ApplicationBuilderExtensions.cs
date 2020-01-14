using Apsk.Cloud.AppSettings;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Apsk.Cloud.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApskConsul(this IApplicationBuilder app, IConfiguration config, IHostApplicationLifetime applicationLifetime)
        {
            var svc = new ServiceSetting();
            config.GetSection(nameof(ServiceSetting)).Bind(svc);

            var consul = new ConsulClient(config =>
            {
                config.Datacenter = "dc1";
                config.Address = svc.ConsulUri;
            });

            var srvRegistration = new AgentServiceRegistration()
            {
                Address = svc.GetAddress(),
                Port = svc.Uri.Port,
                Check = new AgentServiceCheck()
                {
                    HTTP = $"{svc.GetUrl()}/healthCheck",
                    Interval = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                },
                ID = Guid.NewGuid().ToString("N"),
                Name = svc.Name
            };
            applicationLifetime.ApplicationStarted.Register(() => consul.Agent.ServiceRegister(srvRegistration).Wait());
            applicationLifetime.ApplicationStopping.Register(() => consul.Agent.ServiceDeregister(srvRegistration.ID).Wait());
        }
    }
}
