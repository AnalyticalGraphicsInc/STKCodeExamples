using System;
using System.IO;
using System.Windows.Forms;
using AGI.Ui.Plugins;

namespace Stk12.UiPlugin.TetkExample
{
    public partial class TestConnectUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;

        public TestConnectUserInterface()
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
            CommonData.StkRoot.OnLogMessage -= StkRoot_OnLogMessage;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_stkObjectsLibrary = new StkObjectsLibrary();

            CommonData.StkRoot.OnLogMessage += StkRoot_OnLogMessage;
        }

        #endregion

        private void StkRoot_OnLogMessage(string Message, AGI.STKUtil.AgELogMsgType MsgType, int ErrorCode, string Filename, int LineNo, AGI.STKUtil.AgELogMsgDispID DispID)
        {
            if (MsgType == AGI.STKUtil.AgELogMsgType.eLogMsgAlarm)
            {
                MessageBox.Show(Message);
            }
        }

        private void button_RunConnectCommand_Click(object sender, EventArgs e)
        {
            if (textBox_ConnectCommand != null)
            {
                string cmd = textBox_ConnectCommand.Text;
                try
                {
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

        private void button_ExportConnectCmdsToList_Click(object sender, EventArgs e)
        {
            // Get scenario directory
            string scenarioDirectory = CommonData.StkRoot.ExecuteCommand("GetDirectory / Scenario")[0];

            using StreamWriter outputFile = new StreamWriter(Path.Combine(scenarioDirectory, "ExportedConnectCommandList.txt"));
            {
                foreach (string cmd in CommandList.cmdList)
                    outputFile.WriteLine(cmd);
            }
            
        }
    }
}
