using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{    
    public class TokenController : BaseController
    {
        public TokenController()
        {
            this.EntitySetName = "tblTokens";
        }

        public bool Save(tblToken record)
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

        public bool Delete(tblToken record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }
        
        public tblToken Find(long id)
        {
            tblToken rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblTokens.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblToken> FetchAll()
        {
            List<tblToken> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblTokens.ToList();
            }
            return rec;
        }

        public List<tblToken> FetchAll(int viewID)
        {
            List<tblToken> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblTokens.Where(x => x.ViewID == viewID).ToList();
            }
            return rec;
        }
    }

}
