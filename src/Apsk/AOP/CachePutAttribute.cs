using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Apsk.AOP
{
    /// <summary>
    /// 设置缓存AOP拦截器
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
