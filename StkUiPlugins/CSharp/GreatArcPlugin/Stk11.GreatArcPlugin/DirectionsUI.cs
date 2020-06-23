using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AGI.STKObjects;

using System.IO;
using System.Xml;

namespace Agi.Ui.GreatArc
{
    public partial class DirectionsUI : Form
    {

        //private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        //private GvFromDirections_10 m_uiPlugin;
        private string gvName;
        private Directions routeBuilder;

        public DirectionsUI(AgStkObjectRoot stkRoot)
        {            
            InitializeComponent();
            m_root = stkRoot;

            terrainCheckBox.Enabled = Licensing("Professional");
            populateDropdowns();
            routeBuilder = new Directions(m_root);

        }

        private bool Licensing(string licenseSearch)
        {
            bool hasLicense = false;

            AGI.STKUtil.IAgExecCmdResult result = m_root.ExecuteCommand("GetLicenses /");
            foreach (string license in result)
            {
                string[] licenseData = license.Split(',');
                if (licenseData[0].Contains(licenseSearch) & !licenseData[1].Contains("No License"))
                {
                    hasLicense = true;
                }
            }
            return hasLicense;


        }

        

        private void button_LoadGV_Click(object sender, EventArgs e)
        {
            string strScenName = m_root.CurrentScenario.Path.ToString();
            

            Directions.MyWaypoint startPoint;
            Directions.MyWaypoint endPoint;
            // get lat/lon from addresses
            if (radioButton_Text.Checked)
            {
                #region empty address error check
                if (fromTextBox.Text == "" )
                {
                    MessageBox.Show("no start address entered");
                    return;
                }
                if (toTextBox.Text == "" )
                {
                    MessageBox.Show("no end address entered");
                    return;
                }
                #endregion

                startPoint = routeBuilder.Geocode(fromTextBox.Text);
                endPoint = routeBuilder.Geocode(toTextBox.Text);
            }
            else
            {
                #region empty address error check
                if (comboBox_FromObj.Items[comboBox_FromObj.SelectedIndex].ToString() == "")
                {
                    MessageBox.Show("no start address entered");
                    return;
                }
                if (comboBox_ToObj.Items[comboBox_ToObj.SelectedIndex].ToString() == "")
                {
                    MessageBox.Show("no end address entered");
                    return;
                }
                #endregion
                startPoint = routeBuilder.Geocode(m_root.GetObjectFromPath(comboBox_FromObj.Items[comboBox_FromObj.SelectedIndex].ToString()));
                endPoint = routeBuilder.Geocode(m_root.GetObjectFromPath(comboBox_ToObj.Items[comboBox_ToObj.SelectedIndex].ToString()));
            }

            #region address found error check
            if (startPoint.Longitude == 0.0 && startPoint.Latitude == 0.0 && startPoint.Altitude == 0.0)
            {
                MessageBox.Show("start address not found");
                return;
            }
            if (endPoint.Longitude == 0.0 && endPoint.Latitude == 0.0 && endPoint.Altitude == 0.0)
            {
                MessageBox.Show("end address not found");
                return;
            }
            #endregion


            List<Directions.MyWaypoint> routePoints = routeBuilder.CreateRoute(startPoint, endPoint);

            // create new GV and add the waypoints
            GenerateGVName(null, null);
            if (m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eGroundVehicle, gvName))
            {
                MessageBox.Show(gvName + " already exists, please pick a different name");
            } 
            else
	        {
                routeBuilder.PopulateGvRoute(gvName, 
                    routePoints, 
                    double.Parse(speedTextBox.Text), 
                    speedUnitsComboBox.Items[speedUnitsComboBox.SelectedIndex].ToString(), 
                    terrainCheckBox.Checked);
	        }
            gvName = uniqueName(gvName);
        }


        private void populateDropdowns()
        {
            comboBox_FromObj.Items.Clear();
            comboBox_ToObj.Items.Clear();
            foreach (IAgStkObject obj in m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eFacility))
            {
                comboBox_FromObj.Items.Add("*/" + obj.ClassName + "/" + obj.InstanceName);
                comboBox_ToObj.Items.Add("*/" + obj.ClassName + "/" + obj.InstanceName);
            }
            foreach (IAgStkObject obj in m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eTarget))
            {
                comboBox_FromObj.Items.Add("*/" + obj.ClassName + "/" + obj.InstanceName);
                comboBox_ToObj.Items.Add("*/" + obj.ClassName + "/" + obj.InstanceName);
            }
            foreach (IAgStkObject obj in m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.ePlace))
            {
                comboBox_FromObj.Items.Add("*/" + obj.ClassName + "/" + obj.InstanceName);
                comboBox_ToObj.Items.Add("*/" + obj.ClassName + "/" + obj.InstanceName);
            }
        }


        private void GenerateGVName(object sender, EventArgs e)
        {
            gvName = "";
            if (radioButton_Text.Checked)
            {
                gvName = substring(fromTextBox.Text, 8) + "_to_" + substring(toTextBox.Text, 8);
            }
            else if (comboBox_FromObj.SelectedIndex >= 0 & comboBox_ToObj.SelectedIndex >= 0)
            {
                string[] fromObjName = comboBox_FromObj.Items[comboBox_FromObj.SelectedIndex].ToString().Split('/');
                string[] toObjName = comboBox_ToObj.Items[comboBox_ToObj.SelectedIndex].ToString().Split('/');
                gvName = substring(fromObjName[fromObjName.GetUpperBound(0)], 8) +
                    "_to_" + substring(toObjName[toObjName.GetUpperBound(0)], 8);
            }

            gvName = uniqueName(gvName);
        }

        private string uniqueName(string inputName)
        {
            string outputName = inputName.Replace(' ', '_');

            bool nameExists = true;
            int counter = 1;
            while (nameExists)
            {
                if (m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eGroundVehicle, outputName))
                {
                    outputName = inputName + "_" + counter.ToString();
                    ++counter;
                }
                else
                {
                    nameExists = false;
                }
            }
            return outputName;

        }

        private String substring(string inputString, int length)
        {
            string returnString = inputString.Replace(' ', '_');

            if (inputString.Length > length)
            {
                returnString = inputString.Substring(0, length);
            }
            return returnString;

        }


        private void radioButton_Text_CheckedChanged(object sender, EventArgs e)
        {
            fromTextBox.Visible = radioButton_Text.Checked;
            toTextBox.Visible = radioButton_Text.Checked;
            radioButton_Objs.Checked = !radioButton_Text.Checked;
        }

        private void radioButton_Objs_CheckedChanged(object sender, EventArgs e)
        {
            populateDropdowns();
            comboBox_FromObj.Visible = radioButton_Objs.Checked;
            comboBox_ToObj.Visible = radioButton_Objs.Checked;
            radioButton_Text.Checked = !radioButton_Objs.Checked;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help Coming Soon...");
        }
    }
}
