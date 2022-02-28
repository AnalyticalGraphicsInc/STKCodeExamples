using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Windows.Forms;
using AGI.Ui.Core;
using System.Drawing;
using System.Reflection;
using System.IO;


//************************************************************
//Modify "Platform" to x86 under Project Properties/Build
//Modify Plugin Title and Description 
//Rename xml file and modify "DisplayName" inside xml file
//************************************************************

namespace DataImporter
{

    [Guid("82a8c095-554b-4868-badb-32ef00126317")]
    [ProgId("DataImporter")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Setup : IAgUiPlugin, IAgUiPlugin2, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_pSite;
        private CustomUserInterface m_customUserInterface;
        private AgStkObjectRootClass m_root;
        private IAgProgressTrackCancel m_progress;
        private stdole.IPictureDisp m_picture;

        private string m_stringValue;
        private double m_doubleValue;
        private string m_prefPath;
        public IAgUiWindow obj;

        #region Object passed between different parts of the application

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        internal CustomUserInterface CustomUI
        {
            get { return m_customUserInterface; }
            set { m_customUserInterface = value; }
        }

        internal AgStkObjectRootClass STKRoot
        {
            get { return m_root; }
        }

        internal IAgProgressTrackCancel ProgressBar
        {
            get { return m_progress; }
        }

        internal string StringValue
        {
            get { return m_stringValue; }
            set { m_stringValue = value; }
        }

        internal double DoubleValue
        {
            get { return m_doubleValue; }
            set { m_doubleValue = value; }
        }

        internal string PrefPath
        {
            get { return m_prefPath; }
        }

        #endregion

        //string representing your unique command
        private const string m_commandText = "DataImporter";
        private const string m_pluginTitle = "Data Importer";
        private const string m_pluginDescription = "This UI Plugin imports data into STK";
        private const string m_pluginConfigPage = "Sample Config Page";
        private const string m_imageResource = "DataImporter.STK.ico";
        private const string m_configPath = "Sample_UIPlugin_config.txt";

        #region IAgPluginCommandTarget Implementation
        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {



            //Controls what a command does
            if (string.Compare(CommandName, m_commandText, true) == 0)
            {
                m_progress = TrackCancel;
                OpenUserInterface();
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, m_commandText, true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }
        #endregion

        #region IAgUiPlugin Implementation
        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            //Add a Configuration Page
            ConfigPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, m_pluginConfigPage);

        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            //   if (m_root.GetObjectFromPath(m_pSite.Selection[0].Path).ClassName == "Scenario")
            //   {
            MenuBuilder.AddMenuItem(m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            //   }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //Add a Toolbar Button
            ToolbarBuilder.AddButton(m_commandText, m_pluginTitle, m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);

        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            Initialize();
        }
        #endregion

        #region IAgUiPlugin2 Implementation
        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            if (MenuTitle.Contains("Scenario"))
            {
                //Insert a Menu Item
                MenuBuilder.InsertMenuItem(0, m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            }
        }
        #endregion

        public void Initialize()
        {
            if (m_pSite != null)
            {
                Image menuImage = null;

                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                menuImage = Image.FromStream(currentAssembly.GetManifestResourceStream(m_imageResource));
                m_picture = OlePictureHelper.OlePictureFromImage(menuImage);
                object AgUiApp = m_pSite.Application;
                m_root = (AgStkObjectRootClass)Marshal.CreateWrapperOfType(AgUiApp.GetType().InvokeMember("Personality2", System.Reflection.BindingFlags.GetProperty, null, AgUiApp, null), typeof(AgStkObjectRootClass));

                //read preference file
                AGI.STKUtil.IAgExecCmdResult cmdResult = m_root.ExecuteCommand("GetDirectory / DefaultUser");
                string userPath = cmdResult[0].ToString();
                m_prefPath = Path.Combine(userPath, m_configPath);
                if (File.Exists(m_prefPath))
                {
                    using (StreamReader streamReader = new StreamReader(m_prefPath))
                    {
                        m_stringValue = streamReader.ReadLine();
                        m_doubleValue = Double.Parse(streamReader.ReadLine());
                        streamReader.Close();
                    }
                }
                else
                {
                    m_stringValue = "";
                    m_doubleValue = 0.0;
                }

            }
            else
            {
                MessageBox.Show("Error: Couldn't aquire STK Root Object");
            }
        }

        

        private void OpenUserInterface()
        {
            //Open a User Interface
            IAgUiPluginWindowSite windows = m_pSite as IAgUiPluginWindowSite;
            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
                
            }
            else
            {
                IAgUiPluginWindowCreateParameters @params = windows.CreateParameters();
                @params.AllowMultiple = false;
                @params.AssemblyPath = this.GetType().Assembly.Location;
                @params.UserControlFullName = typeof(CustomUserInterface).FullName;
                @params.Caption = m_pluginTitle;
                @params.DockStyle = AgEDockStyle.eDockStyleFloating;
                @params.Width = 625;
                @params.Height = 650;
                @params.X = 10;
                @params.Y = 10;
                obj = windows.CreateNetToolWindowParam(this, @params);
                
            }
        }
    }
}
