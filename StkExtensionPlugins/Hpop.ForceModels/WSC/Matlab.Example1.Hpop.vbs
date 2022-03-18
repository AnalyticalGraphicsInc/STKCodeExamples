'======================================================
'  Copyright 2005, Analytical Graphics, Inc.          
' =====================================================

'================================
' Matlab specific variables
'===============================

Dim m_mFileName
Dim m_MatlabApp

Set m_MatlabApp = nothing
m_mFilename = "example1Hpop"

' NOTE: to attach to an existing matlab session,
' you must execute: enableservice('AutomationServer',true)
' in that matlab session. If you do not, then a new Matlab
' session will be opened

' NOTE: our current experience is that even when you open
' the session yourself and attach to it, it will be closed
' once the plugin component is freed and releases its
' Matlab attachment.

'==========================================
' AgEUtFrame Enumeration
'==========================================
Dim eUtFrameInertial, eUtFrameFixed, eUtFrameLVLH, eUtFrameNTC, eUtFrameICRF, eUtFrameJ2000
eUtFrameInertial          = 0
eUtFrameFixed             = 1
eUtFrameLVLH              = 2
eUtFrameNTC               = 3
eUtFrameICRF              = 4
eUtFrameJ2000             = 5

'==========================================
' AgEUtTimeScale Enumeration
'==========================================
Dim eUTC, eTAI, eTDT, eUT1, eSTKEpochSec, eTDB, eGPS
eUTC             = 0
eTAI             = 1
eTDT             = 2
eUT1             = 3
eSTKEpochSec     = 4
eTDB             = 5
eGPS             = 6

'==========================================
' AgELogMsgType Enumeration
'==========================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm
eLogMsgDebug         = 0
eLogMsgInfo          = 1
eLogMsgForceInfo     = 2
eLogMsgWarning       = 3
eLogMsgAlarm         = 4

'===========================================
' AgEAttrAddFlags Enumeration
'===========================================
Dim eFlagNone, eFlagTransparent, eFlagHidden, eFlagTransient, eFlagReadOnly, eFlagFixed
eFlagNone            = 0
eFlagTransparent     = 2
eFlagHidden          = 4
eFlagTransient       = 8
eFlagReadOnly        = 16
eFlagFixed           = 32

'=================================
' AgEUtSunPosType Enumeration
'=================================
Dim eApparentToTrueCB, eApparent, eTrue, eSRP

eApparentToTrueCB 	= 0
eApparent 			= 1
eTrue 				= 2
eSRP 				= 3

'=================================
' AgEAccelType Enumeration
'=================================
Dim eTotalAccel, eTwoBodyAccel, eGravityAccel, ePerturbedGravityAccel, eSolidTidesAccel
Dim eOceanTidesAccel, eDragAccel, eSRPAccel, eThirdBodyAccel, eGenRelativityAccel, eAddedAccel
Dim eAlbedoAccel, eThermalRadiationPressureAccel

eTotalAccel 			= 0
eTwoBodyAccel 			= 1
eGravityAccel 			= 2
ePerturbedGravityAccel 	= 3
eSolidTidesAccel 		= 4
eOceanTidesAccel 		= 5
eDragAccel 				= 6
eSRPAccel 				= 7
eThirdBodyAccel 		= 8
eGenRelativityAccel 	= 9
eAddedAccel 			= 10
eAlbedoAccel			= 11
eThermalRadiationPressureAccel = 12

' =================================
'  AgEForceModelType Enumeration
' =================================
Dim eGravityModel, eSolidTidesModel, eOceanTidesModel, eDragModel, eSRPModel
Dim eThirdBodyModel, eGenRelativityModel, eAlbedoModel, eThermalRadiationPressureModel, eDensityModel

eGravityModel 			= 0
eSolidTidesModel		= 1
eOceanTidesModel 		= 2
eDragModel				= 3
eSRPModel		 		= 4
eThirdBodyModel 		= 5
eGenRelativityModel 	= 6
eAlbedoModel 			= 7
eThermalRadiationPressureModel 	= 8
eDensityModel 			= 9


'================================
' Global Variables
'================================
Dim m_AgUtPluginSite
Dim m_AgStkPluginSite
Dim m_AgAttrScope
Dim m_CrdnPluginProvider
Dim m_CrdnConfiguredVector

