using Infrastructure.Bus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Bus.Extensions
{
    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerExecutionContext>(new InMemoryEventHandlerExecutionContext(services));
            return services;
        }
    }
}
