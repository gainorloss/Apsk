using System;

namespace Infrastructure.Abstractions
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime OccuredOn { get; }
    }
}
