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
            var serviceSetting = new ServiceSetting();
            config.GetSection(nameof(ServiceSetting)).Bind(serviceSetting);

            var consul = new ConsulClient();
            var srvRegistration = new AgentServiceRegistration()
            {
                Address = serviceSetting.Address,
                Port = serviceSetting.Port,
                Check = new AgentServiceCheck()
                {
                    HTTP = $"{serviceSetting}:{serviceSetting.Port}/healthCheck",
                    Interval = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                },
                ID = Guid.NewGuid().ToString("N"),
                Name = serviceSetting.Name
            };
            applicationLifetime.ApplicationStarted.Register(() => consul.Agent.ServiceRegister(srvRegistration).Wait());
            applicationLifetime.ApplicationStopping.Register(() => consul.Agent.ServiceDeregister(srvRegistration.ID).Wait());
        }
    }
}
