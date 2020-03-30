using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using AGI.Ui.Application;

namespace SensorBoresightViewPlugin
{
    [Guid("E27581C9-C741-46C5-A8C4-09DEA4D0CBA6")]
    [ProgId("STK11.AGI.SensorBoresightViewPlugin")]
    [ClassInterface(ClassInterfaceType.None)]

    public class SensorBoresightViewPlugin:IAgUiPlugin,IAgUiPluginCommandTarget,IAgUiPlugin2
    {
        #region declarations
        private IAgUiPluginSite m_pSite;
        private AgStkObjectRoot m_root;
        private IAgStkObject oSelectedObject;

        internal IAgUiPluginSite Site { get { return m_pSite; } }
        public AgStkObjectRoot STKRoot { get { return m_root; } }
        
        #endregion

        #region IAgUiPlugin Members
        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {

        }
        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind,
           IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            if (MenuBarKind == AgEUiPluginMenuBarKind.eUiPluginMenuBarContextMenu)
            {
                //the STK Object Root
                AgStkObjectRoot root = (AgStkObjectRoot)m_pSite.Application.Personality2;
                //the current Selected Object
                oSelectedObject = root.GetObjectFromPath(m_pSite.Selection[0].Path);

                //Only show menu items for the Scenario or Satellite object
                if (oSelectedObject.ClassName.Equals("Sensor"))
                {
                     stdole.IPictureDisp boresightPic = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture(
                        Properties.Resources.boresight);
                
                    MenuBuilder.InsertMenuItem(3, "AGI.SensorBoresightViewPlugin.SensorBoresightCommand",
                        "Create Sensor View 3D Window",
                        "Create a new 3D Window aligned with the sensor boresight", boresightPic);
                    
                }
            }
        }
        


        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRoot;
            m_root.Isolate();
            m_root.UnitPreferences.ResetUnits();
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        void IAgUiPluginCommandTarget.Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            if (string.Compare(CommandName, "AGI.SensorBoresightViewPlugin.SensorBoresightCommand", true) == 0)
            {
                OpenUserControl();
            }
        }

        AgEUiPluginCommandState IAgUiPluginCommandTarget.QueryState(string CommandName)
        {
            if (string.Compare(CommandName, "AGI.SensorBoresightViewPlugin.SensorBoresightCommand", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }
        #endregion

        #region Misc Functions

       public void OpenUserControl()
        {
            AGI.Ui.Plugins.IAgUiPluginWindowSite windows = m_pSite as AGI.Ui.Plugins.IAgUiPluginWindowSite;
            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IntPtr hMainWnd = (IntPtr)Site.MainWindow;
                SensorViewSetup gvFromDir = new SensorViewSetup(STKRoot, oSelectedObject);
                NativeWindow nativeWindow = new NativeWindow();
                nativeWindow.AssignHandle(hMainWnd);
                gvFromDir.Show(nativeWindow);

            }
        }
       
        #endregion

    }   

}
