using CAPTCHA.API.DTOs;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [ApiController]
    [Route("captcha/text-img")]
    public class TextImgCAPTCHAController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var service = new TextImgCAPTCHAService();

            var result = service.GenerateQuestion();
            if(!result.IsSuccess) return BadRequest(result.Errors);

            var fileResult = File(result.CAPTCHA.ImageBytes, service.defaultOptions.ImageFormatOverTheWire);

            Response.Headers["X-Captcha-Id"] = result.CAPTCHA.Id;

            return fileResult;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ValidateTextImgCAPTCHAAnswer dto)
        {

            return Ok();
        }
    }
}
