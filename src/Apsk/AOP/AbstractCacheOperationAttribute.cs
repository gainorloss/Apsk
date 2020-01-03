// <copyright file="AbstractCacheOperationAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AspectCore.DependencyInjection;
    using AspectCore.DynamicProxy;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// 缓存操作拦截器基类.
    /// </summary>
    public abstract class AbstractCacheOperationAttribute
        : AbstractInterceptorAttribute
    {
        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets key.
        /// </summary>
        public string Key { get; set; } = "{0}";

        [FromServiceContext]
        protected IMemoryCache MemoryCache { get; set; }


        [FromServiceContext]
        protected ILogger<CacheableAttribute> Logger { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheKey = GenerateKey(context);

            await OperateCache(context, next, cacheKey);
        }

        protected abstract Task OperateCache(AspectContext context, AspectDelegate next, string cacheKey);

        /// <summary>
        /// 生成缓存的键.
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
