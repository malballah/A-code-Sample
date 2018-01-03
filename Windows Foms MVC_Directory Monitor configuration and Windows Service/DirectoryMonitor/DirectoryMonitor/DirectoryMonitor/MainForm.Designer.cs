namespace DirectoryMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.directoryImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFileAge = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteDir = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnAddDir = new System.Windows.Forms.Button();
            this.directoryListBox = new System.Windows.Forms.ListBox();
            this.txtCheckPeriod = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExtensions = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSSl = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBCC = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCC = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.dirBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckPeriod)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryImageList
            // 
            this.directoryImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("directoryImageList.ImageStream")));
            this.directoryImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.directoryImageList.Images.SetKeyName(0, "1346238561_folder_classic.png");
            this.directoryImageList.Images.SetKeyName(1, "1346238604_folder_classic_opened.png");
            this.directoryImageList.Images.SetKeyName(2, "1346228331_drive.png");
            this.directoryImageList.Images.SetKeyName(3, "1346228337_drive_cd.png");
            this.directoryImageList.Images.SetKeyName(4, "1346228356_drive_cd_empty.png");
            this.directoryImageList.Images.SetKeyName(5, "1346228364_drive_disk.png");
            this.directoryImageList.Images.SetKeyName(6, "1346228591_drive_network.png");
            this.directoryImageList.Images.SetKeyName(7, "1346228618_drive_link.png");
            this.directoryImageList.Images.SetKeyName(8, "1346228623_drive_error.png");
            this.directoryImageList.Images.SetKeyName(9, "1346228633_drive_go.png");
            this.directoryImageList.Images.SetKeyName(10, "1346228636_drive_delete.png");
            this.directoryImageList.Images.SetKeyName(11, "1346228639_drive_burn.png");
            this.directoryImageList.Images.SetKeyName(12, "1346238642_folder_classic_locked.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFileAge);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnDeleteDir);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtDirectory);
            this.groupBox1.Controls.Add(this.btnAddDir);
            this.groupBox1.Controls.Add(this.directoryListBox);
            this.groupBox1.Controls.Add(this.txtCheckPeriod);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtExtensions);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 293);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Monitor Settings";
            // 
            // txtFileAge
            // 
            this.txtFileAge.Location = new System.Drawing.Point(251, 262);
            this.txtFileAge.Maximum = new decimal(new int[] {
            -559939584,
            902409669,
            54,
            0});
            this.txtFileAge.Name = "txtFileAge";
            this.txtFileAge.Size = new System.Drawing.Size(105, 20);
            this.txtFileAge.TabIndex = 13;
            this.txtFileAge.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtFileAge.ValueChanged += new System.EventHandler(this.txtFileAge_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "File Age (Minues)";
            // 
            // btnDeleteDir
            // 
            this.btnDeleteDir.Enabled = false;
            this.btnDeleteDir.Location = new System.Drawing.Point(367, 139);
            this.btnDeleteDir.Name = "btnDeleteDir";
            this.btnDeleteDir.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDir.TabIndex = 6;
            this.btnDeleteDir.Text = "Delete";
            this.btnDeleteDir.UseVisualStyleBackColor = true;
            this.btnDeleteDir.Click += new System.EventHandler(this.btnDeleteDir_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(367, 111);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtDirectory
            // 
            this.txtDirectory.Location = new System.Drawing.Point(22, 84);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(334, 20);
            this.txtDirectory.TabIndex = 2;
            this.txtDirectory.TextChanged += new System.EventHandler(this.txtDirectory_TextChanged);
            // 
            // btnAddDir
            // 
            this.btnAddDir.Enabled = false;
            this.btnAddDir.Location = new System.Drawing.Point(367, 83);
            this.btnAddDir.Name = "btnAddDir";
            this.btnAddDir.Size = new System.Drawing.Size(75, 23);
            this.btnAddDir.TabIndex = 3;
            this.btnAddDir.Text = "Add Directory";
            this.btnAddDir.UseVisualStyleBackColor = true;
            this.btnAddDir.Click += new System.EventHandler(this.btnAddDir_Click);
            // 
            // directoryListBox
            // 
            this.directoryListBox.FormattingEnabled = true;
            this.directoryListBox.Location = new System.Drawing.Point(22, 112);
            this.directoryListBox.Name = "directoryListBox";
            this.directoryListBox.Size = new System.Drawing.Size(334, 121);
            this.directoryListBox.TabIndex = 5;
            this.directoryListBox.SelectedIndexChanged += new System.EventHandler(this.directoryListBox_SelectedIndexChanged);
            // 
            // txtCheckPeriod
            // 
            this.txtCheckPeriod.Location = new System.Drawing.Point(22, 262);
            this.txtCheckPeriod.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.txtCheckPeriod.Name = "txtCheckPeriod";
            this.txtCheckPeriod.Size = new System.Drawing.Size(105, 20);
            this.txtCheckPeriod.TabIndex = 7;
            this.txtCheckPeriod.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtCheckPeriod.ValueChanged += new System.EventHandler(this.txtCheckPeriod_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Check Period (Minues)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "File Extensions (Separated By Comma)";
            // 
            // txtExtensions
            // 
            this.txtExtensions.Location = new System.Drawing.Point(22, 48);
            this.txtExtensions.Name = "txtExtensions";
            this.txtExtensions.Size = new System.Drawing.Size(420, 20);
            this.txtExtensions.TabIndex = 1;
            this.txtExtensions.TextChanged += new System.EventHandler(this.txtExtensions_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkSSl);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSubject);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtBCC);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtCC);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtTo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtSender);
            this.groupBox2.Controls.Add(this.txtPort);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtServer);
            this.groupBox2.Location = new System.Drawing.Point(12, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 240);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server Settings";
            // 
            // chkSSl
            // 
            this.chkSSl.AutoSize = true;
            this.chkSSl.Location = new System.Drawing.Point(399, 34);
            this.chkSSl.Name = "chkSSl";
            this.chkSSl.Size = new System.Drawing.Size(43, 17);
            this.chkSSl.TabIndex = 28;
            this.chkSSl.Text = "SSL";
            this.chkSSl.UseVisualStyleBackColor = true;
            this.chkSSl.CheckedChanged += new System.EventHandler(this.chkSSl_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Email Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(172, 206);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(270, 20);
            this.txtSubject.TabIndex = 15;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(353, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(89, 20);
            this.txtPassword.TabIndex = 11;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Password";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 174);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "BCC(Separated By Comma)";
            // 
            // txtBCC
            // 
            this.txtBCC.Location = new System.Drawing.Point(172, 171);
            this.txtBCC.Name = "txtBCC";
            this.txtBCC.Size = new System.Drawing.Size(270, 20);
            this.txtBCC.TabIndex = 14;
            this.txtBCC.TextChanged += new System.EventHandler(this.txtBCC_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "CC  (Separated By Comma)";
            // 
            // txtCC
            // 
            this.txtCC.Location = new System.Drawing.Point(172, 136);
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(270, 20);
            this.txtCC.TabIndex = 13;
            this.txtCC.TextChanged += new System.EventHandler(this.txtCC_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "To  (Separated By Comma)";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(172, 102);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(270, 20);
            this.txtTo.TabIndex = 12;
            this.txtTo.TextChanged += new System.EventHandler(this.txtTo_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Sender";
            // 
            // txtSender
            // 
            this.txtSender.Location = new System.Drawing.Point(108, 67);
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(172, 20);
            this.txtSender.TabIndex = 10;
            this.txtSender.TextChanged += new System.EventHandler(this.txtSender_TextChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(353, 32);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(36, 20);
            this.txtPort.TabIndex = 9;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Server Address";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(108, 32);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(172, 20);
            this.txtServer.TabIndex = 8;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(217, 567);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(308, 567);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 567);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(401, 567);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(488, 602);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Directory Monitor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckPeriod)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList directoryImageList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown txtCheckPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExtensions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCC;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBCC;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox directoryListBox;
        private System.Windows.Forms.Button btnAddDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.FolderBrowserDialog dirBrowser;
        private System.Windows.Forms.Button btnDeleteDir;
        private System.Windows.Forms.CheckBox chkSSl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtFileAge;
    }
}

