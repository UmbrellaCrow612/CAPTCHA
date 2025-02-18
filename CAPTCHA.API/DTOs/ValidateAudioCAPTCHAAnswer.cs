using System.ComponentModel.DataAnnotations;

namespace CAPTCHA.API.DTOs
{
    public class ValidateAudioCAPTCHAAnswer
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string Answer { get; set; }
    }
}