Set m_AgUtPluginSite 	   = Nothing
Set m_AgStkPluginSite	   = Nothing
Set m_AgAttrScope 		   = Nothing
Set m_CrdnPluginProvider   = Nothing
Set m_CrdnConfiguredVector = Nothing

Dim m_Name
Dim m_Enabled
Dim m_VectorName
Dim m_SRPArea
Dim m_SrpIsOn
Dim m_AccelRefFrame
Dim m_AccelRefFrameChoices(3)
Dim m_AccelX
Dim m_AccelY
Dim m_AccelZ
Dim m_MsgStatus
Dim m_EvalMsgInterval
Dim m_PostEvalMsgInterval
Dim m_PreNextMsgInterval
Dim m_PreNextCntr
Dim m_EvalCntr
Dim m_PostEvalCntr

m_Name						= "Matlab.Example1.Hpop.wsc"
m_Enabled					= true
m_VectorName				= "Periapsis"
m_SRPArea					= 0.0
m_SrpIsOn					= false

m_AccelRefFrame				= 3
m_AccelRefFrameChoices(0)	= "eUtFrameInertial"
m_AccelRefFrameChoices(1)	= "eUtFrameFixed"
m_AccelRefFrameChoices(2)	= "eUtFrameLVLH"
m_AccelRefFrameChoices(3)	= "eUtFrameNTC"

m_AccelX					= 0.0
m_AccelY					= 0.07
m_AccelZ					= 0.0
m_MsgStatus					= false
m_EvalMsgInterval			= 5000
m_PostEvalMsgInterval		= 5000
m_PreNextMsgInterval		= 1000

m_PreNextCntr				= 0
m_EvalCntr					= 0
m_PostEvalCntr				= 0

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
   
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		'===========================
		' General Plugin attributes
		'===========================
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias",                 "Name",       0 )
		Call AgAttrBuilder.AddBoolDispatchProperty  ( m_AgAttrScope, "PluginEnabled",     "If the plugin is enabled or has experience an error", "Enabled",    0 )
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "VectorName", "Vector Name that affects the srp area",              "VectorName", 0 )
				
		'===========================
		' Propagation related
		'===========================
		Call AgAttrBuilder.AddChoicesDispatchProperty( m_AgAttrScope, "AccelRefFrame", "Acceleration Reference Frame",    "AccelRefFrame", GetAccelRefFrameChoices() )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "AccelX",         "Acceleration in the X direction", "AccelX",        0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "AccelY",         "Acceleration in the Y direction", "AccelY",        0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "AccelZ",         "Acceleration in the Z direction", "AccelZ",        0 )
				
		'===========================
		' Messaging related attributes
		'===========================
		Call AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "UsePropagationMessages",     "Send messages to the message window during propagation",                               "MsgStatus",           0 )
		Call AgAttrBuilder.AddIntDispatchProperty ( m_AgAttrScope, "EvaluateMessageInterval",  "The interval at which to send messages from the Evaluate method during propagation", "EvalMsgInterval",     0 )
		Call AgAttrBuilder.AddIntDispatchProperty ( m_AgAttrScope, "PostEvaluateMessageInterval",  "The interval at which to send messages from the PostEvaluate method during propagation", "PostEvalMsgInterval",     0 )
		Call AgAttrBuilder.AddIntDispatchProperty ( m_AgAttrScope, "PreNextStepMessageInterval", "The interval at which to send messages from the PreNextStep method during propagation", "PreNextMsgInterval", 0 )

	End If

	Set GetPluginConfig = m_AgAttrScope

End Function  

'===========================
' VerifyPluginConfig method
'===========================
Function VerifyPluginConfig(AgUtPluginConfigVerifyResult)
   
    Dim Result
    Dim Message

	Result = true
	Message = "Ok"

    If( Not ( m_AccelX <= 10 And m_AccelX >= -10 ) ) Then
    
    	Result  = false
    	Message = "AccelX was not within the range of -10 to +10 meters per second squared"
    
    ElseIf( Not ( m_AccelY <= 10 And m_AccelY >= -10 ) ) Then

    	Result  = false
    	Message = "AccelY was not within the range of -10 to +10 meters per second squared"

    ElseIf( Not ( m_AccelZ <= 10 And m_AccelZ >= -10 ) ) Then

    	Result  = false
    	Message = "AccelZ was not within the range of -10 to +10 meters per second squared"
    
	End If

	AgUtPluginConfigVerifyResult.Result  = Result
	AgUtPluginConfigVerifyResult.Message = Message

