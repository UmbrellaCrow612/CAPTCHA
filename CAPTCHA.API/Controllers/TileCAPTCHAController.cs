using CAPTCHA.Core.Options;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            if (!res.Succeeded) return BadRequest(res);

            var fileName = $@"tile-captcha-{res.CAPTCHA.Id}";

            var file = File(res.CAPTCHA.GetImageBytes(), MimeTypes.Png, fileName);
            Response.Headers["X-Captcha-Id"] = res.CAPTCHA.Id;
            Response.Headers["X-Base-Matrix"] = JsonSerializer.Serialize(res.Matrix);



            return file;
        }
    }
}
