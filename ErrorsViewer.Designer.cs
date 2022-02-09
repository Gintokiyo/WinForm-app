
namespace ProiectMediiVizuale
{
    partial class ErrorsViewer
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
            this.textBoxErrorFeed = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxErrorFeed
            // 
            this.textBoxErrorFeed.AcceptsReturn = true;
            this.textBoxErrorFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxErrorFeed.Location = new System.Drawing.Point(1, 2);
            this.textBoxErrorFeed.Multiline = true;
            this.textBoxErrorFeed.Name = "textBoxErrorFeed";
            this.textBoxErrorFeed.ReadOnly = true;
            this.textBoxErrorFeed.Size = new System.Drawing.Size(785, 449);
            this.textBoxErrorFeed.TabIndex = 0;
            this.textBoxErrorFeed.WordWrap = false;
            // 
            // ErrorsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 451);
            this.Controls.Add(this.textBoxErrorFeed);
            this.MinimumSize = new System.Drawing.Size(805, 490);
            this.Name = "ErrorsViewer";
            this.Text = "Error Viewer";
            this.Load += new System.EventHandler(this.ErrorsViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxErrorFeed;
    }
}