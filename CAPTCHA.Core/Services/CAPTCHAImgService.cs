using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using System.Drawing;

namespace CAPTCHA.Core.Services
{
    internal class CAPTCHAImgService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static byte[] GenerateImg(TextImgCAPTCHA captcha, TextImgCAPTCHAOptions options)
        {
            using Bitmap bitmap = new((int)options.WidthOfImage, (int)options.HeightOfImage);
            using Graphics graphics = Graphics.FromImage(bitmap);

            // do the drawing etc
            graphics.Clear(Color.Black);

            using MemoryStream memoryStream = new();
            bitmap.Save(memoryStream, options.ImageFormat);

            return memoryStream.ToArray();
        }
    }
}
