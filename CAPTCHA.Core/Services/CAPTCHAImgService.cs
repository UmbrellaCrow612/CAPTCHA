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
            DrawText(_.AnswerInPlainText, options, graphics, brush);
     
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static void DrawText(string s, TextImgCAPTCHAOptions o, Graphics g, Brush b)
        {
            int countOfSlices = s.Length;
            int widthOfASlice = (int)o.WidthOfImage / countOfSlices;

            for (int i = 0; i < countOfSlices; i++)
            {
                int x = i * widthOfASlice;
                var r = new Rectangle(x, 0, widthOfASlice, (int)o.HeightOfImage);
                string letter = s[i].ToString();
                SizeF letterSize = g.MeasureString(letter, o.CaptchaTextFontStyle);
                PointF letterPosition = new(
                    r.Left + (r.Width - letterSize.Width) / 2,
                    r.Top + (r.Height - letterSize.Height) / 2
                );

                g.DrawString(letter, o.CaptchaTextFontStyle, b, letterPosition);
            }
        }
    }
}
