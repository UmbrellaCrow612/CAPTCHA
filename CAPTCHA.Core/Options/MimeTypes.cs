namespace CAPTCHA.Core.Options
{
    public static class MimeTypes
    {
        public const string Png = "image/png";
        public const string Jpeg = "image/jpeg";
        public const string Gif = "image/gif";
        public const string Bmp = "image/bmp";
        public const string Tiff = "image/tiff";
        public const string Webp = "image/webp";
        public const string Svg = "image/svg+xml";
        public const string Ico = "image/x-icon";

        public static string GetMimeType(string extension) => extension.ToLower() switch
        {
            ".png" => Png,
            ".jpg" => Jpeg,
            ".jpeg" => Jpeg,
            ".gif" => Gif,
            ".bmp" => Bmp,
            ".tiff" => Tiff,
            ".webp" => Webp,
            ".svg" => Svg,
            ".ico" => Ico,
            _ => "application/octet-stream" // Default fallback
        };
    }

}
