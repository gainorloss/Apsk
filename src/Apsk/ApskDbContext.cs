/************************************************************************************************
 * 
 * Apsk EntityframeworkCore上下文基类
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019-12-24 16:31:43】
 * 
 * **********************************************************************************************/
using Apsk.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Apsk
{
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
            return await base.SaveChangesAsync();
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
