using Apsk.AspNetCore;
using Apsk.AspNetCore.Annotations;
using Apsk.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace _3._1_api.Application
{
    [RestController("gateway")]
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
    }
}
