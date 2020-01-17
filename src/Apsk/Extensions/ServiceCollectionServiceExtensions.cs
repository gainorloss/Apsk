// <copyright file="ServiceCollectionServiceExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Apsk.Abstractions;
    using Apsk.Annotations;
    using Apsk.AppSettings;
    using Apsk.HostedServices;
    using AspectCore.Extensions.DependencyInjection;
    using DnsClient;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;

    public static class ServiceCollectionServiceExtensions
    {
        private static readonly IEnumerable<ComponentAttribute> components = new List<ComponentAttribute>();

        static ServiceCollectionServiceExtensions()
        {
            components = BootstrapClassLoader.LoadComponents();

            // serilog 设置
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.Console()
             .WriteTo.File("logs/log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
             .CreateLogger();
        }

        /// <summary>
        /// 添加组件.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApskComponents(this IServiceCollection services, IConfiguration configuration)
        {
            // 日志注册
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

            // AOP注册
            services.ConfigureDynamicProxy();

            // 内存缓存
            services.AddMemoryCache();

            // options.
            services.AddOptions();

            // configuration.
            services.AddSingleton<IConfiguration>(sp => configuration);

            // orleans
            // services.AddSingleton<ClientHostedService>();
            // services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<ClientHostedService>());
            // services.AddSingleton(sp => sp.GetRequiredService<ClientHostedService>().ClusterClient);
            foreach (var component in components)
            {
                RegisterPropetySources(component, configuration, services);
                RegisterComponents(component, services);
            }

            // quartz
            services.AddHostedService<QuartzHostedService>();
            return services;
        }

        public static IServiceCollection AddApskBus(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IEventHandlerExecutionContext>(new InMemoryEventHandlerExecutionContext(services));
            return services;
        }

        public static IServiceCollection AddApskServiceDiscovery(this IServiceCollection services, IConfiguration config)
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
                var logger = sp.GetRequiredService<ILogger<ResilienceServiceDiscoveryClient>>();
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                return new ResilienceServiceDiscoveryClientFactory(logger, httpContextAccessor, 3, 3, sp.GetRequiredService<IDnsQuery>());
            });
            services.AddSingleton(sp => sp.GetRequiredService<ResilienceServiceDiscoveryClientFactory>().GetResilienceHttpClient());

            return services;
        }

        /// <summary>
        /// 注册PropertySource
        /// </summary>
        /// <param name="component"></param>
        /// <param name="configuration"></param>
        private static void RegisterPropetySources(ComponentAttribute component, IConfiguration configuration, IServiceCollection services)
        {
            if (!typeof(PropertySourceAttribute).IsAssignableFrom(component.GetType()))
                return;
            var propertySource = component as PropertySourceAttribute;
            var implementationType = propertySource.ImplementationType;
            if (propertySource != null)
            {
                if (string.IsNullOrWhiteSpace(propertySource.Name))
                    propertySource.Name = propertySource.ImplementationType.Name; // 默认使用类名称作为配置节点Key.

                var section = configuration.GetSection(propertySource.Name);

                if (!section.Exists() && !propertySource.IgnoreResourceNotFound)
                    throw new ArgumentNullException($"PropertySource:{propertySource.Name}");

                var instance = Activator.CreateInstance(implementationType);
                configuration.Bind(propertySource.Name, instance);

                component.Instance = instance;
            }
        }

        /// <summary>
        /// 注册组件
        /// </summary>
        /// <param name="component"></param>
        /// <param name="services"></param>
        private static void RegisterComponents(ComponentAttribute component, IServiceCollection services)
        {
            switch (component.LifeTimeScope)
            {
                case ComponentLifeTimeScope.Scoped:
                    if (component.Instance == null)
                        services.AddScoped(component.ServiceType, component.ImplementationType);
                    else
                        services.AddScoped(component.ServiceType, sp => Convert.ChangeType(component.Instance, component.ImplementationType));
                    break;
                case ComponentLifeTimeScope.Singleton:
                    if (component.Instance == null)
                        services.AddSingleton(component.ServiceType, component.ImplementationType);
                    else
                        services.AddSingleton(component.ServiceType, sp => Convert.ChangeType(component.Instance, component.ImplementationType));
                    break;
                default:
                    if (component.Instance == null)
                        services.AddTransient(component.ServiceType, component.ImplementationType);
                    else
                        services.AddTransient(component.ServiceType, sp => Convert.ChangeType(component.Instance, component.ImplementationType));
                    break;
            }
        }
    }
}
