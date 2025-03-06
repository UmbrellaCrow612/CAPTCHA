using CAPTCHA.API.Data;
using CAPTCHA.API.DTOs;
using CAPTCHA.Core;
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
            var result = service.GenerateQuestion();

            await _dbContext.RocketCAPTCHAs.AddAsync(result.CAPTCHA);
            await _dbContext.SaveChangesAsync();

            Response.Headers.Append(Headers.XCaptchaId, result.CAPTCHA.Id);

            var file = File(result.CAPTCHA.GetImageBytes(), MimeTypes.Png);

            return file;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ValidateRocketCaptchaDto dto)
        {
            var captcha = await _dbContext.RocketCAPTCHAs.FindAsync(dto.Id);
            if (captcha is null) return NotFound(Codes.NOT_FOUND);
            if (captcha.IsUsed) return BadRequest(Codes.USED);

            var matrix = JsonSerializer.Deserialize<List<List<int>>>(captcha.MatrixAsJSON);
            if (matrix is null) return BadRequest(Codes.INTERNAL_CAPTCHA_ISSUE);

            var (row, col) = RocketCAPTCHAService.FindRocketPosition(matrix);

            var answerIsCorr = RocketCAPTCHAService.CanMovesReachGoal(dto.Answer, matrix, col, row);
            if (!answerIsCorr) return BadRequest(Codes.WRONG_ANSWER);
            captcha.IsUsed = true;
            _dbContext.RocketCAPTCHAs.Update(captcha);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

       

    }
}
