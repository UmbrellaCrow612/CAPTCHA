using System.Speech.Synthesis;

namespace CAPTCHA.Core.Services
{
    public static class AudioService
    {
        /// <summary>
        /// Produces a WAv file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static byte[] TextToSpeech(string text)
        {
            using SpeechSynthesizer synth = new();
            using MemoryStream ms = new();
            synth.SetOutputToWaveStream(ms);
            synth.Speak(text);
            return ms.ToArray();
        }
    }
}
