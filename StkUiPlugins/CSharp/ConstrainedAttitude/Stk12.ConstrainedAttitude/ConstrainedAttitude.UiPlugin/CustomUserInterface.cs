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
using AGI.STKVgt;
using System.Diagnostics;
using AGI.STKUtil;

namespace ConstrainedAttitude.UiPlugin
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        // Member variables
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;

        private AgSatellite m_selectedObject;
        private AgSatellite m_referenceSatellite;

        private string[] m_vectorType = new string[] { "aligned", "constrained" };
        string[] m_axes = new string[] { "X", "Y", "Z" };

        public enum VectorType
        {
            eAligned,
            eConstrained
        }

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

        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_stkObjectsLibrary = new StkObjectsLibrary();

            // Get object that user selected
            m_selectedObject = CommonData.StkRoot.GetObjectFromPath(m_uiPlugin.Site.Selection[0].Path) as AgSatellite;

            PopulateComboBox();
        }
        #endregion


        void PopulateComboBox()
        {
            // Add all vectors on object to both lists
            IAgCrdnVectorGroup vectors = m_selectedObject.Vgt.Vectors;

            foreach (IAgCrdnVector vector in vectors)
            {
                // All vectors implement IAgCrdn interface which provides
                // information about the vector instance and its type.
                IAgCrdn crdn = vector as IAgCrdn;

                alignedVectorComboBox.Items.Add(crdn.Name);
                constrainedVectorComboBox.Items.Add(crdn.Name);
            }
        }

        private void CreateAttitudeButton_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar.Visible = true;
                UpdateProgress(0);

                // Check inputs
                CheckUserInputs();

                // Get inputs
                string alignedVectorName = alignedVectorComboBox.SelectedItem.ToString();
                string constrainedVectorName = constrainedVectorComboBox.SelectedItem.ToString();
                double angleLimit = Convert.ToDouble(angleLimitTextBox.Text);
                string alignedBodyAxis = GetBodyAxis(VectorType.eAligned);
                string constrainedBodyAxis = GetBodyAxis(VectorType.eConstrained);

                UserInputs inputs = new UserInputs
                {
                    AlignedVectorName = alignedVectorName,
                    AlignedBodyAxis = alignedBodyAxis,
                    ConstrainedVectorName = constrainedVectorName,
                    ConstrainedBodyAxis = constrainedBodyAxis,
                    AngleLimit = angleLimit
                };
                UpdateProgress(25);

                // Duplicate the satellite to create a reference sat. All AWB components are created on this reference sat to avoid circular logic.
                m_referenceSatellite = DuplicateObject(m_selectedObject);
                UpdateProgress(50);

                // Create AWB components from user inputs
                IAgCrdnVector scheduledVector = CreateAlignedScheduledVector(inputs);
                UpdateProgress(75);

                // Set final attitude profile
                IAgCrdnVector selectedConstrainedVector = GetVectorFromObject((IAgStkObject)m_selectedObject, constrainedVectorName);
                SetAlignedConstrainedAttitude(m_selectedObject, scheduledVector, alignedBodyAxis, selectedConstrainedVector, constrainedBodyAxis);


                if (showGraphicsCheckBox.Checked == true)
                {
                    // Get AWB components to display
                    IAgCrdnAxes selectedBodyAxes = m_selectedObject.Vgt.Axes["Body"];
                    IAgCrdnVector selectedConstrainedBodyVector = GetVectorFromAxes((IAgStkObject)m_selectedObject, selectedBodyAxes, constrainedBodyAxis);
                    IAgCrdnVector selectedAlignedVector = GetVectorFromObject((IAgStkObject)m_selectedObject, alignedVectorName);
                    IAgCrdnAngle displayAngle = CreateAngle((IAgStkObject)m_selectedObject, $"Off_{constrainedVectorName}", selectedConstrainedVector, selectedConstrainedBodyVector);

                    // Display in 3D Graphics window.
                    ShowAttitudeGraphics(selectedAlignedVector, selectedConstrainedVector, selectedBodyAxes, displayAngle);

                    // Add negative body axis if necessary
                    if (constrainedBodyAxis.Contains("-"))
                    {
                        IAgCrdnVector bodyVector = m_selectedObject.Vgt.Vectors[$"Body.{constrainedBodyAxis}"];
                        DisplayElement(AgEGeometricElemType.eVectorElem, (IAgCrdn)bodyVector, Color.RoyalBlue);
                    }
                }

                UpdateProgress(100);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            progressBar.Visible = false;
        }

        private void CheckUserInputs()
        {
            CheckComboBoxes();
            CheckAngleInput();
            CheckBodyAxisValues();
            CheckBodyAxisLogic();
        }

        private void CheckComboBoxes()
        {
            // Make sure vector is selected for both
            if (alignedVectorComboBox.SelectedItem == null)
            {
                throw new Exception("Please select an Aligned Vector.");
            }

            if (constrainedVectorComboBox.SelectedItem == null)
            {
                throw new Exception("Please select a Constrained Vector");
            }

            string alignedVector = alignedVectorComboBox.SelectedItem.ToString();
            string constrainedVector = constrainedVectorComboBox.SelectedItem.ToString();
            if (alignedVector == constrainedVector)
            {
                throw new Exception("Aligned Vector and Constrained Vector cannot be the same.");
            }
        }

        private void CheckAngleInput()
        {
            double value;
            try
            {
                value = Convert.ToDouble(angleLimitTextBox.Text);
            }
            catch
            {
                throw new Exception("Invalid Angle Offset Limit. Value must be a number.");
            }

            if (value < 0)
            {
                MessageBox.Show("Angle Offset Limit is an absolute value. Positive value will be used.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                angleLimitTextBox.Text = Math.Abs(value).ToString();
            }
            else if (value > 360)
            {
                throw new Exception("Invalid Angle Offset Limit. Value cannot be greater than 360 deg.");
            }
        }

        private void CheckBodyAxisValues()
        {
            // Loop through all axis textboxes to make sure value is 0, 1 or -1
            foreach (string type in m_vectorType)
            {
                foreach (string axis in m_axes)
                {
                    string name = $"{type}{axis}TextBox";
                    foreach (Control control in Controls)
                    {
                        if (control.Name.Contains("BodyBox"))
                        {
                            foreach (Control innerControl in control.Controls)
                            {
                                if (innerControl.Name == name)
                                {
                                    if (innerControl.Text != "0" && innerControl.Text != "1" && innerControl.Text != "-1")
                                    {
                                        throw new Exception("Body axis value must be 0, 1, or -1.");
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        private void CheckBodyAxisLogic()
        {
            // Make sure only one axis is being set (to +/- 1) for both vectors
            foreach (string type in m_vectorType)
            {
                bool axisSet = false;
                foreach (Control control in Controls)
                {
                    if (control.Name == $"{type}BodyBox")
                    {
                        foreach (Control innerControl in control.Controls)
                        {
                            if (innerControl.Name.Contains(type) && innerControl.Name.Contains("TextBox"))
                            {
                                if (innerControl.Text.Contains("1"))
                                {
                                    if (axisSet == true)
                                    {
                                        string capitalizedType = char.ToUpper(type[0]) + type.Substring(1);
                                        throw new Exception($"{capitalizedType} Vector can only be set to one body axis.");
                                    }
                                    else
                                    {
                                        axisSet = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (axisSet == false)
                {
                    string capitalizedType = char.ToUpper(type[0]) + type.Substring(1);
                    throw new Exception($"Please set {capitalizedType} Vector to a body axis.");
                }
            }

            // Make sure vectors aren't set to the same axis
            string alignedAxis = GetBodyAxis(VectorType.eAligned);
            string constrainedAxis = GetBodyAxis(VectorType.eConstrained);

            if (alignedAxis.Contains(constrainedAxis) || constrainedAxis.Contains(alignedAxis))
            {
                throw new Exception("Aligned Vector body axis and Constrained Vector body axis cannot be co-linear.");
            }

        }

        private IAgCrdnVector CreateAlignedScheduledVector(UserInputs inputs)
        {
            // Unpack inputs
            string alignedVectorName = inputs.AlignedVectorName;
            string alignedBodyAxis = inputs.AlignedBodyAxis;
            string constrainedVectorName = inputs.ConstrainedVectorName;
            string constrainedBodyAxis = inputs.ConstrainedBodyAxis;
            double angleLimit = inputs.AngleLimit;

            // Get vectors from user input
            IAgCrdnVector duplicateAlignedVector = GetVectorFromObject((IAgStkObject)m_referenceSatellite, alignedVectorName);
            IAgCrdnVector duplicateConstrainedVector = GetVectorFromObject((IAgStkObject)m_referenceSatellite, constrainedVectorName);

            // Make sure vectors are valid
            CheckInputVector(duplicateAlignedVector);
            CheckInputVector(duplicateConstrainedVector);

            // Create Body vector string for naming
            string constrainedBodyVectorName = $"Body.{constrainedBodyAxis}";

            // Get body vector
            IAgCrdnAxes duplicateBodyAxes = m_referenceSatellite.Vgt.Axes["Body"];
            IAgCrdnVector duplicateConstrainedBodyVector = GetVectorFromAxes((IAgStkObject)m_referenceSatellite, duplicateBodyAxes, constrainedBodyAxis);

            // Set duplicate satellite's attitude to Aligned & Constrained with user input vectors. This is the unconstrained attitude that our
            // final satellite's attitude is based on.
            SetAlignedConstrainedAttitude(m_referenceSatellite, duplicateAlignedVector, alignedBodyAxis, duplicateConstrainedVector, constrainedBodyAxis);

            // Create angle from constraint vector to specified body axis. Set condition to find when this angle exceeds user limit
            IAgCrdnAngle constraintToBodyAngle = CreateAngle((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}To{constrainedBodyVectorName}", duplicateConstrainedVector, duplicateConstrainedBodyVector);
            IAgCrdnCalcScalar constraintToBodyScalar = CreateScalar((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}To{constrainedBodyVectorName}Value", constraintToBodyAngle);
            IAgCrdnCondition constraintToBodyCondition = CreateCondition((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}To{constrainedBodyVectorName}Condition", constraintToBodyScalar, AgECrdnConditionThresholdOption.eCrdnConditionThresholdOptionBelowMax, angleLimit);

            // Create angle from constraint vector to aligned vector and set condition from 0 to 90 deg. This is because the attitude profile is flipped 
            // if the angle is between 90 and 180 deg. 
            IAgCrdnAngle constraintToAlignedAngle = CreateAngle((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}To{alignedVectorName}", duplicateConstrainedVector, duplicateAlignedVector);
            IAgCrdnCalcScalar constraintToAlignedScalar = CreateScalar((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}To{alignedVectorName}Value", constraintToAlignedAngle);
            IAgCrdnCondition constraintToAlignedCondition = CreateCondition((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}To{alignedVectorName}Condition", constraintToAlignedScalar, AgECrdnConditionThresholdOption.eCrdnConditionThresholdOptionInsideMinMax, 0, 90);

            // Create axes aligned with the constraint vector and constrained with the aligned vector.
            IAgCrdnAxes alignedConstrainedAxes = CreateAxes((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}Axes", duplicateConstrainedVector, constrainedBodyAxis, duplicateAlignedVector, alignedBodyAxis);

            // Rotate the new axes by both the positive and negative Angle Offset value.
            IAgCrdnAxes positiveRotatedAxes = CreateAxes((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}Axes_RotatedPositive", alignedConstrainedAxes, alignedBodyAxis, constrainedBodyAxis, angleLimit);
            IAgCrdnAxes negativeRotatedAxes = CreateAxes((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}Axes_RotatedNegative", alignedConstrainedAxes, alignedBodyAxis, constrainedBodyAxis, -angleLimit);

            // Create scheduled axes
            IAgCrdnAxes scheduledAxes;
            double[] alignedAxisVector = GetCartesianArray(alignedBodyAxis);
            double[] constrainedAxisVector = GetCartesianArray(constrainedBodyAxis);

            // If the cross product vector of the aligned body axis and constrained body axis is positive, the axes need to be 
            // positively rotated when the angle between the aligned vector and constrained vector is between 0 deg and 90 deg.
            // If the angle is between 90 and 180, the axes need to be negatively rotated. This is flipped if the cross product
            // is negative.
            if (NormalVectorIsPositive(alignedAxisVector, constrainedAxisVector))
            {
                scheduledAxes = CreateAxes((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}Axes_Scheduled", constraintToAlignedCondition, positiveRotatedAxes, negativeRotatedAxes);
            }
            else
            {
                scheduledAxes = CreateAxes((IAgStkObject)m_referenceSatellite, $"{constrainedVectorName}Axes_Scheduled", constraintToAlignedCondition, negativeRotatedAxes, positiveRotatedAxes);
            }

            // Create scheduled vector to point along alignment vector when constraint is not broken, and hold at constraint when violated.
            IAgCrdnVector alignedScheduledAxis = GetVectorFromAxes((IAgStkObject)m_referenceSatellite, scheduledAxes, alignedBodyAxis);
            IAgCrdnVector scheduledVector = CreateVector((IAgStkObject)m_referenceSatellite, $"{alignedVectorName}Scheduled", constraintToBodyCondition, duplicateAlignedVector, alignedScheduledAxis);

            return scheduledVector;
        }

        private string GetBodyAxis(VectorType category)
        {
            switch (category)
            {
                case VectorType.eAligned:
                    return GetAxisString(new string[] { alignedXTextBox.Text, alignedYTextBox.Text, alignedZTextBox.Text });

                case VectorType.eConstrained:
                    return GetAxisString(new string[] { constrainedXTextBox.Text, constrainedYTextBox.Text, constrainedZTextBox.Text });

                default:
                    throw new Exception("Undefined category");
            }
        }

        private string GetAxisString(string[] vectorArray)
        {

            string axis;
            // Only one axis can contain +/-1. Once you find it, add a negative sign if it's negative.
            try
            {
                axis = m_axes[Array.IndexOf(vectorArray, "1")];
            }
            catch
            {
                axis = m_axes[Array.IndexOf(vectorArray, "-1")];
                axis = "-" + axis;
            }

            return axis;
        }

        private AgSatellite DuplicateObject(AgSatellite originalObject)
        {
            string duplicateName = $"{originalObject.InstanceName }_Reference";
            CheckForExistingObject($"Satellite/{duplicateName}");

            // Duplicate selected object and hide graphics
            AgSatellite duplicate = originalObject.CopyObject(duplicateName) as AgSatellite;

            // Using a connect command, because disabling graphics with object model still leaves a checked box in the object browser.
            string path = m_stkObjectsLibrary.SimplifiedObjectPath(duplicate.Path);
            string commandString = $"Graphics {path} Basic Show Off";

            try
            {
                CommonData.StkRoot.ExecuteCommand(commandString);
            }
            catch
            {
                throw new Exception($"Command failed while disabling {duplicate.InstanceName}'s graphics.");
            }

            return duplicate;
        }

        private void CheckInputVector(IAgCrdnVector inputVector)
        {
            if (VectorIsValid(inputVector) == false)
            {
                throw new Exception($"{((IAgCrdn)inputVector).Name} Vector is not valid.");
            }
        }

        private void CheckForExistingObject(string objectPath)
        {
            // Ask user to overwrite object if it exists
            if (CommonData.StkRoot.ObjectExists(objectPath))
            {
                DialogResult result = MessageBox.Show($"{objectPath} already exists. Overwrite this object?",
                    "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    IAgStkObject existingObject = CommonData.StkRoot.GetObjectFromPath(objectPath);
                    existingObject.Unload();
                }
            }
        }

        private bool VectorIsValid(IAgCrdnVector vector)
        {
            // If vector cannot be evaluated at scenario start (0 EpSec), it's invalid
            IAgCrdnAxes axes = m_referenceSatellite.Vgt.Axes["Body"];

            string dateFormat = CommonData.StkRoot.UnitPreferences.GetCurrentUnitAbbrv("DateFormat");
            string epoch = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", dateFormat, "0");

            IAgCrdnVectorFindInAxesResult result = vector.FindInAxes(epoch, axes);

            return result.IsValid;
        }

        private IAgCrdnVector GetVectorFromObject(IAgStkObject stkObject, string vectorName)
        {
            IAgCrdnVector vector = stkObject.Vgt.Vectors[vectorName];

            return vector;
        }

        private IAgCrdnVector GetVectorFromAxes(IAgStkObject stkObject, IAgCrdnAxes axes, string axis)
        {
            // Get axis by name
            try
            {
                return stkObject.Vgt.Vectors[$"{((IAgCrdn)axes).Name}.{axis}"];
            }
            catch
            {
                throw new Exception("Unable to get axis vector.");
            }
        }

        private IAgCrdnAngle CreateAngle(IAgStkObject stkObject, string name, IAgCrdnVector fromVector, IAgCrdnVector toVector)
        {
            IAgCrdnAngleFactory factory = stkObject.Vgt.Angles.Factory;

            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.Angles, name);

            IAgCrdnAngleBetweenVectors angle = factory.Create(name, "", AgECrdnAngleType.eCrdnAngleTypeBetweenVectors) as IAgCrdnAngleBetweenVectors;
            angle.FromVector.SetVector(fromVector);
            angle.ToVector.SetVector(toVector);

            return angle as IAgCrdnAngle;
        }

        private IAgCrdnCalcScalar CreateScalar(IAgStkObject stkObject, string name, IAgCrdnAngle angle)
        {
            IAgCrdnCalcScalarFactory factory = stkObject.Vgt.CalcScalars.Factory;

            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.CalcScalars, name);

            IAgCrdnCalcScalarAngle scalar = factory.CreateCalcScalarAngle(name, "") as IAgCrdnCalcScalarAngle;
            scalar.InputAngle = angle;
            return scalar as IAgCrdnCalcScalar;
        }

        private IAgCrdnCondition CreateCondition(IAgStkObject stkObject, string name, IAgCrdnCalcScalar scalar, AgECrdnConditionThresholdOption operation, double value)
        {
            IAgCrdnConditionFactory factory = stkObject.Vgt.Conditions.Factory;

            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.Conditions, name);

            IAgCrdnConditionScalarBounds condition = factory.CreateConditionScalarBounds(name, "") as IAgCrdnConditionScalarBounds;
            condition.Scalar = scalar;
            condition.Operation = operation;

            // Min/Max must be IAgQuantity
            IAgQuantity valueQuantity = CommonData.StkRoot.ConversionUtility.NewQuantity("Angle", "deg", value);
            if (operation == AgECrdnConditionThresholdOption.eCrdnConditionThresholdOptionAboveMin)
            {
                condition.SetMinimum(valueQuantity);
            }
            else if (operation == AgECrdnConditionThresholdOption.eCrdnConditionThresholdOptionBelowMax)
            {
                condition.SetMaximum(valueQuantity);
            }

            return condition as IAgCrdnCondition;
        }

        private IAgCrdnCondition CreateCondition(IAgStkObject stkObject, string name, IAgCrdnCalcScalar scalar, AgECrdnConditionThresholdOption operation, double minimum, double maximum)
        {
            IAgCrdnConditionFactory factory = stkObject.Vgt.Conditions.Factory;

            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.Conditions, name);

            IAgCrdnConditionScalarBounds condition = factory.CreateConditionScalarBounds(name, "") as IAgCrdnConditionScalarBounds;
            condition.Scalar = scalar;
            condition.Operation = operation;

            // Min/Max must be IAgQuantity
            IAgQuantity minQuantity = CommonData.StkRoot.ConversionUtility.NewQuantity("Angle", "deg", minimum);
            IAgQuantity maxQuantity = CommonData.StkRoot.ConversionUtility.NewQuantity("Angle", "deg", maximum);

            condition.Set(minQuantity, maxQuantity);

            return condition as IAgCrdnCondition;
        }

        private IAgCrdnAxes CreateAxes(IAgStkObject stkObject, string name, IAgCrdnVector alignedVector, string alignedAxis, IAgCrdnVector constrainedVector, string constrainedAxis)
        {
            IAgCrdnAxesFactory factory = stkObject.Vgt.Axes.Factory;
            double[] alignedArray = GetCartesianArray(alignedAxis);
            double[] constrainedArray = GetCartesianArray(constrainedAxis);

            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.Axes, name);

            IAgCrdnAxesAlignedAndConstrained axes = factory.Create(name, "", AgECrdnAxesType.eCrdnAxesTypeAlignedAndConstrained) as IAgCrdnAxesAlignedAndConstrained;
            axes.AlignmentReferenceVector.SetVector(alignedVector);
            axes.AlignmentDirection.AssignXYZ(alignedArray[0], alignedArray[1], alignedArray[2]);

            axes.ConstraintReferenceVector.SetVector(constrainedVector);
            axes.ConstraintDirection.AssignXYZ(constrainedArray[0], constrainedArray[1], constrainedArray[2]);

            return axes as IAgCrdnAxes;
        }

        private IAgCrdnAxes CreateAxes(IAgStkObject stkObject, string name, IAgCrdnAxes referenceAxes, string alignedAxis, string constrainedAxis, double angle)
        {
            IAgCrdnAxesFactory factory = stkObject.Vgt.Axes.Factory;
            double[] eulerAngles = GetEulerArray(alignedAxis, constrainedAxis, angle);

            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.Axes, name);

            IAgCrdnAxesFixed axes = factory.Create(name, "", AgECrdnAxesType.eCrdnAxesTypeFixed) as IAgCrdnAxesFixed;
            axes.ReferenceAxes.SetAxes(referenceAxes);
            axes.FixedOrientation.AssignEulerAngles(AgEEulerOrientationSequence.e123, eulerAngles[0], eulerAngles[1], eulerAngles[2]);

            return axes as IAgCrdnAxes;
        }

        private IAgCrdnAxes CreateAxes(IAgStkObject stkObject, string name, IAgCrdnCondition satisfactionCondition, IAgCrdnAxes onScheduleAxes, IAgCrdnAxes offScheduleAxes)
        {
            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.Axes, name);

            // No Object Model for Scheduled Axes, so Connect command must be used.
            string commandString = $"VectorTool * {stkObject.ClassName}/{stkObject.InstanceName} Create Axes {name} \"Scheduled\" Schedule" +
                $" \"{((IAgCrdn)satisfactionCondition).Path}.SatisfactionIntervals\" OnSchedule \"{((IAgCrdn)onScheduleAxes).Path}\"" +
                $" UseOffSchedule On OffSchedule \"{((IAgCrdn)offScheduleAxes).Path}\" UseAdditionalCondition Off UseSlew Off";

            try
            {
                CommonData.StkRoot.ExecuteCommand(commandString);
            }
            catch
            {
                throw new Exception($"Command failed while creating {name} Axes.");
            }

            return stkObject.Vgt.Axes[name];

        }

        private IAgCrdnVector CreateVector(IAgStkObject stkObject, string name, IAgCrdnCondition satisfactionCondition, IAgCrdnVector onScheduleVector, IAgCrdnVector offScheduleVector)
        {
            // Check if component exists.
            CheckExistingVgtComponent(stkObject.Vgt.Vectors, name);

            // No Object Model for Scheduled Vector, so Connect command must be used.
            string commandString = $"VectorTool * {stkObject.ClassName}/{stkObject.InstanceName} Create Vector {name} \"Scheduled\" Schedule " +
                $"\"{((IAgCrdn)satisfactionCondition).Path}.SatisfactionIntervals\" OnSchedule \"{((IAgCrdn)onScheduleVector).Path}\" UseOffSchedule On" +
                $" OffSchedule \"{((IAgCrdn)offScheduleVector).Path}\" UseAdditionalCondition Off UseSlew Off";

            try
            {
                CommonData.StkRoot.ExecuteCommand(commandString);
            }
            catch
            {
                throw new Exception($"Command failed while creating {name} Vector.");
            }

            return stkObject.Vgt.Vectors[name];
        }

        private void CheckExistingVgtComponent(dynamic vgtComponentGroup, string name)
        {
            if (vgtComponentGroup.Contains(name))
            {
                DialogResult result = MessageBox.Show($"The component with the name \"{name}\" already exists. Do you want to replace it?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    vgtComponentGroup.Remove(name);
                }
                else
                {
                    throw new Exception("The component with specified name already exists.");
                }
            }
        }

        private void SetAlignedConstrainedAttitude(AgSatellite satellite, IAgCrdnVector alignedVector, string alignedAxis, IAgCrdnVector constrainedVector, string constrainedAxis)
        {
            // Set attitude to Aligned & Constrained
            IAgVeOrbitAttitudeStandard attitude = satellite.Attitude as IAgVeOrbitAttitudeStandard;
            attitude.Basic.SetProfileType(AgEVeProfile.eProfileAlignedAndConstrained);

            double[] alignedBody = GetCartesianArray(alignedAxis);
            double[] constrainedBody = GetCartesianArray(constrainedAxis);

            // Set vectors and body axis
            IAgVeProfileAlignedAndConstrained profile = attitude.Basic.Profile as IAgVeProfileAlignedAndConstrained;
            profile.AlignedVector.ReferenceVector = ((IAgCrdn)alignedVector).Path;
            profile.AlignedVector.Body.AssignXYZ(alignedBody[0], alignedBody[1], alignedBody[2]);

            profile.ConstrainedVector.ReferenceVector = ((IAgCrdn)constrainedVector).Path;
            profile.ConstrainedVector.Body.AssignXYZ(constrainedBody[0], constrainedBody[1], constrainedBody[2]);
        }

        private double[] GetCartesianArray(string axisName)
        {
            switch (axisName)
            {
                case "X":
                    return new double[] { 1, 0, 0 };

                case "Y":
                    return new double[] { 0, 1, 0 };

                case "Z":
                    return new double[] { 0, 0, 1 };

                case "-X":
                    return new double[] { -1, 0, 0 };

                case "-Y":
                    return new double[] { 0, -1, 0 };

                case "-Z":
                    return new double[] { 0, 0, -1 };

                default:
                    throw new Exception("Axis undefined");
            }
        }

        private double[] GetEulerArray(string axis1, string axis2, double value)
        {
            // This method puts the angle value in the direction normal to the 2 axes.

            double[] crossVector = CalculateCrossProduct(GetCartesianArray(axis1), GetCartesianArray(axis2));

            double[] output = new double[3];

            // Replace normal direction with value. 2 directions have value of 0, so multiplying every element by the value
            // will only replace the unit normal direction.
            for (int i = 0; i < crossVector.Length; i++)
            {
                output[i] = Math.Abs(crossVector[i]) * value;
            }

            return output;
        }

        private bool NormalVectorIsPositive(double[] vector1, double[] vector2)
        {
            double[] crossVector = CalculateCrossProduct(vector1, vector2);

            // Get axis direction from cross product and check for negative sign
            string axis = GetAxisString(new string[] { crossVector[0].ToString(), crossVector[1].ToString(), crossVector[2].ToString() });

            if (axis.Contains("-"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private double[] CalculateCrossProduct(double[] vector1, double[] vector2)
        {
            // Cross product formula
            double x = (vector1[1] * vector2[2]) - (vector1[2] * vector2[1]);
            double y = (vector1[2] * vector2[0]) - (vector1[0] * vector2[2]);
            double z = (vector1[0] * vector2[1]) - (vector1[1] * vector2[0]);

            return new double[] { x, y, z };
        }

        private void ShowAttitudeGraphics(IAgCrdnVector alignedVector, IAgCrdnVector constrainedVector, IAgCrdnAxes bodyAxes, IAgCrdnAngle offAxisAngle)
        {
            // Show aligned vector and constrained vector
            DisplayElement(AgEGeometricElemType.eVectorElem, (IAgCrdn)alignedVector, Color.Yellow);
            DisplayElement(AgEGeometricElemType.eVectorElem, (IAgCrdn)constrainedVector, Color.LawnGreen);

            // Show body axes
            DisplayElement(AgEGeometricElemType.eAxesElem, (IAgCrdn)bodyAxes, Color.RoyalBlue);

            // Display angle from constraint vector to Body axis on original satellite to show limited atttiude.
            DisplayElement(AgEGeometricElemType.eAngleElem, (IAgCrdn)offAxisAngle, Color.White);
        }

        private void DisplayElement(AgEGeometricElemType type, IAgCrdn element, Color color)
        {
            IAgVOVector vectorGraphics = m_selectedObject.VO.Vector;
            IAgVORefCrdn elementGraphics;

            // If element exists, get by name. otherwise add it.
            try
            {
                elementGraphics = vectorGraphics.RefCrdns.GetCrdnByName(type, element.QualifiedPath);
            }
            catch
            {
                elementGraphics = vectorGraphics.RefCrdns.Add(type, element.QualifiedPath);
            }

            elementGraphics.Visible = true;
            elementGraphics.LabelVisible = true;
            elementGraphics.Color = color;

            try
            {
                ((IAgVORefCrdnAngle)elementGraphics).AngleValueVisible = true;
            }
            catch
            {
                // element is not an angle
            }
        }

        private void UpdateProgress(int value)
        {
            progressBar.Value = value;
            progressBar.Refresh();
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text == "")
            {
                textBox.Text = "0";
            }
        }
    }
}
