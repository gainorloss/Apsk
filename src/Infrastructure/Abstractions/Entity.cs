/******************************************************************
 * 
 *   实体
 *   
 *   Creator: gainorloss
 * CreatedAt:【2019-12-11 13:54:21】
 */

using Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.Abstractions
{
    public abstract class Entity<ID>
    {
        public ID Id { get; set; }

        private IList<DomainEvent> _domainEvents;

        public IEnumerable<DomainEvent> DomainEvents => _domainEvents;

        protected void ApplyDomainEvent(DomainEvent @event)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();

            if (!_domainEvents.Contains(@event))
                _domainEvents.Add(@event);
        }

        protected void ClearDomainEvents()
        {
            if (_domainEvents == null)
                return;
            _domainEvents.Clear();
        }
    }
}
