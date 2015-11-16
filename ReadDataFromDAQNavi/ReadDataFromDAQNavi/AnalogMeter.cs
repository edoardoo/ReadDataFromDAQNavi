using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataFromDAQNavi {
    class AnalogMeter {
        private Parameters parameters = new Parameters();
        private Automation.BDaq.ErrorCode ec;
        private double dbl;
        private Automation.BDaq.InstantAiCtrl instantAiCtrl1;
        private System.Windows.Forms.TextBox outputAnalog = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox outputComputedAnalog = new System.Windows.Forms.TextBox();

        private System.Windows.Forms.Timer frequencyTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Panel analogPanel = new System.Windows.Forms.Panel();
          
        public AnalogMeter( Parameters parameters,Automation.BDaq.InstantAiCtrl instantAiCtrl ) {
            this.parameters = parameters;
            instantAiCtrl1 = instantAiCtrl;
            defineTimer();
           
        }
        public System.Windows.Forms.Panel prepareInterface() {
            this.analogPanel.SuspendLayout();
            // 
            // outputAnalog
            // 
            this.outputAnalog.Location = new System.Drawing.Point(14, 14);
            this.outputAnalog.Name = "outputAnalog";
            this.outputAnalog.Size = new System.Drawing.Size(100, 20);
            this.outputAnalog.TabIndex = 4;
            //
            //outputComputedAnalog
            //

            this.outputComputedAnalog.Location = new System.Drawing.Point(114, 14);
            this.outputComputedAnalog.Name = "outputComputedAnalog";
            this.outputComputedAnalog.Size = new System.Drawing.Size(100, 20);
            this.outputComputedAnalog.TabIndex = 4;
            // 
            // analogPanel
            // 
            this.analogPanel.Controls.Add(this.outputAnalog);
            this.analogPanel.Controls.Add(this.outputComputedAnalog);
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
            outputAnalog.Text = this.dbl.ToString();
            outputComputedAnalog.Text = computeValue().ToString();
        }
     
        public void readAnalogInput() {

            ec = instantAiCtrl1.Read(1, out dbl);
            if (ec == Automation.BDaq.ErrorCode.Success) {
                Console.WriteLine("Read another value");

            } else {
                Console.WriteLine("Error in reading daqnavi");
            }
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
