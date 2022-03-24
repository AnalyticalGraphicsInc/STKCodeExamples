//=====================================================//
//  Copyright 2008, Analytical Graphics, Inc.          //
//=====================================================//

#region Spherical SRP Model Description
//-----------------------------------------------------------------------------------
// Intended use: Implements a spherical SRP model that duplicates the built
//      in model in HPOP.  It only has one parameter to estimate (Cr).  
//      You also have the choice of which frame to do the computation in.
//
//      You can compare the built in model with the plugin implementation by 
//      configuring two satellites, one with each model. Set the SRP configuration
//      inputs to be the same and propagate both of them. Then difference the two
//      sets of ephemeris.
//
//	Special Note:  ODTK option for "diffuse reflecting sphere" should NOT 
//				be used because it multiplies the CR by 1 + 4./9.
//-----------------------------------------------------------------------------------
//
//--------------------SOLAR PRESSURE DETAIL-----------------------------------------
//
//	The standard solar pressure model for a sphere is of the form
//		accel(sphere) = CR * X1 * k
//           X1 = A/M * Illum * Irrad / c
//		where CR = 1.0 for a perfectly reflective sphere
//				A = Area, M = Mass, 
//				Illum = illumination factor (0 <= Illum <= 1 )
//				Irrad = irradiance in Watts/Meter^2 
//					  = solar flux = Luminosity/(4*pi*distance_sunFromSat^2),
//				c = speed of light
//				k = unit vector from sun to satellite
//
//-----------------------------------------------------------------------------------
#endregion

using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using AGI.Attr;
using AGI.Plugin;
using AGI.Hpop.Plugin;

namespace AGI.SRP.LightReflection.Spherical.CSharp.Example
{
	/// <summary>
	/// HPOP SRP model plugin example
	/// </summary>

	[Guid("9034280C-29A7-4a88-A01E-421755F8567E")]

    [ProgId("AGI.SRP.LightReflection.Spherical.CSharp.Example")]

	[ClassInterface(ClassInterfaceType.None)]

