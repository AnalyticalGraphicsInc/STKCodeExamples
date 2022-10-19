using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AGI.Realtime.Examples.Output
{
    /// <summary>
    /// A dialog plug-in which configures an instace of OutputTextFile.
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
    [Guid(OutputTextFileForm.ClassId),
    ProgId(OutputTextFileForm.ProgID),
    ClassInterface(ClassInterfaceType.None)]
    public partial class OutputTextFileForm :
        Form,
        IAgUiRtDialog,
        IAgUiRtProvideTrackingData
    {
        #region OutputTextFileForm Members

        public OutputTextFileForm()
        {
            InitializeComponent();
        }

        private void OutputTextFileForm_Load(object sender, EventArgs e)
        {
            m_filename.Text = m_plugin.Filename;
        }

        private void m_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = false;
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.FileName = m_filename.Text;
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
                m_plugin = value as OutputTextFile;
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

        #region IAgUiRtProvideTrackingData Members

        /// <summary>
        /// If OutputEntity multiselect, this function would
        /// simply an instance of it.
        /// </summary>
        /// <param name="EntityIDs"></param>
        /// <returns></returns>
        public IAgUiRtWindowHandle GetUiForEntities(ref Array EntityIDs)
        {
            //Only support single select
            return null;
        }

        /// <summary>
        /// Return an instance of OutputEntity for configuring
        /// the specified entity.
        /// </summary>
        /// <param name="EntityID"></param>
        /// <returns></returns>
        public IAgUiRtWindowHandle GetUiForEntity(string EntityID)
        {
            return new OutputEntity(EntityID, m_plugin);
        }

        #endregion

        #region Variables, Constants, Events & Delegates
        
        //Every RT3 plug-in needs to have a unique ProgID.  You should change
        //the below ProgID to match any new classes you create.
        public const string ProgID = "AGI.Realtime.Examples.Output.OutputTextFileForm";

        //Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
        //your class using GUIDGen, located in "Tools->Create GUID" in Visual
        //Studio or http://www.guidgen.com/
        public const string ClassId = "0E5C86CE-BD0D-4899-A8F8-F22AFBA3DC17";
        
        private OutputTextFile m_plugin;

        #endregion
    }
}