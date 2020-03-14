// <copyright file="IEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System;

    public interface IEvent
    {
        Guid Id { get; }

        DateTime OccuredOn { get; }
    }
}
