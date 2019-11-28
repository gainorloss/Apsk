using System;
using Infrastructure.Bus.Abstractions;

namespace BootSharp.ConsoleApp.AggregatesModel
{
    public class PersonCreatedEvent
        : IEvent
    {
        public Guid Id { get; set; }
        public DateTime OccuredOn { get; set; }
    }
}
