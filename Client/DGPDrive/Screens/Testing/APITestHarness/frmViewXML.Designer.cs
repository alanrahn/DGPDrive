namespace DGPDrive.Screens.Testing
{
    partial class frmViewXML
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewXML));
            this.wbrXMLView = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbrXMLView
            // 
            this.wbrXMLView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbrXMLView.Location = new System.Drawing.Point(0, 0);
            this.wbrXMLView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.wbrXMLView.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbrXMLView.Name = "wbrXMLView";
            this.wbrXMLView.Size = new System.Drawing.Size(1574, 862);
            this.wbrXMLView.TabIndex = 0;
            // 
            // frmViewXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1574, 862);
            this.Controls.Add(this.wbrXMLView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmViewXML";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View XML";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbrXMLView;
    }
}