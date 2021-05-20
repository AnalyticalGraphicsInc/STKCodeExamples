using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReentryCalculator.Utility
{
    class Uncertainty
    {
        double _intrack;
        double _radial;
        double _crosstrack;
        double _intrackRate;
        double _radialRate;
        double _crosstrackRate;

        public Uncertainty(double intrack, double crosstrack, double radial, double intrackRate, double radialRate, double crosstrackRate)
        {
            _intrack = intrack;
            _radial = radial;
            _crosstrack = crosstrack;
            _intrackRate = intrackRate;
            _radialRate = radialRate;
            _crosstrackRate = crosstrackRate;
        }

        public double IntrackPos
        {
            get { return _intrack; }
        }

        public double RadialPos
        {
            get { return _radial; }
        }

        public double CrosstrackPos
        {
            get { return _crosstrack; }
        }

        public double IntrackRate
        {
            get { return _intrackRate; }
        }

        public double RadialRate
        {
            get { return _radialRate; }
        }

        public double CrosstrackRate
        {
            get { return _crosstrackRate; }
        }

        public double[] GetRandomDeviation()
        {
            double[] currentDeviation = new double[6];
            Random rand;
            double u1, u2, randStdNormal;
            double intrackPosDev, radialPosDev, crosstrackPosDev, intrackVelDev, radialVelDev, crosstrackVelDev;

            rand = new Random();
            u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            u2 = 1.0 - rand.NextDouble();
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            intrackPosDev = _intrack * randStdNormal; //random normal(mean,stdDev^2)

            //rand = new Random();
            u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            u2 = 1.0 - rand.NextDouble();
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            radialPosDev = _radial * randStdNormal; //random normal(mean,stdDev^2)

            //rand = new Random();
            u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            u2 = 1.0 - rand.NextDouble();
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            crosstrackPosDev = _crosstrack * randStdNormal; //random normal(mean,stdDev^2)

            //rand = new Random();
            u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            u2 = 1.0 - rand.NextDouble();
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            intrackVelDev = _intrackRate * randStdNormal; //random normal(mean,stdDev^2)

            //rand = new Random();
            u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            u2 = 1.0 - rand.NextDouble();
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            radialVelDev = _radialRate * randStdNormal; //random normal(mean,stdDev^2)

            //rand = new Random();
            u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            u2 = 1.0 - rand.NextDouble();
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            crosstrackVelDev = _crosstrackRate * randStdNormal; //random normal(mean,stdDev^2)

            currentDeviation[0] = intrackPosDev;
            currentDeviation[1] = radialPosDev;
            currentDeviation[2] = crosstrackPosDev;
            currentDeviation[3] = intrackVelDev;
            currentDeviation[4] = radialVelDev;
            currentDeviation[5] = crosstrackVelDev;

            return currentDeviation;
        }

    }
}
