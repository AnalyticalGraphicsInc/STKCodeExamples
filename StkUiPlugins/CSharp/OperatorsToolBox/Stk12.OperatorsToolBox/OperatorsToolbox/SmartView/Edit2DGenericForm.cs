using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.SmartView
{
    public partial class Edit2DGenericForm : Form
    {
        public Edit2DGenericForm()
        {
            InitializeComponent();
            PopulateObjects(ObjectName2D, false);
            PopulateBoxes();

            if (CommonData.SavedViewList[CommonData.SelectedIndex].ViewObjectData != null && CommonData.SavedViewList[CommonData.SelectedIndex].ViewObjectData.Count > 0)
            {
                CommonData.CurrentViewObjectData = CommonData.SavedViewList[CommonData.SelectedIndex].ViewObjectData;
                List<ObjectData> newData = SmartViewFunctions.GetObjectData();
                int index = 0;
                foreach (ObjectData item in newData)
                {
                    if (!CommonData.CurrentViewObjectData.Any(n => n.SimpleName == item.SimpleName))
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

        private void Apply_Click(object sender, EventArgs e)
        {
            ViewData current = new ViewData();

            current.WindowName = WindowSelect.Text;
            current.WindowId = SmartViewFunctions.GetWindowId(WindowSelect.Text, 0);
            current.Name = ViewName2D.Text;
            current.ViewType = "2D";
            current.ViewType2D = TypeSelect2D.Text;
            string className = SmartViewFunctions.GetClassName(ObjectName2D.Text);
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

            if (UseCurrentTime.Checked)
            {
                IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
                double currentTime = animationRoot.CurrentTime;
                current.UseAnimationTime = true;
                current.AnimationTime = CurrentTime.Text;
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

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TypeSelect2D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypeSelect2D.SelectedIndex == 0)
            {
                ObjectName2D.Enabled = false;
                ZoomCenterLat.Enabled = false;
                ZoomCenterLong.Enabled = false;
                ZoomDelta.Enabled = false;
            }
            else if (TypeSelect2D.SelectedIndex == 1)
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
            }
            foreach (string objectName in objectNames)
            {
                className = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "Sensor" && className != "Antenna" && className != "Transmitter" && className != "Receiver" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Radar" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    combo.Items.Add(simpleName);
                    if (className+"/"+simpleName == CommonData.SavedViewList[CommonData.SelectedIndex].ViewTarget)
                    {
                        combo.SelectedIndex = combo.Items.Count - 1;
                    }
                }
            }

        }

        private void PopulateBoxes()
        {
            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseAnimationTime)
            {
                CurrentTime.Text = CommonData.SavedViewList[CommonData.SelectedIndex].AnimationTime;
            }

            TypeSelect2D.Items.Add("ZoomedOut");
            TypeSelect2D.Items.Add("SpecifyCenter");
            TypeSelect2D.Items.Add("ObjectCenter");
            foreach (string item in TypeSelect2D.Items)
            {
                if (item == CommonData.SavedViewList[CommonData.SelectedIndex].ViewType2D)
                {
                    TypeSelect2D.SelectedItem = item;
                }
            }

            GroundTrackBox.Enabled = false;
            if (CommonData.SavedViewList[CommonData.SelectedIndex].EnableUniversalGroundTrack)
            {
                EnableUniversalGroundTrack.Checked = true;
            }
            LeadType2D.Items.Add("OnePass");
            LeadType2D.Items.Add("All");
            LeadType2D.Items.Add("Full");
            LeadType2D.Items.Add("Half");
            LeadType2D.Items.Add("Quarter");
            LeadType2D.Items.Add("None");
            LeadType2D.Items.Add("Time");
            foreach (string item in LeadType2D.Items)
            {
                if (CommonData.SavedViewList[CommonData.SelectedIndex].LeadType.Contains(item))
                {
                    LeadType2D.SelectedItem = item;
                    if (item=="Time")
                    {
                        GroundLeadTime.Text = CommonData.SavedViewList[CommonData.SelectedIndex].LeadTime;
                    }
                }
            }

            TrailType2D.Items.Add("SameAsLead");
            TrailType2D.Items.Add("OnePass");
            TrailType2D.Items.Add("All");
            TrailType2D.Items.Add("Full");
            TrailType2D.Items.Add("Half");
            TrailType2D.Items.Add("Quarter");
            TrailType2D.Items.Add("None");
            TrailType2D.Items.Add("Time");
            foreach (string item in TrailType2D.Items)
            {
                if (CommonData.SavedViewList[CommonData.SelectedIndex].TrailType.Contains(item))
                {
                    TrailType2D.SelectedItem = item;
                    if (item == "Time")
                    {
                        GroundTrailTime.Text = CommonData.SavedViewList[CommonData.SelectedIndex].TrailTime;
                    }
                }
            }

            ViewName2D.Text = CommonData.SavedViewList[CommonData.SelectedIndex].Name;
            ZoomCenterLong.Text = CommonData.SavedViewList[CommonData.SelectedIndex].ZoomCenterLong;
            ZoomCenterLat.Text = CommonData.SavedViewList[CommonData.SelectedIndex].ZoomCenterLat;
            ZoomDelta.Text = CommonData.SavedViewList[CommonData.SelectedIndex].ZoomCenterDelta;

            List<string> windowNames = SmartViewFunctions.GetWindowNames(0);
            foreach (var item in windowNames)
            {
                WindowSelect.Items.Add(item);
                if (item.Contains(CommonData.SavedViewList[CommonData.SelectedIndex].WindowName))
                {
                    WindowSelect.SelectedIndex = WindowSelect.Items.Count - 1;
                }
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].ShowGroundSensors)
            {
                //SensorCoverage2D.Checked = true;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].ShowAerialSensors)
            {
                //AerialSensorCoverage2D.Checked = true;
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

            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseAnimationTime)
            {
                string currentTime = CommonData.SavedViewList[CommonData.SelectedIndex].AnimationTime;
                UseCurrentTime.Checked = true;
                CurrentTime.Text = currentTime;
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

        private void RefreshTime_Click(object sender, EventArgs e)
        {
            IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
            double currentTime = animationRoot.CurrentTime;
            string newTime = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", currentTime.ToString());
            CurrentTime.Text = newTime;
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