End Function  

'======================
' Init Method
'======================
Function Init( AgUtPluginSite )

	Set m_AgUtPluginSite = AgUtPluginSite
	
	If( Not m_AgUtPluginSite is Nothing ) Then
	
		If( m_Enabled = true ) Then
		
			Dim siteName
			siteName = m_AgUtPluginSite.SiteName
			
			If(siteName = "IAgStkPluginSite" Or siteName = "IAgGatorPluginSite") Then
				Set m_CrdnPluginProvider 	= m_AgUtPluginSite.VectorToolProvider
				
				If(Not m_CrdnPluginProvider is Nothing) Then
					Set m_CrdnConfiguredVector  = m_CrdnPluginProvider.ConfigureVector( m_VectorName, "", "J2000", "CentralBody/Earth")
				End If
				
				If ( m_MsgStatus = true ) Then
					 
					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init():" )
					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelRefFrame( " & GetAccelRefFrame() & " )" )
					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelX( " & m_AccelX & " )" )
					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelY( " & m_AccelY & " )" )
					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelZ( " & m_AccelZ & " )" )

				End If
				
				If(m_CrdnConfiguredVector is Nothing) Then
					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Could not obtain " & m_VectorName )
					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Turning off the computation of SRP Area" )
				End If
			Else
				Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): " & siteName & " does not provide VectorToolProvider" )
				Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Turning off the computation of SRP Area" )
			End If
		Else
		
			Call m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Disabled" )
		
		End If
		
		' Get handle to Matlab
		
		If(m_Enabled = true) Then
		    Dim filepath
		    filepath = ""
		    
		    Set m_MatlabApp = GetObject(filepath,"Matlab.Application")

		    If(m_MatlabApp is Nothing) Then
				MsgBox "Cannot get handle to Matlab"
				m_Enabled = false
    		End If
    		    
		End If
	
	End If
    
    Init = m_Enabled

End Function
 
'======================
' PrePropagate Method
'======================
Function PrePropagate( AgAsHpopPluginResult )

	If( Not m_AgUtPluginSite is Nothing ) Then
	
		If( m_Enabled = true ) Then
		
			If( Not AgAsHpopPluginResult is Nothing ) Then
				
				m_SrpIsOn = AgAsHpopPluginResult.IsForceModelOn( eSRPModel )
				
				If(m_SrpIsOn) Then
					m_SRPArea = AgAsHpopPluginResult.SRPArea
				End if
			
			End If
			
		Else
		
			If( m_MsgStatus = true ) Then
			
				Call m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Disabled" )
			
			End If
		
		End If
	
	End If
	
	PrePropagate = m_Enabled

End Function
   
'======================
' PreNextStep Function
'======================
Function PreNextStep( AgAsHpopPluginResult )

	m_PreNextCntr = m_PreNextCntr + 1
	
	If( Not m_AgUtPluginSite is Nothing ) Then
	
		If( m_Enabled = true ) Then

			If( m_MsgStatus = true ) Then
			
				If( m_PreNextCntr Mod m_PreNextMsgInterval = 0 ) Then

					Call m_AgUtPluginSite.Message( eLogMsgDebug, "PreNextStep( " & m_PreNextCntr & " ):" )
	
				End If

			End If
		
		Else
		
			If( m_MsgStatus = true ) Then
			
				Call m_AgUtPluginSite( eLogMsgDebug, "PreNextStep(): Disabled" )
			
			End If
		
		End If

	End If
	
	PreNextStep = m_Enabled
	
End Function

