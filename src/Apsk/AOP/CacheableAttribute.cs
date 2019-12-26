// <copyright file="CacheableAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System.Threading.Tasks;
    using AspectCore.DynamicProxy;
    using Microsoft.Extensions.Caching.Memory;

    public class CacheableAttribute
        : AbstractCacheOperationAttribute
    {
        protected override async Task OperateCache(AspectContext context, AspectDelegate next, string cacheKey)
        {
            if (!MemoryCache.TryGetValue(cacheKey, out var cacheVal))
            {
                await next(context);
                MemoryCache.Set(cacheKey,context.ReturnValue);
                return;
            }

            context.ReturnValue = cacheVal;
        }
    }
}
