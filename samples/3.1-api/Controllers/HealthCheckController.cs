using Apsk.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3._1_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : RestController
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
