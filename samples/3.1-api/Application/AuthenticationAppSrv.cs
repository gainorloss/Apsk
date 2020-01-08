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
    public class AuthenticationAppSrv
        : RestController, IAuthenticationAppSrv
    {
        private readonly IAuthenticationManager _authenticationManager;
        public AuthenticationAppSrv(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        /// <inheritdoc/>
        [HttpGet]
        public RestResult Authenticate()
        {
            return Success(_authenticationManager.Authenticate("", ""));
        }

        /// <inheritdoc/>
        [HttpGet]
        public RestResult ThrowException()
        {
            throw new System.Exception();
        }

        /// <inheritdoc/>
        [HttpGet]
        public RestResult ListApis()
        {
            return Success(new[]{"ListApis"});
        }

        /// <inheritdoc/>
        public RestResult DeleteApis()
        {
            return RestResult.Ok();
        }
    }
}
