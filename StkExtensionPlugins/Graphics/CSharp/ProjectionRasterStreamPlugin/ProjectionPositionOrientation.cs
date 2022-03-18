using System;
using System.Collections.Generic;
using System.Text;
using AGI.STKUtil;

namespace ProjectionRasterStreamPlugin
{
    public class ProjectionPositionOrientation
    {
        public ProjectionPositionOrientation(Array position, Array rotation)
        {
            m_Position = position;
            m_Orientation = rotation;
        }

        public Array Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Array Orientation
        {
            get { return m_Orientation; }
            set { m_Orientation = value; }
        }

        private Array m_Position;
        private Array m_Orientation;
    }
}
