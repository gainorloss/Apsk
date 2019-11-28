using Infrastructure.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Infrastructure.ConsoleApp
{
    public class BootstrapClassLoader
    {
        private static Dictionary<Type, IEnumerable<Object>> TypeMappingContainer = new Dictionary<Type, IEnumerable<Object>>();
        public static IEnumerable<Object> GetServiceType(Type serviceType)
        {
            if (TypeMappingContainer.TryGetValue(serviceType, out var implementationTypes))
                return implementationTypes;

            var sp = new ServiceCollection()
              .AddTransient<IUsrService, UsrService>()
              .BuildServiceProvider();

            var usrSrv = sp.GetRequiredService<IUsrService>();
            TypeMappingContainer.Add(serviceType, new[] { usrSrv });
            return new[] { usrSrv };
        }
    }
}
