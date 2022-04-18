'===================================================== 
'  Copyright YYYY, YOUR COMPANY NAME GOES HERE          
'=====================================================

'========================================================='
'=====
'===== Begin Enumeration Declarations
'=====

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


'=====
'===== End Enumeration Declarations
'=====
'========================================================='

'========================================================='
'=====
'===== Begin Plugin Variable Declarations
'=====

Dim m_AgUtPluginSite, m_AgAttrScope

Set m_AgUtPluginSite 	   = Nothing
Set m_AgAttrScope 		   = Nothing

Dim m_Name, m_MyProperty

m_Name			= "MyPlugin.Hpop.VBScript.wsc"
m_MyProperty	= 0.0

'=====
'===== End Plugin Variable Declarations
'=====
'========================================================='

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
   
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		'==================================================='
		
		' Register all plugin parameters here, using AgAttrBuilder
		
		' Each parameter has a Type (double, bool, integer, filename, string, etc.)
		
		' Example: registration of MyProperty as a double
		
		Call AgAttrBuilder.AddDoubleDispatchProperty( 	_
			m_AgAttrScope, 								_
			"MyProperty",  								_ 
			"A description of MyProperty", 				_
			"MyProperty",								_
			eFlagNone )
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

    ' Perform checks of the ranges of the plugin parameter data
    
    ' If there is an error in the settings, then set Result to false
    ' and provide a Message to be communicated back to the user
    ' indicating the problem

	AgUtPluginConfigVerifyResult.Result  = Result
	AgUtPluginConfigVerifyResult.Message = Message

End Function  

'======================
' Init Method
'======================
Function Init( AgUtPluginSite )

	Set m_AgUtPluginSite = AgUtPluginSite
	
	Dim retVal
	
	retVal = true
	
	' Implement any Init activities here
	' If the plugin cannot initialize (say, because it cannot open a file it needs)
	' then it can return false and the plugin won't be be used
	
	' retVal = false
	
    Init = retVal

End Function
 
'======================
' PrePropagate Method
'======================
Function PrePropagate( AgAsHpopPluginResult )

	' Implement any PrePropogate activities here
	
	PrePropagate = true

End Function
   
'======================
' PreNextStep Function
'======================
Function PreNextStep( AgAsHpopPluginResult )

	Dim retVal

	retVal = true

	' Implement any PreNextStep activities here

	' If you have none, you can tell the application not to even call this function
	' by returning false instead of true

	' retVal = false
		
	PreNextStep = retVal
	
End Function

'=================
' Evaluate Method
'=================
Function Evaluate( AgAsHpopPluginResultEval )

	Dim retVal

	retVal = true

	' Implement any Evaluate activities here

	' If you have none, you can tell the application not to even call this function
	' by returning false instead of true

	' retVal = false
		
	Evaluate = retVal
	
End Function

'=================
' PostEvaluate Method
'=================
Function PostEvaluate( AgAsHpopPluginResultPostEval )

	Dim retVal
	
	retVal = true
	
	' Implement any PostEvaluate activities here
	
	' If you have none, you can tell the application not to even call this function
	' by returning false instead of true
	
	' retVal = false
	
	PostEvaluate = retVal

End Function

'========================================================
' PostPropagate Method
'========================================================
Function PostPropagate( AgAsHpopPluginResult)
	
	' Implement any PostPropogate activities here
	
	PostPropagate = true

End Function
   
'===========================================================
' Free Method
'===========================================================
Sub Free()

	' Perform any plugin cleanup activities here

	Set m_AgUtPluginSite = Nothing
	
End Sub

'=============================================================
' Name Property
'=============================================================
Function GetName()

	GetName = m_Name

End function

Function SetName( name )

	m_Name = name

End function


'=======================================================
' MyProperty property
'=======================================================
Function GetMyProperty()

       GetMyProperty = m_MyProperty

End Function

Function SetMyProperty(value)

       m_MyProperty = value

End Function

'===================================================== 
'  Copyright YYYY, YOUR COMPANY NAME GOES HERE          
'=====================================================