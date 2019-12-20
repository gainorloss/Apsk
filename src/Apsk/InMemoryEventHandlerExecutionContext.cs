
/************************************************************************************************
 * 
 * 事件处理器执行上下文 内存
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月27日17:27:05】
 * 
 * **********************************************************************************************/
using Apsk.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsk
{
    public class InMemoryEventHandlerExecutionContext
        : IEventHandlerExecutionContext
    {
        private readonly ConcurrentDictionary<Type, List<Type>> _registrations = new ConcurrentDictionary<Type, List<Type>>();
        private readonly IServiceCollection _services;
        private readonly Func<IServiceCollection, IServiceProvider> _serviceProviderFactory;
        public InMemoryEventHandlerExecutionContext(IServiceCollection services)
        {
            _services = services;
            _serviceProviderFactory = (svcs => services.BuildServiceProvider());
        }
        public string GetEventKey(Type eventType) => eventType.Name.ToLowerInvariant();

        public Task HandleAsync<T>(T @event) where T : IEvent
        {
            var eventType = @event.GetType();
            if (_registrations.TryGetValue(eventType, out var handlerTypes) && handlerTypes.Any())
            {
                var sp = _serviceProviderFactory(_services);

                using (var scope = sp.CreateScope())
                {
                    var _sp = scope.ServiceProvider;

                    foreach (var handlerType in handlerTypes)
                    {
                        var handler = _sp.GetRequiredService(handlerType);

                        var genericType = typeof(IEventHandler<>).MakeGenericType(eventType.UnderlyingSystemType);
                        var method = genericType.GetMethod("HandleAsync");
                        method.Invoke(handler, new object[] { @event });
                    }
                }
            }
            return Task.CompletedTask;
        }

        public void Register(Action<Type> registerCallback = null)
        {
            var implementationTypes = _services.Where(s => s.ImplementationType != null).Select(s => s.ImplementationType);
            foreach (var implementationType in implementationTypes)
            {
                var handlerType = implementationType.GetInterface("IEventHandler`1");
                if (handlerType == null)
                    continue;

                var args = handlerType.GetGenericArguments();
                if (args.Length != 1)
                    continue;

                var eventType = args[0];

#if DEBUG
                Console.WriteLine($"【+$EventBus】:{eventType.FullName} ===>{implementationType.FullName}(无线电)");
#endif
                Register(eventType, implementationType);

                registerCallback?.Invoke(eventType);
            }
        }

        private void Register(Type eventType, Type handlerType)
        {
            if (_registrations.TryGetValue(eventType, out var handlers) && handlers.Contains(handlerType))
                return;

            if (handlers == null)
            {
                _registrations.TryAdd(eventType, new List<Type> { handlerType });

                return;
            }

            if (!handlers.Contains(handlerType))
            {
                handlers.Add(handlerType);
                return;
            }
        }
    }
}
