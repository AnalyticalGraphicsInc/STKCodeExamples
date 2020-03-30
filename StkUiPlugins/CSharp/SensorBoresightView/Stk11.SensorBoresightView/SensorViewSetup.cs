using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.STKObjects;

namespace SensorBoresightViewPlugin
{
    public partial class SensorViewSetup : Form
    {
        private SensorViewClass sensorViewCreator;

        public SensorViewSetup(AgStkObjectRoot stkRoot, IAgStkObject selectedSensor)
        {
            InitializeComponent();            
            sensorViewCreator = new SensorViewClass(stkRoot, selectedSensor);
        }

        private void buttonCreateSensorView_Click(object sender, EventArgs e)
        {
            sensorViewCreator.CreateSensorWindow(Convert.ToInt16(textboxPixels.Text));

            if (cbCompass.Checked)
            {
                sensorViewCreator.EnableCompass();
            }
            if (cbLLA.Checked)
            {
                sensorViewCreator.EnableLLA();
            }
            if (cbRulers.Checked)
            {
                sensorViewCreator.EnableRulers();
            }
            if (cbCrosshairs.Checked)
            {   
                if (rbSquare.Checked)
                {
                    sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Square);
                    
                }
                else if (rbGrid.Checked)
                {
                    sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Grid);
                }
                else if (rbCircular.Checked)
                {
                    sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Circular);
                }
            }

        }

    }
}
