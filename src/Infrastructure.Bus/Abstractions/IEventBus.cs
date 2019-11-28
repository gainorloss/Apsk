/************************************************************************************************
 * 
 * 事件总线
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:31:33】
 * 
 * **********************************************************************************************/
using System;

namespace Infrastructure.Bus.Abstractions
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
        : IEventPublisher, IEventSubscriber, IDisposable
    { }
}
