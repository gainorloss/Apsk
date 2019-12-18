using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Apsk.AOP
{
    public class CacheEnvictAttribute
        : AbstractCacheOperationAttribute
    {
        public bool AllEntries { get; set; } = false;
        public bool BeforeInvocation { get; set; } = true;
        protected override async Task OperateCache(AspectContext context, AspectDelegate next, string cacheKey)
        {
            if (BeforeInvocation)
                RemoveCacheEntries(cacheKey);

            await next(context);

            if (!BeforeInvocation)
                RemoveCacheEntries(cacheKey);
        }

        private void RemoveCacheEntries(string cacheKey)
        {
            MemoryCache.Remove(cacheKey);
        }
    }
}
