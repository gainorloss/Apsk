using Infrastructure.Bus.Abstractions;
using System;

namespace BootSharp.ConsoleApp.AggregatesModel
{
    public class PersonNameModifiedEvent
        : IEvent
    {
        public Guid Id { get; set; }
        public DateTime OccuredOn { get; set; }
    }
}
