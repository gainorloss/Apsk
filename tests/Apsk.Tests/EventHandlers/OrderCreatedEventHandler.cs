using Apsk.Abstractions;
using Apsk.Annotations;
using Apsk.Tests.AggregateModels;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Apsk.Tests.EventHandlers
{
    [EventHandler(ServiceType = typeof(OrderCreatedEventHandler))]
    public class OrderCreatedEventHandler
        : IEventHandler<OrderCreatedEvent>
    {
        public Task HandleAsync(OrderCreatedEvent @event)
        {
            var ret=$"{@event.Order.OrderNo}-{@event.Order.OrderTime}";
            return Task.CompletedTask;
        }
    }
}
