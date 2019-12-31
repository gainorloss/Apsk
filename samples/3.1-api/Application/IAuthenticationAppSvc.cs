using Apsk.AspNetCore;

namespace _3._1_api.Application
{
    public interface IAuthenticationAppSvc
    {
        RestResult Authenticate();
    }
}
