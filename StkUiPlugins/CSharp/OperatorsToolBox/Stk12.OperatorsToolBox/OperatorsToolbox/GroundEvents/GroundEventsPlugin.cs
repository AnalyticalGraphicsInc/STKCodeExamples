using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKVgt;

namespace OperatorsToolbox.GroundEvents
{
    public partial class GroundEventsPlugin : OpsPluginControl
    {
        public int SelectedIndex { get; set; }
        public GroundEventsPlugin()
        {
            InitializeComponent();
            CommonData.CurrentEvents = new List<GroundEvent>();
            CommonData.Preferences.SensorGraphicsDisplay = true;
            GroundEventFunctions.CreateTimeLineView();
            PopulateSsRs();
            string cmd = "VO * Declutter Enable On";
            CommonData.StkRoot.ExecuteCommand(cmd);
        }

        private void NewSSR_Click(object sender, EventArgs e)
        {
            int oGnumSsr = CommonData.CurrentEvents.Count;
            int startIndex = oGnumSsr;
            if (oGnumSsr == 0)
            {
                startIndex = 0;
            }
            CommonData.NewSsrCreated = false;
            NewGroundEventForm form = new NewGroundEventForm();
            form.ShowDialog();
            if (CommonData.NewSsrCreated == true)
            {
                imageList1.Images.Clear();
                foreach (var item in CommonData.Preferences.EventImageLocations)
                {
                    try
                    {
                        imageList1.Images.Add(Image.FromFile(item));
                    }
                    catch (Exception)
                    {

                    }
                }
                ListSSR.SmallImageList = imageList1;

                for (int j = 0; j < oGnumSsr; j++)
                {
                    int index = GroundEventFunctions.GetImageIndex(CommonData.CurrentEvents[j]);
                    if (index != -1)
                    {
                        ListSSR.Items[j].ImageIndex = index;
                    }
                }

                int newCount = CommonData.CurrentEvents.Count;
                if (newCount != oGnumSsr)
                {
                    for (int i = startIndex; i < newCount; i++)
                    {
                        var listItem = new ListViewItem();
                        int index = GroundEventFunctions.GetImageIndex(CommonData.CurrentEvents[i]);
                        if (index != -1)
                        {
                            listItem.ImageIndex = index;
                        }
                        listItem.SubItems[0].Text = CommonData.CurrentEvents[i].Id;
                        ListSSR.Items.Add(listItem);
                    }
                }
            }
        }

        private void EditSSR_Click(object sender, EventArgs e)
        {
            if (ListSSR.SelectedItems != null && ListSSR.SelectedItems.Count > 0)
            {
                try
                {
                    IAgStkObject place = CommonData.StkRoot.GetObjectFromPath("Place/" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id);
                    IAgCrdnEventIntervalGroup intervals = place.Vgt.EventIntervals;
                    intervals.Remove(CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-Interval");
                }
                catch (Exception)
                {

                }

                EditForm form = new EditForm();
                form.ShowDialog();
                imageList1.Images.Clear();
                foreach (var item in CommonData.Preferences.EventImageLocations)
                {
                    try
                    {
                        imageList1.Images.Add(Image.FromFile(item));
                    }
                    catch (Exception)
                    {

                    }
                }
                ListSSR.SmallImageList = imageList1;
                //var listItem = new ListViewItem();
                int index = GroundEventFunctions.GetImageIndex(CommonData.CurrentEvents[CommonData.EventSelectedIndex]);
                //listItem.ImageIndex = index;
                //listItem.SubItems[0].Text = CommonData.CurrentSSRs[CommonData.SSRSelectedIndex].ID;
                if (index != -1)
                {
                    ListSSR.Items[CommonData.EventSelectedIndex].ImageIndex = index;
                }
                ListSSR.Items[CommonData.EventSelectedIndex].SubItems[0].Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id;
                if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].StartTime == "Unspecified" || CommonData.CurrentEvents[CommonData.EventSelectedIndex].StopTime == "Unspecified")
                {

                }
                else
                {
                    GroundEventFunctions.UpdateTimelineComponent(CommonData.CurrentEvents[CommonData.EventSelectedIndex]);
                }
                string details = ReadWrite.WriteDetails();
                SSRDetails.Text = details;
            }
        }

