using System.ComponentModel.DataAnnotations;

namespace CAPTCHA.API.DTOs
{
    public class ValidateKeyPadCAPTCHADto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string[] SelectedChildrenId { get; set; }
    }
}
