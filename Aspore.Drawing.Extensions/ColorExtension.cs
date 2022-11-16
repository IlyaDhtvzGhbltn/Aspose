using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspore.Drawing.Extensions
{
    public static class ColorExtension
    {

        /// <summary>
        /// Checking is pixel absolute black (R=0 G=0 B=0) or absolute white (R=255 G=255 B=255)
        /// Otherwise throwing exeption.
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns>true or false</returns>
        public static bool IsBlackPixel(this Color pixel)
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
        public static bool IsWhitePixel(this Color pixel)
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



        public static int ToGreyPixel(this Color c)
        {
            int R = c.R;
            int G = c.G;
            int B = c.B;
            int avg = (R + G + B) / 3;
            return avg;
        }

        public static int ToBlackWhitePixel(this Color c)
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
