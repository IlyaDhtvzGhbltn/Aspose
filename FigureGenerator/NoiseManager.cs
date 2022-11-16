using Aspore.Drawing.Extensions;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FigureGenerator
{
    public static class NoiseManager
    {
        private static Color _figuresColor;
        private static Color _backgroundColor;

        public static void Generate(
            CommandLineApplication command, 
            Color figuresColor, 
            Color backgroundColor,
            CancellationTokenSource cts) 
        {
            _figuresColor = figuresColor;
            _backgroundColor = backgroundColor;

            CommandOption normalImg = command.Option("-normal", "Path to a normal bitmap file to save.", CommandOptionType.SingleOrNoValue);
            CommandOption gaussImg = command.Option("-gaussImg", "Path to a gause file to save.", CommandOptionType.SingleOrNoValue);
            CommandOption square = command.Option("-square", "Side of the circumscribed square.", CommandOptionType.SingleOrNoValue);
            CommandOption noiseValue = command.Option("-noiseValue", "Gausse noise value.", CommandOptionType.SingleValue);

            command.OnExecute(() => Task.Factory.StartNew(()=>
            {
                string figuresPathFile = normalImg.HasValue() ? normalImg.Value() : "figures.jpg";
                string gausPathFile = gaussImg.HasValue() ? gaussImg.Value() : "gause.jpg";
                int squareSidePx = square.HasValue() ? int.Parse(square.Value()) : 100;
                int gaus = int.Parse(noiseValue.Value());


                int width = (squareSidePx * 3) + 10;
                int height = squareSidePx * 2;

                using (Bitmap b = new Bitmap(width, height))
                {
                    b.SetBackground(backgroundColor);
                    b.PrintSquare(1, 1, squareSidePx, figuresColor);
                    b.PrintCircle(width / 2, height / 2, squareSidePx, figuresColor);
                    b.PrintTriangle(width - 2, height - 2, squareSidePx, figuresColor);

                    // Save normal bitmap
                    b.Save(figuresPathFile);

                    Bitmap gb = BitmapExtensions.AddGaussianNoise(b, gaus);

                    // Save Gauss
                    gb.Save(gausPathFile);
                    cts.Cancel();
                }
            }, cts.Token));
        }
    }
}
