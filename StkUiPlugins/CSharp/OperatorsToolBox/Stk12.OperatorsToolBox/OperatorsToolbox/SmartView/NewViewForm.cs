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
        public ViewData current = new ViewData();
        private List<string> _threatNames;
        public NewViewForm()
        {
            InitializeComponent();
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            CommonData.CurrentViewObjectData = SmartViewFunctions.GetObjectData();

            PopulateComboBoxes();
            PopulateObjects(FocusedItem, true);
            ViewName3D.Text = "New View";
            ViewName2D.Text = "New View";
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
            PopulateObjects(PredataObject, false);
            PredataObject.Items.Insert(0, "None");
            PredataObject.SelectedIndex = 0;
            DataDisplayOptions.Enabled = false;
            HideShowOptions.Enabled = false;
            VectorHideShow.Enabled = false;
            CustomLeadTrail.Enabled = false;
            //initialize some settings in the current view data
            current.EnableProximityBox = false;
            current.EnableGeoBox = false;
            current.EnableProximityEllipsoid = false;
            current.SecondaryDataDisplay.DataDisplayActive = false;
            current.ApplyVectorScaling = false;
            current.OverrideTimeStep = false;
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
                UseCurrentViewPoint.Enabled = true;
                UseCurrentViewPoint.Visible = true;
                UseCameraPath.Enabled = true;
                UseCameraPath.Visible = true;
                CameraPathName.Enabled = true;
                CameraPathName.Visible = true;
                UseVectorHideShow.Visible = true;
                VectorHideShow.Visible = true;
            }
            else if (TypeSelect.SelectedIndex == 1)
            {
                windowNames = SmartViewFunctions.GetWindowNames(0);
                ViewDefinitionBox3D.Enabled = false;
                ViewDefinitionBox3D.Visible = false;
                ViewDefinitionBox2D.Enabled = true;
                ViewDefinitionBox2D.Visible = true;
                UseCurrentViewPoint.Enabled = false;
                UseCurrentViewPoint.Visible = false;
                UseCameraPath.Enabled = false;
                UseCameraPath.Visible = false;
                CameraPathName.Enabled = false;
                CameraPathName.Visible = false;
                UseVectorHideShow.Visible = false;
                VectorHideShow.Visible = false;
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
                    current.ViewObjectData = CommonData.CurrentViewObjectData;
                    className = SmartViewFunctions.GetClassName(FocusedItem.Text);
                    current.ViewTarget = className+"/"+FocusedItem.Text;
                    current.ViewAxes = "Inertial";

                    if (EnableUniversalOrbitTrack.Checked)
                    {
                        if (UniversalLeadTrail.Checked)
                        {
                            current.EnableUniversalOrbitTrack = true;
                            current.UniqueLeadTrail = false;
                            if (LeadType3D.Text == "Time")
                            {
                                current.LeadType = LeadType3D.Text;
                                current.LeadTime = OrbitLeadTime.Text;
                            }
                            else
                            {
                                current.LeadType = LeadType3D.Text;
                            }

                            if (TrailType3D.Text == "Time")
                            {
                                current.TrailType = TrailType3D.Text;
                                current.TrailTime = OrbitTrailTime.Text;
                            }
                            else
                            {
                                current.TrailType = TrailType3D.Text;
                            }
                        }
                        else if (UniqueLeadTrail.Checked)
                        {
                            current.UniqueLeadTrail = true;
                            current.EnableUniversalOrbitTrack = true;
                        }
                    }
                    else
                    {
                        current.EnableUniversalOrbitTrack = false;
                        current.LeadType = "None";
                        current.TrailType = "None";
                    }

                    //Add required primary data display components
                    if (UseDataDisplay.Checked)
                    {
                        current.PrimaryDataDisplay.DataDisplayActive = true;
                    }
                    else
                    {
                        current.PrimaryDataDisplay.DataDisplayActive = false;
                    }
                    current.PrimaryDataDisplay.DataDisplayLocation = DisplayLocation.Text;
                    className = SmartViewFunctions.GetClassName(DisplayObject.Text);
                    current.PrimaryDataDisplay.DataDisplayObject = className + "/" + DisplayObject.Text;
                    current.PrimaryDataDisplay.DataDisplayReportName = DisplayReport.Text;
                    className = SmartViewFunctions.GetClassName(PredataObject.Text);
                    current.PrimaryDataDisplay.PredataObject = className + "/" + PredataObject.Text;

                    //Define stored view if required by current view point option
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
                    current.ViewObjectData = CommonData.CurrentViewObjectData;
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
                }
            }
            if (UseCurrentTime.Checked)
            {
                current.UseAnimationTime = true;
                IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
                double currentTime = animationRoot.CurrentTime;
                string newTime = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", currentTime.ToString());
                current.AnimationTime = newTime.ToString();
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
            if (UseVectorHideShow.Checked)
            {
                current.VectorHideShow = true;
            }
            else
            {
                current.VectorHideShow = false;
            }

            if (UseCameraPath.Checked)
            {
                current.UseCameraPath = true;
                current.CameraPathName = CameraPathName.Text;
            }
            else
            {
                current.UseCameraPath = false;
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
            TypeSelect.SelectedIndex = 0;

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

            OrbitTrackBox.Enabled = false;
            GroundTrackBox.Enabled = false;

            List<string> paths = SmartViewFunctions.GetCameraPaths();
            foreach (var item in paths)
            {
                CameraPathName.Items.Add(item);
            }
            if (CameraPathName.Items.Count>0)
            {
                CameraPathName.SelectedIndex = 0;
            }
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

        private void UseCurrentViewPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (UseCurrentViewPoint.Checked)
            {
                UseCameraPath.Checked = false;
            }
            else
            {

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

        private void UseCameraPath_CheckedChanged(object sender, EventArgs e)
        {
            if (UseCameraPath.Checked)
            {
                UseCurrentViewPoint.Checked = false;
            }
            else
            {

            }
        }

        private void AdvancedDisplay_Click(object sender, EventArgs e)
        {
            AdvancedDisplayOptionsForm form = new AdvancedDisplayOptionsForm(current, false);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Yes)
            {
                current = form.currentView;
            }
        }

        private void CustomLeadTrail_Click(object sender, EventArgs e)
        {
            CustomLeadTrailForm form = new CustomLeadTrailForm();
            form.ShowDialog();
            current.ViewObjectData = CommonData.CurrentViewObjectData;
        }

        private void UseVectorHideShow_CheckedChanged(object sender, EventArgs e)
        {
            if (UseVectorHideShow.Checked)
            {
                VectorHideShow.Enabled = true;

            }
            else
            {
                VectorHideShow.Enabled = false;
            }
        }

        private void VectorHideShow_Click(object sender, EventArgs e)
        {
            VectorHideShowForm form = new VectorHideShowForm();
            form.ShowDialog();
            current.ViewObjectData = CommonData.CurrentViewObjectData;
        }

        private void UniversalLeadTrail_CheckedChanged(object sender, EventArgs e)
        {
            if (UniversalLeadTrail.Checked)
            {
                UniqueLeadTrail.Checked = false;
            }
        }

        private void UniqueLeadTrail_CheckedChanged(object sender, EventArgs e)
        {
            if (UniqueLeadTrail.Checked)
            {
                UniversalLeadTrail.Checked = false;
                CustomLeadTrail.Enabled = true;
            }
            else
            {
                CustomLeadTrail.Enabled = false;
            }
        }
    }
}
