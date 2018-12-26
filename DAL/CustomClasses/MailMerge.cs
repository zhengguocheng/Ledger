using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class MailMerge
    {
        tblEmailController emailCntr = new tblEmailController();
        EmailTemplateController TemplCntr = new EmailTemplateController();
        tblViewController vwCntr = new tblViewController();
        Reminder reminder = new Reminder();

        public bool SaveEmails(ArrayList lstObj, tblViewController.ViewTypes vwType, tblEmailTemplate template)
        {
            tblView objVw = vwCntr.Find(vwType);

            if (vwType == tblViewController.ViewTypes.Client)
            {
                foreach (var item in lstObj)
                {
                    Client cl = (Client)item;

                    string reciever = string.Empty;

                    if (!string.IsNullOrWhiteSpace(cl.EmailAddress1) && !string.IsNullOrWhiteSpace(cl.EmailAddress2))
                        reciever = cl.EmailAddress1 + "," + cl.EmailAddress2;
                    else if (!string.IsNullOrWhiteSpace(cl.EmailAddress1))
                        reciever = cl.EmailAddress1;
                    else if (!string.IsNullOrWhiteSpace(cl.EmailAddress2))
                        reciever = cl.EmailAddress2;

                    if (!string.IsNullOrWhiteSpace(reciever))
                    {
                        long emailID = emailCntr.AddEmailToSend(reciever, template.ID, objVw.ID, cl.ID);
                        ExtractAndSaveContent(emailID);
                    }
                }
            }
            return true;
        }


        public void StartProcess()
        {
            List<tblEmail> emailList = emailCntr.GetEmailsToProcess();

            List<tblEmailTemplate> tempList;

            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                tempList = context.tblEmailTemplates.ToList();

                foreach (tblEmail email in emailList)
                {
                    tblEmailTemplate template = tempList.FirstOrDefault(x => x.ID == email.EmailTemplateID);
                    if (template != null)
                    {
                        string subject = template.Subject;
                        string body = template.Body;
                        string viewName = template.tblView.ViewName;

                        SqlCommand cmd = new SqlCommand(@"select * from dbo.VwClientTemplate where ID = @clientID;");
                        cmd.Parameters.AddWithValue("clientID", email.RecordID);

                        DataSet ds = ContextCreater.ExecuteDataSet(cmd);
                        if (!ds.IsNullOrEmpty())
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            List<tblToken> tokenLst = template.tblView.tblTokens.ToList<tblToken>();
                            foreach (tblToken token in tokenLst)
                            {
                                subject = subject.Replace(token.TokenName, dr[token.TokenColumn].ToString());
                                body = body.Replace(token.TokenName, dr[token.TokenColumn].ToString());
                            }

                            //reminder.SendEmail(email.ReceiverEmail, email.Subject, email.Body, email);
                        }
                    }
                }
            }
        }

        public bool ExtractAndSaveContent(long emailID)
        {
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                tblEmail email = context.tblEmails.FirstOrDefault(x => x.ID == emailID);

                if (!email.EmailTemplateID.HasValue)
                    return false;
    
                tblEmailTemplate template = context.tblEmailTemplates.FirstOrDefault(x => x.ID == email.EmailTemplateID);
                    
                if (template != null)
                {
                    string subject = template.Subject;
                    string body = template.Body;
                    string viewName = template.tblView.ViewName;

                    SqlCommand cmd = new SqlCommand(@"select * from dbo.VwClientTemplate where ID = @clientID;");
                    cmd.Parameters.AddWithValue("clientID", email.RecordID);

                    DataSet ds = ContextCreater.ExecuteDataSet(cmd);

                    if (!ds.IsNullOrEmpty())
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        List<tblToken> tokenLst = template.tblView.tblTokens.ToList<tblToken>();
                        foreach (tblToken token in tokenLst)
                        {
                            subject = subject.Replace(token.TokenName, dr[token.TokenColumn].ToString());
                            body = body.Replace(token.TokenName, dr[token.TokenColumn].ToString());
                        }
                    }

                    email.Subject = subject;
                    email.Body = body;

                    context.SaveChanges();
                    return true;
                }
            }

            return false;

        }
    }

}
