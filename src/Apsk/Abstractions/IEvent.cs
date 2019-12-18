using System;

namespace Apsk.Abstractions
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime OccuredOn { get; }
    }
}
