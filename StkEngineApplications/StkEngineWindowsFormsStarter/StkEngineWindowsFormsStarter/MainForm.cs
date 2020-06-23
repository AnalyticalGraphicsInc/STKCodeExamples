using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AGI.STKObjects;
using AGI;
using AxAGI.STKX;

namespace StkEngineWindowsFormsStarter
{
    public partial class MainForm : Form
    {
        
        AGI.OverlayToolbar m_overlay;

        private int m_lastMouseClickX, m_lastMouseClickY;

        public MainForm()
        {
            InitializeComponent();
            CommonData.StkRoot = new AgStkObjectRoot();

            
        }

        private void CreateNewScenario()
        {
            if (!CommonData.StkRoot.HasChildren)
            {
                CommonData.StkRoot.NewScenario("Demo");
                m_overlay = new AGI.OverlayToolbar(CommonData.StkRoot, this.axAgUiAxVOCntrl1);

            }
            else
            {
                MessageBox.Show("Scenario already created.");
            }
        }

        private void btnNewScenario_Click(object sender, EventArgs e)
        {
            CreateNewScenario();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void axAgUiAxVOCntrl1_MouseDownEvent(object sender, IAgUiAxVOCntrlEvents_MouseDownEvent e)
        {
            if (m_overlay != null)
                m_overlay.Control3D_MouseDown(sender, e);
        }

        private void axAgUiAxVOCntrl1_MouseUpEvent(object sender, IAgUiAxVOCntrlEvents_MouseUpEvent e)
        {
            // Store the mouse click position
            m_lastMouseClickX = e.x;
            m_lastMouseClickY = e.y;

            if (m_overlay != null)
                m_overlay.Control3D_MouseUp(sender, e);
        }


    }
}
