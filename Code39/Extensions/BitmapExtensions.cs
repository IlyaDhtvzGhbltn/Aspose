using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Code39.Extensions
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
                    int g = GreyPixel(rgb);
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
                    int blackOrWhite = BitPixel(rgb);
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
                bool flag = IsBlackPixel(b.GetPixel(i, 0));
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
                bool flag = IsWhitePixel(b.GetPixel(i, 0));
                if (flag) 
                {
                    int width = (i - startIndex) + 1;
                    return (smallLineWidth: width, bigLineWidth: width * format);
                }
            }
            throw new FormatException("Invalid Code39 format.");
        }


        /// <summary>
        /// Checking is pixel absolute black (R=0 G=0 B=0) or absolute white (R=255 G=255 B=255)
        /// Otherwise throwing exeption.
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns>true or false</returns>
        public static bool IsBlackPixel(Color pixel) 
        {
            if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
            {
                return true;
            }            
            if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
            {
                return false;
            }
            throw new Exception("Pixel isn't pure white or pure black. Execute ToBlackOrWhite(...) method before.");
        }

        /// <summary>
        /// Checking is pixel absolute white (R=255 G=255 B=255) or absolute black (R=0 G=0 B=0)
        /// Otherwise throwing exeption.
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns>true or false</returns>
        public static bool IsWhitePixel(Color pixel)
        {
            if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
            {
                return false;
            }
            if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
            {
                return true;
            }
            throw new Exception("Pixel isn't pure white or pure black. Execute ToBlackOrWhite(...) method before.");
        }



        private static int GreyPixel(Color c) 
        {
            int R = c.R;
            int G = c.G;
            int B = c.B;
            int avg = (R + G + B) / 3;
            return avg;
        }        
        
        private static int BitPixel(Color c) 
        {
            int R = c.R;
            int G = c.G;
            int B = c.B;
            int avg = (R + G + B) / 3;
            int bOrW = avg < 128 ? 0 : 255;

            return bOrW;
        }
    }
}
