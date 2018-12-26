using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblSDBController : BaseController
    {
        public tblSDBController()
        {
            this.EntitySetName = "tblSDBs";
        }

        public bool Save(tblSDB record)
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

        public bool Delete(tblSDB record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblSDB Find(long id)
        {
            tblSDB rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSDBs.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblSDB> FetchAll()
        {
            List<tblSDB> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSDBs.ToList();
            }

            return rec;
        }

        public List<tblSDB> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblSDB> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSDBs.Where(x => x.DocumentItemID == docItemID).ToList<tblSDB>();
            }

            return rec;
        }

       
    }
}
