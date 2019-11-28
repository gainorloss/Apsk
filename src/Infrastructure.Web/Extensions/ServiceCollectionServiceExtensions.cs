using Infrastructure.Web.DynamicApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Infrastructure.Web.Extensions
{
    public static class ServiceCollectionServiceExtensions
    {
        public static void AddRestControllers(this IServiceCollection services)
        {
            var service = services.FirstOrDefault(srv => srv.ServiceType == typeof(ApplicationPartManager));
            if (service == null || service.ImplementationInstance == null)
                throw new System.Exception($"尚未注册{nameof(ApplicationPartManager)}");

            var applicationPartMgr = service.ImplementationInstance as ApplicationPartManager;
            applicationPartMgr.FeatureProviders.Add(new RestControllerFeatureProvider());

            services.Configure<MvcOptions>(opt =>opt.Conventions.Add(new RestControllerConvertion()));
        }
    }
}