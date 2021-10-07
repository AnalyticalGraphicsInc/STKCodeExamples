using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.FacilityCreator
{
    public static class FacilityCreatorFunctions
    {
        public static IAgStkObject AttachFacilityRadar(IAgStkObject parent, string radarName, RadarParams rParams)
        {
            IAgStkObject sensorObj = CreatorFunctions.GetCreateSensor(parent, radarName);
            IAgSensor sensor = sensorObj as IAgSensor;
            sensor.Graphics.IsObjectGraphicsVisible = false;
            IAgSnSimpleConicPattern pattern = sensor.Pattern as IAgSnSimpleConicPattern;
            pattern.ConeAngle = Double.Parse(rParams.HalfAngle);
            IAgSnPtFixed pointing = sensor.Pointing as IAgSnPtFixed;
            pointing.Orientation.AssignAzEl(Double.Parse(rParams.Az), Double.Parse(rParams.El), AGI.STKUtil.AgEAzElAboutBoresight.eAzElAboutBoresightRotate);

            IAgAccessConstraintCollection constraints = sensor.AccessConstraints;
            IAgAccessCnstrMinMax elConstraint;
            IAgAccessCnstrMinMax azConstraint;
            IAgAccessCnstrMinMax rangeConstraint;
            IAgAccessCnstrAngle solarExConstraint;

            elConstraint = CreatorFunctions.GetElCnst(constraints);
            CreatorFunctions.SetCnstMinMax(elConstraint, Double.Parse(rParams.MinEl), Double.Parse(rParams.MaxEl));
            azConstraint = CreatorFunctions.GetAzCnst(constraints);
            CreatorFunctions.SetCnstMinMax(azConstraint, Double.Parse(rParams.MinAz), Double.Parse(rParams.MaxAz));
            rangeConstraint = CreatorFunctions.GetRangeCnst(constraints);
            CreatorFunctions.SetCnstMinMax(rangeConstraint, Double.Parse(rParams.MinRange), Double.Parse(rParams.MaxRange));
            solarExConstraint = CreatorFunctions.GetSunExCnst(constraints);
            solarExConstraint.Angle = rParams.SolarExAngle;

            SetRadarGraphics(sensor);

            return sensorObj;
        }

        public static IAgStkObject AttachFacilityOptical(IAgStkObject parent, string radarName, OpticalParams rParams)
        {
            IAgStkObject sensorObj = CreatorFunctions.GetCreateSensor(parent, radarName);
            IAgSensor sensor = sensorObj as IAgSensor;
            sensor.Graphics.IsObjectGraphicsVisible = false;
            IAgSnSimpleConicPattern pattern = sensor.Pattern as IAgSnSimpleConicPattern;
            pattern.ConeAngle = Double.Parse(rParams.HalfAngle);
            IAgSnPtFixed pointing = sensor.Pointing as IAgSnPtFixed;
            pointing.Orientation.AssignAzEl(Double.Parse(rParams.Az), Double.Parse(rParams.El), AGI.STKUtil.AgEAzElAboutBoresight.eAzElAboutBoresightRotate);

            IAgAccessConstraintCollection constraints = sensor.AccessConstraints;
            IAgAccessCnstrMinMax elConstraint;
            IAgAccessCnstrMinMax azConstraint;
            IAgAccessCnstrMinMax rangeConstraint;
            IAgAccessCnstrMinMax sunElConstraint;
            IAgAccessCnstrAngle lunExConstraint;

            elConstraint = CreatorFunctions.GetElCnst(constraints);
            CreatorFunctions.SetCnstMinMax(elConstraint, Double.Parse(rParams.MinEl), Double.Parse(rParams.MaxEl));
            azConstraint = CreatorFunctions.GetAzCnst(constraints);
            CreatorFunctions.SetCnstMinMax(azConstraint, Double.Parse(rParams.MinAz), Double.Parse(rParams.MaxAz));

            sunElConstraint = CreatorFunctions.GetSunElCnst(constraints);
            CreatorFunctions.SetCnstMinMax(sunElConstraint, -90, Double.Parse(rParams.SunElAngle));

            rangeConstraint = CreatorFunctions.GetRangeCnst(constraints);
            CreatorFunctions.SetCnstMinMax(rangeConstraint, Double.Parse(rParams.MinRange), Double.Parse(rParams.MaxRange));

            lunExConstraint = CreatorFunctions.GetLunExCnst(constraints);
            lunExConstraint.Angle = rParams.LunarExAngle;

            SetOpticalGraphics(sensor);

            return sensorObj;
        }

        public static void SetOpticalGraphics(IAgSensor sensor)
        {
            try
            {
                sensor.Graphics.Projection.UseConstraints = true;
                sensor.Graphics.Projection.EnableConstraint("ElevationAngle");
                sensor.Graphics.Projection.EnableConstraint("AzimuthAngle");
                sensor.Graphics.Projection.EnableConstraint("LOSLunarExclusion");
                CommonData.StkRoot.ExecuteCommand("Animate * Refresh");
            }
            catch (Exception)
            {

            }
        }

        public static void SetRadarGraphics(IAgSensor sensor)
        {
            try
            {
                sensor.Graphics.Projection.UseConstraints = true;
                sensor.Graphics.Projection.EnableConstraint("ElevationAngle");
                sensor.Graphics.Projection.EnableConstraint("AzimuthAngle");
                sensor.Graphics.Projection.EnableConstraint("LOSSunExclusion");
                CommonData.StkRoot.ExecuteCommand("Animate * Refresh");
            }
            catch (Exception)
            {

            }
        }
    }
}
