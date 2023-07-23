namespace DGPDrive.Screens.FileStore
{
    partial class frmFileTags
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileTags));
            this.dgvAssignedTags = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbxFilterTagName = new System.Windows.Forms.TextBox();
            this.dgvAvailableTags = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTags)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAssignedTags
            // 
            this.dgvAssignedTags.AllowUserToAddRows = false;
            this.dgvAssignedTags.AllowUserToDeleteRows = false;
            this.dgvAssignedTags.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAssignedTags.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAssignedTags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAssignedTags.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvAssignedTags.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAssignedTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAssignedTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAssignedTags.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAssignedTags.EnableHeadersVisualStyles = false;
            this.dgvAssignedTags.GridColor = System.Drawing.Color.Black;
            this.dgvAssignedTags.Location = new System.Drawing.Point(24, 48);
            this.dgvAssignedTags.Margin = new System.Windows.Forms.Padding(6);
            this.dgvAssignedTags.MultiSelect = false;
            this.dgvAssignedTags.Name = "dgvAssignedTags";
            this.dgvAssignedTags.ReadOnly = true;
            this.dgvAssignedTags.RowHeadersWidth = 10;
            this.dgvAssignedTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssignedTags.ShowEditingIcon = false;
            this.dgvAssignedTags.Size = new System.Drawing.Size(400, 502);
            this.dgvAssignedTags.TabIndex = 19;
            this.dgvAssignedTags.TabStop = false;
            this.dgvAssignedTags.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssignedTags_CellDoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.BackColor = System.Drawing.Color.LightGray;
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(765, 504);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(116, 46);
            this.btnSearch.TabIndex = 33;
            this.btnSearch.Text = "Filter";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbxFilterTagName
            // 
            this.tbxFilterTagName.BackColor = System.Drawing.Color.LightGray;
            this.tbxFilterTagName.Location = new System.Drawing.Point(461, 519);
            this.tbxFilterTagName.Margin = new System.Windows.Forms.Padding(4);
            this.tbxFilterTagName.Name = "tbxFilterTagName";
            this.tbxFilterTagName.Size = new System.Drawing.Size(276, 31);
            this.tbxFilterTagName.TabIndex = 31;
            // 
            // dgvAvailableTags
            // 
            this.dgvAvailableTags.AllowUserToAddRows = false;
            this.dgvAvailableTags.AllowUserToDeleteRows = false;
            this.dgvAvailableTags.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAvailableTags.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAvailableTags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableTags.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvAvailableTags.CausesValidation = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAvailableTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAvailableTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAvailableTags.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAvailableTags.EnableHeadersVisualStyles = false;
            this.dgvAvailableTags.GridColor = System.Drawing.Color.Black;
            this.dgvAvailableTags.Location = new System.Drawing.Point(461, 47);
            this.dgvAvailableTags.Margin = new System.Windows.Forms.Padding(6);
            this.dgvAvailableTags.MultiSelect = false;
            this.dgvAvailableTags.Name = "dgvAvailableTags";
            this.dgvAvailableTags.ReadOnly = true;
            this.dgvAvailableTags.RowHeadersWidth = 10;
            this.dgvAvailableTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableTags.ShowEditingIcon = false;
            this.dgvAvailableTags.Size = new System.Drawing.Size(420, 436);
            this.dgvAvailableTags.TabIndex = 30;
            this.dgvAvailableTags.TabStop = false;
            this.dgvAvailableTags.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAvailableTags_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "File Tags";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(479, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 25);
            this.label2.TabIndex = 34;
            this.label2.Text = "Available Tags";
            // 
            // frmFileTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(914, 582);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbxFilterTagName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAvailableTags);
            this.Controls.Add(this.dgvAssignedTags);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFileTags";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Tags";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTags)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvAssignedTags;
        private System.Windows.Forms.DataGridView dgvAvailableTags;
        private System.Windows.Forms.TextBox tbxFilterTagName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}