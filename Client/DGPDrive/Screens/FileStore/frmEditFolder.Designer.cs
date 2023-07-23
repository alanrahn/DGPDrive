namespace DGPDrive.Screens.FileStore
{
    partial class frmEditFolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditFolder));
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxGlobalID = new System.Windows.Forms.TextBox();
            this.tbxParentGID = new System.Windows.Forms.TextBox();
            this.tbxFolderName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxDataGroups = new System.Windows.Forms.ComboBox();
            this.tbxDisplayOrder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(24, 190);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "DataGroup*";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(196, 288);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 46);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.BackColor = System.Drawing.Color.LightGray;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(328, 288);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 46);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(476, 288);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 46);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(24, 88);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "Parent GID*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(24, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Global ID";
            // 
            // tbxGlobalID
            // 
            this.tbxGlobalID.BackColor = System.Drawing.Color.LightGray;
            this.tbxGlobalID.ForeColor = System.Drawing.Color.Black;
            this.tbxGlobalID.Location = new System.Drawing.Point(196, 30);
            this.tbxGlobalID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxGlobalID.MaxLength = 50;
            this.tbxGlobalID.Name = "tbxGlobalID";
            this.tbxGlobalID.ReadOnly = true;
            this.tbxGlobalID.Size = new System.Drawing.Size(396, 20);
            this.tbxGlobalID.TabIndex = 0;
            this.tbxGlobalID.TabStop = false;
            // 
            // tbxParentGID
            // 
            this.tbxParentGID.BackColor = System.Drawing.Color.LightGray;
            this.tbxParentGID.Enabled = false;
            this.tbxParentGID.ForeColor = System.Drawing.Color.Black;
            this.tbxParentGID.Location = new System.Drawing.Point(196, 82);
            this.tbxParentGID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxParentGID.MaxLength = 50;
            this.tbxParentGID.Name = "tbxParentGID";
            this.tbxParentGID.Size = new System.Drawing.Size(396, 20);
            this.tbxParentGID.TabIndex = 1;
            // 
            // tbxFolderName
            // 
            this.tbxFolderName.BackColor = System.Drawing.Color.LightGray;
            this.tbxFolderName.ForeColor = System.Drawing.Color.Black;
            this.tbxFolderName.Location = new System.Drawing.Point(196, 134);
            this.tbxFolderName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxFolderName.MaxLength = 50;
            this.tbxFolderName.Name = "tbxFolderName";
            this.tbxFolderName.Size = new System.Drawing.Size(396, 20);
            this.tbxFolderName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(24, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Folder Name*";
            // 
            // cmbxDataGroups
            // 
            this.cmbxDataGroups.BackColor = System.Drawing.Color.LightGray;
            this.cmbxDataGroups.FormattingEnabled = true;
            this.cmbxDataGroups.Location = new System.Drawing.Point(196, 184);
            this.cmbxDataGroups.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbxDataGroups.Name = "cmbxDataGroups";
            this.cmbxDataGroups.Size = new System.Drawing.Size(396, 21);
            this.cmbxDataGroups.TabIndex = 3;
            // 
            // tbxDisplayOrder
            // 
            this.tbxDisplayOrder.BackColor = System.Drawing.Color.LightGray;
            this.tbxDisplayOrder.ForeColor = System.Drawing.Color.Black;
            this.tbxDisplayOrder.Location = new System.Drawing.Point(196, 236);
            this.tbxDisplayOrder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxDisplayOrder.MaxLength = 30;
            this.tbxDisplayOrder.Name = "tbxDisplayOrder";
            this.tbxDisplayOrder.Size = new System.Drawing.Size(396, 20);
            this.tbxDisplayOrder.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(24, 242);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "Display Order";
            // 
            // frmEditFolder
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(624, 362);
            this.Controls.Add(this.tbxDisplayOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbxDataGroups);
            this.Controls.Add(this.tbxFolderName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxParentGID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxGlobalID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Subfolder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxGlobalID;
        private System.Windows.Forms.TextBox tbxParentGID;
        private System.Windows.Forms.TextBox tbxFolderName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxDataGroups;
        private System.Windows.Forms.TextBox tbxDisplayOrder;
        private System.Windows.Forms.Label label3;
    }
}