'=================
' Evaluate Method
'=================
Function Evaluate( AgAsHpopPluginResultEval )

	m_EvalCntr = m_EvalCntr + 1
	
	If( Not m_AgUtPluginSite is Nothing ) Then
	
		If( m_Enabled = true ) Then

			Call EvaluateSRPArea( AgAsHpopPluginResultEval)

			Call AgAsHpopPluginResultEval.AddAcceleration( m_AccelRefFrame, m_AccelX, m_AccelY, m_AccelZ )

			If( m_MsgStatus = true ) Then
			
				If( m_EvalCntr Mod m_EvalMsgInterval = 0 ) Then

					Call m_AgUtPluginSite.Message( eLogMsgDebug, "Evaluate( " & m_EvalCntr & " ):" )
	
				End If

			End If
		
		Else
		
			If( m_MsgStatus = true ) Then
			
				Call m_AgUtPluginSite( eLogMsgDebug, "Evaluate(): Disabled" )
			
			End If
		
		End If

	End If
	
	Evaluate = m_Enabled

End Function

Function EvaluateSRPArea( ResultEval )

	If(m_SrpIsOn) Then
	
		' This interface may not be present
		If( Not m_CrdnConfiguredVector is Nothing) Then

			'=============================
			' Position Velocity variables
			'=============================
			Dim PosVelArray 
			Dim PosX_Index, PosY_Index, PosZ_Index
			Dim VelX_Index, VelY_Index, VelZ_Index

			Set PosVelArray = Nothing
			PosX_Index = 0
			PosY_Index = 1
			PosZ_Index = 2
			VelX_Index = 3
			VelY_Index = 4
			VelZ_Index = 5

			'=============================
			' Vector variables
			'=============================
			Dim VecArray
			Dim VecX_Index, VecY_Index, VecZ_Index

			Set VecArray = Nothing
			VecX_Index = 0
			VecY_Index = 1
			VecZ_Index = 2

			'=============================
			' Calculation variables
			'=============================
			Dim VecPosDotProd, VecMag, PosMag, Theta

			VecPosDotProd = 0.0
			VecMag = 0.0
			PosMag = 0.0
			Theta  = 0.0

			If( Not ResultEval is Nothing ) Then

				PosVelArray = ResultEval.PosVel_Array( eInterial )

				VecArray = m_CrdnConfiguredVector.CurrentValue_Array( ResultEval )
								
				' Set variables into the base workspace of Matlab
				Call m_MatlabApp.PutWorkspaceData("satPositionX", "base", CDbl(PosVelArray( PosX_Index ) ))
				Call m_MatlabApp.PutWorkspaceData("satPositionY", "base", CDbl(PosVelArray( PosY_Index ) ))
				Call m_MatlabApp.PutWorkspaceData("satPositionZ", "base", CDbl(PosVelArray( PosZ_Index ) ))
				Call m_MatlabApp.PutWorkspaceData("vectorX", "base", CDbl(VecArray( VecX_Index )))
				Call m_MatlabApp.PutWorkspaceData("vectorY", "base", CDbl(VecArray( VecY_Index )))
				Call m_MatlabApp.PutWorkspaceData("vectorZ", "base", CDbl(VecArray( VecZ_Index )))
				
				' Execute the mfile on those variables
				
				Dim outResult
				
				Call m_MatlabApp.Feval(m_mFilename, 1, outResult, _
						"satPositionX=", "satPositionY=", "satPositionZ=", _
						"vectorX=", "vectorY=", "vectorZ=")
						
				' Get the computed variable
									
				Theta = CDbl(outResult(0))
				
				' SRP must be on else this is a run-time error
				ResultEval.SRPArea = ( m_SRPArea / 4.0 ) * ( 3 - Sin( Theta ) )

				If( m_AgUtPluginSite is Nothing And m_MsgStatus = true ) Then

					If( m_EvalCntr Mod m_EvalMsgInterval = 0 ) Then

						Dim ThetaDeg
						ThetaDeg = Theta * 57.2957795130823208767

						Call m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " & m_EvalCntr & " ): VecX( " & VecArray(VecX_Index) & " ), VecY( " & VecArray(VecY_Index) & " ), VecZ( " & VecArray(VecZ_Index) & " ) meters/sec" )
						Call m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " & m_EvalCntr & " ): PosX(" & PosVelArray(PosX_Index) & " ), PosY( " & PosVelArray(PosY_Index) & " ), PosZ( " & PosVelArray(PosZ_Index) & " ) meters" )
						Call m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " & m_EvalCntr & " ): VelX(" & PosVelArray(VelX_Index) & " ), VelY( " & PosVelArray(VelY_Index) & " ), VelZ(" & PosVelArray(VelZ_Index) & " ) meters/sec" )
						Call m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " & m_EvalCntr & " ): SrpArea(" & ResultEval.SRPArea & " m^2), Theta( " & ThetaDeg & " deg)" )

					End If

				End If

			Else

				If( Not m_AgUtPluginSite is Nothing And m_MsgStatus = true ) Then

					Call m_AgUtPluginSite.Message( eLogMsgWarning, "Crdn Configured Vector or Result Eval was null" )

				End If

			End If

		End If
	End If
	
	EvaluateSRPArea = True

