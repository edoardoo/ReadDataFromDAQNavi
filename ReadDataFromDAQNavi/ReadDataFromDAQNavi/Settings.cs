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
        //The settings window class

        private Parameters parameters;

        public Settings( Parameters parameters) {
            //getting parameters and then populating the settings panel
            this.parameters = parameters;
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
            tableControls.RowCount = section.getAllParameters().Count();
            tableControls.AutoSize = true;
            foreach (Parameter param in section.getAllParameters()) {
                
                SettingControl controller = new SettingControl(param);
                tableControls.Controls.Add(controller);
            }

        }

        private void closeButton_Click(object sender, EventArgs e) {
            parameters.saveParameters();
            this.Close();
        }

    }
}
