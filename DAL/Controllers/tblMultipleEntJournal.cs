using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Controllers
{   
    public class tblMultipleEntJournalController : BaseController
    {
        public tblMultipleEntJournalController()
        {
            this.EntitySetName = "tblMultipleEntJournals";
        }

        public bool Save(tblMultipleEntJournal record)
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

        public bool Delete(tblMultipleEntJournal record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblMultipleEntJournal Find(long id)
        {
            tblMultipleEntJournal rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblMultipleEntJournals.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblMultipleEntJournal> FetchAll()
        {
            List<tblMultipleEntJournal> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblMultipleEntJournals.ToList();
            }
            return rec;
        }

        public List<tblMultipleEntJournal> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblMultipleEntJournal> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblMultipleEntJournals.Where(x => x.DocumentItemID == docItemID).ToList<tblMultipleEntJournal>();
            }
            return rec;
        }

       
    }
}
