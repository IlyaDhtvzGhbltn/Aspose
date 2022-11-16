using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Aspore.Drawing.Extensions;
using McMaster.Extensions.CommandLineUtils;
using System.Threading;

namespace FigureGenerator
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            try
            {
                var app = new CommandLineApplication();
                app.Command("generate", (c) => NoiseManager.Generate(c, 
                    Color.FromArgb(0, 0, 0), 
                    Color.FromArgb(255, 255, 255), 
                    cts));
                await app.ExecuteAsync(args).ConfigureAwait(false);

                ConsoleLoading();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("End of programm. Press any key to exit.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                cts?.Dispose();
            }
        }

        static void ConsoleLoading() 
        {
            char[] waitSymbols = new [] {'|','/', '-', '\\' };

            while (!cts.IsCancellationRequested)
            {
                foreach (char c in waitSymbols)
                {
                    Console.Clear();
                    Console.WriteLine($"Please wait...{c}");
                    Thread.Sleep(100);
                }
            }
            Console.Clear();
        }
    }
}
