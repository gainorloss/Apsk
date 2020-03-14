// <copyright file="QuartzHostedService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.HostedServices
{
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Apsk.Annotations;
    using Microsoft.Extensions.Hosting;
    using Quartz;
    using Quartz.Impl;

    public class QuartzHostedService
        : IHostedService
    {
        private IScheduler _scheduler;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            _scheduler = await factory.GetScheduler();

            var components = BootstrapClassLoader.LoadComponents();

            foreach (var component in components)
            {
                if (!component.ServiceType.IsAssignableFrom(typeof(IJob))) continue;

                var implementationType = component.ImplementationType;

                var method = implementationType.GetMethod("Execute", new[] { typeof(IJobExecutionContext) });

                if (method == null) continue;

                var scheduled = method.GetCustomAttribute<ScheduledAttribute>();
                if (scheduled == null) continue;

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create(implementationType)
                    .WithIdentity($"job{implementationType.FullName}", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity($"trigger{implementationType.FullName}", "group1")
                    .WithCronSchedule(scheduled.Cron)
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await _scheduler.ScheduleJob(job, trigger);
            }

            if (!_scheduler.IsStarted || _scheduler.IsShutdown)
                await _scheduler.Start();
            // and start it off
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (!_scheduler.IsShutdown)
                await _scheduler.Shutdown();
        }
    }
}
