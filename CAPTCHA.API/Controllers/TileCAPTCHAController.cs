using CAPTCHA.Core.Options;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [Route("captcha/tile")]
    [ApiController]
    public class TileCAPTCHAController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var service = new TileCAPTCHAService();
            var res = service.GenerateQuestion();

            var file = File(res.CAPTCHA.GetImageBytes(), MimeTypes.Png);

            return file;
        }
    }
}
