using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadDataFromDAQNavi {
    public partial class Settings : Form {
        public Settings() {
            InitializeComponent();
            Parameters parameters = new Parameters();
            string nombre = parameters.getSectionByName("Section One").getParameterByName("Nombre").getValue();
            nameTextBox.Text = nombre;
        }

        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e) {

        }


    }
}
