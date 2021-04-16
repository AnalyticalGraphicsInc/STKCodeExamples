﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.SmartView
{
    public partial class Edit3DGenericForm : Form
    {
        private ViewData current;
        public Edit3DGenericForm()
        {
            InitializeComponent();
            current = new ViewData();
            PopulateObjects(FocusedItem, true);
            PopulateDdObjects(DisplayObject, false);
            PopulatePredataObjects(PredataObject, false);
            PopulateComboBoxes();
            current = CommonData.SavedViewList[CommonData.SelectedIndex];
            if (current.UseAnimationTime)
            {
                CurrentTime.Text = current.AnimationTime;
                UseCurrentTime.Checked = true;
            }
            if (CommonData.SavedViewList[CommonData.SelectedIndex].ViewObjectData!=null && CommonData.SavedViewList[CommonData.SelectedIndex].ViewObjectData.Count>0)
            {
                CommonData.CurrentViewObjectData = CommonData.SavedViewList[CommonData.SelectedIndex].ViewObjectData;
                List<ObjectData> newData = SmartViewFunctions.GetObjectData();
                int index = 0;
                foreach (ObjectData item in newData)
                {
                    if (!CommonData.CurrentViewObjectData.Any(n=>n.SimpleName == item.SimpleName))
                    {
                       index = newData.IndexOf(item);
                        CommonData.CurrentViewObjectData.Insert(index, item);
                    }
                }
            }
            else
            {
                CommonData.CurrentViewObjectData = SmartViewFunctions.GetObjectData();
            }

            if (current.UniqueLeadTrail)
            {
                UniqueLeadTrail.Checked = true;
                CustomLeadTrail.Enabled = true;
            }
            else
            {
                UniversalLeadTrail.Checked = true;
                CustomLeadTrail.Enabled = false;
            }

        }

        private void LeadType3D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LeadType3D.SelectedIndex != -1)
            {
                if (LeadType3D.Text == "Time")
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

        private void DisplayObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DisplayObject.SelectedIndex != -1)
            {
                string className = SmartViewFunctions.GetClassName(DisplayObject.Text);
                IAgVODataDisplayCollection ddCollection = null;
                if (className == "Satellite")
                {
                    IAgSatellite myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgSatellite;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "Aircraft")
                {
                    IAgAircraft myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgAircraft;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "Facility")
                {
                    IAgFacility myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgFacility;
                    ddCollection = myObject.VO.DataDisplays;
                }
                else if (className == "Missile")
                {
                    IAgMissile myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgMissile;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "GroundVehicle")
                {
                    IAgGroundVehicle myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgGroundVehicle;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "LaunchVehicle")
                {
                    IAgLaunchVehicle myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgLaunchVehicle;
                    ddCollection = myObject.VO.DataDisplay;
                }
                else if (className == "Place")
                {
                    IAgPlace myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgPlace;
                    ddCollection = myObject.VO.DataDisplays;
                }
                else if (className == "Target")
                {
                    IAgTarget myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + DisplayObject.Text) as IAgTarget;
                    ddCollection = myObject.VO.DataDisplays;
                }
                DisplayReport.Items.Clear();
                if (ddCollection !=null)
                {
                    Array reportNames = ddCollection.AvailableData;
                    foreach (var name in reportNames)
                    {
                        DisplayReport.Items.Add(name);
                        if (name.ToString() == CommonData.SavedViewList[CommonData.SelectedIndex].PrimaryDataDisplay.DataDisplayReportName)
                        {
                            DisplayReport.SelectedIndex = DisplayReport.Items.Count - 1;
                        }
                    }
                    if (DisplayReport.SelectedIndex == -1)
                    {
                        DisplayReport.SelectedIndex = 0;
                    }
                }
            }
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

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            int check = FieldCheck3D();
            if (check==0)
            {
                current.WindowName = WindowSelect.Text;
                current.WindowId = SmartViewFunctions.GetWindowId(WindowSelect.Text, 1);
                current.Name = ViewName3D.Text;
                current.ViewType = "3D";
                string className = SmartViewFunctions.GetClassName(FocusedItem.Text);
                current.ViewTarget = className + "/" + FocusedItem.Text;
                current.ViewAxes = "Inertial";

                if (EnableUniversalOrbitTrack.Checked)
                {
                    if (UniversalLeadTrail.Checked)
                    {
                        current.EnableUniversalOrbitTrack = true;
                        current.UniqueLeadTrail = false;
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

                if (UseDataDisplay.Checked)
                {
                    current.PrimaryDataDisplay.DataDisplayActive = true;
                    current.PrimaryDataDisplay.DataDisplayLocation = DisplayLocation.Text;
                    className = SmartViewFunctions.GetClassName(DisplayObject.Text);
                    current.PrimaryDataDisplay.DataDisplayObject = className + "/" + DisplayObject.Text;
                    current.PrimaryDataDisplay.DataDisplayReportName = DisplayReport.Text;
                    className = SmartViewFunctions.GetClassName(PredataObject.Text);
                    current.PrimaryDataDisplay.PredataObject = className + "/" + PredataObject.Text;
                }
                else
                {
                    current.PrimaryDataDisplay.DataDisplayActive = false;
                    current.PrimaryDataDisplay.DataDisplayLocation = DisplayLocation.Text;
                    className = SmartViewFunctions.GetClassName(DisplayObject.Text);
                    current.PrimaryDataDisplay.DataDisplayObject = className + "/" + DisplayObject.Text;
                    current.PrimaryDataDisplay.DataDisplayReportName = DisplayReport.Text;
                    current.PrimaryDataDisplay.PredataObject = PredataObject.Text;
                }

                if (UseCurrentTime.Checked)
                {
                    current.UseAnimationTime = true;
                    current.AnimationTime = CurrentTime.Text;
                }
                else
                {
                    current.UseAnimationTime = false;
                    IAgScenario scenario = (IAgScenario)(CommonData.StkRoot.CurrentScenario);
                    current.AnimationTime = scenario.StartTime;
                }

                if (UseCurrentViewPoint.Checked)
                {
                    //only refresh view if not previously active. you can also refresh using the 'refresh' button on the screen
                    if (!current.UseStoredView)
                    {
                        current.StoredViewName = ViewName3D.Text;
                        CommonData.StkRoot.ExecuteCommand("VO * SaveStoredView \"" + ViewName3D.Text + "\" " + current.WindowId);
                    }
                    current.UseStoredView = true;
                }
                else
                {
                    current.UseStoredView = false;
                    current.StoredViewName = "None";
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

                CommonData.SavedViewList[CommonData.SelectedIndex] = current;
                CommonData.UpdatedView = true;
                try
                {
                    ReadWrite.WriteSavedViews(CommonData.DirectoryStr + "\\StoredViewData.json");
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not Write Stored Views File");
                }
                this.Close();
            }
        }

        private void PopulateObjects(ComboBox combo, bool includeEarth)
        {
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            combo.Items.Clear();
            string simpleName;
            string className;
            StringCollection objectNames = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            if (includeEarth)
            {
                combo.Items.Add("Earth");
                if ("CentralBody/Earth"== CommonData.SavedViewList[CommonData.SelectedIndex].ViewTarget)
                {
                    combo.SelectedIndex = 0;
                }
            }
            foreach (string objectName in objectNames)
            {
                className = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "Sensor" && className != "Antenna" && className != "Transmitter" && className != "Receiver" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Radar" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    combo.Items.Add(simpleName);
                    if (className + "/" + simpleName == CommonData.SavedViewList[CommonData.SelectedIndex].ViewTarget)
                    {
                        combo.SelectedIndex = combo.Items.Count - 1;
                    }
                }
            }

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

                if (className != "Scenario" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    combo.Items.Add(simpleName);
                    if (className + "/" + simpleName == CommonData.SavedViewList[CommonData.SelectedIndex].PrimaryDataDisplay.DataDisplayObject)
                    {
                        combo.SelectedIndex = combo.Items.Count - 1;
                    }
                }
            }

        }

        private void PopulatePredataObjects(ComboBox combo, bool includeEarth)
        {
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            combo.Items.Clear();
            string simpleName;
            string className;
            StringCollection objectNames = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            combo.Items.Add("None");
            if (includeEarth)
            {
                combo.Items.Add("Earth");
            }
            foreach (string objectName in objectNames)
            {
                className = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    combo.Items.Add(simpleName);
                    if (className + "/" + simpleName == CommonData.SavedViewList[CommonData.SelectedIndex].PrimaryDataDisplay.PredataObject)
                    {
                        combo.SelectedIndex = combo.Items.Count - 1;
                    }
                }
            }
        }

        private void PopulateComboBoxes()
        {
            ViewName3D.Text = CommonData.SavedViewList[CommonData.SelectedIndex].Name;
            //ViewType.Items.Add("Inertial");
            //ViewType.Items.Add("Fixed");
            //ViewType.SelectedIndex = 0;

            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseAnimationTime)
            {
                CurrentTime.Text = CommonData.SavedViewList[CommonData.SelectedIndex].AnimationTime;
                UseCurrentTime.Checked = true;
            }

            List<string> paths = SmartViewFunctions.GetCameraPaths();
            for (int i = 0; i < paths.Count; i++)
            {
                CameraPathName.Items.Add(paths[i]);
                if (CommonData.SavedViewList[CommonData.SelectedIndex].UseCameraPath && paths[i] == CommonData.SavedViewList[CommonData.SelectedIndex].CameraPathName)
                {
                    CameraPathName.SelectedIndex = i;
                }
            }
            if (CameraPathName.SelectedIndex == -1 && CameraPathName.Items.Count > 0)
            {
                CameraPathName.SelectedIndex = 0;
            }


            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseCameraPath)
            {
                UseCameraPath.Checked = true;
                CameraPathName.Enabled = true;
            }
            else
            {
                UseCameraPath.Checked = false;
                CameraPathName.Enabled = false;
            }

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
            if (CommonData.SavedViewList[CommonData.SelectedIndex].PrimaryDataDisplay.DataDisplayActive)
            {
                foreach (string item in DisplayLocation.Items)
                {
                    if (CommonData.SavedViewList[CommonData.SelectedIndex].PrimaryDataDisplay.DataDisplayLocation.Contains(item))
                    {
                        DisplayLocation.SelectedItem = item;
                    }
                }
            }
            OrbitTrackBox.Enabled = false;
            if (CommonData.SavedViewList[CommonData.SelectedIndex].EnableUniversalOrbitTrack)
            {
                EnableUniversalOrbitTrack.Checked = true;
            }
            LeadType3D.Items.Add("OnePass");
            LeadType3D.Items.Add("All");
            LeadType3D.Items.Add("Full");
            LeadType3D.Items.Add("Half");
            LeadType3D.Items.Add("Quarter");
            LeadType3D.Items.Add("None");
            LeadType3D.Items.Add("Time");
            LeadType3D.SelectedIndex = 0;
            if (CommonData.SavedViewList[CommonData.SelectedIndex].EnableUniversalOrbitTrack && !CommonData.SavedViewList[CommonData.SelectedIndex].UniqueLeadTrail)
            {
                foreach (string item in LeadType3D.Items)
                {
                    if (CommonData.SavedViewList[CommonData.SelectedIndex].LeadType.Contains(item))
                    {
                        LeadType3D.SelectedItem = item;
                        if (item == "Time")
                        {
                            OrbitLeadTime.Text = CommonData.SavedViewList[CommonData.SelectedIndex].LeadTime;
                        }
                    }
                }
            }

            TrailType3D.Items.Add("SameAsLead");
            TrailType3D.Items.Add("OnePass");
            TrailType3D.Items.Add("All");
            TrailType3D.Items.Add("Full");
            TrailType3D.Items.Add("Half");
            TrailType3D.Items.Add("Quarter");
            TrailType3D.Items.Add("None");
            TrailType3D.Items.Add("Time");
            TrailType3D.SelectedIndex = 0;
            if (CommonData.SavedViewList[CommonData.SelectedIndex].EnableUniversalOrbitTrack && !CommonData.SavedViewList[CommonData.SelectedIndex].UniqueLeadTrail)
            {
                foreach (string item in TrailType3D.Items)
                {
                    if (CommonData.SavedViewList[CommonData.SelectedIndex].TrailType.Contains(item))
                    {
                        TrailType3D.SelectedItem = item;
                        if (item == "Time")
                        {
                            OrbitTrailTime.Text = CommonData.SavedViewList[CommonData.SelectedIndex].TrailTime;
                        }
                    }
                }
            }

            List<string> windowNames = SmartViewFunctions.GetWindowNames(1);
            foreach (var item in windowNames)
            {
                WindowSelect.Items.Add(item);
                if (item.Contains(CommonData.SavedViewList[CommonData.SelectedIndex].WindowName))
                {
                    WindowSelect.SelectedIndex = WindowSelect.Items.Count - 1;
                }
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].ObjectHideShow)
            {
                ObjectHideShow.Checked = true;
                HideShowOptions.Enabled = true;
            }
            else
            {
                HideShowOptions.Enabled = false;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].VectorHideShow)
            {
                UseVectorHideShow.Checked = true;
                VectorHideShow.Enabled = true;
            }
            else
            {
                UseVectorHideShow.Checked = false;
                VectorHideShow.Enabled = false;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].PrimaryDataDisplay.DataDisplayActive)
            {
                UseDataDisplay.Checked = true;
                DataDisplayOptions.Enabled = true;
            }
            else
            {
                DataDisplayOptions.Enabled = false;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseAnimationTime)
            {
                string currentTime = CommonData.SavedViewList[CommonData.SelectedIndex].AnimationTime;
                UseCurrentTime.Checked = true;
                CurrentTime.Text = currentTime;
            }
            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseStoredView)
            {
                UseCurrentViewPoint.Checked = true;
            }
        }

        private void HideShowOptions_Click(object sender, EventArgs e)
        {
            ObjectHideShowForm form = new ObjectHideShowForm();
            form.ShowDialog();
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

        private void RefreshViewPoint_Click(object sender, EventArgs e)
        {
            int windowId = SmartViewFunctions.GetWindowId(WindowSelect.Text, 1);
            if (ViewName3D.Text!=null && ViewName3D.Text!="")
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO * SaveStoredView \"" + ViewName3D.Text + "\" " + windowId.ToString());
                    current.StoredViewName = ViewName3D.Text;
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not refresh viewpoint");
                }                
            }
            else
            {
                MessageBox.Show("Invalid View Name");
            }
        }

        private void UseCurrentTime_CheckedChanged(object sender, EventArgs e)
        {
            if (UseCurrentTime.Checked)
            {
                CurrentTime.Enabled = true;
                RefreshTime.Enabled = true;
            }
            else
            {
                CurrentTime.Enabled = false;
                RefreshTime.Enabled = false;
            }
        }

        private void UseCurrentViewPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (UseCurrentViewPoint.Checked)
            {
                RefreshViewPoint.Enabled = true;
            }
            else
            {
                RefreshViewPoint.Enabled = false;
            }
        }

        private void RefreshTime_Click(object sender, EventArgs e)
        {
            IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
            double currentTime = animationRoot.CurrentTime;
            string newTime = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", currentTime.ToString());
            CurrentTime.Text = newTime;
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

        private void DataDisplayOptions_Enter(object sender, EventArgs e)
        {

        }

        private void AdvancedDisplay_Click(object sender, EventArgs e)
        {
            AdvancedDisplayOptionsForm form = new AdvancedDisplayOptionsForm(current, true);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Yes)
            {
                current = form.currentView;
            }
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

        private void UseCameraPath_CheckedChanged(object sender, EventArgs e)
        {
            if (UseCameraPath.Checked)
            {
                CameraPathName.Enabled = true;
            }
            else
            {
                CameraPathName.Enabled = false;
            }
        }

        private void CustomLeadTrail_Click(object sender, EventArgs e)
        {
            CustomLeadTrailForm form = new CustomLeadTrailForm();
            form.ShowDialog();
            current.ViewObjectData = CommonData.CurrentViewObjectData;
        }
    }
}
