using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;

namespace DirectoryMonitor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController("DirFilesMonitor", System.Environment.MachineName);
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                     btnStop.Enabled = false;
                    btnStart.Enabled = true;
                }
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(Settings.LoadSettings("Config.xml")))
            {
                 txtExtensions.Text = string.Join(",",Settings.Extentions);
            foreach (var dir in Settings.Directories)
            {
                directoryListBox.Items.Add(dir);
            }
            txtCheckPeriod.Value = Settings.CheckPeriod;
                txtFileAge.Value = Settings.FileAge;
                txtServer.Text = Settings.Server;
            txtPort.Text = Settings.Port.ToString();
            chkSSl.Checked = Settings.SSL;
            txtSender.Text = Settings.Sender;
            txtPassword.Text = Settings.Password;
            txtTo.Text = Settings.To;
            txtCC.Text = Settings.CC;
            txtBCC.Text = Settings.BCC;
            txtSubject.Text = Settings.Subject;
                btnApply.Enabled = false;
            }
           
        }
        
        private void btnApply_Click(object sender, EventArgs e)
        {
            //stop the service
            try
            {
                if (string.IsNullOrEmpty(txtExtensions.Text))
                {
                     MessageBox.Show("Please enter one file extension at least.","",MessageBoxButtons.OK);
                    return;
                }
                if (string.IsNullOrEmpty(txtCheckPeriod.Text))
                {
                    MessageBox.Show("Please enter check period.", "", MessageBoxButtons.OK);
                    return;
                }
                if (string.IsNullOrEmpty(txtFileAge.Text))
                {
                    MessageBox.Show("Please enter file age.", "", MessageBoxButtons.OK);
                    return;
                }
                if (string.IsNullOrEmpty(txtSubject.Text))
                {
                  
                    MessageBox.Show("Please enter email subject.", "", MessageBoxButtons.OK);
                    return;
                }
                if (string.IsNullOrEmpty(txtServer.Text))
                {
                      MessageBox.Show("Please enter server address.", "", MessageBoxButtons.OK);
                    return;
                }

                if (string.IsNullOrEmpty(txtSender.Text)) { 
                    MessageBox.Show("Please enter sender address.", "", MessageBoxButtons.OK);
                return;
            }
                if (string.IsNullOrEmpty(txtPassword.Text)) { 
                    MessageBox.Show("Please enter sender password.", "", MessageBoxButtons.OK);
                return;
            }
                if (string.IsNullOrEmpty(txtTo.Text)) { 
                    MessageBox.Show("Please enter addresses to send email to.", "", MessageBoxButtons.OK);
                return;
            }
                var root = new XElement("Settings");
                var extentions = new XElement("Extensions", txtExtensions.Text);
                root.Add(extentions);

                if (directoryListBox.Items.Count == 0) { 
                    MessageBox.Show("Please select one directory at least.", "", MessageBoxButtons.OK);
                    return;
                }
                var directoriesElement = new XElement("Directories",
                    directoryListBox.Items.OfType<string>().Select(item => new XElement("Directory", item)));
                root.Add(directoriesElement);
                root.Add(new XElement("CheckPeriod", txtCheckPeriod.Value));
                root.Add(new XElement("FileAge", txtFileAge.Value));
                root.Add(new XElement("Server", txtServer.Text));
                root.Add(new XElement("Password",StringCipher.Encrypt(txtPassword.Text,"1q2w3e4r5t6y7u8i9o0p") ));
                root.Add(new XElement("Port", txtPort.Text));
                root.Add(new XElement("SSL", chkSSl.Checked ? 1:0));
                root.Add(new XElement("Sender", txtSender.Text));
                root.Add(new XElement("To", txtTo.Text));
                root.Add(new XElement("CC", txtCC.Text));
                root.Add(new XElement("BCC", txtBCC.Text));
                root.Add(new XElement("EmailSubject", txtSubject.Text));
                File.WriteAllText("Config.xml", root.ToString());
                WriteConfigPathToRegistery(Directory.GetCurrentDirectory() + "\\Config.xml");
                btnApply.Enabled = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void WriteConfigPathToRegistery(string configPath)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
                if (key.GetSubKeyNames().Contains("DirectoryMonitor"))
                return;
                key.CreateSubKey("DirectoryMonitor");
                key = key.OpenSubKey("DirectoryMonitor", true);
                key.SetValue("ConfigPath", configPath,RegistryValueKind.String);
           
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
            btnStop.Enabled = true;
            btnStart.Enabled = false;
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtExtensions_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void directoyTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtCheckPeriod_ValueChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtSender_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
        private void txtFileAge_ValueChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtCC_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkSSl_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
        private void txtBCC_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
btnApply.Enabled = true;
        }
        private void btnAddDir_Click(object sender, EventArgs e)
        {
            directoryListBox.Items.Add(txtDirectory.Text);
            txtDirectory.Text = string.Empty;
            btnApply.Enabled = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var result = dirBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                directoryListBox.Items.Add(dirBrowser.SelectedPath);
                btnApply.Enabled = true;
            }
        }

        private void btnDeleteDir_Click(object sender, EventArgs e)
        {
            directoryListBox.Items.RemoveAt(directoryListBox.SelectedIndex);
        }

        private void txtDirectory_TextChanged(object sender, EventArgs e)
        {
            btnAddDir.Enabled = !string.IsNullOrEmpty(txtDirectory.Text);
        }

        private void directoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteDir.Enabled = directoryListBox.SelectedItems.Count > 0;
        }
     

        private void Stop()
        {
            ServiceController service = new ServiceController("DirFilesMonitor", System.Environment.MachineName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(180000);
                if (service.Status == ServiceControllerStatus.Running&&service.CanStop)
                {
                     service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                   
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private int _times=0;
        private void Start()
        {
            ServiceController service = new ServiceController("DirFilesMonitor",System.Environment.MachineName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(180000);
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start(new string[] {Directory.GetCurrentDirectory() + "\\Config.xml"});
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    if (_times == 0)
                    {
                        _times++;
                        Start();
                    }
                        
                }
                   
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        
    }
}
