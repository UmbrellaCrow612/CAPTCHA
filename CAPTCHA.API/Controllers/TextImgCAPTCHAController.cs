using CAPTCHA.API.Data;
using CAPTCHA.API.DTOs;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [ApiController]
    [Route("captcha/text-img")]
    public class TextImgCAPTCHAController(CAPTCHADbContext dbContext) : ControllerBase
    {
        private readonly CAPTCHADbContext _dbContext = dbContext;

        private static object CreateErrorResponse(string mess)
        {
            return new { message = mess };
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var service = new TextImgCAPTCHAService();

            var result = service.GenerateQuestion();
            if(!result.IsSuccess) return BadRequest(result.Errors);

            await _dbContext.TextImgCAPTCHAs.AddAsync(result.CAPTCHA);
            await _dbContext.SaveChangesAsync();

            var fileName = $@"text-captcha-{result.CAPTCHA.Id}";

            var fileResult = File(result.CAPTCHA.GetImageBytes(), service.defaultOptions.ImageFormatOverTheWire, fileName);

            Response.Headers["X-Captcha-Id"] = result.CAPTCHA.Id;

            return fileResult;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ValidateTextImgCAPTCHAAnswer dto)
        {
            var captcha = await _dbContext.TextImgCAPTCHAs.FindAsync(dto.Id);
            if (captcha is null) return BadRequest(CreateErrorResponse("Captcha not found"));

            var a = captcha.Attempts;
            if (a++ > captcha.GetMaxAttempts()) return BadRequest(CreateErrorResponse("Max attempts reach for this captcha"));
            if (captcha.IsUsed || captcha.UsedAt.HasValue) return BadRequest(CreateErrorResponse("Captcha already used"));
            if (!string.Equals(dto.Answer, captcha.AnswerInPlainText))
            {
                captcha.Attempts += 1;
                await _dbContext.SaveChangesAsync();
                return BadRequest(CreateErrorResponse("Captcha text dose not match"));
            }

            captcha.Attempts += 1;
            captcha.IsUsed = true;
            captcha.UsedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
