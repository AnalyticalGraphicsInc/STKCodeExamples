using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.Ui.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


//************************************************************
//Modify Plugin Title and Description 
//Rename xml file and modify "DisplayName" inside xml file
//************************************************************

namespace Stk12.UiPlugin.ModelPixelUpdater
{

    [Guid("6BF364F0-9994-4986-BF26-414839E06AF0")]
    [ProgId("Stk12.UiPlugin.ModelPixelUpdater")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Setup : IAgUiPlugin, IAgUiPlugin2, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_pSite;

        private IAgStkObjectRoot m_root;
        private ModelUpdateClass m_updater;

        #region Object passed between different parts of the application

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        internal IAgStkObjectRoot STKRoot
        {
            get { return m_root; }
        }

        #endregion

        //string representing your unique command
        private const string m_commandText = "Stk12.UiPlugin.ModelPixelUpdater";

        #region IAgPluginCommandTarget Implementation
        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, "Enable Model Size Update", true) == 0)
            {
                if (m_updater == null)
                {
                    ScaleFactorForm setScale = new ScaleFactorForm();
                    setScale.ShowDialog();
                    if (setScale.accepted)
                    {
                        m_updater = new ModelUpdateClass(m_root)
                        {
                            scaleFactor = setScale.scaleFactor
                        };
                        m_updater.updateModelSize(((IAgAnimation)m_root).CurrentTime);
                    }
                }
                else
                {
                    m_updater.DisableModelUpdate();
                    m_updater = null;
                }
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            if (string.Compare(CommandName, "Enable Model Size Update", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }
        #endregion

        #region IAgUiPlugin Implementation
        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {

        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {

        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {

        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as IAgStkObjectRoot;
            m_root.Isolate();
            m_root.UnitPreferences.ResetUnits();
            ((AgStkObjectRoot)m_root).OnScenarioClose += new IAgStkObjectRootEvents_OnScenarioCloseEventHandler(m_root_OnScenarioClose);
        }

        void m_root_OnScenarioClose()
        {
            m_updater = null;
        }
        #endregion

        #region IAgUiPlugin2 Implementation
        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            string enabled = "Enable";
            //Insert a Menu Item
            if (m_updater != null)
            {
                enabled = "Disable";
            }
            if (MenuTitle.Contains("Scenario"))
            {
                MenuBuilder.InsertMenuItem(1, "Enable Model Size Update", enabled + " Model Size Update", enabled + " Model Size Update", null);
            }

            if (MenuBarKind == AgEUiPluginMenuBarKind.eUiPluginMenuBarContextMenu)
            {
                //the STK Object Root
                AgStkObjectRoot root = (AgStkObjectRoot)m_pSite.Application.Personality2;
                //the current Selected Object
                IAgStkObject oSelectedObject = root.GetObjectFromPath(m_pSite.Selection[0].Path);

                //Only show menu items for the Scenario or Satellite object
                if (oSelectedObject.ClassName.Equals("Scenario"))
                {
                    MenuBuilder.InsertMenuItem(5, "Enable Model Size Update", enabled + " Model Size Update", enabled + " Model Size Update", null);
                }
            }
        }
        #endregion
    }
}
