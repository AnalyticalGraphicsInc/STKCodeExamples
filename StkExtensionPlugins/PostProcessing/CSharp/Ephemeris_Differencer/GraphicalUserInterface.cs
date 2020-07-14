using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AGI.Foundation.Stk;
using AGI.Foundation;
using AGI.Foundation.Propagators;
using AGI.Foundation.Time;
using AGI.Foundation.Geometry;
using AGI.Foundation.Celestial;
using AGI.Foundation.Coordinates;

namespace EphemerisDifferencer
{
    public partial class GraphicalUserInterface : Form
    {
        public GraphicalUserInterface()
        {
            InitializeComponent();
            using (StreamReader reader = new StreamReader("../../files/EOP-v1.1.txt"))
            {
                EarthOrientationParameters eop = EarthOrientationParametersFile.ReadData(reader);
                CentralBodiesFacet.GetFromContext().Earth.OrientationParameters = eop;
            }
            m_unitConversion = UnitsBox.Text.Contains("Kilometers") ? 0.001 : 1.0;
        }

        #region Properties and Fields
        public TextBox InputOne
        {
            get { return InputBoxOne; }
        }
        public TextBox InputTwo
        {
            get { return InputBoxTwo; }
        }
        public AGI.Foundation.Geometry.Point PointOne
        {
            get { return m_pointOne; }
            set { m_pointOne = value; }
        }
        public AGI.Foundation.Geometry.Point PointTwo
        {
            get { return m_pointTwo; }
            set { m_pointTwo = value; }
        }
        public TextBox Output
        {
            get { return OutputBox; }
        }

        public string Units
        {
            get { return UnitsBox.Text; }
        }

        public double UnitConversion
        {
            get { return m_unitConversion; }
        }

        private AGI.Foundation.Geometry.Point m_pointOne;
        private AGI.Foundation.Geometry.Point m_pointTwo;
        private string m_epSecFormat = "{0,8:F}";
        private string m_dataFormat = "{0,17:######0.00000}";
        private IList<double> m_epSecList = new List<double>();
        private IList<double>[] m_positionError = new List<double>[3];
        private IList<double>[] m_velocityError = new List<double>[3];
        private double m_unitConversion = 1.0;
        #endregion

        #region Worker Methods
        private void OpenCartesian_handler(object sender, EventArgs e, int index)
        {
            InitializeCartesian panel = new InitializeCartesian(this, index);
            panel.Show();
            panel.Activate();
        }

        private void OpenKeplerian_handler(object sender, EventArgs e, int index)
        {
            InitializeKeplerian panel = new InitializeKeplerian(this, index);
            panel.Show();
            panel.Activate();
        }

        private void OpenFile_handler(object sender, EventArgs e, int index)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "..\\..\\files\\";
            openFileDialog1.Filter = "All files (*.*)|*.*" +
                "|Ephemeris files (*.e *.tle)|*.e; *.tle";
            //+"|Two Line Element Set (*.tle *.txt)|*.tle";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            if (index == 1)
                            {
                                InputBoxOne.Clear();
                                InputBoxOne.Text = "" + openFileDialog1.FileName;
                            }
                            else
                            {
                                InputBoxTwo.Clear();
                                InputBoxTwo.Text = "" + openFileDialog1.FileName;
                            }
                            // Insert code to read the stream here.
                            string extension = openFileDialog1.FileName.Substring(openFileDialog1.FileName.IndexOf('.'));
                            System.Console.WriteLine(openFileDialog1.FileName + "  " + extension);
                            switch (extension)
                            {
                                case ".e":
                                    System.Console.WriteLine("Recognized .e file");
                                    LoadEFile(myStream, index);
                                    break;
                                case ".tle":
                                    System.Console.WriteLine("Recognized .tle file");
                                    LoadTLEFile(myStream, index);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void LoadEFile(Stream fileStream, int index)
        {
            StreamReader reader = new StreamReader(fileStream);
            StkEphemerisFile ephemeris = StkEphemerisFile.ReadFrom(reader);
            System.Console.WriteLine(ephemeris.Data.Times[0].ToString());
            StkEphemerisFile.EphemerisTimePosVel ephem = ephemeris.Data as StkEphemerisFile.EphemerisTimePosVel;
            if (ephem == null)
            {
                System.Console.WriteLine("Error parsing Ephemeris file.");
            }
            JulianDate epoch = ephemeris.Data.Times[0].ToTimeStandard(TimeStandard.CoordinatedUniversalTime);
            TimeInterval available = new TimeInterval(epoch,
                ephemeris.Data.Times[ephemeris.Data.Times.Count - 1].ToTimeStandard(TimeStandard.CoordinatedUniversalTime));
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" ");
            builder.AppendLine("STK Ephemeris");
            builder.AppendLine("Using Interpolation");
            builder.AppendLine("Start Epoch: " + epoch.ToString());
            builder.AppendLine("End Epoch:   " + available.Stop.ToTimeStandard(TimeStandard.CoordinatedUniversalTime).ToString());
            if (index == 1)
            {
                m_pointOne = new PointInterpolator(CentralBodiesFacet.GetFromContext().Earth.J2000Frame, ephem.Interpolator);
                InputBoxOne.AppendText(builder.ToString());
            }
            else
            {
                m_pointTwo = new PointInterpolator(CentralBodiesFacet.GetFromContext().Earth.J2000Frame, ephem.Interpolator);
                InputBoxTwo.AppendText(builder.ToString());
            }
            StartTimeBox.Text = "" + available.Start.ToTimeStandard(TimeStandard.CoordinatedUniversalTime).TotalDays;
        }

