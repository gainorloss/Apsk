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
    [RestController("apsk")]
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
        [HttpPost]
        public RestResult Authenticate()
        {
            return Success(_authenticationManager.Authenticate("", ""));
        }

        /// <inheritdoc/>
        public RestResult ThrowException()
        {
            throw new System.Exception();
        }

        /// <inheritdoc/>
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
