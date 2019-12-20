/************************************************************************************************
 * 
 * 内存事件总线
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:36:38】
 * 
 * **********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apsk.Annotations;
using Apsk.Abstractions;
using Microsoft.Extensions.Logging;

namespace Apsk
{
    [Component(ComponentLifeTimeScope.Singleton)]
    public class InMemoryEventBus
        : IEventBus
    {
        event EventHandler<EventProcessedArgs> EventHandler;
        private readonly IEventHandlerExecutionContext _ctx;
#if DEBUG
        private readonly ILogger<InMemoryEventBus> _logger;
#endif

        public InMemoryEventBus(IEventHandlerExecutionContext eventHandlerExecutionContext
#if DEBUG
            , ILogger<InMemoryEventBus> logger
#endif
            )
        {
            _ctx = eventHandlerExecutionContext;
#if DEBUG
            _logger = logger;

            _logger.LogCritical("Event bus 初始化...(无线电)");
#endif

            EventHandler += InMemoryEventBus_EventHandler;
        }

        private void InMemoryEventBus_EventHandler(object sender, EventProcessedArgs e)
        {
            _ctx.HandleAsync(e.Event);
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
    }
}
