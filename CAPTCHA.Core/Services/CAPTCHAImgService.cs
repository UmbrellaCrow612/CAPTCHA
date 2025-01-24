using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using System.Drawing;

namespace CAPTCHA.Core.Services
{
    internal class CAPTCHAImgService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static byte[] GenerateImg(TextImgCAPTCHA _, TextImgCAPTCHAOptions options)
        {
            using Bitmap bitmap = new((int)options.WidthOfImage, (int)options.HeightOfImage);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using SolidBrush brush = new(options.CaptchaTextColor);

            DrawBackgroundColor(graphics, options.BackgroundColorOfImage);
            DrawText(graphics, _.AnswerInPlainText, options.CaptchaTextFontStyle, brush, new PointF(10, 10));
            // Add other secuirty layer 
            // lines 
            // dots 
            // shapes 
            // other stuff

            using MemoryStream memoryStream = new();
            bitmap.Save(memoryStream, options.ImageFormat);

            return memoryStream.ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static void DrawBackgroundColor(Graphics graphics, Color color)
        {
            graphics.Clear(color);
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private static void DrawText(Graphics graphics, string text, Font font, Brush brush, PointF pointF)
        {
            graphics.DrawString(text, font, brush, pointF); // distort and fuzz etc
        }
    }
}
