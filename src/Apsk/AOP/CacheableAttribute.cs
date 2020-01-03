// <copyright file="CacheableAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System.Threading.Tasks;
    using AspectCore.DependencyInjection;
    using AspectCore.DynamicProxy;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    public class CacheableAttribute
        : AbstractCacheOperationAttribute
    {
        /// <inheritdoc/>
        protected override async Task OperateCache(AspectContext context, AspectDelegate next, string cacheKey)
        {
            if (!MemoryCache.TryGetValue(cacheKey, out var cacheVal))
            {
                await next(context);
                MemoryCache.Set(cacheKey, context.ReturnValue);
                return;
            }

            Logger.LogDebug($"缓存命中:{cacheKey}");
            context.ReturnValue = cacheVal;
        }
    }
}
