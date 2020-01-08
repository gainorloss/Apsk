using Apsk.AOP;
using Apsk.AspNetCore;

namespace _3._1_api.Application
{
    public interface IAuthenticationAppSrv
    {
        /// <summary>
        /// Authenticate.
        /// </summary>
        /// <returns></returns>
        RestResult Authenticate();

        /// <summary>
        /// List apis.
        /// </summary>
        /// <returns></returns>

        [Cacheable(Value ="apis")]
        RestResult ListApis();

        /// <summary>
        /// Delete apis.
        /// </summary>
        /// <returns></returns>

        [CacheEnvict(Value = "apis")]
        RestResult DeleteApis();
    }
}
