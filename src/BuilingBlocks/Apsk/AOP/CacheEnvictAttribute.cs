// <copyright file="CacheEnvictAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System.Threading.Tasks;
    using AspectCore.DependencyInjection;
    using AspectCore.DynamicProxy;
    using Microsoft.Extensions.Logging;

    public class CacheEnvictAttribute
        : AbstractCacheOperationAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether allEntries.
        /// </summary>
        public bool AllEntries { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether beforeInvocation.
        /// </summary>
        public bool BeforeInvocation { get; set; } = true;

        protected override async Task OperateCache(AspectContext context, AspectDelegate next, string cacheKey)
        {
            if (BeforeInvocation)
                RemoveCacheEntries(cacheKey);

            await next(context);

            if (!BeforeInvocation)
                RemoveCacheEntries(cacheKey);

            Logger.LogDebug($"清除缓存:{cacheKey}");
        }

        private void RemoveCacheEntries(string cacheKey)
        {
            MemoryCache.Remove(cacheKey);
        }
    }
}
