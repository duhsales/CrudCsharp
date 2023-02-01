using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public static class Extensoes
    {
        public static int ToInt(this object valor)
        {
            try { return Convert.ToInt32(valor); }
            catch { return 0; }
        }

        public static double ToDouble(this object valor)
        {
            try { return Convert.ToDouble(valor); }
            catch { return 0; }
        }

        public static DateTime ToDateTime(this object valor)
        {
            try { return Convert.ToDateTime(valor); }
            catch { return DateTime.Now.Date; }
        }
    }
}
