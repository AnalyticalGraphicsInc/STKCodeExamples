using FlowerConstellationBuilder.Routines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerConstellationBuilder
{
    public partial class frmMain : Form
    {
        //const double omegaEarth = 0.000072921159; // rad/s
        const double omegaEarth = 0.000072921150; // rad/s
        const double mu = 3.986004418e5; // km^3/s^2
        const double earthRadius = 6378.1363; // km
        const double J2 = 0.0010826267;

        double nSatellites;
        double nDays;
        double nPetals;
        double fn, fd, fh;
        double initialRaan; // deg
        double initialMeanAnomaly; // deg
        double orbitPeriod; // sec
        double perigeeAlt; // km
        double perigeeRadius, apogeeRadius; // km
        double eccentricity;

        double currentRaan; // deg
        double currentRaanForStk; // deg
        double currentMeanAnomaly; // deg
        double currentMeanAnomalyForStk; // deg 
        double sma; // km
        double inclination; // deg
        double argOfPerigee; // deg

        string constName;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                STK.CheckStkActiveXInstance();
            }
            catch (Exception)
            {
                MessageBox.Show("STK scenario not available. Please create a new one and try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }

            CalculateDerivedParameters();
        }

        #region GUI
        private void btnColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pnlCurrentColor.BackColor = this.colorDialog1.Color;
            }
        }

        private void nudFd_ValueChanged(object sender, EventArgs e)
        { // we assume Fn <= Fd
            if (this.nudFd.Value < this.nudFn.Value)
            {
                this.nudFn.Value = this.nudFd.Value;
            }
            this.nudFn.Maximum = this.nudFd.Value;
            // limitation on max number of satellites
            this.nudSatellites.Maximum = this.nudDays.Value * this.nudFd.Value;
        }

        private void nudSatellites_ValueChanged(object sender, EventArgs e)
        { // we assume 0 <= Fh < nSat
            this.nudFh.Maximum = this.nudSatellites.Value - 1;
            if (this.nudSatellites.Value <= this.nudFh.Maximum)
            {
                this.nudFh.Value = this.nudSatellites.Value - 1;
            }

            // limitation on max number of satellites
            this.nudSatellites.Maximum = this.nudDays.Value * this.nudFd.Value;
        }

        private void nudDays_ValueChanged(object sender, EventArgs e)
        { // limitation on max number of satellites
            this.nudSatellites.Maximum = this.nudDays.Value * this.nudFd.Value;

            CalculateDerivedParameters();
        }

        private void nudInitialRAAN_ValueChanged(object sender, EventArgs e)
        {
            CalculateDerivedParameters();
        }

        private void nudPetals_ValueChanged(object sender, EventArgs e)
        {
            CalculateDerivedParameters();
        }

        private void nudPerigeeAlt_ValueChanged(object sender, EventArgs e)
        {
            CalculateDerivedParameters();
        }

        private void nudInclination_ValueChanged(object sender, EventArgs e)
        {
            CalculateDerivedParameters();
        }

        private void nudArgOfPerigee_ValueChanged(object sender, EventArgs e)
        {
            CalculateDerivedParameters();
        }

        private void nudFn_ValueChanged(object sender, EventArgs e)
        {
            CalculateDerivedParameters();
        }

        private void nudFh_ValueChanged(object sender, EventArgs e)
        {
            CalculateDerivedParameters();
        } 
        #endregion

        private void loneStarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change the input data to match Lone Star configuration
            this.tbConstellationName.Text = "LoneStar";
            this.nudFd.Value = 77;
            this.nudSatellites.Value = 77;
            this.nudPetals.Value = 38;
            this.nudDays.Value = 23;
            this.nudFh.Value = 1;
            this.nudFn.Value = 23;
            this.nudPerigeeAlt.Value = 1300;
            this.nudArgOfPerigee.Value = 270;
            this.nudInclination.Value = 0;
            this.nudInitialRAAN.Value = 0;
            this.nudInitialMeanAnomaly.Value = 0;

            CalculateDerivedParameters();
        }

        private void secondaryClosedPathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change the input data to match a sample configuration
            this.tbConstellationName.Text = "SCP";
            this.nudFd.Value = 90;
            this.nudSatellites.Value = 90;
            this.nudPetals.Value = 8;
            this.nudDays.Value = 1;
            this.nudFh.Value = 1;
            this.nudFn.Value = 1;
            this.nudPerigeeAlt.Value = 3000;
            this.nudArgOfPerigee.Value = 270;
            this.nudInclination.Value = 165;
            this.nudInitialRAAN.Value = 0;
            this.nudInitialMeanAnomaly.Value = 0;

            CalculateDerivedParameters();
        }

        private void molnyiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change the input data to match a sample configuration
            this.tbConstellationName.Text = "Molnyia";
            this.nudFd.Value = 4;
            this.nudSatellites.Value = 4;
            this.nudPetals.Value = 2;
            this.nudDays.Value = 1;
            this.nudFh.Value = 1;
            this.nudFn.Value = 1;
            this.nudPerigeeAlt.Value = 500;
            this.nudArgOfPerigee.Value = 270;
            this.nudInclination.Value = Convert.ToDecimal(63.4);
            this.nudInitialRAAN.Value = 135;
            this.nudInitialMeanAnomaly.Value = 0;

            CalculateDerivedParameters();
        }

        private void btnCreateConstellation_Click(object sender, EventArgs e)
        {
            nSatellites = Convert.ToInt32(this.nudSatellites.Value);

            // check for eccentricity
            if (apogeeRadius < perigeeRadius)
            {
                MessageBox.Show("Apogee should be greater than or equal to Perigee", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < nSatellites; i++)
            {
                string currentSatName = constName + "_" + (i + 1);
                this.lblStatus.Text = "Creating " + currentSatName;
                this.statusStrip1.Update();

                if (i == 0) // first satellite only
                {
                    currentRaan = initialRaan;
                    currentRaanForStk = currentRaan;
                    currentMeanAnomaly = initialMeanAnomaly;
                    currentMeanAnomalyForStk = currentMeanAnomaly;
                }
                else
                {
                    currentRaan = currentRaan - 360 * (fn/ fd);
                    currentRaanForStk = CalculateRaanForStk(currentRaan);
                    // currentMeanAnomaly = 360 * i * ((fn * nPetals + fd * fh) / (fd * nDays));
                    currentMeanAnomaly = currentMeanAnomaly + (360 * (fn / fd)) / (nDays / nPetals); // eq 1.54
                    currentMeanAnomalyForStk = CalculateMeanAnomalyForStk(currentMeanAnomaly);
                }



                STK.CreateSatellite(currentSatName, orbitPeriod, eccentricity, inclination, currentRaanForStk, currentMeanAnomalyForStk, argOfPerigee);
                STK.SetColorSingle(currentSatName, this.pnlCurrentColor.BackColor);

                bool showInertial = this.cbInertialFrame.Checked;
                bool showFixed = this.cbFixedFrame.Checked;
                STK.SetOrbitGraphics(currentSatName, showInertial, showFixed);

            }

            this.lblStatus.Text = "";
            this.statusStrip1.Update();

            if (this.cbConstellationObj.Checked)
            {
                STK.CreateConstellation(constName, Convert.ToInt32(nSatellites));
            }
        }

        private double CalculateRaanForStk(double currentRaan)
        {
            double stkRaan = currentRaan;
            if (currentRaan < 0)
            {   
                do
                { // bring the RAAN vaule in the 0 -> 360 deg domain
                    stkRaan += 360;
                } while (stkRaan < 0);

            }

            return stkRaan;
        }

        private double CalculateMeanAnomalyForStk(double currentMeanAnomaly)
        {
            double stkMeanAnomaly = currentMeanAnomaly;
            if (currentMeanAnomaly > 360)
            {
                do
                { // bring the RAAN vaule in the 0 -> 360 deg domain
                    stkMeanAnomaly -= 360;
                } while (stkMeanAnomaly > 360);

            }

            return stkMeanAnomaly;
        }

        private void CalculateDerivedParameters()
        {
            // get info from the GUI

            nDays = Convert.ToDouble(this.nudDays.Value);
            nPetals = Convert.ToDouble(this.nudPetals.Value);
            fn = Convert.ToDouble(this.nudFn.Value);
            fd = Convert.ToDouble(this.nudFd.Value);
            fh = Convert.ToDouble(this.nudFh.Value);
            perigeeAlt = Convert.ToDouble(this.nudPerigeeAlt.Value);
            inclination = Convert.ToDouble(this.nudInclination.Value);
            constName = this.tbConstellationName.Text;
            initialRaan = Convert.ToDouble(this.nudInitialRAAN.Value);
            initialMeanAnomaly = Convert.ToDouble(this.nudInitialMeanAnomaly.Value);
            argOfPerigee = Convert.ToDouble(this.nudArgOfPerigee.Value);


            //////////////////////////// calculate nodal period ////////////////////////////////////////////

            // Algorithm 70 from Vallado, third edition
            // Krev2rep = nPetals, Kday2Rep = nDays
            // n = meanMotion

            perigeeRadius = perigeeAlt + earthRadius; // perigee readius is an input parameter
            double revPerDay = nPetals / nDays;
            double n = revPerDay * omegaEarth;

            double aNew = Math.Pow((mu * Math.Pow((1 / n), 2)), (1.0 / 3.0));
            double apogeeRadiusNew = 2.0 * aNew - perigeeRadius;
            double eNew = 1.0 - (2.0 / ((apogeeRadiusNew / perigeeRadius) + 1.0));
            eNew = (aNew - perigeeRadius) / aNew;
            double meanAnomalyDot = 0.0;
            double omegaDot = 0.0;




            //double kepPeriod = 2 * Math.PI * Math.Sqrt(Math.Pow(aNew, 3.0) / mu);
            ////double period2 = (2 * Math.PI * nDays) / (nPetals * omegaEarth);
            //double sma = Math.Pow(mu * Math.Pow(kepPeriod / (2 * Math.PI), 2.0), (1.0 / 3.0));

            //double test = FlightDynamics.SMajAxToAnomPeriod(sma, eNew, inclination, ErrorType.NoError);







            for (int i = 0; i < 9; i++)
            {   // just to be sure we do 10 iterations. The for cycle can be changed to a while one
                double a = aNew;
                double e = eNew;
                double p = a * (1.0 - Math.Pow(e, 2.0));
                double raanDot = -(3.0 / 2.0) * n * J2 * Math.Pow((earthRadius / p), 2.0) * Math.Cos(inclination * Math.PI / 180.0);
                omegaDot = (3.0 / 4.0) * n * J2 * Math.Pow((earthRadius / p), 2.0) * (4.0 - 5.0 * Math.Pow(Math.Sin(inclination * Math.PI / 180), 2.2));
                meanAnomalyDot = (3.0 / 4.0) * n * J2 * Math.Pow((earthRadius / p), 2.0) * Math.Pow(1.0 - Math.Pow(e, 2.0), (1.0 / 2.0)) * (2.0 - 3.0 * Math.Pow(Math.Sin(inclination * Math.PI / 180.0), 2.0));
                n = revPerDay * (omegaEarth - raanDot) - (meanAnomalyDot + omegaDot);
                aNew = Math.Pow((mu * Math.Pow((1.0 / n), 2.0)), (1.0 / 3.0));
                eNew = (aNew - perigeeRadius) / aNew;
            }

            //double testPeriod = (2 * Math.PI * nDays) / (nPetals * omegaEarth);
            //double nodalPeriod = keplerianPeriod * (1.0 + (3.0 / 2.0) * J2 * Math.Pow((earthRadius / aNew), 2.0) * (3.0 - 4.0 * Math.Pow(Math.Sin(inclination * Math.PI / 180.0), 2.0)));

            double keplerianPeriod = 2 * Math.PI * Math.Sqrt(Math.Pow(aNew, 3.0) / mu);
            double nodalPeriod = keplerianPeriod * (1.0 / (1.0 + (meanAnomalyDot + omegaDot) / n));


            //////////////////////////// end of nodal period calculation ////////////////////////////////////////////

            orbitPeriod = nodalPeriod;
            sma = aNew;
            eccentricity = eNew;
            apogeeRadius = 2 * sma - perigeeRadius;




            // output data in the GUI
            this.tbSma.Text = Math.Round(sma, 1).ToString();
            this.tbApogeeHeight.Text = Math.Round((apogeeRadius - earthRadius), 1).ToString();
            this.tbEcc.Text = Math.Round(eccentricity, 6).ToString(); ;


        }
    }
}
