'======================================================
'  Copyright 2006-2010, Analytical Graphics, Inc.          
' =====================================================

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

'==================================
' Control Type Enumeration
'==================================
Dim eSearchControlTypesReal

eSearchControlTypesReal = 0

'================================
' Declare Global Variables
'================================
Dim m_AgUtPluginSite
Dim m_AgAttrScope

Set m_AgUtPluginSite = Nothing
Set m_AgAttrScope	 = Nothing

'======================================
' Declare Global 'Attribute' Variables
'======================================
Dim m_Name
Dim m_maxIters

m_Name	= "VBScript.SearchExample.wsc"   
m_maxIters   = 100

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		'===========================
		' General Plugin attributes
		'===========================
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias", "Name", eFlagReadOnly )
		Call AgAttrBuilder.AddIntDispatchProperty  ( m_AgAttrScope, "MaxIterations", "Maximum Iterations", "MaxIterations", 0 )
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

	AgUtPluginConfigVerifyResult.Result  = Result
	AgUtPluginConfigVerifyResult.Message = Message

End Function  

'===========================
' Message method
'===========================
Sub Message( msgType, msg )
   
	If( Not m_AgUtPluginSite is Nothing) then
	   	
		Call m_AgUtPluginSite.Message( msgType, msg )

	End If
   	
End Sub

'======================
' Init Method
'======================
Function Init( AgUtPluginSite )

	Set m_AgUtPluginSite = AgUtPluginSite

	Init = 1

End Function

