using BootSharp.ConsoleApp.AggregatesModel;
using Infrastructure.Bus.Abstractions;
using Infrastructure.Bus.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp.EventHandlers
{
    [EventHandler]
    public class PersonCreatedEventHandler
         : IEventHandler<PersonCreatedEvent>, INotificationHandler<PersonCreatedNotification>
    {
        public Task HandleAsync(PersonCreatedEvent @event)
        {
            System.Console.WriteLine("【$PersonCreatedEvent】:Person created...(无线电)");
            return Task.CompletedTask;
        }

        public Task HandleAsync<T>(T @event) where T : IEvent
        {
            return Task.CompletedTask;
        }

        Task INotificationHandler<PersonCreatedNotification>.Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
