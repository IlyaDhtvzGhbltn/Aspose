using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code39
{
    public class BoolArrayComparer : IEqualityComparer<bool[]>
    {
        public bool Equals(bool[] x, bool[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; ++i)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(bool[] obj)
        {
            int ret = 0;
            for (int i = 0; i < obj.Length; ++i)
            {
                ret ^= obj[i].GetHashCode();
            }
            return ret;
        }
    }
}
