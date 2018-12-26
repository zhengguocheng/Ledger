using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DAL
{
    public class ContextCreater
    {
        public static dbDMSEntities GetContext()
        {
            return new dbDMSEntities(AppConstants.ConnString);
        }

        public static SqlConnection GetSqlConnection()
        {
            //return new dbDMSEntities(AppConstants.ConnString).Connection.;

            dbDMSEntities ctx = new dbDMSEntities(AppConstants.ConnString);
            EntityConnection ec = (EntityConnection)ctx.Connection;
            SqlConnection sc = (SqlConnection)ec.StoreConnection;
            return sc;
        }

        public static DataSet ExecuteDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = ContextCreater.GetSqlConnection())
            {
                con.Open();
                cmd.Connection = con;
                using (cmd)
                {
                    SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);
                    dAdapter.Fill(ds);
                }
            }
            return ds;                
        }


    }
}
