namespace DGPDrive.Screens.FileStore
{
    partial class frmFileUpload2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileUpload2));
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxGlobalID = new System.Windows.Forms.TextBox();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.tbxFileDescrip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.brwsResults = new System.Windows.Forms.WebBrowser();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbxFilePath = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSelectedFolder = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbxAutoClose = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(24, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 91;
            this.label4.Text = "File Name";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(32, 472);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 46);
            this.btnCancel.TabIndex = 86;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnUpload
            // 
            this.btnUpload.AutoSize = true;
            this.btnUpload.BackColor = System.Drawing.Color.LightGray;
            this.btnUpload.Enabled = false;
            this.btnUpload.ForeColor = System.Drawing.Color.Black;
            this.btnUpload.Location = new System.Drawing.Point(528, 472);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(120, 46);
            this.btnUpload.TabIndex = 88;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "Global ID";
            // 
            // tbxGlobalID
            // 
            this.tbxGlobalID.BackColor = System.Drawing.Color.LightGray;
            this.tbxGlobalID.Enabled = false;
            this.tbxGlobalID.ForeColor = System.Drawing.Color.Black;
            this.tbxGlobalID.Location = new System.Drawing.Point(196, 44);
            this.tbxGlobalID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxGlobalID.MaxLength = 50;
            this.tbxGlobalID.Name = "tbxGlobalID";
            this.tbxGlobalID.ReadOnly = true;
            this.tbxGlobalID.Size = new System.Drawing.Size(392, 20);
            this.tbxGlobalID.TabIndex = 81;
            this.tbxGlobalID.TabStop = false;
            // 
            // tbxFileName
            // 
            this.tbxFileName.BackColor = System.Drawing.Color.LightGray;
            this.tbxFileName.ForeColor = System.Drawing.Color.Black;
            this.tbxFileName.Location = new System.Drawing.Point(196, 100);
            this.tbxFileName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxFileName.MaxLength = 50;
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(392, 20);
            this.tbxFileName.TabIndex = 94;
            // 
            // tbxFileDescrip
            // 
            this.tbxFileDescrip.BackColor = System.Drawing.Color.LightGray;
            this.tbxFileDescrip.ForeColor = System.Drawing.Color.Black;
            this.tbxFileDescrip.Location = new System.Drawing.Point(196, 152);
            this.tbxFileDescrip.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxFileDescrip.MaxLength = 250;
            this.tbxFileDescrip.Multiline = true;
            this.tbxFileDescrip.Name = "tbxFileDescrip";
            this.tbxFileDescrip.Size = new System.Drawing.Size(392, 84);
            this.tbxFileDescrip.TabIndex = 95;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(24, 156);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 96;
            this.label5.Text = "File Descrip";
            // 
            // brwsResults
            // 
            this.brwsResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brwsResults.Location = new System.Drawing.Point(0, 0);
            this.brwsResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.brwsResults.MinimumSize = new System.Drawing.Size(20, 20);
            this.brwsResults.Name = "brwsResults";
            this.brwsResults.Size = new System.Drawing.Size(618, 228);
            this.brwsResults.TabIndex = 22;
            // 
            // btnBrowse
            // 
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(528, 124);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(120, 50);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbxFilePath
            // 
            this.tbxFilePath.BackColor = System.Drawing.Color.DarkGray;
            this.tbxFilePath.ForeColor = System.Drawing.Color.Black;
            this.tbxFilePath.Location = new System.Drawing.Point(32, 128);
            this.tbxFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxFilePath.Name = "tbxFilePath";
            this.tbxFilePath.Size = new System.Drawing.Size(468, 20);
            this.tbxFilePath.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(28, 546);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(618, 16);
            this.progressBar1.Step = 5;
            this.progressBar1.TabIndex = 116;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.brwsResults);
            this.panel1.Location = new System.Drawing.Point(28, 596);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 228);
            this.panel1.TabIndex = 117;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(26, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 123;
            this.label2.Text = "Select File to Upload";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(26, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 124;
            this.label3.Text = "Selected Folder";
            // 
            // tbxSelectedFolder
            // 
            this.tbxSelectedFolder.BackColor = System.Drawing.Color.DarkGray;
            this.tbxSelectedFolder.ForeColor = System.Drawing.Color.Black;
            this.tbxSelectedFolder.Location = new System.Drawing.Point(32, 52);
            this.tbxSelectedFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxSelectedFolder.Name = "tbxSelectedFolder";
            this.tbxSelectedFolder.ReadOnly = true;
            this.tbxSelectedFolder.Size = new System.Drawing.Size(618, 20);
            this.tbxSelectedFolder.TabIndex = 125;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxFileDescrip);
            this.groupBox1.Controls.Add(this.tbxGlobalID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbxFileName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new System.Drawing.Point(32, 184);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(618, 264);
            this.groupBox1.TabIndex = 126;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit File Info (optional)";
            // 
            // ckbxAutoClose
            // 
            this.ckbxAutoClose.AutoSize = true;
            this.ckbxAutoClose.Checked = true;
            this.ckbxAutoClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbxAutoClose.ForeColor = System.Drawing.Color.LightGray;
            this.ckbxAutoClose.Location = new System.Drawing.Point(352, 492);
            this.ckbxAutoClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbxAutoClose.Name = "ckbxAutoClose";
            this.ckbxAutoClose.Size = new System.Drawing.Size(91, 27);
            this.ckbxAutoClose.TabIndex = 127;
            this.ckbxAutoClose.Text = "Close Form";
            this.ckbxAutoClose.UseVisualStyleBackColor = true;
            // 
            // frmFileUpload
            // 
            this.AcceptButton = this.btnUpload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(678, 852);
            this.Controls.Add(this.ckbxAutoClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbxSelectedFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbxFilePath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFileUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Upload";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxGlobalID;
        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.TextBox tbxFileDescrip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbxFilePath;
        private System.Windows.Forms.WebBrowser brwsResults;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSelectedFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbxAutoClose;
    }
}