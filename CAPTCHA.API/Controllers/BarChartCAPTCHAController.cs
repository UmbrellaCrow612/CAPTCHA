using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [Route("captcha/bar-chart")]
    [ApiController]
    public class BarChartCAPTCHAController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