End Function

'=================
' PostEvaluate Method
'=================
Function PostEvaluate( AgAsHpopPluginResultPostEval )

	m_PostEvalCntr = m_PostEvalCntr + 1
	
	If( Not m_AgUtPluginSite is Nothing ) Then
	
		If( m_Enabled = true ) Then

			If( m_MsgStatus = true ) Then
			
				If( m_PostEvalCntr Mod m_PostEvalMsgInterval = 0 ) Then
					Dim reportFrame, frameName
					reportFrame = eUtFrameNTC
					frameName = "NTC"

					Dim accelType, aSrpArray, AltInKm
					accelType = eSRPAccel
					
					AltInKm = AgAsHpopPluginResultPostEval.Altitude * 0.001
					
					Dim msgStr
					
					msgStr = m_Name & ".PostEvaluate( " & m_PostEvalCntr & " ): SRPArea ("
					msgStr = msgStr & AgAsHpopPluginResultPostEval.SRPArea & " m^2), Altitude (" & AltInKm & " km)"

					Call m_AgUtPluginSite.Message( eLogMsgDebug, msgStr )

					aSrpArray = AgAsHpopPluginResultPostEval.GetAcceleration_Array( accelType, reportFrame )
					
					If(IsArray(aSrpArray) and UBound(aSrpArray) > 1) Then
					
						msgStr = m_Name & ".PostEvaluate( " & m_PostEvalCntr & " ): SRPAccel ("
						msgStr = msgStr & frameName & ") is (" & aSrpArray(0) & ", "
						msgStr = msgStr & aSrpArray(1) & ", " & aSrpArray(2) & ") meters/secs^2"

						Call m_AgUtPluginSite.Message( eLogMsgDebug, msgStr )
					End If
					
					Dim aThrustArray

					' report out the added acceleration in NTC components
					
					accelType = eAddedAccel

					aThrustArray = AgAsHpopPluginResultPostEval.GetAcceleration_Array( accelType, reportFrame )

					If(IsArray(aThrustArray) and UBound(aThrustArray) > 1) Then
										
						msgStr = m_Name & ".PostEvaluate( " & m_PostEvalCntr & " ): ThrustAccel ("
						msgStr = msgStr & frameName & ") is (" & aThrustArray(0) & ", "
						msgStr = msgStr & aThrustArray(1) & ", " & aThrustArray(2) & ") meters/secs^2"

						Call m_AgUtPluginSite.Message( eLogMsgDebug, msgStr )
					End If
	
				End If

			End If
		
		Else
		
			If( m_MsgStatus = true ) Then
			
				Call m_AgUtPluginSite( eLogMsgDebug, "PostEvaluate(): Disabled" )
			
			End If
		
		End If

	End If
	
	PostEvaluate = m_Enabled

End Function

'========================================================
' PostPropagate Method
'========================================================
Function PostPropagate( AgAsHpopPluginResult)

	If( Not m_AgUtPluginSite is Nothing ) Then
	
		If( m_Enabled = true ) Then
		
			If( m_MsgStatus = true ) Then
			
				Call m_AgUtPluginSite.Message( eLogMsgDebug, "PostPropagate():" )
			
			End If
		
		Else
		
			If( m_MsgStatus = true ) Then
			
				Call m_AgUtPluginSite.Message( eLogMsgDebug, "PostPropagate(): Disabled" )
			
			End If
		
		End If
	
	End If
	
	PostPropagate = m_Enabled

End Function
   
