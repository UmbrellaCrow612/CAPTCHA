using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CAPTCHA.Core.Services
{
    internal class ImgService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static byte[] GenerateImg(RocketCAPTCHA rocketCAPTCHA, RocketCAPTCHAOptions options)
        {
            var matrix = rocketCAPTCHA.GetMatrix();
            int widthOfImage = 250;
            int heightOfImage = 250;

            using Bitmap bitmap = new(widthOfImage, heightOfImage);
            using Graphics graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.White);

            var xSlice = widthOfImage / options.MatrixColumns;
            var ySlice = heightOfImage / options.MatrixRows;

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    var r = new Rectangle(i * xSlice, j * ySlice, xSlice, ySlice);

                    if (matrix[i][j] == (int)RocketBoardItems.RocketPosition)
                    {
                        Point[] rocketPoints =
                        {
                            new(r.X + xSlice / 2, r.Y),               // Tip
                            new(r.X, r.Y + ySlice),                  // Bottom left
                            new(r.X + xSlice, r.Y + ySlice)          // Bottom right
                        };
                        var brush = new SolidBrush(Color.Blue);
                        graphics.FillPolygon(brush, rocketPoints);
                    }

                    if (matrix[i][j] == (int)RocketBoardItems.Meteor)
                    {
                        var brush = new SolidBrush(Color.Red);
                        graphics.FillEllipse(brush, r);
                    }

                    if (matrix[i][j] == (int)RocketBoardItems.TargetGoal)
                    {
                        PointF[] starPoints =
                        {
                            new(r.X + xSlice / 2, r.Y),             // Top
                            new(r.X + xSlice * 0.6f, r.Y + ySlice * 0.4f),
                            new(r.X + xSlice, r.Y + ySlice * 0.5f), // Right
                            new(r.X + xSlice * 0.6f, r.Y + ySlice * 0.6f),
                            new(r.X + xSlice / 2, r.Y + ySlice),    // Bottom
                            new(r.X + xSlice * 0.4f, r.Y + ySlice * 0.6f),
                            new(r.X, r.Y + ySlice * 0.5f),         // Left
                            new(r.X + xSlice * 0.4f, r.Y + ySlice * 0.4f)
                        };
                        var brush = new SolidBrush(Color.Green);
                        graphics.FillPolygon(brush, starPoints);
                    }

                    var pen = new Pen(Color.Black);

                    graphics.DrawRectangle(pen, r);
                }
            }

            using MemoryStream ms = new();
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static byte[] GenerateImg(TileCAPTCHA captcha, TileCAPTCHAOptions options)
        {
            using Bitmap bitmap = new(options.WidthOfImage, options.HeightOfImage);
            var matrixToDraw = captcha.GetMatrix();

            // Calculate the size of each cell based on the image size and matrix dimensions
            int slice = options.HeightOfImage / matrixToDraw.Count;
            int ySlice = options.WidthOfImage / matrixToDraw[0].Count;

            // Create a graphics object for drawing on the bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); // Fill the background with white

                // Loop through the matrix and draw each square
                for (int i = 0; i < matrixToDraw.Count; i++)
                {
                    for (int j = 0; j < matrixToDraw[i].Count; j++)
                    {
                        // Define the position and size of each square in the grid
                        int x = j * ySlice;
                        int y = i * slice;
                        var rectangle = new Rectangle(x, y, ySlice, slice);

                        // Check the matrix value (0 = white, 1 = blue)
                        if (matrixToDraw[i][j] == 1)
                        {
                            // Draw blue square for 1
                            g.FillRectangle(Brushes.Blue, rectangle);
                        }

                        // Draw the black border around each square
                        g.DrawRectangle(Pens.Black, rectangle);
                    }
                }
            }

            // Convert the bitmap to byte array (you can save it or process it further)
            using MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static byte[] GenerateImg(TextImgCAPTCHA captcha, TextImgCAPTCHAOptions options)
        {
            using Bitmap bitmap = new((int)options.WidthOfImage, (int)options.HeightOfImage);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using SolidBrush brush = new(options.CaptchaTextColor);

            DrawBackgroundColor(graphics, options.BackgroundColorOfImage);
            DrawText(captcha.AnswerInPlainText, options, graphics, brush);
            AddSmallTextNoise(graphics, options);
            DrawWaves(graphics, options);
            AddStaticNoise(graphics, options);

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
                List<FontFamily> fonts = [FontFamily.GenericMonospace, FontFamily.GenericSansSerif, FontFamily.GenericMonospace];

                var additionalFonts = new[]
                {
                    "Cooper Black",
                    "Arial Black",
                    "Comic Sans MS",
                    "Courier New",
                    "Impact",
                    "Verdana",
                    "Georgia"
                };
                foreach (var fontName in additionalFonts)
                {
                    var f = FontFamily.Families.FirstOrDefault(f => f.Name == fontName);
                    if (f is not null)
                    {
                        fonts.Add(f);
                    }
                }

                using Font font = new(fonts[random.Next(0, fonts.Count)], fontSize, o.CaptchaTextFontStyle.Style);

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
        private static void AddStaticNoise(Graphics graphics, TextImgCAPTCHAOptions options)
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static void AddSmallTextNoise(Graphics graphics, TextImgCAPTCHAOptions options)
        {
            string letters = options.SmallTextNoiseOptions.CharacterSet.ToString()!;
            var random = new Random();
            var height = (int)options.HeightOfImage;
            var width = (int)options.WidthOfImage;
            var brush = new SolidBrush(options.SmallTextNoiseOptions.ColorOfText);

            for (int i = 0; i < options.SmallTextNoiseOptions.NumberOfLinesDrawns; i++)
            {
                var characterToDraw = letters[random.Next(0, letters.Length)];
                var startPointHeight = random.Next(0, height);

                for (int x = 0; x < width; x += (int)options.SmallTextNoiseOptions.SpaceBetweenEachLetter)
                {
                    using Font font = new(options.SmallTextNoiseOptions.Font.FontFamily, 10, options.SmallTextNoiseOptions.Font.Style);
                    graphics.DrawString(
                        characterToDraw.ToString(),
                        font,
                        brush,
                        x,
                        startPointHeight
                    );
                }
            }
        }
    }
}