        private void LoadTLEFile(Stream fileStream, int index)
        {
            StreamReader reader = new StreamReader(fileStream);
            string data = reader.ReadToEnd();
            TwoLineElementSet tle = new TwoLineElementSet(data);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" ");
            builder.AppendLine("TLE Ephemeris");
            builder.AppendLine("Epoch: " + tle.Epoch.ToTimeStandard(TimeStandard.CoordinatedUniversalTime).ToString());
            builder.AppendLine("Propagator:     " + "SGP4");
            builder.AppendLine("Arg Perigee:    " + Trig.RadiansToDegrees(Trig.ZeroToTwoPi(tle.ArgumentOfPerigee)));
            builder.AppendLine("BStar:          " + tle.BStar);
            builder.AppendLine("Eccentricity:   " + tle.Eccentricity);
            builder.AppendLine("Inclination:    " + Trig.RadiansToDegrees(Trig.ZeroToTwoPi(tle.Inclination)));
            builder.AppendLine("Mean Anomaly:   " + Trig.RadiansToDegrees(Trig.ZeroToTwoPi(tle.MeanAnomaly)));
            builder.AppendLine("Mean Motion:    " + Trig.RadiansToDegrees(Trig.ZeroToTwoPi(tle.MeanMotion)));
            builder.AppendLine("Ascending Node: " + Trig.RadiansToDegrees(Trig.ZeroToTwoPi(tle.RightAscensionOfAscendingNode)));

            if (index == 1)
            {
                m_pointOne = new Sgp4Propagator(tle).CreatePoint();
                InputBoxOne.AppendText(builder.ToString());
            }
            else
            {
                m_pointTwo = new Sgp4Propagator(tle).CreatePoint();
                InputBoxTwo.AppendText(builder.ToString());
            }
        }

        private void CompareFiles()
        {
            PointEvaluator evalOne = m_pointOne.GetEvaluator();
            PointEvaluator evalTwo = m_pointTwo.GetEvaluator();
            TimeIntervalCollection intervalOne = evalOne.AvailabilityIntervals;
            TimeIntervalCollection intervalTwo = evalTwo.AvailabilityIntervals;
            TimeIntervalCollection intervals = intervalOne.Intersect(intervalTwo);

            if (intervals.IsEmpty)
            {
                OutputBox.Clear();
                OutputBox.AppendText("Error: The two ephemerides do not overlap");
            }
            else
            {
                OutputBox.Clear();
                StringBuilder builder = new StringBuilder();

                JulianDate start = intervals[0].Start;

                JulianDate userStart = new JulianDate(Double.Parse(StartTimeBox.Text), TimeStandard.CoordinatedUniversalTime);
                double dt = Double.Parse(TimeStepBox.Text);
                if (userStart > start) start = userStart;

                JulianDate end = intervals[intervals.Count - 1].Stop;
                builder.AppendLine("Result: Second Ephemeris - First Ephemeris");
                string frame = ReferenceFrameComboBox.Text;
                if (frame.Contains("RIC")) frame = frame + " with coordinates defined by the first ephemeris";
                builder.AppendLine("Frame: " + frame);
                builder.AppendLine("Epoch JulianDate: " + start.ToTimeStandard(TimeStandard.CoordinatedUniversalTime));
                builder.AppendLine("Units: [" + UnitsBox.Text + "] [Sec] ");
                builder.AppendLine(" ");
                builder.AppendLine(String.Format(m_epSecFormat, "EpSec") + "" +
                    String.Format(m_dataFormat, "    X_Diff    ") + "" +
                    String.Format(m_dataFormat, "    Y_Diff    ") + "" +
                    String.Format(m_dataFormat, "    Z_Diff    ") + "" +
                    String.Format(m_dataFormat, "    Xdot_Diff ") + "" +
                    String.Format(m_dataFormat, "    Ydot_Diff ") + "" +
                    String.Format(m_dataFormat, "    Zdot_Diff "));
                builder.AppendLine(" ");
                m_epSecList = new List<double>();
                m_positionError = new List<double>[3];
                m_positionError[0] = new List<double>();
                m_positionError[1] = new List<double>();
                m_positionError[2] = new List<double>();
                m_velocityError = new List<double>[3];
                m_velocityError[0] = new List<double>();
                m_velocityError[1] = new List<double>();
                m_velocityError[2] = new List<double>();
                //* Handle User input for Frame
                EarthCentralBody earth = CentralBodiesFacet.GetFromContext().Earth;
                ReferenceFrame userFrame = earth.InertialFrame;
                switch (ReferenceFrameComboBox.Text)
                {
                    case "Earth J2000 XYZ":
                        userFrame = earth.J2000Frame;
                        break;
                    case "Earth Fixed XYZ":
                        userFrame = earth.FixedFrame;
                        break;
                    case "Earth J2000 RIC":
                        userFrame = new ReferenceFrame(PointOne, new AxesLocalVerticalLocalHorizontal(earth.J2000Frame, PointOne));
                        break;
                    default:
                        break;
                }
                string delimiter = "";
                if (TabCheckBox.Checked)
                {
                    delimiter = "\t";
                }
                int numberOfPoints = (int)NumberOfPointsBox.Value;
                VectorEvaluator eval;
                if (ApparentCheckBox.Checked)
                {
                    eval = GeometryTransformer.ObserveVector(new VectorApparentDisplacement(
                        PointOne, PointTwo,
                        earth.InertialFrame, SignalDirection.Transmit, 5.0 * Constants.Epsilon5),
                        userFrame.Axes);
                }
                else
                {
                    eval = GeometryTransformer.ObserveVector(new VectorTrueDisplacement(
                        PointOne, PointTwo), userFrame.Axes);
                }

                for (int i = 0; i < numberOfPoints; i++)
                {
                    JulianDate date = start.AddSeconds(dt * i);
                    if (date > end) break;

                    Motion<Cartesian> result = eval.Evaluate(date, 1);
                    Cartesian dp = result.Value;
                    Cartesian dv = result.FirstDerivative;
                    m_epSecList.Add(dt * i);
                    m_positionError[0].Add(dp.X * m_unitConversion);
                    m_positionError[1].Add(dp.Y * m_unitConversion);
                    m_positionError[2].Add(dp.Z * m_unitConversion);
                    m_velocityError[0].Add(dv.X * m_unitConversion);
                    m_velocityError[1].Add(dv.Y * m_unitConversion);
                    m_velocityError[2].Add(dv.Z * m_unitConversion);
                    builder.AppendLine("" + String.Format(m_epSecFormat, (dt * i)) + delimiter +
                        String.Format(m_dataFormat, dp.X * m_unitConversion) + delimiter +
                        String.Format(m_dataFormat, dp.Y * m_unitConversion) + delimiter +
                        String.Format(m_dataFormat, dp.Z * m_unitConversion) + delimiter +
                        String.Format(m_dataFormat, dv.X * m_unitConversion) + delimiter +
                        String.Format(m_dataFormat, dv.Y * m_unitConversion) + delimiter +
                        String.Format(m_dataFormat, dv.Z));
                }
                OutputBox.AppendText(builder.ToString());
            }
        }
        #endregion

