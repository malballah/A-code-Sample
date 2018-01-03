using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;

namespace DirectoryMonitorService
{
    public partial class DirectoryMonitorService : ServiceBase
    {
        private Timer _timer;
        public DirectoryMonitorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string configPath = ReadConfigPathFromRegistery();
                if(string.IsNullOrEmpty(configPath))
                    throw new Exception("Configuration file not exists");
            
            var msg = Settings.LoadSettings(configPath);
            if (string.IsNullOrEmpty(msg))
            {
                _timer = new Timer {Interval = (double)Settings.CheckPeriod * 60 * 1000};
                _timer.Elapsed += _timer_Elapsed;
                _timer.Enabled = true;
            }
           
        }

        private string ReadConfigPathFromRegistery()
        {
            var key1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            var key2 = key1.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadSubTree);
           var key3 = key2.OpenSubKey("DirectoryMonitor",RegistryKeyPermissionCheck.ReadSubTree);
            if (key3 == null)
            {
                key1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                key2 = key1.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadSubTree);
                key3 = key2.OpenSubKey("DirectoryMonitor", RegistryKeyPermissionCheck.ReadSubTree);
            }
            var value = key3.GetValue("ConfigPath","");
            return value.ToString();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //monitor directory
                var oldFiles = new List<FileInfo>();
                foreach (var dir in Settings.Directories)
                {
                    string[] files;
                    try
                    {
                        files = Directory.GetFiles(dir);
                    }
                    catch
                    {
                        continue;
                    }
                    foreach (var file in files)
                    {
                        try
                        {
                            var fileInfo = new FileInfo(file);
                            if (!string.IsNullOrEmpty(fileInfo.Extension) &&
                                Settings.Extentions.Contains(fileInfo.Extension.ToLower().Substring(1)) &&
                                fileInfo.LastWriteTime < DateTime.Now.AddMinutes(-1*(double)Settings.FileAge))
                                oldFiles.Add(fileInfo);
                        }
                        catch
                        {
                           
                        }
                    }
                }
                //send mail with old files
                if (oldFiles.Any())
                {
                    Settings.Message =
                        "Old files :<br/><br/><table style='border: 1px solid;text-align:center;width:100%'><tr><th style='border: 1px solid;text-align:center'>File</th><th colspan='6' style='border: 1px solid;text-align:center'>Age</th></tr>";
                    Settings.Message +=
                        "<tr><th></th><th style='border: 1px solid;text-align:center'>Years</th><th style='border: 1px solid;text-align:center'>Months</th><th style='border: 1px solid;text-align:center'>Days</th><th style='border: 1px solid;text-align:center'>Hours</th><th style='border: 1px solid;text-align:center'>Minutes</th><th style='border: 1px solid;text-align:center'>Seconds</th></tr>";
                    foreach (var item in oldFiles)
                    {
                        var dateDiff = new DateDiff().Count(item.LastWriteTime, DateTime.Now);
                        Settings.Message += "<tr>";
                        Settings.Message += "<td style='border: 1px solid;text-align:center'>" + item.FullName +
                                            "</td><td style='border: 1px solid;text-align:center'>" + dateDiff.Years +
                                            "</td><td style='border: 1px solid;text-align:center'>" + dateDiff.Months +
                                            "</td><td style='border: 1px solid;text-align:center'>" + dateDiff.Days +
                                            "</td><td style='border: 1px solid;text-align:center'>" + dateDiff.Hours +
                                            "</td><td style='border: 1px solid;text-align:center'>" + dateDiff.Minutes +
                                            "</td><td style='border: 1px solid;text-align:center'>" + dateDiff.Seconds +
                                            "</td>";
                        Settings.Message += "</tr>";
                    }
                    Settings.Message += "</table>";
                    MailService.SendMail();
                }
            }
            catch (Exception exp)
            {
                while (exp.InnerException != null)
                    exp = exp.InnerException;
                MailService.SendError(exp.Message);
            }

        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
        }
    }
}
