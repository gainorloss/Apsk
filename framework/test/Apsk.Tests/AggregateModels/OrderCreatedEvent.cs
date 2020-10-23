namespace Apsk.Tests.AggregateModels
{
    public class OrderCreatedEvent
        : DomainEvent
    {
        private OrderCreatedEvent()
        { }
        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }
        public Order Order { get; private set; }
    }
}
