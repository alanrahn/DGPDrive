namespace DGPDrive.Screens.FileStore
{
    partial class frmTagHist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTagHist));
            this.lblTagName = new System.Windows.Forms.Label();
            this.lalel1 = new System.Windows.Forms.Label();
            this.dgvTagHistory = new System.Windows.Forms.DataGridView();
            this.btnRecoverSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTagHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTagName
            // 
            this.lblTagName.AutoSize = true;
            this.lblTagName.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblTagName.Location = new System.Drawing.Point(196, 18);
            this.lblTagName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTagName.Name = "lblTagName";
            this.lblTagName.Size = new System.Drawing.Size(0, 25);
            this.lblTagName.TabIndex = 22;
            // 
            // lalel1
            // 
            this.lalel1.AutoSize = true;
            this.lalel1.ForeColor = System.Drawing.Color.LightGray;
            this.lalel1.Location = new System.Drawing.Point(24, 18);
            this.lalel1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lalel1.Name = "lalel1";
            this.lalel1.Size = new System.Drawing.Size(123, 25);
            this.lalel1.TabIndex = 21;
            this.lalel1.Text = "Tag Name: ";
            // 
            // dgvTagHistory
            // 
            this.dgvTagHistory.AllowUserToAddRows = false;
            this.dgvTagHistory.AllowUserToDeleteRows = false;
            this.dgvTagHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTagHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTagHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTagHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvTagHistory.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTagHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTagHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTagHistory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTagHistory.EnableHeadersVisualStyles = false;
            this.dgvTagHistory.GridColor = System.Drawing.Color.Black;
            this.dgvTagHistory.Location = new System.Drawing.Point(24, 56);
            this.dgvTagHistory.Margin = new System.Windows.Forms.Padding(6);
            this.dgvTagHistory.MultiSelect = false;
            this.dgvTagHistory.Name = "dgvTagHistory";
            this.dgvTagHistory.ReadOnly = true;
            this.dgvTagHistory.RowHeadersWidth = 10;
            this.dgvTagHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTagHistory.ShowEditingIcon = false;
            this.dgvTagHistory.Size = new System.Drawing.Size(1136, 420);
            this.dgvTagHistory.TabIndex = 0;
            this.dgvTagHistory.TabStop = false;
            // 
            // btnRecoverSelected
            // 
            this.btnRecoverSelected.AutoSize = true;
            this.btnRecoverSelected.BackColor = System.Drawing.Color.LightGray;
            this.btnRecoverSelected.Enabled = false;
            this.btnRecoverSelected.ForeColor = System.Drawing.Color.Black;
            this.btnRecoverSelected.Location = new System.Drawing.Point(872, 488);
            this.btnRecoverSelected.Margin = new System.Windows.Forms.Padding(6);
            this.btnRecoverSelected.Name = "btnRecoverSelected";
            this.btnRecoverSelected.Size = new System.Drawing.Size(286, 46);
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
            this.btnCancel.Location = new System.Drawing.Point(730, 488);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 46);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmTagHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1174, 550);
            this.Controls.Add(this.btnRecoverSelected);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTagName);
            this.Controls.Add(this.lalel1);
            this.Controls.Add(this.dgvTagHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTagHist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tag Record History";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTagHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTagName;
        private System.Windows.Forms.Label lalel1;
        private System.Windows.Forms.DataGridView dgvTagHistory;
        private System.Windows.Forms.Button btnRecoverSelected;
        private System.Windows.Forms.Button btnCancel;
    }
}