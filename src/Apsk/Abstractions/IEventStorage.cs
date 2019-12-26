// <copyright file="IEventStorage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 事件存储器.
    /// </summary>
    public interface IEventStorage : IDisposable
    {
        /// <summary>
        /// 事件持久化.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task StoreAsync<T>(T @event) where T : IEvent;

        /// <summary>
        /// 事件持久化.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task LoadAsync<T>(T @event) where T : IEvent;
    }
}
