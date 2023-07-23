namespace DGPDrive.Screens.Security
{
    partial class frmGroupHist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroupHist));
            this.lblGroupName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvGroupHistory = new System.Windows.Forms.DataGridView();
            this.btnRecoverSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblGroupName.Location = new System.Drawing.Point(98, 9);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(0, 13);
            this.lblGroupName.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "DataGroup Name: ";
            // 
            // dgvGroupHistory
            // 
            this.dgvGroupHistory.AllowUserToAddRows = false;
            this.dgvGroupHistory.AllowUserToDeleteRows = false;
            this.dgvGroupHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvGroupHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGroupHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvGroupHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvGroupHistory.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGroupHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGroupHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGroupHistory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGroupHistory.EnableHeadersVisualStyles = false;
            this.dgvGroupHistory.GridColor = System.Drawing.Color.Black;
            this.dgvGroupHistory.Location = new System.Drawing.Point(12, 28);
            this.dgvGroupHistory.MultiSelect = false;
            this.dgvGroupHistory.Name = "dgvGroupHistory";
            this.dgvGroupHistory.ReadOnly = true;
            this.dgvGroupHistory.RowHeadersWidth = 10;
            this.dgvGroupHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGroupHistory.ShowEditingIcon = false;
            this.dgvGroupHistory.Size = new System.Drawing.Size(568, 210);
            this.dgvGroupHistory.TabIndex = 0;
            // 
            // btnRecoverSelected
            // 
            this.btnRecoverSelected.AutoSize = true;
            this.btnRecoverSelected.BackColor = System.Drawing.Color.Gainsboro;
            this.btnRecoverSelected.Enabled = false;
            this.btnRecoverSelected.ForeColor = System.Drawing.Color.Black;
            this.btnRecoverSelected.Location = new System.Drawing.Point(436, 244);
            this.btnRecoverSelected.Name = "btnRecoverSelected";
            this.btnRecoverSelected.Size = new System.Drawing.Size(143, 23);
            this.btnRecoverSelected.TabIndex = 26;
            this.btnRecoverSelected.Text = "Recover Selected Record";
            this.btnRecoverSelected.UseVisualStyleBackColor = false;
            this.btnRecoverSelected.Click += new System.EventHandler(this.btnRecoverSelected_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(366, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmGroupHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(587, 275);
            this.Controls.Add(this.btnRecoverSelected);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblGroupName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvGroupHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGroupHist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DataGroup Record History";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvGroupHistory;
        private System.Windows.Forms.Button btnRecoverSelected;
        private System.Windows.Forms.Button btnCancel;
    }
}