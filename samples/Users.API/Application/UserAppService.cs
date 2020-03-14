using Apsk.AspNetCore;
using Apsk.AspNetCore.Annotations;

namespace Users.API.Application
{
    [RestController()]
    public class UserAppService
        : RestController,IUserAppService
    {
        public RestResult GetName() => Success("apsk");
    }
}
