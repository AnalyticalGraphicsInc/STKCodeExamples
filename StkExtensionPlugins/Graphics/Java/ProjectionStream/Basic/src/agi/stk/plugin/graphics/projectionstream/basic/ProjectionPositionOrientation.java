package agi.stk.plugin.graphics.projectionstream.basic;

public class ProjectionPositionOrientation
{
    private Object[] m_Position;
    private Object[] m_Orientation;

    public ProjectionPositionOrientation(Object[] position, Object[] rotation)
    {
        this.m_Position = position;
        this.m_Orientation = rotation;
    }

    public Object[] getPosition()
    {
        return this.m_Position;
    }
    
    public void setPosition(Object[] value)
    {
        this.m_Position = value;
    }

    public Object[] getOrientation()
    {
        return this.m_Orientation;
    }
    
    public void setOrientation(Object[] value)
    {
        this.m_Orientation = value;
    }
}
