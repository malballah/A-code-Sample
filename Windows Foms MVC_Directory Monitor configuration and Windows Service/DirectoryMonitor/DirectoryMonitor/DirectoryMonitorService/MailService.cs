using System;
using System.Net;
using System.Net.Mail;

namespace DirectoryMonitorService
{
    public class MailService
    {
        public static string SendMail()
        {
            try
            {
                var client = new SmtpClient
                {
                    Host = Settings.Server,
                    Port = Settings.Port,
                    Credentials = new NetworkCredential(Settings.Sender, Settings.Password),
                    EnableSsl = Settings.SSL
                };
                var message = new MailMessage
                {
                    Body = Settings.Message,
                    Subject = Settings.Subject,
                    From = new MailAddress(Settings.Sender, string.Empty),
                    IsBodyHtml = true
                };
                message.To.Add(Settings.To);
                if (!string.IsNullOrEmpty(Settings.CC))
                    message.CC.Add(Settings.CC);
                if (!string.IsNullOrEmpty(Settings.BCC))
                    message.Bcc.Add(Settings.BCC);

                client.Send(message);
                return string.Empty;
            }
            catch (Exception exp)
            {
                System.IO.File.WriteAllText("errors.txt", exp.Message);
                //send to db and mail
                return exp.Message;
            }
        }
        public static string SendError(string error)
        {
            try
            {
                var client = new SmtpClient
                {
                    Host = Settings.Server,
                    Port = Settings.Port,
                    Credentials = new NetworkCredential(Settings.Sender, Settings.Password),
                    EnableSsl = Settings.SSL
                };
                var message = new MailMessage
                {
                    Body = error,
                    Subject = "Directory Monitor Error",
                    From = new MailAddress(Settings.Sender, string.Empty),
                    IsBodyHtml = true
                };
                //message.To.Add(Settings.To);
                
                client.Send(message);
                return string.Empty;
            }
            catch (Exception exp)
            {
                System.IO.File.WriteAllText("errors.txt", exp.Message);
                //send to db and mail
                return exp.Message;
            }
        }
    }
}