        #region Button Handlers
        private void GraphButton_Click(object sender, EventArgs e)
        {
            if (m_pointOne == null || m_pointTwo == null)
            {
                OutputBox.Clear();
                OutputBox.AppendText("Error: Ephemerides not initialized.");
            }
            else
            {
                try
                {
                    CompareFiles();

                    if (ShowGraphCheckBox.Checked)
                    {
                        ErrorGraph graph = new ErrorGraph();
                        ErrorGraph.UpdateGraph(graph.PositionErrorGraph, "" + ReferenceFrameComboBox.Text + " Position Error [" + UnitsBox.Text + "]", m_epSecList, m_positionError);
                        ErrorGraph.UpdateGraph(graph.VelocityErrorGraph, "" + ReferenceFrameComboBox.Text + " Velocity Error [" + UnitsBox.Text + "/Sec]", m_epSecList, m_velocityError);

                        graph.Visible = true;
                        graph.Activate();
                        graph.Show();
                    }
                }
                catch
                {
                    OutputBox.Clear();
                    OutputBox.AppendText("Error: There was an unexpected error.  Please check the input and try again.");
                }
            }
        }

        private void OpenOne_Click(object sender, EventArgs e)
        {
            int index = 1;
            switch (FirstElementsBox.Text)
            {
                case "Ephemeris from File":
                    OpenFile_handler(sender, e, index);
                    break;
                case "Cartesian Elements":
                    OpenCartesian_handler(sender, e, index);
                    break;
                case "Keplerian Elements":
                    OpenKeplerian_handler(sender, e, index);
                    break;
                default:
                    break;
            }
        }

        private void OpenTwo_Click(object sender, EventArgs e)
        {
            int index = 2;
            switch (SecondElementsBox.Text)
            {
                case "Ephemeris from File":
                    OpenFile_handler(sender, e, index);
                    break;
                case "Cartesian Elements":
                    OpenCartesian_handler(sender, e, index);
                    break;
                case "Keplerian Elements":
                    OpenKeplerian_handler(sender, e, index);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Miscellaneous
        private void GraphicalUserInterface_Load(object sender, EventArgs e)
        {

        }


        private void NumberOfPointsBox_Return(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                e.Handled = true;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Lock_ComboBox(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        private void UnitsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnitsBox.Text.Contains("Kilometers"))
            {
                m_unitConversion = 0.001;
            }
            else // "Meters"
            {
                m_unitConversion = 1.0;
            }
        }

    }
}