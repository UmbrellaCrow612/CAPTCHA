﻿using CAPTCHA.API.Data;
using CAPTCHA.API.DTOs;
using CAPTCHA.Core;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPTCHA.API.Controllers
{
    [ApiController]
    [Route("captcha/audio")]
    public class AudioCAPTCHAController(CAPTCHADbContext dbContext) : ControllerBase
    {
        private readonly CAPTCHADbContext _dbContext = dbContext;

        private static object CreateErrorResponse(string mess)
        {
            return new { message = mess };
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = new AudioCAPTCHAService();
            var res = service.GenerateQuestion();

            if (!res.IsSucceeded)
                return BadRequest(res.Errors);

            await _dbContext.AudioCAPTCHAs.AddAsync(res.CAPTCHA);
            await _dbContext.SaveChangesAsync();

            var fileName = $@"audio-captcha-{res.CAPTCHA.Id}";

            var fileResult = File(res.CAPTCHA.GetRawAudioBytes(), service.defaultOptions.AudioFileFormatOverTheWire, fileName);

            Response.Headers[Headers.XCaptchaId] = res.CAPTCHA.Id;

            return fileResult;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ValidateAudioCAPTCHAAnswer dto)
        {
            var captcha = await _dbContext.AudioCAPTCHAs.FindAsync(dto.Id);
            if (captcha is null) return BadRequest(CreateErrorResponse(Codes.NOT_FOUND));

            var a = captcha.Attempts;
            if (DateTime.UtcNow > captcha.ExpiresAt) return BadRequest(CreateErrorResponse(Codes.EXPIRED));
            if (a++ > captcha.GetMaxAttempts()) return BadRequest(CreateErrorResponse(Codes.MAX_ATTEMPTS));
            if (captcha.IsUsed || captcha.UsedAt.HasValue) return BadRequest(CreateErrorResponse(Codes.USED));
            if (!string.Equals(dto.Answer, captcha.AnswerInPlainText))
            {
                captcha.Attempts += 1;
                await _dbContext.SaveChangesAsync();
                return BadRequest(CreateErrorResponse(Codes.WRONG_ANSWER));
            }

            captcha.Attempts += 1;
            captcha.IsUsed = true;
            captcha.UsedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
