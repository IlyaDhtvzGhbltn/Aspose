using System;
using System.Drawing;
using Aspore.Drawing.Extensions;
using System.Drawing.Imaging;


namespace Code39.ED.Encoding
{
    public class Code39Encoder
    {
        private readonly string filePath;

        public Code39Encoder(string filePath)
        {
            this.filePath = filePath;
        }

        public string Encode() 
        {
            var bm = new Bitmap(filePath);

            // A 1px high bar contains the all information.
            // No need convert all picture to black and white.
            var line = bm.ToLine();

            // Code39 contains only black and white pixels.
            // Convert RGB to black and white.
            line.ToGrey();
            line.ToBlackOrWhite();

            // According the standard Code39 line begins from `*` symbol.
            // The `*` starts with small black line and then big white line after that.

            // Finding a Code39 line beginning position otherwise throwing exception.
            // Since the `*` always stays at the line first and starts with a black pixel,
            // we are looking for a black pixel first.
            int startPx = line.GetBlackPixelIndex();

            // Finding a width of the small and big lines
            // We know that startpx is black, so no need to check it
            var widthes = line.GetSmallAndBigLinesWidthPx(startPx + 1);

            // Any Code39 character could contains only 15 small lines.
            int codeWidth = widthes.smallLineWidth * 15;
            int codeCount = line.Width / codeWidth;

            bool firstAsteriskPresented = false;
            string text = string.Empty;

            for (int i = 0; i < codeCount; i++)
            {
                // Space between Code39 characters equals small string and must be removed
                int space = i * widthes.smallLineWidth;
                Rectangle cropArea = new Rectangle(startPx + i * codeWidth + space, 0, codeWidth, 1);
                Bitmap slice = line.Clone(cropArea, PixelFormat.Format32bppRgb);

                var symbol = new Code39Symbol(widthes.smallLineWidth, widthes.bigLineWidth, slice);
                char @char = Code39Constants.Code39CharSet[symbol.Pattern];

                // Asterisk is not printed 
                if (@char == '*')
                {
                    // First Asterisk indicates to start of line.
                    if (!firstAsteriskPresented)
                    {
                        firstAsteriskPresented = true;
                    }
                    else
                    {
                        // Second Asterisk indicates to end of line.
                        // Exit from cycle
                        break;
                    }
                }
                else
                {
                    text += @char;
                }
                slice?.Dispose();
            }

            return text;
        }
    }
}
