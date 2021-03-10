using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using OperatorsToolbox.SmartView;

namespace OperatorsToolbox.Coverage
{
    public partial class CoveragePlugin : OpsPluginControl
    {
        public CoveragePlugin()
        {
            InitializeComponent();
            CommonData.CoverageList = new List<CoverageData>();
            PopulateCoverageList();
        }

        private void CoverageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoverageList.SelectedItems != null && CoverageList.SelectedIndices.Count>0)
            {
                if (!CommonData.CoverageList[CoverageList.FocusedItem.Index].IsCustom)
                {
                    CommonData.CoverageIndex = CoverageList.FocusedItem.Index;
                    if (CommonData.CoverageList[CoverageList.FocusedItem.Index].Type.Contains("Object AOI"))
                    {
                        if (CommonData.CoverageList[CommonData.CoverageIndex].TargetName == "Earth")
                        {
                            HomeView_Click(sender, e);
                        }
                        else
                        {
                            string className = SmartViewFunctions.GetClassName(CommonData.CoverageList[CommonData.CoverageIndex].TargetName);
                            try
                            {
                                string cmd = "VO * View FromTo FromRegName \"STK Object\" FromName \"" + className + "/" + CommonData.CoverageList[CommonData.CoverageIndex].TargetName + "\" ToRegName  \"STK Object\" ToName  \"" + className + "/" + CommonData.CoverageList[CoverageList.FocusedItem.Index].TargetName + "\" WindowID 1";
                                CommonData.StkRoot.ExecuteCommand(cmd);
                                CommonData.StkRoot.ExecuteCommand("VO * View Top WindowID 1");
                                CommonData.StkRoot.ExecuteCommand("VO * View Zoom WindowID 1 FractionofCB -1");
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    else if (CommonData.CoverageList[CommonData.CoverageIndex].Type.Contains("Country/Region"))
                    {
                        try
                        {
                            string cmd = "VO * View FromTo FromRegName \"STK Object\" FromName \"AreaTarget/" + CommonData.CoverageList[CommonData.CoverageIndex].Country.Replace(' ', '_') + "\" ToRegName  \"STK Object\" ToName  \"AreaTarget/" + CommonData.CoverageList[CoverageList.FocusedItem.Index].Country.Replace(' ', '_') + "\" WindowID 1";
                            CommonData.StkRoot.ExecuteCommand(cmd);
                            CommonData.StkRoot.ExecuteCommand("VO * View Top WindowID 1");
                            CommonData.StkRoot.ExecuteCommand("VO * View Zoom WindowID 1 FractionofCB -1");
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else if (CommonData.CoverageList[CommonData.CoverageIndex].Type.Contains("Global"))
                    {
                        HomeView_Click(sender, e);
                    }
                    CoverageDetails.Text = "";
                    string details = ReadWrite.WriteCoverageDetails(CommonData.CoverageList[CommonData.CoverageIndex].CdName);
                    CoverageDetails.Text = details;

                    CoverageFunctions.RemoveFomLegends();
                    CoverageFunctions.ShowLegend();
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void NewCoverage_Click(object sender, EventArgs e)
        {
            CommonData.CovEdit = false;
            CoverageOptionsForm form = new CoverageOptionsForm();
            form.ShowDialog();
            if (!CommonData.CovDefFail && CommonData.NewCoverage && CommonData.CoverageCompute)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + CommonData.OaName + " Access Compute");
                }
                catch (Exception)
                {
                    string mes = "Could not Compute Coverage";
                    MessageBox.Show(mes);
                }
            }

            try
            {
                ReadWrite.WriteCoverageData(CommonData.DirectoryStr + "\\CoverageData.txt");
            }
            catch (Exception)
            {

                MessageBox.Show("Could not write file");
            }
            PopulateCoverageList();
        }

        private void EditCoverage_Click(object sender, EventArgs e)
        {
            if (CoverageList.FocusedItem != null && CoverageList.FocusedItem.Index != -1)
            {
                CommonData.CovEdit = true;
                if (CommonData.CoverageList[CommonData.CoverageIndex].IsCustom)
                {
                    MessageBox.Show("This is a custom Coverage Definition (i.e. not created in this plugin).\nOnly access can be computed from this interface.");
                }
                else
                {
                    CoverageOptionsForm form = new CoverageOptionsForm();
                    form.ShowDialog();
                    if (!CommonData.CovDefFail && CommonData.CoverageCompute)
                    {
                        try
                        {
                            CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + CommonData.OaName + " Access Compute");
                        }
                        catch (Exception)
                        {
                            string mes = "Could not Compute Coverage";
                            MessageBox.Show(mes);
                        }
                    }

                    try
                    {
                        ReadWrite.WriteCoverageData(CommonData.DirectoryStr + "\\CoverageData.txt");
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Could not write file");
                    }
                    PopulateCoverageList();
                }
            }
        }

        private void Compute_Click(object sender, EventArgs e)
        {
            if (CoverageList.FocusedItem != null && CoverageList.FocusedItem.Index != -1)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + CommonData.CoverageList[CommonData.CoverageIndex].CdName + " Access Compute");
                }
                catch (Exception)
                {
                    string mes = "Could not Compute Coverage";
                    MessageBox.Show(mes);
                }
            }
        }

