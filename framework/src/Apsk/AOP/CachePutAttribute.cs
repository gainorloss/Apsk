// <copyright file="CachePutAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System.Threading.Tasks;
    using AspectCore.DynamicProxy;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// 设置缓存AOP拦截器.
    /// </summary>
    public class CachePutAttribute
        : AbstractCacheOperationAttribute
    {
        protected override async Task OperateCache(AspectContext context, AspectDelegate next, string cacheKey)
        {
            await next(context);
            MemoryCache.Set(cacheKey, context.ReturnValue);
        }
    }
}
