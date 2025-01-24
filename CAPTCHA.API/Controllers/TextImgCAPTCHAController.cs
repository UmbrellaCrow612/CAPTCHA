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

            // add to redis cache
            // key = result.Value.Id value = result.Value.AnswerInPlainText

            return Ok(result.CAPTCHA.ImageBytes);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ValidateTextImgCAPTCHAAnswer dto)
        {

            return Ok();
        }
    }
}
