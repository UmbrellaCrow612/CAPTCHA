using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using System.Drawing;
using System.Drawing.Drawing2D;

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
            DrawWaves(graphics, options);
            AddNoise(graphics, options);

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static void DrawWaves(Graphics graphics, TextImgCAPTCHAOptions o)
        {
            using Pen wavePen = new(o.WaveColor, 2); // Adjust the pen width as needed
            Random random = new();

            for (int i = 0; i < o.WaveCount; i++)
            {
                // Calculate the Y position for the wave
                int y = random.Next(0, (int)o.HeightOfImage);

                // Create a GraphicsPath to draw the wave
                using GraphicsPath path = new();

                // Start the wave at the left side of the image
                path.StartFigure();
                path.AddLine(0, y, 50, y); // Start with a straight line

                // Add Bézier curves to create the wave effect
                for (int x = 50; x < (int)o.WidthOfImage; x += 100)
                {
                    int controlY1 = y + random.Next(-20, 20); // Randomize the control points for a natural wave
                    int controlY2 = y + random.Next(-20, 20);
                    int endY = y + random.Next(-10, 10);

                    path.AddBezier(
                        x, y,                     // Start point
                        x + 25, controlY1,        // Control point 1
                        x + 75, controlY2,        // Control point 2
                        x + 100, endY             // End point
                    );

                    y = endY; // Update the Y position for the next segment
                }

                // Draw the wave
                graphics.DrawPath(wavePen, path);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static void AddNoise(Graphics graphics, TextImgCAPTCHAOptions options)
        {
            Random random = new();

            // Add random dots
            using var dotBrush = new SolidBrush(options.WaveColor);
            int numberOfDots = random.Next(50, 100);
            for (int i = 0; i < numberOfDots; i++)
            {
                float x = random.Next(0, (int)options.WidthOfImage);
                float y = random.Next(0, (int)options.HeightOfImage);
                graphics.FillEllipse(dotBrush, x, y, 2, 2);
            }

            // Add salt and pepper noise
            int noisePixels = ((int)options.WidthOfImage * (int)options.HeightOfImage) / 50; // 2% of pixels
            using var pixelBrush = new SolidBrush(Color.FromArgb(random.Next(0, 255),
                random.Next(0, 255),
                random.Next(0, 255)));

            for (int i = 0; i < noisePixels; i++)
            {
                float x = random.Next(0, (int)options.WidthOfImage);
                float y = random.Next(0, (int)options.HeightOfImage);
                graphics.FillRectangle(pixelBrush, x, y, 1, 1);
            }
        }
    }
}
