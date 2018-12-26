using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    
    public class RecycleBinController : BaseController
    {
        public RecycleBinController()
        {
            this.EntitySetName = "tblRecycleBins";
        }

        public bool Save(tblRecycleBin record)
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

        public bool Delete(tblRecycleBin record)
        {
            return DeleteEntity(record);
        }

        public tblRecycleBin Find(long id)
        {
            tblRecycleBin rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.tblRecycleBins.FirstOrDefault(x => x.ID == id);
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<tblRecycleBin> FetchAll()
        {
            List<tblRecycleBin> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.tblRecycleBins.ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }
        
        
    }

}
