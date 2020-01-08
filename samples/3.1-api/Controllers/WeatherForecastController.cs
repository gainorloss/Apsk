using System;
using System.Collections.Generic;
using System.Linq;
using _3._1_api.Application;
using Apsk.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _3._1_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class WeatherForecastController : RestController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthenticationAppSrv _authenticationAppSvc;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IAuthenticationAppSrv authenticationAppSvc)
        {
            _logger = logger;
            _authenticationAppSvc = authenticationAppSvc;
        }

        [HttpGet]
        public RestResult Get()
        {
            _authenticationAppSvc.ListApis();
            var rng = new Random();
            return Success(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [HttpGet]
        public RestResult Delete()
        {
            _authenticationAppSvc.DeleteApis();
            return Success();
        }
    }
}
