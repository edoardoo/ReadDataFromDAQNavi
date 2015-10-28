using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadParameters {
    public partial class Flash : Form {
        public Flash() {
            //mostra la ventana con los parametros
            InitializeComponent();
            Parameters parameters = new Parameters();
            this.parametersTextBox.Text = parameters.getFormattedParams();
            

        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void authorLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //mostra la pagina web del Autor
            try {
                System.Diagnostics.Process.Start("http://edoardoo.com");
            } catch { }
        }

        private void closeFlash_Click(object sender, EventArgs e) {
            //cerra la ventana
            this.Close();
        }

      
    }
}
