using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblItemInfoController : BaseController
    {
        public tblItemInfoController()
        {
            this.EntitySetName = "tblItemInfoes";
        }

        public bool Save(tblItemInfo record)
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

        public bool Delete(tblItemInfo record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblItemInfo Find(long id)
        {
            tblItemInfo rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblItemInfoes.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblItemInfo> FetchAll()
        {
            List<tblItemInfo> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblItemInfoes.ToList();
            }
            return rec;
        }

        public List<tblItemInfo> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblItemInfo> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblItemInfoes.Where(x => x.DocumentItemID == docItemID).ToList<tblItemInfo>();
            }
            return rec;
        }

    }
}
