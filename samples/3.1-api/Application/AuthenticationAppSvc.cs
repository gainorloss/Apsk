using Apsk.Annotations;
using Apsk.AspNetCore;
using Apsk.AspNetCore.Annotations;
using Apsk.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace _3._1_api.Application
{
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
        [HttpGet]
        public RestResult Authenticate()
        {
            return Success(_authenticationManager.Authenticate("", ""));
        }

        [HttpGet]
        public RestResult ThrowException()
        {
            throw new System.Exception();
        }

        [HttpGet]
        public RestResult ListApis()
        {
            return Success(new[]{"ListApis"});
        }

        public RestResult DeleteApis()
        {
            return RestResult.Ok();
        }
    }
}
