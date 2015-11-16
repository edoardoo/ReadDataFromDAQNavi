namespace ReadDataFromDAQNavi {
	partial class ScopeControl {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if ( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.drawTimer = new System.Windows.Forms.Timer(this.components);
			this.timeUnitLabel = new System.Windows.Forms.Label();
			this.locLabel = new System.Windows.Forms.Label();
			this.historyScrollBar = new System.Windows.Forms.HScrollBar();
			this.timeLeftLabel = new System.Windows.Forms.Label();
			this.timeRightLabel = new System.Windows.Forms.Label();
			this.lblSamples = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// drawTimer
			// 
			this.drawTimer.Interval = 50;
			this.drawTimer.Tick += new System.EventHandler(this.drawTimer_Tick);
			// 
			// timeUnitLabel
			// 
			this.timeUnitLabel.AutoSize = true;
			this.timeUnitLabel.Location = new System.Drawing.Point(15, 53);
			this.timeUnitLabel.Name = "timeUnitLabel";
			this.timeUnitLabel.Size = new System.Drawing.Size(71, 13);
			this.timeUnitLabel.TabIndex = 0;
			this.timeUnitLabel.Text = "timeUnitLabel";
			// 
			// locLabel
			// 
			this.locLabel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.locLabel.AutoSize = true;
			this.locLabel.Location = new System.Drawing.Point(221, 122);
			this.locLabel.Name = "locLabel";
			this.locLabel.Size = new System.Drawing.Size(0, 13);
			this.locLabel.TabIndex = 1;
			this.locLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// historyScrollBar
			// 
			this.historyScrollBar.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.historyScrollBar.Location = new System.Drawing.Point(27, 81);
			this.historyScrollBar.Name = "historyScrollBar";
			this.historyScrollBar.Size = new System.Drawing.Size(150, 13);
			this.historyScrollBar.TabIndex = 2;
			this.historyScrollBar.Visible = false;
			this.historyScrollBar.ValueChanged += new System.EventHandler(this.historyScrollBar_ValueChanged);
			// 
			// timeLeftLabel
			// 
			this.timeLeftLabel.AutoSize = true;
			this.timeLeftLabel.Location = new System.Drawing.Point(59, 22);
			this.timeLeftLabel.Name = "timeLeftLabel";
			this.timeLeftLabel.Size = new System.Drawing.Size(27, 13);
			this.timeLeftLabel.TabIndex = 3;
			this.timeLeftLabel.Text = "t=0s";
			// 
			// timeRightLabel
			// 
			this.timeRightLabel.AutoSize = true;
			this.timeRightLabel.Location = new System.Drawing.Point(141, 12);
			this.timeRightLabel.Name = "timeRightLabel";
			this.timeRightLabel.Size = new System.Drawing.Size(27, 13);
			this.timeRightLabel.TabIndex = 4;
			this.timeRightLabel.Text = "t=0s";
			// 
			// lblSamples
			// 
			this.lblSamples.AutoSize = true;
			this.lblSamples.Location = new System.Drawing.Point(131, 53);
			this.lblSamples.Name = "lblSamples";
			this.lblSamples.Size = new System.Drawing.Size(27, 13);
			this.lblSamples.TabIndex = 5;
			this.lblSamples.Text = "t=0s";
			// 
			// ScopeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblSamples);
			this.Controls.Add(this.timeRightLabel);
			this.Controls.Add(this.timeLeftLabel);
			this.Controls.Add(this.historyScrollBar);
			this.Controls.Add(this.locLabel);
			this.Controls.Add(this.timeUnitLabel);
			this.Cursor = System.Windows.Forms.Cursors.Cross;
			this.Name = "ScopeControl";
			this.Size = new System.Drawing.Size(224, 135);
			this.Load += new System.EventHandler(this.Scope_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer drawTimer;
		private System.Windows.Forms.Label timeUnitLabel;
		private System.Windows.Forms.Label locLabel;
		private System.Windows.Forms.HScrollBar historyScrollBar;
		private System.Windows.Forms.Label timeLeftLabel;
		private System.Windows.Forms.Label timeRightLabel;
		private System.Windows.Forms.Label lblSamples;

	}
}
