using Aspore.Drawing.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code39
{
    public class Code39Symbol
    {
        public const int SmallLinesInOneSymbol = 15;

        public bool[] Pattern;
        public readonly int SmallLineWidthPx;
        public readonly int BigLineWidthPx;

        public Code39Symbol(int smallLineWidthPx, int bigLineWidthPx, Bitmap bm)
        {
            Pattern = new bool[SmallLinesInOneSymbol];
            SmallLineWidthPx = smallLineWidthPx;
            BigLineWidthPx = bigLineWidthPx;
            FillPattern(bm);
            ValidatePattern();
        }

        private void FillPattern(Bitmap bm)
        {
            int line = 0;
            bool currentPx;
            bool firstPxInSmallLine = false;

            for (int px = 0; px < bm.Width; px++)
            {
                // If current px is first px in small line
                if (px == 0 || px % SmallLineWidthPx == 0)
                {
                    firstPxInSmallLine = bm.GetPixel(px, 0).IsBlackPixel();
                    Pattern[line] = firstPxInSmallLine;
                    line += 1;
                }
                else
                {
                    // If current px isn't first it should be same color as first px within small line.
                    // otherwise throwing exception.
                    currentPx = bm.GetPixel(px, 0).IsBlackPixel();
                    if (currentPx != firstPxInSmallLine)
                    {
                        throw new FormatException("Within a small line all pixels should be the same.");
                    }
                }
            }
        }


        /// <summary>
        /// Pattern cannot contain more than 3 black or 3 white small lines successively.
        /// otherwise throwing exception.
        /// </summary>
        private void ValidatePattern()
        {
            for(int i = 0; i < Pattern.Length; i++) 
            {
                if (i > 3)
                {
                    if (Pattern[i] == Pattern[i-1] && Pattern[i - 1] == Pattern[i-2] && Pattern[i - 2] == Pattern[i-3])
                    {
                        throw new FormatException("Pattern cannot contain more than 3 black or 3 white small lines successively.");
                    }
                }
            }
        }
    }
}
