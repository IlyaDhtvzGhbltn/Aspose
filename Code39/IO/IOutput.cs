using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code39.IO
{
    public interface IOutput
    {
        void OutRegularText(string encoded);
        void OutImg(Bitmap bm);
    }
}
