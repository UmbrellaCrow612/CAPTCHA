namespace CAPTCHA.Core.Options
{
    public static class AudioMimeTypes
    {
        public const string Wave = "audio/wav";
        public const string Mpeg = "audio/mpeg";
        public const string WebM = "audio/webm";
        public const string Ogg = "audio/ogg";
        public const string Flac = "audio/flac";
        public const string Aac = "audio/aac";
        public const string Mp4Audio = "audio/mp4";
        public const string WmAudio = "audio/x-ms-wma";

        public const string Midi = "audio/midi";
        public const string WebPCM = "audio/pcm";
        public const string RealAudio = "audio/vnd.rn-realaudio";

        public static bool IsSupported(string mimeType) =>
            mimeType switch
            {
                Wave or Mpeg or WebM or Ogg or Flac or Aac or Mp4Audio or WmAudio or Midi or WebPCM or RealAudio => true,
                _ => false
            };
    }
}
