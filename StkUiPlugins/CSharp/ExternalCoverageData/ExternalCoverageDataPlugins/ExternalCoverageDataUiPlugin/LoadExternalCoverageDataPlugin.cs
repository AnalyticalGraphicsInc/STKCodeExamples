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
using AGI.Plugin;
using AGI.Attr;

namespace LoadExternalCoverageData
{
	[Guid("3B6F1214-C44A-43B9-A260-2B33BA6E7C25")]
    [ProgId("LoadExternalCoverageData")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class LoadExternalCoverageDataPlugin : IAgUiPlugin, IAgUiPluginCommandTarget, IAgUiPlugin2, IAgUtPluginConfig
    {
        internal sealed class IPictureDispHost : AxHost
        {
            private IPictureDispHost() : base(string.Empty) { }

            public new static object GetIPictureDispFromPicture(Image image)
            {
                return AxHost.GetIPictureDispFromPicture(image);
            }

            public new static Image GetPictureFromIPicture(object picture)
            {
                return AxHost.GetPictureFromIPicture(picture);
            }
        }

        private IAgUiPluginSite m_pSite;
        private CustomUserInterface m_customUserInterface;
        private AgStkObjectRootClass m_root;
        private IAgProgressTrackCancel m_progress;

        private bool m_integrate = true;

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        #region IAgUiPlugin Members

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            //Add a Configuration Page
            //ConfigPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, "Basic CSharp Config Page");
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            if (m_integrate)
            {
                stdole.IPictureDisp picture;
                string imageResource = "LoadExternalCoverageData.CoverageDefinitionFromFile.ico";
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
                picture = (stdole.IPictureDisp)IPictureDispHost.GetIPictureDispFromPicture(icon.ToBitmap());
                //Add a Menu Item
                MenuBuilder.AddMenuItem("AGI.BasicCSharpPlugin.MyFirstContextMenuCommand", "A CSharp Menu Item", "Open a Custom user interface.", picture);
            }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //converting an ico file to be used as the image for toolbat button
            stdole.IPictureDisp picture;
            string imageResource = "LoadExternalCoverageData.CoverageDefinitionFromFile.ico";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
            picture = (stdole.IPictureDisp)IPictureDispHost.GetIPictureDispFromPicture(icon.ToBitmap());
            //Add a Toolbar Button
            ToolbarBuilder.AddButton("AGI.BasicCSharpPlugin.MyFirstCommand", "Example CSharp Ui Plugin Toolbar Button", "Open a Custom user interface.", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture);
        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            //Get the AgStkObjectRoot
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRootClass;
        }

        #endregion

        #region IAgUiPlugin2 Members

        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            stdole.IPictureDisp picture;
            string imageResource = "LoadExternalCoverageData.STK.ico";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
            picture = (stdole.IPictureDisp)IPictureDispHost.GetIPictureDispFromPicture(icon.ToBitmap());

            if (m_integrate)
            {
                if (MenuTitle.Contains("View"))
                {
                    //Insert a Menu Item
                    MenuBuilder.InsertMenuItem(0, "AGI.BasicCSharpPlugin.MyFirstTopLevelMenuCommand", "A CSharp Top Level Menu Item", "Open a simple message box.", picture);
                }
                if (MenuBarKind == AgEUiPluginMenuBarKind.eUiPluginMenuBarContextMenu)
                {
                    //Add a Menu Item
                    MenuBuilder.AddMenuItem("AGI.BasicCSharpPlugin.MyFirstTopLevelMenuCommand", "A CSharp Context Menu Item", "Open a simple message box.", picture);
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

        #region IAgUtPluginConfig Interface Implementation

        public object GetPluginConfig(AGI.Attr.AgAttrBuilder pAttrBuilder)
        {
            object scope = pAttrBuilder.NewScope();
            pAttrBuilder.AddBoolDispatchProperty(scope, "Integrate", "Whether to integrate with the context menu", "Integrate", (int)AgEAttrAddFlags.eAddFlagHidden);
            return scope;
        }

        public void VerifyPluginConfig(AgUtPluginConfigVerifyResult pPluginCfgResult)
        {
        }

        #endregion

        internal CustomUserInterface customUI
        {
            get { return m_customUserInterface; }
            set { m_customUserInterface = value; }
        }

        public bool Integrate
        {
            get { return m_integrate; }
            set { m_integrate = value; }
        }

        internal AgStkObjectRootClass STKRoot
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
                @params.UserControlFullName = typeof(CustomUserInterface).FullName;
                @params.Caption = "Load External Coverage Data";
                @params.DockStyle = AgEDockStyle.eDockStyleDockedLeft;
                @params.Height = 650;
                @params.Width = 600;
                object obj = windows.CreateNetToolWindowParam(this, @params);
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
