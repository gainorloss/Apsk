using BootSharp.API.AppSettings;
using Infrastructure.Web.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BootSharp.API.Application
{
    [RestController(Scene = "Phoneix", Separator = ".")]
    public class UserAppService
        : IUserAppService
    {
        private readonly MySqlConnectionSetting _mySqlConnectionSetting;
        public UserAppService(MySqlConnectionSetting mySqlConnectionSetting)
        {
            _mySqlConnectionSetting = mySqlConnectionSetting;
        }
        [HttpGet]
        public Task<string> GetUserNameAsync()
        {
            return Task.FromResult("gainorloss");
        }
    }
}
