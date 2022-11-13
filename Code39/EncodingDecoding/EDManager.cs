using System;
using Code39.IO;
using McMaster.Extensions.CommandLineUtils;
using Code39Encoder = Code39.ED.Encoding.Code39Encoder;
using Code39Decoder = Code39.EncodingDecoding.Decoding.Code39Decoder;
using System.Drawing;

namespace Code39.ED
{
    public static class EDManager
    {
        public static void Encode(CommandLineApplication command, IOutput output) 
        {
            CommandOption file = command.Option("-file", "Path to the Code39 image.", CommandOptionType.SingleValue);
            command.OnExecute((System.Action)(() =>
            {
                string imgPath = file.Value();
                if (!string.IsNullOrWhiteSpace(imgPath))
                {
                    var encoder = new Code39Encoder(imgPath);
                    string regularText = encoder.Encode();

                    output.OutRegularText($"Encoded Text: {Environment.NewLine}{regularText}{Environment.NewLine}");
                }
                else 
                {
                    throw new ArgumentException("Invalid img path");
                }
            }));
        }

        public static void Decode(CommandLineApplication command, IOutput output) 
        {
            CommandOption file = command.Option("-file", "Path to the Code39 image.", CommandOptionType.SingleValue);
            CommandOption text = command.Option("-text", "Regular text, which should be decoded to Code 39.", CommandOptionType.SingleValue);
            command.OnExecute(() => 
            {
                string regularText = text.Value();
                if (!string.IsNullOrWhiteSpace(text.Value()))
                {
                    string ouputFile = file.Value() == null ? $"{regularText}.jpg" : file.Value();

                    Code39Decoder decoder = new Code39Decoder(text.Value().ToUpper());
                    Bitmap code39 = decoder.Decode();
                    code39.Save(ouputFile);
                }
                else
                {
                    throw new ArgumentException("Text line is empty");
                }

            });
        }
    }
}
