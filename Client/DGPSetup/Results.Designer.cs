﻿namespace DGPSetup
{
    partial class Results
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Results));
            this.brwsResults = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // brwsResults
            // 
            this.brwsResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brwsResults.Location = new System.Drawing.Point(0, 0);
            this.brwsResults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.brwsResults.MinimumSize = new System.Drawing.Size(10, 10);
            this.brwsResults.Name = "brwsResults";
            this.brwsResults.Size = new System.Drawing.Size(625, 327);
            this.brwsResults.TabIndex = 0;
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 327);
            this.Controls.Add(this.brwsResults);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Results";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DGPDrive Database Setup Results";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser brwsResults;
    }
}