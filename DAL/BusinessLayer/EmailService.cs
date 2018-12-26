using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using System.Net.Mail;
using System.Net;
using System.Threading;


namespace DAL
{
    //public class EmailErrorEventArgs : EventArgs
    //{
    //    public EmailStatus objEmailStatus { get; set; }
    //}

    public abstract class EmailService
    {
        tblEmailController cntroller = new tblEmailController();
        SMTPEmailer emailer = new SMTPEmailer();
        
        public event EventHandler EmailError;

        protected virtual void OnEmailError(EventArgs e)
        {
            if (EmailError != null)
                EmailError(this, e);
        }

        //public void SendEmail(tblTask task, bool isSync = true)
        //{
        //    if (AppConstants.SendEmails)
        //    {
        //        string subject = string.Empty, body = string.Empty, reciever = string.Empty;
        //        task.GetEmailContent(ref subject, ref body, ref reciever);
        //        if (!string.IsNullOrEmpty(reciever) && !string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
        //        {
        //            emailHelper.SendMail(AppConstants.DMSEmail, GetPass(), reciever, subject, body,defaultSMTP,isSync);
        //        }
        //    }
        //}

        //public void SendEmail(tblEmail task, bool isSync = true)
        //{
        //    if (AppConstants.SendEmails)
        //    {
        //        if (!string.IsNullOrEmpty(task.ReceiverEmail) && !string.IsNullOrEmpty(task.Body) && !string.IsNullOrEmpty(task.Subject))
        //        {
        //            emailHelper.SendMail(AppConstants.DMSEmail, GetPass(), task.ReceiverEmail, task.Subject, task.Body,defaultSMTP, isSync);
        //        }
        //    }
        //}

        public void AddEmailToSend(tblTask task)
        {
            string subject = string.Empty, body = string.Empty, reciever = string.Empty;
            task.GetEmailContent(ref subject, ref body, ref reciever);
            
            if (!string.IsNullOrEmpty(reciever) && !string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
            {
                tblEmail email = new tblEmail();
                email.Subject = subject;
                email.Body = body;
                email.ReceiverEmail = reciever;
                email.IsSent = false;
                email.RecordID = task.ID;
                email.TableID = (int)AppConstants.RecordType.Task;
                email.SendingDate = DateTime.Now; //task.EndDate.Value.AddDays(-1 * task.ReminderBeforeDays.Value);
                email.Priority = (int)EmailPriority.Medium;
                cntroller.Save(email);
            }
        }

        int sleepSec = 15 * 1000;
        public void SendPendingEmails()
        {
            if (!AppConstants.SendEmails) return;

            List<tblEmail> lst = cntroller.GetEmailsToProcess();
            foreach (tblEmail rec in lst)
            {
                EmailStatus st = emailer.Send(rec.Subject, rec.Body, rec.ReceiverEmail);
                if (st.IsSent)
                {
                    rec.IsSent = true;
                    rec.DateSent = DateTime.Now;
                    rec.SenderEmail = st.SenderSMTPSettings.SenderEmail;
                    cntroller.Save(rec);
                    Thread.Sleep(sleepSec);
                }
                else
                {
                    st.EmailID = rec.ID;
                   
                    
                    EmailError(st, EventArgs.Empty);
                }
            }
        }

        public bool SendEmail(string receiver, string subject, string body,tblEmail email = null)
        {
            EmailStatus st = emailer.Send(subject, body, receiver);
            if (email != null)
            {
                if(st.IsSent)
                {
                    email.IsSent = true;
                }
            }
            return st.IsSent;
        }

    }
}
