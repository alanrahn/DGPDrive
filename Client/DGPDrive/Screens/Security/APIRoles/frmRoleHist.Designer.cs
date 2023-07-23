namespace DGPDrive.Screens.Security
{
    partial class frmRoleHist
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoleHist));
            this.lblRoleName = new System.Windows.Forms.Label();
            this.lalel1 = new System.Windows.Forms.Label();
            this.dgvRoleHistory = new System.Windows.Forms.DataGridView();
            this.btnRecoverSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = true;
            this.lblRoleName.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblRoleName.Location = new System.Drawing.Point(98, 9);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(0, 13);
            this.lblRoleName.TabIndex = 22;
            // 
            // lalel1
            // 
            this.lalel1.AutoSize = true;
            this.lalel1.ForeColor = System.Drawing.Color.LightGray;
            this.lalel1.Location = new System.Drawing.Point(12, 9);
            this.lalel1.Name = "lalel1";
            this.lalel1.Size = new System.Drawing.Size(66, 13);
            this.lalel1.TabIndex = 21;
            this.lalel1.Text = "Role Name: ";
            // 
            // dgvRoleHistory
            // 
            this.dgvRoleHistory.AllowUserToAddRows = false;
            this.dgvRoleHistory.AllowUserToDeleteRows = false;
            this.dgvRoleHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvRoleHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRoleHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvRoleHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvRoleHistory.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoleHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRoleHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoleHistory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRoleHistory.EnableHeadersVisualStyles = false;
            this.dgvRoleHistory.GridColor = System.Drawing.Color.Black;
            this.dgvRoleHistory.Location = new System.Drawing.Point(12, 28);
            this.dgvRoleHistory.MultiSelect = false;
            this.dgvRoleHistory.Name = "dgvRoleHistory";
            this.dgvRoleHistory.ReadOnly = true;
            this.dgvRoleHistory.RowHeadersWidth = 10;
            this.dgvRoleHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoleHistory.ShowEditingIcon = false;
            this.dgvRoleHistory.Size = new System.Drawing.Size(568, 210);
            this.dgvRoleHistory.TabIndex = 0;
            this.dgvRoleHistory.TabStop = false;
            // 
            // btnRecoverSelected
            // 
            this.btnRecoverSelected.AutoSize = true;
            this.btnRecoverSelected.BackColor = System.Drawing.Color.LightGray;
            this.btnRecoverSelected.Enabled = false;
            this.btnRecoverSelected.ForeColor = System.Drawing.Color.Black;
            this.btnRecoverSelected.Location = new System.Drawing.Point(436, 244);
            this.btnRecoverSelected.Name = "btnRecoverSelected";
            this.btnRecoverSelected.Size = new System.Drawing.Size(143, 23);
            this.btnRecoverSelected.TabIndex = 20;
            this.btnRecoverSelected.Text = "Recover Selected Record";
            this.btnRecoverSelected.UseVisualStyleBackColor = false;
            this.btnRecoverSelected.Click += new System.EventHandler(this.btnRecoverSelected_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(365, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmRoleHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(587, 275);
            this.Controls.Add(this.btnRecoverSelected);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblRoleName);
            this.Controls.Add(this.lalel1);
            this.Controls.Add(this.dgvRoleHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRoleHist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "APIRole Record History";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.Label lalel1;
        private System.Windows.Forms.DataGridView dgvRoleHistory;
        private System.Windows.Forms.Button btnRecoverSelected;
        private System.Windows.Forms.Button btnCancel;
    }
}