/************************************************************************************************
 * 
 * 实体接口 主要用于上下文实体跟踪抽象出来的
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月27日17:25:04】
 * 
 * **********************************************************************************************/
using System.Collections.Generic;

namespace Apsk.Abstractions
{
    public interface IEntity
    {
        IEnumerable<DomainEvent> DomainEvents { get; }

        void ApplyDomainEvent(DomainEvent @event);

        void ClearDomainEvents();
    }
}
