namespace CAPTCHA.Core.Models
{
    /// <summary>
    /// CAPTCHA audio question model
    /// </summary>
    public class AudioCAPTCHA
    {
        /// <summary>
        /// Unique identifier for the CAPTCHA
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The question answer
        /// </summary>
        public required string AnswerInPlainText { get; set; }

        /// <summary>
        /// Time when the CAPTCHA was created in UTC time
        /// </summary>
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        /// <summary>
        /// When the CAPTCHA expires in UTC time
        /// </summary>
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(2);

        /// <summary>
        /// If this specific captcha ha been already used and verfied - as it could be used in the same time  window multiple times
        /// </summary>
        public bool IsUsed { get; set; } = false;

        /// <summary>
        /// When this captcha was used at
        /// </summary>
        public DateTime? UsedAt { get; set; } = null;

        /// <summary>
        /// Amount of times someone tried to use this
        /// </summary>
        public int Attempts { get; set; } = 0;

        /// <summary>
        /// wav file as bytes[]
        /// </summary>
        private byte[] RawAudioBytes = [];

        /// <summary>
        /// How many failed attempt we want to allow beofre we just block them for this captcha
        /// </summary>
        private readonly int MaxAttempt = 3;
   
        public int GetMaxAttempts()
        {
            return MaxAttempt;
        }

        public byte[] GetRawAudioBytes()
        {
            return RawAudioBytes;
        }

        public void SetRawAudioBytes(byte[] bytes)
        {
            RawAudioBytes = bytes;
        }
    }
}
