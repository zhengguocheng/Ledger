using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblBankReconcileController : BaseController
    {
        public tblBankReconcileController()
        {
            this.EntitySetName = "tblBankReconciles";
        }

        public bool Save(tblBankReconcile record)
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

        public bool Delete(tblBankReconcile record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblBankReconcile Find(long id)
        {
            tblBankReconcile rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankReconciles.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblBankReconcile> FetchAll()
        {
            List<tblBankReconcile> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankReconciles.ToList();
            }
            return rec;
        }

        public List<tblBankReconcile> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblBankReconcile> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankReconciles.Where(x => x.DocumentItemID == docItemID).ToList<tblBankReconcile>();
            }
            return rec;
        }

     
    }
}
