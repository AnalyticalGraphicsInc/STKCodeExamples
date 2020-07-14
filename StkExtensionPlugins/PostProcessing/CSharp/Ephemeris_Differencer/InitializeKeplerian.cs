using System;
using System.Text;
using System.Windows.Forms;
using AGI.Foundation.Coordinates;
using AGI.Foundation.Time;
using AGI.Foundation;
using AGI.Foundation.Propagators;
using AGI.Foundation.Celestial;

namespace EphemerisDifferencer
{
    public partial class InitializeKeplerian : Form
    {
        public InitializeKeplerian(GraphicalUserInterface gui, int index)
        {
            m_gui = gui;
            m_index = index;
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            this.XLabel.Text = "SemiMajorAxis [" + m_gui.Units + "]";
            this.SemiMajorAxisBox.Text = "" + 7000.0e3 * m_gui.UnitConversion;
            this.GravityTextBox.Text = "" + 398600.4415e9 * Math.Pow(m_gui.UnitConversion, 3);
            this.label6.Text = "[" + m_gui.Units + "^3 / sec^2]";
        }

        public JulianDate Epoch
        {
            get { return m_epoch; }
        }

        public KeplerianElements Elements
        {
            get { return m_elements; }
        }

        private GraphicalUserInterface m_gui;
        private int m_index;
        private JulianDate m_epoch;
        private KeplerianElements m_elements;

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            try
            {
                double jdate = Double.Parse(EpochTextBox.Text);
                double axis = Double.Parse(SemiMajorAxisBox.Text);
                double ecc = Double.Parse(EccentricityBox.Text);
                double i = Double.Parse(InclinationBox.Text);
                double RAAN = Double.Parse(RAANBox.Text);
                double w = Double.Parse(ArgOfPeriapsisBox.Text);
                double v = Double.Parse(AnomalyBox.Text);
                double mu = Double.Parse(GravityTextBox.Text);
                
                m_epoch = new JulianDate(jdate, TimeStandard.CoordinatedUniversalTime);
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Epoch:          " + Epoch);
                builder.AppendLine("Gravity:        " + mu);
                builder.AppendLine("Propagator:     " + "TwoBody");
                builder.AppendLine("SemiMajorAxis:  " + axis);
                builder.AppendLine("Eccentricity:   " + ecc);
                builder.AppendLine("Inclination:    " + i);
                builder.AppendLine("RightAscension: " + RAAN);
                builder.AppendLine("ArgPeriapsis:   " + w);
                builder.AppendLine("TrueAnomaly:    " + v);

                axis = axis / m_gui.UnitConversion;
                mu = mu / Math.Pow(m_gui.UnitConversion, 3);

                // convert to radians
                m_elements = new KeplerianElements(axis, ecc, Trig.DegreesToRadians(i), Trig.DegreesToRadians(w),
                    Trig.DegreesToRadians(RAAN), Trig.DegreesToRadians(v), mu);
                TwoBodyPropagator prop = new TwoBodyPropagator(Epoch, CentralBodiesFacet.GetFromContext().Earth.InertialFrame, m_elements);
                
                if (m_index == 1)
                {
                    m_gui.PointOne = prop.CreatePoint();
                    m_gui.InputOne.Clear();
                    m_gui.InputOne.Text = builder.ToString();
                }
                else
                {
                    m_gui.PointTwo = prop.CreatePoint();
                    m_gui.InputTwo.Clear();
                    m_gui.InputTwo.Text = builder.ToString();
                }
                this.Close();
            }
            catch (Exception)
            {
                System.Console.WriteLine("Error parsing Keplerian input");
            }
        }
    }
}