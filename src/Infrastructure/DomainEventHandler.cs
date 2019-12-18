/************************************************************************************************
 * 
 * 领域事件处理器
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月29日16:32:04】
 * 
 * **********************************************************************************************/
using Infrastructure.Abstractions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class DomainEventHandler<T>
            : IEventHandler<T>
            where T : IEvent
    {
        public abstract Task HandleAsync(T @event);

        public async Task HandleAsync(IEvent @event)
        {
            await HandleAsync((T)@event);
        }
    }
}
