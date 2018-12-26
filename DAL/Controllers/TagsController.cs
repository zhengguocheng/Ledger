using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class TagsController : BaseController
    {
        public TagsController()
        {
            this.EntitySetName = "tblTags";
        }

        public bool Save(tblTag record)
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

        public bool Delete(tblTag record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblTag Find(long id)
        {
            tblTag rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblTags.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblTag> FetchAll()
        {
            List<tblTag> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblTags.ToList();
            }

            return rec;
        }
    }

}
