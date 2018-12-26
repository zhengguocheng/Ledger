using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblAccountController : BaseController
    {
        public tblAccountController()
        {
            this.EntitySetName = "tblAccounts";
        }

        public bool Save(tblAccount record)
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

        public bool Delete(tblAccount record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblAccount Find(long id)
        {
            tblAccount rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAccounts.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public tblAccount FindByClientID(long id)
        {
            tblAccount rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAccounts.FirstOrDefault(x => x.ClientID == id);
            }
            return rec;
        }

        public List<tblAccount> FetchAll()
        {
            List<tblAccount> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAccounts.ToList();
            }

            return rec;
        }



    }
}
