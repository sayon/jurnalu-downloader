namespace jurnalu_downloader
{
    partial class MainForm
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
            this.labelInputUrl = new System.Windows.Forms.Label();
            this.linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelInputUrl
            // 
            this.labelInputUrl.AutoSize = true;
            this.labelInputUrl.Location = new System.Drawing.Point(13, 13);
            this.labelInputUrl.Name = "labelInputUrl";
            this.labelInputUrl.Size = new System.Drawing.Size(122, 13);
            this.labelInputUrl.TabIndex = 0;
            this.labelInputUrl.Text = "URL of any comic page:";
            // 
            // linkLabelAbout
            // 
            this.linkLabelAbout.AutoSize = true;
            this.linkLabelAbout.Location = new System.Drawing.Point(306, 81);
            this.linkLabelAbout.Name = "linkLabelAbout";
            this.linkLabelAbout.Size = new System.Drawing.Size(139, 13);
            this.linkLabelAbout.TabIndex = 1;
            this.linkLabelAbout.TabStop = true;
            this.linkLabelAbout.Text = "See project page on GitHub";
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(16, 39);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(351, 20);
            this.textBoxInput.TabIndex = 2;
            this.textBoxInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBoxInput_PreviewKeyDown);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(373, 39);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(75, 23);
            this.buttonDownload.TabIndex = 3;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 114);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.linkLabelAbout);
            this.Controls.Add(this.labelInputUrl);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Jurnalu.ru downloader";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInputUrl;
        private System.Windows.Forms.LinkLabel linkLabelAbout;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonDownload;
    }
}