'===========================================================
' Free Method
'===========================================================
Sub Free()

	If( Not m_AgUtPluginSite is Nothing ) Then
	
		If( m_MsgStatus = true ) Then
		
			Call m_AgUtPluginSite.Message( eLogMsgDebug, "Free():" )
			Call m_AgUtPluginSite.Message( eLogMsgDebug, "Free(): PreNextCntr( " & m_PreNextCntr & " )" )
			Call m_AgUtPluginSite.Message( eLogMsgDebug, "Free(): EvalCntr( " & m_EvalCntr & " )" )
			Call m_AgUtPluginSite.Message( eLogMsgDebug, "Free(): PostEvalCntr( " & m_PostEvalCntr & " )" )
		
		End If
		
		Set m_AgUtPluginSite 		= Nothing
		Set m_CrdnPluginProvider   	= Nothing
		Set m_CrdnConfiguredVector 	= Nothing

	End If

End Sub

'=============================================================
' Name Method
'=============================================================
Function GetName()

	GetName = m_Name

End function

Function SetName( name )

	m_Name = name

End function

'============================================================
' Enabled property
'============================================================
Function GetEnabled()

       GetEnabled = m_Enabled

End Function

Function SetEnabled( enabled )

       m_Enabled = enabled

End Function

'============================================================
' VectorName property
'============================================================
Function GetVectorName()

       GetVectorName = m_VectorName

End Function

Function SetVectorName(vectorname)

       m_VectorName = vectorname

End Function

'===========================================================
' AccelRefFrame property
'===========================================================
Function GetAccelRefFrame()
       
	GetAccelRefFrame = m_AccelRefFrameChoices( m_AccelRefFrame )

End Function

Function SetAccelRefFrame(accelrefframe)

	If( m_AccelRefFrameChoices(0) = accelrefframe ) Then
	
		m_AccelRefFrame = 0

	ElseIf( m_AccelRefFrameChoices(1) = accelrefframe ) Then
	
		m_AccelRefFrame = 1

	ElseIf( m_AccelRefFrameChoices(2) = accelrefframe ) Then
	
		m_AccelRefFrame = 2

	ElseIf( m_AccelRefFrameChoices(3) = accelrefframe ) Then
	
		m_AccelRefFrame = 3

	End If
	
End Function

'===========================================================
' AccelRefFrameChoices property
'===========================================================
Function GetAccelRefFrameChoices()

       GetAccelRefFrameChoices = m_AccelRefFrameChoices

End Function

Function SetAccelRefFrameChoices(accelrefframechoices)

       m_AccelRefFrameChoices = accelrefframechoices

End Function

'==========================================================
' AccelX property
'==========================================================
Function GetAccelX()

       GetAccelX = m_AccelX

End Function

Function SetAccelX(accelx)

       m_AccelX = accelx

End Function

'=========================================================
' AccelY property
'=========================================================
Function GetAccelY()

       GetAccelY = m_AccelY

End Function

Function SetAccelY(accely)

       m_AccelY = accely

End Function

'=========================================================
' AccelZ property
'=========================================================
Function GetAccelZ()

       GetAccelZ = m_AccelZ

End Function

Function SetAccelZ(accelz)

       m_AccelZ = accelz

End Function

'======================================================
' MsgStatus property
'======================================================
Function GetMsgStatus()

       GetMsgStatus = m_MsgStatus

End Function

Function SetMsgStatus(msgstatus)

       m_MsgStatus = msgstatus

End Function

'=======================================================
' EvalMsgInterval property
'=======================================================
Function GetEvalMsgInterval()

       GetEvalMsgInterval = m_EvalMsgInterval

End Function

Function SetEvalMsgInterval(evalmsginterval)

       m_EvalMsgInterval = evalmsginterval

End Function

'=======================================================
' PostEvalMsgInterval property
'=======================================================
Function GetPostEvalMsgInterval()

       GetPostEvalMsgInterval = m_PostEvalMsgInterval

End Function

Function SetPostEvalMsgInterval(postevalmsginterval)

       m_PostEvalMsgInterval = postevalmsginterval

End Function

'=======================================================
' PreNextMsgInterval property
'=======================================================
Function GetPreNextMsgInterval()

       GetPreNextMsgInterval = m_PreNextMsgInterval

End Function

Function SetPreNextMsgInterval(prenextmsginterval)

       m_PreNextMsgInterval = prenextmsginterval

End Function

'======================================================
'  Copyright 2005, Analytical Graphics, Inc.          
' =====================================================