'======================
' Run Method
'======================
Function Run( AgSearchOperand, testing )

	Dim controls, results
    Set controls = AgSearchOperand.Controls
    Set results = AgSearchOperand.Results
      
	Dim controlIndex, resultIndex

	controlIndex = 0	
	found = false
    while (not found And controlIndex < controls.Count)
		if (controls(controlIndex).IsActive = true) then
			found = true
		else
			controlIndex = controlIndex + 1
		end if
	wend
	
	resultIndex = 0
	found = false
	while (not found and resultIndex < results.Count)
		if (results(resultIndex).IsActive = true) then
			found = true
		else
			resultIndex = resultIndex + 1
		end if
	wend
	
	if (controlIndex >= controls.Count Or resultIndex >= results.Count) then
		Call Message(eLogMsgAlarm, "There must be one active control and one active result.")
		Run = 0
		exit function
	end if
	
    Dim count
    count = 0
          
	Call AgSearchOperand.Evaluate2(True)  ' the true flag lets the run appear on graphs
	
	Dim a, b, fa, fb
	a = controls(controlIndex).CurrentValue
	b = a
	fa = results(resultIndex).CurrentValue
	fb = fa
	
	Dim stepSize
	stepSize = controls(controlIndex).Step
	
	Dim desired, tolerance
	desired = results(resultIndex).DesiredValue
	tolerance = results(resultIndex).Tolerance
	
	Dim insideTolerance
	insideTolerance = False
	' are we already within tolerance?
	If (abs(fa - desired) < tolerance) Then
		insideTolerance = True
	End If
	
	If (testing = False And insideTolerance = False) Then
		' if we're testing or already inside tolerance we won't do any searching
		

		' make the status grid
	    Call AgSearchOperand.StatusGrid.CreateGrid(2, 6)
	    Call AgSearchOperand.StatusGrid.SetColumnToTruncateLeft(0)
	    Call AgSearchOperand.StatusGrid.SetColumnToTruncateLeft(2)
	    Call AgSearchOperand.StatusGrid.SetHeaderCellString(0, 0, "Control Name")
	    Call AgSearchOperand.StatusGrid.SetHeaderCellString(0, 1, "Control Value")
	    Call AgSearchOperand.StatusGrid.SetHeaderCellString(0, 2, "Result Name")
	    Call AgSearchOperand.StatusGrid.SetHeaderCellString(0, 3, "Result Value")
	    Call AgSearchOperand.StatusGrid.SetHeaderCellString(0, 4, "Desired Value")
	    Call AgSearchOperand.StatusGrid.SetHeaderCellString(0, 5, "Tolerance")

	    Call AgSearchOperand.StatusGrid.SetCellString(1, 0, controls(controlIndex).ObjectName + " : " + controls(controlIndex).ControlName)
	    Call AgSearchOperand.StatusGrid.SetCellString(1, 2, results(resultIndex).ObjectName + " : " + results(resultIndex).ResultName)
	    Call AgSearchOperand.StatusGrid.SetCellResultValue(1, 4, resultIndex, results(resultIndex).DesiredValue, 8)
	    ' tolerance is in delta units
	    Call AgSearchOperand.StatusGrid.SetCellResultDeltaValue(1, 5, resultIndex, results(resultIndex).Tolerance, 8)

	    Call AgSearchOperand.StatusGrid.SetStatus("Initial run")
		
		
		Dim changedSign, bounded
		changedSign = False
		bounded = False
		
		' find the initial set that bounds zero
		Dim boundable
		boundable = True
		While bounded = False And boundable = True And count < m_maxIters
		
		    count = count + 1
			a = b
			fa = fb
			b = a + stepSize
			controls(controlIndex).CurrentValue = b
		
			Call AgSearchOperand.Evaluate2(True)

			fb = results(resultIndex).CurrentValue
			
		    Call AgSearchOperand.StatusGrid.SetCellControlValue(1, 1, controlIndex, b, 8)
		    Call AgSearchOperand.StatusGrid.SetCellResultValue(1, 3, resultIndex, fb, 8)
		    Call AgSearchOperand.StatusGrid.SetStatus("Iteration " & count & ": Searching for bounds")
		    Call AgSearchOperand.StatusGrid.Refresh()
			
			
			' see if b hit the desired value
			If (abs(fb - desired) < tolerance) Then
				insideTolerance = True
				bounded = True
		        Call AgSearchOperand.StatusGrid.SetStatus("Desired value reached while searching for bounds.")
		        Call AgSearchOperand.StatusGrid.Refresh()
			Else
				bounded = (fa > desired And fb < desired) Or (fa < desired And fb > desired)
			End If
			
			' make sure we are getting closer to the desired value
			If ( bounded = False And abs(fb - desired) >= abs(fa - desired) ) Then
				' search in the other direction, unless we've already changed the step once
				If (changedSign = False) Then
					changedSign = True
					stepSize = -stepSize
					b = a
					fb = fa
				Else
					' error out
			        Call AgSearchOperand.StatusGrid.SetStatus("Unable to bound desired value")
			        Call AgSearchOperand.StatusGrid.Refresh()
					Call Message(eLogMsgAlarm, "Unable to bound desired value with given initial guess and step")
					boundable = False
				End If
			End If
		Wend
		
		' Now do bisection search within the bounds if necessary and possible
		If (bounded = True And insideTolerance = False) Then
			
			Dim c, fc
			c = b
			fc = fb
			While (abs(fc - desired) > tolerance And count < m_maxIters)
			
			    count = count + 1
			
				c = (a + b) / 2.0
				
				controls(controlIndex).CurrentValue = c
				
				Call AgSearchOperand.Evaluate2(True)
				
				fc = results(resultIndex).CurrentValue
				
				If ( (fc > desired And fa > desired) Or (fc < desired And fa < desired)) Then
					a = c
					fa = fc
				Else
					b = c
					fb = fc
				End If
				
		        Call AgSearchOperand.StatusGrid.SetStatus("Iteration " & count & ": Searching for root")
		        Call AgSearchOperand.StatusGrid.SetCellControlValue(1, 1, controlIndex, c, 8)
		        Call AgSearchOperand.StatusGrid.SetCellResultValue(1, 3, resultIndex, fc, 8)
		        Call AgSearchOperand.StatusGrid.Refresh()

			Wend
			
			If (abs(fc - desired) > tolerance) Then
	            Call AgSearchOperand.StatusGrid.SetStatus("Unable to converge within " & m_maxIters & " iterations.")
	            Call AgSearchOperand.StatusGrid.Refresh()
				insideTolerance = False
			Else
	            Call AgSearchOperand.StatusGrid.SetStatus("Converged after  " & count & " iterations.")
	            Call AgSearchOperand.StatusGrid.Refresh()
				InsideTolerance = True
			End If
		End If
	End If
	
	If (insideTolerance = True) Then
		Run = 1
	Else
		Run = 0
	End If
	
End Function

'=============================
' Get Control's Prog Id Method
'=============================
Function GetControlsProgId( controlType )

	if (controlType = eSearchControlTypesReal) then
		GetControlsProgId = "VBScript.SearchControlRealExample.WSC"
	else
		GetControlsProgId = ""
	end if

End Function

'=============================
' Get Results's Prog Id Method
'=============================
Function GetResultsProgId()
      
	GetResultsProgId = "VBScript.SearchResultExample.WSC"
       	
End Function

'=================
' Free Method
'=================
Sub Free()

	' do nothing
        
End Sub
    
'==================
' Name property
'==================
Function GetName()

	GetName = m_Name

End function

'=======================
' MaxIterations property
'=======================
Function GetMaxIterations()

       GetMaxIterations = m_maxIters

End Function

Function SetMaxIterations( val )

       m_maxIters = val

End Function

'======================================================
'  Copyright 2006-2010, Analytical Graphics, Inc.          
' =====================================================