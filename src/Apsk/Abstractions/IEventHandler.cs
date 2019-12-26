// <copyright file="IEventHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/************************************************************************************************
 *
 * 事件处理器
 *
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:32:53】
 *
 * **********************************************************************************************/
namespace Apsk.Abstractions
{
    using System.Threading.Tasks;

    public interface IEventHandler<T>
        where T : IEvent
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task HandleAsync(T @event);
    }
}
