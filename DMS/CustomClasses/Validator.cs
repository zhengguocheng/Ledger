using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMS
{
    public class CustomValidator
    {
        public static bool IsInt(object val)
        {
            int n;
            if (val == null) 
                return false;
            if (int.TryParse(val.ToString().Trim(), out n))
                return true;
            else
                return false;
        }

        public static bool IsDecimal(object val)
        {
            decimal n;
            
            if (val == null)
                return false;
            if (decimal.TryParse(val.ToString().Trim(), out n))
                return true;
            else
                return false;
        }
                
        public static bool IsDecimal(object val,int percision)
        {
            decimal n;

            if (val == null)
                return false;

            bool valid = false;

            if (decimal.TryParse(val.ToString().Trim(), out n))
            {
                string s = n.ToString();
                //s = s.TrimEnd('0');
                int st = s.IndexOf('.');
                if ( st == -1 || (s.Length - 1) - st <= percision)
                    valid = true;
            }

            return valid;
        }

        public static bool Isfloat(object val)
        {
            float n;
            if (val == null)
                return false;
            if (float.TryParse(val.ToString().Trim(), out n))
                return true;
            else
                return false;
        }

        public static bool IsDate(object val)
        {
            DateTime n;
            if (val == null)
                return false;
            if (DateTime.TryParse(val.ToString().Trim(), out n))
                return true;
            else
                return false;
        }
    }
}
