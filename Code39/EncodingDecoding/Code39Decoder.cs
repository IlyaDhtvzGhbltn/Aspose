using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Code39.EncodingDecoding.Decoding
{
    public class Code39Decoder
    {
        private readonly string regularText;
        private readonly int lineBetweenChars;
        private readonly int linesInOneChar = 15;
        private readonly Regex regex = new Regex("^[\\w, \\d, \\s]{0,}$");

        /// <summary>
        /// Asterisk in the beginning and in the end will be added inside constructor.
        /// </summary>
        /// <param name="regularText">Regular text without asterisks</param>
        /// <param name="smallLineWidthPx">Small line's width in pixels</param>
        public Code39Decoder(string regularText)
        {
            Validate(regularText);
            this.regularText = $"*{regularText}*";
            this.lineBetweenChars = 1;
        }

        public Bitmap Decode() 
        {
            // Any Code39 starts and ends with an asterisk.
            // Between two chars in code39 stay one white small line.

            //All small white lines between chars width
            int dividesWidth = (regularText.Length - 1) * lineBetweenChars;

            // Any Code39 character contains 15 small lines.
            int width = (regularText.Length * lineBetweenChars * linesInOneChar) + dividesWidth;
            Bitmap code39 = new Bitmap(width, 1);


            int betweenCharsSpace = 0;
            for (int charIndx = 0; charIndx < regularText.Length; charIndx++)
            {
                char @char = regularText[charIndx];
                bool[] trueFallseArr = Code39Constants.Code39CharSet.First(x => x.Value == @char).Key;

                for (int flagIndx = 0; flagIndx < trueFallseArr.Length; flagIndx++)
                {
                    var color = Code39Constants.BlackWhitePixel[trueFallseArr[flagIndx]];
                    for (int pxPos = 0; pxPos < lineBetweenChars; pxPos++)
                    {
                        code39.SetPixel((charIndx * linesInOneChar * lineBetweenChars) + flagIndx + pxPos + betweenCharsSpace, 0, color);
                    }
                }
                betweenCharsSpace += lineBetweenChars;
            }
            return Resize(code39);
        }

        private void Validate(string decodedText) 
        {
            bool valid = regex.IsMatch(decodedText);
            if (!valid)
            {
                throw new FormatException("Decoded Text contains invalid characters. Only latin, digits and spaces supported.");
            }
        }

        private Bitmap Resize(Bitmap b, int height = 30) 
        {
            Bitmap resized = new Bitmap(b, new Size(b.Width, height));
            return resized;
        }
    }
}
