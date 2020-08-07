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

namespace OperatorsToolbox
{
    public partial class CustomConfigPage : UserControl, IAgUiPluginConfigurationPageActions2
    {
        #region Members

        IAgUiPluginConfigurationPageSite _mSite;
        Setup _mPlugin;

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

        public void OnCreated(IAgUiPluginConfigurationPageSite site)
        {
            _mSite = site;
            _mPlugin = _mSite.Plugin as Setup;

            txtStringValue.Text = _mPlugin.StringValue;
            nudDoubleValue.Value = (decimal)_mPlugin.DoubleValue;
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
            _mPlugin.StringValue = txtStringValue.Text;
            _mPlugin.DoubleValue = (double)nudDoubleValue.Value;

            using (StreamWriter streamWriter = new StreamWriter(_mPlugin.PrefPath))
            {
                streamWriter.WriteLine(txtStringValue.Text);
                streamWriter.WriteLine(nudDoubleValue.Value.ToString());
                streamWriter.Close();
            }
        }
    }
}
