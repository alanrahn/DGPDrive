namespace DGPDrive.Screens.Security
{
    partial class ucDataGroup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDataGroup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsForm = new System.Windows.Forms.ToolStrip();
            this.tslblTitle = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslblSearchFld = new System.Windows.Forms.ToolStripLabel();
            this.tscmbSearchCol = new System.Windows.Forms.ToolStripComboBox();
            this.tslblSearchVal = new System.Windows.Forms.ToolStripLabel();
            this.tstbxSearchVal = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.tscmbState = new System.Windows.Forms.ToolStripComboBox();
            this.tscmbSortOrder = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.txbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnLast = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNext = new System.Windows.Forms.ToolStripButton();
            this.tslblPages = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPrev = new System.Windows.Forms.ToolStripButton();
            this.tsbtnFirst = new System.Windows.Forms.ToolStripButton();
            this.dgvDataGroups = new System.Windows.Forms.DataGridView();
            this.ctxmnGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoverDeletedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataGroups)).BeginInit();
            this.ctxmnGrid.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsForm
            // 
            this.tsForm.AllowMerge = false;
            this.tsForm.BackColor = System.Drawing.Color.DarkGray;
            this.tsForm.CanOverflow = false;
            this.tsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsForm.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsForm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsForm.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblTitle,
            this.toolStripSeparator1,
            this.tslblSearchFld,
            this.tscmbSearchCol,
            this.tslblSearchVal,
            this.tstbxSearchVal,
            this.tsbtnSearch,
            this.tscmbState,
            this.tscmbSortOrder,
            this.tsbtnClear,
            this.txbtnNew,
            this.tsbtnLast,
            this.tsbtnNext,
            this.tslblPages,
            this.tsbtnPrev,
            this.tsbtnFirst});
            this.tsForm.Location = new System.Drawing.Point(0, 0);
            this.tsForm.Name = "tsForm";
            this.tsForm.Padding = new System.Windows.Forms.Padding(1);
            this.tsForm.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsForm.Size = new System.Drawing.Size(1100, 30);
            this.tsForm.TabIndex = 1;
            // 
            // tslblTitle
            // 
            this.tslblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tslblTitle.ForeColor = System.Drawing.Color.Black;
            this.tslblTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tslblTitle.Name = "tslblTitle";
            this.tslblTitle.Padding = new System.Windows.Forms.Padding(1);
            this.tslblTitle.Size = new System.Drawing.Size(84, 24);
            this.tslblTitle.Text = "  Data Groups";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // tslblSearchFld
            // 
            this.tslblSearchFld.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.tslblSearchFld.ForeColor = System.Drawing.Color.Black;
            this.tslblSearchFld.Margin = new System.Windows.Forms.Padding(2);
            this.tslblSearchFld.Name = "tslblSearchFld";
            this.tslblSearchFld.Padding = new System.Windows.Forms.Padding(1);
            this.tslblSearchFld.Size = new System.Drawing.Size(72, 24);
            this.tslblSearchFld.Text = "Search Field";
            // 
            // tscmbSearchCol
            // 
            this.tscmbSearchCol.BackColor = System.Drawing.Color.Silver;
            this.tscmbSearchCol.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscmbSearchCol.ForeColor = System.Drawing.Color.Black;
            this.tscmbSearchCol.Items.AddRange(new object[] {
            "GroupName"});
            this.tscmbSearchCol.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbSearchCol.Name = "tscmbSearchCol";
            this.tscmbSearchCol.Padding = new System.Windows.Forms.Padding(1);
            this.tscmbSearchCol.Size = new System.Drawing.Size(87, 24);
            this.tscmbSearchCol.SelectedIndexChanged += new System.EventHandler(this.tscmbSearchCol_SelectedIndexChanged);
            // 
            // tslblSearchVal
            // 
            this.tslblSearchVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.tslblSearchVal.ForeColor = System.Drawing.Color.Black;
            this.tslblSearchVal.Margin = new System.Windows.Forms.Padding(2);
            this.tslblSearchVal.Name = "tslblSearchVal";
            this.tslblSearchVal.Padding = new System.Windows.Forms.Padding(1);
            this.tslblSearchVal.Size = new System.Drawing.Size(40, 24);
            this.tslblSearchVal.Text = "Value";
            // 
            // tstbxSearchVal
            // 
            this.tstbxSearchVal.BackColor = System.Drawing.Color.Silver;
            this.tstbxSearchVal.ForeColor = System.Drawing.Color.Black;
            this.tstbxSearchVal.Margin = new System.Windows.Forms.Padding(2);
            this.tstbxSearchVal.Name = "tstbxSearchVal";
            this.tstbxSearchVal.Padding = new System.Windows.Forms.Padding(1);
            this.tstbxSearchVal.Size = new System.Drawing.Size(78, 24);
            // 
            // tsbtnSearch
            // 
            this.tsbtnSearch.AutoToolTip = false;
            this.tsbtnSearch.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnSearch.ForeColor = System.Drawing.Color.Black;
            this.tsbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearch.Image")));
            this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearch.Margin = new System.Windows.Forms.Padding(20, 5, 20, 5);
            this.tsbtnSearch.Name = "tsbtnSearch";
            this.tsbtnSearch.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnSearch.Size = new System.Drawing.Size(48, 18);
            this.tsbtnSearch.Text = "Search";
            this.tsbtnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
            // 
            // tscmbState
            // 
            this.tscmbState.BackColor = System.Drawing.Color.Silver;
            this.tscmbState.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscmbState.ForeColor = System.Drawing.Color.Black;
            this.tscmbState.Items.AddRange(new object[] {
            "ACTIVE",
            "DELETED"});
            this.tscmbState.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbState.Name = "tscmbState";
            this.tscmbState.Padding = new System.Windows.Forms.Padding(1);
            this.tscmbState.Size = new System.Drawing.Size(75, 24);
            this.tscmbState.SelectedIndexChanged += new System.EventHandler(this.tscmbState_SelectedIndexChanged);
            // 
            // tscmbSortOrder
            // 
            this.tscmbSortOrder.BackColor = System.Drawing.Color.Silver;
            this.tscmbSortOrder.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscmbSortOrder.ForeColor = System.Drawing.Color.Black;
            this.tscmbSortOrder.Items.AddRange(new object[] {
            "ASC",
            "DESC"});
            this.tscmbSortOrder.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbSortOrder.Name = "tscmbSortOrder";
            this.tscmbSortOrder.Padding = new System.Windows.Forms.Padding(1);
            this.tscmbSortOrder.Size = new System.Drawing.Size(75, 24);
            this.tscmbSortOrder.SelectedIndexChanged += new System.EventHandler(this.tscmbSortOrder_SelectedIndexChanged);
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnClear.ForeColor = System.Drawing.Color.Black;
            this.tsbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClear.Image")));
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Margin = new System.Windows.Forms.Padding(10, 5, 1, 5);
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnClear.Size = new System.Drawing.Size(40, 18);
            this.tsbtnClear.Text = "Clear";
            this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
            // 
            // txbtnNew
            // 
            this.txbtnNew.AutoToolTip = false;
            this.txbtnNew.BackColor = System.Drawing.Color.Gainsboro;
            this.txbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.txbtnNew.ForeColor = System.Drawing.Color.Black;
            this.txbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("txbtnNew.Image")));
            this.txbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txbtnNew.Margin = new System.Windows.Forms.Padding(20, 5, 1, 5);
            this.txbtnNew.Name = "txbtnNew";
            this.txbtnNew.Padding = new System.Windows.Forms.Padding(1);
            this.txbtnNew.Size = new System.Drawing.Size(37, 18);
            this.txbtnNew.Text = "New";
            this.txbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.txbtnNew.Click += new System.EventHandler(this.txbtnNew_Click);
            // 
            // tsbtnLast
            // 
            this.tsbtnLast.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnLast.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnLast.ForeColor = System.Drawing.Color.Black;
            this.tsbtnLast.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLast.Image")));
            this.tsbtnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLast.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnLast.Name = "tsbtnLast";
            this.tsbtnLast.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnLast.Size = new System.Drawing.Size(24, 18);
            this.tsbtnLast.Text = ">|";
            this.tsbtnLast.Click += new System.EventHandler(this.tsbtnLast_Click);
            // 
            // tsbtnNext
            // 
            this.tsbtnNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnNext.ForeColor = System.Drawing.Color.Black;
            this.tsbtnNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNext.Image")));
            this.tsbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNext.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnNext.Name = "tsbtnNext";
            this.tsbtnNext.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnNext.Size = new System.Drawing.Size(23, 18);
            this.tsbtnNext.Text = ">";
            this.tsbtnNext.Click += new System.EventHandler(this.tsbtnNext_Click);
            // 
            // tslblPages
            // 
            this.tslblPages.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslblPages.ForeColor = System.Drawing.Color.Black;
            this.tslblPages.Margin = new System.Windows.Forms.Padding(2);
            this.tslblPages.Name = "tslblPages";
            this.tslblPages.Padding = new System.Windows.Forms.Padding(1);
            this.tslblPages.Size = new System.Drawing.Size(67, 24);
            this.tslblPages.Text = "Page 0 of 0";
            // 
            // tsbtnPrev
            // 
            this.tsbtnPrev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnPrev.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnPrev.ForeColor = System.Drawing.Color.Black;
            this.tsbtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrev.Image")));
            this.tsbtnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrev.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnPrev.Name = "tsbtnPrev";
            this.tsbtnPrev.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnPrev.Size = new System.Drawing.Size(23, 18);
            this.tsbtnPrev.Text = "<";
            this.tsbtnPrev.Click += new System.EventHandler(this.tsbtnPrev_Click);
            // 
            // tsbtnFirst
            // 
            this.tsbtnFirst.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnFirst.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnFirst.ForeColor = System.Drawing.Color.Black;
            this.tsbtnFirst.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFirst.Image")));
            this.tsbtnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFirst.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnFirst.Name = "tsbtnFirst";
            this.tsbtnFirst.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnFirst.Size = new System.Drawing.Size(27, 18);
            this.tsbtnFirst.Text = "| <";
            this.tsbtnFirst.Click += new System.EventHandler(this.tsbtnFirst_Click);
            // 
            // dgvDataGroups
            // 
            this.dgvDataGroups.AllowUserToAddRows = false;
            this.dgvDataGroups.AllowUserToDeleteRows = false;
            this.dgvDataGroups.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDataGroups.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDataGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDataGroups.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDataGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataGroups.ContextMenuStrip = this.ctxmnGrid;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDataGroups.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDataGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataGroups.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDataGroups.EnableHeadersVisualStyles = false;
            this.dgvDataGroups.GridColor = System.Drawing.Color.Black;
            this.dgvDataGroups.Location = new System.Drawing.Point(0, 30);
            this.dgvDataGroups.Margin = new System.Windows.Forms.Padding(0);
            this.dgvDataGroups.MultiSelect = false;
            this.dgvDataGroups.Name = "dgvDataGroups";
            this.dgvDataGroups.ReadOnly = true;
            this.dgvDataGroups.RowHeadersWidth = 10;
            this.dgvDataGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataGroups.ShowEditingIcon = false;
            this.dgvDataGroups.Size = new System.Drawing.Size(1100, 620);
            this.dgvDataGroups.TabIndex = 4;
            this.dgvDataGroups.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDataGroups_CellMouseUp);
            // 
            // ctxmnGrid
            // 
            this.ctxmnGrid.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxmnGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMenuItem,
            this.viewHistoryMenuItem,
            this.recoverDeletedMenuItem});
            this.ctxmnGrid.Name = "ctxmnApiMethod";
            this.ctxmnGrid.Size = new System.Drawing.Size(160, 70);
            // 
            // editMenuItem
            // 
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(159, 22);
            this.editMenuItem.Text = "Edit...";
            this.editMenuItem.Click += new System.EventHandler(this.editMenuItem_Click);
            // 
            // viewHistoryMenuItem
            // 
            this.viewHistoryMenuItem.Name = "viewHistoryMenuItem";
            this.viewHistoryMenuItem.Size = new System.Drawing.Size(159, 22);
            this.viewHistoryMenuItem.Text = "View History...";
            this.viewHistoryMenuItem.Click += new System.EventHandler(this.viewHistoryMenuItem_Click);
            // 
            // recoverDeletedMenuItem
            // 
            this.recoverDeletedMenuItem.Enabled = false;
            this.recoverDeletedMenuItem.Name = "recoverDeletedMenuItem";
            this.recoverDeletedMenuItem.Size = new System.Drawing.Size(159, 22);
            this.recoverDeletedMenuItem.Text = "Recover Deleted";
            this.recoverDeletedMenuItem.Click += new System.EventHandler(this.recoverDeletedMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDataGroups, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsForm, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1100, 650);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // ucDataGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucDataGroup";
            this.Size = new System.Drawing.Size(1100, 650);
            this.tsForm.ResumeLayout(false);
            this.tsForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataGroups)).EndInit();
            this.ctxmnGrid.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsForm;
        private System.Windows.Forms.ToolStripLabel tslblTitle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslblSearchFld;
        private System.Windows.Forms.ToolStripComboBox tscmbSearchCol;
        private System.Windows.Forms.ToolStripLabel tslblSearchVal;
        private System.Windows.Forms.ToolStripTextBox tstbxSearchVal;
        private System.Windows.Forms.ToolStripButton tsbtnSearch;
        private System.Windows.Forms.ToolStripButton txbtnNew;
        private System.Windows.Forms.DataGridView dgvDataGroups;
        private System.Windows.Forms.ToolStripButton tsbtnFirst;
        private System.Windows.Forms.ToolStripButton tsbtnPrev;
        private System.Windows.Forms.ToolStripLabel tslblPages;
        private System.Windows.Forms.ToolStripButton tsbtnNext;
        private System.Windows.Forms.ToolStripButton tsbtnLast;
        private System.Windows.Forms.ToolStripComboBox tscmbSortOrder;
        private System.Windows.Forms.ContextMenuStrip ctxmnGrid;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripComboBox tscmbState;
        private System.Windows.Forms.ToolStripMenuItem viewHistoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recoverDeletedMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
