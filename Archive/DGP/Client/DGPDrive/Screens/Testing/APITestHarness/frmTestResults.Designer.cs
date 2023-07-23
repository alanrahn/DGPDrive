namespace DGPDrive.Screens.Testing
{
    partial class frmTestResults
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestResults));
            this.dgvTestResults = new System.Windows.Forms.DataGridView();
            this.ctxmnTestResults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.evalInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReqMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewRespMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTestResults = new System.Windows.Forms.ToolStrip();
            this.tsbtnSaveCSV = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tslblMethCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tslblPassed = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tslblFailed = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tslblUploaded = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tslblClient = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.tslblServer = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResults)).BeginInit();
            this.ctxmnTestResults.SuspendLayout();
            this.tsTestResults.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTestResults
            // 
            this.dgvTestResults.AllowUserToAddRows = false;
            this.dgvTestResults.AllowUserToDeleteRows = false;
            this.dgvTestResults.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTestResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTestResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvTestResults.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTestResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTestResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestResults.ContextMenuStrip = this.ctxmnTestResults;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTestResults.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTestResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTestResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvTestResults.EnableHeadersVisualStyles = false;
            this.dgvTestResults.Location = new System.Drawing.Point(4, 46);
            this.dgvTestResults.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dgvTestResults.MultiSelect = false;
            this.dgvTestResults.Name = "dgvTestResults";
            this.dgvTestResults.ReadOnly = true;
            this.dgvTestResults.RowHeadersWidth = 10;
            this.dgvTestResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTestResults.ShowEditingIcon = false;
            this.dgvTestResults.Size = new System.Drawing.Size(1760, 1070);
            this.dgvTestResults.TabIndex = 5;
            this.dgvTestResults.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTestResults_CellMouseUp);
            this.dgvTestResults.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvTestResults_DataBindingComplete);
            // 
            // ctxmnTestResults
            // 
            this.ctxmnTestResults.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxmnTestResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.evalInfoMenuItem,
            this.viewReqMenuItem,
            this.viewRespMenuItem});
            this.ctxmnTestResults.Name = "ctxmnTestResults";
            this.ctxmnTestResults.Size = new System.Drawing.Size(366, 118);
            // 
            // evalInfoMenuItem
            // 
            this.evalInfoMenuItem.Name = "evalInfoMenuItem";
            this.evalInfoMenuItem.Size = new System.Drawing.Size(365, 38);
            this.evalInfoMenuItem.Text = "View Eval Info...";
            this.evalInfoMenuItem.Click += new System.EventHandler(this.evalInfoMenuItem_Click);
            // 
            // viewReqMenuItem
            // 
            this.viewReqMenuItem.Name = "viewReqMenuItem";
            this.viewReqMenuItem.Size = new System.Drawing.Size(365, 38);
            this.viewReqMenuItem.Text = "View Request Message...";
            this.viewReqMenuItem.Click += new System.EventHandler(this.viewReqMenuItem_Click);
            // 
            // viewRespMenuItem
            // 
            this.viewRespMenuItem.Name = "viewRespMenuItem";
            this.viewRespMenuItem.Size = new System.Drawing.Size(365, 38);
            this.viewRespMenuItem.Text = "View Response Message...";
            this.viewRespMenuItem.Click += new System.EventHandler(this.viewRespMenuItem_Click);
            // 
            // tsTestResults
            // 
            this.tsTestResults.BackColor = System.Drawing.Color.DarkGray;
            this.tsTestResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsTestResults.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTestResults.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsTestResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSaveCSV,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tslblMethCount,
            this.toolStripSeparator3,
            this.toolStripLabel3,
            this.tslblPassed,
            this.toolStripSeparator4,
            this.toolStripLabel5,
            this.tslblFailed,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.tslblUploaded,
            this.toolStripSeparator5,
            this.toolStripLabel4,
            this.tslblClient,
            this.toolStripSeparator6,
            this.toolStripLabel6,
            this.tslblServer});
            this.tsTestResults.Location = new System.Drawing.Point(0, 0);
            this.tsTestResults.Name = "tsTestResults";
            this.tsTestResults.Padding = new System.Windows.Forms.Padding(0);
            this.tsTestResults.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsTestResults.Size = new System.Drawing.Size(1768, 40);
            this.tsTestResults.TabIndex = 9;
            // 
            // tsbtnSaveCSV
            // 
            this.tsbtnSaveCSV.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnSaveCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnSaveCSV.ForeColor = System.Drawing.Color.Black;
            this.tsbtnSaveCSV.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSaveCSV.Image")));
            this.tsbtnSaveCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveCSV.Margin = new System.Windows.Forms.Padding(2);
            this.tsbtnSaveCSV.Name = "tsbtnSaveCSV";
            this.tsbtnSaveCSV.Size = new System.Drawing.Size(148, 36);
            this.tsbtnSaveCSV.Text = "Save to CSV";
            this.tsbtnSaveCSV.Click += new System.EventHandler(this.tsbtnSaveCSV_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(168, 36);
            this.toolStripLabel1.Text = "Method Count:";
            // 
            // tslblMethCount
            // 
            this.tslblMethCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblMethCount.ForeColor = System.Drawing.Color.White;
            this.tslblMethCount.Margin = new System.Windows.Forms.Padding(2);
            this.tslblMethCount.Name = "tslblMethCount";
            this.tslblMethCount.Size = new System.Drawing.Size(28, 36);
            this.tslblMethCount.Text = "0";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(90, 36);
            this.toolStripLabel3.Text = "Passed:";
            // 
            // tslblPassed
            // 
            this.tslblPassed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblPassed.ForeColor = System.Drawing.Color.DarkGreen;
            this.tslblPassed.Margin = new System.Windows.Forms.Padding(2);
            this.tslblPassed.Name = "tslblPassed";
            this.tslblPassed.Size = new System.Drawing.Size(28, 36);
            this.tslblPassed.Text = "0";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.toolStripLabel5.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(82, 36);
            this.toolStripLabel5.Text = "Failed:";
            // 
            // tslblFailed
            // 
            this.tslblFailed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblFailed.ForeColor = System.Drawing.Color.Maroon;
            this.tslblFailed.Name = "tslblFailed";
            this.tslblFailed.Size = new System.Drawing.Size(28, 34);
            this.tslblFailed.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(120, 36);
            this.toolStripLabel2.Text = "Uploaded:";
            // 
            // tslblUploaded
            // 
            this.tslblUploaded.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblUploaded.ForeColor = System.Drawing.Color.Blue;
            this.tslblUploaded.Margin = new System.Windows.Forms.Padding(2);
            this.tslblUploaded.Name = "tslblUploaded";
            this.tslblUploaded.Size = new System.Drawing.Size(28, 36);
            this.tslblUploaded.Text = "0";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(151, 34);
            this.toolStripLabel4.Text = "ClientAvgMS:";
            // 
            // tslblClient
            // 
            this.tslblClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblClient.Name = "tslblClient";
            this.tslblClient.Size = new System.Drawing.Size(28, 34);
            this.tslblClient.Text = "0";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(156, 34);
            this.toolStripLabel6.Text = "ServerAvgMS:";
            // 
            // tslblServer
            // 
            this.tslblServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblServer.Name = "tslblServer";
            this.tslblServer.Size = new System.Drawing.Size(28, 34);
            this.tslblServer.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvTestResults, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsTestResults, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1768, 1122);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // frmTestResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1768, 1122);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTestResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Results";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResults)).EndInit();
            this.ctxmnTestResults.ResumeLayout(false);
            this.tsTestResults.ResumeLayout(false);
            this.tsTestResults.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTestResults;
        private System.Windows.Forms.ContextMenuStrip ctxmnTestResults;
        private System.Windows.Forms.ToolStrip tsTestResults;
        private System.Windows.Forms.ToolStripButton tsbtnSaveCSV;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem viewReqMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewRespMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evalInfoMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tslblMethCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel tslblPassed;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel tslblFailed;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tslblUploaded;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel tslblClient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel tslblServer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}