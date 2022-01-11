using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKX;

namespace WPFEngineApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AgSTKXApplication m_application;
        AgStkObjectRoot m_stkRoot;
        public MainWindow()
        {
            InitializeComponent();
           // m_application = new AgSTKXApplication();
            m_stkRoot = new AgStkObjectRoot();
            m_stkRoot.NewScenario("test");
        }
        #region Button clicks
        private void btnStop_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void btnPlay_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            m_stkRoot.PlayForward();
        }

        private void btnPause_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            m_stkRoot.Pause();
        }

        private void btnSlowDown_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            m_stkRoot.Slower();
        }

        private void btnFaster_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            m_stkRoot.Faster();
        }

        private void btnZoom_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AGIMapControl.Map.ZoomIn();
        }

        private void btnHome_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void btnPan_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
        #endregion

        private void btnZoom1_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AGIGlobeControl.Globe.ZoomIn();
        }

        private void btnHome1_Clicked(object sender, System.Windows.Input.MouseEventArgs e)
        {
            m_stkRoot.ExecuteCommand("VO * View Home");
        }
    }
}
