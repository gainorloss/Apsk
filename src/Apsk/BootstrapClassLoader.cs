// <copyright file="BootstrapClassLoader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;
    using Apsk.Annotations;
    using Microsoft.Extensions.DependencyModel;

    public class BootstrapClassLoader
    {
        private static IEnumerable<ComponentAttribute> components;
        private static readonly object @lock = new object();

        public static IEnumerable<ComponentAttribute> LoadComponents()
        {
            if (components == null)
            {
                lock (@lock)
                {
                    if (components == null)
                    {
                        var libs = DependencyContext.Default.CompileLibraries.Where(lib => lib.Type.Equals("project"));

                        var asmLoadContext = AssemblyLoadContext.Default;
                        var asms = libs.Select(lib => asmLoadContext.LoadFromAssemblyName(new AssemblyName(lib.Name)));

                        var implementationTypes = asms.SelectMany(asm => asm.GetTypes().Where(type => type.IsClass
                               && type.IsPublic
                               && !type.IsAbstract
                               && type.GetCustomAttribute<ComponentAttribute>() != null));

                        components = implementationTypes.Select(implementationType =>
                        {
                            var component = implementationType.GetCustomAttribute<ComponentAttribute>();

                            if (string.IsNullOrWhiteSpace(component.Key))
                                component.Key = implementationType.FullName;

                            var serviceTypes = implementationType.GetInterfaces();
                            component.ImplementationType = implementationType;

                            if (component.ServiceType == null)
                                component.ServiceType = serviceTypes != null && serviceTypes.Any() ? serviceTypes.FirstOrDefault() : implementationType;

#if DEBUG
                            System.Console.WriteLine($"【+$Component】:{component.Key} ===>{component.ServiceType},{Enum.GetName(typeof(ComponentLifeTimeScope), component.LifeTimeScope)}（无线电）");
#endif
                            return component;
                        });
                    }
                }
            }

            return components;
        }
    }
}
