using BootSharp.Grains;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootSharp.API.Controllers
{
    [ApiController]
    [Route("api/account/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IClusterClient _client;
        private readonly string uid = "gainorloss";
        public AccountController(IClusterClient client)
        {
            _client = client;
        }
        [HttpGet]
        public async Task<string> LoginAsync()
        {
            var grain = _client.GetGrain<IAccountGrain>(uid);
            await grain.LoginAsync(uid);
            return uid;
        }
        [HttpGet]
        public async Task<IEnumerable<string>> GetAllUsersAsync()
        {
            var grain = _client.GetGrain<IAccountGrain>(uid);
            return await grain.GetAllUsersAsync();
        }
    }
}