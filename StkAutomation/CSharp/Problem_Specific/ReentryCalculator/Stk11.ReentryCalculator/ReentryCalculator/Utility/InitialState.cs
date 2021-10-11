using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReentryCalculator.Utility
{
    class InitialState
    {
        string _epoch;
        string _cartesianPosX;
        string _cartesianPosY;
        string _cartesianPosZ;
        string _cartesianVelX;
        string _cartesianVelY;
        string _cartesianVelZ;

        public string Epoch
        {
            get { return _epoch; }
            set { _epoch = value; }
        }

        public string CartesianPosX
        {
            get { return _cartesianPosX; }
            set { _cartesianPosX = value; }
        }

        public string CartesianPosY
        {
            get { return _cartesianPosY; }
            set { _cartesianPosY = value; }
        }

        public string CartesianPosZ
        {
            get { return _cartesianPosZ; }
            set { _cartesianPosZ = value; }
        }

        public string CartesianVelX
        {
            get { return _cartesianVelX; }
            set { _cartesianVelX = value; }
        }

        public string CartesianVelY
        {
            get { return _cartesianVelY; }
            set { _cartesianVelY = value; }
        }

        public string CartesianVelZ
        {
            get { return _cartesianVelZ; }
            set { _cartesianVelZ = value; }
        }
    }
}
