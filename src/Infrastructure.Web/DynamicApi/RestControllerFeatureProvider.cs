using System.Reflection;
using Infrastructure.Web.Annotations;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Infrastructure.Web.DynamicApi
{
    public class RestControllerFeatureProvider
       : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            return typeInfo.GetCustomAttribute<RestControllerAttribute>() != null && typeInfo.IsClass && typeInfo.IsPublic && !typeInfo.IsAbstract;
        }
    }
}
