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
    public partial class Main : Form {
        private Boolean isRunning;
       
        private Parameters parameters = new Parameters();
        private GraphicPanelController analogMeter;

        public Main() {
            InitializeComponent();
            analogMeter = new GraphicPanelController(parameters, instantAiCtrl1);
            this.SuspendLayout();
            this.Controls.Add(analogMeter.prepareInterface());
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       

        private void creditsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Flash credits = new Flash();
            credits.ShowDialog();
        }

        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void operationsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Settings settings = new Settings(parameters);
            settings.ShowDialog();
        }

        private void play_CheckedChanged(object sender, EventArgs e) {
            if(play.Checked == true) {
                this.play.Image = global::ReadDataFromDAQNavi.Properties.Resources.stop;
                analogMeter.run(true);
                //this.setIsRunnintState(false);
            } else {
                analogMeter.run(false);
                this.play.Image = global::ReadDataFromDAQNavi.Properties.Resources.go;
                //this.setIsRunnintState(true);
            }
        }


    }
}