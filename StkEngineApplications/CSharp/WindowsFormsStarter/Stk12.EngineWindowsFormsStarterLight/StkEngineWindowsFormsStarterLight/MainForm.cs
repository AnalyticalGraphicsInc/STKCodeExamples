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

namespace StkEngineWindowsFormsStarterLight
{
    public partial class MainForm : Form
    {
        AgStkObjectRoot m_stkRoot;
        public MainForm()
        {
            InitializeComponent();
            m_stkRoot = new AgStkObjectRoot();
        }

        private void CreateNewScenario()
        {
            if (!m_stkRoot.HasChildren)
            {
                m_stkRoot.NewScenario("Demo");
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
    }
}
