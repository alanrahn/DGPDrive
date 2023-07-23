namespace DGPDrive.Screens.Testing
{
    partial class ucApiTestHarness
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucApiTestHarness));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsForm = new System.Windows.Forms.ToolStrip();
            this.tslblTitle = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnBrowse = new System.Windows.Forms.ToolStripButton();
            this.tstbxDirPath = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnUpload = new System.Windows.Forms.ToolStripButton();
            this.tscmbTestType = new System.Windows.Forms.ToolStripComboBox();
            this.tscmbIterations = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnRunTests = new System.Windows.Forms.ToolStripButton();
            this.tspbTestRun = new System.Windows.Forms.ToolStripProgressBar();
            this.dgvTestBatchFiles = new System.Windows.Forms.DataGridView();
            this.ctxmnGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tstbxLoopCount = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnStopRun = new System.Windows.Forms.ToolStripButton();
            this.tsForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestBatchFiles)).BeginInit();
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
            this.tsbtnBrowse,
            this.tstbxDirPath,
            this.tsbtnUpload,
            this.tscmbTestType,
            this.tscmbIterations,
            this.tsbtnRunTests,
            this.tspbTestRun,
            this.tstbxLoopCount,
            this.tsbtnStopRun});
            this.tsForm.Location = new System.Drawing.Point(0, 0);
            this.tsForm.Name = "tsForm";
            this.tsForm.Padding = new System.Windows.Forms.Padding(2);
            this.tsForm.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsForm.Size = new System.Drawing.Size(2200, 60);
            this.tsForm.TabIndex = 1;
            // 
            // tslblTitle
            // 
            this.tslblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tslblTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tslblTitle.Name = "tslblTitle";
            this.tslblTitle.Padding = new System.Windows.Forms.Padding(1);
            this.tslblTitle.Size = new System.Drawing.Size(146, 52);
            this.tslblTitle.Text = "  API Tester";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnBrowse
            // 
            this.tsbtnBrowse.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnBrowse.ForeColor = System.Drawing.Color.Black;
            this.tsbtnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnBrowse.Image")));
            this.tsbtnBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnBrowse.Margin = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.tsbtnBrowse.Name = "tsbtnBrowse";
            this.tsbtnBrowse.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnBrowse.Size = new System.Drawing.Size(97, 46);
            this.tsbtnBrowse.Text = "Browse";
            this.tsbtnBrowse.Click += new System.EventHandler(this.tsbtnBrowse_Click);
            // 
            // tstbxDirPath
            // 
            this.tstbxDirPath.AutoSize = false;
            this.tstbxDirPath.BackColor = System.Drawing.Color.Silver;
            this.tstbxDirPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbxDirPath.ForeColor = System.Drawing.Color.Black;
            this.tstbxDirPath.Margin = new System.Windows.Forms.Padding(2);
            this.tstbxDirPath.Name = "tstbxDirPath";
            this.tstbxDirPath.Padding = new System.Windows.Forms.Padding(1);
            this.tstbxDirPath.ReadOnly = true;
            this.tstbxDirPath.Size = new System.Drawing.Size(600, 39);
            // 
            // tsbtnUpload
            // 
            this.tsbtnUpload.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnUpload.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUpload.Image")));
            this.tsbtnUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpload.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.tsbtnUpload.Name = "tsbtnUpload";
            this.tsbtnUpload.Size = new System.Drawing.Size(141, 46);
            this.tsbtnUpload.Text = "Upload: Off";
            this.tsbtnUpload.Click += new System.EventHandler(this.tsbtnUpload_Click);
            // 
            // tscmbTestType
            // 
            this.tscmbTestType.BackColor = System.Drawing.Color.Gainsboro;
            this.tscmbTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscmbTestType.DropDownWidth = 50;
            this.tscmbTestType.Items.AddRange(new object[] {
            "ALL",
            "POS",
            "NEG"});
            this.tscmbTestType.Margin = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.tscmbTestType.Name = "tscmbTestType";
            this.tscmbTestType.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tscmbTestType.Size = new System.Drawing.Size(146, 52);
            this.tscmbTestType.SelectedIndexChanged += new System.EventHandler(this.tscmbTestType_SelectedIndexChanged);
            // 
            // tscmbIterations
            // 
            this.tscmbIterations.BackColor = System.Drawing.Color.Gainsboro;
            this.tscmbIterations.Items.AddRange(new object[] {
            "1",
            "10",
            "100",
            "1000"});
            this.tscmbIterations.Margin = new System.Windows.Forms.Padding(10, 0, 2, 0);
            this.tscmbIterations.Name = "tscmbIterations";
            this.tscmbIterations.Size = new System.Drawing.Size(100, 56);
            // 
            // tsbtnRunTests
            // 
            this.tsbtnRunTests.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnRunTests.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnRunTests.ForeColor = System.Drawing.Color.Black;
            this.tsbtnRunTests.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRunTests.Image")));
            this.tsbtnRunTests.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRunTests.Margin = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.tsbtnRunTests.Name = "tsbtnRunTests";
            this.tsbtnRunTests.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnRunTests.Size = new System.Drawing.Size(122, 46);
            this.tsbtnRunTests.Text = "Run Tests";
            this.tsbtnRunTests.Click += new System.EventHandler(this.tsbtnRunTests_Click);
            // 
            // tspbTestRun
            // 
            this.tspbTestRun.AutoSize = false;
            this.tspbTestRun.Margin = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.tspbTestRun.Name = "tspbTestRun";
            this.tspbTestRun.Size = new System.Drawing.Size(200, 26);
            this.tspbTestRun.Step = 1;
            // 
            // dgvTestBatchFiles
            // 
            this.dgvTestBatchFiles.AllowUserToAddRows = false;
            this.dgvTestBatchFiles.AllowUserToDeleteRows = false;
            this.dgvTestBatchFiles.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTestBatchFiles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTestBatchFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestBatchFiles.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTestBatchFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTestBatchFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestBatchFiles.ContextMenuStrip = this.ctxmnGrid;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTestBatchFiles.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTestBatchFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTestBatchFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTestBatchFiles.EnableHeadersVisualStyles = false;
            this.dgvTestBatchFiles.Location = new System.Drawing.Point(0, 60);
            this.dgvTestBatchFiles.Margin = new System.Windows.Forms.Padding(0);
            this.dgvTestBatchFiles.Name = "dgvTestBatchFiles";
            this.dgvTestBatchFiles.ReadOnly = true;
            this.dgvTestBatchFiles.RowHeadersWidth = 10;
            this.dgvTestBatchFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTestBatchFiles.ShowEditingIcon = false;
            this.dgvTestBatchFiles.Size = new System.Drawing.Size(2200, 1240);
            this.dgvTestBatchFiles.TabIndex = 4;
            this.dgvTestBatchFiles.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTestBatchFiles_CellMouseUp);
            // 
            // ctxmnGrid
            // 
            this.ctxmnGrid.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxmnGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewfileMenuItem});
            this.ctxmnGrid.Name = "ctxmnApiMethod";
            this.ctxmnGrid.Size = new System.Drawing.Size(201, 42);
            // 
            // viewfileMenuItem
            // 
            this.viewfileMenuItem.Name = "viewfileMenuItem";
            this.viewfileMenuItem.Size = new System.Drawing.Size(200, 38);
            this.viewfileMenuItem.Text = "View File...";
            this.viewfileMenuItem.Click += new System.EventHandler(this.viewfileMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tsForm, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvTestBatchFiles, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2200, 1300);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tstbxLoopCount
            // 
            this.tstbxLoopCount.BackColor = System.Drawing.Color.Silver;
            this.tstbxLoopCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbxLoopCount.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.tstbxLoopCount.Name = "tstbxLoopCount";
            this.tstbxLoopCount.Padding = new System.Windows.Forms.Padding(1);
            this.tstbxLoopCount.Size = new System.Drawing.Size(100, 52);
            this.tstbxLoopCount.Text = "0";
            // 
            // tsbtnStopRun
            // 
            this.tsbtnStopRun.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnStopRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnStopRun.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStopRun.Image")));
            this.tsbtnStopRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStopRun.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.tsbtnStopRun.Name = "tsbtnStopRun";
            this.tsbtnStopRun.Padding = new System.Windows.Forms.Padding(1);
            this.tsbtnStopRun.Size = new System.Drawing.Size(118, 52);
            this.tsbtnStopRun.Text = "Stop Run";
            this.tsbtnStopRun.Click += new System.EventHandler(this.tsbtnStopRun_Click);
            // 
            // ucApiTestHarness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucApiTestHarness";
            this.Size = new System.Drawing.Size(2200, 1300);
            this.tsForm.ResumeLayout(false);
            this.tsForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestBatchFiles)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvTestBatchFiles;
        private System.Windows.Forms.ContextMenuStrip ctxmnGrid;
        private System.Windows.Forms.ToolStripMenuItem viewfileMenuItem;
        private System.Windows.Forms.ToolStripButton tsbtnBrowse;
        private System.Windows.Forms.ToolStripTextBox tstbxDirPath;
        private System.Windows.Forms.ToolStripButton tsbtnRunTests;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton tsbtnUpload;
        private System.Windows.Forms.ToolStripProgressBar tspbTestRun;
        private System.Windows.Forms.ToolStripComboBox tscmbTestType;
        private System.Windows.Forms.ToolStripComboBox tscmbIterations;
        private System.Windows.Forms.ToolStripTextBox tstbxLoopCount;
        private System.Windows.Forms.ToolStripButton tsbtnStopRun;
    }
}
