/************************************************************************************************
 * 
 * 事件处理器
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:32:53】
 * 
 * **********************************************************************************************/
using System.Threading.Tasks;

namespace Apsk.Abstractions
{
    public interface IEventHandler
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleAsync(IEvent @event);
    }

    public interface IEventHandler<T> : IEventHandler
        where T : IEvent
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleAsync(T @event);

    }
}
