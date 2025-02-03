namespace CAPTCHA.Core.Options
{
    public class AudioCAPTCHAOptions
    {
        /// <summary>
        /// Audio words to be used in <see cref="Models.AudioCAPTCHA"/>
        /// </summary>
        public HashSet<string> WordSet { get; set; } = ["Plain", "Text"];

        /// <summary>
        /// Amount of words to use in the <see cref="Models.AudioCAPTCHA"/> 
        /// more words are harder to understand
        /// </summary>
        public uint CountOfWordsUsed { get; set; } = 1;
    }
}
