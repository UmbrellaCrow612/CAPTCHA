using System.Drawing.Imaging;

namespace CAPTCHA.Core.Options
{
    public class TextImgCAPTCHAOptions
    {
        /// <summary>
        /// The characters that will be used as part of the text shown in the CAPTCHA img, by default it ranges from
        /// the letters A - Z and a-z
        /// </summary>
        public HashSet<char> CharacterSet = new(
            Enumerable.Range('A', 26).Select(c => (char)c)
            .Concat(Enumerable.Range('a', 26).Select(c => (char)c))
        );

        /// <summary>
        /// The time in minutes when the CAPTCHA expires or is considred invalid
        /// </summary>
        public double ExpiresAtInMinutes { get; set; } = 2;

        /// <summary>
        /// The width of the CAPTCHA image
        /// </summary>
        public uint WidthOfImage { get; set; } = 200;

        /// <summary>
        /// The height of the CAPTCHA image
        /// </summary>
        public uint HeightOfImage { get; set; } = 50;

        /// <summary>
        /// The format the image will be sent as to the client
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;

    }
}