	public class SRPPluginExample :
        ISRPPluginExample,
        IAgAsLightReflectionPlugin,
		IAgUtPluginConfig
	{

		#region Tuple3 (used for vector operations)

		class Tuple3
		{
			public double x = 0.0;
			public double y = 0.0;
			public double z = 0.0;

			public Tuple3()
			{
			}

			public Tuple3(double aX, double aY, double aZ)
			{
				x = aX;	y = aY;	z = aZ;
			}

			public Tuple3(Tuple3 a)
			{
				x = a.x;
				y = a.y;
				z = a.z;
			}

			public void scaleBy(double val)
			{
				x *= val;
				y *= val;
				z *= val;
			}

			public void addTo(Tuple3 a)
			{
				x += a.x;
				y += a.y;
				z += a.z;
			}
		}

		double dotProduct(Tuple3 a, Tuple3 b)
		{
			double dotp = a.x * b.x + a.y * b.y + a.z * b.z;

			return dotp;
		}

		double magnitude(Tuple3 a)
		{
			double mag = Math.Sqrt(dotProduct(a, a));

			return mag;
		}

		double normalize(ref Tuple3 a)
		{
			double mag = magnitude(a);

			if(mag > 0.0)
			{
				a.x /= mag;
				a.y /= mag;
				a.z /= mag;
			}

			return mag;
		}
	
		void crossProduct(Tuple3 a, Tuple3 b, ref Tuple3 c)
		{
			c.x = a.y*b.z - a.z*b.y;
			c.y = a.z*b.x - a.x*b.z;
			c.z = a.x*b.y - a.y*b.x;
		}

		#endregion

		#region Plugin Private Data Members

        //=============================================
        // Plugin Attributes to be configured
        // will be exposed via .NET properties
        // and using the Attribute Builder reference
        // passed as a parameter to the GetPluginConfig
        // Method.
        //==============================================

        private bool                m_Enabled;
        private int                 m_MsgInterval;
        private string              m_RefFrame;
        private double              m_SRPArea;   

        // Other private data members

        private string              m_Name = "AGI.SRP.Reflection.Spherical.CSharp.Example";
		private IAgUtPluginSite		m_UPS = null;
		private object				m_Scope = null;	

		private int					m_MsgCntr = -1;
		private bool				m_EvalMsgsOn = false;
        private int                 m_CrIndex = -1;
		private bool                m_DebugMode = false;

        private ArrayList           m_refFrameNames = new ArrayList();
       
		#endregion

		#region Life Cycle Methods
		public SRPPluginExample() : base()
		{
			try
			{
				Debug.WriteLine( "--> Entered", m_Name+".Example1()");

                // Set up initialize list of reference frames

                m_refFrameNames.Add(AgEUtFrame.eUtFrameInertial.ToString());	//0
                m_refFrameNames.Add(AgEUtFrame.eUtFrameFixed.ToString());    //1
                m_refFrameNames.Add(AgEUtFrame.eUtFrameLVLH.ToString());		//2
                m_refFrameNames.Add(AgEUtFrame.eUtFrameNTC.ToString());      //3
                m_refFrameNames.Add("Body");                        //4

                //===========================
                // General Plugin attributes
                //===========================

                m_Name = "AGI.SRP.Reflection.Spherical.CSharp.Example";
                m_Enabled = true;
                m_DebugMode = false;

                //===========================
                // SRP model specific parameters 
                //===========================

                m_SRPArea = 20.0;       // m^2
                m_RefFrame = "Body";    // Body frame

                //===========================
                // Logging related attributes
                //===========================

                m_MsgInterval = 500;

			}
			finally
			{
				Debug.WriteLine( "<-- Exited", m_Name+".Example1()");
			}
		}

        #endregion
		
		#region Messaging Code

		private void Message (AgEUtLogMsgType severity, String msgStr)
		{
			if(  m_UPS != null )
			{
				m_UPS.Message( severity, msgStr);
			}
		}

		private void DebugMsg(String msgStr)
		{
			if(m_DebugMode && m_EvalMsgsOn)
			{
				if(m_MsgCntr % m_MsgInterval == 0)
				{
					Message(AgEUtLogMsgType.eUtLogMsgDebug, msgStr);
				}
			}
		}

		#endregion

 

		#region Attribute Interface Implementation

		public string Name
		{
			get
			{
				Debug.WriteLine( m_Name, m_Name+".getName()");
				return m_Name;
			}
			set
			{
				m_Name = value;
				Debug.WriteLine( m_Name, m_Name+".setName()");
			}
		}

		public bool Enabled
		{
			get
			{
				Debug.WriteLine( m_Enabled, m_Name+".getEnabled()");
				return m_Enabled;
			}
			set
			{
				m_Enabled = value;
				Debug.WriteLine( m_Enabled, m_Name+".setEnabled()");
			}
		}

		public bool DebugMode
		{
			get
			{
				Debug.WriteLine( m_DebugMode, m_Name+".getDebugMode()");
				return m_DebugMode;
			}
			set
			{
				m_DebugMode = value;
				Debug.WriteLine( m_DebugMode, m_Name+".setDebugMode()");
			}
		}

		public int MsgInterval
		{
			get
			{
				Debug.WriteLine( m_MsgInterval, m_Name+".getMsgInterval()");
				return m_MsgInterval;
			}
			set
			{
				m_MsgInterval = value;
				Debug.WriteLine( m_MsgInterval, m_Name+".setMsgInterval()");
			}
		}

		public double SRPArea
		{
			get
			{
				Debug.WriteLine( m_SRPArea, m_Name+".getSRPArea()");
                return m_SRPArea;
			}
			set
			{
                m_SRPArea = value;
                Debug.WriteLine(m_SRPArea, m_Name + ".setSRPArea()");
			}
		}

		public string RefFrame
		{
			get
			{
				Debug.WriteLine( m_RefFrame, m_Name+".getRefFrame()");
                return m_RefFrame;
			}
			set
			{
                m_RefFrame = value;
                Debug.WriteLine(m_RefFrame, m_Name + ".setRefFrame()");
			}
		}
		
		#endregion

        #region IAgAsLightReflectionPlugin Interface Implementation

        public void Register(AgAsLightReflectionResultRegister Result)
        {
		    if( m_DebugMode)
		    {
			    Result.Message( AgEUtLogMsgType.eUtLogMsgInfo, "Register() called" );
		    }
    		
		    m_CrIndex = Result.RegisterParameter("Cr", 1.67, 1.0, 2.0, "Unitless");
    		
		    if(m_CrIndex > -1)
		    {
			    if(m_DebugMode)
			    {
                    Result.Message(AgEUtLogMsgType.eUtLogMsgInfo, "Registered Cr as Unitless parameter");
			    }
		    }
		    else
		    {
                Result.Message(AgEUtLogMsgType.eUtLogMsgAlarm, "Unable to register Cr as Unitless parameter");
		    }
        }

		public bool Init( IAgUtPluginSite Ups )
		{
			try
			{
				Debug.WriteLine( "--> Entered", m_Name+".Init()");

				m_UPS = Ups;

				if( m_UPS != null )
				{
					if( m_DebugMode )
					{
						if( m_Enabled )
						{
							Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name+" Enabled in "+m_Name+".Init()");
						}
						else
						{
							Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name+" Disabled in "+m_Name+".Init()");
						}
					}
				}
				else
				{
					throw new Exception( "UtPluginSite was null" );
				}
			}
			catch( Exception ex )
			{
				m_Enabled = false;

				Message( AgEUtLogMsgType.eUtLogMsgAlarm, m_Name+".Init(): Exception Message( " + ex.Message + " )" );
				Message( AgEUtLogMsgType.eUtLogMsgAlarm, m_Name+".Init(): Exception StackTr( " + ex.StackTrace + " )" );
				
				Debug.WriteLine( "Exception Message( " + ex.Message + " )", m_Name+".Init()" );
				Debug.WriteLine( "Exception StackTr( " + ex.StackTrace + " )", m_Name+".Init()");
			}
			finally
			{
				Debug.WriteLine( "<-- Exited", m_Name+".Init()" );
			}

