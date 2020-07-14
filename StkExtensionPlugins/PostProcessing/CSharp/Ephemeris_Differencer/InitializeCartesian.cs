using System;
using System.Text;
using System.Windows.Forms;
using AGI.Foundation.Coordinates;
using AGI.Foundation.Time;
using AGI.Foundation.Propagators;
using AGI.Foundation.Celestial;

namespace EphemerisDifferencer
{
    public partial class InitializeCartesian : Form
    {
        public InitializeCartesian(GraphicalUserInterface gui, int index)
        {
            m_gui = gui;
            m_index = index;
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            this.ZdotLabel.Text = "Z [" + m_gui.Units + "/Sec]";
            this.YdotLabel.Text = "Y [" + m_gui.Units + "/Sec]";
            this.XdotLabel.Text = "X [" + m_gui.Units + "/Sec]";
            this.ZdotTextBox.Text = "" + (-1.0e3 * m_gui.UnitConversion);
            this.YdotTextBox.Text = "" + 8.3e3 * m_gui.UnitConversion;
            this.XdotTextBox.Text = "" + 0 * m_gui.UnitConversion;
            this.ZLabel.Text = "Z [" + m_gui.Units + "]";
            this.YLabel.Text = "Y [" + m_gui.Units + "]";
            this.XLabel.Text = "X [" + m_gui.Units + "]";
            this.ZTextBox.Text = "" + 0 * m_gui.UnitConversion;
            this.YTextBox.Text = "" + 0 * m_gui.UnitConversion;
            this.XTextBox.Text = "" + 7000.0e3 * m_gui.UnitConversion;
            this.GravityTextBox.Text = "" + 398600.4415e9 * Math.Pow(m_gui.UnitConversion, 3);
            this.label6.Text = "[" + m_gui.Units + "^3 / sec^2]";
        }

        public JulianDate Epoch
        {
            get { return m_epoch; }
        }
        public Cartesian Position
        {
            get { return m_position; }
        }
        public Cartesian Velocity
        {
            get { return m_velocity; }
        }
        public double Gravity
        {
            get { return m_gravity; }
        }

        private JulianDate m_epoch;
        private Cartesian m_position;
        private Cartesian m_velocity;
        private double m_gravity;
        private GraphicalUserInterface m_gui;
        private int m_index;

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            try
            {
                double jdate = Double.Parse(EpochTextBox.Text);
                double X = Double.Parse(XTextBox.Text);
                double Y = Double.Parse(YTextBox.Text);
                double Z = Double.Parse(ZTextBox.Text);
                double dX = Double.Parse(XdotTextBox.Text);
                double dY = Double.Parse(YdotTextBox.Text);
                double dZ = Double.Parse(ZdotTextBox.Text);
                double mu = Double.Parse(GravityTextBox.Text);
                m_epoch = new JulianDate(jdate, TimeStandard.CoordinatedUniversalTime);
                m_position = new Cartesian(X, Y, Z);
                m_velocity = new Cartesian(dX, dY, dZ);
                m_gravity = mu;
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Epoch:      " + Epoch);
                builder.AppendLine("Gravity:    " + Gravity);
                builder.AppendLine("Propagator: " + "TwoBody");
                builder.AppendLine("Position:   " + Position);
                builder.AppendLine("Velocity:   " + Velocity);

                m_position = m_position.Divide(m_gui.UnitConversion);
                m_velocity = m_velocity.Divide(m_gui.UnitConversion);
                m_gravity = m_gravity / Math.Pow(m_gui.UnitConversion, 3);

                TwoBodyPropagator prop = new TwoBodyPropagator(Epoch, CentralBodiesFacet.GetFromContext().Earth.InertialFrame,
                    new AGI.Foundation.Motion<Cartesian>(m_position, m_velocity), m_gravity);
                     
                if (m_index == 1)
                {
                    m_gui.PointOne = prop.CreatePoint();
                    m_gui.InputOne.Text = builder.ToString();
                }
                else
                {
                    m_gui.PointTwo = prop.CreatePoint();
                    m_gui.InputTwo.Text = builder.ToString();
                }
                this.Close();
            }
            catch (Exception)
            {
                System.Console.WriteLine("Error parsing Cartesian input");
            }
        }
    }
}