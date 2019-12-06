using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootSharp.API.Application;
using BootSharp.API.Models;
using BootSharp.Grains;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;

namespace BootSharp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : RestController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IAccountGrain _accountGrain;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserAppService _usrAppSrv;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IAccountGrain accountGrain,
            IUserAppService usrAppSrv
            )
        {
            _logger = logger;
            _accountGrain = accountGrain;
            _usrAppSrv = usrAppSrv;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public async Task<RestResult> LoginAsync()
        {
            await _accountGrain.LoginAsync("gain");
            return Failure("500","堆栈溢出");
        }
        [HttpGet]
        public async Task<RestResult> GetAllUsersAsync()
        {
            return Success(await _accountGrain.GetAllUsersAsync());
        }
    }
}
