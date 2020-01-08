// <copyright file="PropertySourceAttribute.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PropertySourceAttribute
        : ComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySourceAttribute"/> class.
        /// </summary>
        public PropertySourceAttribute()
        {
            LifeTimeScope = ComponentLifeTimeScope.Singleton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySourceAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
        public PropertySourceAttribute(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ignoreResourceNotFound.
        /// </summary>
        public bool IgnoreResourceNotFound { get; set; } = false;

        /// <summary>
        /// Gets or sets encoding.
        /// </summary>
        public string Encoding { get; set; }
    }
}
