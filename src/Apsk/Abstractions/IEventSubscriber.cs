﻿/************************************************************************************************
 * 
 * 事件订阅器
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:35:01】
 * 
 * **********************************************************************************************/
namespace Apsk.Abstractions
{
    public interface IEventSubscriber
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        void Subscribe();
    }
}