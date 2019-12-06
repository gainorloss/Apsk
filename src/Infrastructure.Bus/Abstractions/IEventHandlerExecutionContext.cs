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

namespace Infrastructure.Bus.Abstractions
{
    /// <summary>
    /// 事件处理器执行上下文
    /// </summary>
    public interface IEventHandlerExecutionContext
    {
        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void Register<T, TH>();

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handlerType"></param>
        void Register(Type eventType, Type handlerType);

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
        string GetEventKey<T>();

        /// <summary>
        /// 事件Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetEventKey(Type eventType);
    }
}
