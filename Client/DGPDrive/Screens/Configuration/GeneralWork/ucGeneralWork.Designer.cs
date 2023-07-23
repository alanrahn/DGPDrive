namespace DGPDrive.Screens.Configuration
{
    partial class ucGeneralWork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGeneralWork));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsForm = new System.Windows.Forms.ToolStrip();
            this.tslblTitle = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslblSearchFld = new System.Windows.Forms.ToolStripLabel();
            this.tstbxSearchVal = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.tscmbPageSize = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnLast = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNext = new System.Windows.Forms.ToolStripButton();
            this.tslblPages = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPrev = new System.Windows.Forms.ToolStripButton();
            this.tsbtnFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvGeneralWork = new System.Windows.Forms.DataGridView();
            this.ctxmnGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneralWork)).BeginInit();
            this.ctxmnGrid.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsForm
            // 
            this.tsForm.AllowMerge = false;
            this.tsForm.AutoSize = false;
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
            this.tstbxSearchVal,
            this.tsbtnSearch,
            this.tscmbPageSize,
            this.tsbtnClear,
            this.tsbtnNew,
            this.tsbtnLast,
            this.tsbtnNext,
            this.tslblPages,
            this.tsbtnPrev,
            this.tsbtnFirst,
            this.toolStripSeparator6});
            this.tsForm.Location = new System.Drawing.Point(1, 1);
            this.tsForm.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tsForm.Name = "tsForm";
            this.tsForm.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tsForm.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsForm.Size = new System.Drawing.Size(1098, 28);
            this.tsForm.TabIndex = 1;
            // 
            // tslblTitle
            // 
            this.tslblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tslblTitle.ForeColor = System.Drawing.Color.Black;
            this.tslblTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tslblTitle.Name = "tslblTitle";
            this.tslblTitle.Padding = new System.Windows.Forms.Padding(1);
            this.tslblTitle.Size = new System.Drawing.Size(130, 22);
            this.tslblTitle.Text = "  GeneralWork Queue";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 24);
            // 
            // tslblSearchFld
            // 
            this.tslblSearchFld.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.tslblSearchFld.ForeColor = System.Drawing.Color.Black;
            this.tslblSearchFld.Margin = new System.Windows.Forms.Padding(2);
            this.tslblSearchFld.Name = "tslblSearchFld";
            this.tslblSearchFld.Padding = new System.Windows.Forms.Padding(1);
            this.tslblSearchFld.Size = new System.Drawing.Size(72, 22);
            this.tslblSearchFld.Text = "SrcDBName";
            // 
            // tstbxSearchVal
            // 
            this.tstbxSearchVal.BackColor = System.Drawing.Color.LightGray;
            this.tstbxSearchVal.ForeColor = System.Drawing.Color.Black;
            this.tstbxSearchVal.Margin = new System.Windows.Forms.Padding(2);
            this.tstbxSearchVal.Name = "tstbxSearchVal";
            this.tstbxSearchVal.Padding = new System.Windows.Forms.Padding(1);
            this.tstbxSearchVal.Size = new System.Drawing.Size(78, 22);
            // 
            // tsbtnSearch
            // 
            this.tsbtnSearch.AutoToolTip = false;
            this.tsbtnSearch.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnSearch.ForeColor = System.Drawing.Color.Black;
            this.tsbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearch.Image")));
            this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearch.Margin = new System.Windows.Forms.Padding(20, 5, 20, 5);
            this.tsbtnSearch.Name = "tsbtnSearch";
            this.tsbtnSearch.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnSearch.Size = new System.Drawing.Size(48, 16);
            this.tsbtnSearch.Text = "Search";
            this.tsbtnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnClear.ForeColor = System.Drawing.Color.Black;
            this.tsbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClear.Image")));
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Margin = new System.Windows.Forms.Padding(10, 5, 2, 5);
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(38, 16);
            this.tsbtnClear.Text = "Clear";
            this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
            // 
            // tscmbPageSize
            // 
            this.tscmbPageSize.BackColor = System.Drawing.Color.LightGray;
            this.tscmbPageSize.ForeColor = System.Drawing.Color.Black;
            this.tscmbPageSize.Items.AddRange(new object[] {
            "25",
            "50"});
            this.tscmbPageSize.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbPageSize.Name = "tscmbPageSize";
            this.tscmbPageSize.Size = new System.Drawing.Size(75, 22);
            this.tscmbPageSize.SelectedIndexChanged += new System.EventHandler(this.tscmbPageSize_SelectedIndexChanged);
            // 
            // tsbtnNew
            // 
            this.tsbtnNew.AutoToolTip = false;
            this.tsbtnNew.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnNew.ForeColor = System.Drawing.Color.Black;
            this.tsbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNew.Image")));
            this.tsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNew.Margin = new System.Windows.Forms.Padding(20, 5, 2, 5);
            this.tsbtnNew.Name = "tsbtnNew";
            this.tsbtnNew.Size = new System.Drawing.Size(35, 16);
            this.tsbtnNew.Text = "New";
            this.tsbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbtnNew.Click += new System.EventHandler(this.txbtnNew_Click);
            // 
            // tsbtnLast
            // 
            this.tsbtnLast.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnLast.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnLast.ForeColor = System.Drawing.Color.Black;
            this.tsbtnLast.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLast.Image")));
            this.tsbtnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLast.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnLast.Name = "tsbtnLast";
            this.tsbtnLast.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnLast.Size = new System.Drawing.Size(24, 16);
            this.tsbtnLast.Text = ">|";
            this.tsbtnLast.Click += new System.EventHandler(this.tsbtnLast_Click);
            // 
            // tsbtnNext
            // 
            this.tsbtnNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnNext.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnNext.ForeColor = System.Drawing.Color.Black;
            this.tsbtnNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNext.Image")));
            this.tsbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNext.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnNext.Name = "tsbtnNext";
            this.tsbtnNext.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnNext.Size = new System.Drawing.Size(23, 16);
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
            this.tslblPages.Size = new System.Drawing.Size(67, 22);
            this.tslblPages.Text = "Page 0 of 0";
            // 
            // tsbtnPrev
            // 
            this.tsbtnPrev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnPrev.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnPrev.ForeColor = System.Drawing.Color.Black;
            this.tsbtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrev.Image")));
            this.tsbtnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrev.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnPrev.Name = "tsbtnPrev";
            this.tsbtnPrev.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnPrev.Size = new System.Drawing.Size(23, 16);
            this.tsbtnPrev.Text = "<";
            this.tsbtnPrev.Click += new System.EventHandler(this.tsbtnPrev_Click);
            // 
            // tsbtnFirst
            // 
            this.tsbtnFirst.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnFirst.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnFirst.ForeColor = System.Drawing.Color.Black;
            this.tsbtnFirst.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFirst.Image")));
            this.tsbtnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFirst.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnFirst.Name = "tsbtnFirst";
            this.tsbtnFirst.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnFirst.Size = new System.Drawing.Size(27, 16);
            this.tsbtnFirst.Text = "| <";
            this.tsbtnFirst.Click += new System.EventHandler(this.tsbtnFirst_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 24);
            // 
            // dgvGeneralWork
            // 
            this.dgvGeneralWork.AllowUserToAddRows = false;
            this.dgvGeneralWork.AllowUserToDeleteRows = false;
            this.dgvGeneralWork.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvGeneralWork.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGeneralWork.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvGeneralWork.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGeneralWork.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGeneralWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGeneralWork.ContextMenuStrip = this.ctxmnGrid;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGeneralWork.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGeneralWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGeneralWork.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGeneralWork.EnableHeadersVisualStyles = false;
            this.dgvGeneralWork.GridColor = System.Drawing.Color.Black;
            this.dgvGeneralWork.Location = new System.Drawing.Point(0, 30);
            this.dgvGeneralWork.Margin = new System.Windows.Forms.Padding(0);
            this.dgvGeneralWork.MultiSelect = false;
            this.dgvGeneralWork.Name = "dgvGeneralWork";
            this.dgvGeneralWork.ReadOnly = true;
            this.dgvGeneralWork.RowHeadersWidth = 10;
            this.dgvGeneralWork.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGeneralWork.ShowEditingIcon = false;
            this.dgvGeneralWork.Size = new System.Drawing.Size(1100, 620);
            this.dgvGeneralWork.TabIndex = 4;
            this.dgvGeneralWork.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGeneralWork_CellMouseUp);
            // 
            // ctxmnGrid
            // 
            this.ctxmnGrid.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxmnGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMenuItem,
            this.cloneMenuItem});
            this.ctxmnGrid.Name = "ctxmnApiMethod";
            this.ctxmnGrid.Size = new System.Drawing.Size(115, 48);
            // 
            // editMenuItem
            // 
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(114, 22);
            this.editMenuItem.Text = "Edit...";
            this.editMenuItem.Click += new System.EventHandler(this.editMenuItem_Click);
            // 
            // cloneMenuItem
            // 
            this.cloneMenuItem.Name = "cloneMenuItem";
            this.cloneMenuItem.Size = new System.Drawing.Size(114, 22);
            this.cloneMenuItem.Text = "Clone...";
            this.cloneMenuItem.Click += new System.EventHandler(this.cloneMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tsForm, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvGeneralWork, 0, 1);
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
            // ucGeneralWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucGeneralWork";
            this.Size = new System.Drawing.Size(1100, 650);
            this.tsForm.ResumeLayout(false);
            this.tsForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneralWork)).EndInit();
            this.ctxmnGrid.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsForm;
        private System.Windows.Forms.ToolStripLabel tslblTitle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslblSearchFld;
        private System.Windows.Forms.ToolStripTextBox tstbxSearchVal;
        private System.Windows.Forms.ToolStripButton tsbtnSearch;
        private System.Windows.Forms.ToolStripButton tsbtnNew;
        private System.Windows.Forms.DataGridView dgvGeneralWork;
        private System.Windows.Forms.ToolStripButton tsbtnFirst;
        private System.Windows.Forms.ToolStripButton tsbtnPrev;
        private System.Windows.Forms.ToolStripLabel tslblPages;
        private System.Windows.Forms.ToolStripButton tsbtnNext;
        private System.Windows.Forms.ToolStripButton tsbtnLast;
        private System.Windows.Forms.ToolStripComboBox tscmbPageSize;
        private System.Windows.Forms.ContextMenuStrip ctxmnGrid;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem cloneMenuItem;
    }
}
