// <copyright file="ComponentAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentAttribute
        : Attribute
    {
        public ComponentAttribute(ComponentLifeTimeScope lifeTimeScope = ComponentLifeTimeScope.Transient)
        {
            LifeTimeScope = lifeTimeScope;
        }

        /// <summary>
        /// Gets or sets key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets lifeTimeScope.
        /// </summary>
        public ComponentLifeTimeScope LifeTimeScope { get; set; }

        /// <summary>
        /// Gets or sets serviceType.
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Gets or sets implementationType.
        /// </summary>
        public Type ImplementationType { get; set; }

        /// <summary>
        /// Gets or sets instance.
        /// </summary>
        public object Instance { get; set; }
    }
}
