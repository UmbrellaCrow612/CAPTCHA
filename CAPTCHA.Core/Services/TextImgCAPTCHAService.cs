using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;

namespace CAPTCHA.Core.Services
{
    public class TextImgCAPTCHAService
    {
        private TextImgCAPTCHAOptions defaultOptions = new();

        public TextImgCAPTCHAService()
        {

        }

        public TextImgCAPTCHAService(TextImgCAPTCHAOptions options)
        {
            defaultOptions = options;
        }

        public TextImgCAPTCHAResult GenerateQuestion()
        {
            var result = new TextImgCAPTCHAResult();
            try
            {
                var textToDisplayInCaptcha = defaultOptions.CharacterSet
                    .OrderBy(c => Guid.NewGuid())
                    .Take(5)
                    .Aggregate("", (acc, c) => acc + c);

                var captcha = new TextImgCAPTCHA
                {
                    AnswerInPlainText = textToDisplayInCaptcha,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(defaultOptions.ExpiresAtInMinutes)
                };
                result.CAPTCHA = captcha;

                // Generate the image as a base64 btye[] with the options
                try
                {
                    var bytes = CAPTCHAImgService.GenerateImg(captcha, defaultOptions);
                    captcha.SetImageBytes(bytes);
                }
                catch (Exception e)
                {
                    result.Errors.Add(e.Message);
                    return result;
                }

                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                return result;
            }
        }
    }

    public class TextImgCAPTCHAResult
    {
        public bool IsSuccess { get; set; } = false;
        public ICollection<string> Errors { get; set; } = [];
        public TextImgCAPTCHA CAPTCHA { get; set; } = new() { AnswerInPlainText = "DEFAULT", ExpiresAt = DateTime.UtcNow };
    }
}