        private void RemoveCoverage_Click(object sender, EventArgs e)
        {
            if (CoverageList.FocusedItem != null && CoverageList.FocusedItem.Index != -1)
            {
                try
                {
                    IAgStkObject obj = CommonData.StkRoot.CurrentScenario.Children[CommonData.CoverageList[CommonData.CoverageIndex].CdName];
                    obj.Unload();
                    StkObjectsLibrary library = new StkObjectsLibrary();
                    IAgExecCmdResult result;
                    result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class AreaTarget");
                    if (result[0] != "None")
                    {
                        string[] facArray = result[0].Split(null);
                        foreach (var item in facArray)
                        {
                            if (item != null && item != "" && item.Contains(CommonData.CoverageList[CommonData.CoverageIndex].CdName))
                            {
                                obj = CommonData.StkRoot.GetObjectFromPath(library.TruncatedObjectPath(item));
                                obj.Unload();
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
                CommonData.CoverageList.RemoveAt(CommonData.CoverageIndex);
                CoverageList.Items.RemoveAt(CommonData.CoverageIndex);
                HomeView_Click(sender, e);

                try
                {
                    ReadWrite.WriteCoverageData(CommonData.DirectoryStr + "\\CoverageData.txt");
                }
                catch (Exception)
                {

                    MessageBox.Show("Could not write file");
                }
                PopulateCoverageList();
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

        private void PopulateCoverageList()
        {
            CoverageList.Items.Clear();
            CommonData.CoverageList.Clear();
            List<CoverageData> savedData = new List<CoverageData>();
            int index = -1;
            if (File.Exists(CommonData.DirectoryStr + "\\CoverageData.txt"))
            {
                savedData = ReadWrite.ReadCoverageData(CommonData.DirectoryStr + "\\CoverageData.txt");
            }
            IAgExecCmdResult result;
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class CoverageDefinition");
            if (result[0] != "None")
            {
                string[] constArray = result[0].Split(null);
                foreach (var item in constArray)
                {
                    string newItem = item.Split('/').Last();
                    var listItem = new ListViewItem();
                    if (newItem != "" && newItem != null)
                    {
                        index = savedData.FindIndex(p => p.CdName.Equals(newItem));
                        if (index!=-1)
                        {
                            savedData[index].IsCustom = false;
                            CommonData.CoverageList.Add(savedData[index]);

                        }
                        else
                        {
                            CoverageData newCoverage = new CoverageData();
                            newCoverage.CdName = newItem;
                            newCoverage.IsCustom = true;
                            CommonData.CoverageList.Add(newCoverage);
                        }
                        listItem.SubItems[0].Text = newItem;
                        CoverageList.Items.Add(listItem);
                    }
                }
            }

        }

        #region ToopTips
        private void NewCoverage_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.NewCoverage, "New Coverare");
        }

        private void EditCoverage_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.EditCoverage, "Edit Coverare");
        }

        private void Compute_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Compute, "Compute Coverare");
        }

        private void RemoveCoverage_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.RemoveCoverage, "Delete Coverare");
        }

        private void HomeView_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.HomeView, "Home View");
        }

        private void ClearLegends_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.ClearLegends, "Clear FOM Legends");
        }
        #endregion

        private void ClearLegends_Click(object sender, EventArgs e)
        {
            CoverageFunctions.RemoveFomLegends();
        }
    }
}
