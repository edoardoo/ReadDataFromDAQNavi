namespace ReadDataFromDAQNavi {
    partial class Settings {
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
            this.tabsPanel = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.dateTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControls = new System.Windows.Forms.TabPage();
            this.closeButton = new System.Windows.Forms.Button();
            this.maxLabel = new System.Windows.Forms.Label();
            this.maxSlider = new System.Windows.Forms.TrackBar();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.minSlider = new System.Windows.Forms.TrackBar();
            this.minLabel = new System.Windows.Forms.Label();
            this.setPointTextBox = new System.Windows.Forms.TextBox();
            this.setPointSlider = new System.Windows.Forms.TrackBar();
            this.setPointLabel = new System.Windows.Forms.Label();
            this.supTextBox = new System.Windows.Forms.TextBox();
            this.supSlider = new System.Windows.Forms.TrackBar();
            this.supLabel = new System.Windows.Forms.Label();
            this.infTextBox = new System.Windows.Forms.TextBox();
            this.infSlider = new System.Windows.Forms.TrackBar();
            this.infLabel = new System.Windows.Forms.Label();
            this.kTextBox = new System.Windows.Forms.TextBox();
            this.kSlider = new System.Windows.Forms.TrackBar();
            this.kLabel = new System.Windows.Forms.Label();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.timeSlider = new System.Windows.Forms.TrackBar();
            this.timeLabel = new System.Windows.Forms.Label();
            this.tabsPanel.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setPointSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // tabsPanel
            // 
            this.tabsPanel.Controls.Add(this.tabGeneral);
            this.tabsPanel.Controls.Add(this.tabControls);
            this.tabsPanel.Location = new System.Drawing.Point(12, 12);
            this.tabsPanel.Name = "tabsPanel";
            this.tabsPanel.SelectedIndex = 0;
            this.tabsPanel.Size = new System.Drawing.Size(517, 397);
            this.tabsPanel.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.descriptionTextBox);
            this.tabGeneral.Controls.Add(this.dateTextBox);
            this.tabGeneral.Controls.Add(this.nameTextBox);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(509, 371);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            this.tabGeneral.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(110, 93);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(358, 257);
            this.descriptionTextBox.TabIndex = 5;
            // 
            // dateTextBox
            // 
            this.dateTextBox.Location = new System.Drawing.Point(110, 56);
            this.dateTextBox.Name = "dateTextBox";
            this.dateTextBox.ReadOnly = true;
            this.dateTextBox.Size = new System.Drawing.Size(358, 20);
            this.dateTextBox.TabIndex = 4;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(110, 17);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(358, 20);
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.Leave += new System.EventHandler(this.nameTextBox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tabControls
            // 
            this.tabControls.Controls.Add(this.timeTextBox);
            this.tabControls.Controls.Add(this.timeSlider);
            this.tabControls.Controls.Add(this.timeLabel);
            this.tabControls.Controls.Add(this.kTextBox);
            this.tabControls.Controls.Add(this.kSlider);
            this.tabControls.Controls.Add(this.kLabel);
            this.tabControls.Controls.Add(this.infTextBox);
            this.tabControls.Controls.Add(this.infSlider);
            this.tabControls.Controls.Add(this.infLabel);
            this.tabControls.Controls.Add(this.supTextBox);
            this.tabControls.Controls.Add(this.supSlider);
            this.tabControls.Controls.Add(this.supLabel);
            this.tabControls.Controls.Add(this.setPointTextBox);
            this.tabControls.Controls.Add(this.setPointSlider);
            this.tabControls.Controls.Add(this.setPointLabel);
            this.tabControls.Controls.Add(this.minTextBox);
            this.tabControls.Controls.Add(this.minSlider);
            this.tabControls.Controls.Add(this.minLabel);
            this.tabControls.Controls.Add(this.maxTextBox);
            this.tabControls.Controls.Add(this.maxSlider);
            this.tabControls.Controls.Add(this.maxLabel);
            this.tabControls.Location = new System.Drawing.Point(4, 22);
            this.tabControls.Name = "tabControls";
            this.tabControls.Padding = new System.Windows.Forms.Padding(3);
            this.tabControls.Size = new System.Drawing.Size(509, 371);
            this.tabControls.TabIndex = 1;
            this.tabControls.Text = "Controls";
            this.tabControls.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(429, 411);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 44);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "< BACK";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // maxLabel
            // 
            this.maxLabel.AutoSize = true;
            this.maxLabel.Location = new System.Drawing.Point(25, 27);
            this.maxLabel.Name = "maxLabel";
            this.maxLabel.Size = new System.Drawing.Size(27, 13);
            this.maxLabel.TabIndex = 0;
            this.maxLabel.Text = "Max";
            this.maxLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // maxSlider
            // 
            this.maxSlider.Location = new System.Drawing.Point(80, 15);
            this.maxSlider.Name = "maxSlider";
            this.maxSlider.Size = new System.Drawing.Size(305, 45);
            this.maxSlider.TabIndex = 1;
            // 
            // maxTextBox
            // 
            this.maxTextBox.Location = new System.Drawing.Point(403, 27);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxTextBox.TabIndex = 2;
            // 
            // minTextBox
            // 
            this.minTextBox.Location = new System.Drawing.Point(403, 78);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.Size = new System.Drawing.Size(100, 20);
            this.minTextBox.TabIndex = 5;
            // 
            // minSlider
            // 
            this.minSlider.Location = new System.Drawing.Point(80, 66);
            this.minSlider.Name = "minSlider";
            this.minSlider.Size = new System.Drawing.Size(305, 45);
            this.minSlider.TabIndex = 4;
            // 
            // minLabel
            // 
            this.minLabel.AutoSize = true;
            this.minLabel.Location = new System.Drawing.Point(25, 78);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(24, 13);
            this.minLabel.TabIndex = 3;
            this.minLabel.Text = "Min";
            // 
            // setPointTextBox
            // 
            this.setPointTextBox.Location = new System.Drawing.Point(403, 129);
            this.setPointTextBox.Name = "setPointTextBox";
            this.setPointTextBox.Size = new System.Drawing.Size(100, 20);
            this.setPointTextBox.TabIndex = 8;
            // 
            // setPointSlider
            // 
            this.setPointSlider.Location = new System.Drawing.Point(80, 117);
            this.setPointSlider.Name = "setPointSlider";
            this.setPointSlider.Size = new System.Drawing.Size(305, 45);
            this.setPointSlider.TabIndex = 7;
            // 
            // setPointLabel
            // 
            this.setPointLabel.AutoSize = true;
            this.setPointLabel.Location = new System.Drawing.Point(25, 129);
            this.setPointLabel.Name = "setPointLabel";
            this.setPointLabel.Size = new System.Drawing.Size(50, 13);
            this.setPointLabel.TabIndex = 6;
            this.setPointLabel.Text = "Set Point";
            // 
            // supTextBox
            // 
            this.supTextBox.Location = new System.Drawing.Point(403, 180);
            this.supTextBox.Name = "supTextBox";
            this.supTextBox.Size = new System.Drawing.Size(100, 20);
            this.supTextBox.TabIndex = 11;
            // 
            // supSlider
            // 
            this.supSlider.Location = new System.Drawing.Point(80, 168);
            this.supSlider.Name = "supSlider";
            this.supSlider.Size = new System.Drawing.Size(305, 45);
            this.supSlider.TabIndex = 10;
            // 
            // supLabel
            // 
            this.supLabel.AutoSize = true;
            this.supLabel.Location = new System.Drawing.Point(25, 180);
            this.supLabel.Name = "supLabel";
            this.supLabel.Size = new System.Drawing.Size(26, 13);
            this.supLabel.TabIndex = 9;
            this.supLabel.Text = "Sup";
            // 
            // infTextBox
            // 
            this.infTextBox.Location = new System.Drawing.Point(403, 231);
            this.infTextBox.Name = "infTextBox";
            this.infTextBox.Size = new System.Drawing.Size(100, 20);
            this.infTextBox.TabIndex = 14;
            // 
            // infSlider
            // 
            this.infSlider.Location = new System.Drawing.Point(80, 219);
            this.infSlider.Name = "infSlider";
            this.infSlider.Size = new System.Drawing.Size(305, 45);
            this.infSlider.TabIndex = 13;
            // 
            // infLabel
            // 
            this.infLabel.AutoSize = true;
            this.infLabel.Location = new System.Drawing.Point(25, 231);
            this.infLabel.Name = "infLabel";
            this.infLabel.Size = new System.Drawing.Size(19, 13);
            this.infLabel.TabIndex = 12;
            this.infLabel.Text = "Inf";
            // 
            // kTextBox
            // 
            this.kTextBox.Location = new System.Drawing.Point(403, 282);
            this.kTextBox.Name = "kTextBox";
            this.kTextBox.Size = new System.Drawing.Size(100, 20);
            this.kTextBox.TabIndex = 17;
            // 
            // kSlider
            // 
            this.kSlider.Location = new System.Drawing.Point(80, 270);
            this.kSlider.Name = "kSlider";
            this.kSlider.Size = new System.Drawing.Size(305, 45);
            this.kSlider.TabIndex = 16;
            // 
            // kLabel
            // 
            this.kLabel.AutoSize = true;
            this.kLabel.Location = new System.Drawing.Point(25, 282);
            this.kLabel.Name = "kLabel";
            this.kLabel.Size = new System.Drawing.Size(14, 13);
            this.kLabel.TabIndex = 15;
            this.kLabel.Text = "K";
            // 
            // timeTextBox
            // 
            this.timeTextBox.Location = new System.Drawing.Point(403, 332);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(100, 20);
            this.timeTextBox.TabIndex = 20;
            // 
            // timeSlider
            // 
            this.timeSlider.Location = new System.Drawing.Point(80, 320);
            this.timeSlider.Name = "timeSlider";
            this.timeSlider.Size = new System.Drawing.Size(305, 45);
            this.timeSlider.TabIndex = 19;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(25, 332);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(30, 13);
            this.timeLabel.TabIndex = 18;
            this.timeLabel.Text = "Time";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 458);
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tabsPanel);
            this.Name = "Settings";
            this.Text = "Settings";
            this.tabsPanel.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabControls.ResumeLayout(false);
            this.tabControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setPointSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSlider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabsPanel;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabControls;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox dateTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox infTextBox;
        private System.Windows.Forms.TrackBar infSlider;
        private System.Windows.Forms.Label infLabel;
        private System.Windows.Forms.TextBox supTextBox;
        private System.Windows.Forms.TrackBar supSlider;
        private System.Windows.Forms.Label supLabel;
        private System.Windows.Forms.TextBox setPointTextBox;
        private System.Windows.Forms.TrackBar setPointSlider;
        private System.Windows.Forms.Label setPointLabel;
        private System.Windows.Forms.TextBox minTextBox;
        private System.Windows.Forms.TrackBar minSlider;
        private System.Windows.Forms.Label minLabel;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.TrackBar maxSlider;
        private System.Windows.Forms.Label maxLabel;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.TrackBar timeSlider;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox kTextBox;
        private System.Windows.Forms.TrackBar kSlider;
        private System.Windows.Forms.Label kLabel;
    }
}