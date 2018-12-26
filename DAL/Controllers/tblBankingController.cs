using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblBankingController : BaseController
    {
        public tblBankingController()
        {
            this.EntitySetName = "tblBankings";
        }

        public bool Save(tblBanking record)
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

        public bool Delete(tblBanking record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblBanking Find(long id)
        {
            tblBanking rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankings.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblBanking> FetchAll()
        {
            List<tblBanking> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankings.ToList();
            }
            return rec;
        }

        public List<tblBanking> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblBanking> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankings.Where(x => x.DocumentItemID == docItemID).ToList<tblBanking>();
            }
            return rec;
        }

        public List<tblBanking> FetchNonZeroVat(long docItemID, int zeroVatRateID)
        {
            List<tblBanking> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankings.Where(x => x.DocumentItemID == docItemID && x.VATRateID != zeroVatRateID).ToList<tblBanking>();
            }
            return rec;
        }
    }
}