			return m_Enabled;
		}

        public bool PreCompute(AgAsLightReflectionResult Result)
		{
			try
			{
				Debug.WriteLine( "--> Entered", m_Name+".PreCompute()");
			}
			catch( Exception ex )
			{
				m_Enabled = false;

				Message( AgEUtLogMsgType.eUtLogMsgAlarm, m_Name+".PreCompute(): Exception Message( " + ex.Message + " )" );
                Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name + ".PreCompute(): Exception StackTr( " + ex.StackTrace + " )");

                Debug.WriteLine("Exception Message( " + ex.Message + " )", m_Name + ".PreCompute()");
                Debug.WriteLine("Exception StackTr( " + ex.StackTrace + " )", m_Name + ".PreCompute()");
			}
			finally
			{
                Debug.WriteLine("<-- Exited", m_Name + ".PreCompute()");
			}

			return m_Enabled;
		}

        public bool Evaluate(AgAsLightReflectionResultEval ResultEval)
        {
            try
            {
                if (m_MsgCntr % m_MsgInterval == 0)
                {
                    Debug.WriteLine("--> Entered", m_Name + ".Evaluate( " + m_MsgCntr + " )");
                }

                if (m_Enabled)
                {
                    // Get value of Cr from STK/ODTK

                    double cr = ResultEval.ParameterValue(m_CrIndex);

                    if (m_RefFrame == "Body")
                    {
                        m_Enabled = setSphericalReflectanceInBody(ResultEval, cr);
                    }
                    else
                    {
                        m_Enabled = setSphericalReflectanceUsingFrame(ResultEval, cr, (AgEUtFrame) m_refFrameNames.IndexOf(m_RefFrame));
                    }
                }

                return m_Enabled;
            }
            catch (Exception ex)
            {
                m_Enabled = false;

                Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name + ".Evaluate(): Exception Message( " + ex.Message + " )");
                Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name + ".Evaluate(): Exception StackTr( " + ex.StackTrace + " )");

                Debug.WriteLine("Exception Message( " + ex.Message + " )", m_Name + ".Evaluate()");
                Debug.WriteLine("Exception StackTr( " + ex.StackTrace + " )", m_Name + ".Evaluate()");
            }
            finally
            {
                if (m_MsgCntr % m_MsgInterval == 0)
                {
                    Debug.WriteLine("<-- Exited", m_Name + ".Evaluate()");
                }
            }

            return m_Enabled;
        }

        private bool setSphericalReflectanceInBody(AgAsLightReflectionResultEval ResultEval, double cr)
        {
            double reflectanceMag;

            Tuple3 incidentVec = new Tuple3();

            ResultEval.IncidentDirectionInBody(ref incidentVec.x, ref incidentVec.y, ref incidentVec.z);

            // reflectance is positive along the incident direction

            incidentVec.scaleBy(m_SRPArea);

            if (m_CrIndex > -1)
            {
                ResultEval.SetReflectanceInBodyParamPartials(m_CrIndex, incidentVec.x, incidentVec.y, incidentVec.z);
            }

            incidentVec.scaleBy(cr);

            ResultEval.SetReflectanceInBody(incidentVec.x, incidentVec.y, incidentVec.z);

            double[,] incidentDirPosPartials, posPartials;
            
            incidentDirPosPartials = new double[3,3];
            posPartials = new double[3,3];

            ResultEval.IncidentDirectionBodyCompPosPartials(
                ref incidentDirPosPartials[0, 0], ref incidentDirPosPartials[0, 1], ref incidentDirPosPartials[0, 2],
                ref incidentDirPosPartials[1, 0], ref incidentDirPosPartials[1, 1], ref incidentDirPosPartials[1, 2],
                ref incidentDirPosPartials[2, 0], ref incidentDirPosPartials[2, 1], ref incidentDirPosPartials[2, 2]);

            reflectanceMag = cr * m_SRPArea;

            posPartials[0, 0] = reflectanceMag * incidentDirPosPartials[0,0];
            posPartials[0, 1] = reflectanceMag * incidentDirPosPartials[0,1];
            posPartials[0, 2] = reflectanceMag * incidentDirPosPartials[0,2];

            posPartials[1, 0] = reflectanceMag * incidentDirPosPartials[1,0];
            posPartials[1, 1] = reflectanceMag * incidentDirPosPartials[1,1];
            posPartials[1, 2] = reflectanceMag * incidentDirPosPartials[1,2];

            posPartials[2, 0] = reflectanceMag * incidentDirPosPartials[2,0];
            posPartials[2, 1] = reflectanceMag * incidentDirPosPartials[2,1];
            posPartials[2, 2] = reflectanceMag * incidentDirPosPartials[2,2];

            ResultEval.SetReflectanceBodyCompPosPartials(
                posPartials[0, 0], posPartials[0, 1], posPartials[0, 2],
                posPartials[1, 0], posPartials[1, 1], posPartials[1, 2],
                posPartials[2, 0], posPartials[2, 1], posPartials[2, 2]);

            // VelPartials are zero in inertial - we'll set this anyway to test it

            bool doVelPartials = true;

            if (doVelPartials)
            {
                double[,] incidentDirVelPartials, velPartials;
                
                incidentDirVelPartials = new double[3, 3];
                velPartials = new double[3, 3]; ;

                ResultEval.IncidentDirectionBodyCompVelPartials(
                    ref incidentDirVelPartials[0, 0], ref incidentDirVelPartials[0, 1], ref incidentDirVelPartials[0, 2],
                    ref incidentDirVelPartials[1, 0], ref incidentDirVelPartials[1, 1], ref incidentDirVelPartials[1, 2],
                    ref incidentDirVelPartials[2, 0], ref incidentDirVelPartials[2, 1], ref incidentDirVelPartials[2, 2]);

                velPartials[0, 0] = reflectanceMag * incidentDirVelPartials[0, 0];
                velPartials[0, 1] = reflectanceMag * incidentDirVelPartials[0, 1];
                velPartials[0, 2] = reflectanceMag * incidentDirVelPartials[0, 2];

                velPartials[1, 0] = reflectanceMag * incidentDirVelPartials[1, 0];
                velPartials[1, 1] = reflectanceMag * incidentDirVelPartials[1, 1];
                velPartials[1, 2] = reflectanceMag * incidentDirVelPartials[1, 2];

                velPartials[2, 0] = reflectanceMag * incidentDirVelPartials[2, 0];
                velPartials[2, 1] = reflectanceMag * incidentDirVelPartials[2, 1];
                velPartials[2, 2] = reflectanceMag * incidentDirVelPartials[2, 2];

                ResultEval.SetReflectanceBodyCompVelPartials(
                    velPartials[0, 0], velPartials[0, 1], velPartials[0, 2],
                    velPartials[1, 0], velPartials[1, 1], velPartials[1, 2],
                    velPartials[2, 0], velPartials[2, 1], velPartials[2, 2]);
            }

            Debug.WriteLine(m_MsgCntr + " " + cr + " : frame = Body : (" + incidentVec.x + "," + incidentVec.y + "," + incidentVec.z + ")");

            return true;
        }

        private bool setSphericalReflectanceUsingFrame(AgAsLightReflectionResultEval ResultEval, double cr, AgEUtFrame frame)
        {
            double reflectanceMag;

            Tuple3 incidentVec = new Tuple3();

            ResultEval.IncidentDirection((AgEUtFrame) frame, ref incidentVec.x, ref incidentVec.y, ref incidentVec.z);

            // reflectance is positive along the incident direction

            incidentVec.scaleBy(m_SRPArea);

            if (m_CrIndex > -1)
            {
                ResultEval.SetReflectanceParamPartials(m_CrIndex, frame, incidentVec.x, incidentVec.y, incidentVec.z);
            }

            incidentVec.scaleBy(cr);

            ResultEval.SetReflectance((AgEUtFrame) frame, incidentVec.x, incidentVec.y, incidentVec.z);

            double[,] incidentDirPosPartials, posPartials;

            incidentDirPosPartials = new double[3, 3];
            posPartials = new double[3, 3];

            ResultEval.IncidentDirectionCompPosPartials((AgEUtFrame) frame,
                ref incidentDirPosPartials[0, 0], ref incidentDirPosPartials[0, 1], ref incidentDirPosPartials[0, 2],
                ref incidentDirPosPartials[1, 0], ref incidentDirPosPartials[1, 1], ref incidentDirPosPartials[1, 2],
                ref incidentDirPosPartials[2, 0], ref incidentDirPosPartials[2, 1], ref incidentDirPosPartials[2, 2]);

            reflectanceMag = cr * m_SRPArea;

            posPartials[0, 0] = reflectanceMag * incidentDirPosPartials[0, 0];
            posPartials[0, 1] = reflectanceMag * incidentDirPosPartials[0, 1];
            posPartials[0, 2] = reflectanceMag * incidentDirPosPartials[0, 2];

            posPartials[1, 0] = reflectanceMag * incidentDirPosPartials[1, 0];
            posPartials[1, 1] = reflectanceMag * incidentDirPosPartials[1, 1];
            posPartials[1, 2] = reflectanceMag * incidentDirPosPartials[1, 2];

            posPartials[2, 0] = reflectanceMag * incidentDirPosPartials[2, 0];
            posPartials[2, 1] = reflectanceMag * incidentDirPosPartials[2, 1];
            posPartials[2, 2] = reflectanceMag * incidentDirPosPartials[2, 2];

            ResultEval.SetReflectanceCompPosPartials((AgEUtFrame) frame,
                posPartials[0, 0], posPartials[0, 1], posPartials[0, 2],
                posPartials[1, 0], posPartials[1, 1], posPartials[1, 2],
                posPartials[2, 0], posPartials[2, 1], posPartials[2, 2]);

            // VelPartials are zero in inertial - we'll set this anyway to test it

            bool doVelPartials = true;

            if (doVelPartials)
            {
                double[,] incidentDirVelPartials, velPartials;

                incidentDirVelPartials = new double[3, 3];
                velPartials = new double[3, 3]; ;

                ResultEval.IncidentDirectionCompVelPartials((AgEUtFrame) frame,
                    ref incidentDirVelPartials[0, 0], ref incidentDirVelPartials[0, 1], ref incidentDirVelPartials[0, 2],
                    ref incidentDirVelPartials[1, 0], ref incidentDirVelPartials[1, 1], ref incidentDirVelPartials[1, 2],
                    ref incidentDirVelPartials[2, 0], ref incidentDirVelPartials[2, 1], ref incidentDirVelPartials[2, 2]);

                velPartials[0, 0] = reflectanceMag * incidentDirVelPartials[0, 0];
                velPartials[0, 1] = reflectanceMag * incidentDirVelPartials[0, 1];
                velPartials[0, 2] = reflectanceMag * incidentDirVelPartials[0, 2];

                velPartials[1, 0] = reflectanceMag * incidentDirVelPartials[1, 0];
                velPartials[1, 1] = reflectanceMag * incidentDirVelPartials[1, 1];
                velPartials[1, 2] = reflectanceMag * incidentDirVelPartials[1, 2];

                velPartials[2, 0] = reflectanceMag * incidentDirVelPartials[2, 0];
                velPartials[2, 1] = reflectanceMag * incidentDirVelPartials[2, 1];
                velPartials[2, 2] = reflectanceMag * incidentDirVelPartials[2, 2];

                ResultEval.SetReflectanceCompVelPartials((AgEUtFrame) frame,
                    velPartials[0, 0], velPartials[0, 1], velPartials[0, 2],
                    velPartials[1, 0], velPartials[1, 1], velPartials[1, 2],
                    velPartials[2, 0], velPartials[2, 1], velPartials[2, 2]);
            }

            Debug.WriteLine(m_MsgCntr + " " + cr + " : frame = Body : (" + incidentVec.x + "," + incidentVec.y + "," + incidentVec.z + ")");

            return true;
        }

        public bool PostCompute(AgAsLightReflectionResult Result)
        {
            Debug.WriteLine("--> Entered", m_Name + ".PostCompute()");

            if (m_DebugMode)
            {
                if (m_Enabled)
                {
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, "PostCompute(): Enabled");
                }
                else
                {
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, "PostCompute(): Disabled");
                }
            }

            Debug.WriteLine("<-- Exited", m_Name + ".PostCompute()");

            // don't output on every call here  - this gets called a lot and would output too many messages!

            return m_Enabled;
        }

		public void Free()
		{
			try
			{
				Debug.WriteLine( "--> Entered", m_Name+".Free()");

				if( m_UPS != null)
				{
					if ( m_DebugMode )
					{
						Message( AgEUtLogMsgType.eUtLogMsgDebug, m_Name+".Free():" );
						Message( AgEUtLogMsgType.eUtLogMsgDebug, m_Name+".Free(): MsgCntr( " + m_MsgCntr + " )" );
					}

					Marshal.ReleaseComObject( m_UPS );
					m_UPS = null;

				}
			}
			catch( Exception ex )
			{
				Message( AgEUtLogMsgType.eUtLogMsgAlarm, m_Name+".Free(): Exception Message( " + ex.Message + " )" );
				Message( AgEUtLogMsgType.eUtLogMsgAlarm, m_Name+".Free(): Exception StackTr( " + ex.StackTrace + " )" );
				
				Debug.WriteLine( "Exception Message( " + ex.Message + " )", m_Name+".Free()" );
				Debug.WriteLine( "Exception StackTr( " + ex.StackTrace + " )", m_Name+".Free()");
			}
			finally
			{
				Debug.WriteLine( "<-- Exited", m_Name+".Free()" );
			}
        }

        #endregion


        #region IAgUtPluginConfig Interface Implementation
        public object GetPluginConfig( AgAttrBuilder aab )
		{
			try
			{
				Debug.WriteLine( "--> Entered", m_Name+".GetPluginConfig()");

				if( m_Scope == null )
				{
					m_Scope = aab.NewScope();

                    // ==============================
					//  Model specific attributes
					// ==============================
	
                    aab.AddQuantityDispatchProperty2(m_Scope, "SRPArea", "SRP Area", "SRPArea", "Area", "m^2", "m^2", (int)AgEAttrAddFlags.eAddFlagNone);
                    aab.AddChoicesDispatchProperty (m_Scope, "RefFrame", "Reference Frame", "RefFrame", m_refFrameNames.ToArray());

					//===========================
					// General Plugin attributes
					//===========================

					aab.AddStringDispatchProperty(m_Scope, "PluginName", "Human readable plugin name or alias", "Name", (int)AgEAttrAddFlags.eAddFlagNone );
					aab.AddBoolDispatchProperty  (m_Scope, "PluginEnabled", "If the plugin is enabled or has experienced an error", "Enabled", (int)AgEAttrAddFlags.eAddFlagNone );
					aab.AddBoolDispatchProperty  (m_Scope, "DebugMode", "Turn debug messages on or off", "DebugMode", (int)AgEAttrAddFlags.eAddFlagNone );
								
					//===========================
					// Messaging related attributes
					//===========================
					aab.AddIntDispatchProperty(m_Scope, "MessageInterval", "The interval at which to send messages during propagation in Debug mode", "MsgInterval", (int)AgEAttrAddFlags.eAddFlagNone );
				}
				string config;
				config = aab.ToString(this, m_Scope);
				Debug.WriteLine(m_Name+".GetPluginConfig():");
				Debug.WriteLine("============Attr Scope==============");
				Debug.WriteLine(config);
				Debug.WriteLine("====================================");
			}
			finally
			{
				Debug.WriteLine( "<-- Exited", m_Name+".GetPluginConfig()");
			}

			return m_Scope;
		}

		public void VerifyPluginConfig( AgUtPluginConfigVerifyResult apcvr )
		{
			bool	result	= true;
			string	message = "Ok";

			Debug.WriteLine( "--> Entered", m_Name+".VerifyPluginConfig()" );

			if(m_SRPArea < 0.0 )
			{
				result = false;
				message = "SRP Area must be > 0";
			}
			
			Debug.WriteLine( "<-- Exited", m_Name+".VerifyPluginConfig()" );

			apcvr.Result	= result;
			apcvr.Message	= message;
		}
		#endregion

        #region Registration functions
        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        [ComRegisterFunction]
        [ComVisible(false)]
        public static void RegisterFunction(Type t)
        {
            RemoveOtherVersions(t);
        }

        /// <summary>
        /// Called when the assembly is unregistered for use from COM.
        /// </summary>
        /// <param name="t">The type exposed to COM.</param>
        [ComUnregisterFunctionAttribute]
        [ComVisible(false)]
        public static void UnregisterFunction(Type t)
        {
            // Do nothing.
        }

        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// Eliminates the other versions present in the registry for
        /// this type.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        public static void RemoveOtherVersions(Type t)
        {
            try
            {
                using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                {
                    StringBuilder guidString = new StringBuilder("{");
                    guidString.Append(t.GUID.ToString());
                    guidString.Append("}");
                    using (RegistryKey guidKey = clsidKey.OpenSubKey(guidString.ToString()))
                    {
                        if (guidKey != null)
                        {
                            using (RegistryKey inproc32Key = guidKey.OpenSubKey("InprocServer32", true))
                            {
                                if (inproc32Key != null)
                                {
                                    string currentVersion = t.Assembly.GetName().Version.ToString();
                                    string[] subKeyNames = inproc32Key.GetSubKeyNames();
                                    if (subKeyNames.Length > 1)
                                    {
                                        foreach (string subKeyName in subKeyNames)
                                        {
                                            if (subKeyName != currentVersion)
                                            {
                                                inproc32Key.DeleteSubKey(subKeyName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore all exceptions...
            }
        }
        #endregion

 	}

}
//=====================================================//
//  Copyright 2008, Analytical Graphics, Inc.          //
//=====================================================//
