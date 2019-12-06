using BootSharp.API.AppSettings;
using BootSharp.API.Models;
using Infrastructure.Annotations;
using Infrastructure.Web;
using Infrastructure.Web.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BootSharp.API.Application
{
    [Service]
    [RestController(Scene = "Phoneix", Separator = ".")]
    public class UserAppService
        : RestController, IUserAppService
    {
        private readonly MySqlConnectionSetting _mySqlConnectionSetting;
        public UserAppService(MySqlConnectionSetting mySqlConnectionSetting)
        {
            _mySqlConnectionSetting = mySqlConnectionSetting;
        }
        [HttpGet]
        public Task<RestResult> GetUserNameAsync()
        {
            var rt = Success(new User("Zhang", "Jian"));
            return Task.FromResult(rt);
        }
    }
}
