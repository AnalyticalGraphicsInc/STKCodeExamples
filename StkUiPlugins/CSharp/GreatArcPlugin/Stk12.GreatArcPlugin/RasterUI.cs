using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AGI.STKObjects;
using System.Drawing;

using System.IO;
using System.Xml;

namespace Agi.Ui.GreatArc.Stk12
{
    public partial class RasterUI : Form
    {
        private AgStkObjectRoot root;
        private RasterSearch routeBuilder;

        private RasterSearch.SwathWidthType swathType = RasterSearch.SwathWidthType.SensorFOV;
        private double swathParameter = 10.0;
        private string[] enduranceUnits = new string[6] { "nm", "mi", "km", "s", "min", "hr" };

        public RasterUI(AgStkObjectRoot m_root)
        {            
            InitializeComponent();
            root = m_root;
            routeBuilder = new RasterSearch(root);
            PopulateListViews();
            RadioChanged();
            setupGridView();
            setupUnitsComboBoxes();
            checkBox_UseEndurance_CheckedChanged(null, null);
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help coming soon...");
        }

        private void button_LoadAC_Click(object sender, EventArgs e)
        {
            IAgAircraft newAircraft = CreateRoute();
            if (checkBox_UseEndurance.Checked)
            {
                routeBuilder.CheckFlightEndurance(newAircraft, 
                    combobox_Units.Text, double.Parse(textBox_Endurance.Text));
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioChanged();
        }

        private void setupUnitsComboBoxes()
        {
            combobox_Units.Items.Clear();
            foreach (string unit in enduranceUnits)
            {
                combobox_Units.Items.Add(unit);
            }
            combobox_Units.Text = "hr";
        }

        private void setupGridView()
        {
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "Name";
            col1.Width = 120;

            DataGridViewCheckBoxColumn col2 = new DataGridViewCheckBoxColumn();
            col2.Name = "Raster NS";
            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader; 
            
            DataGridViewCheckBoxColumn col3 = new DataGridViewCheckBoxColumn();
            col3.Name = "Raster EW";
            col3.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            DataGridViewCheckBoxColumn col4 = new DataGridViewCheckBoxColumn();
            col4.Name = "ExpandingSquare";
            col4.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dataGridView1.Columns.AddRange(col1, col2, col3, col4);
            dataGridView1.AllowUserToAddRows = false;
            
        }
        private void AddGridRow(string siteName)
        {
            DataGridViewRow gcsRow = new DataGridViewRow();
            DataGridViewTextBoxCell gcsNameCell = new DataGridViewTextBoxCell();
            gcsNameCell.Value = siteName;

            if (siteName.Contains("AreaTarget"))
            {
                DataGridViewCheckBoxCell rasterNS = new DataGridViewCheckBoxCell();
                rasterNS.Value = true;
                DataGridViewCheckBoxCell rasterEW = new DataGridViewCheckBoxCell();
                rasterEW.Value = false;
                DataGridViewCheckBoxCell expandSq = new DataGridViewCheckBoxCell();
                expandSq.Value = false;
                gcsRow.Cells.AddRange(gcsNameCell, rasterNS, rasterEW, expandSq);
            }
            else
            {
                DataGridViewCheckBoxCell rasterNS = new DataGridViewCheckBoxCell();                
                DataGridViewCheckBoxCell rasterEW = new DataGridViewCheckBoxCell();                
                DataGridViewCheckBoxCell expandSq = new DataGridViewCheckBoxCell();                
                gcsRow.Cells.AddRange(gcsNameCell, rasterNS, rasterEW, expandSq);
                rasterNS.Style.BackColor = Color.Gray;
                rasterEW.Style.BackColor = Color.Gray;
                expandSq.Style.BackColor = Color.Gray; 
                rasterNS.ReadOnly = true;
                rasterEW.ReadOnly = true;
                expandSq.ReadOnly = true;
            }           

            dataGridView1.Rows.Add(gcsRow);
        }
        
        private void button_AddObject_Click(object sender, EventArgs e)
        {
            AddGridRow(treeView_availableObjs.SelectedNode.Tag as string);
        }

        private void treeView_availableObjs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddGridRow(treeView_availableObjs.SelectedNode.Tag as string);
        }

