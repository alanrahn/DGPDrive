namespace DGPDrive.Screens.Testing
{
    partial class frmLatticeMetrics
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLatticeMetrics));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvLatticeMetrics = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnLast = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNext = new System.Windows.Forms.ToolStripButton();
            this.tslblPages = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPrev = new System.Windows.Forms.ToolStripButton();
            this.tsbtnFirst = new System.Windows.Forms.ToolStripButton();
            this.tslblSearchFld = new System.Windows.Forms.ToolStripLabel();
            this.tscmbSearchCol = new System.Windows.Forms.ToolStripComboBox();
            this.tslblSearchVal = new System.Windows.Forms.ToolStripLabel();
            this.tstbxSearchVal = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tscmbSortOrder = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLatticeMetrics)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvLatticeMetrics, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1774, 1129);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvLatticeMetrics
            // 
            this.dgvLatticeMetrics.AllowUserToAddRows = false;
            this.dgvLatticeMetrics.AllowUserToDeleteRows = false;
            this.dgvLatticeMetrics.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLatticeMetrics.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvLatticeMetrics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLatticeMetrics.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLatticeMetrics.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvLatticeMetrics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLatticeMetrics.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvLatticeMetrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLatticeMetrics.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLatticeMetrics.EnableHeadersVisualStyles = false;
            this.dgvLatticeMetrics.Location = new System.Drawing.Point(0, 60);
            this.dgvLatticeMetrics.Margin = new System.Windows.Forms.Padding(0);
            this.dgvLatticeMetrics.Name = "dgvLatticeMetrics";
            this.dgvLatticeMetrics.ReadOnly = true;
            this.dgvLatticeMetrics.RowHeadersWidth = 10;
            this.dgvLatticeMetrics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLatticeMetrics.ShowEditingIcon = false;
            this.dgvLatticeMetrics.Size = new System.Drawing.Size(1774, 1069);
            this.dgvLatticeMetrics.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnLast,
            this.tsbtnNext,
            this.tslblPages,
            this.tsbtnPrev,
            this.tsbtnFirst,
            this.tslblSearchFld,
            this.tscmbSearchCol,
            this.tslblSearchVal,
            this.tstbxSearchVal,
            this.tsbtnSearch,
            this.tsbtnRefresh,
            this.tscmbSortOrder,
            this.tsbtnClear,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1774, 60);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnLast
            // 
            this.tsbtnLast.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnLast.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnLast.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLast.Image")));
            this.tsbtnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLast.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnLast.Name = "tsbtnLast";
            this.tsbtnLast.Size = new System.Drawing.Size(46, 50);
            this.tsbtnLast.Text = ">|";
            this.tsbtnLast.Click += new System.EventHandler(this.tsbtnLast_Click);
            // 
            // tsbtnNext
            // 
            this.tsbtnNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNext.Image")));
            this.tsbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNext.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnNext.Name = "tsbtnNext";
            this.tsbtnNext.Size = new System.Drawing.Size(46, 50);
            this.tsbtnNext.Text = ">";
            this.tsbtnNext.Click += new System.EventHandler(this.tsbtnNext_Click);
            // 
            // tslblPages
            // 
            this.tslblPages.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslblPages.Margin = new System.Windows.Forms.Padding(2);
            this.tslblPages.Name = "tslblPages";
            this.tslblPages.Size = new System.Drawing.Size(135, 56);
            this.tslblPages.Text = "Page 0 of 0";
            // 
            // tsbtnPrev
            // 
            this.tsbtnPrev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnPrev.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrev.Image")));
            this.tsbtnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrev.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnPrev.Name = "tsbtnPrev";
            this.tsbtnPrev.Size = new System.Drawing.Size(46, 50);
            this.tsbtnPrev.Text = "<";
            this.tsbtnPrev.Click += new System.EventHandler(this.tsbtnPrev_Click);
            // 
            // tsbtnFirst
            // 
            this.tsbtnFirst.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnFirst.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnFirst.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFirst.Image")));
            this.tsbtnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFirst.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnFirst.Name = "tsbtnFirst";
            this.tsbtnFirst.Size = new System.Drawing.Size(48, 50);
            this.tsbtnFirst.Text = "| <";
            this.tsbtnFirst.Click += new System.EventHandler(this.tsbtnFirst_Click);
            // 
            // tslblSearchFld
            // 
            this.tslblSearchFld.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.tslblSearchFld.Margin = new System.Windows.Forms.Padding(2);
            this.tslblSearchFld.Name = "tslblSearchFld";
            this.tslblSearchFld.Size = new System.Drawing.Size(124, 56);
            this.tslblSearchFld.Text = "Filter Field";
            // 
            // tscmbSearchCol
            // 
            this.tscmbSearchCol.AutoSize = false;
            this.tscmbSearchCol.BackColor = System.Drawing.Color.Silver;
            this.tscmbSearchCol.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscmbSearchCol.Items.AddRange(new object[] {
            "AppName",
            "FormName",
            "WebSvcName"});
            this.tscmbSearchCol.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbSearchCol.Name = "tscmbSearchCol";
            this.tscmbSearchCol.Size = new System.Drawing.Size(200, 56);
            this.tscmbSearchCol.Click += new System.EventHandler(this.tscmbSearchCol_SelectedIndexChanged);
            // 
            // tslblSearchVal
            // 
            this.tslblSearchVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.tslblSearchVal.Margin = new System.Windows.Forms.Padding(2);
            this.tslblSearchVal.Name = "tslblSearchVal";
            this.tslblSearchVal.Size = new System.Drawing.Size(74, 56);
            this.tslblSearchVal.Text = "Value";
            // 
            // tstbxSearchVal
            // 
            this.tstbxSearchVal.AutoSize = false;
            this.tstbxSearchVal.BackColor = System.Drawing.Color.Silver;
            this.tstbxSearchVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbxSearchVal.Margin = new System.Windows.Forms.Padding(2);
            this.tstbxSearchVal.Name = "tstbxSearchVal";
            this.tstbxSearchVal.Size = new System.Drawing.Size(120, 56);
            // 
            // tsbtnSearch
            // 
            this.tsbtnSearch.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearch.Image")));
            this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearch.Margin = new System.Windows.Forms.Padding(20, 5, 2, 5);
            this.tsbtnSearch.Name = "tsbtnSearch";
            this.tsbtnSearch.Size = new System.Drawing.Size(98, 50);
            this.tsbtnSearch.Text = "Refresh";
            this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(46, 54);
            // 
            // tscmbSortOrder
            // 
            this.tscmbSortOrder.AutoSize = false;
            this.tscmbSortOrder.BackColor = System.Drawing.Color.Silver;
            this.tscmbSortOrder.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscmbSortOrder.Items.AddRange(new object[] {
            "DESC",
            "ASC"});
            this.tscmbSortOrder.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbSortOrder.Name = "tscmbSortOrder";
            this.tscmbSortOrder.Size = new System.Drawing.Size(120, 56);
            this.tscmbSortOrder.Click += new System.EventHandler(this.tscmbSortOrder_SelectedIndexChanged);
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClear.Image")));
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Margin = new System.Windows.Forms.Padding(20, 5, 2, 5);
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(73, 50);
            this.tsbtnClear.Text = "Clear";
            this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 54);
            // 
            // frmLatticeMetrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1774, 1129);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLatticeMetrics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lattice Metrics";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLatticeMetrics)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnFirst;
        private System.Windows.Forms.ToolStripButton tsbtnPrev;
        private System.Windows.Forms.ToolStripLabel tslblPages;
        private System.Windows.Forms.ToolStripButton tsbtnNext;
        private System.Windows.Forms.ToolStripButton tsbtnLast;
        private System.Windows.Forms.ToolStripComboBox tscmbSortOrder;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripLabel tslblSearchFld;
        private System.Windows.Forms.ToolStripComboBox tscmbSearchCol;
        private System.Windows.Forms.ToolStripLabel tslblSearchVal;
        private System.Windows.Forms.ToolStripTextBox tstbxSearchVal;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsbtnSearch;
        private System.Windows.Forms.DataGridView dgvLatticeMetrics;
    }
}