        private void SubObjectsClick_Click(object sender, EventArgs e)
        {
            if (ListSSR.SelectedItems != null && ListSSR.SelectedItems.Count > 0)
            {
                SubObjectsForm form = new SubObjectsForm();
                form.ShowDialog();
            }
        }

        private void RemoveSSR_Click(object sender, EventArgs e)
        {
            if (ListSSR.SelectedItems != null && ListSSR.SelectedItems.Count > 0)
            {
                if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects.Count > 0)
                {
                    foreach (var subObject in CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects)
                    {
                        string placeName = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + subObject.Name;
                        string unloadcmd = "Unload / */Place/" + placeName;
                        CommonData.StkRoot.ExecuteCommand(unloadcmd);
                    }
                }
                try
                {
                    string cmd = "Unload / */Place/" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id;
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");
                }
                catch (Exception)
                {
                    //Likely the user deleted the object manually in the browser and it no longer exists or a timeline view is not present to change
                }
                ReadWrite.RemoveEvent();
                ListSSR.Items.RemoveAt(CommonData.EventSelectedIndex);
            }
        }

        private void HomeView_Click(object sender, EventArgs e)
        {
            try
            {
                CommonData.StkRoot.ExecuteCommand("VO * View Home");
            }
            catch (Exception)
            {

            }
        }

        private void ListSSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListSSR.SelectedItems != null && ListSSR.SelectedItems.Count > 0)
            {
                CommonData.EventSelectedIndex = ListSSR.FocusedItem.Index;
                if (CommonData.EventSelectedIndex != -1)
                {
                    try
                    {
                        string cmd = "VO * View FromTo FromRegName \"STK Object\" FromName \"Place/" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "\" ToRegName  \"STK Object\" ToName  \"Place/" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "\" WindowID 1";

                        CommonData.StkRoot.ExecuteCommand(cmd);

                        CommonData.StkRoot.ExecuteCommand("VO * View Top WindowID 1");

                        CommonData.StkRoot.ExecuteCommand("VO * View Zoom WindowID 1 FractionofCB -1");

                        SSRDetails.Text = "";
                        string details = ReadWrite.WriteDetails();
                        SSRDetails.Text = details;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }

        private void PopulateSsRs()
        {
            CommonData.EventFileStr = CommonData.DirectoryStr + "\\" + "CurrentSSRs.txt";
            List<GroundEvent> fileSections = new List<GroundEvent>();
            if (File.Exists(CommonData.DirectoryStr + "\\" + "CurrentSSRs.txt"))
            {
                fileSections = ReadWrite.ReadEventFile(CommonData.EventFileStr);
            }
            imageList1.Images.Clear();
            foreach (var item in CommonData.Preferences.EventImageLocations)
            {
                try
                {
                    imageList1.Images.Add(Image.FromFile(item));
                }
                catch (Exception)
                {

                }
            }
            ListSSR.SmallImageList = imageList1;
            foreach (var item in fileSections)
            {
                if (item != null)
                {
                    CommonData.CurrentEvents.Add(item);
                    try
                    {
                        GroundEventFunctions.CreateGroundEvent(item);
                        //SSRList.Items.Add(item.ID);
                        var listItem = new ListViewItem();
                        int index = GroundEventFunctions.GetImageIndex(item);
                        if (index != -1)
                        {
                            listItem.ImageIndex = index;
                        }
                        listItem.SubItems[0].Text = item.Id;
                        ListSSR.Items.Add(listItem);

                        if (item.SubObjects.Count > 0)
                        {
                            foreach (var subObject in item.SubObjects)
                            {
                                GroundEventFunctions.CreateSubObject(item, subObject);
                            }
                        }

                    }
                    catch (Exception)
                    {
                        string mes = "Could not load SSR: " + item.Id;
                        MessageBox.Show(mes);
                    }
                }
            }

        }
        #region ToopTips
        private void NewSSR_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.NewSSR, "New Event");
        }

        private void EditSSR_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.EditSSR, "Edit Event");
        }

        private void SubObjectsClick_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.SubObjectsClick, "Sub-Objects");
        }

        private void RemoveSSR_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.RemoveSSR, "Delete Event");
        }

        private void HomeView_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.HomeView, "Home View");
        }
        #endregion
    }
}
