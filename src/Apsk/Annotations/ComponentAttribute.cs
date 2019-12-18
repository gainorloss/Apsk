using System;

namespace Apsk.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentAttribute
        : Attribute
    {
        public ComponentAttribute(ComponentLifeTimeScope lifeTimeScope = ComponentLifeTimeScope.Transient)
        {
            LifeTimeScope = lifeTimeScope;
        }
        public string Key { get; set; }

        public ComponentLifeTimeScope LifeTimeScope { get; set; }

        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }

        public object Instance { get; set; }
    }
}
