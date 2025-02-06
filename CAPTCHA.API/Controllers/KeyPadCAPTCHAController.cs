using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [Route("captcha/key-pad")]
    [ApiController]
    public class KeyPadCAPTCHAController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var service = new KeyPadCAPTCHAService();
            var result = service.GenerateQuestion();
            if(!result.Succeeded) return BadRequest(result);

            return Ok(new { result.CAPTCHA.Id, result.CAPTCHA.Children, result.VisualAnswer });
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
