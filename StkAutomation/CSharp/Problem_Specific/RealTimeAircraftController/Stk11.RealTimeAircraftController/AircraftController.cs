using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AGI.STKObjects;

namespace RealtimeACcontrol
{
    public static class AircraftController
    {
        private static object _stkApplication;
        private static AgStkObjectRoot _root;
        private static IAgScenario _scen;

        public static double Heading { get; set; }
        public static double Pitch { get; set; }
        public static double Roll { get; set; }
        public static double Lat { get; set; }
        public static double Lon { get; set; }
        public static double Alt { get; set; }
        public static double PitchRate { get; set; }
        public static double RollRate { get; set; }
        public static double MaxVel { get; set; }
        public static double Vel { get; set; }
        private static bool DefaultsSet { get; set; }

        private static string AircraftName { get; set; }
        public static void Initialize()
        {
            if (!DefaultsSet)
            {
                SetInitialConditions();
            }

            _stkApplication = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application");
            _root = _stkApplication.GetType().InvokeMember("Personality2", System.Reflection.BindingFlags.GetProperty, null, _stkApplication, null) as AgStkObjectRoot;

            if (!_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eAircraft).Contains(AircraftName))
            {
                _root.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, AircraftName);
            }

            _root.ExecuteCommand("SetUnits / EPOCHSEC");//   %sets date format to epochsec
            _root.ExecuteCommand($"Realtime */Aircraft/{AircraftName} SetProp");
            _root.ExecuteCommand($"SetAttitude */Aircraft/{AircraftName} RealTime Extrapolate 100 200");

            try
            {
                _root.ExecuteCommand($"SetAttitude */Aircraft/{AircraftName} DATAREFERENCE Fixed QUAT 0 0 0 1 \"Aircraft/{AircraftName} FlightPath\"");
                _root.ExecuteCommand($"VO */Aircraft/{AircraftName} Model File \"C:\\Program Files\\AGI\\STK 11\\STKData\\VO\\Models\\Air\\rq-1a_predator.mdl\"");
            }
            catch 
            {
            }

            _scen = (IAgScenario)_root.CurrentScenario;
            _scen.Epoch = "Today";
            _root.PlayForward();
        }

        public static void SetInitialConditions()
        {
            AircraftName = "Aircraft";
            // Set some default values for performance
            Vel = .00077;
            MaxVel = .00097;
            PitchRate = 1.5;
            RollRate = 5.0;
            // Set initial starting position
            Lat = 37.71;
            Lon = -122.3;
            Alt = 1000;
            // Set initial attitude
            Heading = 0.0;
            Pitch = 0.0;
            Roll = 0.0;
            DefaultsSet = true;
        }

        public static void SetInitialConditions(string aircraftName, double initialVelocityKps, double maximumVelocityKps, double pitchRate, double rollRate, 
            double initialLatDeg, double initialLonDeg, double initialAltM, double initialHeading, double initialPitch, double initialRoll)
        {
            AircraftName = aircraftName;

            // Set some default values for performance
            Vel = .00077;
            MaxVel = .00097;
            PitchRate = 1.5;
            RollRate = 5.0;
            // Set initial starting position
            Lat = 37.71;
            Lon = -122.3;
            Alt = 1000;
            // Set initial attitude
            Heading = 0.0;
            Pitch = 0.0;
            Roll = 0.0;
            DefaultsSet = true;
        }

        public static void ExecuteStepInTime()
        {
            var i = 1;
            var lastTime = _root.CurrentTime;
            while (i > 0)
            {
                var currentTime = _root.CurrentTime;

                var delta = lastTime - currentTime;

                NewPosition(delta, Lat, Lon, Alt);

                if (i == 2)
                {
                    _root.PlayForward();
                }

                var command1 = $"SetPosition */Aircraft/{AircraftName} LLA \"{currentTime}\" {Lat} {Lon} {Alt}";

                //Transfer attitude information to STK
                var pitchS = -Pitch / Math.PI * 180;
                var rollS = -Roll;
                var command2 = $"AddAttitude */Aircraft/{AircraftName} EULER \"{currentTime}\" 321 0.0 {pitchS} {rollS}";

                Roll = Roll / 1.2;
                lastTime = currentTime;

                try
                {
                    _root.ExecuteCommand(command1);
                    _root.ExecuteCommand(command2);
                }
                catch { }

                i += 1;
                Thread.Sleep(10);
            }
        }

        private static void NewPosition(double deltaT, double lastLat, double lastLon, double lastAlt)
        {
            var dlat = Math.Cos(Pitch) * Math.Sin(Heading) * Vel;
            var dlon = Math.Cos(Pitch) * Math.Cos(Heading) * Vel;
            var dalt = Math.Sin(Pitch) * 100 * PitchRate / 1.5;
            Lat = lastLat + deltaT * dlat;
            Lon = lastLon + deltaT * dlon;
            Alt = lastAlt + deltaT * dalt;
        }

        public static void PitchUp()
        {
            Pitch = Pitch - PitchRate * Math.PI / 180.0;
        }

        public static void PitchDown()
        {
            Pitch = Pitch + PitchRate * Math.PI / 180.0;
        }

        public static void TurnRight()
        {
            Roll = Roll - RollRate;
            if (Roll < -45)
            {
                Roll = -45;
            }
            Heading = Heading - RollRate * Math.PI / 180.0;
        }

        public static void TurnLeft()
        {
            Roll = Roll + RollRate;
            if (Roll > 45)
            {
                Roll = 45;
            }

            Heading = Heading + RollRate * Math.PI / 180.0;
        }

        public static void PauseAnimation()
        {
            _root.Pause();
        }
    }
}
