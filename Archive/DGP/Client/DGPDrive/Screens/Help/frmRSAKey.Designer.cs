
namespace DGPDrive.Screens.Help
{
    partial class frmRSAKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRSAKey));
            this.tbxPublicKey = new System.Windows.Forms.TextBox();
            this.tbxKeyPair = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateKeys = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtn2048 = new System.Windows.Forms.RadioButton();
            this.rbtn4096 = new System.Windows.Forms.RadioButton();
            this.ckbXmlEncode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbxPublicKey
            // 
            this.tbxPublicKey.Location = new System.Drawing.Point(24, 68);
            this.tbxPublicKey.Multiline = true;
            this.tbxPublicKey.Name = "tbxPublicKey";
            this.tbxPublicKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxPublicKey.Size = new System.Drawing.Size(744, 76);
            this.tbxPublicKey.TabIndex = 0;
            // 
            // tbxKeyPair
            // 
            this.tbxKeyPair.Location = new System.Drawing.Point(24, 193);
            this.tbxKeyPair.Multiline = true;
            this.tbxKeyPair.Name = "tbxKeyPair";
            this.tbxKeyPair.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxKeyPair.Size = new System.Drawing.Size(744, 180);
            this.tbxKeyPair.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(21, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Public Key";
            // 
            // btnCreateKeys
            // 
            this.btnCreateKeys.AutoSize = true;
            this.btnCreateKeys.BackColor = System.Drawing.Color.LightGray;
            this.btnCreateKeys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateKeys.ForeColor = System.Drawing.Color.Black;
            this.btnCreateKeys.Location = new System.Drawing.Point(668, 22);
            this.btnCreateKeys.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateKeys.Name = "btnCreateKeys";
            this.btnCreateKeys.Size = new System.Drawing.Size(100, 28);
            this.btnCreateKeys.TabIndex = 3;
            this.btnCreateKeys.Text = "Create Keys";
            this.btnCreateKeys.UseVisualStyleBackColor = false;
            this.btnCreateKeys.Click += new System.EventHandler(this.btnCreateKeys_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(21, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Key Pair";
            // 
            // rbtn2048
            // 
            this.rbtn2048.AutoSize = true;
            this.rbtn2048.Checked = true;
            this.rbtn2048.ForeColor = System.Drawing.Color.LightGray;
            this.rbtn2048.Location = new System.Drawing.Point(522, 24);
            this.rbtn2048.Name = "rbtn2048";
            this.rbtn2048.Size = new System.Drawing.Size(62, 26);
            this.rbtn2048.TabIndex = 5;
            this.rbtn2048.TabStop = true;
            this.rbtn2048.Text = "2048";
            this.rbtn2048.UseVisualStyleBackColor = true;
            // 
            // rbtn4096
            // 
            this.rbtn4096.AutoSize = true;
            this.rbtn4096.ForeColor = System.Drawing.Color.LightGray;
            this.rbtn4096.Location = new System.Drawing.Point(590, 24);
            this.rbtn4096.Name = "rbtn4096";
            this.rbtn4096.Size = new System.Drawing.Size(62, 26);
            this.rbtn4096.TabIndex = 6;
            this.rbtn4096.Text = "4096";
            this.rbtn4096.UseVisualStyleBackColor = true;
            // 
            // ckbXmlEncode
            // 
            this.ckbXmlEncode.AutoSize = true;
            this.ckbXmlEncode.Checked = true;
            this.ckbXmlEncode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbXmlEncode.ForeColor = System.Drawing.Color.LightGray;
            this.ckbXmlEncode.Location = new System.Drawing.Point(415, 25);
            this.ckbXmlEncode.Name = "ckbXmlEncode";
            this.ckbXmlEncode.Size = new System.Drawing.Size(101, 27);
            this.ckbXmlEncode.TabIndex = 7;
            this.ckbXmlEncode.Text = "XML Encode";
            this.ckbXmlEncode.UseVisualStyleBackColor = true;
            // 
            // frmRSAKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(794, 400);
            this.Controls.Add(this.ckbXmlEncode);
            this.Controls.Add(this.rbtn4096);
            this.Controls.Add(this.rbtn2048);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCreateKeys);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxKeyPair);
            this.Controls.Add(this.tbxPublicKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRSAKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSA Key Creation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxPublicKey;
        private System.Windows.Forms.TextBox tbxKeyPair;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateKeys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtn2048;
        private System.Windows.Forms.RadioButton rbtn4096;
        private System.Windows.Forms.CheckBox ckbXmlEncode;
    }
}