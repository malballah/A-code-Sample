namespace MonitorDirectory
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.directoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.directoryMonitorDataSet = new MonitorDirectory.DirectoryMonitorDataSet();
            this.label2 = new System.Windows.Forms.Label();
            this.tbExt = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.directoryMonitorDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.lbExt = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnDelExt = new System.Windows.Forms.Button();
            this.tbCheck = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.directoriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.directoryMonitorDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.directoryMonitorDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(551, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "Choose Folders";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Directories To Monitor:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(145, 191);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(400, 459);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.Enter += new System.EventHandler(this.listBox1_Enter);
            // 
            // directoriesBindingSource
            // 
            this.directoriesBindingSource.DataMember = "Directories";
            this.directoriesBindingSource.DataSource = this.directoryMonitorDataSet;
            this.directoriesBindingSource.CurrentChanged += new System.EventHandler(this.directoriesBindingSource_CurrentChanged);
            // 
            // directoryMonitorDataSet
            // 
            this.directoryMonitorDataSet.DataSetName = "DirectoryMonitorDataSet";
            this.directoryMonitorDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "File Extentions To Monitor For:";
            // 
            // tbExt
            // 
            this.tbExt.Location = new System.Drawing.Point(171, 11);
            this.tbExt.Name = "tbExt";
            this.tbExt.Size = new System.Drawing.Size(373, 20);
            this.tbExt.TabIndex = 5;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "\\\\nas1\\edi\\";
            // 
            // directoryMonitorDataSetBindingSource
            // 
            this.directoryMonitorDataSetBindingSource.DataSource = this.directoryMonitorDataSet;
            this.directoryMonitorDataSetBindingSource.Position = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(553, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 24);
            this.button2.TabIndex = 6;
            this.button2.Text = "Delete Item";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbExt
            // 
            this.lbExt.FormattingEnabled = true;
            this.lbExt.Location = new System.Drawing.Point(151, 50);
            this.lbExt.Name = "lbExt";
            this.lbExt.Size = new System.Drawing.Size(392, 95);
            this.lbExt.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(561, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 25);
            this.button3.TabIndex = 8;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDelExt
            // 
            this.btnDelExt.Location = new System.Drawing.Point(561, 50);
            this.btnDelExt.Name = "btnDelExt";
            this.btnDelExt.Size = new System.Drawing.Size(81, 25);
            this.btnDelExt.TabIndex = 9;
            this.btnDelExt.Text = "Delete";
            this.btnDelExt.UseVisualStyleBackColor = true;
            this.btnDelExt.Click += new System.EventHandler(this.btnDelExt_Click);
            // 
            // tbCheck
            // 
            this.tbCheck.Location = new System.Drawing.Point(24, 50);
            this.tbCheck.Name = "tbCheck";
            this.tbCheck.Size = new System.Drawing.Size(103, 94);
            this.tbCheck.TabIndex = 10;
            this.tbCheck.Text = "Check";
            this.tbCheck.UseVisualStyleBackColor = true;
            this.tbCheck.Click += new System.EventHandler(this.tbCheck_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 662);
            this.Controls.Add(this.tbCheck);
            this.Controls.Add(this.btnDelExt);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbExt);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbExt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Directory Monitoring";
            ((System.ComponentModel.ISupportInitialize)(this.directoriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.directoryMonitorDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.directoryMonitorDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbExt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.BindingSource directoriesBindingSource;
        private DirectoryMonitorDataSet directoryMonitorDataSet;
        private System.Windows.Forms.BindingSource directoryMonitorDataSetBindingSource;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lbExt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnDelExt;
        private System.Windows.Forms.Button tbCheck;
    }
}

