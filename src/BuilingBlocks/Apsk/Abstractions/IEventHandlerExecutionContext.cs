// <copyright file="IEventHandlerExecutionContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 事件处理器执行上下文.
    /// </summary>
    public interface IEventHandlerExecutionContext
    {
        /// <summary>
        /// 注册所有.
        /// </summary>
        void Register(Action<Type> registerCallback = null);

        /// <summary>
        /// 处理事件.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task HandleAsync<T>(T @event)
            where T : IEvent;

        /// <summary>
        /// 事件Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetEventKey(Type eventType);
    }
}
