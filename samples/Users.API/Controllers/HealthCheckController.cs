using Apsk.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Users.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : RestController
    {
        public RestResult Get()
        {
            return Success();
        }
    }
}