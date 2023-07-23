namespace DGPDrive.Screens.FileStore
{
    partial class frmFileDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileDownload));
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbxFilePath = new System.Windows.Forms.TextBox();
            this.tbxFileSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxGlobalID = new System.Windows.Forms.TextBox();
            this.brwsResults = new System.Windows.Forms.WebBrowser();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ckbOpen = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckbxAutoClose = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(528, 60);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(130, 50);
            this.btnBrowse.TabIndex = 100;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbxFilePath
            // 
            this.tbxFilePath.BackColor = System.Drawing.Color.DarkGray;
            this.tbxFilePath.ForeColor = System.Drawing.Color.Black;
            this.tbxFilePath.Location = new System.Drawing.Point(28, 64);
            this.tbxFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxFilePath.Name = "tbxFilePath";
            this.tbxFilePath.Size = new System.Drawing.Size(464, 20);
            this.tbxFilePath.TabIndex = 99;
            // 
            // tbxFileSize
            // 
            this.tbxFileSize.BackColor = System.Drawing.Color.LightGray;
            this.tbxFileSize.ForeColor = System.Drawing.Color.Black;
            this.tbxFileSize.Location = new System.Drawing.Point(204, 150);
            this.tbxFileSize.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxFileSize.MaxLength = 30;
            this.tbxFileSize.Name = "tbxFileSize";
            this.tbxFileSize.ReadOnly = true;
            this.tbxFileSize.Size = new System.Drawing.Size(396, 20);
            this.tbxFileSize.TabIndex = 109;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(32, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 110;
            this.label5.Text = "File Size";
            // 
            // tbxFileName
            // 
            this.tbxFileName.BackColor = System.Drawing.Color.LightGray;
            this.tbxFileName.ForeColor = System.Drawing.Color.Black;
            this.tbxFileName.Location = new System.Drawing.Point(204, 96);
            this.tbxFileName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxFileName.MaxLength = 50;
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(396, 20);
            this.tbxFileName.TabIndex = 108;
            this.tbxFileName.TextChanged += new System.EventHandler(this.tbxFileName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(32, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 106;
            this.label4.Text = "File Name";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(28, 366);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 46);
            this.btnCancel.TabIndex = 103;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSize = true;
            this.btnDownload.BackColor = System.Drawing.Color.LightGray;
            this.btnDownload.Enabled = false;
            this.btnDownload.ForeColor = System.Drawing.Color.Black;
            this.btnDownload.Location = new System.Drawing.Point(528, 366);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(130, 46);
            this.btnDownload.TabIndex = 104;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(36, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "Global ID";
            // 
            // tbxGlobalID
            // 
            this.tbxGlobalID.BackColor = System.Drawing.Color.LightGray;
            this.tbxGlobalID.Enabled = false;
            this.tbxGlobalID.ForeColor = System.Drawing.Color.Black;
            this.tbxGlobalID.Location = new System.Drawing.Point(204, 42);
            this.tbxGlobalID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxGlobalID.MaxLength = 50;
            this.tbxGlobalID.Name = "tbxGlobalID";
            this.tbxGlobalID.ReadOnly = true;
            this.tbxGlobalID.Size = new System.Drawing.Size(396, 20);
            this.tbxGlobalID.TabIndex = 101;
            this.tbxGlobalID.TabStop = false;
            // 
            // brwsResults
            // 
            this.brwsResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brwsResults.Location = new System.Drawing.Point(0, 0);
            this.brwsResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.brwsResults.MinimumSize = new System.Drawing.Size(20, 20);
            this.brwsResults.Name = "brwsResults";
            this.brwsResults.Size = new System.Drawing.Size(628, 278);
            this.brwsResults.TabIndex = 116;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(28, 442);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(628, 18);
            this.progressBar1.TabIndex = 119;
            // 
            // ckbOpen
            // 
            this.ckbOpen.AutoSize = true;
            this.ckbOpen.Checked = true;
            this.ckbOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbOpen.ForeColor = System.Drawing.Color.LightGray;
            this.ckbOpen.Location = new System.Drawing.Point(348, 376);
            this.ckbOpen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbOpen.Name = "ckbOpen";
            this.ckbOpen.Size = new System.Drawing.Size(84, 27);
            this.ckbOpen.TabIndex = 120;
            this.ckbOpen.Text = "Open File";
            this.ckbOpen.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxFileSize);
            this.groupBox1.Controls.Add(this.tbxGlobalID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbxFileName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new System.Drawing.Point(28, 124);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(628, 212);
            this.groupBox1.TabIndex = 121;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit File Info (optional)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(24, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 122;
            this.label2.Text = "Select Download Folder";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.brwsResults);
            this.panel1.Location = new System.Drawing.Point(28, 488);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 278);
            this.panel1.TabIndex = 123;
            // 
            // ckbxAutoClose
            // 
            this.ckbxAutoClose.AutoSize = true;
            this.ckbxAutoClose.Checked = true;
            this.ckbxAutoClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbxAutoClose.ForeColor = System.Drawing.Color.LightGray;
            this.ckbxAutoClose.Location = new System.Drawing.Point(180, 376);
            this.ckbxAutoClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbxAutoClose.Name = "ckbxAutoClose";
            this.ckbxAutoClose.Size = new System.Drawing.Size(91, 27);
            this.ckbxAutoClose.TabIndex = 128;
            this.ckbxAutoClose.Text = "Close Form";
            this.ckbxAutoClose.UseVisualStyleBackColor = true;
            // 
            // frmFileDownload
            // 
            this.AcceptButton = this.btnDownload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(686, 796);
            this.Controls.Add(this.ckbxAutoClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ckbOpen);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbxFilePath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDownload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmFileDownload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Download";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbxFilePath;
        private System.Windows.Forms.TextBox tbxFileSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxGlobalID;
        private System.Windows.Forms.WebBrowser brwsResults;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox ckbOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckbxAutoClose;
    }
}