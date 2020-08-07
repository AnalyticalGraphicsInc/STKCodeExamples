using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReentryCalculator.Utility
{

    class TLE
    {
        string filePath;
        string row1;
        string row2;
        string satNumber;
        string inclination;
        string eccentricity;
        string revNumber;
        string meanMotion;
        int epochYear;
        double epochDays;
        int mon, day, hr, minute;
        double sec;
        DateTime tleEpoch;

        public TLE (string filePath)
        {
            this.filePath = filePath;

            ReadRows();
        }

        public TLE(DateTime epoch, string sscN, string inc, string ecc, string orbitN, string mMotion)
        {
            tleEpoch = epoch;
            satNumber = sscN;
            inclination = inc;
            eccentricity = ecc;
            revNumber = orbitN;
            meanMotion = mMotion;
        }

        private void ReadRows()
        {
            StreamReader tleFile = new StreamReader(filePath);
            while (tleFile.Peek() > -1)
            {
                string line = tleFile.ReadLine();
                if (line.StartsWith("1"))
                {
                    row1 = line;
                    satNumber = line.Substring(2, 5);
                    epochYear = Convert.ToInt16(line.Substring(18, 2));
                    if (epochYear < 57)
                        epochYear = epochYear + 2000;
                    else
                        epochYear = epochYear + 1900;

                    epochDays = Convert.ToDouble(line.Substring(20, 12)); //Convert.ToDouble(linedata[6]);
                    days2mdhms(epochYear, epochDays, out mon, out day, out hr, out minute, out sec);
                    tleEpoch = new DateTime(epochYear, mon, day, hr, minute, Convert.ToInt16(sec));
                }
                if (line.StartsWith("2"))
                {
                    row2 = line;
                    inclination = Convert.ToString(Convert.ToDouble(line.Substring(8, 8)));
                    eccentricity = Convert.ToString(Convert.ToDouble(line.Substring(26, 7)) * 0.0000001);
                    revNumber = Convert.ToString(Convert.ToDouble(line.Substring(63, 5)));
                    meanMotion = line.Substring(52, 11);
                }
            }
        }

        private void days2mdhms(int year, double days, out int mon, out int day, out int hr, out int minute, out double sec)
        {
            /* -----------------------------------------------------------------------------
            *
            *                           procedure days2mdhms
            *
            *  this procedure converts the day of the year, days, to the equivalent month
            *    day, hour, minute and second.
            *
            *  algorithm     : set up array for the number of days per month
            *                  find leap year - use 1900 because 2000 is a leap year
            *                  loop through a temp value while the value is < the days
            *                  perform int conversions to the correct day and month
            *                  convert remainder into h m s using type conversions
            *
            *  author        : david vallado                  719-573-2600    1 mar 2001
            *
            *  inputs          description                    range / units
            *    year        - year                           1900 .. 2100
            *    days        - julian day of the year         0.0  .. 366.0
            *
            *  outputs       :
            *    mon         - month                          1 .. 12
            *    day         - day                            1 .. 28,29,30,31
            *    hr          - hour                           0 .. 23
            *    min         - minute                         0 .. 59
            *    sec         - second                         0.0 .. 59.999
            *
            *  locals        :
            *    dayofyr     - day of year
            *    temp        - temporary extended values
            *    inttemp     - temporary int value
            *    i           - index
            *    lmonth[12]  - int array containing the number of days per month
            *
            *  coupling      :
            *    none.
            * --------------------------------------------------------------------------- */

            int i, inttemp, dayofyr;
            double temp;
            int[] lmonth = new int[13] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            dayofyr = (int)Math.Floor(days);
            /* ----------------- find month and day of month ---------------- */
            if ((year % 4) == 0)
                lmonth[2] = 29;

            i = 1;
            inttemp = 0;
            while ((dayofyr > inttemp + lmonth[i]) && (i < 12))
            {
                inttemp = inttemp + lmonth[i];
                i = i + 1;
            }
            mon = i;
            day = dayofyr - inttemp;

            /* ----------------- find hours minutes and seconds ------------- */
            temp = (days - dayofyr) * 24.0;
            hr = Convert.ToInt16(Math.Floor(temp));
            temp = (temp - hr) * 60.0;
            minute = Convert.ToInt16(Math.Floor(temp));
            sec = (temp - minute) * 60.0;
        }  //  days2mdhms

        public string GetFilePath()
        {
            return filePath;
        }

        public DateTime GetTleEpoch()
        {
            return tleEpoch;
        }

        public string GetSatNumber()
        {
            return satNumber;
        }

        public string GetInclination()
        {
            return inclination;
        }

        public string GetEccentricity()
        {
            return eccentricity;
        }

        public string GetRevNumber()
        {
            return revNumber;
        }

        public string GetMeanMotion()
        {
            return meanMotion;
        }
    }
}
