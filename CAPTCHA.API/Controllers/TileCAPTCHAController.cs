using CAPTCHA.API.Data;
using CAPTCHA.API.DTOs;
using CAPTCHA.Core.Options;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CAPTCHA.API.Controllers
{
    [Route("captcha/tile")]
    [ApiController]
    public class TileCAPTCHAController(CAPTCHADbContext dbContext) : ControllerBase
    {
        private readonly CAPTCHADbContext _dbContext = dbContext;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = new TileCAPTCHAService();
            var res = service.GenerateQuestion();
            if (!res.Succeeded) return BadRequest(res);

            var fileName = $@"tile-captcha-{res.CAPTCHA.Id}";

            var file = File(res.CAPTCHA.GetImageBytes(), MimeTypes.Png, fileName);
            Response.Headers["X-Captcha-Id"] = res.CAPTCHA.Id;
            Response.Headers["X-Base-Matrix"] = JsonSerializer.Serialize(res.Matrix);

            await _dbContext.TileCAPTCHAs.AddAsync(res.CAPTCHA);
            await _dbContext.SaveChangesAsync();

            return file;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ValidateTileCAPTCHADto dto)
        {
            var captcha = await _dbContext.TileCAPTCHAs.FindAsync(dto.Id);
            if (captcha is null) return BadRequest("NOT_FOUND");

            if (captcha.IsUsed) return BadRequest("USED");
            if (!captcha.IsAnswerCorrect(dto.Answer)) return BadRequest("TILES_DO_NOT_MATCH");

            return Ok();
        }
    }
}
