﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadDataFromDAQNavi {
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
                System.Diagnostics.Process.Start("https://github.com/edoardoo/ReadDataFromDAQNavi");
            } catch { }
        }



        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
