using Apsk.AspNetCore.Annotations;

namespace Users.API.Application
{
    [RestController()]
    public class UserAppService
        : IUserAppService
    {
        public string GetName() => "apsk";
    }
}
