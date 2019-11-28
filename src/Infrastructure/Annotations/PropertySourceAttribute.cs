using System;

namespace Infrastructure.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PropertySourceAttribute
        : ComponentAttribute
    {
        public PropertySourceAttribute()
        {
            LifeTimeScope = ComponentLifeTimeScope.Singleton;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IgnoreResourceNotFound { get; set; } = false;
        public string Encoding { get; set; }
    }
}
