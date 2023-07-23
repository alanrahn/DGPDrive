namespace DGPDrive.Screens.Security
{
    partial class frmEditUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditUser));
            this.label12 = new System.Windows.Forms.Label();
            this.tbxRateLimit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxAcctType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxAcctState = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxLastName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxMiddleName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxFirstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxGlobalID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.LightGray;
            this.label12.Location = new System.Drawing.Point(24, 558);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 82;
            this.label12.Text = "Rate Limit (min)";
            // 
            // tbxRateLimit
            // 
            this.tbxRateLimit.BackColor = System.Drawing.Color.LightGray;
            this.tbxRateLimit.ForeColor = System.Drawing.Color.Black;
            this.tbxRateLimit.Location = new System.Drawing.Point(204, 552);
            this.tbxRateLimit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxRateLimit.MaxLength = 45;
            this.tbxRateLimit.Name = "tbxRateLimit";
            this.tbxRateLimit.Size = new System.Drawing.Size(396, 20);
            this.tbxRateLimit.TabIndex = 100;
            this.tbxRateLimit.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(24, 450);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 81;
            this.label8.Text = "Acct Type*";
            // 
            // cbxAcctType
            // 
            this.cbxAcctType.BackColor = System.Drawing.Color.LightGray;
            this.cbxAcctType.ForeColor = System.Drawing.Color.Black;
            this.cbxAcctType.FormattingEnabled = true;
            this.cbxAcctType.Items.AddRange(new object[] {
            "STANDARD",
            "SYSTEM"});
            this.cbxAcctType.Location = new System.Drawing.Point(202, 444);
            this.cbxAcctType.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbxAcctType.Name = "cbxAcctType";
            this.cbxAcctType.Size = new System.Drawing.Size(396, 21);
            this.cbxAcctType.TabIndex = 80;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.LightGray;
            this.label11.Location = new System.Drawing.Point(24, 138);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 80;
            this.label11.Text = "Password*";
            // 
            // tbxPassword
            // 
            this.tbxPassword.BackColor = System.Drawing.Color.LightGray;
            this.tbxPassword.ForeColor = System.Drawing.Color.Black;
            this.tbxPassword.Location = new System.Drawing.Point(202, 132);
            this.tbxPassword.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxPassword.MaxLength = 180;
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.Size = new System.Drawing.Size(396, 20);
            this.tbxPassword.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(202, 604);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 46);
            this.btnCancel.TabIndex = 110;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.LightGray;
            this.label10.Location = new System.Drawing.Point(24, 346);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 79;
            this.label10.Text = "Email*";
            // 
            // tbxEmail
            // 
            this.tbxEmail.BackColor = System.Drawing.Color.LightGray;
            this.tbxEmail.ForeColor = System.Drawing.Color.Black;
            this.tbxEmail.Location = new System.Drawing.Point(204, 340);
            this.tbxEmail.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxEmail.MaxLength = 45;
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(396, 20);
            this.tbxEmail.TabIndex = 60;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.BackColor = System.Drawing.Color.LightGray;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(334, 604);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 46);
            this.btnDelete.TabIndex = 120;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(482, 604);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 46);
            this.btnSave.TabIndex = 130;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.LightGray;
            this.label9.Location = new System.Drawing.Point(24, 504);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 78;
            this.label9.Text = "Acct State*";
            // 
            // cbxAcctState
            // 
            this.cbxAcctState.BackColor = System.Drawing.Color.LightGray;
            this.cbxAcctState.ForeColor = System.Drawing.Color.Black;
            this.cbxAcctState.FormattingEnabled = true;
            this.cbxAcctState.Items.AddRange(new object[] {
            "ENABLED",
            "DISABLED"});
            this.cbxAcctState.Location = new System.Drawing.Point(202, 498);
            this.cbxAcctState.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbxAcctState.Name = "cbxAcctState";
            this.cbxAcctState.Size = new System.Drawing.Size(396, 21);
            this.cbxAcctState.TabIndex = 90;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.LightGray;
            this.label7.Location = new System.Drawing.Point(24, 404);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 77;
            this.label7.Text = "Expiration*";
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Location = new System.Drawing.Point(204, 392);
            this.dtpExpiration.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(396, 20);
            this.dtpExpiration.TabIndex = 70;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(24, 294);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Last Name*";
            // 
            // tbxLastName
            // 
            this.tbxLastName.BackColor = System.Drawing.Color.LightGray;
            this.tbxLastName.ForeColor = System.Drawing.Color.Black;
            this.tbxLastName.Location = new System.Drawing.Point(202, 288);
            this.tbxLastName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxLastName.MaxLength = 45;
            this.tbxLastName.Name = "tbxLastName";
            this.tbxLastName.Size = new System.Drawing.Size(396, 20);
            this.tbxLastName.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(24, 242);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Middle Name";
            // 
            // tbxMiddleName
            // 
            this.tbxMiddleName.BackColor = System.Drawing.Color.LightGray;
            this.tbxMiddleName.ForeColor = System.Drawing.Color.Black;
            this.tbxMiddleName.Location = new System.Drawing.Point(202, 236);
            this.tbxMiddleName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxMiddleName.MaxLength = 45;
            this.tbxMiddleName.Name = "tbxMiddleName";
            this.tbxMiddleName.Size = new System.Drawing.Size(396, 20);
            this.tbxMiddleName.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(24, 190);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "First Name*";
            // 
            // tbxFirstName
            // 
            this.tbxFirstName.BackColor = System.Drawing.Color.LightGray;
            this.tbxFirstName.ForeColor = System.Drawing.Color.Black;
            this.tbxFirstName.Location = new System.Drawing.Point(204, 184);
            this.tbxFirstName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxFirstName.MaxLength = 45;
            this.tbxFirstName.Name = "tbxFirstName";
            this.tbxFirstName.Size = new System.Drawing.Size(396, 20);
            this.tbxFirstName.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(24, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "User Name*";
            // 
            // tbxUserName
            // 
            this.tbxUserName.BackColor = System.Drawing.Color.LightGray;
            this.tbxUserName.ForeColor = System.Drawing.Color.Black;
            this.tbxUserName.Location = new System.Drawing.Point(202, 80);
            this.tbxUserName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxUserName.MaxLength = 45;
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(396, 20);
            this.tbxUserName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(24, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Global ID";
            // 
            // tbxGlobalID
            // 
            this.tbxGlobalID.BackColor = System.Drawing.Color.LightGray;
            this.tbxGlobalID.Enabled = false;
            this.tbxGlobalID.ForeColor = System.Drawing.Color.Black;
            this.tbxGlobalID.Location = new System.Drawing.Point(202, 28);
            this.tbxGlobalID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxGlobalID.MaxLength = 45;
            this.tbxGlobalID.Name = "tbxGlobalID";
            this.tbxGlobalID.ReadOnly = true;
            this.tbxGlobalID.Size = new System.Drawing.Size(396, 20);
            this.tbxGlobalID.TabIndex = 0;
            this.tbxGlobalID.TabStop = false;
            // 
            // frmEditUser
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(628, 682);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbxRateLimit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbxAcctType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbxEmail);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbxAcctState);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpExpiration);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxLastName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxMiddleName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxFirstName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxGlobalID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxRateLimit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxAcctType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxEmail;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxAcctState;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxLastName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxMiddleName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxFirstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxGlobalID;
    }
}