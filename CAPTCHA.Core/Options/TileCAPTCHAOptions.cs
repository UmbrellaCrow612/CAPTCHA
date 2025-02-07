namespace CAPTCHA.Core.Options
{
    public class TileCAPTCHAOptions
    {
        /// <summary>
        /// Rows of the 2D matrix for <see cref="Models.TileCAPTCHA.Matrix"/>
        /// </summary>
        public int MatrixRows { get; set; } = 5;

        /// <summary>
        /// Columns of the 2D matrix for <see cref="Models.TileCAPTCHA.Matrix"/>
        /// </summary>
        public int MatrixColumns { get; set; } = 5;

        /// <summary>
        /// How many tiles will be highlighted in the answer
        /// </summary>
        public int NumberOfAnswerTiles { get; set; } = 6;

        /// <summary>
        /// Width of the captcha image
        /// </summary>
        public int WidthOfImage { get; set; } = 200;

        /// <summary>
        /// Height of the captcha image
        /// </summary>
        public int HeightOfImage { get; set; } = 150;
    }
}
