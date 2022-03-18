package agi.stk.plugin.graphics.projectionstream.basic;

import java.util.*;
import agi.core.*;
import agi.stkutil.*;
import agi.stk.plugin.graphics.*;
import agi.stk.plugin.util.AgEUtLogMsgType;

public class JavaExample
extends agi.stk.plugin.graphics.rasterstream.basic.JavaExample
implements IAgStkGraphicsPluginProjectionStream
{
	public double				m_NearPlane;
	public double				m_FarPlane;
	public double				m_FieldOfViewHorizontal;
	public double				m_FieldOfViewVertical;
	public ArrayList<Double>	m_Dates;
	public ArrayList<Double[]>	m_Positions;
	public ArrayList<Double[]>	m_Orientations;

	public JavaExample()
	{
        // Set default values
        m_NearPlane = 1.0;
        m_FarPlane = 10000;
        m_FieldOfViewHorizontal = 0.5;
        m_FieldOfViewVertical = 0.5;
        m_Dates = new ArrayList<Double>();
        m_Positions = new ArrayList<Double[]>();
        m_Orientations = new ArrayList<Double[]>();
	}

	//=============================================================
	// For IDispatch access of configuration through Plugin Driver
	//=============================================================
	public void setNearPlane(AgVariant value) 
	throws AgCoreException
	{
		this.m_NearPlane = ((Double)value.getObject()).doubleValue();
		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "NearPlane set with size "+this.m_Dates.size());
	}
	public Object getNearPlane()
	{
		return new Double(this.m_NearPlane);
	}
	public void setFarPlane(AgVariant value)
	throws AgCoreException
	{
		this.m_FarPlane = ((Double)value.getObject()).doubleValue();
		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "FarPlane set with size "+this.m_Dates.size());
	}
	public Object getFarPlane()
	{
		return new Double(this.m_FarPlane);
	}
	public void setFieldOfViewHorizontal(AgVariant value)
	throws AgCoreException
	{
		this.m_FieldOfViewHorizontal = ((Double)value.getObject()).doubleValue();
		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "FieldOfViewHorizontal set with size "+this.m_Dates.size());
	}
	public Object getFieldOfViewHorizontal()
	{
		return new Double(this.m_FieldOfViewHorizontal);
	}
	public void setFieldOfViewVertical(AgVariant value)
	throws AgCoreException
	{
		this.m_FieldOfViewVertical = ((Double)value.getObject()).doubleValue();
		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "FieldOfViewVertical set with size "+this.m_Dates.size());
	}
	public Object getFieldOfViewVertical()
	{
		return new Double(this.m_FieldOfViewVertical);
	}
	
	public void setDates(AgVariant dates)
	{
		try
		{
			if((dates.getVariantType() & AgVariantTypes.VT_ARRAY) == AgVariantTypes.VT_ARRAY)
			{
				AgSafeArray sa = dates.getSafeArray();
				if(sa.isOneDimensional())
				{
					Object[] a = sa.getObjectArray();
					if(a instanceof AgVariant[])
					{
						AgVariant[] values = (AgVariant[])a;
						for(int i=0; i<values.length; i++)
						{
							AgVariant var2 = values[i];
							this.m_Dates.add(var2.getDouble());
						}
					}
				}
			}
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "dates set with size "+this.m_Dates.size());
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public void setPositions(AgVariant positions)
	{
		try
		{
			if((positions.getVariantType() & AgVariantTypes.VT_ARRAY) == AgVariantTypes.VT_ARRAY)
			{
				AgSafeArray sa = positions.getSafeArray();
				if(sa.isTwoDimensional())
				{
					Object[][] values = sa.getJavaObject2DArray();
					int rowCount = values.length;
					for(int i=0; i<rowCount; i++)
					{
						int colCount = values[i].length;
						Double[] row = new Double[colCount];
						for(int j=0; j<colCount; j++)
						{
							Object value = values[i][j];
							if(value instanceof Double)
							{
								row[j] = (Double)value;
							}
							else
							{
								throw new Exception("was not double but "+value.toString());
							}
						}
						this.m_Positions.add(row);
					}
				}
			}
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "positions set with size "+this.m_Dates.size());
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public void setOrientations(AgVariant orientations)
	{
		try
		{
			if((orientations.getVariantType() & AgVariantTypes.VT_ARRAY) == AgVariantTypes.VT_ARRAY)
			{
				AgSafeArray sa = orientations.getSafeArray();
				if(sa.isTwoDimensional())
				{
					Object[][] values = sa.getJavaObject2DArray();
					int rowCount = values.length;
					for(int i=0; i<rowCount; i++)
					{
						int colCount = values[i].length;
						Double[] row = new Double[colCount];
						for(int j=0; j<colCount; j++)
						{
							Object value = values[i][j];
							if(value instanceof Double)
							{
								row[j] = (Double)value;
							}
							else
							{
								throw new Exception("was not double but "+value.toString());
							}
						}
						this.m_Orientations.add(row);
					}
				}
			}
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "orientations set with size "+this.m_Dates.size());
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	//==============================
	// IAgStkGraphicsPluginProjectionStream
	//==============================

	public boolean onGetFirstProjection(IAgDate time, IAgStkGraphicsPluginProjectionStreamContext context)
	throws AgCoreException
	{
		boolean result = false;
		try
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetFirstProjection ENTER");
	
			context.setNearPlane(this.m_NearPlane);
	        context.setFarPlane(this.m_FarPlane);
	        context.setFieldOfViewHorizontal(this.m_FieldOfViewHorizontal);
	        context.setFieldOfViewVertical(this.m_FieldOfViewVertical);
	
	        StringBuilder dataMsg = new StringBuilder();
	
	        double eTime = Double.parseDouble(time.format("epSec"));
	        dataMsg.append("eTime="+eTime);
	        
	        ProjectionPositionOrientation ppo = null;
	        ppo = evaluateProjectionPositionOrientation(eTime, 0, m_Dates.size());

	        Object[] xyz = ppo.getPosition();
	        dataMsg.append(" xyz [");
	        dataMsg.append(" "+xyz[0]);
	        dataMsg.append(" "+xyz[1]);
	        dataMsg.append(" "+xyz[2]);
	        dataMsg.append("]");
	
	        Object[] quat = ppo.getOrientation();
	        dataMsg.append(" quat [");
	        dataMsg.append(" "+quat[0]);
	        dataMsg.append(" "+quat[1]);
	        dataMsg.append(" "+quat[2]);
	        dataMsg.append(" "+quat[3]);
	        dataMsg.append("]");
	
	        message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, 0, "JavaExample.onGetNextProjection", 202, dataMsg.toString());
	
			context.setPosition(xyz);
	        context.setOrientation(quat);
	
	        message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetFirstProjection EXIT");
	
	        result = true;
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
		
		return result;
	}

	public boolean onGetNextProjection(IAgDate time, IAgDate nextTime, IAgStkGraphicsPluginProjectionStreamContext context)
	throws AgCoreException
	{
		boolean result = false;
		try
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetNextProjection ENTER");
	
			context.setNearPlane(this.m_NearPlane);
	        context.setFarPlane(this.m_FarPlane);
	        context.setFieldOfViewHorizontal(this.m_FieldOfViewHorizontal);
	        context.setFieldOfViewVertical(this.m_FieldOfViewVertical);
	
	        StringBuilder dataMsg = new StringBuilder();
	        
	        double eTime = Double.parseDouble(time.format("epSec"));
	        dataMsg.append("eTime="+eTime);
	        
	        double eNextTime = Double.parseDouble(nextTime.format("epSec"));
	        dataMsg.append(" eNextTime="+eNextTime);
	
	        ProjectionPositionOrientation ppo = null;
	        ppo = evaluateProjectionPositionOrientation(eTime, 0, m_Dates.size());
	
	        Object[] xyz = ppo.getPosition();
	        dataMsg.append(" xyz [");
	        dataMsg.append(" "+xyz[0]);
	        dataMsg.append(" "+xyz[1]);
	        dataMsg.append(" "+xyz[2]);
	        dataMsg.append("]");
	
	        Object[] quat = ppo.getOrientation();
	        dataMsg.append(" quat [");
	        dataMsg.append(" "+quat[0]);
	        dataMsg.append(" "+quat[1]);
	        dataMsg.append(" "+quat[2]);
	        dataMsg.append(" "+quat[3]);
	        dataMsg.append("]");
	
	        message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, 0, "JavaExample.onGetNextProjection", 202, dataMsg.toString());
	
			context.setPosition(xyz);
	        context.setOrientation(quat);
	
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetNextProjection EXIT");
	
			result = true;
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}

		return result;
	}

    private ProjectionPositionOrientation evaluateProjectionPositionOrientation(double searchDate, int startIndex, int searchLength)
    throws AgCoreException
    {
		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "evaluateProjectionPositionOrientation ENTER");

		// Find the midpoint of the length
        int midpoint = startIndex + (searchLength / 2);

        // Base cases
        if (m_Dates.get(startIndex) == searchDate)
        {
            return new ProjectionPositionOrientation(m_Positions.get(startIndex), m_Orientations.get(startIndex));
        }
        if (m_Dates.get(midpoint) == searchDate)
        {
            return new ProjectionPositionOrientation(m_Positions.get(midpoint), m_Orientations.get(midpoint));
        }
        if (searchLength == 3)
        {
            if (searchDate < m_Dates.get(midpoint))
                return interpolatePositionAndOrientation(midpoint - 1, midpoint, searchDate);
            else
                return interpolatePositionAndOrientation(midpoint, midpoint + 1, searchDate);
        }
        if (searchLength == 2)
        {
            return interpolatePositionAndOrientation(startIndex, startIndex + 1, searchDate);
        }
        if (searchLength < 2)
        {
            // This will only occur if animating backwards, so try to interpolate with the lower index first.
            if (startIndex - 1 > 0)
                return interpolatePositionAndOrientation(startIndex, startIndex + 1, searchDate);
            else if (startIndex + 1 < m_Dates.size())
                return interpolatePositionAndOrientation(startIndex - 1, startIndex, searchDate);
            else
                return new ProjectionPositionOrientation(m_Positions.get(startIndex), m_Orientations.get(startIndex));
        }

        ProjectionPositionOrientation p = null;
        
        // Normal case: binary search
        if (searchDate < m_Dates.get(midpoint))
        {
            p = evaluateProjectionPositionOrientation(searchDate, startIndex, midpoint - startIndex);
        }
        else
        {
            p = evaluateProjectionPositionOrientation(searchDate, midpoint + 1, startIndex + searchLength - (midpoint + 1));
        }

		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "evaluateProjectionPositionOrientation EXIT");
		
		return p;
    }

    /**
    * Interpolates a ProjectionPositionOrientation that would suffice at a desiredDate between
    * lowerIndex and higherIndex.
    * 
    * @param lowerIndex The index reparenting the position in Dates that serves as the lower bound of interpolation.
    * @param higherIndex The index reparenting the position in Dates that serves as the upper bound of interpolation.
    * @param desiredDate The date to interpolate for.
    * @return A ProjectionPositionOrientation containing the interpolated position and orientation.
     * @throws AgCoreException 
    */
    private ProjectionPositionOrientation interpolatePositionAndOrientation(int lowerIndex, int higherIndex, double desiredDate)
    throws AgCoreException
    {
		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "interpolatePositionAndOrientation ENTER");

		double range = m_Dates.get(higherIndex) - m_Dates.get(lowerIndex);
        double locationInRange = desiredDate - m_Dates.get(lowerIndex);
        double ratio = locationInRange / range;

        ProjectionPositionOrientation p = null;
        p = new ProjectionPositionOrientation(
            interpolatePosition(m_Positions.get(lowerIndex), m_Positions.get(higherIndex), ratio),
            interpolateOrientation(m_Orientations.get(lowerIndex), m_Orientations.get(higherIndex), ratio));
        
		message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "interpolatePositionAndOrientation EXIT");
		
		return p;
    }

    /**
    * This function will calculate an interpolated cartesian position according to the linear interpolation (LERP) formula
    * such that its location is an amount t between pos1 and pos2.
    * @param pos1 The first IAgCartesian3Vector. Serves as the lower position of the interpolation.
    * @param pos2 The second IAgCartesian3Vector. Serves as the uppoer position of the interpolation.
    * @param t The amount of interpolation (i.e. the desired amount between pos1 and pos2 to calculate).
     * @throws AgCoreException 
    * @returns An Array of cartesian position components that is an amount t between pos1 and pos2.
    */
    private static AgVariant[] interpolatePosition(Double[] pos1, Double[] pos2, double t) 
    throws AgCoreException
    {
        return new AgVariant[]
        {
            new AgVariant(pos1[0] + (pos2[0] - pos1[0]) * t),
            new AgVariant(pos1[1] + (pos2[1] - pos1[1]) * t),
            new AgVariant(pos1[2] + (pos2[2] - pos1[2]) * t)
        };
    }

    /**
    * This function will calculate an interpolated quaternion according to the spherical linear interpolation (SLERP) formula
    * such that its values lay an amount t between q1 and q2.
    * @param q1 The first IAgOrientation. Serves as the lower quaterion of the interpolation.
    * @param q2 The second IAgOrientation. Serves as the upper quaterion of the interpolation.
    * @param t The amount of interpolation (i.e. the desired amount between q1 and q1 to calculate).
    * @return An Array of quaterion components that lay an amount t between q1 and q2.
     * @throws AgCoreException 
    */
    private static AgVariant[] interpolateOrientation(Double[] q1, Double[] q2, double t) 
    throws AgCoreException
    {
        double EPS = 0.001;

        double theta, cosineTheta, sineTheta, scale0, scale1;
        double[] qi = new double[4];

        double q1x = q1[0],
               q1y = q1[1],
               q1z = q1[2],
               q1w = q1[3];

        double q2x = q2[0],
               q2y = q2[1],
               q2z = q2[2],
               q2w = q2[3];

        // Do a linear interpolation between two quaternions (0 <= t <= 1).
        cosineTheta = q1x * q2x + q1y * q2y + q1z * q2z + q1w * q2w;  // dot product

        if (cosineTheta < 0)
        {
            cosineTheta = -cosineTheta;
            qi[0] = -q2x;
            qi[1] = -q2y;
            qi[2] = -q2z;
            qi[3] = -q2w;
        }
        else
        {
            qi[0] = q2x;
            qi[1] = q2y;
            qi[2] = q2z;
            qi[3] = q2w;
        }

        if ((1 - cosineTheta) <= EPS)// If the quaternions are really close, do a simple linear interpolation.
        {
            scale0 = 1 - t;
            scale1 = t;
        }
        else
        {
            // Otherwise SLERP.
            theta = (float)Math.acos(cosineTheta);
            sineTheta = (float)Math.sin(theta);
            scale0 = (float)Math.sin((1 - t) * theta) / sineTheta;
            scale1 = (float)Math.sin(t * theta) / sineTheta;
        }

        // Calculate interpolated quaternion:
        double resultX, resultY, resultZ, resultW;
        resultX = scale0 * q1x + scale1 * qi[0];
        resultY = scale0 * q1y + scale1 * qi[1];
        resultZ = scale0 * q1z + scale1 * qi[2];
        resultW = scale0 * q1w + scale1 * qi[3];

        return new AgVariant[] { new AgVariant(resultX), new AgVariant(resultY), new AgVariant(resultZ), new AgVariant(resultW) };
    }
}
