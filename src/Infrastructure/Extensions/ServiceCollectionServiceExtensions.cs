using AspectCore.Extensions.DependencyInjection;
using Infrastructure.Abstractions;
using Infrastructure.Annotations;
using Infrastructure.HostedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionServiceExtensions
    {
        private readonly static IEnumerable<ComponentAttribute> components = new List<ComponentAttribute>();
        static ServiceCollectionServiceExtensions()
        {
            components = BootstrapClassLoader.LoadComponents();

            //serilog 设置
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.Console()
             .WriteTo.File("logs/log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
             .CreateLogger();
        }

        /// <summary>
        /// 添加组件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddComponents(this IServiceCollection services, IConfiguration configuration)
        {
            //日志注册
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

            //AOP注册
            services.ConfigureDynamicProxy();
            //内存缓存
            services.AddMemoryCache();

            //orleans
            services.AddSingleton<ClientHostedService>();
            services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<ClientHostedService>());
            services.AddSingleton(sp => sp.GetRequiredService<ClientHostedService>().ClusterClient);

            foreach (var component in components)
            {
                RegisterPropetySources(component, configuration);
                RegisterComponents(component, services);
            }

            //quartz
            services.AddHostedService<QuartzHostedService>();
            return services;
        }

        public static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerExecutionContext>(new InMemoryEventHandlerExecutionContext(services));
            return services;
        }

        /// <summary>
        /// 注册PropertySource
        /// </summary>
        /// <param name="component"></param>
        /// <param name="configuration"></param>
        private static void RegisterPropetySources(ComponentAttribute component, IConfiguration configuration)
        {
            if (!typeof(PropertySourceAttribute).IsAssignableFrom(component.GetType()))
                return;
            var propertySource = component as PropertySourceAttribute;
            var implementationType = propertySource.ImplementationType;
            if (propertySource != null)
            {
                if (string.IsNullOrWhiteSpace(propertySource.Name))
                    propertySource.Name = propertySource.ImplementationType.Name;//默认使用类名称作为配置节点Key

                var section = configuration.GetSection(propertySource.Name);

                if (!section.Exists() && !propertySource.IgnoreResourceNotFound)
                    throw new ArgumentNullException($"PropertySource:{propertySource.Name}");

                var props = implementationType.GetProperties();

                var instance = Activator.CreateInstance(implementationType);
                foreach (var prop in props)
                {
                    var val = section[prop.Name];
                    if (val != null)
                        prop.SetValue(instance, Convert.ChangeType(val, prop.PropertyType));
                }

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
