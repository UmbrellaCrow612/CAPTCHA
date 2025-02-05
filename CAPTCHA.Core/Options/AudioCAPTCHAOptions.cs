namespace CAPTCHA.Core.Options
{
    public class AudioCAPTCHAOptions
    {
        /// <summary>
        /// Audio words to be used in <see cref="Models.AudioCAPTCHA"/>
        /// </summary>
        public HashSet<string> WordSet { get; set; } = [
            "Security", "Network", "Cloud", "System", "Access",
            "Server", "Database", "Firewall", "Encrypt", "Monitor",
            "Digital", "Protect", "Connect", "Bridge", "Shield",
            "Secure", "Global", "Signal", "Power", "Control",
            "Crypto", "Guard", "Defend", "Virtual", "Channel"
         ];

        /// <summary>
        /// Amount of words to use in the <see cref="Models.AudioCAPTCHA"/> 
        /// more words are harder to understand
        /// </summary>
        public uint CountOfWordsUsed { get; set; } = 2;

        /// <summary>
        /// The audio file type used to save the <see cref="Models.AudioCAPTCHA"/> when sent to the client - use <see cref="AudioMimeTypes"/>
        /// </summary>
        /// <remarks>
        /// By default it is a <see cref="AudioMimeTypes.Mpeg"/> MP3 format so it can be played in the Scalar UI and in browsers
        /// </remarks>
        public string AudioFileFormatOverTheWire { get; set; } = AudioMimeTypes.Mpeg;
    }
}
