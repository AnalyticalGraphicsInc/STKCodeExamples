using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// AGI using directives
using AGI.STKObjects;
using AGI.Ui.Application;

// This example will use the COM interface to bind to an open version of STK
// and then begin extracting information which will be populated in the form

namespace PullDataFromSTKExample
{
    public partial class MainForm : Form
    {
        // Member Variables
        AgStkObjectRoot         m_root;
        IAgDataPrvTimeVar       m_accessProviderIntervals;
        IAgStkAccess            m_access;

        Array m_dataProviderElements = new object[] { "Azimuth", "Elevation" };

        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Connect to open STK
                AgUiApplication app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK12.Application") as AgUiApplication;
                m_root = app.Personality2 as AgStkObjectRoot;

                if (m_root.Children.Count == 0)
                {
                    throw new Exception("No scenario open.");
                }

                PopulateComboBoxes();

                //IMPORTANT - set default units to epoch seconds
                m_root.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");

                // Add event handlers to update combo boxes when object is added/removed/modified
                m_root.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(Root_OnStkObjectAdded);
                m_root.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(Root_OnStkObjectDeleted);
                m_root.OnStkObjectChanged += new IAgStkObjectRootEvents_OnStkObjectChangedEventHandler(Root_OnStkObjectChanged);

                buttonConnect.Enabled = false;
                radioButtonOpen.Enabled = true;
                radioButtonClose.Enabled = true;

            }
            catch (Exception exception)
            {
                MessageBox.Show("Error connecting to STK: " + exception.Message, "Error");             
            }

        }

        private void PopulateComboBoxes()
        {
            Invoke((MethodInvoker)delegate ()
            {
                AddStkObjects(comboBoxSatellite, AgESTKObjectType.eSatellite);
                AddStkObjects(comboBoxFacility, AgESTKObjectType.eFacility);
            });

        }

        private void AddStkObjects(ComboBox comboBox, AgESTKObjectType objectType)
        {
            try
            {
                // Save the current selection
                object currentItem = comboBox.SelectedItem;
                comboBox.Items.Clear();

                foreach (IAgStkObject stkObject in m_root.CurrentScenario.Children.GetElements(objectType))
                {
                    // Add item by name as everyone will be unique and this list only has satellites
                    comboBox.Items.Add(stkObject.InstanceName);
                }

                // Keep the previous selection, unless there wasn't one or it was deleted.
                if (currentItem == null)
                {
                    try
                    {
                        comboBox.SelectedIndex = 0;
                    }
                    catch { }
                }
                else
                {
                    if (comboBox.Items.Contains(currentItem))
                    {
                        comboBox.SelectedItem = currentItem;
                    }
                    else
                    {
                        try
                        {
                            comboBox.SelectedIndex = 0;
                        }
                        catch { }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void RadioButtonOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOpen.Checked.Equals(true))
            {
                try
                {

                    if (comboBoxSatellite.SelectedItem == null)
                    {
                        throw new Exception("No satellite selected.");
                    }
                    if (comboBoxFacility.SelectedItem == null)
                    {
                        throw new Exception("No facility selected.");
                    }

                    // Disable combo boxes for now
                    comboBoxSatellite.Enabled = false;
                    comboBoxFacility.Enabled = false;

                    // Disable any previous events initialized
                    try
                    {
                        m_root.OnAnimUpdate -= new IAgStkObjectRootEvents_OnAnimUpdateEventHandler(Root_OnAnimUpdate);
                    }
                    catch { }

                    // Compute access
                    IAgStkObject accessObject = m_root.GetObjectFromPath("Facility/" + comboBoxFacility.SelectedItem.ToString());
                    m_access = accessObject.GetAccess("Satellite/" + comboBoxSatellite.SelectedItem.ToString());
                    m_access.ComputeAccess();

                    // Get AER data provider
                    m_accessProviderIntervals = m_access.DataProviders.GetDataPrvTimeVarFromPath("AER Data/Default");

                    // Add the event handler we will need
                    m_root.OnAnimUpdate += new IAgStkObjectRootEvents_OnAnimUpdateEventHandler(Root_OnAnimUpdate);
                }
                catch (Exception exception)
                {
                    radioButtonOpen.Checked = false;
                    MessageBox.Show(exception.Message, "Error");
                }
            }
        }

        private void RadioButtonClose_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (radioButtonClose.Checked.Equals(true))
                {
                    // Terminate stream
                    try
                    {
                        m_root.OnAnimUpdate -= new IAgStkObjectRootEvents_OnAnimUpdateEventHandler(Root_OnAnimUpdate);
                        m_access.RemoveAccess();
                    }
                    catch
                    {
                        throw new Exception("Unable to disable stream");
                    }

                    // Enable combo boxes
                    comboBoxSatellite.Enabled = true;
                    comboBoxFacility.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                radioButtonClose.Checked = false;
                MessageBox.Show(exception.Message, "Error");
            }

        }

        void Root_OnAnimUpdate(double timeEpSec)
        {
            // Gather data and update the text boxes
            IAgDrResult result = m_accessProviderIntervals.ExecSingleElements(timeEpSec, ref m_dataProviderElements);
            if (result.Intervals.Count > 0)
            {
                Array azimuth = result.Intervals[0].DataSets.GetDataSetByName("Azimuth").GetValues();
                Array elevation = result.Intervals[0].DataSets.GetDataSetByName("Elevation").GetValues();

                SetResponseText(textBoxAzimuth, azimuth.GetValue(0).ToString());
                SetResponseText(textBoxElevation, elevation.GetValue(0).ToString());
            }
            else
            {
                SetResponseText(textBoxAzimuth, "No Values");
                SetResponseText(textBoxElevation, "No Values");
            }
           
        }
        
        void Root_OnStkObjectChanged(IAgStkObjectChangedEventArgs pArgs)
        {
            PopulateComboBoxes();
        }

        void Root_OnStkObjectDeleted(object Sender)
        {
            PopulateComboBoxes();
        }

        void Root_OnStkObjectAdded(object Sender)
        {
            PopulateComboBoxes();
        }

        delegate void UpdateTextBox(TextBox textBox, string text);

        // Function for handling text updates
        private void SetResponseText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                UpdateTextBox updateTextBoxes = new UpdateTextBox(SetResponseText);
                Invoke(updateTextBoxes, new object[] { textBox, text });
            }
            else
            {
                textBox.Text = text;
            }
        }
    }
}