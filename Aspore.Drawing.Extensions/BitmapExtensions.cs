using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Aspore.Drawing.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap ToLine(this Bitmap b)
        {
            var cropArea = new Rectangle(0, b.Height / 2, b.Width, 1);
            Bitmap pixelLine = b.Clone(cropArea, PixelFormat.Format32bppRgb);
            b?.Dispose();
            return pixelLine;
        }

        public static void ToGrey(this Bitmap b)
        {
            for (int height = 0; height < b.Height; height++)
            {
                for (int width = 0; width < b.Width; width++)
                {
                    Color rgb = b.GetPixel(width, 0);
                    int g = rgb.ToGreyPixel();
                    b.SetPixel(width, height, Color.FromArgb(g, g, g));
                }
            }
        }        
        
        public static void ToBlackOrWhite(this Bitmap b)
        {
            for (int height = 0; height < b.Height; height++)
            {
                for (int width = 0; width < b.Width; width++)
                {
                    Color rgb = b.GetPixel(width, 0);
                    int blackOrWhite = rgb.ToBlackWhitePixel();
                    b.SetPixel(width, height, Color.FromArgb(blackOrWhite, blackOrWhite, blackOrWhite));
                }
            }
        }

        /// <summary>
        /// Looking for a absolutelly black pixel's index.
        /// If offset = 0, then first black pixel is a code line beginning. 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GetBlackPixelIndex(this Bitmap b, int offset = 0)
        {
            for (int i = offset; i < b.Width; i++) 
            {
                bool flag = b.GetPixel(i, 0).IsBlackPixel();
                if (flag) 
                {
                    return i;
                }
            }
            throw new FormatException("Invalid Code39 format.");
        }

        /// <summary>
        /// Looking for a absolutely white pixel's index after pointed position.
        /// If the startIndex indicates to the first black pixel position, 
        /// then pixel we are finding pointes to the end of first small line.
        /// Thereby we are known not only small line's width but a big line's width as well.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="startIndex"></param>
        /// <returns>Tuple containing width of the small and big lines in Code 39 set.</returns>
        public static (int smallLineWidth, int bigLineWidth) GetSmallAndBigLinesWidthPx(this Bitmap b, int startIndex = 1, int format = 3) 
        {
            for (int i = startIndex; i < b.Width; i++)
            {
                bool flag = b.GetPixel(i, 0).IsWhitePixel();
                if (flag) 
                {
                    int width = (i - startIndex) + 1;
                    return (smallLineWidth: width, bigLineWidth: width * format);
                }
            }
            throw new FormatException("Invalid Code39 format.");
        }

        /// <summary>
        /// Fills entire bitmap by mono color
        /// </summary>
        /// <param name="b">Target bitmap</param>
        /// <param name="color">Background color</param>
        public static void SetBackground(this Bitmap b, Color color)
        {
            for (int i = 0; i < b.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    b.SetPixel(j, i, color);
                }
            }
        }

        /// <summary>
        /// Prints Square by X,Y center coordinate, side and color
        /// </summary>
        /// <param name="b">Target bitmap</param>
        /// <param name="X">X center</param>
        /// <param name="Y">Y center</param>
        /// <param name="side">Side</param>
        /// <param name="color">Color</param>
        public static void PrintSquare(this Bitmap b, int X, int Y, int side, Color color)
        {
            for (int x = X; x < X + side; x++)
            {
                x = x < 0 ? 0 : x;
                for (int y = Y; y < Y + side; y++)
                {
                    y = y < 0 ? 0 : y;
                    b.SetPixel(x, y, color);
                }
            }
        }

        /// <summary>
        /// Prints circle by X,Y center, diameter and color
        /// </summary>
        /// <param name="b"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="diameter"></param>
        /// <param name="color"></param>
        public static void PrintCircle(this Bitmap b, int X, int Y, int diameter, Color color)
        {
            for (int r = 0; r < diameter / 2; r++)
            {
                var radius = (r + 1);
                for (double i = 0.0; i < 360.0; i += 0.1)
                {
                    double angle = i * Math.PI / 180;
                    int x = (int)(X + radius * Math.Cos(angle));
                    int y = (int)(Y + radius * Math.Sin(angle));

                    x = x < 0 ? 0 : x;
                    y = y < 0 ? 0 : y;
                    b.SetPixel(x, y, color);
                }
            }
        }

        /// <summary>
        /// Prints triangle
        /// </summary>
        /// <param name="b"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="side"></param>
        /// <param name="color"></param>
        public static void PrintTriangle(this Bitmap b, int X, int Y, int side, Color color)
        {
            int agile = side;
            for (int x = X; x > X - agile; x--)
            {
                for (int y = Y; y > Y - side; y--)
                {
                    b.SetPixel(x, y, color);
                }
                side -= 1;
            }
        }


        /// <summary>
        /// Gauss distribution aka Normal distribution X ~ N(0, 1)
        /// </summary>
        /// <param name="bmp">Target bitmap</param>
        /// <param name="noisePower"></param>
        /// <returns>Gaussed bitmap</returns>
        public static Bitmap AddGaussianNoise(Bitmap bmp, int noisePower)
        {
            Bitmap res = (Bitmap)bmp.Clone();
            bmp.Dispose();

            var rnd = new Random();

            using (var wrapper = new BitmapWrapper(res))
            {
                foreach (Point p in wrapper)
                {
                    var c = wrapper[p];
                    // Generate noise mask
                    double noise = (rnd.NextDouble() + rnd.NextDouble() + rnd.NextDouble() + rnd.NextDouble() - 2) * noisePower;

                    // Add noise to bitmap
                    wrapper.SetPixel(p, c.R + noise, c.G + noise, c.B + noise);
                }

                return res;
            }
        }
    }
}
