// <copyright file="InMemoryEventBus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Apsk
{
    using System;
    using System.Threading.Tasks;
    using Apsk.Abstractions;
    using Apsk.Annotations;
    using Microsoft.Extensions.Logging;

    [Component(ComponentLifeTimeScope.Singleton)]
    public class InMemoryEventBus
        : IEventBus
    {
        private readonly IEventHandlerExecutionContext _ctx;

#if DEBUG
        private readonly ILogger<InMemoryEventBus> _logger;
#endif

        event EventHandler<EventProcessedArgs> EventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryEventBus"/> class.
        /// </summary>
        /// <param name="eventHandlerExecutionContext"></param>
        /// <param name="logger"></param>
        public InMemoryEventBus(
            IEventHandlerExecutionContext eventHandlerExecutionContext
#if DEBUG
            , ILogger<InMemoryEventBus> logger
#endif
            )
        {
            _ctx = eventHandlerExecutionContext;
#if DEBUG
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogCritical("Event bus 初始化...(无线电)");
#endif

            EventHandler += InMemoryEventBus_EventHandler;
        }

        public void Dispose()
        {
            EventHandler -= InMemoryEventBus_EventHandler;
            EventHandler = null;
#if DEBUG
            _logger.LogCritical("Event bus 释放...(无线电)");
#endif
        }

        public Task PublishAsync<T>(T @event) where T : IEvent
        {
            EventHandler.Invoke(this, new EventProcessedArgs(@event));
            return Task.CompletedTask;
        }

        public void Subscribe()
        {
            _ctx.Register();
        }

        private void InMemoryEventBus_EventHandler(object sender, EventProcessedArgs e)
        {
            _ctx.HandleAsync(e.Event);
        }
    }
}
