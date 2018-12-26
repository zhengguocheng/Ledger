using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblPCBController : BaseController
    {
        public tblPCBController()
        {
            this.EntitySetName = "tblPCBs";
        }

        public bool Save(tblPCB record)
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

        public bool Delete(tblPCB record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblPCB Find(long id)
        {
            tblPCB rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPCBs.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblPCB> FetchAll()
        {
            List<tblPCB> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPCBs.ToList();
            }
            return rec;
        }

        public List<tblPCB> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblPCB> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPCBs.Where(x => x.DocumentItemID == docItemID).ToList<tblPCB>();
            }
            return rec;
        }

        public List<tblPCB> FetchNonZeroVat(long docItemID, int zeroVatRateID)
        {
            List<tblPCB> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblPCBs.Where(x => x.DocumentItemID == docItemID && x.VATRateID != zeroVatRateID).ToList<tblPCB>();
            }
            return rec;
        }
    }
}
