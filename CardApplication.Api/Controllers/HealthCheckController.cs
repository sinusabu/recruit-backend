using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardApplication.Api.Controllers
{
    [Route("healthcheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok("Card application version - 1.0.0.0");
        }
    }
}
