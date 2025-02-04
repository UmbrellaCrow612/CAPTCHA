﻿using System.Drawing;
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
        public uint WidthOfImage { get; set; } = 350;

        /// <summary>
        /// The height of the CAPTCHA image
        /// </summary>
        public uint HeightOfImage { get; set; } = 150;

        /// <summary>
        /// The format the image will be sent as to the client
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;

        /// <summary>
        /// The background color of the CAPTCHA image
        /// </summary>
        public Color BackgroundColorOfImage { get; set; } = Color.Gray;

        /// <summary>
        /// Color used to draw the text onto the img with
        /// </summary>
        public Color CaptchaTextColor { get; set; } = Color.Black;

        /// <summary>
        /// The style of the font that the text drawn on the CAPTCHA will have , it's font family and size
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public Font CaptchaTextFontStyle { get; set; } = new Font(FontFamily.GenericSerif, 20);

        /// <summary>
        /// olor used to draw the waves
        /// </summary>
        public Color WaveColor {  get; set; } = Color.DarkKhaki;

        /// <summary>
        /// Amount of waves to draw on the img
        /// </summary>
        public uint WaveCount { get; set; } = 7;

        /// <summary>
        /// Draw some small text across the captcha seperate than the main text to be shown and read, this is for security 
        /// </summary>
        public SmallTextNoiseOptions SmallTextNoiseOptions { get; set; } = new();


    }

    public class SmallTextNoiseOptions
    {
        /// <summary>
        /// Characters to use
        /// </summary>
        public HashSet<char> CharacterSet { get; set; } = ['A', 'B', 'C'];

        /// <summary>
        /// Number of lines to draw
        /// </summary>
        public uint NumberOfLinesDrawns { get; set; } = 4;

        /// <summary>
        /// Space between each letter when drawn i a line - these are betwene each letter in the same line
        /// </summary>
        public uint SpaceBetweenEachLetter { get; set; } = 10;

        /// <summary>
        /// Each letters font
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public Font Font {  get; set; } = new(FontFamily.GenericMonospace, 10, FontStyle.Regular);

        /// <summary>
        /// Each letters colors
        /// </summary>
        public Color ColorOfText { get; set; } = Color.DarkOliveGreen;
    }
}
