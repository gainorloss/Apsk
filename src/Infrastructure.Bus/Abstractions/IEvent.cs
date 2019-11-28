using System;

namespace Infrastructure.Bus.Abstractions
{
    public interface IEvent
    {
        Guid Id { get; set; }
        DateTime OccuredOn { get; set; }
    }
}
