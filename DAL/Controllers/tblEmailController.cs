using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblEmailController : BaseController
    {
        public tblEmailController()
        {
            this.EntitySetName = "tblEmails";
        }

        public bool Save(tblEmail record)
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

        public long AddEmailToSend(string reciever, int templateID, long viewTypeID, long recID)
        {
            //if (!string.IsNullOrEmpty(reciever) && templateID > 0)
            //{
                //receiever is always null for template type email.
                tblEmail email = new tblEmail();
                email.ReceiverEmail = reciever;
                email.IsSent = false;
                email.RecordID = recID;
                email.TableID = Convert.ToInt32(viewTypeID);
                email.EmailTemplateID = templateID;
                email.SendingDate = DateTime.Now.Date;
                email.Priority = (int)EmailPriority.High;
                Save(email);
                return email.ID;
            //}
            //return 0;
        }

        public bool Delete(tblEmail record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblEmail Find(long id)
        {
            tblEmail rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblEmails.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblEmail> FetchAll()
        {
            List<tblEmail> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblEmails.ToList();
            }

            return rec;
        }

        public List<tblEmail> FetchView(int currentPage,ref long totalRecords)
        {
            List<tblEmail> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                totalRecords = context.tblEmails.ToList().Count;
                //ListSource.Skip(AppConstants.RecordPerPage * (currentPage - 1)).Take(AppConstants.RecordPerPage);
                var query = from e in context.tblEmails
                            join cl in context.Clients on e.RecordID equals cl.ID into tblCl
                            from subCr in tblCl.DefaultIfEmpty()
                            
                            select new
                            {
                                ID = e.ID,
                                EmailTemplateID = e.EmailTemplateID,
                                RecordCaption = subCr.Client_Name,
                                ReceiverEmail = e.ReceiverEmail,
                                IsSent = e.IsSent,
                                SendingDate = e.SendingDate,
                                DateSent = e.DateSent,
                            };

                rec = query.ToList().Skip(AppConstants.RecordPerPage * (currentPage - 1)).Take(AppConstants.RecordPerPage).Select(t => new tblEmail
                {
                    ID = t.ID,
                    EmailTemplateID = t.EmailTemplateID,
                    RecordCaption = t.RecordCaption,
                    ReceiverEmail = t.ReceiverEmail,
                    IsSent = t.IsSent,
                    SendingDate = t.SendingDate,
                    DateSent = t.DateSent,

                }).ToList();
            }
            return rec;
        }

        public List<tblEmail> GetEmailsToProcess()
        {
            List<tblEmail> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                var query = from t in context.tblEmails where t.IsSent != true
                            orderby t.SendingDate select t;

                rec = query.ToList();
            }
            return rec;
        
        }
    }
}
