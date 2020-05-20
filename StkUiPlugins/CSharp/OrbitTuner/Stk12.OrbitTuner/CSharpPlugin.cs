using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AGI.Ui.Plugins;
using AGI.Ui.Core;
using AGI.STKObjects;
using AGI.Ui.Application;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using Microsoft.Win32;

namespace OrbitTunerUiPlugin
{
    [Guid("E1F49147-5AAC-4EDC-B904-A2ABD8B29E53")]
    [ProgId("Agi.Ui.Plugins.OrbitTuner")]
    [ClassInterface(ClassInterfaceType.None)]
    public class CSharpPlugin : IAgUiPlugin, IAgUiPluginCommandTarget, IAgUiPlugin2
    {
        private IAgUiPluginSite m_pSite;
        private OrbitTunerUserPage m_customUserInterface;
        private AgStkObjectRoot m_root;
        private IAgProgressTrackCancel m_progress;

        private bool m_integrate = true;
        //private static final String = "OrbitTunerSat";

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        #region IAgUiPlugin Members

        
        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            //Add a Configuration Page
            
        }
        
        

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            if (m_integrate)
            {
                stdole.IPictureDisp picture;
                string imageResource = "OrbitTunerUiPlugin.STK.ico";
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
                picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(icon);
                //Add a Menu Item
                MenuBuilder.AddMenuItem("AGI.BasicCSharpPlugin.MyFirstContextMenuCommand", "Orbit Tuner", "Open the Orbit Tuner custom user interface.", picture);
                
            }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //converting an ico file to be used as the image for toolbat button
            stdole.IPictureDisp picture;
            string imageResource = "OrbitTunerUiPlugin.STK.ico";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
            picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(icon);
            //Add a Toolbar Button
            ToolbarBuilder.AddButton("AGI.BasicCSharpPlugin.MyFirstCommand", "Orbit Tuner", "Open the Orbit Tune custom user interface.", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture);
        }

        public void OnShutdown()
        {
            m_pSite = null;
            m_root.OnStkObjectAdded -= new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(UpdateListAdded);
            m_root.OnStkObjectChanged -= new IAgStkObjectRootEvents_OnStkObjectChangedEventHandler(UpdateListChanged);
            m_root.OnStkObjectRenamed -= new IAgStkObjectRootEvents_OnStkObjectRenamedEventHandler(UpdateListRenamed);
            m_root.OnStkObjectDeleted -= new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(UpdateListDeleted);
            m_root.OnScenarioBeforeClose -= new IAgStkObjectRootEvents_OnScenarioBeforeCloseEventHandler(ScenarioCloseEvent);
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            //Get the AgStkObjectRoot
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRoot;
            m_root.Isolate();
            m_root.UnitPreferences.ResetUnits();
            m_root.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(UpdateListAdded);
            m_root.OnStkObjectChanged += new IAgStkObjectRootEvents_OnStkObjectChangedEventHandler(UpdateListChanged);
            m_root.OnStkObjectRenamed += new IAgStkObjectRootEvents_OnStkObjectRenamedEventHandler(UpdateListRenamed);
            m_root.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(UpdateListDeleted);
            m_root.OnScenarioBeforeClose += new IAgStkObjectRootEvents_OnScenarioBeforeCloseEventHandler(ScenarioCloseEvent);
           
            STKHelper.StkRoot = m_root;
        }

        public void ScenarioCloseEvent()
        {
            m_root.OnStkObjectDeleted -= new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(UpdateListDeleted);
        }

        
        public void UpdateListRenamed(object sender, string oldPath, string newPath)
        {
            string[] oldPathArray = oldPath.Split('/');
            string[] newPathArray = newPath.Split('/');

            string newName = newPathArray[6];
            string oldName = oldPathArray[6];
            m_customUserInterface.ChangeNameSatComboBox(oldName, newName); // 
        }

        public void UpdateListDeleted(object sender)
        {
            if (m_customUserInterface != null && m_pSite != null)
            {
                m_customUserInterface.UpdateSatAfterDelete();
            }
        }

        public void UpdateListAdded(object sender)
        {
            
            string[] path = sender.ToString().Split('/'); ;
            m_customUserInterface.UpdateSatAfterAdd(path[6]);
            
        }

        public void UpdateListChanged(IAgStkObjectChangedEventArgs sender)
        {
            if (m_customUserInterface == null)
            {
                return;
            }

            var path = sender.Path;
            UpdateSatList(path.ToString());
        }

        private void UpdateSatList(string path)
        {
            string[] words = path.Split('/');
            string className = words[5];

            if (className == "Satellite")
            {
                m_customUserInterface.UpdateSelectSatComboBox();                
            }         
        }
       

        #endregion

        #region IAgUiPlugin2 Members

        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            stdole.IPictureDisp picture;
            string imageResource = "OrbitTunerUiPlugin.STK.ico";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
            picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(icon);

            
            if (m_integrate)
            {
                if (MenuTitle.Contains("View"))
                {
                    //Insert a Menu Item
                    //MenuBuilder.InsertMenuItem(0, "AGI.BasicCSharpPlugin.MyFirstTopLevelMenuCommand", "A CSharp Top Level Menu Item", "Open a simple message box.", picture);
                }
                if (MenuBarKind == AgEUiPluginMenuBarKind.eUiPluginMenuBarContextMenu)
                {
                    //Add a Menu Item
                    //MenuBuilder.AddMenuItem("AGI.BasicCSharpPlugin.MyFirstTopLevelMenuCommand", "A CSharp Context Menu Item", "Open a simple message box.", picture);
                }
            }
            
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstCommand", true) == 0 || 
                string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstContextMenuCommand", true) == 0) 
            {
                m_progress = TrackCancel;
                OpenUserInterface();
            }
            if (string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstTopLevelMenuCommand", true) == 0)
            {
                MessageBox.Show("A simple message box.", "Message Box");
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstCommand", true) == 0 || 
                string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstContextMenuCommand", true) == 0 ||
                string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstTopLevelMenuCommand", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        #endregion

        internal OrbitTunerUserPage customUI
        {
            get { return m_customUserInterface; }
            set { m_customUserInterface = value; }
        }

        internal bool Integrate
        {
            get { return m_integrate; }
            set { m_integrate = value; }
        }

        internal AgStkObjectRoot STKRoot
        {
            get { return m_root; }
        }

        internal IAgProgressTrackCancel ProgressBar
        {
            get { return m_progress; }
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
                @params.UserControlFullName = typeof(OrbitTunerUserPage).FullName;
                @params.Caption = "Orbit Tuner";
                @params.Width = 400;
               
                @params.DockStyle = AgEDockStyle.eDockStyleDockedLeft;
                
                IAgUiWindow obj = windows.CreateNetToolWindowParam(this, @params);
               
            }
        }

        #region Registration functions
        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        [ComRegisterFunction]
        [ComVisible(false)]
        public static void RegisterFunction(Type t)
        {
            RemoveOtherVersions(t);
        }

        /// <summary>
        /// Called when the assembly is unregistered for use from COM.
        /// </summary>
        /// <param name="t">The type exposed to COM.</param>
        [ComUnregisterFunctionAttribute]
        [ComVisible(false)]

        
        public static void UnregisterFunction(Type t)
        {
            // Do nothing.
        }
        

        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// Eliminates the other versions present in the registry for
        /// this type.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        public static void RemoveOtherVersions(Type t)
        {
            try
            {
                using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                {
                    StringBuilder guidString = new StringBuilder("{");
                    guidString.Append(t.GUID.ToString());
                    guidString.Append("}");
                    using (RegistryKey guidKey = clsidKey.OpenSubKey(guidString.ToString()))
                    {
                        if (guidKey != null)
                        {
                            using (RegistryKey inproc32Key = guidKey.OpenSubKey("InprocServer32", true))
                            {
                                if (inproc32Key != null)
                                {
                                    string currentVersion = t.Assembly.GetName().Version.ToString();
                                    string[] subKeyNames = inproc32Key.GetSubKeyNames();
                                    if (subKeyNames.Length > 1)
                                    {
                                        foreach (string subKeyName in subKeyNames)
                                        {
                                            if (subKeyName != currentVersion)
                                            {
                                                inproc32Key.DeleteSubKey(subKeyName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore all exceptions...
            }
        }
        #endregion
    }
}
