using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;

namespace CAPTCHA.Core.Services
{
    public class AudioCAPTCHAService
    {
        private readonly AudioCAPTCHAOptions _options = new();

        public AudioCAPTCHAService()
        {

        }

        public AudioCAPTCHAService(AudioCAPTCHAOptions options)
        {
            _options = options;
        }


        public AudioCAPTCHAServiceResult GenerateQuestion()
        {
            var result = new AudioCAPTCHAServiceResult();

            try
            {
                var wordsToUse = _options.WordSet.OrderBy(x => Guid.NewGuid().ToString()).Take((int)_options.CountOfWordsUsed).ToList();
                string sentence = string.Join(" ", wordsToUse);

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
