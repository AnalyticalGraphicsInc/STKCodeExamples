using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using System.IO;

namespace ConstrainedAttitude.UiPlugin
{
    public partial class CustomConfigPage : UserControl, IAgUiPluginConfigurationPageActions2
    {
        #region Members

        IAgUiPluginConfigurationPageSite m_site;
        Setup m_plugin;

        #endregion

        public CustomConfigPage()
        {
            InitializeComponent();
        }

        public bool OnApply()
        {
            SaveChanges();
            return true;
        }

        public void OnCancel()
        {
            // Intentionally left empty
        }

        public void OnCreated(IAgUiPluginConfigurationPageSite Site)
        {
            m_site = Site;
            m_plugin = m_site.Plugin as Setup;

            txtStringValue.Text = m_plugin.StringValue;
            nudDoubleValue.Value = (decimal)m_plugin.DoubleValue;
        }

        public void OnHelp()
        {

        }

        public void OnOK()
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            m_plugin.StringValue = txtStringValue.Text;
            m_plugin.DoubleValue = (double)nudDoubleValue.Value;

            using (StreamWriter streamWriter = new StreamWriter(m_plugin.PrefPath))
            {
                streamWriter.WriteLine(txtStringValue.Text);
                streamWriter.WriteLine(nudDoubleValue.Value.ToString());
                streamWriter.Close();
            }
        }
    }
}
