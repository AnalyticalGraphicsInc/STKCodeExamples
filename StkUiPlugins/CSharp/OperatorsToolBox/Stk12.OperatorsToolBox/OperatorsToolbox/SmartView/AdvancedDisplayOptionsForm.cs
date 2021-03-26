using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox.SmartView
{
    public partial class AdvancedDisplayOptionsForm : Form
    {
        private StkObjectsLibrary _mStkObjectsLibrary;
        private bool _fromEdit;
        public ViewData currentView;
        public AdvancedDisplayOptionsForm(ViewData data, bool fromEdit)
        {
            InitializeComponent();
            _fromEdit = fromEdit;
            if (_fromEdit)
            {
                RemoveSensorViewLink.Visible = true;
                RemoveSensorViewLink.Enabled = true;
            }
            else
            {
                RemoveSensorViewLink.Visible = false;
                RemoveSensorViewLink.Enabled = false;
            }
            currentView = data;
            #region initialize boxes
            DataDisplayOptions.Enabled = false;
            EllipsoidDefinition.Enabled = false;
            PlaneSettings.Enabled = false;
            GeoBoxOptions.Enabled = false;
            if (data.EllipsoidX != null)
            {
                EllipsoidX.Text = data.EllipsoidX;
            }
            else
            {
                EllipsoidX.Text = "100";
            }

            if (data.EllipsoidY != null)
            {
                EllipsoidY.Text = data.EllipsoidY;
            }
            else
            {
                EllipsoidY.Text = "100";
            }

            if (data.EllipsoidZ != null)
            {
                EllipsoidZ.Text = data.EllipsoidZ;
            }
            else
            {
                EllipsoidZ.Text = "100";
            }

            if (data.ProxGridSpacing != null)
            {
                GridSpacing.Text = data.ProxGridSpacing;
            }
            else
            {
                GridSpacing.Text = "50";
            }


            if (data.VectorScalingValue != null)
            {
                VectorScalingFactor.Text = data.VectorScalingValue.ToString();
            }
            else
            {
                VectorScalingFactor.Text = "3";
            }

            if (data.GeoEastWest != null)
            {
                GEOEastWest.Text = data.GeoEastWest;
            }
            else
            {
                GEOEastWest.Text = "50";
            }

            if (data.GeoLongitude != null)
            {
                GEOLongitude.Text = data.GeoLongitude;
            }
            else
            {
                GEOLongitude.Text = "-100";
            }

            if (data.GeoNorthSouth != null)
            {
                GeoNorthSouth.Text = data.GeoNorthSouth;
            }
            else
            {
                GeoNorthSouth.Text = "0.05";
            }

            if (data.GeoNorthSouth != null)
            {
                GeoNorthSouth.Text = data.GeoNorthSouth;
            }
            else
            {
                GeoNorthSouth.Text = "0.05";
            }

            if (data.GeoRadius != null)
            {
                GEORadius.Text = data.GeoRadius;
            }
            else
            {
                GEORadius.Text = "42166.3";
            }

            if (data.TimeStep != null)
            {
                TimeStep.Text = data.TimeStep;
            }
            else
            {
                TimeStep.Text = "1";
            }

            if (data.ApplyVectorScaling)
            {
                UseVectorScaling.Checked = true;
            }
            if (data.SecondaryDataDisplay.DataDisplayActive)
            {
                UseSecondaryDataDisplay.Checked = true;
            }
            if (data.EnableGeoBox)
            {
                UseGEOBox.Checked = true;
            }
            if (data.EnableProximityBox)
            {
                UseProxBox.Checked = true;
            }
            if (data.EnableProximityEllipsoid)
            {
                EnableEllipsoid.Checked = true;
            }
            if (data.OverrideTimeStep)
            {
                OverrideTimeStep.Checked = true;
            }

            #endregion
            PopulateComboBoxs();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            if (UseVectorScaling.Checked)
            {
                currentView.ApplyVectorScaling = true;
            }
            else
            {
                currentView.ApplyVectorScaling = false;
            }
            currentView.VectorScalingValue = Double.Parse(VectorScalingFactor.Text);

            if (UseProxBox.Checked)
            {
                currentView.EnableProximityBox = true;
            }
            else
            {
                currentView.EnableProximityBox = false;
            }
            currentView.ProxGridSpacing = GridSpacing.Text;

            if (EnableEllipsoid.Checked)
            {
                currentView.EnableProximityEllipsoid = true;
            }
            else
            {
                currentView.EnableProximityEllipsoid = false;
            }
            currentView.EllipsoidX = EllipsoidX.Text;
            currentView.EllipsoidY = EllipsoidY.Text;
            currentView.EllipsoidZ = EllipsoidZ.Text;

            if (UseGEOBox.Checked)
            {
                currentView.EnableGeoBox = true;
            }
            else
            {
                currentView.EnableGeoBox = false;
            }
            currentView.GeoEastWest = GEOEastWest.Text;
            currentView.GeoNorthSouth = GeoNorthSouth.Text;
            currentView.GeoRadius = GEORadius.Text;
            currentView.GeoLongitude = GEOLongitude.Text;

            if (UseSecondaryDataDisplay.Checked)
            {
                string className = null;
                currentView.SecondaryDataDisplay.DataDisplayActive = true;
                currentView.SecondaryDataDisplay.DataDisplayLocation = DisplayLocation.Text;
                currentView.SecondaryDataDisplay.DataDisplayReportName = DisplayReport.Text;
                className = SmartViewFunctions.GetClassName(DisplayObject.Text);
                currentView.SecondaryDataDisplay.DataDisplayObject = className + "/" + DisplayObject.Text;
                className = SmartViewFunctions.GetClassName(PredataObject.Text);
                currentView.SecondaryDataDisplay.PredataObject = className + "/" + PredataObject.Text;
            }
            else
            {
                currentView.SecondaryDataDisplay.DataDisplayActive = false;
            }
            if (OverrideTimeStep.Checked)
            {
                currentView.OverrideTimeStep = true;
                currentView.TimeStep = TimeStep.Text;
            }
            else
            {
                currentView.OverrideTimeStep = false;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private int FieldCheck()
        {
            int check = 0;

            return check;
        }

        private void PopulateDdObjects(ComboBox combo, bool includeEarth)
        {
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            combo.Items.Clear();
            string simpleName;
            string className;
            StringCollection objectNames = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            if (includeEarth)
            {
                combo.Items.Add("Earth");
            }
            foreach (string objectName in objectNames)
            {
                className = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "Sensor" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    combo.Items.Add(simpleName);
                    if (_fromEdit)
                    {
                        if (className + "/" + simpleName == CommonData.SavedViewList[CommonData.SelectedIndex].SecondaryDataDisplay.DataDisplayObject)
                        {
                            combo.SelectedIndex = combo.Items.Count - 1;
                        }
                    }
                }
            }
            if (combo.SelectedIndex == -1)
            {
                combo.SelectedIndex = 0;
            }

        }

        private void PopulatePreDataObjects(ComboBox combo, bool includeEarth)
        {
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            combo.Items.Clear();
            string simpleName;
            string className;
            StringCollection objectNames = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            if (includeEarth)
            {
                combo.Items.Add("Earth");
                if ("CentralBody/Earth" == CommonData.SavedViewList[CommonData.SelectedIndex].ViewTarget)
                {
                    combo.SelectedIndex = 0;
                }
            }
            combo.Items.Add("None");
            foreach (string objectName in objectNames)
            {
                className = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "Sensor" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    combo.Items.Add(simpleName);
                    if (_fromEdit)
                    {
                        if (className + "/" + simpleName == CommonData.SavedViewList[CommonData.SelectedIndex].SecondaryDataDisplay.PredataObject)
                        {
                            combo.SelectedIndex = combo.Items.Count - 1;
                        }
                    }
                }
            }

            if (combo.SelectedIndex == -1)
            {
                combo.SelectedIndex = 0;
            }
        }

        private void PopulateComboBoxs()
        {
            DisplayLocation.Items.Add("TopLeft");
            DisplayLocation.Items.Add("TopCenter");
            DisplayLocation.Items.Add("TopRight");
            DisplayLocation.Items.Add("CenterLeft");
            DisplayLocation.Items.Add("Center");
            DisplayLocation.Items.Add("CenterRight");
            DisplayLocation.Items.Add("BottomLeft");
            DisplayLocation.Items.Add("BottomCenter");
            DisplayLocation.Items.Add("BottomRight");
            int count = 0;
            foreach (var item in DisplayLocation.Items)
            {
                if (item.ToString() == currentView.SecondaryDataDisplay.DataDisplayLocation)
                {
                    DisplayLocation.SelectedIndex = count;
                }
                count++;
            }
            if (DisplayLocation.SelectedIndex == -1)
            {
                DisplayLocation.SelectedIndex = 0;
            }
            PopulatePreDataObjects(PredataObject, false);
            PopulateDdObjects(DisplayObject, false);

            if (DisplayReport.Items.Count>0 && !string.IsNullOrEmpty(currentView.SecondaryDataDisplay.DataDisplayReportName))
            {
                count = 0;
                foreach (var item in DisplayReport.Items)
                {
                    if (item.ToString() == currentView.SecondaryDataDisplay.DataDisplayReportName)
                    {
                        DisplayReport.SelectedIndex = count;
                    }
                    count++;
                }
            }

            if (currentView.ApplyVectorScaling)
            {
                UseVectorScaling.Checked = true;
                VectorScalingFactor.Text = currentView.VectorScalingValue.ToString();
            }
            else
            {
                VectorScalingFactor.Text = "1.2";
                VectorScalingFactor.Enabled = false;
            }

        }

        private void PopulateDisplayReports(ComboBox box, ComboBox ddObject)
        {
            if (ddObject.SelectedIndex != -1)
            {
                string className = SmartViewFunctions.GetClassName(ddObject.Text);
                IAgVODataDisplayCollection ddCollection = null;
                if (className == "Satellite")
                {
                    IAgSatellite myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgSatellite;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "Aircraft")
                {
                    IAgAircraft myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgAircraft;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "Facility")
                {
                    IAgFacility myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgFacility;
                    ddCollection = myObject.VO.DataDisplays;
                }
                else if (className == "Missile")
                {
                    IAgMissile myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgMissile;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "GroundVehicle")
                {
                    IAgGroundVehicle myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgGroundVehicle;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "LaunchVehicle")
                {
                    IAgLaunchVehicle myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgLaunchVehicle;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "Place")
                {
                    IAgPlace myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgPlace;
                    ddCollection = myObject.VO.DataDisplays;
                }
                else if (className == "Target")
                {
                    IAgTarget myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + ddObject.Text) as IAgTarget;
                    ddCollection = myObject.VO.DataDisplays;
                }
                box.Items.Clear();
                Array reportNames = ddCollection.AvailableData;
                foreach (var name in reportNames)
                {
                    box.Items.Add(name);
                }
                box.SelectedIndex = 0;
            }


        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UseProxBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UseProxBox.Checked)
            {
                PlaneSettings.Enabled = true;
            }
            else
            {
                PlaneSettings.Enabled = false;
            }
        }

        private void EnableEllipsoid_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableEllipsoid.Checked)
            {
                EllipsoidDefinition.Enabled = true;
            }
            else
            {
                EllipsoidDefinition.Enabled = false;
            }
        }

        private void UseGEOBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UseGEOBox.Checked)
            {
                GeoBoxOptions.Enabled = true;
            }
            else
            {
                GeoBoxOptions.Enabled = false;
            }
        }

        private void UseSecondaryDataDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (UseSecondaryDataDisplay.Checked)
            {
                DataDisplayOptions.Enabled = true;
            }
            else
            {
                DataDisplayOptions.Enabled = false;
            }
        }

        private void PopulateObjects(ComboBox combo, bool includeEarth)
        {
            _mStkObjectsLibrary = new StkObjectsLibrary();
            combo.Items.Clear();
            string simpleName;
            string className;
            StringCollection objectNames = _mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            if (includeEarth)
            {
                combo.Items.Add("Earth");
            }
            foreach (string objectName in objectNames)
            {
                className = _mStkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "Sensor" && className != "Antenna" && className != "Transmitter" && className != "Receiver" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Radar" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = _mStkObjectsLibrary.ObjectName(objectName);

                    combo.Items.Add(simpleName);
                }
            }

        }

        private void DisplayObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDisplayReports(DisplayReport,DisplayObject);
        }

        private void UseVectorScaling_CheckedChanged(object sender, EventArgs e)
        {
            if (UseVectorScaling.Checked)
            {
                VectorScalingFactor.Enabled = true;
            }
            else
            {
                VectorScalingFactor.Enabled = false;
            }
        }

        private void RemoveSensorViewLink_Click(object sender, EventArgs e)
        {
            currentView.LinkToSensorView  = false;
        }

        private void OverrideTimeStep_CheckedChanged(object sender, EventArgs e)
        {
            if (OverrideTimeStep.Checked)
            {
                TimeStep.Enabled = true;
            }
            else
            {
                TimeStep.Enabled = false;
            }
        }
    }
}
