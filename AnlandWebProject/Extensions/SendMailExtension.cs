using AnlandProject.Service.BusinessModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace AnlandProject.Web.Extensions
{
    public static class SendMailExtension
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        
        public static void SendMailBySMTP(this SMTPSetupModel smtpData, string senderMailAdd, string subject, string mailContent)
        {
            try
            {
                string htmlBody = "<html><body><p><span class='infoText' style='white-space: pre-line;'>" + HttpUtility.HtmlDecode(mailContent) + "</span></p></body></html>";

                SmtpClient client = new SmtpClient(smtpData.SMTP)
                {
                    //If you need to authenticate
                    Credentials = new NetworkCredential(smtpData.SMTPUserName, smtpData.SMTPPassword)
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderMailAdd)
                };
                mailMessage.To.Add(smtpData.Recipient);
                mailMessage.CC.Add(senderMailAdd);
                mailMessage.Subject = string.Format("{0}{1}", smtpData.Subject, subject);
                mailMessage.Body = htmlBody;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                logger.Error("SendMailBySMTP Error:" + ex);
            }
        }

        public static void SendMailByWeb(this SMTPSetupModel smtpData, string toMailAdd, string mailContent)
        {
            try
            {
                string htmlBody = "<html><body><p><span class='infoText' style='white-space: pre-line;'>" + HttpUtility.HtmlDecode(mailContent) + "</span></p></body></html>";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "aspmx.l.google.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("", "");

                //using (var message = new MailMessage(smtpData.Recipient, toMailAdd))
                using (var message = new MailMessage("", ""))
                {
                    message.Subject = smtpData.Subject;
                    message.Body = htmlBody;
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}