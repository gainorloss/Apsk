using Microsoft.AspNetCore.Mvc;

namespace CatalogItems.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}