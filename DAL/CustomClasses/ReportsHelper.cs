using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ReportsHelper
    {
        public static DataTable PaymentSummary(long docItemID)
        {
            LedgerRepository repCntr = new LedgerRepository();
            var yrEndFol = repCntr.GetYearEndFolder(docItemID);

            var ds = DBHelper.ExecuteSP(SPNames.SpExcelSheetSummary, new SqlParameter("@docItemID", docItemID), new SqlParameter("@yrEndFolID", yrEndFol.ID));

            var rptDt = ds.Tables[0].Copy();

            rptDt.Merge(ds.Tables[1]);
            rptDt.Merge(ds.Tables[2]);

            return rptDt;

        }

        public static DataTable PaymentSheet(long docItemID, out string clientName)
        {
            clientName = string.Empty;
            LedgerRepository repCntr = new LedgerRepository();
            var yrEndFol = repCntr.GetYearEndFolder(docItemID);

            var ds = DBHelper.ExecuteSP(SPNames.SpExcelSheetSummary, new SqlParameter("@docItemID", docItemID), new SqlParameter("@yrEndFolID", yrEndFol.ID));
            var dt = ProcessSplitRows(ds.Tables[3]);

            if(ds.Tables.Count >= 5)
            {
                var dtName = ds.Tables[4];
                clientName = dtName.Rows[0][0].ToString();
            }
            return dt;
        }

        public static DataTable ProcessSplitRows(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (
                    AppConstants.sIsSplitText(dr["Description"].ToString())
                    //string.Equals(dr["Description"].ToString(), AppConstants.splitValue)
                    )
                {
                    dr["Date"] = DBNull.Value;
                    dr["Reference"] = DBNull.Value;
                    dr["Gross"] = DBNull.Value;
                    dr["VATCode"] = DBNull.Value;
                    dr["VAT"] = DBNull.Value;
                }
            }
            return dt;
        }

        
    }
}
