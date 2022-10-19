using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AGI.Realtime.Examples.Output
{
    [ComSourceInterfaces(typeof(IAgUiRtWindowHandleEvents))]
    public partial class OutputEntity : Form, IAgUiRtWindowHandle
    {
        public OutputEntity(string entityID, OutputTextFile plugin)
        {
            InitializeComponent();

            m_plugin = plugin;
            m_entityID = entityID;

            m_affiliation.Items.Add(Affiliation.Friendly);
            m_affiliation.Items.Add(Affiliation.Hostile);
            m_affiliation.Items.Add(Affiliation.Neutral);

            OutputSettings settings;
            if (m_plugin.GetConfiguration(m_entityID, out settings))
            {
                m_affiliation.SelectedItem = settings.Affiliation;
                m_symbol.Text = settings.Symbology;
            }
            else
            {
                m_affiliation.SelectedItem = Affiliation.Friendly;
                m_symbol.Text = "SFAPMF----*****";
            }
        }

        #region IAgUiRtWindowHandle Members

        public void Apply()
        {
            OutputSettings settings = new OutputSettings((Affiliation)m_affiliation.SelectedItem, m_symbol.Text);
            m_plugin.SetConfiguraiton(m_entityID, settings);

        }

        public object Data
        {
            get
            {
                return m_plugin;
            }
            set
            {
                m_plugin = value as OutputTextFile;
            }
        }

        public int HWND
        {
            get
            {
                return this.Handle.ToInt32();
            }
        }

        #endregion

        public delegate void OnUiModifiedDelegate(IAgUiRtWindowHandle Sender);
        [DispId((int)AgERtEventDispatchID.eUiRtModified)]
        public event OnUiModifiedDelegate OnUiModified;

        private void UiModified(object sender, EventArgs e)
        {
            if (OnUiModified != null)
            {
                OnUiModified(this);
            }
        }

        OutputTextFile m_plugin;
        string m_entityID;
}
}