using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{   
    public class tblClosingTrialBalanceController : BaseController
    {
        public tblClosingTrialBalanceController()
        {
            this.EntitySetName = "tblClosingTrialBalances";
        }

        public bool Save(tblClosingTrialBalance record)
        {
            if (record.ID == 0)
            {
                return this.AddEntity(record);
            }
            else
            {
                return this.UpdateEntity(record);
            }
        }

        public bool Delete(tblClosingTrialBalance record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblClosingTrialBalance Find(long id)
        {
            tblClosingTrialBalance rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblClosingTrialBalances.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblClosingTrialBalance> FetchAll()
        {
            List<tblClosingTrialBalance> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblClosingTrialBalances.ToList();
            }
            return rec;
        }

        public List<tblClosingTrialBalance> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblClosingTrialBalance> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblClosingTrialBalances.Where(x => x.DocumentItemID == docItemID).ToList<tblClosingTrialBalance>();
            }
            return rec;
        }

        public bool CopyData(long srcDocID, long desDocID)
        {
            var lst = FetchAllByDocumentItemID(srcDocID);

            foreach (var srcItem in lst)
            {
                var newItem = new tblClosingTrialBalance();
                newItem.NominalCodeID = srcItem.NominalCodeID;
                newItem.Debit = srcItem.Debit;
                newItem.Credit = srcItem.Credit;
                newItem.Description = srcItem.Description;
                newItem.DocumentItemID = desDocID;

                Save(newItem);
            }

            return true;
        }

        public bool CopyData(DataTable dt , long desDocID)
        {
            LedgerRepository rp = new LedgerRepository();
            var docYE = rp.GetYearEndFolder(desDocID);

            tblChartAccountController nomCodeCnt = new tblChartAccountController();
            var lstNomCode = nomCodeCnt.FetchByYearEndID(docYE.ID);

            foreach (DataRow srcItem in dt.Rows)
            {
                string nomCode = srcItem["NominalCode"].ToString();
                
                if(nomCode != null)
                {
                    var objNC = lstNomCode.FirstOrDefault(x => x.Code == nomCode.Trim());
                    if(objNC != null)
                    {
                        var newItem = new tblClosingTrialBalance();
                        newItem.NominalCodeID = objNC.ID;
                        newItem.Debit = Convert.ToDecimal(srcItem["Debit"]);
                        newItem.Credit = Convert.ToDecimal(srcItem["Credit"]);
                        newItem.Description = srcItem["Description"].ToString();
                        newItem.DocumentItemID = desDocID;

                        Save(newItem);
                    }
                }
            }

            return true;
        }
    }
}
