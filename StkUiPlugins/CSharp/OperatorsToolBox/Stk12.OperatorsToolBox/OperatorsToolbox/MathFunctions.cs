using System;

namespace OperatorsToolbox
{
    public static class MathFunctions
    {
        public static double ArrayMin(Array thisArray)
        {
            double min = 9999999;
            double current = 0.0;
            try
            {
                foreach (var item in thisArray)
                {
                    current = Double.Parse(item.ToString());
                    if (current < min)
                    {
                        min = current;
                    }
                }
            }
            catch (Exception)
            {
                min = -1;
            }
            return min;
        }

        public static double ArrayMax(Array thisArray)
        {
            double max = -9999999;
            double current = -9999999;
            try
            {
                foreach (var item in thisArray)
                {
                    current = Double.Parse(item.ToString());
                    if (current > max)
                    {
                        max = current;
                    }
                }
            }
            catch (Exception)
            {
                max = -1;
            }
            return max;
        }

        public static double ArrayMinAbs(Array thisArray)
        {
            double min = 9999999;
            double current = 0.0;
            try
            {
                foreach (var item in thisArray)
                {
                    current = Double.Parse(item.ToString());
                    if (Math.Abs(current) < min)
                    {
                        min = Math.Abs(current);
                    }
                }
            }
            catch (Exception)
            {
                min = -1;
            }

            return min;
        }
    }
}
