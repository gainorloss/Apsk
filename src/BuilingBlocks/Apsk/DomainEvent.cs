// <copyright file="DomainEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Apsk
{
    using System;
    using Apsk.Abstractions;

    public abstract class DomainEvent
        : IEvent
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public Guid Id { get; private set; }

        /// <inheritdoc/>
        public DateTime OccuredOn { get; private set; }
    }
}
