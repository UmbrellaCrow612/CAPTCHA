using CAPTCHA.API.DTOs;
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

            var children = result.CAPTCHA.Children.OrderBy(x => Guid.NewGuid().ToString()).ToList();

            return Ok(new { result.CAPTCHA.Id, children, result.VisualAnswer });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ValidateKeyPadCAPTCHADto dto)
        {
            return Ok();
        }
    }
}
