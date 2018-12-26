using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
   
    public class tblDescriptionController : BaseController
    {
        public tblDescriptionController()
        {
            this.EntitySetName = "tblDescriptions";
        }

        public bool Save(tblDescription record)
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

        public bool Delete(tblDescription record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblDescription Find(long id)
        {
            tblDescription rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblDescriptions.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblDescription> FetchAll()
        {
            List<tblDescription> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblDescriptions.ToList();
            }

            return rec;
        }



    }
}
