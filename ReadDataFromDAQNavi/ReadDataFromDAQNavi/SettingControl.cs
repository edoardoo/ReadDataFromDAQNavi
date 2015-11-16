using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadDataFromDAQNavi {
    
    class SettingControl : System.Windows.Forms.TableLayoutPanel {
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TrackBar slider;
        private System.Windows.Forms.TextBox currentValue;
        private Parameter relatedParameter;

        public SettingControl( Parameter parameter ) {
            this.ColumnCount = 3;
            this.relatedParameter = parameter;
            int rowHeight = 55;
            
            this.name = new System.Windows.Forms.Label();
            this.name.Text = parameter.getKey();
            this.name.AutoSize = true;

            this.Dock = DockStyle.Fill;
            this.Height = rowHeight;
            TableLayoutColumnStyleCollection styles = this.ColumnStyles;
            this.ColumnStyles.Clear();
            foreach (ColumnStyle style in styles) {
                
                if (style.SizeType == SizeType.AutoSize) {
                    style.SizeType = SizeType.Percent;

                    // Set the column width to be a percentage
                    // of the TableLayoutPanel control's width.
                    style.Width = 10;
                }
            }
                
            
            this.slider = new System.Windows.Forms.TrackBar();

            this.slider.Minimum = -10;
            this.slider.Maximum = 10;
            if(parameter.getKey() == "tm") {
                this.slider.Minimum = 1;
                this.slider.Maximum = 1000;
               
            }

            this.slider.Value = Int32.Parse(parameter.getValue());
            this.slider.Size = new System.Drawing.Size(298, 45);
            this.slider.Scroll += new System.EventHandler(this.updateValue);
            this.currentValue = new System.Windows.Forms.TextBox();
            this.currentValue.Text = parameter.getValue();

            this.Name = "flowLayoutPanel1";
            this.Size = new System.Drawing.Size(496, 361);
            this.TabIndex = 0;
            this.Controls.Add(this.name);
            this.Controls.Add(this.slider);
            this.Controls.Add(this.currentValue);
            this.currentValue.TextChanged += new System.EventHandler(updateValueFromTextBox);



        }
        public void getName() {

        }
        public void setName() {

        }
        public void getSlider() {

        }
        public void setSlider() {

        }
        public void getCurrentValue() {

        }
        public void setCurrentValue() {

        }
        public void updateValue(object sender, System.EventArgs e) {
            int value = this.slider.Value;
            this.currentValue.Text = value.ToString();
            this.relatedParameter.setValue(value.ToString());
            
        }
        public void updateValueFromTextBox(object sender, System.EventArgs e) {
            int value = 0;
            Int32.TryParse(this.currentValue.Text, out value);
            if(value <= this.slider.Maximum && value >= this.slider.Minimum) {
                this.slider.Value = value;
                this.relatedParameter.setValue(value.ToString());
            } else {
                this.currentValue.Text = this.relatedParameter.getValue();
            }

        }

    }
}
