using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL.Controllers
{
    public class tblExcelSheetController : BaseController
    {
        public tblExcelSheetController()
        {
            this.EntitySetName = "tblExcelSheets";
        }

        public bool Save(tblExcelSheet record)
        {
            if(
                AppConstants.sIsSplitText(record.Description) 
                //record.Description == AppConstants.splitValue 
                && record.SplitParentID == null)
            {
                throw new Exception("Split row must have SplitParentID");
            }
            if (record.NominalCode == null)
            {
                throw new Exception("record must have nominal code.");
            }

            if (record.ID == 0)
            {
                return this.AddEntity(record);
            }
            else
            {
                return this.UpdateEntity(record);
            }
        }

        public bool Delete(tblExcelSheet record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblExcelSheet Find(long id)
        {
            tblExcelSheet rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblExcelSheets.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblExcelSheet> FetchAll()
        {
            List<tblExcelSheet> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblExcelSheets.ToList();
            }
            return rec;
        }

        public List<tblExcelSheet> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblExcelSheet> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblExcelSheets.Where(x => x.DocumentItemID == docItemID).ToList<tblExcelSheet>();
            }
            return rec;
        }

        public List<tblExcelSheet> FetchNonZeroVat(long docItemID, int zeroVatRateID)
        {
            List<tblExcelSheet> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblExcelSheets.Where(x => x.DocumentItemID == docItemID && x.VATCode != zeroVatRateID).ToList<tblExcelSheet>();
            }
            return rec;
        }

        public static void FindReplaceDescription(string descOld, string descNew)
        {
            DBHelper.ExecuteSP_NoRead(SPNames.FindnReplaceDescription,
                new SqlParameter("@descOld", descOld),
                new SqlParameter("@descNew", descNew));
        }

        public DataTable GetReconcileData(long docItemID, int stPeriod, int endPeriod, long NomCodeID, bool? ticked = null)
        {
            LedgerRepository repCntr = new LedgerRepository();
            var fol = repCntr.GetYearEndFolder(docItemID);

            var ds = DBHelper.ExecuteSP(SPNames.SpBankReconcile,
                //new SqlParameter("@ClientID", clientID), 
                new SqlParameter("@stPeriod", stPeriod),
                new SqlParameter("@endPeriod", endPeriod),
                new SqlParameter("@yrEndFolderID", fol.ID),
                new SqlParameter("@NominalCodeID", NomCodeID)
                );

            var dt = ds.Tables[0];

            if (ticked.HasValue)
            {
                if (ticked == true)
                {
                    var dv = dt.DefaultView;
                    dv.RowFilter = "Tick = true";
                    dt = dv.ToTable();
                }
                else
                {
                    var dv = dt.DefaultView;
                    dv.RowFilter = "Tick = false OR Tick IS NULL";
                    dt = dv.ToTable();
                }
            }
            return dt;
        }
        
    }
}
