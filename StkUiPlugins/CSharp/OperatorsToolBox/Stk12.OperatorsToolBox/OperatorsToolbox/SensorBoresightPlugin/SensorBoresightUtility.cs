using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox.SensorBoresightPlugin
{
    public partial class SensorBoresightUtility : OpsPluginControl
    {
        private SensorViewClass sensorViewCreator;
        private List<string> sensorPaths;
        public SensorBoresightUtility()
        {
            InitializeComponent();
            sensorPaths = CreatorFunctions.PopulateListByClass("Sensor");
            IAgSensor sensor;
            foreach (var item in sensorPaths)
            {
                sensor = CommonData.StkRoot.GetObjectFromPath(item) as IAgSensor;
                if (sensor.PatternType == AgESnPattern.eSnSimpleConic || sensor.PatternType == AgESnPattern.eSnRectangular || sensor.PatternType == AgESnPattern.eSnEOIR)
                {
                    SelectedSensor.Items.Add(((IAgStkObject)sensor).InstanceName);
                }
            }
            if (SelectedSensor.Items.Count > 0)
            {
                SelectedSensor.SelectedIndex = 0;
            }

            if (CommonData.SavedViewList != null)
            {
                foreach (var item in CommonData.SavedViewList)
                {
                    if (item.ViewType == "3D")
                    {
                        SelectedView.Items.Add(item.Name);
                    }
                }
                if (SelectedView.Items.Count > 0)
                {
                    SelectedView.SelectedIndex = 0;
                }
            }

            UpVector.Items.Add("X");
            UpVector.Items.Add("-X");
            UpVector.Items.Add("Y");
            UpVector.Items.Add("-Y");
            UpVector.SelectedIndex = 0;
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void buttonCreateSensorView_Click_1(object sender, EventArgs e)
        {
            IAgStkObject sensor = null;
            SensorViewData viewData = new SensorViewData();
            foreach (var item in sensorPaths)
            {
                if (item.Contains(SelectedSensor.Text))
                {
                    sensor = CommonData.StkRoot.GetObjectFromPath(item);
                    viewData.SelectedSensor = item;
                    break;
                }
            }
            if (sensor != null)
            {
                sensorViewCreator = new SensorViewClass(CommonData.StkRoot, sensor);
                double vertPixdb;
                if (Double.TryParse(textboxPixels.Text, out vertPixdb))
                {

                }
                else
                {
                    vertPixdb = 400;
                }

                if (AutoUpVector.Checked)
                {
                    sensorViewCreator.CreateSensorWindow(Convert.ToInt16(vertPixdb));
                }
                else
                {
                    sensorViewCreator.CreateSensorWindow(Convert.ToInt16(vertPixdb),UpVector.Text);
                    viewData.AutoUpVector = true;
                    viewData.UpVector = UpVector.Text;
                }
                viewData.VertWinSize = Convert.ToInt16(vertPixdb);

                if (cbCompass.Checked)
                {
                    sensorViewCreator.EnableCompass();
                    viewData.ShowCompass = true;
                }
                if (cbLLA.Checked)
                {
                    sensorViewCreator.EnableLLA();
                    viewData.ShowLatLon = true;
                }
                if (cbRulers.Checked)
                {
                    sensorViewCreator.EnableRulers();
                    viewData.ShowRulers = true;
                }
                if (cbCrosshairs.Checked)
                {
                    viewData.ShowCrosshairs = true;
                    if (rbSquare.Checked)
                    {
                        sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Square);
                        viewData.CrosshairType = SensorViewClass.CrosshairType.Square;
                    }
                    else if (rbGrid.Checked)
                    {
                        sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Grid);
                        viewData.CrosshairType = SensorViewClass.CrosshairType.Grid;
                    }
                    else if (rbCircular.Checked)
                    {
                        sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Circular);
                        viewData.CrosshairType = SensorViewClass.CrosshairType.Circular;
                    }
                }
                if (LinkToView.Checked && SelectedView.SelectedIndex != -1)
                {
                    viewData.LinkToView = true;
                    viewData.ViewName = SelectedView.Text;
                    foreach (var item in CommonData.SavedViewList)
                    {
                        if (item.Name == SelectedView.Text)
                        {
                            item.LinkToSensorView = true;
                            item.SensorBoresightData = viewData;
                            try
                            {
                                ReadWrite.WriteSavedViews(CommonData.DirectoryStr + "\\StoredViewData.json");
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Could not Write Stored Views File");
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void cbCrosshairs_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCrosshairs.Checked)
            {
                gboxCrosshairType.Enabled = true;
            }
            else
            {
                gboxCrosshairType.Enabled = false;
            }
        }

        private void cbCompass_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LinkToView_CheckedChanged(object sender, EventArgs e)
        {
            if (LinkToView.Checked)
            {
                SelectedView.Enabled = true;
            }
            else
            {
                SelectedView.Enabled = false;
            }
        }

        private void AutoUpVector_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoUpVector.Checked)
            {
                UpVector.Enabled = false;
            }
            else
            {
                UpVector.Enabled = true;
            }
        }
    }
}