        private void button_RemoveObject_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }
            else if (dataGridView1.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell item in dataGridView1.SelectedCells)
                {
                    dataGridView1.Rows.RemoveAt(item.RowIndex);
                }
            }
            
        }

        private void button_moveUp_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                rowIndex = dataGridView1.SelectedRows[0].Index;
            }
            else if (dataGridView1.SelectedCells.Count > 0)
            {
                rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            }
            if (rowIndex != -1)
            {
                DataGridViewRow aboveRow = dataGridView1.Rows[rowIndex - 1];
                dataGridView1.Rows.Remove(aboveRow);
                dataGridView1.Rows.Insert(rowIndex, aboveRow);
            }

        }

        private void button_moveDown_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                rowIndex = dataGridView1.SelectedRows[0].Index;
            }
            else if (dataGridView1.SelectedCells.Count > 0)
            {
                rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            }
            if (rowIndex != -1)
            {
                DataGridViewRow belowRow = dataGridView1.Rows[rowIndex + 1];
                dataGridView1.Rows.Remove(belowRow);
                dataGridView1.Rows.Insert(rowIndex, belowRow);
            }
        }

        private void RadioChanged()
        {
            if (radioButton_GrnElev.Checked)
            {
                swathType = RasterSearch.SwathWidthType.GroundElev;
                swathParameter = double.Parse(textBox_GrnElev.Text);
            }
            else if (radioButton_SensorFOV.Checked)
            {
                swathType = RasterSearch.SwathWidthType.SensorFOV;
                swathParameter = double.Parse(textBox_SensorFOV.Text);
            }
            else if (radioButton_SlantRange.Checked)
            {
                swathType = RasterSearch.SwathWidthType.SlantRange;
                swathParameter = double.Parse(textBox_SlantRange.Text);
            }
            else
            {
                swathType = RasterSearch.SwathWidthType.NumberOfPasses;
                swathParameter = double.Parse(textBox_numPasses.Text);
            }
            textBox_GrnElev.Enabled = radioButton_GrnElev.Checked;
            textBox_SensorFOV.Enabled = radioButton_SensorFOV.Checked;
            textBox_SlantRange.Enabled = radioButton_SlantRange.Checked;
            textBox_numPasses.Enabled = radioButton_NumPasses.Checked;
        }

        private void PopulateListViews()
        {
            ImageList objects = new ImageList();
            objects.Images.Add(Agi.Ui.GreatArc.Stk12.Properties.Resources.AreaTarget);
            objects.Images.Add(Agi.Ui.GreatArc.Stk12.Properties.Resources.Facility);
            objects.Images.Add(Agi.Ui.GreatArc.Stk12.Properties.Resources.Place);
            objects.Images.Add(Agi.Ui.GreatArc.Stk12.Properties.Resources.Target);

            treeView_availableObjs.ImageList = objects;

            foreach (IAgStkObject obj in root.CurrentScenario.Children.GetElements(AgESTKObjectType.eAreaTarget))
            {
                TreeNode newnode = treeView_availableObjs.Nodes.Add(obj.InstanceName);
                newnode.Tag = obj.ClassName + "/" + obj.InstanceName;
                newnode.ImageIndex = 0;
                newnode.SelectedImageIndex = 0;
                newnode.StateImageIndex = 0;
            }
            foreach (IAgStkObject obj in root.CurrentScenario.Children.GetElements(AgESTKObjectType.eFacility))
            {
                TreeNode newnode = treeView_availableObjs.Nodes.Add(obj.InstanceName);
                newnode.Tag = obj.ClassName + "/" + obj.InstanceName; 
                newnode.ImageIndex = 1;
                newnode.SelectedImageIndex = 1;
                newnode.StateImageIndex = 1;
            } 
            foreach (IAgStkObject obj in root.CurrentScenario.Children.GetElements(AgESTKObjectType.ePlace))
            {
                TreeNode newnode = treeView_availableObjs.Nodes.Add(obj.InstanceName);
                newnode.Tag = obj.ClassName + "/" + obj.InstanceName; 
                newnode.ImageIndex = 2;
                newnode.SelectedImageIndex = 2;
                newnode.StateImageIndex = 2;
            }
            foreach (IAgStkObject obj in root.CurrentScenario.Children.GetElements(AgESTKObjectType.eTarget))
            {
                TreeNode newnode = treeView_availableObjs.Nodes.Add(obj.InstanceName);
                newnode.Tag = obj.ClassName + "/" + obj.InstanceName; 
                newnode.ImageIndex = 3;
                newnode.SelectedImageIndex = 3;
                newnode.StateImageIndex = 3;
            }
            
        }

        private bool Licensing(string licenseSearch)
        {
            bool hasLicense = false;

            AGI.STKUtil.IAgExecCmdResult result = root.ExecuteCommand("GetLicenses /");
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


        private IAgAircraft CreateRoute()
        {
            double speed = double.Parse(textBox_Speed.Text) * 0.000514444444; //knots to m/s
            double acturnGs = double.Parse(textBox_TurnGs.Text);
            double altitude = double.Parse(textBox_Altitude.Text) * 0.0003048;// ft to meters
            double turnRadius = (((speed * 1000) * (speed * 1000)) / (acturnGs * 9.81)) / 1000; //  m/s to km
            RadioChanged();
            List<RasterSearch.Waypoint> waypoints = new List<RasterSearch.Waypoint>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                IAgStkObject thisObj = root.GetObjectFromPath("*/"  + row.Cells[0].Value.ToString());

                switch (thisObj.ClassType)
                {

                    case AgESTKObjectType.eAreaTarget:
                        List<RasterSearch.Waypoint> searchWaypoints = new List<RasterSearch.Waypoint>();
                        if (row.Cells[1].Value.ToString() == "True")
                        {
                            searchWaypoints = routeBuilder.RasterSearchWaypointGenerator(
                                thisObj.Path, "NorthSouth", speed, altitude, turnRadius, swathType, swathParameter);
                            foreach (RasterSearch.Waypoint thisWaypoint in searchWaypoints)
                            {
                                waypoints.Add(thisWaypoint);
                            }
                        }
                        if (row.Cells[2].Value.ToString() == "True")
                        {
                            searchWaypoints = routeBuilder.RasterSearchWaypointGenerator(
                                   thisObj.Path, "EastWest", speed, altitude, turnRadius, swathType, swathParameter);
                            foreach (RasterSearch.Waypoint thisWaypoint in searchWaypoints)
                            {
                                waypoints.Add(thisWaypoint);
                            }
                        }
                        if (row.Cells[3].Value.ToString() == "True")
                        {
                            searchWaypoints = routeBuilder.ExpandingSquareWaypointGenerator(
                                thisObj.Path, speed, altitude, turnRadius, swathType, swathParameter);
                            foreach (RasterSearch.Waypoint thisWaypoint in searchWaypoints)
                            {
                                waypoints.Add(thisWaypoint);
                            }
                        }
                        
                        break;
                    case AgESTKObjectType.eFacility:
                    case AgESTKObjectType.ePlace:
                    case AgESTKObjectType.eTarget:
                        waypoints.Add(routeBuilder.LocationWaypointGenerator(thisObj.Path, altitude, speed, turnRadius));
                        break;

                    default:
                        break;
                }

            }
            bool createSensor = radioButton_SensorFOV.Checked;
            double sensorParameter = double.Parse(textBox_SensorFOV.Text);

            IAgAircraft aircraft = routeBuilder.CreateAircraft("AutoRoute", createSensor, sensorParameter, double.Parse(textBox_Speed.Text));
            routeBuilder.WaypointsToGreatArc(aircraft, waypoints, checkBox_useTakeoffLanding.Checked);
            return aircraft;
        }

        private void checkBox_UseEndurance_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Endurance.Enabled = checkBox_UseEndurance.Checked;
            combobox_Units.Enabled = checkBox_UseEndurance.Checked;
        }







    }
}
