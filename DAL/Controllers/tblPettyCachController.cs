using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblPettyCashController : BaseController
    {
        public tblPettyCashController()
        {
            this.EntitySetName = "tblPettyCashes";
        }

        public bool Save(tblPettyCash record)
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

        public bool Delete(tblPettyCash record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblPettyCash Find(long id)
        {
            tblPettyCash rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPettyCashes.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblPettyCash> FetchAll()
        {
            List<tblPettyCash> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPettyCashes.ToList();
            }
            return rec;
        }

        public List<tblPettyCash> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblPettyCash> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPettyCashes.Where(x => x.DocumentItemID == docItemID).ToList<tblPettyCash>();
            }
            return rec;
        }

        public List<tblPettyCash> FetchNonZeroVat(long docItemID, int zeroVatRateID)
        {
            List<tblPettyCash> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPettyCashes.Where(x => x.DocumentItemID == docItemID && x.VATRateID != zeroVatRateID).ToList<tblPettyCash>();
            }
            return rec;
        }
    }
}
