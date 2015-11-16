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
        
        //creating an istance of parameters and the graphic panel      
        private Parameters parameters = new Parameters();
        private GraphicPanelController graphicPanel;

        public Main() {
            InitializeComponent();
            graphicPanel = new GraphicPanelController(parameters, instantAiCtrl1);
            //adding the panel to the interface:
            this.SuspendLayout();
            this.Controls.Add(graphicPanel.prepareInterface());
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       

        private void creditsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //Showing credits:
            Flash credits = new Flash();
            credits.ShowDialog();
        }

        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void operationsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //opening the settings window
            Settings settings = new Settings(parameters);
            settings.ShowDialog();
        }

        private void play_CheckedChanged(object sender, EventArgs e) {
            //logic of the play/pause button
            if(play.Checked == true) {
                this.play.Image = global::ReadDataFromDAQNavi.Properties.Resources.stop;
                graphicPanel.run(true);
            } else {
                graphicPanel.run(false);
                this.play.Image = global::ReadDataFromDAQNavi.Properties.Resources.go;
            }
        }


    }
}