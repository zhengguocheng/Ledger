using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Utilities
{
    public enum SmtpType
    {
        Hotmail,Gmail
    }

    public class EmailHelper
    {
        SmtpClient hotmailClnt, gmailClnt;
        
        public EmailHelper()
        {
           
        }

        SmtpClient GetSMTPClient(SmtpType smtpType, string senderEmail,string senderPassword)
        {
            if (smtpType == SmtpType.Hotmail)
            {
                if (hotmailClnt == null)
                {
                    hotmailClnt = new SmtpClient
                       {
                           Host = "smtp.live.com",
                           Port = 587,
                           EnableSsl = true,
                           DeliveryMethod = SmtpDeliveryMethod.Network,
                           Credentials = new NetworkCredential(senderEmail, senderPassword),
                           Timeout = 9000
                       };

                    hotmailClnt.SendCompleted += new SendCompletedEventHandler(SMTP_SendCompleted);
                }
                return hotmailClnt;
            }
            else if (smtpType == SmtpType.Gmail)
            {
                if (gmailClnt == null)
                {
                    gmailClnt = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(senderEmail, senderPassword),
                        Timeout = 9000
                    };
                    gmailClnt.SendCompleted += new SendCompletedEventHandler(SMTP_SendCompleted);

                }
                return gmailClnt;
            }
            else
                return null;
        }

        

        public void SendMail(string senderEmail, string senderPassword, string recieverEmail, string subject, string body, SmtpType smtpType, bool isSync)
        {
            SmtpClient smtp = GetSMTPClient(smtpType,senderEmail,senderPassword);
            MailMessage message = new MailMessage(senderEmail, recieverEmail, subject, body);
            message.IsBodyHtml = true;
            
            if(isSync)
                smtp.Send(message);
            else
                smtp.SendAsync(message,new object());

        }

        void SMTP_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //if(e.Error == null)
        }
    }
}
