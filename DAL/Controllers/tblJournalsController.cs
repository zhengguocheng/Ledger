using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Controllers
{   
    public class tblJournalsController : BaseController
    {
        public tblJournalsController()
        {
            this.EntitySetName = "tblJournals";
        }

        public bool Save(tblJournal record)
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

        public bool Delete(tblJournal record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblJournal Find(long id)
        {
            tblJournal rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblJournals.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblJournal> FetchAll()
        {
            List<tblJournal> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblJournals.ToList();
            }
            return rec;
        }

        public List<tblJournal> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblJournal> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblJournals.Where(x => x.DocumentItemID == docItemID).ToList<tblJournal>();
            }
            return rec;
        }

       
    }
}
