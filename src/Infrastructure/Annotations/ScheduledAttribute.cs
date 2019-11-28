using System;

namespace Infrastructure.Annotations
{
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
