using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Windows.Forms;
using AGI.Ui.Core;
using System.Drawing;
using System.Reflection;
using System.IO;
using AGI.Ui.Application;


//************************************************************
//Modify "Platform" to x86 under Project Properties/Build
//Modify Plugin Title and Description 
//Rename xml file and modify "DisplayName" inside xml file
//************************************************************

namespace Stk10.UiPlugin.FomGfxUpdater
{

    [Guid("292839ae-b987-4fdb-af0c-2a7b44d3d6ff")]
    [ProgId("Stk10.UiPlugin.FomGfxUpdater")]
    [ClassInterface(ClassInterfaceType.None)]
    public class StkPluginPoint : IAgUiPlugin, IAgUiPlugin2, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_pSite;
        private AgStkObjectRootClass m_root;


        #region Object passed between different parts of the application

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        internal AgStkObjectRootClass STKRoot
        {
            get { return m_root; }
        }

        #endregion

        //string representing your unique command
        private const string m_commandText = "Stk10.UiPlugin.FomGfxUpdater";

        #region IAgPluginCommandTarget Implementation
        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, "UpdateStaticContours", true) == 0)
            {
                CoverageClass.AutoContour(m_pSite.Selection[0].Path, "Static");
            }
            if (string.Compare(CommandName, "UpdateAnimationContours", true) == 0)
            {
                CoverageClass.AutoContour(m_pSite.Selection[0].Path, "Animation");
            }
            if (string.Compare(CommandName, "ReportGridStats", true) == 0)
            {
                CoverageClass.GenerateGridStatsReport(m_pSite.Selection[0].Path);
            }

            
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, "UpdateStaticContours", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            //Enable commands
            if (string.Compare(CommandName, "UpdateAnimationContours", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            //Enable commands
            if (string.Compare(CommandName, "ReportGridStats", true) == 0)
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
            //Get the AgStkObjectRoot
            m_pSite = PluginSite;
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRootClass;
            m_root.Isolate();
            m_root.UnitPreferences.ResetUnits();

            CoverageClass.root = m_root;
        }
        #endregion

        #region IAgUiPlugin2 Implementation
        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            if (MenuTitle.Contains("FigureOfMerit"))
            {
                //Insert a Menu Item
                MenuBuilder.InsertMenuItem(1, "ReportGridStats", "Generate Grid Stats Report", "Generate Grid Stats Report", null);
                MenuBuilder.InsertMenuItem(1, "UpdateStaticContours", "Auto Update Static Contours", "FOM Graphics Auto Update", null);
                MenuBuilder.InsertMenuItem(1, "UpdateAnimationContours", "Auto Update Animation Contours", "FOM Graphics Auto Update", null);
            }

            if (MenuBarKind == AgEUiPluginMenuBarKind.eUiPluginMenuBarContextMenu)
            {
                //the STK Object Root
                AgStkObjectRoot root = (AgStkObjectRoot)m_pSite.Application.Personality2;
                //the current Selected Object
                IAgStkObject oSelectedObject = root.GetObjectFromPath(m_pSite.Selection[0].Path);

                //Only show menu items for the Scenario or Satellite object
                if (oSelectedObject.ClassName.Equals("FigureOfMerit"))
                {
                    MenuBuilder.InsertMenuItem(5, "ReportGridStats", "Report Grid Stats", "Report Grid Stats", null);
                    MenuBuilder.InsertMenuItem(5, "UpdateStaticContours", "Auto Update Static Contours", "FOM Graphics Auto Update", null);
                    MenuBuilder.InsertMenuItem(5, "UpdateAnimationContours", "Auto Update Animation Contours", "FOM Graphics Auto Update", null);
                }
            }
                
        }
        #endregion


    }
}
