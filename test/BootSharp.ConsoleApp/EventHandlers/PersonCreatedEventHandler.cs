using BootSharp.ConsoleApp.AggregatesModel;
using Infrastructure.Bus;
using Infrastructure.Bus.Annotations;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp.EventHandlers
{
    [EventHandler(ServiceType = typeof(PersonCreatedEventHandler))]
    public class PersonCreatedEventHandler
         : DomainEventHandler<PersonCreatedEvent>
    {
        public override Task HandleAsync(PersonCreatedEvent @event)
        {
            System.Console.WriteLine("【$PersonCreatedEvent】:Person created...(无线电)********");
            return Task.CompletedTask;
        }
    }
}
