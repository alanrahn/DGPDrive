namespace DGPDrive.Screens.Help
{
    partial class frmUtility
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUtility));
            this.dtpSelectDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxUnixTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSelUnixTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxDateString = new System.Windows.Forms.TextBox();
            this.btnConvertDate = new System.Windows.Forms.Button();
            this.btnConvertUnixtime = new System.Windows.Forms.Button();
            this.grpbxEventSource = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCreateEventSource = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxEventID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxEventSourceName = new System.Windows.Forms.TextBox();
            this.grpbxUnixTime = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.tbxFinishText = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxEncryptKey = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxStartText = new System.Windows.Forms.TextBox();
            this.grpbxEventSource.SuspendLayout();
            this.grpbxUnixTime.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpSelectDate
            // 
            this.dtpSelectDate.Location = new System.Drawing.Point(23, 62);
            this.dtpSelectDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpSelectDate.Name = "dtpSelectDate";
            this.dtpSelectDate.Size = new System.Drawing.Size(400, 31);
            this.dtpSelectDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(21, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Date";
            // 
            // tbxUnixTime
            // 
            this.tbxUnixTime.Location = new System.Drawing.Point(629, 64);
            this.tbxUnixTime.Margin = new System.Windows.Forms.Padding(4);
            this.tbxUnixTime.Name = "tbxUnixTime";
            this.tbxUnixTime.Size = new System.Drawing.Size(400, 31);
            this.tbxUnixTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(623, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Unix Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(21, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Select Unix Time";
            // 
            // tbxSelUnixTime
            // 
            this.tbxSelUnixTime.Location = new System.Drawing.Point(23, 158);
            this.tbxSelUnixTime.Margin = new System.Windows.Forms.Padding(4);
            this.tbxSelUnixTime.Name = "tbxSelUnixTime";
            this.tbxSelUnixTime.Size = new System.Drawing.Size(400, 31);
            this.tbxSelUnixTime.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(623, 128);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Date";
            // 
            // tbxDateString
            // 
            this.tbxDateString.Location = new System.Drawing.Point(629, 158);
            this.tbxDateString.Margin = new System.Windows.Forms.Padding(4);
            this.tbxDateString.Name = "tbxDateString";
            this.tbxDateString.Size = new System.Drawing.Size(400, 31);
            this.tbxDateString.TabIndex = 6;
            // 
            // btnConvertDate
            // 
            this.btnConvertDate.AutoSize = true;
            this.btnConvertDate.BackColor = System.Drawing.Color.LightGray;
            this.btnConvertDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvertDate.ForeColor = System.Drawing.Color.Black;
            this.btnConvertDate.Location = new System.Drawing.Point(457, 49);
            this.btnConvertDate.Margin = new System.Windows.Forms.Padding(4);
            this.btnConvertDate.Name = "btnConvertDate";
            this.btnConvertDate.Size = new System.Drawing.Size(138, 50);
            this.btnConvertDate.TabIndex = 2;
            this.btnConvertDate.Text = "Convert";
            this.btnConvertDate.UseVisualStyleBackColor = false;
            this.btnConvertDate.Click += new System.EventHandler(this.btnConvertDate_Click);
            // 
            // btnConvertUnixtime
            // 
            this.btnConvertUnixtime.AutoSize = true;
            this.btnConvertUnixtime.BackColor = System.Drawing.Color.LightGray;
            this.btnConvertUnixtime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvertUnixtime.ForeColor = System.Drawing.Color.Black;
            this.btnConvertUnixtime.Location = new System.Drawing.Point(457, 142);
            this.btnConvertUnixtime.Margin = new System.Windows.Forms.Padding(4);
            this.btnConvertUnixtime.Name = "btnConvertUnixtime";
            this.btnConvertUnixtime.Size = new System.Drawing.Size(138, 50);
            this.btnConvertUnixtime.TabIndex = 5;
            this.btnConvertUnixtime.Text = "Convert";
            this.btnConvertUnixtime.UseVisualStyleBackColor = false;
            this.btnConvertUnixtime.Click += new System.EventHandler(this.btnConvertUnixtime_Click);
            // 
            // grpbxEventSource
            // 
            this.grpbxEventSource.Controls.Add(this.label7);
            this.grpbxEventSource.Controls.Add(this.btnCreateEventSource);
            this.grpbxEventSource.Controls.Add(this.label6);
            this.grpbxEventSource.Controls.Add(this.tbxEventID);
            this.grpbxEventSource.Controls.Add(this.label5);
            this.grpbxEventSource.Controls.Add(this.tbxEventSourceName);
            this.grpbxEventSource.ForeColor = System.Drawing.Color.LightGray;
            this.grpbxEventSource.Location = new System.Drawing.Point(23, 272);
            this.grpbxEventSource.Name = "grpbxEventSource";
            this.grpbxEventSource.Size = new System.Drawing.Size(1058, 154);
            this.grpbxEventSource.TabIndex = 17;
            this.grpbxEventSource.TabStop = false;
            this.grpbxEventSource.Text = "Event Source";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.LightGray;
            this.label7.Location = new System.Drawing.Point(21, 113);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(700, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "(You must be logged in as a Windows admin to create an Event Source)";
            // 
            // btnCreateEventSource
            // 
            this.btnCreateEventSource.AutoSize = true;
            this.btnCreateEventSource.BackColor = System.Drawing.Color.LightGray;
            this.btnCreateEventSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateEventSource.ForeColor = System.Drawing.Color.Black;
            this.btnCreateEventSource.Location = new System.Drawing.Point(891, 51);
            this.btnCreateEventSource.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateEventSource.Name = "btnCreateEventSource";
            this.btnCreateEventSource.Size = new System.Drawing.Size(138, 50);
            this.btnCreateEventSource.TabIndex = 9;
            this.btnCreateEventSource.Text = "Create";
            this.btnCreateEventSource.UseVisualStyleBackColor = false;
            this.btnCreateEventSource.Click += new System.EventHandler(this.btnCreateEventSource_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.LightGray;
            this.label6.Location = new System.Drawing.Point(451, 37);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "Event ID (optional)";
            // 
            // tbxEventID
            // 
            this.tbxEventID.Location = new System.Drawing.Point(457, 67);
            this.tbxEventID.Margin = new System.Windows.Forms.Padding(4);
            this.tbxEventID.Name = "tbxEventID";
            this.tbxEventID.Size = new System.Drawing.Size(400, 31);
            this.tbxEventID.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(20, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Event Source Name";
            // 
            // tbxEventSourceName
            // 
            this.tbxEventSourceName.Location = new System.Drawing.Point(24, 67);
            this.tbxEventSourceName.Margin = new System.Windows.Forms.Padding(4);
            this.tbxEventSourceName.Name = "tbxEventSourceName";
            this.tbxEventSourceName.Size = new System.Drawing.Size(400, 31);
            this.tbxEventSourceName.TabIndex = 7;
            // 
            // grpbxUnixTime
            // 
            this.grpbxUnixTime.Controls.Add(this.btnConvertDate);
            this.grpbxUnixTime.Controls.Add(this.dtpSelectDate);
            this.grpbxUnixTime.Controls.Add(this.btnConvertUnixtime);
            this.grpbxUnixTime.Controls.Add(this.label1);
            this.grpbxUnixTime.Controls.Add(this.tbxUnixTime);
            this.grpbxUnixTime.Controls.Add(this.label4);
            this.grpbxUnixTime.Controls.Add(this.label2);
            this.grpbxUnixTime.Controls.Add(this.tbxDateString);
            this.grpbxUnixTime.Controls.Add(this.tbxSelUnixTime);
            this.grpbxUnixTime.Controls.Add(this.label3);
            this.grpbxUnixTime.ForeColor = System.Drawing.Color.LightGray;
            this.grpbxUnixTime.Location = new System.Drawing.Point(23, 22);
            this.grpbxUnixTime.Name = "grpbxUnixTime";
            this.grpbxUnixTime.Size = new System.Drawing.Size(1058, 230);
            this.grpbxUnixTime.TabIndex = 18;
            this.grpbxUnixTime.TabStop = false;
            this.grpbxUnixTime.Text = "Unix Time";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnEncrypt);
            this.groupBox1.Controls.Add(this.tbxFinishText);
            this.groupBox1.Controls.Add(this.btnDecrypt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbxEncryptKey);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbxStartText);
            this.groupBox1.ForeColor = System.Drawing.Color.LightGray;
            this.groupBox1.Location = new System.Drawing.Point(23, 448);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1058, 478);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encryption / Decryption";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(21, 290);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 25);
            this.label8.TabIndex = 23;
            this.label8.Text = "Finished Text";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.AutoSize = true;
            this.btnEncrypt.BackColor = System.Drawing.Color.LightGray;
            this.btnEncrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEncrypt.ForeColor = System.Drawing.Color.Black;
            this.btnEncrypt.Location = new System.Drawing.Point(26, 219);
            this.btnEncrypt.Margin = new System.Windows.Forms.Padding(4);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(138, 50);
            this.btnEncrypt.TabIndex = 11;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = false;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // tbxFinishText
            // 
            this.tbxFinishText.Location = new System.Drawing.Point(23, 319);
            this.tbxFinishText.Margin = new System.Windows.Forms.Padding(4);
            this.tbxFinishText.Multiline = true;
            this.tbxFinishText.Name = "tbxFinishText";
            this.tbxFinishText.Size = new System.Drawing.Size(1003, 121);
            this.tbxFinishText.TabIndex = 14;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.AutoSize = true;
            this.btnDecrypt.BackColor = System.Drawing.Color.LightGray;
            this.btnDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecrypt.ForeColor = System.Drawing.Color.Black;
            this.btnDecrypt.Location = new System.Drawing.Point(891, 219);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(4);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(138, 50);
            this.btnDecrypt.TabIndex = 13;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = false;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.LightGray;
            this.label9.Location = new System.Drawing.Point(208, 209);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(263, 25);
            this.label9.TabIndex = 20;
            this.label9.Text = "Symmetric Encryption Key";
            // 
            // tbxEncryptKey
            // 
            this.tbxEncryptKey.Location = new System.Drawing.Point(213, 238);
            this.tbxEncryptKey.Margin = new System.Windows.Forms.Padding(4);
            this.tbxEncryptKey.Name = "tbxEncryptKey";
            this.tbxEncryptKey.Size = new System.Drawing.Size(624, 31);
            this.tbxEncryptKey.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.LightGray;
            this.label10.Location = new System.Drawing.Point(20, 37);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 25);
            this.label10.TabIndex = 18;
            this.label10.Text = "Starting Text";
            // 
            // tbxStartText
            // 
            this.tbxStartText.Location = new System.Drawing.Point(26, 67);
            this.tbxStartText.Margin = new System.Windows.Forms.Padding(4);
            this.tbxStartText.Multiline = true;
            this.tbxStartText.Name = "tbxStartText";
            this.tbxStartText.Size = new System.Drawing.Size(1003, 121);
            this.tbxStartText.TabIndex = 10;
            // 
            // frmUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1108, 956);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpbxUnixTime);
            this.Controls.Add(this.grpbxEventSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUtility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DGP Lattice Utility";
            this.grpbxEventSource.ResumeLayout(false);
            this.grpbxEventSource.PerformLayout();
            this.grpbxUnixTime.ResumeLayout(false);
            this.grpbxUnixTime.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpSelectDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxUnixTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSelUnixTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxDateString;
        private System.Windows.Forms.Button btnConvertDate;
        private System.Windows.Forms.Button btnConvertUnixtime;
        private System.Windows.Forms.GroupBox grpbxEventSource;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCreateEventSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxEventID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxEventSourceName;
        private System.Windows.Forms.GroupBox grpbxUnixTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox tbxFinishText;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxEncryptKey;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxStartText;
    }
}