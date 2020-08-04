using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.SmartView
{
    public partial class EditGeoDriftForm : Form
    {
        public EditGeoDriftForm()
        {
            InitializeComponent();
            CommonData.CurrentViewObjectData = SmartViewFunctions.GetObjectData();
            PopulateComboBoxes();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            ViewData current = new ViewData();
            string className = null;
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

        private void GEODisplayObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GEODisplayObject.SelectedIndex!=-1)
            {
                string className = SmartViewFunctions.GetClassName(GEODisplayObject.Text);
                IAgVODataDisplayCollection ddCollection = null;
                IAgSatellite myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + GEODisplayObject.Text) as IAgSatellite;
                ddCollection = myObject.VO.DataDisplay;
                Array reportNames = ddCollection.AvailableData;
                foreach (var name in reportNames)
                {
                    GEODisplayReport.Items.Add(name);
                    if (name.ToString() == CommonData.SavedViewList[CommonData.SelectedIndex].GeoDataDisplayReportName)
                    {
                        GEODisplayReport.SelectedIndex = GEODisplayReport.Items.Count - 1;
                    }
                }
                if (GEODisplayReport.SelectedIndex==-1)
                {
                    GEODisplayReport.SelectedIndex = 0;
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

        private void PopulateComboBoxes()
        {
            GEOViewName.Text = CommonData.SavedViewList[CommonData.SelectedIndex].Name;
            List<string> windowNames = SmartViewFunctions.GetWindowNames(1);
            foreach (var item in windowNames)
            {
                WindowSelect.Items.Add(item);
                if (item.Contains(CommonData.SavedViewList[CommonData.SelectedIndex].WindowName))
                {
                    WindowSelect.SelectedIndex = WindowSelect.Items.Count - 1;
                }
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseAnimationTime)
            {
                CurrentTime.Text = CommonData.SavedViewList[CommonData.SelectedIndex].AnimationTime;
            }

            GEOLongitude.Text = CommonData.SavedViewList[CommonData.SelectedIndex].GeoLongitude;
            GEORadius.Text = CommonData.SavedViewList[CommonData.SelectedIndex].GeoRadius;
            GeoNorthSouth.Text = CommonData.SavedViewList[CommonData.SelectedIndex].GeoNorthSouth;
            GEOEastWest.Text = CommonData.SavedViewList[CommonData.SelectedIndex].GeoEastWest;

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
            if (CommonData.SavedViewList[CommonData.SelectedIndex].GeoDataDisplayActive)
            {
                foreach (string item in GEODisplayLocation.Items)
                {
                    if (CommonData.SavedViewList[CommonData.SelectedIndex].GeoDataDisplayLocation.Contains(item))
                    {
                        GEODisplayLocation.SelectedItem = item;
                    }
                }
            }

            foreach (ObjectData item in CommonData.CurrentViewObjectData)
            {
                if (item.ClassName == "Satellite")
                {
                    GEOViewTarget.Items.Add(item.SimpleName);
                    GEODisplayObject.Items.Add(item.SimpleName);
                    if (CommonData.SavedViewList[CommonData.SelectedIndex].GeoDataDisplayObject.Contains(item.SimpleName))
                    {
                        GEODisplayObject.SelectedIndex = GEODisplayObject.Items.Count - 1;
                    }

                    if (CommonData.SavedViewList[CommonData.SelectedIndex].ViewTarget.Contains(item.SimpleName))
                    {
                        GEOViewTarget.SelectedIndex = GEOViewTarget.Items.Count - 1;
                    }
                }
            }
            if (GEOViewTarget.SelectedIndex==-1)
            {
                GEOViewTarget.SelectedIndex = 0;
            }
            if (GEODisplayObject.SelectedIndex==-1)
            {
                GEODisplayObject.SelectedIndex = 0;
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

            if (CommonData.SavedViewList[CommonData.SelectedIndex].GeoDataDisplayActive)
            {
                GEOUseDataDisplay.Checked = true;
                GEODataDisplayOptions.Enabled = true;
            }
            else
            {
                GEODataDisplayOptions.Enabled = false;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].EnableGeoBox)
            {
                UseGEOBox.Checked = true;
                GeoBoxOptions.Enabled = true;
            }
            else
            {
                GeoBoxOptions.Enabled = true;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseAnimationTime)
            {
                string currentTime = CommonData.SavedViewList[CommonData.SelectedIndex].AnimationTime;
                UseCurrentTime.Checked = true;
                CurrentTime.Text = currentTime;
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

        private void GEODriftDefinitionBox_Enter(object sender, EventArgs e)
        {

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
    }
}
