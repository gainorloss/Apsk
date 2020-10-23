// <copyright file="ScheduledAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ScheduledAttribute
        : Attribute
    {
        /// <summary>
        /// cron expression.
        /// </summary>
        public string Cron { get; set; }
    }
}
