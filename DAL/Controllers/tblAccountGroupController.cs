using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblAccountGroupController : BaseController
    {
        public tblAccountGroupController()
        {
            this.EntitySetName = "tblAccountGroups";
        }

        public bool Save(tblAccountGroup record)
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

        public bool Delete(tblAccountGroup record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblAccountGroup Find(long id)
        {
            tblAccountGroup rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAccountGroups.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblAccountGroup> FetchAll()
        {
            List<tblAccountGroup> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAccountGroups.ToList();
            }

            return rec;
        }

        public List<tblAccountGroup> FetchByYearEndID(long yrEndFolID)
        {
            List<tblAccountGroup> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAccountGroups.Where(x => x.YearEndFolderID == yrEndFolID).ToList();
            }

            return rec;
        }

    }
}
