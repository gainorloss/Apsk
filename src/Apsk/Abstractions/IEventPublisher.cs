// <copyright file="IEventPublisher.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System.Threading.Tasks;

    public interface IEventPublisher
    {
        /// <summary>
        /// 派发事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task PublishAsync<T>(T @event)
            where T : IEvent;
    }
}
