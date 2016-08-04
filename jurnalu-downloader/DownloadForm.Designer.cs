namespace jurnalu_downloader
{
    partial class DownloadForm
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
            this.labelDownloadingInto = new System.Windows.Forms.Label();
            this.labelDownloadingIntoContent = new System.Windows.Forms.Label();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonShowInExplorer = new System.Windows.Forms.Button();
            this.progressBarCurrent = new System.Windows.Forms.ProgressBar();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.buttonMore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDownloadingInto
            // 
            this.labelDownloadingInto.AutoSize = true;
            this.labelDownloadingInto.Location = new System.Drawing.Point(13, 13);
            this.labelDownloadingInto.Name = "labelDownloadingInto";
            this.labelDownloadingInto.Size = new System.Drawing.Size(89, 13);
            this.labelDownloadingInto.TabIndex = 0;
            this.labelDownloadingInto.Text = "Downloading into";
            // 
            // labelDownloadingIntoContent
            // 
            this.labelDownloadingIntoContent.AutoSize = true;
            this.labelDownloadingIntoContent.Location = new System.Drawing.Point(112, 13);
            this.labelDownloadingIntoContent.Name = "labelDownloadingIntoContent";
            this.labelDownloadingIntoContent.Size = new System.Drawing.Size(25, 13);
            this.labelDownloadingIntoContent.TabIndex = 1;
            this.labelDownloadingIntoContent.Text = "      ";
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(13, 36);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(52, 13);
            this.labelPage.TabIndex = 2;
            this.labelPage.Text = "Page 0/0";
            this.labelPage.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonShowInExplorer
            // 
            this.buttonShowInExplorer.Location = new System.Drawing.Point(115, 31);
            this.buttonShowInExplorer.Name = "buttonShowInExplorer";
            this.buttonShowInExplorer.Size = new System.Drawing.Size(75, 23);
            this.buttonShowInExplorer.TabIndex = 3;
            this.buttonShowInExplorer.Text = "Show folder";
            this.buttonShowInExplorer.UseVisualStyleBackColor = true;
            this.buttonShowInExplorer.Click += new System.EventHandler(this.buttonShowInExplorer_Click);
            // 
            // progressBarCurrent
            // 
            this.progressBarCurrent.Location = new System.Drawing.Point(16, 64);
            this.progressBarCurrent.Name = "progressBarCurrent";
            this.progressBarCurrent.Size = new System.Drawing.Size(429, 23);
            this.progressBarCurrent.TabIndex = 4;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(16, 94);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(429, 23);
            this.progressBarTotal.TabIndex = 5;
            // 
            // linkLabelAbout
            // 
            this.linkLabelAbout.AutoSize = true;
            this.linkLabelAbout.Location = new System.Drawing.Point(306, 130);
            this.linkLabelAbout.Name = "linkLabelAbout";
            this.linkLabelAbout.Size = new System.Drawing.Size(139, 13);
            this.linkLabelAbout.TabIndex = 6;
            this.linkLabelAbout.TabStop = true;
            this.linkLabelAbout.Text = "See project page on GitHub";
            // 
            // buttonMore
            // 
            this.buttonMore.Location = new System.Drawing.Point(369, 31);
            this.buttonMore.Name = "buttonMore";
            this.buttonMore.Size = new System.Drawing.Size(75, 23);
            this.buttonMore.TabIndex = 7;
            this.buttonMore.Text = "More";
            this.buttonMore.UseVisualStyleBackColor = true;
            this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 152);
            this.Controls.Add(this.buttonMore);
            this.Controls.Add(this.linkLabelAbout);
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.progressBarCurrent);
            this.Controls.Add(this.buttonShowInExplorer);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.labelDownloadingIntoContent);
            this.Controls.Add(this.labelDownloadingInto);
            this.Name = "DownloadForm";
            this.ShowIcon = false;
            this.Text = "Jurnalu.ru downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDownloadingInto;
        private System.Windows.Forms.Label labelDownloadingIntoContent;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button buttonShowInExplorer;
        private System.Windows.Forms.ProgressBar progressBarCurrent;
        private System.Windows.Forms.ProgressBar progressBarTotal;
        private System.Windows.Forms.LinkLabel linkLabelAbout;
        private System.Windows.Forms.Button buttonMore;
    }
}