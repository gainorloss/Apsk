/************************************************************************************************
 * 
 * 事件派发器
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:34:35】
 * 
 * **********************************************************************************************/
using System.Threading.Tasks;

namespace Infrastructure.Bus.Abstractions
{
    public interface IEventPublisher
    {
        /// <summary>
        /// 派发事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task PublishAsync<T>(T @event) where T : IEvent;

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        void Publish<T>(T @event) where T : IEvent;
    }
}
