// <copyright file="RestControllerFeatureProvider.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.DynamicApi
{
    using System.Reflection;
    using Apsk.AspNetCore.Annotations;
    using Microsoft.AspNetCore.Mvc.Controllers;

    /// <summary>
    /// Rest controller feature provider.
    /// </summary>
    public class RestControllerFeatureProvider
       : ControllerFeatureProvider
    {
        /// <inheritdoc/>
        protected override bool IsController(TypeInfo typeInfo)
        {
            return typeInfo.GetCustomAttribute<RestControllerAttribute>() != null && typeInfo.IsClass && typeInfo.IsPublic && !typeInfo.IsAbstract;
        }
    }
}
