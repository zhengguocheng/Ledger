using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class StatusController: BaseController
    {
        public StatusController()
        {
            this.EntitySetName = "tblStatus";
        }

        public bool Save(tblStatu record)
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

        public bool Delete(tblStatu record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblStatu Find(long id)
        {
            tblStatu rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblStatus.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblStatu> FetchAll()
        {
            List<tblStatu> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblStatus.ToList();
            }
            
            return rec;
        }

     

    }
   
}
