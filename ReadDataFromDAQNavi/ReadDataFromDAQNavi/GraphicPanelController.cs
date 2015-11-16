using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataFromDAQNavi {
    class GraphicPanelController {

        //This is the class that populate the graphic panel

        private Parameters parameters = new Parameters();
        private Automation.BDaq.ErrorCode ec;
        private double dbl;
        private Automation.BDaq.InstantAiCtrl instantAiCtrl1;
        private System.Windows.Forms.Timer frequencyTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Panel analogPanel = new System.Windows.Forms.Panel();

        //the input elements
        private System.Windows.Forms.Label inputAnalogLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox inputAnalogTextBox = new System.Windows.Forms.TextBox();
        private AnalogMeter inputAnalogMeter = new AnalogMeter();

        //the output elements
        private System.Windows.Forms.Label outputAnalogLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox outputAnalogTextBox = new System.Windows.Forms.TextBox();
        private AnalogMeter outputAnalogMeter = new AnalogMeter();

        //The error elements
        private System.Windows.Forms.Label errorLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox errorTextBox = new System.Windows.Forms.TextBox();

        //The alarm bell
        private System.Windows.Forms.Label alarmLabel = new System.Windows.Forms.Label();

        //The scope
        private ScopeControl scope = new ScopeControl();


        public GraphicPanelController( Parameters parameters,Automation.BDaq.InstantAiCtrl instantAiCtrl ) {
            //Initialize the basic elements
            this.parameters = parameters;
            instantAiCtrl1 = instantAiCtrl;
            defineTimer();
            prepareScope();
           
        }

        public void prepareScope() {
            //prepare the scope with the traces
            Trace inputTrace = new Trace();
            Trace outputTrace = new Trace();

            inputTrace.TraceColor = System.Drawing.ColorTranslator.FromHtml("#4aa3df");
            inputTrace.UnitName = "Input Volts";
            outputTrace.TraceColor = System.Drawing.ColorTranslator.FromHtml("#e74c3c");
            outputTrace.UnitName = "Output Volts";
            scope.Traces.Add(inputTrace);
            scope.Traces.Add(outputTrace);
        }

        public System.Windows.Forms.Panel prepareInterface() {
            //populating the graphic panel with all the elements
            //TODO: need to be clean

            this.analogPanel.SuspendLayout();
            drawAnalogInput();
            drawAnalogOutput();
            drawScope();

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
            // Error Label
            //
            this.errorLabel.Location = new System.Drawing.Point(0, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.TabIndex = 4;
            this.errorLabel.Text = "Error:";


            //
            // Error TextBox
            //

            this.errorTextBox.Location = new System.Drawing.Point(110, 0);
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.Size = new System.Drawing.Size(100, 20);
            this.errorTextBox.TabIndex = 4;
            this.errorTextBox.Text = "0";
            this.errorTextBox.ReadOnly = true;

            //
            // Alarm
            //

            this.alarmLabel.Location = new System.Drawing.Point(570, 0);
            this.alarmLabel.Name = "alarmLabel";
            this.alarmLabel.TabIndex = 4;
            this.alarmLabel.Text = "ALARM";
            this.alarmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alarmLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.alarmLabel.Visible = false;
            // 
            // analogPanel
            // 

            this.analogPanel.Controls.Add(this.inputAnalogTextBox);
            this.analogPanel.Controls.Add(this.outputAnalogTextBox);
            this.analogPanel.Controls.Add(this.inputAnalogLabel);
            this.analogPanel.Controls.Add(this.outputAnalogLabel);
            this.analogPanel.Controls.Add(this.errorLabel);
            this.analogPanel.Controls.Add(this.errorTextBox);
            this.analogPanel.Controls.Add(this.alarmLabel);
            this.analogPanel.Location = new System.Drawing.Point(16, 40);
            this.analogPanel.Name = "analogPanel";
            this.analogPanel.Size = new System.Drawing.Size(894, 380);
            this.analogPanel.TabIndex = 5;
            this.analogPanel.ResumeLayout(false);
            this.analogPanel.PerformLayout();
            return this.analogPanel;
        }

        private void defineTimer() {
            //the timer that control all the process

            this.frequencyTimer.Enabled = false;
            updateTimer();
            this.frequencyTimer.Tick += new System.EventHandler(this.frequencyTimer_Tick);
        }
        private void updateTimer() {
            //updating the timer with the new parameters section

            int interval = 0;
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("tm").getValue(), out interval);
            this.frequencyTimer.Interval = interval;
        }

        private void frequencyTimer_Tick(object sender, EventArgs e) {
            //triggered when the timer ticks
            //reading again the input and then updating the interface
            readAnalogInput();
            updateInterface();
            
        }

        private void updateInterface() {
            //updating the interface with the new values 
            //after being generated by the DAQNavi library

            double inputValue = this.dbl;
            double computedValue = computeValue();
            inputAnalogTextBox.Text = this.dbl.ToString();
            outputAnalogTextBox.Text = computedValue.ToString();
            inputAnalogMeter.Value = (float) inputValue;
            outputAnalogMeter.Value = (float) computedValue;
            double setPoint = 0;
            Double.TryParse(parameters.getSectionByName("Controls").getParameterByName("sp").getValue(), out setPoint);
            errorTextBox.Text = (dbl - setPoint).ToString();

            //updating the scope
            scope.BeginAddPoint();
            scope.AddPoint(0, (float)dbl);
            scope.AddPoint(1, (float)computedValue);
            scope.EndAddPoint();
        }
     
        public void readAnalogInput() {
            //reading the input from the DAQNavi library, logging error in case

            ec = instantAiCtrl1.Read(1, out dbl);
            if (ec == Automation.BDaq.ErrorCode.Success) {
                Console.WriteLine("Read another value");

            } else {
                Console.WriteLine("Error in reading daqnavi");
            }
        }
        private void drawAnalogInput() {
            // Drawing the input analog meter
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
            // Drawing the output analog meter
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

        private void drawScope() {
            // Drawing the scope
            // scopeControl
            // 
            this.scope.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scope.BackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
            this.scope.Cursor = System.Windows.Forms.Cursors.Cross;
            this.scope.ForeColor = System.Drawing.Color.White;
            this.scope.GridSpacing = 1F;
            this.scope.Location = new System.Drawing.Point(12, 30);
            this.scope.Name = "scopeControl";
            this.scope.Size = new System.Drawing.Size(610, 362);
            this.scope.TabIndex = 0;
            this.scope.UnitsY = 30F;

            this.analogPanel.Controls.Add(this.scope);
        }
        public double computeValue() {
            //computing the value for the output

            double originalValue =  this.dbl;
            double outputValue = 0 ;
            int sup, inf, min, max, k;

            //getting the parameters from the Parameters object and saving them locally
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("sup").getValue(), out sup);
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("inf").getValue(), out inf);
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("min").getValue(), out min);
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("max").getValue(), out max);
            Int32.TryParse(parameters.getSectionByName("Controls").getParameterByName("k").getValue(), out k);

            //computing output:
            if( originalValue > sup && originalValue <= max  ) {
                outputValue = k * originalValue;
                alarm("on");
            }else if ( originalValue >= inf && originalValue <= sup) {
                outputValue = 0;
                alarm("off");
            }else if ( originalValue >= min && originalValue < inf) {
                outputValue = k * originalValue;
                alarm("on");
            }
            updateTimer();
            return outputValue;
        }

        public void alarm(String state) {
            //toggle the alarm label

            this.alarmLabel.Visible = false;
            if(state == "on") {
                this.alarmLabel.Visible = true;
            } 
        }
        public void toggle() {
            //toggle the application
            if (this.frequencyTimer.Enabled) {
                run(true);
            }else {
                run(false);

            }
        }

        public void run(Boolean state) {
            //turn on or off the application

            this.frequencyTimer.Enabled = state;
            if (state) {
                this.scope.Start();

            }else {
                this.scope.Stop();
            }

        }

    }
}
