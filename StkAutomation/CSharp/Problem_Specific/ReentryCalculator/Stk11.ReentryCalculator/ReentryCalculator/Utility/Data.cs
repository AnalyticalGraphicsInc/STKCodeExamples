using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReentryCalculator.Utility
{
    class Data
    {
        double _mass;
        double _dragArea;
        double _cd;
        double _sunArea;
        double _cr;

        public Data (double mass, double dragArea, double cd, double sunArea, double cr)
        {
            _mass = mass;
            _dragArea = dragArea;
            _cd = cd;
            _sunArea = sunArea;
            _cr = cr;
        }

        public double Mass
        {
            get { return _mass; }
            set { _mass = value; }
        }

        public double DragArea
        {
            get { return _dragArea; }
            set { _dragArea = value; }
        }

        public double Cd
        {
            get { return _cd; }
            set { _cd = value; }
        }

        public double SunArea
        {
            get { return _sunArea; }
            set { _sunArea = value; }
        }

        public double Cr
        {
            get { return _cr; }
            set { _cr = value; }
        }
    }
}
