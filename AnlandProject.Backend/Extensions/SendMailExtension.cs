using AnlandProject.Service.BusinessModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace AnlandProject.Backend.Extensions
{
    public static class SendMailExtension
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static void SendMailBySMTP(this SMTPSetupModel smtpData, string toMailAdd, string mailContent)
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
                    From = new MailAddress(smtpData.Recipient)
                };
                mailMessage.To.Add(toMailAdd);
                mailMessage.Subject = smtpData.Subject;
                mailMessage.Body = htmlBody;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static void SendMailBySMTPInternal(this SMTPSetupModel smtpData, string toMailAdd, string mailContent)
        {
            try
            {
                string htmlBody = $"<html><body><p><span class='infoText' style='white-space: pre-line;'><br /><div>管理員已為您重設密碼</div><div>新密碼為：{mailContent}</div><div>請利用新密碼登入進行設定</div></span></p></body></html>";

                SmtpClient client = new SmtpClient(smtpData.SMTP)
                {
                    //If you need to authenticate
                    Credentials = new NetworkCredential(smtpData.SMTPUserName, smtpData.SMTPPassword)
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpData.Recipient)
                };
                mailMessage.To.Add(toMailAdd);
                mailMessage.Subject = "密碼重設通知信";
                mailMessage.Body = htmlBody;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static void SendMailBySMTPPWDExpiredWarning(this SMTPSetupModel smtpData, string mailTo, string account, DateTime changeDate)
        {
            try
            {
                string htmlBody = $"<html><body><p><span class='infoText' style='white-space: pre-line;'><br />" +
                    $"<div>{account} 您好!</div><br /><br />" +
                    $"<div>您目前的密碼將於 {changeDate:yyyy/MM/dd} 到期</div><br />" +
                    $"<div>請盡速至個人設定進行密碼修改!!</div>" +
                    $"</span></p></body></html>";

                SmtpClient client = new SmtpClient(smtpData.SMTP)
                {
                    //If you need to authenticate
                    Credentials = new NetworkCredential(smtpData.SMTPUserName, smtpData.SMTPPassword)
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpData.Recipient)
                };
                mailMessage.To.Add(mailTo);
                mailMessage.Subject = "密碼即將到期通知";
                mailMessage.Body = htmlBody;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static void SendMailByWeb(this SMTPSetupModel smtpData, string toMailAdd, string mailContent)
        {
            try
            {
                string htmlBody = "<html><body><p><span class='infoText' style='white-space: pre-line;'>" + HttpUtility.HtmlDecode(mailContent) + "</span></p></body></html>";

                SmtpClient smtp = new SmtpClient
                {
                    Host = "aspmx.l.google.com",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("", "")
                };

                //using (var message = new MailMessage(smtpData.Recipient, toMailAdd))
                using (var message = new MailMessage(smtpData.Recipient, ""))
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