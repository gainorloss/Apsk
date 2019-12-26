// <copyright file="Entity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System.Collections.Generic;

    public abstract class Entity<TID> : IEntity
    {
        private IList<DomainEvent> domainEvents;

        /// <summary>
        /// Gets or sets iD.
        /// </summary>
        public TID Id { get; set; }

        public IEnumerable<DomainEvent> DomainEvents => domainEvents;

        /// <inheritdoc/>
        public void ApplyDomainEvent(DomainEvent @event)
        {
            domainEvents = domainEvents ?? new List<DomainEvent>();

            if (!domainEvents.Contains(@event))
                domainEvents.Add(@event);
        }

        /// <inheritdoc/>
        public void ClearDomainEvents()
        {
            if (domainEvents == null)
                return;
            domainEvents.Clear();
        }
    }
}
