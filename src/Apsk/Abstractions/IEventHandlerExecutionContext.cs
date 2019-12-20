/************************************************************************************************
 * 
 * 事件处理器执行上下文
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月27日17:27:05】
 * 
 * **********************************************************************************************/
using System;
using System.Threading.Tasks;

namespace Apsk.Abstractions
{
    /// <summary>
    /// 事件处理器执行上下文
    /// </summary>
    public interface IEventHandlerExecutionContext
    {
        #region 注册
        /// <summary>
        /// 注册所有
        /// </summary>
        void Register(Action<Type> registerCallback=null);
        #endregion

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleAsync<T>(T @event) where T : IEvent;

        /// <summary>
        /// 事件Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetEventKey(Type eventType);
    }
}
