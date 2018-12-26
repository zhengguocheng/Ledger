using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public enum SPNames { SpExcelSheetSummary, SpLedgerReport, SpRemoveDeletedItem, SpBankReconcile, SpNextYrOpnBal, DeleteOrphanSplitRows 
                            ,VATReportSP, FindnReplaceDescription}
    public class DBHelper
    {
        static readonly SqlConnection conn;
        static string error = string.Empty;
        static DBHelper()
        {
            conn = new SqlConnection();
        }
        public static string GetConnStr()
        {
            if (AppConstants.IsDeployed)
            {
                return @"Data Source=192.168.1.11\SQLEXPRESS;Initial Catalog=dbDMSIntegrated;User ID=admin;Password=admin;MultipleActiveResultSets=True;";
            }
            else
                return @"Data Source=SHAHBAZKHURRAM\SQLEXPRESS;Initial Catalog=dbDMSIntegrated;Integrated Security=True;MultipleActiveResultSets=True;";
        }
        public static SqlConnection GetActiveConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                return conn;
            }
            else
            {
                conn.ConnectionString = GetConnStr();
                conn.Open();
                return conn;
            }
        }

        public static DataSet GetDataSetWithSchema(string tableName)
        {
            string query = string.Format("select * from [{0}]", tableName);
            SqlDataAdapter dAdapter = new SqlDataAdapter(query, GetActiveConnection());
            DataSet ds = new DataSet();

            try
            {
                DataTable table = new DataTable();
                dAdapter.Fill(table);
                dAdapter.FillSchema(table, SchemaType.Mapped);
                ds.Tables.Add(table);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }
        public static decimal GetSum(string tableName, string columnName, string filter)
        {
            decimal i = 0;

            string query = string.Format("select SUM([{0}]) AS TotalAmount from [{1}] {2}", columnName, tableName, filter);
            SqlDataAdapter dAdapter = new SqlDataAdapter(query, GetActiveConnection());
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(query, GetActiveConnection());
            try
            {
                var obj = cmd.ExecuteScalar();
                decimal.TryParse(obj.ToString(), out i);

                //AgentProfiles eb = new AgentProfiles();
                //string q = string.Format("select SUM([{0}]) AS TotalAmount from [{1}] where ExpenseDate > @0 and ExpenseDate < @1", columnName, tableName);
                //object o = eb.ExecuteSingle(q, DateTime.Now, DateTime.Now);

            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return i;
        }
        public static decimal GetChequeSum(string tableName, string columnName, string filter)
        {
            decimal i = 0;

            string query = string.Format("select SUM([{0}]) AS TotalAmount from [{1}] ", columnName, tableName);
            SqlDataAdapter dAdapter = new SqlDataAdapter(query, GetActiveConnection());
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(query, GetActiveConnection());
            try
            {
                i = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return i;
        }

        #region generic methods

        public static DataTable GetTableData(string tableName)
        {
            string query = string.Format("select * from {0}", tableName);
            SqlDataAdapter dAdapter = new SqlDataAdapter(query, GetActiveConnection());
            DataSet ds = new DataSet();

            try
            {
                dAdapter.Fill(ds);
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds.Tables[0];
        }

        public static DataSet ExecuteSQL(string sql)
        {
            SqlDataAdapter dAdapter = new SqlDataAdapter(sql, GetActiveConnection());
            DataSet ds = new DataSet();

            try
            {
                dAdapter.Fill(ds);
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }

        public static object ExecuteScalar(string qry)
        {
            object obj;
            SqlCommand cmd = new SqlCommand(qry, GetActiveConnection());
            try
            {
                obj = cmd.ExecuteScalar();
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return obj;
        }

        public static int ExecuteNonQuery(string qry)
        {
            int recordEffected;
            SqlCommand cmd = new SqlCommand(qry, GetActiveConnection());
            try
            {
                recordEffected = cmd.ExecuteNonQuery();
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return recordEffected;
        }

        public static DataSet ExecuteSP(SPNames name, params SqlParameter[] lstSqlParam)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlCommand cmd = new SqlCommand(name.ToString(), GetActiveConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var p in lstSqlParam)
                {
                    if (!p.ParameterName.StartsWith("@"))
                        p.ParameterName = "@" + p.ParameterName;//cmd.Parameters.AddWithValue("@LastName", txtlastname);

                    cmd.Parameters.Add(p);
                }
                
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }

        public static void ExecuteSP_NoRead(SPNames name, params SqlParameter[] lstSqlParam)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(name.ToString(), GetActiveConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var p in lstSqlParam)
                {
                    if (!p.ParameterName.StartsWith("@"))
                        p.ParameterName = "@" + p.ParameterName;//cmd.Parameters.AddWithValue("@LastName", txtlastname);

                    cmd.Parameters.Add(p);
                }
                cmd.ExecuteNonQuery();
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        
        #endregion

    }
}
