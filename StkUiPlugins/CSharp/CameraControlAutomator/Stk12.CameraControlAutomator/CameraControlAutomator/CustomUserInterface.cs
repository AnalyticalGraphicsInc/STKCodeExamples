using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;
using System.Collections.Specialized;
using AGI.STKUtil;

namespace CameraControlAutomator
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;

        public CustomUserInterface()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Implementation
        public stdole.IPictureDisp GetIcon()
        {
            return null;
        }

        public void OnClosing()
        {
            CommonData.StkRoot.OnStkObjectAdded -= m_root_OnStkObjectAdded;
            CommonData.StkRoot.OnStkObjectDeleted -= m_root_OnStkObjectDeleted;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_stkObjectsLibrary = new StkObjectsLibrary();

            //EXAMPLE: Hooking to STK Exents
            CommonData.StkRoot.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            CommonData.StkRoot.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);

            //EXAMPLE: Using preference value
            //m_uiPlugin.DoubleValue;

            //EXAMPLE: Populate combo box with STK Objects
            PopulateComboBox();
            cbStkObjects.SelectedIndex = 0;
            PopulateCurrentPaths();
            NewPath.Checked = true;
            PopulateAxes(Rotation1Axis);
            Rotation1Axis.SelectedIndex = 0;
            Rotation1Deg.Text = "0";
            Rotation1Deg.Enabled = false;
            PopulateAxes(Rotation2Axis);
            Rotation2Axis.SelectedIndex = 0;
            Rotation2Deg.Text = "0";
            Rotation2Deg.Enabled = false;
            Rotation2Axis.Enabled = false;
            MinorAxisLength.Text = "50";
            MajorAxisLength.Text = "50";
            ZOffset.Text = "0";
            PostRotation.Checked = true;
            PathName.Text = "NewPath";
            StartTime.Text = "0";
            Duration.Text = "30";
            StartDeg.Text = "0";
            StopDeg.Text = "360";
            FOVValue.Text = "45";
            NumPoints.Text = "30";
            CommonData.StkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");
        }


        #endregion

        #region Sample code
        void m_root_OnStkObjectDeleted(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            cbStkObjects.Items.Remove(simpleName);
        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            cbStkObjects.Items.Add(simpleName);
        }

        void PopulateComboBox()
        {
            cbStkObjects.Items.Clear();
            string simpleName;
            string className;
            StringCollection objectNames = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");

            foreach (string objectName in objectNames)
            {
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "Sensor" && className != "Antenna" && className != "Transmitter" && className != "Receiver" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Radar" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = m_stkObjectsLibrary.ObjectName(objectName);

                    cbStkObjects.Items.Add(simpleName);
                }
            }

            if (cbStkObjects.Items.Count > 0)
            {
                cbStkObjects.SelectedIndex = 0;
            }
        }
        #endregion

        private void lblStkObjects_Click(object sender, EventArgs e)
        {

        }

        private void cbStkObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonData.SelectedObjectName = cbStkObjects.Text;

            string simpleName;
            string className;
            simpleName = cbStkObjects.Text;
            StringCollection objectPaths = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string path in objectPaths)
            {
                string objectName = m_stkObjectsLibrary.ObjectName(path);
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                if (objectName == simpleName && className != "Scenario")
                {
                    className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                    CommonData.SelectedObjectClass = className;
                }
            }
        }

        private void NewPath_CheckedChanged(object sender, EventArgs e)
        {
            
            if (NewPath.Checked)
            {
                PathName.Enabled = true;
                AddPathName.Enabled = false;
            }
        }

        private void AddToPath_CheckedChanged(object sender, EventArgs e)
        {
            if (AddToPath.Checked)
            {
                PathName.Enabled = false;
                AddPathName.Enabled = true;
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            int check = FieldCheck();
            if (check==0)
            {
                string cmd;
                IAgExecCmdResult result;
                IAgConversionUtility converter = CommonData.StkRoot.ConversionUtility;
                string objectPath = CommonData.SelectedObjectClass + "/" + CommonData.SelectedObjectName;
                int numPoints = Int32.Parse(NumPoints.Text);
                double startTime = Double.Parse(StartTime.Text);
                double duration = Double.Parse(Duration.Text);
                double currentTime;
                double startDeg = Double.Parse(StartDeg.Text);
                double stopDeg = Double.Parse(StopDeg.Text);
                double b = Double.Parse(MinorAxisLength.Text);
                double a = Double.Parse(MajorAxisLength.Text);
                double zOffset = Double.Parse(ZOffset.Text);
                double multiplier = Math.PI / 180;
                double theta;
                double rotation1deg = Double.Parse(Rotation1Deg.Text);
                double rotation2deg = Double.Parse(Rotation2Deg.Text);
                double x;
                double y;
                double z;
                double[,] rotation1 = new double[3, 3];
                double[,] rotation2 = new double[3, 3];
                double[,] finalRotation = new double[3, 3];
                double[] position = new double[3];
                double[] newPosition = new double[3];
                int startKey = 1;
                int key;
                int error = 0;
                int error1 = 0;
                string errorCode = null;
                string pathName = null;
                double step = duration / numPoints;
                string currentTimeStr = null;
                if (NewPath.Checked)
                {
                    try
                    {
                        pathName = PathName.Text;
                        startKey = 1;
                        result = CommonData.StkRoot.ExecuteCommand("VO_R * CameraControl GetPaths");
                        string resultStr = result[0].Replace("\"", "");
                        string[] pathNames = resultStr.Split(null);
                        foreach (var path in pathNames)
                        {
                            if (path == pathName)
                            {
                                CommonData.StkRoot.ExecuteCommand("VO * CameraControl CameraPath Delete Name \"" + pathName + "\"");
                            }
                        }
                        CommonData.StkRoot.ExecuteCommand("VO * CameraControl CameraPath Add Name \"" + pathName + "\"");
                        CommonData.StkRoot.ExecuteCommand("VO * CameraControl KeyframeProps \"" + pathName + "\" ReferenceAxes \"" + objectPath + " VVLH(CBF) Axes\"");
                        CommonData.StkRoot.ExecuteCommand("VO * CameraControl 3DWindowProps FollowMode off OrientVecEditMode on ActivePath \"" + pathName + "\"");
                    }
                    catch (Exception)
                    {
                        errorCode = errorCode + " 1";
                        error1++;
                    }
                }
                else if (AddToPath.Checked)
                {
                    try
                    {
                        pathName = AddPathName.Text;
                        //CommonData.StkRoot.ExecuteCommand("VO * CameraControl KeyframeProps \"" + pathName + "\" ReferenceAxes \"" + objectPath + " VVLH(CBF) Axes\"");
                        CommonData.StkRoot.ExecuteCommand("VO * CameraControl 3DWindowProps FollowMode off OrientVecEditMode on ActivePath \"" + pathName + "\"");
                        pathName = AddPathName.Text;
                        result = CommonData.StkRoot.ExecuteCommand("VO_R * CameraControl GetKeyframeData \"" + pathName + "\" Data Time");
                        string[] separator = new string[] { "\" \"" };
                        string[] times = result[0].Split(separator, StringSplitOptions.None);
                        startKey = times.Length + 1;

                        currentTimeStr = converter.ConvertDate("EpSec", "UTCG", (startTime + 1).ToString());
                        CommonData.StkRoot.ExecuteCommand("SetAnimation * CurrentTime \"" + currentTimeStr + "\"");
                    }
                    catch (Exception)
                    {
                        errorCode = errorCode + " 2";
                        error1++;
                    }
                }

                for (int i = 0; i <= numPoints; i++)
                {
                    try
                    {
                        CommonData.StkRoot.ExecuteCommand("VO * CameraControl Keyframes \"" + pathName + "\" Add");
                    }
                    catch (Exception)
                    {
                        errorCode = errorCode + " 3";
                        error1++;
                    }
                }
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO * CameraControl KeyframeProps \"" + pathName + "\" ReferenceAxes \"" + objectPath + " VVLH(CBF) Axes\"");
                }
                catch (Exception)
                {
                    errorCode = errorCode + " 4";
                    error1++;
                }

                for (int i = 0; i <=numPoints; i++)
                {
                    theta = startDeg + i * (stopDeg - startDeg) / numPoints;
                    currentTime = startTime + i * (duration / numPoints);
                    currentTimeStr = converter.ConvertDate("EpSec", "UTCG", currentTime.ToString());
                    x = a * Math.Cos(theta * multiplier);
                    y = b * Math.Sin(theta * multiplier);
                    if (PreRotation.Checked)
                    {
                        z = zOffset;
                    }
                    else
                    {
                        z = 0.0;
                    }
                    position[0] = x; position[1] = y; position[2] = z;
                    if (Rotation1Axis.SelectedIndex!=0)
                    {
                        rotation1 = MatrixFunctions.RotateAbout(Rotation1Axis.SelectedIndex, rotation1deg, "deg");
                        if (Rotation2Axis.SelectedIndex!=0)
                        {
                            rotation2 = MatrixFunctions.RotateAbout(Rotation2Axis.SelectedIndex, rotation2deg, "deg");
                            finalRotation = MatrixFunctions.MatrixMultiply(rotation1, rotation2);
                        }
                        else
                        {
                            finalRotation = rotation1;
                        }
                        newPosition = MatrixFunctions.RotateVector(finalRotation, position);
                        if (PostRotation.Checked)
                        {
                            newPosition[2] = newPosition[2] + zOffset;
                        }
                    }
                    else
                    {
                        newPosition[0] = x; newPosition[1] = y;
                        if (PreRotation.Checked)
                        {
                            newPosition[2] = z;
                        }
                        else
                        {
                            newPosition[2] = z + zOffset;
                        }
                    }
                    key = startKey + i;
                    
                    try
                    {
                        cmd = "VO * CameraControl Keyframes \"" + pathName + "\" Modify " + key.ToString() + " Position " + newPosition[0].ToString() + " " + newPosition[1].ToString() + " " + newPosition[2].ToString() + " FieldOfView " + FOVValue.Text +" Time \""+currentTimeStr+"\"";
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {

                        error++;
                    }
                }
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO * CameraControl KeyframeProps \"" + pathName + "\" ReferenceAxes \"" + objectPath + " TopoCentric Axes\"");
                }
                catch (Exception)
                {

                }
                if (error!=0)
                {
                    MessageBox.Show("Could not create " + error.ToString() + " keyframes");
                }
                if (error1!=0)
                {
                    MessageBox.Show("Error Code(s): " + errorCode);
                }
                PopulateCurrentPaths();
            }
        }

        private void UseCurrentTime_Click(object sender, EventArgs e)
        {
            IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
            double currentTime = animationRoot.CurrentTime;
            StartTime.Text = currentTime.ToString();
        }

        private void PopulateCurrentPaths()
        {
            AddPathName.Items.Clear();
            IAgExecCmdResult result= CommonData.StkRoot.ExecuteCommand("VO_R * CameraControl GetPaths");
            string resultStr = result[0].Replace("\"", "");
            string[] pathNames = resultStr.Split(null);
            foreach (var path in pathNames)
            {
                AddPathName.Items.Add(path);
            }
            if (pathNames.Length!=0)
            {
                AddPathName.SelectedIndex = 0;
            }
        }
        private void PopulateAxes(ComboBox box)
        {
            box.Items.Add("None");
            box.Items.Add("X-Axis");
            box.Items.Add("Y-Axis");
            box.Items.Add("Z-Axis");
        }

        private int FieldCheck()
        {
            int check = 0;
            double temp;
            int temp2;
            bool isNumerical = Double.TryParse(MajorAxisLength.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Semi-Major Axis field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(MinorAxisLength.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Semi-Minor Axis field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(FOVValue.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("FOV field not a number");
                check = 1;
            }
            else if (temp>100||temp<0)
            {
                MessageBox.Show("FOV value must be between 0 and 100 deg");
                check = 1;
            }
            isNumerical = Double.TryParse(StartDeg.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Start Angle field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(StopDeg.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Stop Angle field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(Rotation1Deg.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Rotation 1 Angle field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(Rotation2Deg.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Rotation 2 Angle field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(StartTime.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Start Time field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(Duration.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Duration field not a number");
                check = 1;
            }
            isNumerical = Int32.TryParse(NumPoints.Text, out temp2);
            if (!isNumerical)
            {
                MessageBox.Show("Number of Points field not a whole number");
                check = 1;
            }
            isNumerical = Double.TryParse(ZOffset.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Z-Offset field not a whole number");
                check = 1;
            }


            return check;
        }

        private void Rotation1Axis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Rotation1Axis.SelectedIndex!=-1)
            {
                if (Rotation1Axis.SelectedIndex == 0)
                {

                    Rotation2Axis.Enabled = false;
                    Rotation2Deg.Enabled = false;
                    Rotation1Deg.Enabled = false;
                }
                else
                {
                    Rotation2Axis.Enabled = true;
                    Rotation1Deg.Enabled = true;
                }
            }
        }

        private void Rotation2Axis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Rotation2Axis.SelectedIndex != -1)
            {
                if (Rotation2Axis.SelectedIndex == 0)
                {
                    Rotation2Deg.Enabled = false;
                }
                else
                {
                    Rotation2Deg.Enabled = true;
                }
            }
        }

        private void PostRotation_CheckedChanged(object sender, EventArgs e)
        {
            if(PostRotation.Checked)
            {
                PreRotation.Checked = false;
            }
        }

        private void PreRotation_CheckedChanged(object sender, EventArgs e)
        {
            if (PreRotation.Checked)
            {
                PostRotation.Checked = false;
            }
        }
    }
}
