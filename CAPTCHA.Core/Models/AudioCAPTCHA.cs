namespace CAPTCHA.Core.Models
{
    /// <summary>
    /// CAPTCHA audio question model
    /// </summary>
    public class AudioCAPTCHA
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string AnswerInPlainText { get; set; }

        public byte[] RawAudioBytes { get; private set; } = [];

        public void SetRawAudioBytes(byte[] bytes)
        {
            RawAudioBytes = bytes;
        }
    }
}
