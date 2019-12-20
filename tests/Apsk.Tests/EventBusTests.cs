using Apsk.Abstractions;
using Apsk.Extensions;
using Apsk.Tests.AggregateModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Apsk.Tests
{
    public class EventBusTests
    {
        private  IServiceProvider _sp;
        private  IEventBus _bus;
        public EventBusTests()
        {
            var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", true, true)
                   .Build();

            _sp = new ServiceCollection()
                .AddComponents(configuration)
                .AddBus()
                .BuildServiceProvider();

            _bus = _sp.GetRequiredService<IEventBus>();
            _bus.Subscribe();
        }
        [Fact]
        public async Task Published_Should_Subscribed_NormalAsync()
        {
            var order = new Order() { OrderNo = "27119999", OrderTime = DateTime.Now };
            await _bus.PublishAsync(new OrderCreatedEvent(order));
        }
    }
}
