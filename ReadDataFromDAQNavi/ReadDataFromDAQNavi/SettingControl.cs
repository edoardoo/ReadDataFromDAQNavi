using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataFromDAQNavi {
    
    class SettingControl : System.Windows.Forms.FlowLayoutPanel {
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TrackBar slider;
        private System.Windows.Forms.TextBox currentValue;
        public SettingControl( Parameter parameter , int index ) {
            int rowHeight = 55;
            int elementsBaseHeight = rowHeight * index;
            
            this.name = new System.Windows.Forms.Label();
            this.name.Text = parameter.getKey();
            this.name.Location = new System.Drawing.Point(13, elementsBaseHeight);
            this.name.AutoSize = true;
            

            this.slider = new System.Windows.Forms.TrackBar();
            this.slider.Value = Int32.Parse(parameter.getValue());
            this.slider.Size = new System.Drawing.Size(298, 45);
            this.slider.Location = new System.Drawing.Point( this.name.Size.Width+10 , elementsBaseHeight);

            this.currentValue = new System.Windows.Forms.TextBox();
            this.currentValue.Text = parameter.getValue();
            this.currentValue.Location = new System.Drawing.Point( this.slider.Location.X+this.slider.Size.Width , elementsBaseHeight);

            this.Location = new System.Drawing.Point(7, 7);
            this.Name = "flowLayoutPanel1";
            this.Size = new System.Drawing.Size(496, 361);
            this.TabIndex = 0;

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

    }
}
