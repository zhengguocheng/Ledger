using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class AppDataTable
    {
        static string GetFilterString(string colName, object val)
        {
            var filter = string.Format("[{0}] = '{1}'", colName, val);
            return filter;
        }

        public static decimal GetSum(string colName, DataTable dt)
        {
            //var qry = string.Format("SUM({0})", colName);
            //var obj = dt.Compute(qry,string.Empty);
            //return Convert.ToSingle(obj);
            //var result = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x[colName]));

            decimal n = 0;
            decimal total = 0;

            foreach (DataRow dr in dt.Rows)
            {
                n = 0;
                if (dr[colName] != DBNull.Value && decimal.TryParse(dr[colName].ToString(), out n))
                {
                    total += n;
                }
            }

            return total;
        }

        public static bool ExistRow(string colName, object val, DataTable dt)
        {
            var arr = FilterRows(GetFilterString(colName, val), dt);
            if (arr != null && arr.Length > 0)
                return true;
            else
                return false;
        }
        public static bool ExistRow(string filter, DataTable dt)
        {
            var arr = FilterRows(filter, dt);
            if (arr != null && arr.Length > 0)
                return true;
            else
                return false;
        }

        public static DataRow[] FilterRows(string filter, DataTable dt)
        {
            var arr = dt.Select(filter);
            return arr;
        }

        public static DataRow[] FilterRows(string colName, object val, DataTable dt)
        {
            var filter = GetFilterString(colName, val);

            //try
            //{

            var arr = dt.Select(filter);
            return arr;
            //}
            //catch (Exception ecp)
            //{
            //    throw new Exception("App Exception: " + filter);
            //}
        }
    }
}
