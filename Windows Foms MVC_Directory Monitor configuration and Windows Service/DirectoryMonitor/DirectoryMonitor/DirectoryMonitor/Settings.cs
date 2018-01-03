using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DirectoryMonitor
{
    public static class Settings
    {
        public static bool SSL { get; set; }
        public static List<string> Extentions { get; set; }
        public static List<string> Directories { get; set; }
        public static decimal CheckPeriod { get; set; }
        public static decimal FileAge { get; set; }
        public static string Server { get; set; }
        public static int Port { get; set; }
        public static string Sender { get; set; }
        public static string Password { get; set; }
        public static string From { get; set; }
        public static string To { get; set; }
        public static string CC { get; set; }
        public static string BCC { get; set; }
        public  static string Subject { get; set; }

        public static string Message
        {
            get;
            set;
        }

        public static string LoadSettings(string filePath)
        {
            try
            {
                var root = XElement.Load(filePath);
                Extentions =
                    root.Element("Extensions")
                        .Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                Directories = root.Element("Directories").Elements("Directory").Select(item => item.Value).ToList();
                CheckPeriod = decimal.Parse(root.Element("CheckPeriod").Value);
                FileAge = decimal.Parse(root.Element("FileAge").Value);
                Server = root.Element("Server").Value;
                Password = root.Element("Password").Value;
                Port = int.Parse(root.Element("Port").Value);
                SSL = root.Element("SSL").Value=="1";
                Sender = root.Element("Sender").Value;
                To = root.Element("To").Value;
                CC = root.Element("CC").Value;
                BCC = root.Element("BCC").Value;
                Subject = root.Element("EmailSubject").Value;
                return string.Empty;
            }
            catch(Exception exp)
            {
                return exp.Message;
            }

        }
    }
}
