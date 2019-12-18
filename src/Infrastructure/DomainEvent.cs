﻿/************************************************************************************************
 * 
 * 领域事件
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019-12-11 14:27:18】
 * 
 * **********************************************************************************************/
using Infrastructure.Abstractions;
using System;

namespace Infrastructure
{
    public abstract class DomainEvent
        : IEvent
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = DateTime.UtcNow;
        }
        public Guid Id { get; private set; }
        public DateTime OccuredOn { get; private set; }
    }
}
