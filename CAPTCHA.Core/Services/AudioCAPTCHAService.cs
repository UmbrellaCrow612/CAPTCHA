using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;

namespace CAPTCHA.Core.Services
{
    public class AudioCAPTCHAService
    {
        public readonly AudioCAPTCHAOptions defaultOptions = new();

        public AudioCAPTCHAService()
        {

        }

        public AudioCAPTCHAService(AudioCAPTCHAOptions options)
        {
            defaultOptions = options;
        }


        public AudioCAPTCHAServiceResult GenerateQuestion()
        {
            var result = new AudioCAPTCHAServiceResult();

            try
            {
                var wordsToUse = defaultOptions.WordSet.OrderBy(x => Guid.NewGuid().ToString()).Take((int)defaultOptions.CountOfWordsUsed).ToList();
                string sentence = string.Join(" ", wordsToUse).ToLower();

                result.CAPTCHA.AnswerInPlainText = sentence;

                List<byte> bytes = [];
                try
                {
                    bytes = [.. AudioService.TextToSpeech(sentence)];
                }
                catch (Exception e)
                {
                    result.Errors.Add(e.Message);
                    throw;
                }
                result.CAPTCHA.SetRawAudioBytes([.. bytes]);

                result.IsSucceeded = true;
                return result;
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                return result;
            }
        }
    }

    public class AudioCAPTCHAServiceResult
    {
        public bool IsSucceeded { get; set; } = false;
        public AudioCAPTCHA CAPTCHA { get; set; } = new AudioCAPTCHA { AnswerInPlainText = "DEFAULT" };
        public ICollection<string> Errors { get; set; } = [];
    }
}
