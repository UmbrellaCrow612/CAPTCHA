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
            if (captcha is null) return BadRequest(CreateErrorResponse("NOT_FOUND"));

            var a = captcha.Attempts;
            if (DateTime.UtcNow > captcha.ExpiresAt) return BadRequest(CreateErrorResponse("EXPIRED"));
            if (a++ > captcha.GetMaxAttempts()) return BadRequest(CreateErrorResponse("MAX_ATTEMPTS"));
            if (captcha.IsUsed || captcha.UsedAt.HasValue) return BadRequest(CreateErrorResponse("USED"));
            if (!string.Equals(dto.Answer, captcha.AnswerInPlainText))
            {
                captcha.Attempts += 1;
                await _dbContext.SaveChangesAsync();
                return BadRequest(CreateErrorResponse("TEXT_DOSE_NOT_MATCH"));
            }

            captcha.Attempts += 1;
            captcha.IsUsed = true;
            captcha.UsedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
