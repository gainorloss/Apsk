// <copyright file="EventProcessedArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using Apsk.Abstractions;

    internal class EventProcessedArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessedArgs"/> class.
        /// </summary>
        /// <param name="event"></param>
        public EventProcessedArgs(IEvent @event)
        {
            Event = @event;
        }

        public IEvent Event { get; private set; }
    }
}