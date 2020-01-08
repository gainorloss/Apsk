using Apsk.AOP;
using Apsk.AspNetCore;

namespace _3._1_api.Application
{
    public interface IAuthenticationAppSvc
    {
        /// <summary>
        /// Authenticate.
        /// </summary>
        /// <returns></returns>
        RestResult Authenticate();

        [Cacheable(Value ="apis")]
        RestResult ListApis();

        [CacheEnvict(Value = "apis")]
        RestResult DeleteApis();
    }
}
