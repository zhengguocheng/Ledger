using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace DAL
{

    public static class ExtentionMethods
    {
        //Return whether DataSet contains DataRow(s) or not
        public static bool IsNullOrEmpty(this DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0)
                return true;

            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count > 0)
                    return false;
            }

            return true;
        }

        public static DateTime? ToDateTime(this string str)
        {

            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime d = Convert.ToDateTime(str);
                    return d;
                }

            }
            catch (Exception ecp)
            {
                //return null;
            }
            return null;
        }


        public static Single? ToSingle(this string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Trim();
                    string newStr = "";
                    foreach (char c in str)
                    {
                        if (c != ' ')
                            newStr += c;
                    }
                    return Convert.ToSingle(newStr);
                }
            }
            catch { }
            return null;
        }

        public static string ToDateString(this string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Replace("12:00:00 AM",string.Empty);
                }
            }
            catch { }
            return str;
        }


        public static string ToDB_Bool(this bool val)
        {
            return (val) ? "1" : "0";
        }

        static StringBuilder sb = new StringBuilder();
        public static string ToFirstCaps(this string str)
        {
            sb.Clear();
            bool found = false;
            str = str.ToLower();
            for (int i = 0; i < str.Length; i++)
            {
                if (found == false && Char.IsLetter(str[i]))
                {
                    sb.Append(str[i].ToString().ToUpper());
                    found = true;
                }
                else
                    sb.Append(str[i]);
            }

            return sb.ToString();
        }

        public static  string FirstLetterToUpper(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1).ToLower();

            return str.ToUpper();


        }

        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string CamelCaseToString(this string str)
        {
            sb.Clear();
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i == 0 && Char.IsLetter(str[i]))
                    {
                        sb.Append(str[i].ToString().ToUpper());
                    }
                    else if (Char.IsLetter(str[i]) && char.IsUpper(str[i]))
                    {
                        sb.Append(" " + str[i].ToString().ToUpper());
                    }
                    else
                        sb.Append(str[i]);
                }

                return sb.ToString();
            }
            catch (Exception ecp)
            {
                return str;
            }
        }




    }

}
