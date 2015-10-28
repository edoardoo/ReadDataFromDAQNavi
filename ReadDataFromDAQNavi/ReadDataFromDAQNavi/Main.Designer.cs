namespace ReadDataFromDAQNavi {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.operationsLink = new System.Windows.Forms.LinkLabel();
            this.creditsLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // operationsLink
            // 
            this.operationsLink.AutoSize = true;
            this.operationsLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.operationsLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.operationsLink.Location = new System.Drawing.Point(13, 13);
            this.operationsLink.Name = "operationsLink";
            this.operationsLink.Size = new System.Drawing.Size(58, 13);
            this.operationsLink.TabIndex = 0;
            this.operationsLink.TabStop = true;
            this.operationsLink.Text = "Operations";
            // 
            // creditsLink
            // 
            this.creditsLink.AutoSize = true;
            this.creditsLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.creditsLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.creditsLink.Location = new System.Drawing.Point(91, 13);
            this.creditsLink.Name = "creditsLink";
            this.creditsLink.Size = new System.Drawing.Size(39, 13);
            this.creditsLink.TabIndex = 1;
            this.creditsLink.TabStop = true;
            this.creditsLink.Text = "Credits";
            this.creditsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.creditsLink_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 494);
            this.Controls.Add(this.creditsLink);
            this.Controls.Add(this.operationsLink);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel operationsLink;
        private System.Windows.Forms.LinkLabel creditsLink;
    }
}

