using CAPTCHA.Core.Options;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [ApiController]
    [Route("captcha/rocket")]
    public class RocketCAPTCHAController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var service = new RocketCAPTCHAService();
            var m = service.GenerateQuestion();

            var file = File(m.CAPTCHA.GetImageBytes(), MimeTypes.Png);

            return file;
        }
    }
}
