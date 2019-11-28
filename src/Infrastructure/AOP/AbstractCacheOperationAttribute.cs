using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Infrastructure.AOP
{
    /// <summary>
    /// 缓存操作拦截器基类
    /// </summary>
    public abstract class AbstractCacheOperationAttribute
        : AbstractInterceptorAttribute
    {
        public string Value { get; set; }
        public string Key { get; set; } = "{0}";

        [FromContainer]
        protected IMemoryCache MemoryCache { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheKey = GenerateKey(context);

            await OperateCache(context, next, cacheKey);
        }

        protected abstract Task OperateCache(AspectContext context, AspectDelegate next, string cacheKey);

        /// <summary>
        /// 生成缓存的键
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GenerateKey(AspectContext context)
        {
            var method = context.ServiceMethod;
            var parameters = context.Parameters;

            var methodName = context.ServiceMethod.Name;
            var paras = method.GetParameters().Select((para, i) =>
            {
                if (para.GetType().IsAssignableFrom(typeof(ValueTuple)))
                    return $"{para.Name}:{parameters[i]}";
                else
                    return $"{para.Name}:{JsonConvert.SerializeObject(parameters[i])}";
            });

            var args = new List<object>();
            args.Add(Value);
            args.Add(methodName);
            args.AddRange(paras);

            var cacheKey = string.Format(Key, args.ToArray());
            return cacheKey;
        }
    }
}
