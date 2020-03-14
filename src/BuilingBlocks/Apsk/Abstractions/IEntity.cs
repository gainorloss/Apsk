// <copyright file="IEntity.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Apsk.Abstractions
{
    using System.Collections.Generic;

    public interface IEntity
    {
        IEnumerable<DomainEvent> DomainEvents { get; }

        void ApplyDomainEvent(DomainEvent @event);

        void ClearDomainEvents();
    }
}
