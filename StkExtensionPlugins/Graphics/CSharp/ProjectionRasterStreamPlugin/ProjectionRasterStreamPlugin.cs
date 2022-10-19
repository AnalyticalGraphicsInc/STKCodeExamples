using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AGI.STKGraphics.Plugins;
using Microsoft.Win32;

namespace ProjectionRasterStreamPlugin
{
	[Guid("5B1AC88E-3ADA-437c-9F40-1B8184ECB485")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ProjectionRasterStreamPlugin.CSharp")]
    public class ProjectionRasterStreamPlugin : IAgStkGraphicsPluginProjectionStream,
                                                IAgStkGraphicsPluginRasterStream
    {
        public ProjectionRasterStreamPlugin()
        {
            // Set default values
            NearPlane = 1.0;
            FarPlane = 10000;
            FieldOfViewHorizontal = 0.5;
            FieldOfViewVertical = 0.5;
            Dates = new List<double>();
            Positions = new List<Array>();
            Orientations = new List<Array>();
        }

        #region IAgStkGraphicsPluginProjectionStream Members

        bool IAgStkGraphicsPluginProjectionStream.OnGetFirstProjection(AGI.STKUtil.IAgDate Time, IAgStkGraphicsPluginProjectionStreamContext pContext)
        {
            pContext.NearPlane = NearPlane;
            pContext.FarPlane = FarPlane;
            pContext.FieldOfViewHorizontal = FieldOfViewHorizontal;
            pContext.FieldOfViewVertical = FieldOfViewVertical;

            ProjectionPositionOrientation result = EvaluateProjectionPositionOrientation(double.Parse(Time.Format("epSec")), 0, Dates.Count);
            Array xyz = result.Position;
            Array quat = result.Orientation;

            pContext.SetPosition(ref xyz);
            pContext.SetOrientation(ref quat);

            return true;
        }

        bool IAgStkGraphicsPluginProjectionStream.OnGetNextProjection(AGI.STKUtil.IAgDate Time, AGI.STKUtil.IAgDate NextTime, IAgStkGraphicsPluginProjectionStreamContext pContext)
        {
            pContext.NearPlane = NearPlane;
            pContext.FarPlane = FarPlane;
            pContext.FieldOfViewHorizontal = FieldOfViewHorizontal;
            pContext.FieldOfViewVertical = FieldOfViewVertical;

            ProjectionPositionOrientation result = EvaluateProjectionPositionOrientation(double.Parse(Time.Format("epSec")), 0, Dates.Count);
            Array xyz = result.Position;
            Array quat = result.Orientation;

            pContext.SetPosition(ref xyz);
            pContext.SetOrientation(ref quat);

            return true;
        }

        #endregion

        #region IAgStkGraphicsPluginRasterStream Members

        bool IAgStkGraphicsPluginRasterStream.OnGetRasterAttributes(IAgStkGraphicsPluginRasterStreamAttributes pAttributes)
        {
            // Set gifProvider for the raster
            gifProvider = new GifProvider(RasterPath);

            // Assign the raster's attributes
            pAttributes.Width = gifProvider.Size.Width;
            pAttributes.Height = gifProvider.Size.Height;
            pAttributes.RasterFormat = AgEStkGraphicsPluginRasterFormat.eStkGraphicsPluginRasterFormatBgra;
            pAttributes.RasterType = AgEStkGraphicsPluginRasterType.eStkGraphicsPluginRasterTypeUnsignedByte;

            return true;
        }

        bool IAgStkGraphicsPluginRasterStream.OnGetNextRaster(AGI.STKUtil.IAgDate Time, AGI.STKUtil.IAgDate NextTime, IAgStkGraphicsPluginRasterStreamContext pContext)
        {
            if (m_LastTime == null)
                m_LastTime = Time.Subtract("sec", 1.0);

            // Only update if animating
            if (Time.OLEDate > m_LastTime.OLEDate)
            {
                pContext.RasterAsBitmap.SetBitmap(gifProvider.NextFrame().GetHbitmap());
                m_LastTime = Time;
            }
            else if (Time.OLEDate < m_LastTime.OLEDate)
            {
                pContext.RasterAsBitmap.SetBitmap(gifProvider.PreviousFrame().GetHbitmap());
                m_LastTime = Time;
            }

            return true;
        }

        #endregion

        private ProjectionPositionOrientation EvaluateProjectionPositionOrientation(double searchDate, int startIndex, int searchLength)
        {
            // Find the midpoint of the length
            int midpoint = startIndex + (searchLength / 2);

            // Base cases
            if (Dates[startIndex] == searchDate)
            {
                return new ProjectionPositionOrientation(Positions[startIndex], Orientations[startIndex]);
            }
            if (Dates[midpoint] == searchDate)
            {
                return new ProjectionPositionOrientation(Positions[midpoint], Orientations[midpoint]);
            }
            if (searchLength == 3)
            {
                if (searchDate < Dates[midpoint])
                    return InterpolatePositionAndOrientation(midpoint - 1, midpoint, searchDate);
                else
                    return InterpolatePositionAndOrientation(midpoint, midpoint + 1, searchDate);
            }
            if (searchLength == 2)
            {
                InterpolatePositionAndOrientation(startIndex, startIndex + 1, searchDate);
            }
            if (searchLength < 2)
            {
                // This will only occur if animating backwards, so try to interpolate with the lower index first.
                if (startIndex - 1 > 0)
                    return InterpolatePositionAndOrientation(startIndex, startIndex + 1, searchDate);
                else if (startIndex + 1 < Dates.Count)
                    return InterpolatePositionAndOrientation(startIndex - 1, startIndex, searchDate);
                else
                    return new ProjectionPositionOrientation(Positions[startIndex], Orientations[startIndex]);
            }

            // Normal case: binary search
            if (searchDate < Dates[midpoint])
            {
                return EvaluateProjectionPositionOrientation(searchDate, startIndex, midpoint - startIndex);
            }
            else
            {
                return EvaluateProjectionPositionOrientation(searchDate, midpoint + 1, startIndex + searchLength - (midpoint + 1));
            }
        }

        /// <summary>
        /// Interpolates a <typeparamref name="ProjectionPositionOrientation"/> that would suffice at a <paramref name="desiredDate"/> between
        /// Dates[<paramref name="lowerIndex"/>] and Dates[<paramref name="higherIndex"/>].
        /// </summary>
        /// <param name="lowerIndex">The index reprenting the position in Dates that serves as the lower bound of interpolation.</param>
        /// <param name="higherIndex">The index reprenting the position in Dates that serves as the upper bound of interpolation.</param>
        /// <param name="desiredDate">The date to interpolate for.</param>
        /// <returns>A <typeparamref name="ProjectionPositionOrientation"/> containing the interpolated position and orientation.</returns>
        private ProjectionPositionOrientation InterpolatePositionAndOrientation(int lowerIndex, int higherIndex, double desiredDate)
        {
            double range = Dates[higherIndex] - Dates[lowerIndex];
            double locationInRange = desiredDate - Dates[lowerIndex];
            double ratio = locationInRange / range;

            return new ProjectionPositionOrientation(
                InterpolatePosition(Positions[lowerIndex], Positions[higherIndex], ratio),
                InterpolateOrientation(Orientations[lowerIndex], Orientations[higherIndex], ratio));
        }

        /// <summary>
        /// This function will calculate an interpolated cartesian position according to the linear interpolation (LERP) formula
        /// such that its location is an amount <paramref name="t"/> between <paramref name="pos1"/> and <paramref name="pos2"/>.
        /// </summary>
        /// <param name="pos1">The first IAgCartesian3Vector. Serves as the lower position of the interpolation.</param>
        /// <param name="pos2">The second IAgCartesian3Vector. Serves as the uppoer position of the interpolation.</param>
        /// <param name="t">The amount of interpolation (i.e. the desired amount between <paramref name="pos1"/> and <paramref name="pos2"/> to calculate).</param>
        /// <returns>An <typeparamref name="Array"/> of cartesian position components that is an amount <paramref name="t"/> between <paramref name="pos1"/> and <paramref name="pos2"/>.</returns>
        private static Array InterpolatePosition(Array pos1, Array pos2, double t)
        {
            return new object[]
            {
                (double)pos1.GetValue(0) + ((double)pos2.GetValue(0) - (double)pos1.GetValue(0)) * t,
                (double)pos1.GetValue(1) + ((double)pos2.GetValue(1) - (double)pos1.GetValue(1)) * t,
                (double)pos1.GetValue(2) + ((double)pos2.GetValue(2) - (double)pos1.GetValue(2)) * t,
            };
        }

        /// <summary>
        /// This function will calculate an interpolated quaternion according to the spherical linear interpolation (SLERP) formula
        /// such that its values lay an amount <paramref name="t"/> between <paramref name="q1"/> and <paramref name="q2"/>.
        /// </summary>
        /// <param name="q1">The first IAgOrientation. Serves as the lower quaterion of the interpolation.</param>
        /// <param name="q2">the second IAgOrientation. Serves as the upper quaterion of the interpolation.</param>
        /// <param name="t">The amount of interpolation (i.e. the desired amount between <paramref name="q1"/> and <paramref name="q1"/> to calculate).</param>
        /// <returns>An <typeparamref name="Array"/> of quaterion components that lay an amount <paramref name="t"/> between <paramref name="q1"/> and <paramref name="q2"/>.</returns>
        private static Array InterpolateOrientation(Array q1, Array q2, double t)
        {
            double EPS = 0.001;

            double theta, cosineTheta, sineTheta, scale0, scale1;
            double[] qi = new double[4];

            double q1x = (double)q1.GetValue(0),
                   q1y = (double)q1.GetValue(1),
                   q1z = (double)q1.GetValue(2),
                   q1w = (double)q1.GetValue(3);

            double q2x = (double)q2.GetValue(0),
                   q2y = (double)q2.GetValue(1),
                   q2z = (double)q2.GetValue(2),
                   q2w = (double)q2.GetValue(3);

            // Do a linear interpolation between two quaternions (0 <= t <= 1).
            cosineTheta = q1x * q2x + q1y * q2y + q1z * q2z + q1w * q2w;  // dot product

            if (cosineTheta < 0)
            {
                cosineTheta = -cosineTheta;
                qi[0] = -q2x;
                qi[1] = -q2y;
                qi[2] = -q2z;
                qi[3] = -q2w;
            }
            else
            {
                qi[0] = q2x;
                qi[1] = q2y;
                qi[2] = q2z;
                qi[3] = q2w;
            }

            if ((1 - cosineTheta) <= EPS)// If the quaternions are really close, do a simple linear interpolation.
            {
                scale0 = 1 - t;
                scale1 = t;
            }
            else
            {
                // Otherwise SLERP.
                theta = (float)Math.Acos(cosineTheta);
                sineTheta = (float)Math.Sin(theta);
                scale0 = (float)Math.Sin((1 - t) * theta) / sineTheta;
                scale1 = (float)Math.Sin(t * theta) / sineTheta;
            }

            // Calculate interpolated quaternion:
            double resultX, resultY, resultZ, resultW;
            resultX = scale0 * q1x + scale1 * qi[0];
            resultY = scale0 * q1y + scale1 * qi[1];
            resultZ = scale0 * q1z + scale1 * qi[2];
            resultW = scale0 * q1w + scale1 * qi[3];

            return new object[] { resultX, resultY, resultZ, resultW };
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

        public double NearPlane { get; set; }
        public double FarPlane { get; set; }
        public double FieldOfViewHorizontal { get; set; }
        public double FieldOfViewVertical { get; set; }
        public IList<double> Dates { get; set; }
        public IList<Array> Positions { get; set; }
        public IList<Array> Orientations { get; set; }

        public string RasterPath { get; set; }
        private GifProvider gifProvider;
        private AGI.STKUtil.IAgDate m_LastTime;
    }
}
