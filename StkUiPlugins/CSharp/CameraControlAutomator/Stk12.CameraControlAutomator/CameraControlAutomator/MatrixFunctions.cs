using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControlAutomator
{
    class MatrixFunctions
    {
        #region Matrix Functions
        // Function to return single axis rotation matrices:
        public static double[,] RotateAbout(int axis, double angle, string unit)
        {
            // Create output array:
            double[,] rotationMatrix;
            rotationMatrix = new double[3, 3];

            // Get angle multiplier based on unit:
            double multiplier = 1;
            if (unit.Equals("deg"))
            {
                multiplier = Math.PI / 180;
            }

            // Change formulation based on rotation axis:
            switch (axis)
            {
                case 1:
                    rotationMatrix[0, 0] = 1; rotationMatrix[0, 1] = 0; rotationMatrix[0, 2] = 0;
                    rotationMatrix[1, 0] = 0; rotationMatrix[1, 1] = Math.Cos(angle * multiplier); rotationMatrix[1, 2] = Math.Sin(angle * multiplier);
                    rotationMatrix[2, 0] = 0; rotationMatrix[2, 1] = -Math.Sin(angle * multiplier); rotationMatrix[2, 2] = Math.Cos(angle * multiplier);
                    return rotationMatrix;

                case 2:
                    rotationMatrix[0, 0] = Math.Cos(angle * multiplier); rotationMatrix[0, 1] = 0; rotationMatrix[0, 2] = -Math.Sin(angle * multiplier);
                    rotationMatrix[1, 0] = 0; rotationMatrix[1, 1] = 1; rotationMatrix[1, 2] = 0;
                    rotationMatrix[2, 0] = Math.Sin(angle * multiplier); rotationMatrix[2, 1] = 0; rotationMatrix[2, 2] = Math.Cos(angle * multiplier);
                    return rotationMatrix;

                case 3:
                    rotationMatrix[0, 0] = Math.Cos(angle * multiplier); rotationMatrix[0, 1] = Math.Sin(angle * multiplier); rotationMatrix[0, 2] = 0;
                    rotationMatrix[1, 0] = -Math.Sin(angle * multiplier); rotationMatrix[1, 1] = Math.Cos(angle * multiplier); rotationMatrix[1, 2] = 0;
                    rotationMatrix[2, 0] = 0; rotationMatrix[2, 1] = 0; rotationMatrix[2, 2] = 1;
                    return rotationMatrix;

                default:
                    return rotationMatrix;
            }
        }

        // Function to return skew-symmetric matrix:
        public static double[,] SkewSymmetric(double[] vector)
        {
            double[,] skewSymmetricMatrix;
            skewSymmetricMatrix = new double[3, 3];

            // Assign elements of this matrix:
            skewSymmetricMatrix[0, 0] = 0; skewSymmetricMatrix[0, 1] = -vector[2]; skewSymmetricMatrix[0, 2] = vector[1];
            skewSymmetricMatrix[1, 0] = vector[2]; skewSymmetricMatrix[1, 1] = 0; skewSymmetricMatrix[1, 2] = -vector[0];
            skewSymmetricMatrix[2, 0] = -vector[1]; skewSymmetricMatrix[2, 1] = vector[0]; skewSymmetricMatrix[2, 2] = 0;
            return skewSymmetricMatrix;
        }

        // Function to take 3x3 matrix determinant:
        public static double GetDeterminant(double[,] matrix)
        {
            // Initialize determinant value, cofactor value, subDeterminant value:
            double determinant = 0;
            double cofactor = 0;
            double subDeterminant = 0;

            // Loop across top row:
            for (int i = 0; i < 3; i++)
            {
                // Get cofactor:
                cofactor = matrix[0, i] * Math.Pow(-1, i);

                // Get determinant of lower submatrix:
                if (i == 0)
                {
                    subDeterminant = matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1];
                }
                if (i == 1)
                {
                    subDeterminant = matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0];
                }
                if (i == 2)
                {
                    subDeterminant = matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0];
                }
                determinant = determinant + cofactor * subDeterminant;
            }
            return determinant;
        }

        // Function to multiply two 3x3 matrices:
        public static double[,] MatrixMultiply(double[,] matrix1, double[,] matrix2)
        {
            // Initialize output matrix:
            double[,] matrix = new double[3, 3];

            // Perform multiplication:
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        matrix[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            // Return matrix:
            return matrix;
        }

        public static double[] RotateVector(double[,] rotationMatrix, double[] vector)
        {
            double[] newVector = new double[3];
            for (int i = 0; i < 3; i++)
            {
                newVector[i] = 0;
                for (int j = 0; j < 3; j++)
                {
                    newVector[i] += rotationMatrix[i, j] * vector[j];
                }
            }
            return newVector;

        }
        #endregion

        public static List<double> GetNewAngles(double[,] dcm)
        {
            List<double> angles = new List<double>();
            double yaw = Math.Atan2(dcm[0, 1], dcm[0, 0]);
            double pitch = Math.Asin(-1 * dcm[0, 2]);
            double roll = Math.Atan2(dcm[1, 2], dcm[2, 2]);

            //double yaw = Math.Atan(dcm[2, 0] / (-1*dcm[2, 1]));
            //double pitch = Math.Acos(dcm[2, 2]);
            //double roll = Math.Atan(dcm[0, 2] / dcm[1, 2]);

            angles.Add(yaw);
            angles.Add(pitch);
            angles.Add(roll);


            return angles;


        }
        public static double[,] Creat3x3Identity()
        {
            double[,] identity = new double[3, 3];
            identity[0, 0] = 1;
            identity[1, 1] = 1;
            identity[2, 2] = 1;
            identity[0, 1] = 0;
            identity[0, 2] = 0;
            identity[1, 0] = 0;
            identity[1, 2] = 0;
            identity[2, 0] = 0;
            identity[2, 1] = 0;

            return identity;
        }


    }
}
