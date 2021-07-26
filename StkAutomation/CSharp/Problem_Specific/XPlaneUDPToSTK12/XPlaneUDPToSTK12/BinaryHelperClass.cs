using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPlaneToSTK
{
    public class BinaryHelperClass
    {
        public string BinaryStringToValue(string binaryString)
        {
            // First I have to split the full 4 byte binary string that i am passing to the function into 3 seperate sections
            // for example:  0 10000101 11101100100000000000000
            // The first one is the sign of the number (0 is positive, 1 is negative), the second part is the biased exponent
            // and the third is the mantissa

            int numberSign = Convert.ToInt32(binaryString.Substring(0, 1));
            string biasedExponent = binaryString.Substring(1, 8);
            string mantissa = binaryString.Substring(9, 23);

            double exponent = ConvertExponentToDouble(biasedExponent);
            double significand = ConvertMantissaToDouble(mantissa);

            // Create value. 
            string value = ((numberSign * -2 + 1) * significand * Math.Pow(2, exponent)).ToString();

            return value;
        }

        private double ConvertExponentToDouble(string exponentString)
        {
            double exponentDouble = 0;
            for (int i = 0; i < 8; i++)
            {
                exponentDouble += (Convert.ToDouble(exponentString.Substring(i, 1)) * Math.Pow(2, (7 - i)));
            }
            exponentDouble -= 127;

            return exponentDouble;
        }

        private double ConvertMantissaToDouble(string mantissaString)
        {
            // Now work through the mantissa
            double mantissaDouble = 1;
            for (int i = 0; i < 23; i++)
            {
                mantissaDouble += (Convert.ToDouble(mantissaString.Substring(i, 1)) * Math.Pow(2, (i + 1) * -1));
            }

            return mantissaDouble;
        }

        public string ConvertToBinary(double RunningTotal)
        {
            int c;
            //double RunningTotal = 246;
            string NumberString = "";

            for (c = 1; c <= 8; c++)
            {
                if (RunningTotal >= Math.Pow(2, (8 - c)))
                {
                    NumberString += "1";
                    RunningTotal -= Math.Pow(2, (8 - c));
                }
                else
                {
                    NumberString += "0";
                }
            }
            return NumberString;
        }
    }
}
