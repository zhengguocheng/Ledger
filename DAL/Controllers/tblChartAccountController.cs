using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblChartAccountController : BaseController
    {
        public tblChartAccountController()
        {
            this.EntitySetName = "tblChartAccounts";
        }

        public bool Save(tblChartAccount record)
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

        public bool Delete(tblChartAccount record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblChartAccount Find(long id)
        {
            tblChartAccount rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblChartAccounts.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblChartAccount> FetchAll()
        {
            List<tblChartAccount> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblChartAccounts.ToList();
            }

            return rec;
        }

        public List<tblChartAccount> FetchByYearEndID(long yrEndFolID)
        {
            List<tblChartAccount> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblChartAccounts.Where(x => x.YearEndFolderID == yrEndFolID).ToList();
            }

            return rec;
        }

        public List<tblChartAccount> FetchByCode(string code)
        {
            List<tblChartAccount> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblChartAccounts.Where(x => x.Code == code).ToList();
            }
            return rec;
        }

        public tblChartAccount FetchByCode(string code, long yrEndFolID)//long yrEndFolID = 0 for Common NominalCode
        {
            tblChartAccount rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblChartAccounts.FirstOrDefault(x => x.YearEndFolderID == yrEndFolID && x.Code == code);
            }
            return rec;
        }

        public tblChartAccount FetchSimilarRecord(long similarNomCodeID,long yrEndFolID)
        {
            //Fetch record against a specific yrEndID  that has same Code as Code of similarNomCodeID
            tblChartAccount rec = null;

            var objParent = Find(similarNomCodeID);

            if(objParent != null)
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.tblChartAccounts.FirstOrDefault(x => x.YearEndFolderID == yrEndFolID && x.Code == objParent.Code);
                }
            }
            
            return rec;
        }

        public tblChartAccount FetchParentNomCode(long nomCodeID)
        {
            var rec = FetchSimilarRecord(nomCodeID, 0);//0 for parent
            return rec;
        }

    }
}
