using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataFromDAQNavi {
    class GraphicPanelController {
        private Parameters parameters = new Parameters();
        private Automation.BDaq.ErrorCode ec;
        private double dbl;
        private Automation.BDaq.InstantAiCtrl instantAiCtrl1;
        private System.Windows.Forms.TextBox inputAnalogTextBox = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox outputAnalogTextBox = new System.Windows.Forms.TextBox();

        private System.Windows.Forms.Timer frequencyTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Panel analogPanel = new System.Windows.Forms.Panel();
        private AnalogMeter inputAnalogMeter = new AnalogMeter();
        private AnalogMeter outputAnalogMeter = new AnalogMeter();
        private System.Windows.Forms.Label inputAnalogLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label outputAnalogLabel = new System.Windows.Forms.Label();


        public GraphicPanelController( Parameters parameters,Automation.BDaq.InstantAiCtrl instantAiCtrl ) {
            this.parameters = parameters;
            instantAiCtrl1 = instantAiCtrl;
            defineTimer();
           
        }
        public System.Windows.Forms.Panel prepareInterface() {
            this.analogPanel.SuspendLayout();
            drawAnalogInput();
            drawAnalogOutput();
            //
            // input Label
            //
            this.inputAnalogLabel.Location = new System.Drawing.Point(652, 5);
            this.inputAnalogLabel.Name = "inputAnalogLabel";
            this.inputAnalogLabel.TabIndex = 4;
            this.inputAnalogLabel.Text = "Input in volts:";

            // 
            // input TextBox
            // 
            this.inputAnalogTextBox.Location = new System.Drawing.Point(775, 5);
            this.inputAnalogTextBox.Name = "inputAnalogTextBox";
            this.inputAnalogTextBox.Size = new System.Drawing.Size(100, 20);
            this.inputAnalogTextBox.TabIndex = 4;
            this.inputAnalogTextBox.Text = "0";

            //
            // output Label
            //
            this.outputAnalogLabel.Location = new System.Drawing.Point(652, 205);
            this.outputAnalogLabel.Name = "outputAnalogLabel";
            this.outputAnalogLabel.TabIndex = 4;
            this.outputAnalogLabel.Text = "Output in volts:";
            

            //
            // output TextBox
            //

            this.outputAnalogTextBox.Location = new System.Drawing.Point(775, 205);
            this.outputAnalogTextBox.Name = "outputAnalogTextBox";
            this.outputAnalogTextBox.Size = new System.Drawing.Size(100, 20);
            this.outputAnalogTextBox.TabIndex = 4;
            this.outputAnalogTextBox.Text = "0";

            // 
            // analogPanel
            // 
            this.analogPanel.Controls.Add(this.inputAnalogTextBox);
            this.analogPanel.Controls.Add(this.outputAnalogTextBox);
            this.analogPanel.Controls.Add(this.inputAnalogLabel);
            this.analogPanel.Controls.Add(this.outputAnalogLabel);

            this.analogPanel.Location = new System.Drawing.Point(16, 40);
            this.analogPanel.Name = "analogPanel";
            this.analogPanel.Size = new System.Drawing.Size(894, 380);
            this.analogPanel.TabIndex = 5;
            this.analogPanel.ResumeLayout(false);
            this.analogPanel.PerformLayout();
            return this.analogPanel;
        }

        private void defineTimer() {
            this.frequencyTimer.Enabled = false;
            updateTimer();
            this.frequencyTimer.Tick += new System.EventHandler(this.frequencyTimer_Tick);
        }
        private void updateTimer() {
            int interval = 0;
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("tm").getValue(), out interval);
            this.frequencyTimer.Interval = interval;
        }

        private void frequencyTimer_Tick(object sender, EventArgs e) {
            readAnalogInput();
            updateInterface();
        }

        private void updateInterface() {
            double inputValue = this.dbl;
            double computedValue = computeValue();
            inputAnalogTextBox.Text = this.dbl.ToString();
            outputAnalogTextBox.Text = computedValue.ToString();
            inputAnalogMeter.Value = (float) inputValue;
            outputAnalogMeter.Value = (float) computedValue;
        }
     
        public void readAnalogInput() {

            ec = instantAiCtrl1.Read(1, out dbl);
            if (ec == Automation.BDaq.ErrorCode.Success) {
                Console.WriteLine("Read another value");

            } else {
                Console.WriteLine("Error in reading daqnavi");
            }
        }
        private void drawAnalogInput() {
            // 
            // analogMeter1
            // 
            this.inputAnalogMeter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputAnalogMeter.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputAnalogMeter.FrameColor = System.Drawing.Color.Black;
            this.inputAnalogMeter.FramePadding = new System.Windows.Forms.Padding(5);
            this.inputAnalogMeter.InternalPadding = new System.Windows.Forms.Padding(5);
            this.inputAnalogMeter.Location = new System.Drawing.Point(652, 30);
            this.inputAnalogMeter.MaxValue = 10F;
            this.inputAnalogMeter.MinValue = -10F;
            this.inputAnalogMeter.Name = "Analog Meter";
            this.inputAnalogMeter.Size = new System.Drawing.Size(224, 149);
            this.inputAnalogMeter.TabIndex = 2;
            this.inputAnalogMeter.Text = "Input Volts";
            this.inputAnalogMeter.TickStartAngle = 20F;
            this.inputAnalogMeter.Value = 0F;
            this.analogPanel.Controls.Add(inputAnalogMeter);

        }
        private void drawAnalogOutput() {
            // 
            // output
            // 
            this.outputAnalogMeter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.outputAnalogMeter.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputAnalogMeter.FrameColor = System.Drawing.Color.Black;
            this.outputAnalogMeter.FramePadding = new System.Windows.Forms.Padding(5);
            this.outputAnalogMeter.InternalPadding = new System.Windows.Forms.Padding(5);
            this.outputAnalogMeter.Location = new System.Drawing.Point(652, 231);
            this.outputAnalogMeter.MaxValue = 10F;
            this.outputAnalogMeter.MinValue = -10F;
            this.outputAnalogMeter.Name = "Analog Meter";
            this.outputAnalogMeter.Size = new System.Drawing.Size(224, 149);
            this.outputAnalogMeter.TabIndex = 2;
            this.outputAnalogMeter.Text = "Output Volts";
            this.outputAnalogMeter.TickStartAngle = 20F;
            this.outputAnalogMeter.Value = 0F;
            this.analogPanel.Controls.Add(outputAnalogMeter);

        }
        public double computeValue() {
            double originalValue =  this.dbl;
            double outputValue = 0 ;
            int sup, inf, min, max, K, k;

            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("sup").getValue(), out sup);
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("inf").getValue(), out inf);

            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("min").getValue(), out min);

            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("max").getValue(), out max);

            //Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("K").getValue(), out K);
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("k").getValue(), out k);

            if( originalValue > sup && originalValue <= max  ) {
                outputValue = k * originalValue;
            }else if ( originalValue >= inf && originalValue <= sup) {
                outputValue = 0;
            }else if ( originalValue >= min && originalValue < inf) {
                outputValue = k * originalValue;
            }
            updateTimer();
            return outputValue;
        }
        public void toggle() {
            if (this.frequencyTimer.Enabled) {
                run(true);
            }else {
                run(false);

            }
        }
        public void run(Boolean state) {
            this.frequencyTimer.Enabled = state;
           
        }

    }
}
