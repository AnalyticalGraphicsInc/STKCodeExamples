//========================================================//
//     Copyright 2012, Analytical Graphics, Inc.          //
//========================================================//

namespace AGI.EphemerisFileReader.Plugin.Examples.CSharp
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using AgAsPlugin;
    using AGI.Plugin;
    using Microsoft.Win32;
    using System.Text;

    /// <summary>
    /// This example reads the "ECF Position Velocity" data report style from STK.
    /// There is an example "Aircraft1 ECF Position Velocity.txt" file that can be used to test.
    ///
    ///                                                                                                16 Feb 2012 12:53:33
    ///Aircraft-Aircraft1:  ECF Position & Velocity
    ///
    ///
    ///       Time (UTCG)             x (km)         y (km)          z (km)      vx (km/sec)    vy (km/sec)    vz (km/sec)
    ///------------------------    -----------    ------------    -----------    -----------    -----------    -----------
    ///16 Feb 2012 17:00:00.000    1112.305671    -4768.000721    4078.774342       0.064113      -0.018443      -0.038782
    ///16 Feb 2012 17:11:16.675    1155.651887    -4780.321097    4052.394203       0.035475       0.049247       0.047655
    /// </summary>
	[Guid("29121F44-6B3D-4c95-82C4-784C09052B27")]
    [ProgId("AGI.EphemerisFileReader.Plugin.Examples.CSharp.Example")]
	[ClassInterface(ClassInterfaceType.None)]
	public class Example : IAgAsEphemFileReaderPlugin
	{
        private const string m_extension = ".txt";
        private IAgUtPluginSite m_site;

        public bool Init(IAgUtPluginSite Site)
        {
            m_site = Site;

            return true;
        }

        public void Register(AgAsEphemFileReaderPluginResultReg Result)
        {
            if (Result != null)
            {
                Result.FormatID = "EphemerisFileReader.Plugin.Examples.CSharp.Example";
                Result.Name = "CSharp ECF Report Reader";
                Result.AddFileExtension(m_extension);
            }
        }

        public void ReadEphemeris(AgAsEphemFileReaderPluginResultEphem Result)
        {
            if (Result != null)
            {
                Result.CentralBody = "Earth";
                Result.CoordinateSystem = "Fixed";
                Result.InterpolationMethod = AgEAsEphemInterpolationMethod.eAsEphemInterpolationMethodLagrange;
                Result.InterpolationOrder = 5;
                Result.CovarianceRepresentation = AgEAsCovRep.eAsCovRepRIC;
                Result.SetUnits(AgEAsEphemFileDistanceUnit.eAsEphemFileDistanceUnitKilometer, AgEAsEphemFileTimeUnit.eAsEphemFileTimeUnitSecond);

                string[] fileLines = File.ReadAllLines(Result.Filename);
                bool readHeader = false;
                bool readSeparator = false;
                int numPointsRead = 0;

                // Go through each line in the data report and parse it
                foreach (string fileLine in fileLines)
                {
                    string[] elements = fileLine.Split(new string[] { "   " }, StringSplitOptions.RemoveEmptyEntries);

                    if (elements.Length == 7)
                    {
                        if (!readHeader)
                        {
                            readHeader = true;
                            continue;
                        }

                        if (!readSeparator)
                        {
                            readSeparator = true;
                            continue;
                        }

                        DateTime time = DateTime.Parse(elements[0], CultureInfo.InvariantCulture);
                        double x = double.Parse(elements[1], CultureInfo.InvariantCulture);
                        double y = double.Parse(elements[2], CultureInfo.InvariantCulture);
                        double z = double.Parse(elements[3], CultureInfo.InvariantCulture);

                        double vx = double.Parse(elements[4], CultureInfo.InvariantCulture);
                        double vy = double.Parse(elements[5], CultureInfo.InvariantCulture);
                        double vz = double.Parse(elements[6], CultureInfo.InvariantCulture);

                        bool added = Result.AddEphemerisOnDate(
                            AgEUtTimeScale.eUtTimeScaleUTC, time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second,
                            x, y, z, vx, vy, vz, null);

                        if (added)
                        {
                            numPointsRead++;
                        }
                        else
                        {
                            m_site.Message(AgEUtLogMsgType.eUtLogMsgWarning, "Could not add point #" + numPointsRead);
                        }
                    }
                }

                string message = string.Format("ECF Data Report Reader read {0} points", numPointsRead);
                m_site.Message(AgEUtLogMsgType.eUtLogMsgInfo, message);
            }
        }

        public void ReadMetaData(AgAsEphemFileReaderPluginResultData Result)
        {
            if (Result != null)
            {
                Result.AddMetaData("My Metadata", "Custom");
            }
        }

        public void Verify(AgAsEphemFileReaderPluginResultVerify Result)
        {
            if (Result != null)
            {
                if (Path.GetExtension(Result.Filename) != m_extension)
                {
                    Result.IsValid = false;
                    Result.Message = "File must have a ." + m_extension + " extension";
                    return;
                }

                // For instance, let's reject text files that we don't recognize as the ECF Position & Velocity Data Report
                using (StreamReader streamReader = new StreamReader(Result.Filename))
                {
                    try
                    {
                        // Read past the time stamp line
                        streamReader.ReadLine();

                        string secondLine = streamReader.ReadLine();
                        if (!secondLine.Contains("ECF Position & Velocity"))
                        {
                            Result.IsValid = false;
                            Result.Message = "File doesn't seem to be an ECF Position & Velocity Data Report";
                        }
                    }
                    catch (Exception)
                    {
                        Result.IsValid = false;
                        Result.Message = "Error reading file header";
                    }
                }
            }
        }

        public void Free()
        {
            m_site = null;
        }

        #region Registration functions
        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        [ComRegisterFunction]
        [ComVisible(false)]
        public static void RegisterFunction(Type t)
        {
            RemoveOtherVersions(t);
        }

        /// <summary>
        /// Called when the assembly is unregistered for use from COM.
        /// </summary>
        /// <param name="t">The type exposed to COM.</param>
        [ComUnregisterFunctionAttribute]
        [ComVisible(false)]
        public static void UnregisterFunction(Type t)
        {
            // Do nothing.
        }

        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// Eliminates the other versions present in the registry for
        /// this type.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        public static void RemoveOtherVersions(Type t)
        {
            try
            {
                using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                {
                    StringBuilder guidString = new StringBuilder("{");
                    guidString.Append(t.GUID.ToString());
                    guidString.Append("}");
                    using (RegistryKey guidKey = clsidKey.OpenSubKey(guidString.ToString()))
                    {
                        if (guidKey != null)
                        {
                            using (RegistryKey inproc32Key = guidKey.OpenSubKey("InprocServer32", true))
                            {
                                if (inproc32Key != null)
                                {
                                    string currentVersion = t.Assembly.GetName().Version.ToString();
                                    string[] subKeyNames = inproc32Key.GetSubKeyNames();
                                    if (subKeyNames.Length > 1)
                                    {
                                        foreach (string subKeyName in subKeyNames)
                                        {
                                            if (subKeyName != currentVersion)
                                            {
                                                inproc32Key.DeleteSubKey(subKeyName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore all exceptions...
            }
        }
        #endregion
    }
}
//========================================================//
//     Copyright 2012, Analytical Graphics, Inc.          //
//========================================================//