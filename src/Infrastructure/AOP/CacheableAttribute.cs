using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.AOP
{
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
