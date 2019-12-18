using System.Reflection;
using Apsk.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Apsk.AspNetCore.DynamicApi
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
