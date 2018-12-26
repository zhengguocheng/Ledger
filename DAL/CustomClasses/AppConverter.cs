using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class AppConverter
    {
        public static float ToFloat(object o, float defaultVal)
        {
            float n = defaultVal;
            if (o != null && float.TryParse(o.ToString(), out n))
                return n;
            return defaultVal;

        }

        public static decimal ToDecimal(object o, decimal defaultVal)
        {
            decimal n = defaultVal;
            if (o != null && decimal.TryParse(o.ToString(), out n))
                return n;
            return defaultVal;

        }

        public static int ToInt(object o, int defaultVal)
        {
            int n = defaultVal;
            if (o != null && int.TryParse(o.ToString(), out n))
                return n;
            return defaultVal;
        }

        public static bool ToBool(object o, bool defaultVal)
        {
            bool n = defaultVal;
            if (o != null && bool.TryParse(o.ToString(), out n))
                return n;
            return defaultVal;
        }
    }
}
