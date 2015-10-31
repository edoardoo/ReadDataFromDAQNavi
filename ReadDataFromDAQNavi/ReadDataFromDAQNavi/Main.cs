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
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void creditsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Flash credits = new Flash();
            credits.ShowDialog();
        }

        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void operationsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void play_CheckedChanged(object sender, EventArgs e) {
            if(play.Checked == true) {
                this.play.Image = global::ReadDataFromDAQNavi.Properties.Resources.stop;
            } else {
                this.play.Image = global::ReadDataFromDAQNavi.Properties.Resources.go;

            }
        }
    }
}