using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code39.IO
{
    public class ConsoleOut : IOutput
    {
        public void OutImg(Bitmap bm)
        {
            throw new NotImplementedException();
        }

        public void OutRegularText(string encoded)
        {
            Console.WriteLine(encoded);
        }
    }
}
