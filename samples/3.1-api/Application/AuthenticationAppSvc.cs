using Apsk.Annotations;
using Apsk.AspNetCore;
using Apsk.AspNetCore.Annotations;
using Apsk.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace _3._1_api.Application
{
    /// <summary>
    /// Authenticate application service.
    /// </summary>
    [RestController("gateway")]
    [Service]
    public class AuthenticationAppSvc
        : RestController, IAuthenticationAppSvc
    {
        private readonly IAuthenticationManager _authenticationManager;
        public AuthenticationAppSvc(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        /// <inheritdoc/>
        [HttpGet]
        public RestResult Authenticate()
        {
            return Success(_authenticationManager.Authenticate("", ""));
        }

        /// <summary>
        /// Throw exception.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RestResult ThrowException()
        {
            throw new System.Exception();
        }

        /// <summary>
        /// List apis.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RestResult ListApis()
        {
            return Success(new[]{"ListApis"});
        }

        /// <summary>
        /// Delete apis.
        /// </summary>
        /// <returns></returns>
        public RestResult DeleteApis()
        {
            return RestResult.Ok();
        }
    }
}
