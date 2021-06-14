using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerConstellationBuilder.Routines
{
    class FlightDynamics
    {
        const double omegaEarth = 0.000072921150; // rad/s
        const double mu = 3.986004418e5; // km^3/s^2
        const double earthRadius = 6378.1363; // km
        const double J2 = 0.0010826267;

        

        public static double AnomPeriodToSMajAx (double period, double eccen, double inclination, ErrorType error)
        {
            double epsilon = 1.0e-12;

            ErrorType rc = ErrorType.NoError;
            double diffPeriod, semimajorAxis, newPeriod;

            // First guess is the unperturbed semimajor axis length
            semimajorAxis = Math.Pow(mu * Math.Pow( period / (2 * Math.PI), 2.0), (1.0 / 3.0));

            if (eccen < 0 || eccen >= 1.0)
            {
                error = ErrorType.BadEcc;
                semimajorAxis = 0.0;
            }

            if ((error) == ErrorType.NoError)
            {
                newPeriod = SMajAxToAnomPeriod(semimajorAxis, eccen, inclination, rc);
                diffPeriod = period - newPeriod;

                // Perform an iterative solution to get the correct semimajor axis length
                while (Math.Abs(diffPeriod / period) > epsilon && rc == ErrorType.NoError)
                {
                    semimajorAxis += 2.0 * semimajorAxis * diffPeriod / (3.0 * newPeriod);
                    newPeriod = SMajAxToAnomPeriod(semimajorAxis, eccen, inclination, rc);
                    diffPeriod = period - newPeriod;
                }

                error = rc;
            }

            error = rc;

            return (semimajorAxis);
        }

        public static double SMajAxToAnomPeriod(double semimajorAxis, double eccen, double inclination, ErrorType error)
        {
            ErrorType rc = ErrorType.NoError;
            double period, anomMeanMotion;

            if (semimajorAxis < 0.0)
            {
                error = ErrorType.BadSemimajorAxis;
                period = 0.0;
            }
            else if (eccen < 0 || eccen >= 1.0)
            {
                error = ErrorType.BadEcc;
                period = 0.0;
            }
            else
            {
                anomMeanMotion = SMajAxToAnomMeanMotn(semimajorAxis, eccen, inclination, rc);

                period = (2 * Math.PI) / anomMeanMotion;
                error = rc;
            }

            return (period);
        }

        public static double SMajAxToAnomMeanMotn(double semimajorAxis, double eccen, double inclination, ErrorType error)
        {
            ErrorType rc = ErrorType.NoError;
            double meanMotion,
                         sini,
                         p,
                         temp;


            if (semimajorAxis < 0.0)
            {
                error = ErrorType.BadSemimajorAxis;
                meanMotion = 0.0;
            }
            else if (eccen < 0 || eccen >= 1.0)
            {
                error = ErrorType.BadEcc;
                meanMotion = 0.0;
            }
            else
            {
                meanMotion = Math.Sqrt(mu / (semimajorAxis * semimajorAxis * semimajorAxis));

                // cdav delete next stuff and make anomlalistic (doesn't affect repeatgt)

                sini = Math.Sin(inclination);
                temp = 1.0 - eccen * eccen;
                p = semimajorAxis * temp / earthRadius;

                meanMotion -= 1.5 * J2 * meanMotion * Math.Sqrt(temp) * (1.5 * sini * sini - 1.0) / (p * p);

                error = rc;
            }

            return (meanMotion);
        }

        public static double SMajAxToAnomPeriod(double semimajorAxis, double eccen, double inclination, double eqRadius, ErrorType error)
        {
            ErrorType rc = ErrorType.NoError;
            double period,
                         anomMeanMotion;

            if (semimajorAxis < 0.0)
            {
                error = ErrorType.BadSemimajorAxis;
                period = 0.0;
            }
            else if (eccen < 0 || eccen >= 1.0)
            {
                error = ErrorType.BadEcc;
                period = 0.0;
            }
            else
            {
                anomMeanMotion = SMajAxToAnomMeanMotn(semimajorAxis, eccen, inclination, rc);

                period = 2 * Math.PI / anomMeanMotion;
                error = rc;
            }

            return (period);
        }

    }
}
