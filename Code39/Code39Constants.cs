using System.Collections.Generic;
using System.Drawing;

namespace Code39
{
    public class Code39Constants
    {
        public static readonly Color Black = Color.FromArgb(0, 0, 0);
        public static readonly Color White = Color.FromArgb(255, 255, 255);

        public static readonly bool[] Asterisk = { true, false, false, false, true, false, true, true, true, false, true, true, true, false, true };
        public static readonly bool[] Zero = { true, false, true, false, false, false, true, true, true, false, true, true, true, false, true };
        public static readonly bool[] One = { true, true, true, false, true, false, false, false, true, false, true, false, true, true, true };        
        public static readonly bool[] Two = { true, false, true, true, true, false, false, false, true, false, true, false, true, true, true };
        public static readonly bool[] Three = { true, true, true, false, true, true, true, false, false, false, true, false, true, false, true };
        public static readonly bool[] Four = { true, false, true, false, false, false, true, true, true, false, true, false, true, true, true };
        public static readonly bool[] Five = { true, true, true, false, true, false, false, false, true, true, true, false, true, false, true };
        public static readonly bool[] Six = { true, false, true, true, true, false, false, false, true, true, true, false, true, false, true };
        public static readonly bool[] Seven = { true, false, true, false, false, false, true, false, true, true, true, false, true, true, true };
        public static readonly bool[] Eight = { true, true, true, false, true, false, false, false, true, false, true, true, true, false, true };
        public static readonly bool[] Nine = { true, false, true, true, true, false, false, false, true, false, true, true, true, false, true };
        
        public static readonly bool[] A = { true, true, true, false, true, false, true, false, false, false, true, false, true, true, true };
        public static readonly bool[] B = { true, false, true, true, true, false, true, false, false, false, true, false, true, true, true };
        public static readonly bool[] C = { true, true, true, false, true, true, true, false, true, false, false, false, true, false, true };
        public static readonly bool[] D = { true, false, true, false, true, true, true, false, false, false, true, false, true, true, true };
        public static readonly bool[] E = { true, true, true, false, true, false, true, true, true, false, false, false, true, false, true };
        public static readonly bool[] F = { true, false, true, true, true, false, true, true, true, false, false, false, true, false, true };
        public static readonly bool[] G = { true, false, true, false, true, false, false, false, true, true, true, false, true, true, true };
        public static readonly bool[] H = { true, true, true, false, true, false, true, false, false, false, true, true, true, false, true };
        public static readonly bool[] I = { true, false, true, true, true, false, true, false, false, false, true, true, true, false, true };
        public static readonly bool[] J = { true, false, true, false, true, true, true, false, false, false, true, true, true, false, true };
        public static readonly bool[] K = { true, true, true, false, true, false, true, false, true, false, false, false, true, true, true };
        public static readonly bool[] L = { true, false, true, true, true, false, true, false, true, false, false, false, true, true, true };
        public static readonly bool[] M = { true, true, true, false, true, true, true, false, true, false, true, false, false, false, true };
        public static readonly bool[] N = { true, false, true, false, true, true, true, false, true, false, false, false, true, true, true };
        public static readonly bool[] O = { true, true, true, false, true, false, true, true, true, false, true, false, false, false, true };
        public static readonly bool[] P = { true, false, true, true, true, false, true, true, true, false, true, false, false, false, true };
        public static readonly bool[] Q = { true, false, true, false, true, false, true, true, true, false, false, false, true, true, true };
        public static readonly bool[] R = { true, true, true, false, true, false, true, false, true, true, true, false, false, false, true };
        public static readonly bool[] S = { true, false, true, true, true, false, true, false, true, true, true, false, false, false, true };
        public static readonly bool[] T = { true, false, true, false, true, true, true, false, true, true, true, false, false, false, true };
        public static readonly bool[] U = { true, true, true, false, false, false, true, false, true, false, true, false, true, true, true };
        public static readonly bool[] V = { true, false, false, false, true, true, true, false, true, false, true, false, true, true, true };
        public static readonly bool[] W = { true, true, true, false, false, false, true, true, true, false, true, false, true, false, true };
        public static readonly bool[] X = { true, false, false, false, true, false, true, true, true, false, true, false, true, true, true };
        public static readonly bool[] Y = { true, true, true, false, false, false, true, false, true, true, true, false, true, false, true };
        public static readonly bool[] Z = { true, false, false, false, true, true, true, false, true, true, true, false, true, false, true };
        public static readonly bool[] SPACE = { true, false, false, false, true, true, true, false, true, false, true, true, true, false, true };

        public static Dictionary<bool[], char> Code39CharSet = new Dictionary<bool[], char>(new BoolArrayComparer()) 
        {
            { Asterisk, '*'},
            { Zero,  '0'},
            { One,   '1'},
            { Two,   '2'},
            { Three, '3'},
            { Four,  '4'},
            { Five,  '5'},
            { Six,   '6'},
            { Seven, '7'},
            { Eight, '8'},
            { Nine,  '9'},

            { A,  'A'},
            { B,  'B'},
            { C,  'C'},
            { D,  'D'},
            { E,  'E'},
            { F,  'F'},
            { G,  'G'},
            { H,  'H'},
            { I,  'I'},
            { J,  'J'},
            { K,  'K'},
            { L,  'L'},
            { M,  'M'},
            { N,  'N'},
            { O,  'O'},
            { P,  'P'},
            { Q,  'Q'},
            { R,  'R'},
            { S,  'S'},
            { T,  'T'},
            { U,  'U'},
            { V,  'V'},
            { W,  'W'},
            { X,  'X'},
            { Y,  'Y'},
            { Z,  'Z'},
            { SPACE,  ' '},
        };

        public static Dictionary<bool, Color> BlackWhitePixel = new Dictionary<bool, Color>()
        {
            { true, Color.FromArgb(0, 0, 0) },
            { false, Color.FromArgb(255, 255, 255) }
        };

    }
}
