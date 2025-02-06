namespace CAPTCHA.Core.Options
{
    public class KeyPadCAPTCHAOptions
    {
        /// <summary>
        /// The characters to be displayed in each key pad button option sent across as a <see cref="Models.KeyPadChild"/>
        /// </summary>
        public HashSet<char> CharacterSet { get; set; } = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

        /// <summary>
        /// Amount of <see cref="CharacterSet"/> to use to send across and display as well as for the <see cref="Models.KeyPadCAPTCHA.AnswerInPlainText"/>
        /// </summary>
        public uint NumberOfCharactersToUse { get; set; } = 9;

        /// <summary>
        /// The length of the visual pattern needed to be answered for
        /// </summary>
        public uint AnswerSize { get; set; } = 4;
    }
}
