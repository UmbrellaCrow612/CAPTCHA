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

            int countOfSlices = _.AnswerInPlainText.Length;
            int widthOfASlice = (int)options.WidthOfImage / countOfSlices;

            for (int i = 0; i < countOfSlices; i++)
            {
                int x = i * widthOfASlice;
                var r = new Rectangle(x, 0, widthOfASlice, (int)options.HeightOfImage);
                string letter = _.AnswerInPlainText[i].ToString();
                SizeF letterSize = graphics.MeasureString(letter, options.CaptchaTextFontStyle);
                PointF letterPosition = new PointF(
                    r.Left + (r.Width - letterSize.Width) / 2,
                    r.Top + (r.Height - letterSize.Height) / 2
                );

                graphics.DrawString(letter,options.CaptchaTextFontStyle, brush, letterPosition );
            }

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
    }
}
