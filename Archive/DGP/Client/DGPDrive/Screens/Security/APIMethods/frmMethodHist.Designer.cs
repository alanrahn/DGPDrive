namespace DGPDrive.Screens.Security
{
    partial class frmMethodHist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMethodHist));
            this.lblMethName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMethodHistory = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRecoverSelected = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethodHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMethName
            // 
            this.lblMethName.AutoSize = true;
            this.lblMethName.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblMethName.Location = new System.Drawing.Point(98, 9);
            this.lblMethName.Name = "lblMethName";
            this.lblMethName.Size = new System.Drawing.Size(0, 13);
            this.lblMethName.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Method Name: ";
            // 
            // dgvMethodHistory
            // 
            this.dgvMethodHistory.AllowUserToAddRows = false;
            this.dgvMethodHistory.AllowUserToDeleteRows = false;
            this.dgvMethodHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvMethodHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMethodHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMethodHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMethodHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMethodHistory.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMethodHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMethodHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMethodHistory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMethodHistory.EnableHeadersVisualStyles = false;
            this.dgvMethodHistory.GridColor = System.Drawing.Color.Black;
            this.dgvMethodHistory.Location = new System.Drawing.Point(12, 28);
            this.dgvMethodHistory.MultiSelect = false;
            this.dgvMethodHistory.Name = "dgvMethodHistory";
            this.dgvMethodHistory.ReadOnly = true;
            this.dgvMethodHistory.RowHeadersWidth = 10;
            this.dgvMethodHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMethodHistory.ShowEditingIcon = false;
            this.dgvMethodHistory.Size = new System.Drawing.Size(568, 210);
            this.dgvMethodHistory.TabIndex = 0;
            this.dgvMethodHistory.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            // btnRecoverSelected
            // 
            this.btnRecoverSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            // frmMethodHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(587, 275);
            this.Controls.Add(this.btnRecoverSelected);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMethName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvMethodHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMethodHist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "APIMethod Record History";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethodHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMethName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMethodHistory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRecoverSelected;
    }
}