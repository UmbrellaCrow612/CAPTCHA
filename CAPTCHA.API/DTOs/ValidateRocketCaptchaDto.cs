using System.ComponentModel.DataAnnotations;

namespace CAPTCHA.API.DTOs
{
    public class ValidateRocketCaptchaDto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required List<int> Answer {  get; set; }
    }
}
