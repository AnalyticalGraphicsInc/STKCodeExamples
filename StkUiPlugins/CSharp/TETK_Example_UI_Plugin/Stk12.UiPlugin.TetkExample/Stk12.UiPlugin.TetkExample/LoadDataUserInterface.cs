using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AGI.Ui.Plugins;

namespace Stk12.UiPlugin.TetkExample
{
    public partial class LoadDataUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;

        public LoadDataUserInterface()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Implementation
        public stdole.IPictureDisp GetIcon()
        {
            return null;
        }

        public void OnClosing()
        {
            //CommonData.StkRoot.OnStkObjectAdded -= m_root_OnStkObjectAdded;
            //CommonData.StkRoot.OnStkObjectDeleted -= m_root_OnStkObjectDeleted;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_stkObjectsLibrary = new StkObjectsLibrary();
        }

        #endregion

        private void button_ImportDataMappings_Click(object sender, EventArgs e)
        {
            try
            {
                // TE_AnalysisObject Connect command (https://help.agi.com/stkdevkit/index.htm#../Subsystems/connectCmds/Content/cmd_TE_AnalysisObject.htm)
                string cmd = @"TE_Mapping * Import File ""C:\AGI\TETK_Notional_Test_Datasets\TETK_Example_Plugin.tedm""";
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommandList.cmdList.Add(cmd);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Error:" + exception);
                return;
            }
        }
        private void button_LoadOwnship_Click(object sender, EventArgs e)
        {
            try
            {
                // TE_AnalysisObject Connect command (https://help.agi.com/stkdevkit/index.htm#../Subsystems/connectCmds/Content/cmd_TE_AnalysisObject.htm)
                string cmd =
                    @"TE_AnalysisObject * Add File ""C:\AGI\TETK_Notional_Test_Datasets\Flight_Test_Data\Flight385_XPlane_F35_EdwardsAFB_12Mar2021.csv"" Name ""F35"" ObjectType ""Aircraft"" Model ""C:\Program Files\AGI\STK 12\STKData\VO\Models\Air\f-35_jsf_cv.mdl"" TSPIMapping ""Flight385_Ownship_TSPI"" TimeColumn ""mission_time"" TimeEpoch ""26 Jan 2021 17:00:00.000"" TimeUnits ""second""";
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommandList.cmdList.Add(cmd);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error:" + exception);
                return;
            }
        }

        private void button_LoadAssociatedState_Click(object sender, EventArgs e)
        {
            List<string> acNum = new List<string> {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10"};
            try
            {
                // TE_AssociatedState Connect command (https://help.agi.com/stkdevkit/index.htm#../Subsystems/connectCmds/Content/cmd_TE_AssociatedObject.htm)
                for (int i = 0; i < acNum.Count; i++)
                { 
                    string cmd = string.Format(@"TE_AssociatedObject * Add File ""C:\AGI\TETK_Notional_Test_Datasets\Flight_Test_Data\Aircraft_{0}.txt"" Name ""Aircraft_{0}"" Ownship ""F35"" ObjectType ""Aircraft"" Model ""C:\Program Files\AGI\STK 12\STKData\VO\Models\Air\fighter.mdl"" TSPIMapping ""Flight385_AssocState_TSPI"" TimeColumn ""Time""", acNum[i]);
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommandList.cmdList.Add(cmd);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error:" + exception);
                return;
            }

        }

        private void button_LoadAdditionalData_Click(object sender, EventArgs e)
        {
            try
            {
                // TE_AdditionalDate Connect command (https://help.agi.com/stkdevkit/index.htm#../Subsystems/connectCmds/Content/cmd_TE_AdditionalData.htm)
                string cmd = @"TE_AdditionalData * Add File ""C:\AGI\TETK_Notional_Test_Datasets\TE_26 Jan 2021 17-00-00-050250000\Target1_Sensor1_Track2-Merged.txt"" Ownship ""F35"" TimeColumn ""Time""";
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommandList.cmdList.Add(cmd);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error:" + exception);
                return;
            }



        }


    }
}
