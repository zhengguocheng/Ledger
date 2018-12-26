using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblLedgerController : BaseController
    {
        public tblLedgerController()
        {
            this.EntitySetName = "tblLedgers";
        }

        public bool Save(tblLedger record)
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

        public bool Delete(tblLedger record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblLedger Find(long id)
        {
            tblLedger rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblLedgers.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblLedger> FetchAll()
        {
            List<tblLedger> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblLedgers.ToList();
            }
            return rec;
        }

        public List<tblLedger> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblLedger> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblLedgers.Where(x => x.DocumentItemID == docItemID).ToList<tblLedger>();
            }
            return rec;
        }

        //public List<tblLedger> FetchNonZeroVat(long docItemID, int zeroVatRateID)
        //{
        //    List<tblLedger> rec = null;
        //    using (dbDMSEntities context = ContextCreater.GetContext())
        //    {
        //        rec = context.tblLedgers.Where(x => x.DocumentItemID == docItemID && x.VATRateID != zeroVatRateID).ToList<tblLedger>();
        //    }
        //    return rec;
        //}
    }
}
