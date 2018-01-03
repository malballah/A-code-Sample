using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Truck.Services
{
    public class MailService:IMailService
    {
        private static string MailSender => ConfigurationManager.AppSettings["MAILSENDER"];

        private static string MailSenderPw => ConfigurationManager.AppSettings["MAILSENDERPW"];

        private static string MailServer => ConfigurationManager.AppSettings["MAILSERVER"];

        private static string MailPort => ConfigurationManager.AppSettings["MAILPORT"];
        private static string MailBcc => ConfigurationManager.AppSettings["MAILBCC"];
        private static string MailAdmins => ConfigurationManager.AppSettings["MAILADMINS"];
        private static string EnableSSl => ConfigurationManager.AppSettings["ENABLESSL"];

        private static string MailFrom => string.IsNullOrEmpty(ConfigurationManager.AppSettings["MAILFROM"]) ? ConfigurationManager.AppSettings["MAILSENDER"] : ConfigurationManager.AppSettings["MAILFROM"];

        public static bool SendMail(string to, string subject, string body, string cc = null, string bcc = null, bool isBodyHtml = true, string[] attachments = null, bool async = false)
        {
            try
            {
                var client = new SmtpClient
                {
                    Host = MailServer,
                    Port = int.Parse(MailPort),
                    Credentials = new NetworkCredential(MailSender, MailSenderPw),
                    EnableSsl = EnableSSl=="1"
                };
                var message = new MailMessage
                {
                    Body = body,
                    Subject = subject,
                    From = new MailAddress(MailFrom, MailFrom),
                    IsBodyHtml = isBodyHtml
                };
                message.To.Add(to);
                if (cc != null)
                    message.CC.Add(cc);
                if (bcc != null)
                    message.Bcc.Add(bcc);
                //append attachments
                if(attachments!=null)
                    foreach (var file in attachments)
                    {
                        message.Attachments.Add(new Attachment(file));
                    }
                if (async)
                    client.SendAsync(message, null);
                else
                    client.Send(message);
                return true;
            }
            catch (Exception exp)
            {
                SendBug("Email","SendEmail",exp);
                //send to db and mail
                return false;
            }
        }

        bool IMailService.SendBug(string controller, string action, Exception exp)
        {
            return SendBug(controller, action, exp);
        }

        public static bool SendBug(string controller,string action,Exception exp)
        {
            try
            {
                var error = string.Empty;
                while (exp != null)
                {
                    error += "<br/>" + exp.Message;
                    exp = exp.InnerException;
                }
                var client = new SmtpClient
                {
                    Host = MailServer,
                    Port = int.Parse(MailPort),
                    Credentials = new NetworkCredential(MailSender, MailSenderPw),
                    EnableSsl = EnableSSl == "1"
                };
                var message = new MailMessage
                {
                    Body = "Controller : "+controller+"<br/>Action : "+action+"+<br/>Exception Message : " + error,
                    Subject = "Bug in Truck System",
                    From = new MailAddress(MailSender, "Truck System"),
                    IsBodyHtml = true
                };
                message.To.Add("malballah@gmail.com");
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
       bool IMailService.SendMail(string to, string subject, string body, string cc, string bcc, bool isBodyHtml,
            string[] attachments, bool async)
        {
            return SendMail(to, subject, body, cc, bcc, isBodyHtml, attachments, async);
        }
    }
}