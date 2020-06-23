using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payload
{
    public class ThePayload
    {
        public double ConfigurationMetric { get; set; }

        public double TargetCrossSection_SqMeters { get; set; }

        public ThePayload()
        {
            ConfigurationMetric = 1e12;
            TargetCrossSection_SqMeters = 5;
        }

        public double ComputeTargetDetectionProbability(double relPosX, double relPosY, double relPosZ, double sunPosX, double sunPosY, double sunPosZ)
        {
            double detectability = ConfigurationMetric * Math.Pow(TargetCrossSection_SqMeters,3)
                / (relPosX * relPosX + relPosY * relPosY + relPosZ * relPosZ);

            List<double> toTargetVector = new List<double>() { relPosX, relPosY, relPosZ };
            List<double> sunVector = new List<double>() { sunPosX, sunPosY, sunPosZ };

            double angleBetween = AngleBetween(toTargetVector, sunVector, false);

            if (angleBetween < (Math.PI / 2))
            {
                detectability = detectability * Math.Sin(angleBetween);
            }

            return detectability;
        }


        public double AngleBetween(List<double> u, List<double> v, bool returndegrees)
        {
            double toppart = 0;
            for (int d = 0; d < 3; d++) toppart += u[d] * v[d];

            double u2 = 0; //u squared
            double v2 = 0; //v squared
            for (int d = 0; d < 3; d++)
            {
                u2 += u[d] * u[d];
                v2 += v[d] * v[d];
            }

            double bottompart = 0;
            bottompart = Math.Sqrt(u2 * v2);

            double rtnval = Math.Acos(toppart / bottompart);
            if (returndegrees) rtnval *= 360.0 / (2 * Math.PI);
            return rtnval;
        }
    }
}
