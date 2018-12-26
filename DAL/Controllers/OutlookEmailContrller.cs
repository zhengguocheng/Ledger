using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class OutlookEmailContrller : BaseController
    {
        public OutlookEmailContrller()
        {
            this.EntitySetName = "tblOutlookEmails";
        }

        public bool Exist(tblOutlookEmail input)
        {
            tblOutlookEmail itm = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                itm = context.tblOutlookEmails.FirstOrDefault(
                                                                    x => x.EntryID == input.EntryID ||
                                                                    (x.Subject == input.Subject && 
                                                                    x.ReceivedTime == input.ReceivedTime && 
                                                                    x.SenderEmailAddress == input.SenderEmailAddress 
                                                                    && x.ToAddress == input.ToAddress)
                                                             );
            }

            return itm != null;
        }

        public bool Create(tblOutlookEmail record)
        {
            record.CreatedAt = DateTime.Now;
            if (Exist(record))
                return false;

            record.HTMLBody = null;//dont save body in db
            if (record.ID == 0)
            {
                return this.AddEntity(record);
            }
            else
            {
                return this.UpdateEntity(record);
            }
        }

        public bool Update(tblOutlookEmail record)
        {
            return this.UpdateEntity(record);
        }

        public bool Delete(tblOutlookEmail record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblOutlookEmail Find(long id)
        {
            tblOutlookEmail rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblOutlookEmails.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblOutlookEmail> FetchAll()
        {
            List<tblOutlookEmail> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblOutlookEmails.ToList();
            }

            return rec;
        }

        public List<tblOutlookEmail> FetchAllByEmail(string emailID)
        {
            if (string.IsNullOrWhiteSpace(emailID))
                return new List<tblOutlookEmail>();

            List<tblOutlookEmail> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblOutlookEmails.Where(x => (
                    x.BCC.Contains(emailID) || x.CC.Contains(emailID) || x.ToAddress.Contains(emailID) || x.SenderEmailAddress.Contains(emailID)
                    )).ToList();
            }

            rec.FindAll(x =>(
                            (x.BCC != null && x.BCC.Contains(emailID)) ||
                            (x.CC != null && x.CC.Contains(emailID)) ||
                            (x.ToAddress != null && x.ToAddress.Contains(emailID))
                            )
                        ).ToList()
                                .ForEach(x => x.IsSent = true);//means ivan has sent email to specific id. All remeaning are emails are recieved(issent = false)

            return rec;
        }

        public List<tblOutlookEmail> FetchAllByList(List<string> lstEmail)
        {
            List<tblOutlookEmail> rec = new List<tblOutlookEmail>();
            foreach (string email in lstEmail)
            {
                List<tblOutlookEmail> lst = FetchAllByEmail(email);
                rec = rec.Union(lst).ToList();//Union of list to erase duplicate. But it uses equality comparison.
            }
            return rec;
        }

        public tblDocumentItem FetchBody(long emailID)
        {
            EmailRepository rp = new EmailRepository();
            return rp.FetchBody(emailID);
        }

        public List<tblDocumentItem> FetchAttachments(long emailID)
        {
            EmailRepository rp = new EmailRepository();
            return rp.FetchAttachments(emailID);
        }

       
        //Fetch all documents related to given email
        public List<tblDocumentItem> FetchAllDocs(long emailID)
        {
            EmailRepository rp = new EmailRepository();
            return rp.FetchAllDocs(emailID);
        }

        public DateTime? GetLastExtractedTime(string profile)
        {
            DateTime? lastTime = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                tblOutlookEmail rec = context.tblOutlookEmails.Where(x => x.ExtractedProfile == profile).OrderByDescending(x => x.ReceivedTime).FirstOrDefault();
                if(rec != null)
                    lastTime = rec.ReceivedTime;
            }
            return lastTime;
        }
    }
}