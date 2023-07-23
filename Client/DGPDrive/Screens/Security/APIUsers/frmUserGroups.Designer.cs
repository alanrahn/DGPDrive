namespace DGPDrive.Screens.Security
{
    partial class frmUserGroups
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserGroups));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAvailableGroups = new System.Windows.Forms.DataGridView();
            this.dgvAssignedGroups = new System.Windows.Forms.DataGridView();
            this.rbtnReadOnly = new System.Windows.Forms.RadioButton();
            this.rbtnReadWrite = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(300, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Available Groups";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Assigned Groups";
            // 
            // dgvAvailableGroups
            // 
            this.dgvAvailableGroups.AllowUserToAddRows = false;
            this.dgvAvailableGroups.AllowUserToDeleteRows = false;
            this.dgvAvailableGroups.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAvailableGroups.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAvailableGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAvailableGroups.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvAvailableGroups.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAvailableGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAvailableGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAvailableGroups.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAvailableGroups.EnableHeadersVisualStyles = false;
            this.dgvAvailableGroups.GridColor = System.Drawing.Color.Black;
            this.dgvAvailableGroups.Location = new System.Drawing.Point(303, 27);
            this.dgvAvailableGroups.MultiSelect = false;
            this.dgvAvailableGroups.Name = "dgvAvailableGroups";
            this.dgvAvailableGroups.ReadOnly = true;
            this.dgvAvailableGroups.RowHeadersWidth = 10;
            this.dgvAvailableGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableGroups.ShowEditingIcon = false;
            this.dgvAvailableGroups.Size = new System.Drawing.Size(268, 296);
            this.dgvAvailableGroups.TabIndex = 10;
            this.dgvAvailableGroups.TabStop = false;
            this.dgvAvailableGroups.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAvailableGroups_CellDoubleClick);
            // 
            // dgvAssignedGroups
            // 
            this.dgvAssignedGroups.AllowUserToAddRows = false;
            this.dgvAssignedGroups.AllowUserToDeleteRows = false;
            this.dgvAssignedGroups.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAssignedGroups.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAssignedGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAssignedGroups.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvAssignedGroups.CausesValidation = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAssignedGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAssignedGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAssignedGroups.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAssignedGroups.EnableHeadersVisualStyles = false;
            this.dgvAssignedGroups.GridColor = System.Drawing.Color.Black;
            this.dgvAssignedGroups.Location = new System.Drawing.Point(13, 27);
            this.dgvAssignedGroups.MultiSelect = false;
            this.dgvAssignedGroups.Name = "dgvAssignedGroups";
            this.dgvAssignedGroups.ReadOnly = true;
            this.dgvAssignedGroups.RowHeadersWidth = 10;
            this.dgvAssignedGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssignedGroups.ShowEditingIcon = false;
            this.dgvAssignedGroups.Size = new System.Drawing.Size(268, 296);
            this.dgvAssignedGroups.TabIndex = 0;
            this.dgvAssignedGroups.TabStop = false;
            this.dgvAssignedGroups.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssignedGroups_CellDoubleClick);
            // 
            // rbtnReadOnly
            // 
            this.rbtnReadOnly.AutoSize = true;
            this.rbtnReadOnly.Checked = true;
            this.rbtnReadOnly.ForeColor = System.Drawing.Color.DarkGray;
            this.rbtnReadOnly.Location = new System.Drawing.Point(416, 7);
            this.rbtnReadOnly.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtnReadOnly.Name = "rbtnReadOnly";
            this.rbtnReadOnly.Size = new System.Drawing.Size(72, 17);
            this.rbtnReadOnly.TabIndex = 20;
            this.rbtnReadOnly.TabStop = true;
            this.rbtnReadOnly.Text = "ReadOnly";
            this.rbtnReadOnly.UseVisualStyleBackColor = true;
            // 
            // rbtnReadWrite
            // 
            this.rbtnReadWrite.AutoSize = true;
            this.rbtnReadWrite.ForeColor = System.Drawing.Color.DarkGray;
            this.rbtnReadWrite.Location = new System.Drawing.Point(494, 7);
            this.rbtnReadWrite.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtnReadWrite.Name = "rbtnReadWrite";
            this.rbtnReadWrite.Size = new System.Drawing.Size(76, 17);
            this.rbtnReadWrite.TabIndex = 21;
            this.rbtnReadWrite.Text = "ReadWrite";
            this.rbtnReadWrite.UseVisualStyleBackColor = true;
            // 
            // frmUserGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(584, 337);
            this.Controls.Add(this.rbtnReadWrite);
            this.Controls.Add(this.rbtnReadOnly);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAvailableGroups);
            this.Controls.Add(this.dgvAssignedGroups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User DataGroup Access";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAvailableGroups;
        private System.Windows.Forms.DataGridView dgvAssignedGroups;
        private System.Windows.Forms.RadioButton rbtnReadOnly;
        private System.Windows.Forms.RadioButton rbtnReadWrite;
    }
}