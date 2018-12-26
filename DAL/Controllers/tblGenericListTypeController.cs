using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{

    

    public class tblGenericListTypeController : BaseController
    {
        public tblGenericListTypeController()
        {
            this.EntitySetName = "tblGenericListTypes";
        }

        public bool Save(tblGenericListType record)
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

        public bool Delete(tblGenericListType record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblGenericListType Find(long id)
        {
            tblGenericListType rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGenericListTypes.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblGenericListType> FetchAll()
        {
            List<tblGenericListType> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGenericListTypes.ToList();
            }

            return rec;
        }
    }
}
