namespace CAPTCHA.Core.Models
{
    public class TextImgCAPTCHA
    {
        /// <summary>
        /// Unique identifier for the CAPTCHA
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The question answer hashed
        /// </summary>
        public required string AnswerInPlainText { get; set; }

        /// <summary>
        /// Time when the CAPTCHA was created in UTC time
        /// </summary>
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        /// <summary>
        /// When the CAPTCHA expires in UTC time
        /// </summary>
        public required DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Image data in ImageBytes
        /// </summary>
        public byte[] ImageBytes { get; private set; } = [];

        public void SetImageBytes(byte[] bytes)
        {
            ImageBytes = bytes;
        }
    }
}
