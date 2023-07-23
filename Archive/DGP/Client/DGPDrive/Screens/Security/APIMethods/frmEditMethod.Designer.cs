namespace DGPDrive.Screens.Security
{
    partial class frmEditMethod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditMethod));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxMethodName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxGlobalID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxAPIName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxVersionName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(200, 340);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 46);
            this.btnCancel.TabIndex = 50;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.BackColor = System.Drawing.Color.LightGray;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(332, 340);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 46);
            this.btnDelete.TabIndex = 60;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(480, 340);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 46);
            this.btnSave.TabIndex = 70;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(32, 136);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Method Name*";
            // 
            // tbxMethodName
            // 
            this.tbxMethodName.BackColor = System.Drawing.Color.LightGray;
            this.tbxMethodName.ForeColor = System.Drawing.Color.Black;
            this.tbxMethodName.Location = new System.Drawing.Point(200, 130);
            this.tbxMethodName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxMethodName.MaxLength = 90;
            this.tbxMethodName.Name = "tbxMethodName";
            this.tbxMethodName.Size = new System.Drawing.Size(396, 20);
            this.tbxMethodName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Global ID";
            // 
            // tbxGlobalID
            // 
            this.tbxGlobalID.BackColor = System.Drawing.Color.LightGray;
            this.tbxGlobalID.ForeColor = System.Drawing.Color.Black;
            this.tbxGlobalID.Location = new System.Drawing.Point(200, 26);
            this.tbxGlobalID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxGlobalID.MaxLength = 45;
            this.tbxGlobalID.Name = "tbxGlobalID";
            this.tbxGlobalID.ReadOnly = true;
            this.tbxGlobalID.Size = new System.Drawing.Size(396, 20);
            this.tbxGlobalID.TabIndex = 0;
            this.tbxGlobalID.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(32, 240);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "Description*";
            // 
            // tbxDescription
            // 
            this.tbxDescription.BackColor = System.Drawing.Color.LightGray;
            this.tbxDescription.ForeColor = System.Drawing.Color.Black;
            this.tbxDescription.Location = new System.Drawing.Point(200, 234);
            this.tbxDescription.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxDescription.MaxLength = 90;
            this.tbxDescription.Multiline = true;
            this.tbxDescription.Name = "tbxDescription";
            this.tbxDescription.Size = new System.Drawing.Size(396, 90);
            this.tbxDescription.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(32, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "API Name*";
            // 
            // tbxAPIName
            // 
            this.tbxAPIName.BackColor = System.Drawing.Color.LightGray;
            this.tbxAPIName.ForeColor = System.Drawing.Color.Black;
            this.tbxAPIName.Location = new System.Drawing.Point(200, 78);
            this.tbxAPIName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxAPIName.MaxLength = 30;
            this.tbxAPIName.Name = "tbxAPIName";
            this.tbxAPIName.Size = new System.Drawing.Size(396, 20);
            this.tbxAPIName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(32, 188);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Version Name*";
            // 
            // tbxVersionName
            // 
            this.tbxVersionName.BackColor = System.Drawing.Color.LightGray;
            this.tbxVersionName.ForeColor = System.Drawing.Color.Black;
            this.tbxVersionName.Location = new System.Drawing.Point(200, 182);
            this.tbxVersionName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxVersionName.MaxLength = 90;
            this.tbxVersionName.Name = "tbxVersionName";
            this.tbxVersionName.Size = new System.Drawing.Size(396, 20);
            this.tbxVersionName.TabIndex = 30;
            // 
            // frmEditMethod
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(628, 414);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxVersionName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxAPIName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxMethodName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxGlobalID);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditMethod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Method";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxMethodName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxGlobalID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxAPIName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxVersionName;
    }
}