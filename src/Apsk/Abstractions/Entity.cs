/******************************************************************
 * 
 *   实体
 *   
 *   Creator: gainorloss
 * CreatedAt:【2019-12-11 13:54:21】
 */

using System.Collections.Generic;

namespace Apsk.Abstractions
{
    public abstract class Entity<ID> : IEntity
    {
        public ID Id { get; set; }

        private IList<DomainEvent> _domainEvents;

        public IEnumerable<DomainEvent> DomainEvents => _domainEvents;

        public void ApplyDomainEvent(DomainEvent @event)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();

            if (!_domainEvents.Contains(@event))
                _domainEvents.Add(@event);
        }

        public void ClearDomainEvents()
        {
            if (_domainEvents == null)
                return;
            _domainEvents.Clear();
        }
    }
}
