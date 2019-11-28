/************************************************************************************************
 * 
 * 时间存储器
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月27日17:25:04】
 * 
 * **********************************************************************************************/
using System;
using System.Threading.Tasks;

namespace Infrastructure.Bus.Abstractions
{
    /// <summary>
    /// 事件存储器
    /// </summary>
    public interface IEventStorage : IDisposable
    {
        /// <summary>
        /// 事件持久化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task SaveEventAsync<T>(T @event) where T : IEvent;
    }
}
