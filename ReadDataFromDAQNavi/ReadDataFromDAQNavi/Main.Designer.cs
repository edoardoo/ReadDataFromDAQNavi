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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.operationsLink = new System.Windows.Forms.LinkLabel();
            this.creditsLink = new System.Windows.Forms.LinkLabel();
            this.closeButton = new System.Windows.Forms.Button();
            this.play = new System.Windows.Forms.CheckBox();
            this.instantAiCtrl1 = new Automation.BDaq.InstantAiCtrl(this.components);
            this.frequencyTimer = new System.Windows.Forms.Timer(this.components);
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
            this.operationsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.operationsLink_LinkClicked);
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
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(810, 431);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 44);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "CLOSE";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // play
            // 
            this.play.Appearance = System.Windows.Forms.Appearance.Button;
            this.play.AutoSize = true;
            this.play.BackColor = System.Drawing.SystemColors.Menu;
            this.play.FlatAppearance.BorderSize = 0;
            this.play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.play.Image = global::ReadDataFromDAQNavi.Properties.Resources.go;
            this.play.Location = new System.Drawing.Point(717, 426);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(56, 56);
            this.play.TabIndex = 3;
            this.play.UseVisualStyleBackColor = false;
            this.play.CheckedChanged += new System.EventHandler(this.play_CheckedChanged);
            // 
            // instantAiCtrl1
            // 
            this.instantAiCtrl1._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("instantAiCtrl1._StateStream")));
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 494);
            this.ControlBox = false;
            this.Controls.Add(this.play);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.creditsLink);
            this.Controls.Add(this.operationsLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ReadDataFromDAQNavi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel operationsLink;
        private System.Windows.Forms.LinkLabel creditsLink;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox play;
        private Automation.BDaq.InstantAiCtrl instantAiCtrl1;
        private System.Windows.Forms.Timer frequencyTimer;
    }
}

