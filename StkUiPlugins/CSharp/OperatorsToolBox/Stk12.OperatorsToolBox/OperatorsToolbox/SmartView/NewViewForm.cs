using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.SmartView
{
    public partial class NewViewForm : Form
    {
        private StkObjectsLibrary _mStkObjectsLibrary;
        private List<string> _threatNames;
        public NewViewForm()
        {
            InitializeComponent();
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            CommonData.CurrentViewObjectData = SmartViewFunctions.GetObjectData();
            _threatNames = new List<string>();

            PopulateComboBoxes();
            PopulateObjects(FocusedItem, true);
            if (FocusedItem.Items.Count!=0)
            {
                FocusedItem.SelectedIndex = 0;
            }
            PopulateObjects(DisplayObject, false);
            if (DisplayObject.Items.Count!=0)
            {
                DisplayObject.SelectedIndex = 0;
            }
            PopulateObjects(ObjectName2D, false);
            if (ObjectName2D.Items.Count!=0)
            {
                ObjectName2D.SelectedIndex = 0;
            }
            DataDisplayOptions.Enabled = false;
            TTDataDisplayOptions.Enabled = false;
            GEODataDisplayOptions.Enabled = false;
            GeoBoxOptions.Enabled = false;
            HideShowOptions.Enabled = false;
            EllipsoidDefinition.Enabled = false;
            PopulateObjects(GEODisplayObject, false);

            mStkObjectsLibrary = new StkObjectsLibrary();
        }

        private void TypeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            WindowSelect.Items.Clear();
            List<string> windowNames = new List<string>();
            if (TypeSelect.SelectedIndex==0)
            {
                windowNames = SmartViewFunctions.GetWindowNames(1);
                ViewDefinitionBox2D.Enabled = false;
                ViewDefinitionBox2D.Visible = false;
                ViewDefinitionBox3D.Enabled = true;
                ViewDefinitionBox3D.Visible = true;
                TTDefinitionBox.Visible = false;
                TTDefinitionBox.Enabled = false;
                GEODriftDefinitionBox.Enabled = false;
                GEODriftDefinitionBox.Visible = false;
                UseCurrentViewPoint.Enabled = true;
                UseCurrentViewPoint.Visible = true;
            }
            else if (TypeSelect.SelectedIndex == 1)
            {
                windowNames = SmartViewFunctions.GetWindowNames(0);
                ViewDefinitionBox3D.Enabled = false;
                ViewDefinitionBox3D.Visible = false;
                ViewDefinitionBox2D.Enabled = true;
                ViewDefinitionBox2D.Visible = true;
                TTDefinitionBox.Visible = false;
                TTDefinitionBox.Enabled = false;
                GEODriftDefinitionBox.Enabled = false;
                GEODriftDefinitionBox.Visible = false;
                UseCurrentViewPoint.Enabled = false;
                UseCurrentViewPoint.Visible = false;
            }
            else if (TypeSelect.SelectedIndex == 2)
            {
                windowNames = SmartViewFunctions.GetWindowNames(1);
                ViewDefinitionBox2D.Enabled = false;
                ViewDefinitionBox2D.Visible = false;
                ViewDefinitionBox3D.Enabled = false;
                ViewDefinitionBox3D.Visible = false;
                TTDefinitionBox.Visible = true;
                TTDefinitionBox.Enabled = true;
                GEODriftDefinitionBox.Enabled = false;
                GEODriftDefinitionBox.Visible = false;
                UseCurrentViewPoint.Enabled = false;
                UseCurrentViewPoint.Visible = false;
            }
            else if (TypeSelect.SelectedIndex == 3)
            {
                windowNames = SmartViewFunctions.GetWindowNames(1);
                ViewDefinitionBox2D.Enabled = false;
                ViewDefinitionBox2D.Visible = false;
                ViewDefinitionBox3D.Enabled = false;
                ViewDefinitionBox3D.Visible = false;
                TTDefinitionBox.Visible =false;
                TTDefinitionBox.Enabled = false;
                GEODriftDefinitionBox.Enabled = true;
                GEODriftDefinitionBox.Visible = true;
                UseCurrentViewPoint.Enabled = false;
                UseCurrentViewPoint.Visible = false;
            }
            foreach (var item in windowNames)
            {
                WindowSelect.Items.Add(item);
            }
            //try/catch for case when no windows of a certain type exist
            try
            {
                WindowSelect.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void WindowSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Create_Click(object sender, EventArgs e)
        {
            string className;
            ViewData current = new ViewData();
            int check;
            if (TypeSelect.SelectedIndex==0) //3D
            {
                check = FieldCheck3D();
                if (check==0)
                {
                    current.WindowName = WindowSelect.Text;
                    current.WindowId = SmartViewFunctions.GetWindowId(WindowSelect.Text, 1);
                    current.Name = ViewName3D.Text;
                    current.ViewType = TypeSelect.Text;
                    className = SmartViewFunctions.GetClassName(FocusedItem.Text);
                    current.ViewTarget = className+"/"+FocusedItem.Text;
                    current.ViewAxes = ViewType.Text;

                    if (EnableUniversalOrbitTrack.Checked)
                    {
                        current.EnableUniversalOrbitTrack = true;
                        if (LeadType3D.Text == "Time")
                        {
                            current.LeadType = LeadType3D.Text + " " + OrbitLeadTime.Text;
                            current.LeadTime = OrbitLeadTime.Text;
                        }
                        else
                        {
                            current.LeadType = LeadType3D.Text;
                        }

                        if (TrailType3D.Text == "Time")
                        {
                            current.TrailType = TrailType3D.Text + " " + OrbitTrailTime.Text;
                            current.TrailTime = OrbitTrailTime.Text;
                        }
                        else
                        {
                            current.TrailType = TrailType3D.Text;
                        }
                    }
                    else
                    {
                        current.LeadType = "None";
                        current.TrailType = "None";
                    }

                    if (UseDataDisplay.Checked)
                    {
                        current.DataDisplayActive = true;
                        current.DataDisplayLocation = DisplayLocation.Text;
                        className = SmartViewFunctions.GetClassName(DisplayObject.Text);
                        current.DataDisplayObject = className+"/"+DisplayObject.Text;
                        current.DataDisplayReportName = DisplayReport.Text;
                    }
                    else
                    {
                        current.DataDisplayActive = false;
                        current.DataDisplayLocation = DisplayLocation.Text;
                        className = SmartViewFunctions.GetClassName(DisplayObject.Text);
                        current.DataDisplayObject = className + "/" + DisplayObject.Text;
                        current.DataDisplayReportName = DisplayReport.Text;
                    }

                    if (UseCurrentViewPoint.Checked)
                    {
                        current.UseStoredView = true;
                        current.StoredViewName = ViewName3D.Text;
                        CommonData.StkRoot.ExecuteCommand("VO * SaveStoredView \""+ViewName3D.Text+"\" "+current.WindowId);
                    }
                    else
                    {
                        current.UseStoredView = false;
                        current.StoredViewName = "None";
                    }

                }
            }
            else if(TypeSelect.SelectedIndex==1) //2D
            {
                check = FieldCheck2D();
                if (check == 0)
                {
                    current.WindowName = WindowSelect.Text;
                    current.WindowId = SmartViewFunctions.GetWindowId(WindowSelect.Text, 0);
                    current.Name = ViewName2D.Text;
                    current.ViewType = TypeSelect.Text;
                    current.ViewType2D = TypeSelect2D.Text;
                    className = SmartViewFunctions.GetClassName(ObjectName2D.Text);
                    current.ViewTarget = className + "/" + ObjectName2D.Text;
                    current.ZoomCenterLat = ZoomCenterLat.Text;
                    current.ZoomCenterLong = ZoomCenterLong.Text;
                    current.ZoomCenterDelta = ZoomDelta.Text;

                    if (EnableUniversalGroundTrack.Checked)
                    {
                        if (LeadType2D.Text == "Time")
                        {
                            current.LeadType = LeadType2D.Text + " " + GroundLeadTime.Text;
                            current.LeadTime = GroundLeadTime.Text;
                        }
                        else
                        {
                            current.LeadType = LeadType2D.Text;
                        }

                        if (TrailType2D.Text == "Time")
                        {
                            current.TrailType = TrailType2D.Text + " " + GroundTrailTime.Text;
                            current.TrailTime = GroundTrailTime.Text;
                        }
                        else
                        {
                            current.TrailType = TrailType2D.Text;
                        }
                    }
                    else
                    {
                        current.LeadType = "None";
                        current.TrailType = "None";
                    }

                    current.ShowAerialSensors = false;

                    current.ShowGroundSensors = false;

                }
            }
            else if (TypeSelect.SelectedIndex==2) //Target/Threat
            {
                current.WindowName = WindowSelect.Text;
                current.ViewType = "Target/Threat";
                current.ThreatSatNames = _threatNames;
                className = SmartViewFunctions.GetClassName(TargetSatellite.Text);
                current.TargetSatellite = className + "/" + TargetSatellite.Text;
                current.Name = TTViewName.Text;

                if (TTUseDataDisplay.Checked)
                {
                    current.TtDataDisplayActive = true;
                    current.DataDisplayActive = true;
                    current.TtDataDisplayLocation = TTDisplayLocation.Text;
                    if (TTDisplayObject.SelectedIndex == 0)
                    {
                        className = SmartViewFunctions.GetClassName(TargetSatellite.Text);
                        current.TtDataDisplayObject = className + "/" + TargetSatellite.Text;
                        current.TtDataDisplayReportName = TTDisplayReport.Text;
                    }
                    else if (TTDisplayObject.SelectedIndex == 1)
                    {
                        current.TtDataDisplayObject = "AllThreat";
                        current.TtDataDisplayReportName = TTDisplayReport.Text;
                    }
                    else
                    {
                        className = SmartViewFunctions.GetClassName(TTDisplayObject.Text);
                        current.TtDataDisplayObject = className + "/" + TTDisplayObject.Text;
                        current.TtDataDisplayReportName = TTDisplayReport.Text;
                    }
                }
                else
                {
                    current.TtDataDisplayActive = false;
                    current.DataDisplayActive = false;
                    current.TtDataDisplayLocation = TTDisplayLocation.Text;
                    className = SmartViewFunctions.GetClassName(TTDisplayObject.Text);
                    current.TtDataDisplayObject = className + "/" + TTDisplayObject.Text;
                    current.TtDataDisplayReportName = TTDisplayReport.Text;
                }

                if (UseProxBox.Checked)
                {
                    current.EnableProximityBox = true;
                }
                else
                {
                    current.EnableProximityBox = false;
                }

                if (EnableEllipsoid.Checked)
                {
                    current.EnableProximityEllipsoid = true;
                    current.EllipsoidX = EllipsoidX.Text;
                    current.EllipsoidY = EllipsoidX.Text;
                    current.EllipsoidZ = EllipsoidX.Text;
                }
                else
                {
                    current.EnableProximityEllipsoid = false;
                    current.EllipsoidX = "100";
                    current.EllipsoidY = "100";
                    current.EllipsoidZ = "100";
                }

            }
            else if (TypeSelect.SelectedIndex==3) //GEO Drift
            {
                current.WindowName = WindowSelect.Text;
                current.ViewType = "GEODrift";
                current.Name = GEOViewName.Text;
                className = SmartViewFunctions.GetClassName(GEOViewTarget.Text);
                current.ViewTarget = className + "/" + GEOViewTarget.Text;
                current.EnableGeoBox = UseGEOBox.Checked;
                current.GeoLongitude = GEOLongitude.Text;
                current.GeoNorthSouth = GeoNorthSouth.Text;
                current.GeoEastWest = GEOEastWest.Text;
                current.GeoRadius = GEORadius.Text;
                current.GeoDataDisplayActive = GEOUseDataDisplay.Checked;
                className = SmartViewFunctions.GetClassName(GEODisplayObject.Text);
                current.GeoDataDisplayObject = className + "/" + GEODisplayObject.Text;
                current.GeoDataDisplayReportName = GEODisplayReport.Text;
                current.GeoDataDisplayLocation = GEODisplayLocation.Text;
            }

            if (UseCurrentTime.Checked)
            {
                IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
                double currentTime = animationRoot.CurrentTime;
                current.AnimationTime = currentTime.ToString();
            }
            else
            {
                current.UseAnimationTime = false;
                IAgScenario scenario = (IAgScenario)(CommonData.StkRoot.CurrentScenario);
                current.AnimationTime = scenario.StartTime;
            }
            if (ObjectHideShow.Checked)
            {
                current.ObjectHideShow = true;
                List<ObjectData> data = new List<ObjectData>();
                data = CommonData.CurrentViewObjectData;
                current.ViewObjectData = data;
            }
            else
            {
                current.ObjectHideShow = false;
            }
            CommonData.SavedViewList.Add(current);
            try
            {
                ReadWrite.WriteSavedViews(CommonData.DirectoryStr + "\\StoredViewData.json");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Write Stored Views File");
            }

            //try
            //{
            //    ReadWrite.WriteObjectData(CommonData.DirectoryStr + "\\StoredObjectData.txt");
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Could not Write Object Data File");
            //}
            CommonData.NewView = true;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SensorCoverage2D_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UseDataDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (UseDataDisplay.Checked)
            {
                DataDisplayOptions.Enabled = true;
            }
            else
            {
                DataDisplayOptions.Enabled = false;
            }
        }

        private void ObjectHideShow_CheckedChanged(object sender, EventArgs e)
        {
            if (ObjectHideShow.Checked)
            {
                HideShowOptions.Enabled = true;

            }
            else
            {
                HideShowOptions.Enabled = false;
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

        private void PopulateComboBoxes()
        {
            TypeSelect.Items.Add("3D");
            TypeSelect.Items.Add("2D");
            TypeSelect.Items.Add("Target/Actor");
            TypeSelect.Items.Add("GEO Drift");
            TypeSelect.SelectedIndex = 0;

            ViewType.Items.Add("Inertial");
            //ViewType.Items.Add("Fixed");
            ViewType.SelectedIndex = 0;

            DisplayLocation.Items.Add("TopLeft");
            DisplayLocation.Items.Add("TopCenter");
            DisplayLocation.Items.Add("TopRight");
            DisplayLocation.Items.Add("CenterLeft");
            DisplayLocation.Items.Add("Center");
            DisplayLocation.Items.Add("CenterRight");
            DisplayLocation.Items.Add("BottomLeft");
            DisplayLocation.Items.Add("BottomCenter");
            DisplayLocation.Items.Add("BottomRight");
            DisplayLocation.SelectedIndex = 0;

            TypeSelect2D.Items.Add("ZoomedOut");
            TypeSelect2D.Items.Add("SpecifyCenter");
            TypeSelect2D.Items.Add("ObjectCenter");
            TypeSelect2D.SelectedIndex = 0;


            LeadType3D.Items.Add("OnePass");
            LeadType3D.Items.Add("All");
            LeadType3D.Items.Add("Full");
            LeadType3D.Items.Add("Half");
            LeadType3D.Items.Add("Quarter");
            LeadType3D.Items.Add("None");
            LeadType3D.Items.Add("Time");
            LeadType3D.SelectedIndex = 0;

            LeadType2D.Items.Add("OnePass");
            LeadType2D.Items.Add("All");
            LeadType2D.Items.Add("Full");
            LeadType2D.Items.Add("Half");
            LeadType2D.Items.Add("Quarter");
            LeadType2D.Items.Add("None");
            LeadType2D.Items.Add("Time");
            LeadType2D.SelectedIndex = 0;

            TrailType3D.Items.Add("SameAsLead");
            TrailType3D.Items.Add("OnePass");
            TrailType3D.Items.Add("All");
            TrailType3D.Items.Add("Full");
            TrailType3D.Items.Add("Half");
            TrailType3D.Items.Add("Quarter");
            TrailType3D.Items.Add("None");
            TrailType3D.Items.Add("Time");
            TrailType3D.SelectedIndex = 0;

            TrailType2D.Items.Add("SameAsLead");
            TrailType2D.Items.Add("OnePass");
            TrailType2D.Items.Add("All");
            TrailType2D.Items.Add("Full");
            TrailType2D.Items.Add("Half");
            TrailType2D.Items.Add("Quarter");
            TrailType2D.Items.Add("None");
            TrailType2D.Items.Add("Time");
            TrailType2D.SelectedIndex = 0;

            ZoomCenterLong.Text = "0";
            ZoomCenterLat.Text = "0";
            ZoomDelta.Text = "10";

            TTDisplayObject.Items.Add("Target Satellite");
            TTDisplayObject.Items.Add("All Actors (Up to 4)");
            foreach (ObjectData item in CommonData.CurrentViewObjectData)
            {
                if (item.ClassName=="Satellite")
                {
                    TargetSatellite.Items.Add(item.SimpleName);
                    GEOViewTarget.Items.Add(item.SimpleName);
                    if (item.SimpleName != TargetSatellite.Text)
                    {
                        TTDisplayObject.Items.Add(item.SimpleName);
                    }
                }
            }
            TTDisplayObject.SelectedIndex = 0;
            if (TargetSatellite.Items.Count!=0)
            {
                TargetSatellite.SelectedIndex = 0;
                GEOViewTarget.SelectedIndex = 0;
                PopulateThreats();
            }

            TTDisplayLocation.Items.Add("TopLeft");
            TTDisplayLocation.Items.Add("TopCenter");
            TTDisplayLocation.Items.Add("TopRight");
            TTDisplayLocation.Items.Add("CenterLeft");
            TTDisplayLocation.Items.Add("Center");
            TTDisplayLocation.Items.Add("CenterRight");
            TTDisplayLocation.Items.Add("BottomLeft");
            TTDisplayLocation.Items.Add("BottomCenter");
            TTDisplayLocation.Items.Add("BottomRight");
            TTDisplayLocation.SelectedIndex = 0;

            EllipsoidX.Text = "100";
            EllipsoidY.Text = "100";
            EllipsoidZ.Text = "100";

            GEODisplayLocation.Items.Add("TopLeft");
            GEODisplayLocation.Items.Add("TopCenter");
            GEODisplayLocation.Items.Add("TopRight");
            GEODisplayLocation.Items.Add("CenterLeft");
            GEODisplayLocation.Items.Add("Center");
            GEODisplayLocation.Items.Add("CenterRight");
            GEODisplayLocation.Items.Add("BottomLeft");
            GEODisplayLocation.Items.Add("BottomCenter");
            GEODisplayLocation.Items.Add("BottomRight");
            GEODisplayLocation.SelectedIndex = 0;

            GEOLongitude.Text = "-100";
            GEOEastWest.Text = "1";
            GeoNorthSouth.Text = "1";
            GEORadius.Text = "42166.3";

            OrbitTrackBox.Enabled = false;
            GroundTrackBox.Enabled = false;
        }

        private void DisplayObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDisplayReports(DisplayReport,DisplayObject);
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

        private int FieldCheck2D()
        {
            int check = 0;
            bool isNumerical;
            double temp;
            if (ViewName2D.Text ==null || ViewName2D.Text =="")
            {
                MessageBox.Show("View Name Required");
                check = 1;
            }
            if (TypeSelect2D.SelectedIndex==0)
            {
            }
            else if (TypeSelect2D.SelectedIndex == 1)
            {
                isNumerical = Double.TryParse(ZoomCenterLat.Text, out temp);
                if (!isNumerical)
                {
                    MessageBox.Show("Latitude field not a number ");
                    check = 1;
                }
                isNumerical = Double.TryParse(ZoomCenterLong.Text, out temp);
                if (!isNumerical)
                {
                    MessageBox.Show("Longitude field not a number ");
                    check = 1;
                }
                isNumerical = Double.TryParse(ZoomDelta.Text, out temp);
                if (!isNumerical)
                {
                    MessageBox.Show("Zoom delta not a number ");
                    check = 1;
                }
            }
            else if (TypeSelect2D.SelectedIndex == 2)
            {
                if (ObjectName2D.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select object of interest");
                    check = 1;
                }
            }
            if (LeadType2D.Text=="Time")
            {
                isNumerical = Double.TryParse(GroundLeadTime.Text, out temp);
                if (!isNumerical)
                {
                    MessageBox.Show("Lead time not a number ");
                    check = 1;
                }
            }
            if (TrailType2D.Text=="Time")
            {
                isNumerical = Double.TryParse(GroundTrailTime.Text, out temp);
                if (!isNumerical)
                {
                    MessageBox.Show("trail time not a number ");
                    check = 1;
                }
            }
            return check;
        }

        private int FieldCheck3D()
        {
            int check = 0;
            bool isNumerical;
            double temp;
            if (ViewName3D.Text == null || ViewName3D.Text == "")
            {
                MessageBox.Show("View Name Required");
                check = 1;
            }
            if (LeadType3D.Text == "Time")
            {
                isNumerical = Double.TryParse(OrbitLeadTime.Text, out temp);
                if (!isNumerical)
                {
                    MessageBox.Show("Lead time not a number ");
                    check = 1;
                }
            }
            if (TrailType3D.Text == "Time")
            {
                isNumerical = Double.TryParse(OrbitTrailTime.Text, out temp);
                if (!isNumerical)
                {
                    MessageBox.Show("Trail time not a number ");
                    check = 1;
                }
            }
            return check;
        }

        private void TypeSelect2D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypeSelect2D.SelectedIndex==0)
            {
                ObjectName2D.Enabled = false;
                ZoomCenterLat.Enabled = false;
                ZoomCenterLong.Enabled = false;
                ZoomDelta.Enabled = false;
            }
            else if(TypeSelect2D.SelectedIndex == 1)
            {
                ObjectName2D.Enabled = false;
                ZoomCenterLat.Enabled = true;
                ZoomCenterLong.Enabled = true;
                ZoomDelta.Enabled = true;
            }
            else if (TypeSelect2D.SelectedIndex == 2)
            {
                ObjectName2D.Enabled = true;
                ZoomCenterLat.Enabled = false;
                ZoomCenterLong.Enabled = false;
                ZoomDelta.Enabled = true;
            }
        }

        private void LeadType3D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LeadType3D.SelectedIndex!=-1)
            {
                if (LeadType3D.Text=="Time")
                {
                    OrbitLeadTime.Enabled = true;
                }
                else
                {
                    OrbitLeadTime.Enabled = false;
                }
            }
        }

        private void TrailType3D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrailType3D.SelectedIndex != -1)
            {
                if (TrailType3D.Text == "Time")
                {
                    OrbitTrailTime.Enabled = true;
                }
                else
                {
                    OrbitTrailTime.Enabled = false;
                }
            }
        }

        private void LeadType2D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LeadType2D.SelectedIndex != -1)
            {
                if (LeadType2D.Text == "Time")
                {
                    GroundLeadTime.Enabled = true;
                }
                else
                {
                    GroundLeadTime.Enabled = false;
                }
            }
        }

        private void TrailType2D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrailType2D.SelectedIndex != -1)
            {
                if (TrailType2D.Text == "Time")
                {
                    GroundTrailTime.Enabled = true;
                }
                else
                {
                    GroundTrailTime.Enabled = false;
                }
            }
        }

        private void HideShowOptions_Click(object sender, EventArgs e)
        {
            ObjectHideShowForm form = new ObjectHideShowForm();
            form.ShowDialog();
        }

        private void Select_Click(object sender, EventArgs e)
        {
            string className = null;
            if (ThreatList.FocusedItem != null)
            {
                foreach (int index in ThreatList.SelectedIndices)
                {
                    ThreatList.Items[index].Font = new Font(ThreatList.Items[index].Font, FontStyle.Bold);
                    if (!_threatNames.Contains(ThreatList.Items[index].SubItems[0].Text))
                    {
                        className = SmartViewFunctions.GetClassName(ThreatList.Items[index].SubItems[0].Text);
                        _threatNames.Add(className+"/"+ThreatList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void Unselect_Click(object sender, EventArgs e)
        {
            string className = null;
            if (ThreatList.FocusedItem != null)
            {
                foreach (int index in ThreatList.SelectedIndices)
                {
                    ThreatList.Items[index].Font = new Font(ThreatList.Items[index].Font, FontStyle.Regular);
                    if (_threatNames.Contains(ThreatList.Items[index].SubItems[0].Text))
                    {
                        className = SmartViewFunctions.GetClassName(ThreatList.Items[index].SubItems[0].Text);
                        _threatNames.Remove(className+"/"+ThreatList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void TTDisplayObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTDisplayObject.SelectedIndex != -1)
            {
                TTDisplayReport.Items.Clear();
                if (TTDisplayObject.SelectedIndex==0)
                {
                    TTDisplayLocation.Enabled = true;
                    TTDisplayReport.Items.Add("LLA Position");
                    TTDisplayReport.Items.Add("Classical Orbital Elements");
                    TTDisplayReport.Items.Add("Inertial Position Velocity");
                    TTDisplayReport.Items.Add("Fixed Position Velocity");
                }
                else if(TTDisplayObject.SelectedIndex == 1)
                {
                    TTDisplayLocation.Enabled = false;
                    TTDisplayReport.Items.Add("RIC");
                    TTDisplayReport.Items.Add("LLA Position");
                    TTDisplayReport.Items.Add("Classical Orbital Elements");
                    TTDisplayReport.Items.Add("Inertial Position Velocity");
                    TTDisplayReport.Items.Add("Fixed Position Velocity");
                }
                else
	            {
                    TTDisplayLocation.Enabled = true;
                    string className = SmartViewFunctions.GetClassName(TTDisplayObject.Text);
                    IAgVODataDisplayCollection ddCollection = null;
                    if (className == "Satellite")
                    {
                        IAgSatellite myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + TTDisplayObject.Text) as IAgSatellite;
                        ddCollection = myObject.VO.DataDisplay;
                    }
                    Array reportNames = ddCollection.AvailableData;
                    foreach (var name in reportNames)
                    {
                        TTDisplayReport.Items.Add(name);
                    }
                }
                TTDisplayReport.SelectedIndex = 0;

            }
        }

        private void TTUseDataDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (TTUseDataDisplay.Checked)
            {
                TTDataDisplayOptions.Enabled = true;
            }
            else
            {
                TTDataDisplayOptions.Enabled = false;
            }
        }

        private void UseProxBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TargetSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TargetSatellite.SelectedIndex!=-1)
            {
                PopulateThreats();
            }
        }

        private void PopulateThreats()
        {
            ThreatList.Items.Clear();
            _threatNames.Clear();
            foreach (ObjectData item in CommonData.CurrentViewObjectData)
            {
                if (item.ClassName == "Satellite" && item.SimpleName!=TargetSatellite.Text)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = item.SimpleName;
                    int index = ThreatList.Items.IndexOf(listItem);
                    if (index==-1)
                    {
                        ThreatList.Items.Add(listItem);
                    }
                }
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

        private void GEOUseDataDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (GEOUseDataDisplay.Checked)
            {
                GEODataDisplayOptions.Enabled = true;
            }
            else
            {
                GEODataDisplayOptions.Enabled = false;
            }
        }

        private void GEODisplayObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDisplayReports(GEODisplayReport, GEODisplayObject);
        }

        private void GEODisplayReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UseCurrentViewPoint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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

        private void EnableUniversalOrbitTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableUniversalOrbitTrack.Checked)
            {
                OrbitTrackBox.Enabled = true;
            }
            else
            {
                OrbitTrackBox.Enabled = false;
            }
        }

        private void EnableUniversalGroundTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableUniversalGroundTrack.Checked)
            {
                GroundTrackBox.Enabled = true;
            }
            else
            {
                GroundTrackBox.Enabled = false;
            }
        }
    }
}
