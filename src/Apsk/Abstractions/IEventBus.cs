// <copyright file="IEventBus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Apsk.Abstractions
{
    using System;

    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
        : IEventPublisher, IEventSubscriber, IDisposable
    { }
}
