using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GroupController : BaseController
    {
        public GroupController()
        {
            this.EntitySetName = "tblGroups";
        }

        public bool Save(tblGroup record)
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

        public bool Delete(tblGroup record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblGroup Find(long id)
        {
            tblGroup rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGroups.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public tblGroup Find(string name)
        {
            tblGroup rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGroups.FirstOrDefault(x => x.GroupName == name);
            }
            return rec;
        }

        public List<tblGroup> FetchAll()
        {
            List<tblGroup> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGroups.ToList();
            }

            return rec;
        }


    }

}
