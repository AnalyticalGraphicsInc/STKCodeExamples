using System;
using System.Windows.Forms;
using System.Threading;
using AGI.STKObjects;

namespace RealtimeACcontrol
{
    public partial class ControlForm : Form
    {
        private Thread _timestepLoop;

        public ControlForm()
        {
            InitializeComponent();
            AircraftController.Initialize();
            scroll_Throttle.Value = 100;
        }

        private void btn_PitchUp_Click(object sender, EventArgs e)
        {
            AircraftController.PitchUp();
        }

        private void btn_PitchDown_Click(object sender, EventArgs e)
        {
            AircraftController.PitchDown();
        }

        private void btn_turnRight_Click(object sender, EventArgs e)
        {
            AircraftController.TurnRight();
        }

        private void btn_leftTurn_Click(object sender, EventArgs e)
        {
            AircraftController.TurnLeft();
        }
        
        private void scroll_Throttle_Scroll(object sender, ScrollEventArgs e)
        {
            AircraftController.Vel = (double)(100- scroll_Throttle.Value) / 100 * AircraftController.MaxVel;
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            AircraftController.PauseAnimation();
            _timestepLoop.Abort();
        }
        
        private void btn_Start_Click(object sender, EventArgs e)
        {
            _timestepLoop = new Thread(new ThreadStart(AircraftController.ExecuteStepInTime))
            {
                IsBackground = true
            };

            _timestepLoop.Start();
        }
    }
}
