﻿// <copyright file="ApskDbContext.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using System.Linq;
    using System.Threading.Tasks;
    using Apsk.Abstractions;
    using Microsoft.EntityFrameworkCore;

    public class ApskDbContext
         : DbContext, IUnitOfWork
    {
        private readonly IEventBus _eventBus;

        public ApskDbContext(DbContextOptions options)
            : base(options)
        { }

        public ApskDbContext(DbContextOptions options, IEventBus eventBus)
           : base(options)
        {
            _eventBus = eventBus;
        }

        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }

        public async Task<int> SaveEntitiesAsync()
        {
            var entities = ChangeTracker.Entries<IEntity>().Where(entry => entry.Entity.DomainEvents != null && entry.Entity.DomainEvents.Any()).Select(entry => entry.Entity);

            var events = entities.SelectMany(entity => entity.DomainEvents);
            var tasks = events.ToList().Select(async @event => await _eventBus.PublishAsync(@event));
            Task.WaitAll(tasks.ToArray());

            entities.ToList().ForEach(entity => entity.ClearDomainEvents());
            return await SaveAsync();
        }
    }
}
