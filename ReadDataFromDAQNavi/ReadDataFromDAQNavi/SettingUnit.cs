using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataFromDAQNavi {
    class SettingUnit {
        //the settig basic unit
        private string id;
        private System.Windows.Forms.TextBox textBox;
        public SettingUnit( string id, System.Windows.Forms.TextBox textBox) {
            this.textBox = textBox;
            this.id = id;
        }
        public System.Windows.Forms.TextBox getTextBox() {
            return this.textBox;
        }
        public string getId() {
            return this.id;
        }
    }
}
