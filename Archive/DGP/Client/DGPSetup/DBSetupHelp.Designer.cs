namespace DGPSetup
{
    partial class DBSetupHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBSetupHelp));
            this.brwsHelp = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // brwsHelp
            // 
            this.brwsHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brwsHelp.Location = new System.Drawing.Point(0, 0);
            this.brwsHelp.MinimumSize = new System.Drawing.Size(20, 20);
            this.brwsHelp.Name = "brwsHelp";
            this.brwsHelp.Size = new System.Drawing.Size(1474, 929);
            this.brwsHelp.TabIndex = 0;
            // 
            // DBSetupHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 929);
            this.Controls.Add(this.brwsHelp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DBSetupHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lattice Database Setup Help";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser brwsHelp;
    }
}