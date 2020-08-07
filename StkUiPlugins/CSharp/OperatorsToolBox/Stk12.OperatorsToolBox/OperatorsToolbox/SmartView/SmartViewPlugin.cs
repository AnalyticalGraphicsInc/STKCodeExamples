using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.SmartView
{
    public partial class SmartViewPlugin : OpsPluginControl
    {
        int _selectedView = -1;
        public SmartViewPlugin()
        {
            InitializeComponent();
            PopulateStoredViews();
        }

        private void NewView_Click(object sender, EventArgs e)
        {
            CommonData.NewView = false;
            NewViewForm form = new NewViewForm();
            form.ShowDialog();
            if (CommonData.NewView)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Text = CommonData.SavedViewList.Last().Name;
                listItem.SubItems.Add(CommonData.SavedViewList.Last().ViewType);

                StoredViewList.Items.Add(listItem);
            }
        }

        private void RemoveView_Click(object sender, EventArgs e)
        {
            if (StoredViewList.SelectedItems != null && StoredViewList.SelectedItems.Count > 0)
            {
                CommonData.SavedViewList.RemoveAt(StoredViewList.FocusedItem.Index);
                StoredViewList.Items.RemoveAt(StoredViewList.FocusedItem.Index);
                ReadWrite.WriteSavedViews(CommonData.DirectoryStr + "\\StoredViewData.json");
                //ReadWrite.WriteObjectData(CommonData.DirectoryStr + "\\StoredObjectData.txt");
            }
        }

        private void EditView_Click(object sender, EventArgs e)
        {
            if (StoredViewList.SelectedItems != null && StoredViewList.SelectedItems.Count > 0)
            {
                CommonData.UpdatedView = false;
                if (CommonData.SavedViewList[CommonData.SelectedIndex].ViewType == "2D")
                {
                    Edit2DGenericForm form = new Edit2DGenericForm();
                    form.ShowDialog();
                }
                else if (CommonData.SavedViewList[CommonData.SelectedIndex].ViewType == "3D")
                {
                    Edit3DGenericForm form = new Edit3DGenericForm();
                    form.ShowDialog();
                }
                else if (CommonData.SavedViewList[CommonData.SelectedIndex].ViewType == "Target/Threat")
                {
                    EditTargetThreatForm form = new EditTargetThreatForm();
                    form.ShowDialog();
                }
                else if (CommonData.SavedViewList[CommonData.SelectedIndex].ViewType == "GEODrift")
                {
                    EditGeoDriftForm form = new EditGeoDriftForm();
                    form.ShowDialog();
                }
                if (CommonData.UpdatedView == true)
                {
                    StoredViewList.Items.Clear();
                    foreach (var item in CommonData.SavedViewList)
                    {
                        ListViewItem listItem = new ListViewItem();
                        listItem.Text = item.Name;
                        listItem.SubItems.Add(item.ViewType);

                        StoredViewList.Items.Add(listItem);
                    }
                    ReadWrite.WriteObjectData(CommonData.DirectoryStr + "\\StoredObjectData.txt");
                }
            }
        }

        private void PopulateStoredViews()
        {
            CommonData.SavedViewList.Clear();
            if (File.Exists(CommonData.DirectoryStr + "\\StoredViewData.json"))
                CommonData.SavedViewList = ReadWrite.ReadSavedViews(CommonData.DirectoryStr + "\\StoredViewData.json");

            foreach (var item in CommonData.SavedViewList)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Text = item.Name;
                listItem.SubItems.Add(item.ViewType);

                StoredViewList.Items.Add(listItem);
            }
        }

        private void StoredViewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StoredViewList.FocusedItem != null && StoredViewList.FocusedItem.Index != -1)
            {
                StoredViewList.Items[StoredViewList.FocusedItem.Index].Selected = true;
                CommonData.SelectedIndex = StoredViewList.FocusedItem.Index;
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            RemovePlanes();
            CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
            foreach (ObjectData item in CommonData.InitialObjectData)
            {
                SmartViewFunctions.SetAllObjectData(item);
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO " + item.SimplePath + " DynDataText RemoveAll");
                }
                catch (Exception)
                {

                }
            }
            CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
        }

        private void RemovePlanes()
        {
            List<ObjectData> data = SmartViewFunctions.GetObjectData();
            foreach (ObjectData item in data)
            {
                if (item.ClassName == "Satellite")
                {
                    IAgStkObject sat = CommonData.StkRoot.GetObjectFromPath(item.SimplePath);
                    IAgSatellite sat1 = CommonData.StkRoot.GetObjectFromPath(item.SimplePath) as IAgSatellite;
                    try
                    {
                        sat.Vgt.Planes.Remove("ProximityPlane");
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        sat1.VO.Proximity.GeoBox.IsVisible = false;
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        sat1.VO.Proximity.Ellipsoid.IsVisible = false;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            if (_selectedView != -1)
            {
                if (StoredViewList.SelectedItems != null && StoredViewList.SelectedItems.Count > 0)
                {
                    RemovePlanes();
                    if (CommonData.SavedViewList[_selectedView].ViewType == "2D")
                    {
                        SmartViewFunctions.Change2DView(CommonData.SavedViewList[_selectedView]);
                    }
                    else if (CommonData.SavedViewList[_selectedView].ViewType == "3D")
                    {
                        SmartViewFunctions.Change3DView(CommonData.SavedViewList[_selectedView]);
                    }
                    else if (CommonData.SavedViewList[_selectedView].ViewType == "Target/Threat")
                    {
                        SmartViewFunctions.ChangeTargetThreatView(CommonData.SavedViewList[_selectedView]);
                    }
                    else if (CommonData.SavedViewList[_selectedView].ViewType == "GEODrift")
                    {
                        SmartViewFunctions.ChangeGeoDriftView(CommonData.SavedViewList[_selectedView]);
                    }
                }
            }
        }

        private void SelectView_Click(object sender, EventArgs e)
        {
            if (StoredViewList.SelectedItems != null && StoredViewList.SelectedItems.Count > 0)
            {
                _selectedView = StoredViewList.FocusedItem.Index;
                RemovePlanes();
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");

                StoredViewList.Items[StoredViewList.FocusedItem.Index].Selected = true;
                CommonData.SelectedIndex = StoredViewList.FocusedItem.Index;
                if (CommonData.SavedViewList[StoredViewList.FocusedItem.Index].ViewType == "2D")
                {
                    SmartViewFunctions.Change2DView(CommonData.SavedViewList[StoredViewList.FocusedItem.Index]);
                }
                else if (CommonData.SavedViewList[StoredViewList.FocusedItem.Index].ViewType == "3D")
                {
                    SmartViewFunctions.Change3DView(CommonData.SavedViewList[StoredViewList.FocusedItem.Index]);
                }
                else if (CommonData.SavedViewList[StoredViewList.FocusedItem.Index].ViewType == "Target/Threat")
                {
                    SmartViewFunctions.ChangeTargetThreatView(CommonData.SavedViewList[StoredViewList.FocusedItem.Index]);
                }
                else if (CommonData.SavedViewList[StoredViewList.FocusedItem.Index].ViewType == "GEODrift")
                {
                    SmartViewFunctions.ChangeGeoDriftView(CommonData.SavedViewList[StoredViewList.FocusedItem.Index]);
                }

                CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        #region Tool Tips
        private void NewView_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.NewView, "New View");
        }

        private void EditView_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.EditView, "Edit Selected View");
        }

        private void RemoveView_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.RemoveView, "Remove Selected View");
        }

        private void Refresh_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Refresh, "Refresh Current View");
        }

        private void Reset_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Reset, "Reset Objects to Scenario Load");
        }
        #endregion
    }
}
