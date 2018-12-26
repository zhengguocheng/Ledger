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
    public enum EmailPriority
    {
        Low = 1, Medium = 2, High = 3, 
    }

    public class EmailStatus
    {
        public bool IsSent { get; set; }
        public SMTPSettings SenderSMTPSettings { get; set; }
        public Exception Error { get; set; }
        public long EmailID { get; set; }
        
    }

    public class SMTPSettings
    {
        public SmtpClient smtpClient { get; set; }
        public SmtpType ServerType { get; set; }
        public string SenderEmail { get; set; }

        public SMTPSettings(string sender, string password, SmtpType type)
        {
            string host = "";

            if (type == SmtpType.Hotmail)
                host = "smtp.live.com";
            else if (type == SmtpType.Gmail)
                host = "smtp.gmail.com";

            NetworkCredential cred = new NetworkCredential(sender, AppConstants.GetPassword());

            smtpClient = new SmtpClient
            {
                Host = host,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = cred,
                Timeout = 9000
            };
            SenderEmail = sender;
            ServerType = type;

        }
    }

    public class SMTPEmailer
    {
        List<SMTPSettings> SmtpLst = new List<SMTPSettings>();

        public SMTPEmailer()
        {
            SmtpLst.Add(new SMTPSettings("documentmanagementsystem@hotmail.com", AppConstants.GetPassword(), SmtpType.Hotmail));
            SmtpLst.Add(new SMTPSettings("gdocumentmanagementsystem@gmail.com", AppConstants.GetPassword(), SmtpType.Gmail));
        }

        public EmailStatus Send(string subject, string body, string receiverEmail)
        {
            EmailStatus status = new EmailStatus();
            foreach (SMTPSettings setting in SmtpLst)
            {
               
                try
                {
                    MailMessage message = new MailMessage(setting.SenderEmail, receiverEmail, subject, body);
                    message.IsBodyHtml = true;

                    setting.smtpClient.Send(message);

                    status.IsSent = true;
                    status.SenderSMTPSettings = setting;
                    break;
                }
                catch (Exception ecp)
                {
                    status.IsSent = false;
                    status.Error = ecp;
                }
            }

            return status;
        }
    }
}
