using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootSharp.Grains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;

namespace BootSharp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IAccountGrain _accountGrain;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IAccountGrain accountGrain)
        {
            _logger = logger;
            _accountGrain = accountGrain;
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
        public async Task<string> LoginAsync()
        {
            await _accountGrain.LoginAsync("gain");
            return "gain";
        }
        [HttpGet]
        public async Task<IEnumerable<string>> GetAllUsersAsync()
        {
            return await _accountGrain.GetAllUsersAsync();
        }
    }
}
