using System.Drawing;
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
        public double ExpiresAtInMinutes { get; set; } = 3;

        /// <summary>
        /// The width of the CAPTCHA image
        /// </summary>
        public uint WidthOfImage { get; set; } = 300;  

        /// <summary>
        /// The height of the CAPTCHA image
        /// </summary>
        public uint HeightOfImage { get; set; } = 120; 

        /// <summary>
        /// The format the image will be created as. Note it it could be diffrent to <see cref="ImageFormatOverTheWire"/>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;

        /// <summary>
        /// The background color of the CAPTCHA image
        /// </summary>
        public Color BackgroundColorOfImage { get; set; } = Color.LightGray;  

        /// <summary>
        /// Color used to draw the text onto the img with
        /// </summary>
        public Color CaptchaTextColor { get; set; } = Color.DarkBlue;  

        /// <summary>
        /// The style of the font that the text drawn on the CAPTCHA will have, it's font family and size
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public Font CaptchaTextFontStyle { get; set; } = new Font(FontFamily.GenericSansSerif, 22, FontStyle.Bold); 

        /// <summary>
        /// Color used to draw the waves
        /// </summary>
        public Color WaveColor { get; set; } = Color.CornflowerBlue;  

        /// <summary>
        /// Amount of waves to draw on the img
        /// </summary>
        public uint WaveCount { get; set; } = 6;  

        /// <summary>
        /// Draw some small text across the captcha separate than the main text to be shown and read, this is for security 
        /// </summary>
        public SmallTextNoiseOptions SmallTextNoiseOptions { get; set; } = new();

        /// <summary>
        /// Format of the file that is sent over http to the client - can be diffrent than the base CAPTCHA img that it is created as - <see cref="ImageFormat"/> the supported 
        /// options are in the class <see cref="MimeTypes"/> as they are the default options <see cref="File"/> will support.
        /// </summary>
        public string ImageFormatOverTheWire { get; set; } = MimeTypes.Webp;
    }

    public class SmallTextNoiseOptions
    {
        /// <summary>
        /// Characters to use
        /// </summary>
        public HashSet<char> CharacterSet { get; set; } = new HashSet<char> { 'A', 'B', 'C', 'D', 'E' }; 

        /// <summary>
        /// Number of lines to draw
        /// </summary>
        public uint NumberOfLinesDrawns { get; set; } = 6;  

        /// <summary>
        /// Space between each letter when drawn in a line - these are between each letter in the same line
        /// </summary>
        public uint SpaceBetweenEachLetter { get; set; } = 10;  

        /// <summary>
        /// Each letters font
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public Font Font { get; set; } = new(FontFamily.GenericMonospace, 8, FontStyle.Italic); 

        /// <summary>
        /// Each letters colors
        /// </summary>
        public Color ColorOfText { get; set; } = Color.SlateGray;  
    }
}
