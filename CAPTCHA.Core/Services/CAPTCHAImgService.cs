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
            Random random = new();

            for (int i = 0; i < countOfSlices; i++)
            {
                int x = i * widthOfASlice;
                var r = new Rectangle(x, 0, widthOfASlice, (int)o.HeightOfImage);
                string letter = s[i].ToString();

                float fontSize = random.Next(20, 36);
                using Font font = new(o.CaptchaTextFontStyle.FontFamily, fontSize, o.CaptchaTextFontStyle.Style);

                SizeF letterSize = g.MeasureString(letter, font);

                float posX = r.Left + random.Next(0, Math.Max(0, (int)(r.Width - letterSize.Width)));
                float posY = r.Top + random.Next(0, Math.Max(0, (int)(r.Height - letterSize.Height)));
                PointF letterPosition = new(posX, posY);
           
                float angle = random.Next(0, 2) == 0 ? -45 : 45; 
                g.TranslateTransform(letterPosition.X + letterSize.Width / 2, letterPosition.Y + letterSize.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(letterPosition.X + letterSize.Width / 2), -(letterPosition.Y + letterSize.Height / 2));

                g.DrawString(letter, font, b, letterPosition);

                g.ResetTransform();
            }
        }
    }
}
