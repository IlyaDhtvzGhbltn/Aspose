using Code39.Extensions;
using System.Drawing;
using System;
using System.IO;
using System.Drawing.Imaging;
using McMaster.Extensions.CommandLineUtils;
using Code39.IO;

namespace Code39
{
    class Program
    {
        static void Main(string[] args)
        {
            var cw = new ConsoleOut();
            var bm = new BitmapOut();

            try
            {
                var app = new CommandLineApplication();
                app.Command("encode", (command) => Code39.ED.EDManager.Encode(command, cw));
                app.Command("decode", (command) => Code39.ED.EDManager.Decode(command, bm));
                app.Execute(args);
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
            }

        }
    }
}
