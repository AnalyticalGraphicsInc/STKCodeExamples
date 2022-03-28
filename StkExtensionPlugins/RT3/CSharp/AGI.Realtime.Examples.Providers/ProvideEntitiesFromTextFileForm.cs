using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AGI.Realtime.Examples.Providers
{
    /// <summary>
    /// A dialog plug-in which configures an instace of ProvideEntitiesFromFile.
    /// 
    /// This class implements IAgUiRtDialog, which allows for an application to
    /// discover a GUI at runtime which can be used to configure the associated
    /// plug-in.  It is the only required interface for a dialog plug-in.
    /// 
    /// To use this class in RT3, simply compile the library and load the
    /// included reg file into the Windows registry.
    /// 
    /// For more information on RT3 interfaces and class, please see the RT3
    /// Development Kit documentation.
    /// </summary>
    [Guid(ProvideEntitiesFromTextFileForm.ClassId),
    ProgId(ProvideEntitiesFromTextFileForm.ProgID),
    ClassInterface(ClassInterfaceType.None)]
    public partial class ProvideEntitiesFromTextFileForm :
        Form,
        IAgUiRtDialog
    {
        #region ProvideEntitiesFromTextFileForm Members

        public ProvideEntitiesFromTextFileForm()
        {
            InitializeComponent();
        }

        private void ProvideEntitiesFromTextFileForm_Load(object sender, EventArgs e)
        {
            m_filename.Text = m_plugin.Filename;

            //If we are running, make the GUI read-only
            if (m_plugin.Active)
            {
                m_browse.Enabled = false;
            }
        }

        private void m_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = m_filename.Text;
            dlg.Filter = "All Files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_filename.Text = dlg.FileName;
            }
        }

        private void m_ok_Click(object sender, EventArgs e)
        {
            m_plugin.Filename = m_filename.Text;
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region IAgUiRtDialog Members

        /// <summary>
        /// Gets or sets the plug-in to be configured.
        /// </summary>
        public object Data
        {
            get
            {
                return m_plugin;
            }
            set
            {
                m_plugin = value as ProvideEntitiesFromTextFile;
            }
        }

        /// <summary>
        /// Displays the dialog, either modal or modeless, and configures
        /// the plug-in that has been passed in as the Data property.
        /// </summary>
        /// <param name="Parent">The HWND of the parent window.</param>
        /// <returns>The result of the dialog.</returns>
        public AgEUiRtDialogResult Activate(ref int Parent)
        {
            if (ShowDialog() == DialogResult.Cancel)
            {
                return AgEUiRtDialogResult.eUiRtDialogResultCancel;
            }

            return AgEUiRtDialogResult.eUiRtDialogResultOK;
        }

        #endregion

        #region Variables, Constants, Events & Delegates
        
        //Every RT3 plug-in needs to have a unique ProgID.  You should change
        //the below ProgID to match any new classes you create.
        public const string ProgID = "AGI.Realtime.Examples.Providers.ProvideEntitiesFromTextFileForm";

        //Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
        //your class using GUIDGen, located in "Tools->Create GUID" in Visual
        //Studio or http://www.guidgen.com/
        public const string ClassId = "22232A1B-8C0C-4d25-BC3E-D30F11BB1453";
        
        private ProvideEntitiesFromTextFile m_plugin;

        #endregion
    }
}