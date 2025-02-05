using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [ApiController]
    [Route("captcha/audio")]
    public class AudioCAPTCHAController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var service = new AudioCAPTCHAService();
            var res = service.GenerateQuestion();

            if (!res.IsSucceeded)
                return BadRequest(res.Errors);
            var fileName = $@"audio-captcha-{res.CAPTCHA.Id}";

            var fileResult = File(res.CAPTCHA.RawAudioBytes, service.defaultOptions.AudioFileFormatOverTheWire, fileName);

            Response.Headers["X-Captcha-Id"] = res.CAPTCHA.Id;

            return fileResult;
        }

    }
}
