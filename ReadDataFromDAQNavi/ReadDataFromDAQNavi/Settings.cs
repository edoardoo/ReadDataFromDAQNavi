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
        private Parameters parameters = new Parameters();
        public Settings() {
            InitializeComponent();
            fillGeneralPanel();
            fillControlsPanel();
        }
        private void fillGeneralPanel() {
            List<SettingUnit> paramsToSet = new List<SettingUnit> {
                new SettingUnit( "Name", nameTextBox ),
                new SettingUnit( "Date", dateTextBox  ),
                new SettingUnit( "Description", descriptionTextBox  )

            };
            foreach (SettingUnit setting in paramsToSet) {

                setting.getTextBox().Text = parameters.getSectionByName("General").getParameterByName(setting.getId()).getValue();
                
            }
        }

        private void fillControlsPanel() {
            ParamsSection section = parameters.getSectionByName("Controls");
            int counter = 0;
            foreach (Parameter param in section.getAllParameters()) {
                
                SettingControl controller = new SettingControl(param, counter);
                this.Controls.Add( controller );
            }
        }
        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e) {

        }

        private void nameTextBox_Leave(object sender, EventArgs e) {
            //parameters.getSectionByName("General").getParameterByName("Name").setValue(nameTextBox.Text);
            //parameters.saveParameters();
        }

      
    }
}
