using CAPTCHA.API.Data;
using CAPTCHA.API.DTOs;
using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using CAPTCHA.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CAPTCHA.API.Controllers
{
    [ApiController]
    [Route("captcha/rocket")]
    public class RocketCAPTCHAController(CAPTCHADbContext dbContext) : ControllerBase
    {
        private readonly CAPTCHADbContext _dbContext = dbContext;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = new RocketCAPTCHAService();
            var m = service.GenerateQuestion();

            await _dbContext.RocketCAPTCHAs.AddAsync(m.CAPTCHA);
            await _dbContext.SaveChangesAsync();

            Response.Headers.Append("x-captcha-id", m.CAPTCHA.Id);

            var file = File(m.CAPTCHA.GetImageBytes(), MimeTypes.Png);

            return file;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ValidateRocketCaptchaDto dto)
        {
            var captcha = await _dbContext.RocketCAPTCHAs.FindAsync(dto.Id);
            if (captcha is null) return NotFound("CAPTCHA_NOT_FOUND");

            var matrix = JsonSerializer.Deserialize<List<List<int>>>(captcha.MatrixAsJSON);
            if (matrix is null) return BadRequest("CAPTCHA_ISSUE");

            var (row, col) = RocketCAPTCHAService.FindRocketPosition(matrix);

            var answerIsCorr = RocketCAPTCHAService.CanMovesReachGoal(dto.Answer, matrix, col, row);
            if (!answerIsCorr) return BadRequest("WRONG_MOVES");

            return Ok();
        }

       

    }
}
