﻿namespace DGPDrive.Screens.Configuration
{
    partial class frmEditReplica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditReplica));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxGlobalID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxBatchSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSchemaType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxIntervalMS = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxNextRun = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbRunState = new System.Windows.Forms.ComboBox();
            this.tbxSchemaTable = new System.Windows.Forms.TextBox();
            this.tbxSrcURL = new System.Windows.Forms.TextBox();
            this.tbxDestURL = new System.Windows.Forms.TextBox();
            this.tbxSrcMethod = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxDestMethod = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbLogging = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxStartID = new System.Windows.Forms.TextBox();
            this.tbxSrcDBName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbWorkType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbxFinalID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxMaxDuration = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbNetwork = new System.Windows.Forms.ComboBox();
            this.tbxShardName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(802, 436);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 46);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.BackColor = System.Drawing.Color.LightGray;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(934, 436);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 46);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(1082, 436);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 46);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(30, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Global ID";
            // 
            // tbxGlobalID
            // 
            this.tbxGlobalID.BackColor = System.Drawing.Color.LightGray;
            this.tbxGlobalID.ForeColor = System.Drawing.Color.Black;
            this.tbxGlobalID.Location = new System.Drawing.Point(200, 24);
            this.tbxGlobalID.Margin = new System.Windows.Forms.Padding(6);
            this.tbxGlobalID.MaxLength = 45;
            this.tbxGlobalID.Name = "tbxGlobalID";
            this.tbxGlobalID.ReadOnly = true;
            this.tbxGlobalID.Size = new System.Drawing.Size(396, 20);
            this.tbxGlobalID.TabIndex = 0;
            this.tbxGlobalID.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(636, 129);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Batch Size*";
            // 
            // tbxBatchSize
            // 
            this.tbxBatchSize.BackColor = System.Drawing.Color.LightGray;
            this.tbxBatchSize.ForeColor = System.Drawing.Color.Black;
            this.tbxBatchSize.Location = new System.Drawing.Point(806, 126);
            this.tbxBatchSize.Margin = new System.Windows.Forms.Padding(6);
            this.tbxBatchSize.MaxLength = 10;
            this.tbxBatchSize.Name = "tbxBatchSize";
            this.tbxBatchSize.Size = new System.Drawing.Size(396, 20);
            this.tbxBatchSize.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(30, 181);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Schema Table*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(30, 384);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 70;
            this.label3.Text = "Source URL*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(30, 488);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 72;
            this.label5.Text = "Dest URL*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.LightGray;
            this.label6.Location = new System.Drawing.Point(30, 129);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "Schema Type*";
            // 
            // cmbSchemaType
            // 
            this.cmbSchemaType.BackColor = System.Drawing.Color.LightGray;
            this.cmbSchemaType.FormattingEnabled = true;
            this.cmbSchemaType.ItemHeight = 13;
            this.cmbSchemaType.Items.AddRange(new object[] {
            "REPLICA",
            "FILESHARD"});
            this.cmbSchemaType.Location = new System.Drawing.Point(200, 126);
            this.cmbSchemaType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSchemaType.Name = "cmbSchemaType";
            this.cmbSchemaType.Size = new System.Drawing.Size(396, 21);
            this.cmbSchemaType.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.LightGray;
            this.label7.Location = new System.Drawing.Point(636, 181);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 76;
            this.label7.Text = "Interval MS*";
            // 
            // tbxIntervalMS
            // 
            this.tbxIntervalMS.BackColor = System.Drawing.Color.LightGray;
            this.tbxIntervalMS.ForeColor = System.Drawing.Color.Black;
            this.tbxIntervalMS.Location = new System.Drawing.Point(806, 178);
            this.tbxIntervalMS.Margin = new System.Windows.Forms.Padding(6);
            this.tbxIntervalMS.MaxLength = 10;
            this.tbxIntervalMS.Name = "tbxIntervalMS";
            this.tbxIntervalMS.Size = new System.Drawing.Size(396, 20);
            this.tbxIntervalMS.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(636, 233);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 78;
            this.label8.Text = "Next Run";
            // 
            // tbxNextRun
            // 
            this.tbxNextRun.BackColor = System.Drawing.Color.LightGray;
            this.tbxNextRun.ForeColor = System.Drawing.Color.Black;
            this.tbxNextRun.Location = new System.Drawing.Point(806, 230);
            this.tbxNextRun.Margin = new System.Windows.Forms.Padding(6);
            this.tbxNextRun.MaxLength = 10;
            this.tbxNextRun.Name = "tbxNextRun";
            this.tbxNextRun.Size = new System.Drawing.Size(396, 20);
            this.tbxNextRun.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.LightGray;
            this.label10.Location = new System.Drawing.Point(636, 283);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 82;
            this.label10.Text = "Run State*";
            // 
            // cmbRunState
            // 
            this.cmbRunState.BackColor = System.Drawing.Color.LightGray;
            this.cmbRunState.FormattingEnabled = true;
            this.cmbRunState.ItemHeight = 13;
            this.cmbRunState.Items.AddRange(new object[] {
            "READY",
            "CLAIMED",
            "STOPPED",
            "DISABLED"});
            this.cmbRunState.Location = new System.Drawing.Point(806, 280);
            this.cmbRunState.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRunState.Name = "cmbRunState";
            this.cmbRunState.Size = new System.Drawing.Size(396, 21);
            this.cmbRunState.TabIndex = 16;
            // 
            // tbxSchemaTable
            // 
            this.tbxSchemaTable.BackColor = System.Drawing.Color.LightGray;
            this.tbxSchemaTable.ForeColor = System.Drawing.Color.Black;
            this.tbxSchemaTable.Location = new System.Drawing.Point(200, 178);
            this.tbxSchemaTable.Margin = new System.Windows.Forms.Padding(6);
            this.tbxSchemaTable.MaxLength = 50;
            this.tbxSchemaTable.Name = "tbxSchemaTable";
            this.tbxSchemaTable.Size = new System.Drawing.Size(396, 20);
            this.tbxSchemaTable.TabIndex = 3;
            // 
            // tbxSrcURL
            // 
            this.tbxSrcURL.BackColor = System.Drawing.Color.LightGray;
            this.tbxSrcURL.ForeColor = System.Drawing.Color.Black;
            this.tbxSrcURL.Location = new System.Drawing.Point(200, 381);
            this.tbxSrcURL.Margin = new System.Windows.Forms.Padding(6);
            this.tbxSrcURL.MaxLength = 100;
            this.tbxSrcURL.Name = "tbxSrcURL";
            this.tbxSrcURL.Size = new System.Drawing.Size(396, 20);
            this.tbxSrcURL.TabIndex = 7;
            // 
            // tbxDestURL
            // 
            this.tbxDestURL.BackColor = System.Drawing.Color.LightGray;
            this.tbxDestURL.ForeColor = System.Drawing.Color.Black;
            this.tbxDestURL.Location = new System.Drawing.Point(200, 485);
            this.tbxDestURL.Margin = new System.Windows.Forms.Padding(6);
            this.tbxDestURL.MaxLength = 100;
            this.tbxDestURL.Name = "tbxDestURL";
            this.tbxDestURL.Size = new System.Drawing.Size(396, 20);
            this.tbxDestURL.TabIndex = 9;
            // 
            // tbxSrcMethod
            // 
            this.tbxSrcMethod.BackColor = System.Drawing.Color.LightGray;
            this.tbxSrcMethod.ForeColor = System.Drawing.Color.Black;
            this.tbxSrcMethod.Location = new System.Drawing.Point(200, 433);
            this.tbxSrcMethod.Margin = new System.Windows.Forms.Padding(6);
            this.tbxSrcMethod.MaxLength = 100;
            this.tbxSrcMethod.Name = "tbxSrcMethod";
            this.tbxSrcMethod.Size = new System.Drawing.Size(396, 20);
            this.tbxSrcMethod.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.LightGray;
            this.label9.Location = new System.Drawing.Point(30, 436);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 86;
            this.label9.Text = "Source Method*";
            // 
            // tbxDestMethod
            // 
            this.tbxDestMethod.BackColor = System.Drawing.Color.LightGray;
            this.tbxDestMethod.ForeColor = System.Drawing.Color.Black;
            this.tbxDestMethod.Location = new System.Drawing.Point(200, 537);
            this.tbxDestMethod.Margin = new System.Windows.Forms.Padding(6);
            this.tbxDestMethod.MaxLength = 100;
            this.tbxDestMethod.Name = "tbxDestMethod";
            this.tbxDestMethod.Size = new System.Drawing.Size(396, 20);
            this.tbxDestMethod.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.LightGray;
            this.label11.Location = new System.Drawing.Point(30, 540);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 88;
            this.label11.Text = "Dest Method*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.LightGray;
            this.label13.Location = new System.Drawing.Point(636, 335);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 92;
            this.label13.Text = "Logging*";
            // 
            // cmbLogging
            // 
            this.cmbLogging.BackColor = System.Drawing.Color.LightGray;
            this.cmbLogging.FormattingEnabled = true;
            this.cmbLogging.ItemHeight = 13;
            this.cmbLogging.Items.AddRange(new object[] {
            "OFF",
            "ON"});
            this.cmbLogging.Location = new System.Drawing.Point(806, 332);
            this.cmbLogging.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLogging.Name = "cmbLogging";
            this.cmbLogging.Size = new System.Drawing.Size(396, 21);
            this.cmbLogging.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.LightGray;
            this.label14.Location = new System.Drawing.Point(636, 27);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 94;
            this.label14.Text = "StartID*";
            // 
            // tbxStartID
            // 
            this.tbxStartID.BackColor = System.Drawing.Color.LightGray;
            this.tbxStartID.ForeColor = System.Drawing.Color.Black;
            this.tbxStartID.Location = new System.Drawing.Point(806, 24);
            this.tbxStartID.Margin = new System.Windows.Forms.Padding(6);
            this.tbxStartID.MaxLength = 25;
            this.tbxStartID.Name = "tbxStartID";
            this.tbxStartID.Size = new System.Drawing.Size(396, 20);
            this.tbxStartID.TabIndex = 11;
            // 
            // tbxSrcDBName
            // 
            this.tbxSrcDBName.BackColor = System.Drawing.Color.LightGray;
            this.tbxSrcDBName.ForeColor = System.Drawing.Color.Black;
            this.tbxSrcDBName.Location = new System.Drawing.Point(200, 230);
            this.tbxSrcDBName.Margin = new System.Windows.Forms.Padding(6);
            this.tbxSrcDBName.MaxLength = 50;
            this.tbxSrcDBName.Name = "tbxSrcDBName";
            this.tbxSrcDBName.Size = new System.Drawing.Size(396, 20);
            this.tbxSrcDBName.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.LightGray;
            this.label15.Location = new System.Drawing.Point(30, 233);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 96;
            this.label15.Text = "SrcDBName*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.LightGray;
            this.label16.Location = new System.Drawing.Point(30, 332);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 98;
            this.label16.Text = "Work Type*";
            // 
            // cmbWorkType
            // 
            this.cmbWorkType.BackColor = System.Drawing.Color.LightGray;
            this.cmbWorkType.FormattingEnabled = true;
            this.cmbWorkType.ItemHeight = 13;
            this.cmbWorkType.Items.AddRange(new object[] {
            "REPLICATE",
            "VERIFY"});
            this.cmbWorkType.Location = new System.Drawing.Point(200, 329);
            this.cmbWorkType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWorkType.Name = "cmbWorkType";
            this.cmbWorkType.Size = new System.Drawing.Size(396, 21);
            this.cmbWorkType.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.LightGray;
            this.label17.Location = new System.Drawing.Point(636, 77);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 100;
            this.label17.Text = "FinalID*";
            // 
            // tbxFinalID
            // 
            this.tbxFinalID.BackColor = System.Drawing.Color.LightGray;
            this.tbxFinalID.ForeColor = System.Drawing.Color.Black;
            this.tbxFinalID.Location = new System.Drawing.Point(806, 74);
            this.tbxFinalID.Margin = new System.Windows.Forms.Padding(6);
            this.tbxFinalID.MaxLength = 25;
            this.tbxFinalID.Name = "tbxFinalID";
            this.tbxFinalID.Size = new System.Drawing.Size(396, 20);
            this.tbxFinalID.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.LightGray;
            this.label12.Location = new System.Drawing.Point(636, 387);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 102;
            this.label12.Text = "Max Duration*";
            // 
            // tbxMaxDuration
            // 
            this.tbxMaxDuration.BackColor = System.Drawing.Color.LightGray;
            this.tbxMaxDuration.ForeColor = System.Drawing.Color.Black;
            this.tbxMaxDuration.Location = new System.Drawing.Point(806, 384);
            this.tbxMaxDuration.Margin = new System.Windows.Forms.Padding(6);
            this.tbxMaxDuration.MaxLength = 10;
            this.tbxMaxDuration.Name = "tbxMaxDuration";
            this.tbxMaxDuration.Size = new System.Drawing.Size(396, 20);
            this.tbxMaxDuration.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.LightGray;
            this.label18.Location = new System.Drawing.Point(30, 80);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 13);
            this.label18.TabIndex = 104;
            this.label18.Text = "Network*";
            // 
            // cmbNetwork
            // 
            this.cmbNetwork.BackColor = System.Drawing.Color.LightGray;
            this.cmbNetwork.FormattingEnabled = true;
            this.cmbNetwork.ItemHeight = 13;
            this.cmbNetwork.Items.AddRange(new object[] {
            "LOCALHOST",
            "INTERNAL",
            "DMZ",
            "EXTERNAL"});
            this.cmbNetwork.Location = new System.Drawing.Point(200, 74);
            this.cmbNetwork.Margin = new System.Windows.Forms.Padding(4);
            this.cmbNetwork.Name = "cmbNetwork";
            this.cmbNetwork.Size = new System.Drawing.Size(396, 21);
            this.cmbNetwork.TabIndex = 1;
            // 
            // tbxShardName
            // 
            this.tbxShardName.BackColor = System.Drawing.Color.LightGray;
            this.tbxShardName.ForeColor = System.Drawing.Color.Black;
            this.tbxShardName.Location = new System.Drawing.Point(200, 279);
            this.tbxShardName.Margin = new System.Windows.Forms.Padding(6);
            this.tbxShardName.MaxLength = 50;
            this.tbxShardName.Name = "tbxShardName";
            this.tbxShardName.Size = new System.Drawing.Size(396, 20);
            this.tbxShardName.TabIndex = 5;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.LightGray;
            this.label19.Location = new System.Drawing.Point(30, 282);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 13);
            this.label19.TabIndex = 106;
            this.label19.Text = "ShardName";
            // 
            // frmEditReplica
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1238, 598);
            this.Controls.Add(this.tbxShardName);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cmbNetwork);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbxMaxDuration);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.tbxFinalID);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbWorkType);
            this.Controls.Add(this.tbxSrcDBName);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbxStartID);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbLogging);
            this.Controls.Add(this.tbxDestMethod);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbxSrcMethod);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbxDestURL);
            this.Controls.Add(this.tbxSrcURL);
            this.Controls.Add(this.tbxSchemaTable);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbRunState);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbxNextRun);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxIntervalMS);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSchemaType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxBatchSize);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxGlobalID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditReplica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit ReplicaWork Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxGlobalID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxBatchSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSchemaType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxIntervalMS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxNextRun;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbRunState;
        private System.Windows.Forms.TextBox tbxSchemaTable;
        private System.Windows.Forms.TextBox tbxSrcURL;
        private System.Windows.Forms.TextBox tbxDestURL;
        private System.Windows.Forms.TextBox tbxSrcMethod;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxDestMethod;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbLogging;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxStartID;
        private System.Windows.Forms.TextBox tbxSrcDBName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbWorkType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxFinalID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxMaxDuration;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbNetwork;
        private System.Windows.Forms.TextBox tbxShardName;
        private System.Windows.Forms.Label label19;
    }
}