'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================
Dim m_AgAttrScope
Dim m_pluginSite
Dim m_constantRCS
Dim m_verbose
Dim m_showMsg
Dim m_Rad2Deg
Dim m_enablePol
Dim m_pi
Dim m_halfPi
Dim m_epsilon12
Set m_AgAttrScope = Nothing

Sub Initialize( pluginSite )

   Set m_pluginSite = pluginSite
   m_constantRCS = 1.0
   m_verbose = false
   m_showMsg = true
   m_pi = 3.1415926535897932384626433832795
   m_halfPi = m_pi / 2.0
   m_Rad2Deg = 180.0 / m_pi
   m_enablePol = false
   m_epsilon12 = 0.000000000001
   
End Sub

Function GetIsDynamic()
     GetIsDynamic = false
End Function

Sub ProcessSignals( processSignalsParams )

   Dim primPolSignal
   Set primPolSignal = processSignalsParams.PrimaryPolChannelSignal
   
   Dim primPol
   Set primPol = primPolSignal.Polarization

   Dim orthoPolSignal
   Set orthoPolSignal = processSignalsParams.OrthoPolChannelSignal
   
   Dim orthoPol
   If( Not orthoPolSignal is Nothing) Then
      Set orthPol = orthoPolSignal.Polarization
   Else
      Set orthPol = Nothing
   End If

   If ( m_verbose And m_showMsg) Then
   
      Dim incBodyFixed
      incBodyFixed = processSignalsParams.InicidentBodyFixedVectorArray
      
      Dim refBodyFixed
      refBodyFixed = processSignalsParams.ReflectedBodyFixedVectorArray          
      
      Dim orthSignalParams
      orthSignalParams = "   No orthogonal signal"
      
      If (Not orthoPolSignal Is Nothing) Then
      
        orthSignalParams = "   Time = " & orthoPolSignal.Time & " EpSec" & vbCrLf & _
	                   "   Frequency = " & FormatNumber(orthoPolSignal.Frequency / 1.0e9, 2) & " GHz" & vbCrLf & _
	                   "   Upper Band Limit = " & FormatNumber(orthoPolSignal.UpperBandLimit / 1.0e9, 2) & " GHz" & vbCrLf & _
	                   "   Lower Band Limit = " & FormatNumber(orthoPolSignal.LowerBandLimit / 1.0e9, 2) & " GHz" & vbCrLf & _
                           "   Power = " & FormatNumber(10 * Log10(orthoPolSignal.Power), 2) & " dBW" & vbCrLf   
                                   
	Dim orthoPolStr
	orthoPolStr = "   No polarization"

	If(Not orthoPol is Nothing) Then

           Dim orthPolTypeStr
           orthPolTypeStr = "Unknown"

	   Dim orthPolType
	   orthPolType = orthoPol.Type
	   If ( orthPolType = 1 ) Then
	      orthPolTypeStr = "Linear"
	   ElseIf (orthPolType = 2) Then
	      orthPolTypeStr = "LHC"
	   ElseIf (orthPolType = 3) Then
              orthPolTypeStr = "RHC"
	   ElseIf (orthPolType = 4) Then
	      orthPolTypeStr = "Elliptical"
           End If

	   Dim orthTiltAngle
           orthTiltAngle = FormatNumber(orthoPol.TiltAngle * m_Rad2Deg, 2)

           Dim orthAxialRatio
	   orthAxialRatio = FormatNumber(orthoPol.AxialRatio, 2)

           Dim orthRefAxis
           orthRefAxis = orthoPol.ReferenceAxis

	   Dim orthRefAxisStr
	   orthRefAxisStr = "Unknown"

           If ( orthRefAxis = 1 ) Then
              orthRefAxisStr = "X"
           ElseIf ( orthRefAxis = 2 ) Then
              orthRefAxisStr = "Y"
           ElseIf ( orthRefAxis = 3 ) Then
	      orthRefAxisStr = "Z"
           End If

           orthoPolStr = "   Pol Type = " & orthPolTypeStr & vbCrLf & _
		         "   Pol Tilt Angle = " & orthTiltAngle & " deg" & vbCrLf & _
		         "   Pol Axial Ratio = " & orthAxialRatio & vbCrLf & _
		         "   Pol Ref Axis = " & orthRefAxisStr
	 End If  
	 
	 orthSignalParams = orthSignalParams & orthoPolStr
	 
      End If
      
           
      Dim primPolStr
      primPolStr = "   No polarization"
      
      If(Not primPol is Nothing) Then
         
         Dim primPolTypeStr
         primPolTypeStr = "Unknown"
         
         Dim primPolType
         primPolType = primPol.Type
         If ( primPolType = 1 ) Then
            primPolTypeStr = "Linear"
         ElseIf (primPolType = 2) Then
            primPolTypeStr = "LHC"
         ElseIf (primPolType = 3) Then
            primPolTypeStr = "RHC"
         ElseIf (primPolType = 4) Then
            primPolTypeStr = "Elliptical"
         End If
      
    	 Dim primTiltAngle
    	 primTiltAngle = FormatNumber(primPol.TiltAngle * m_Rad2Deg, 2)
    	 
    	 Dim primAxialRatio
    	 primAxialRatio = FormatNumber(primPol.AxialRatio, 2)
    	 
    	 Dim primRefAxis
    	 primRefAxis = primPol.ReferenceAxis
    	 
    	 Dim primRefAxisStr
    	 primRefAxisStr = "Unknown"
    	 
    	 If ( primRefAxis = 1 ) Then
    	     primRefAxisStr = "X"
    	 ElseIf ( primRefAxis = 2 ) Then
    	     primRefAxisStr = "Y"
    	 ElseIf ( primRefAxis = 3 ) Then
    	     primRefAxisStr = "Z"
    	 End If

         primPolStr = "   Pol Type = " & primPolTypeStr & vbCrLf & _
                      "   Pol Tilt Angle = " & primTiltAngle & " deg" & vbCrLf & _
                      "   Pol Axial Ratio = " & primAxialRatio & vbCrLf & _
                      "   Pol Ref Axis = " & primRefAxisStr
      End If 
      
      Dim msgStr
      msgStr = "Process Signals Parameters:" & vbCrLf & _
               "   Incident Vec Body Fixed: x = " & FormatNumber(incBodyFixed(0), 2) & ", y = " & FormatNumber(incBodyFixed(1), 2) & ", z = " & FormatNumber(incBodyFixed(2), 2) & vbCrLf & _
               "   Reflected Vec Body Fixed: x = " & FormatNumber(refBodyFixed(0), 2) & ", y = " & FormatNumber(refBodyFixed(1), 2) & ", z = " & FormatNumber(refBodyFixed(2), 2) & vbCrLf & _
               "Primary Channel Signal Parameters:" & vbCrLf & _
               "   Time = " & primPolSignal.Time & " EpSec" & vbCrLf & _
	       "   Frequency = " & FormatNumber(primPolSignal.Frequency / 1.0e9, 2) & " GHz" & vbCrLf & _
	       "   Upper Band Limit = " & FormatNumber(primPolSignal.UpperBandLimit / 1.0e9, 2) & " GHz" & vbCrLf & _
	       "   Lower Band Limit = " & FormatNumber(primPolSignal.LowerBandLimit / 1.0e9, 2) & " GHz" & vbCrLf & _
               "   Power = " & FormatNumber(10 * Log10(primPolSignal.Power), 2) & " dBW" & vbCrLf & primPolStr & vbCrLf & _
               "Orthogonal Channel Signal Parameters:" & vbCrLf & orthSignalParams

      MsgBox msgStr
      
      m_showMsg = false
   End If
   
   
   primPolSignal.Power = primPolSignal.Power * m_constantRCS
   primPolSignal.Rcs = m_constantRCS
   
   
   If (Not primPol is Nothing) Then
      Dim newPol
      Set newPol = Nothing
      
      If (m_enablePol) Then
         Set newPol = GetOrthogonalPol(processSignalsParams, primPolSignal)
      End If

      primPolSignal.Polarization = newPol
   End If
   
   If (Not orthoPolSignal Is Nothing) Then
      orthoPolSignal.Power = orthoPolSignal.Power * m_constantRCS
      orthoPolSignal.Rcs = m_constantRCS
      
      If (Not orthPol is Nothing) Then
         Dim newOrthPol
         Set newOrthPol = Nothing
      
         If (m_enablePol) Then
            Set newOrthPol = GetOrthogonalPol(processSignalsParams, orthoPolSignal)
         End If
   
         orthoPolSignal.Polarization = newOrthPol
      End If
      
   End If   
   
   
End Sub

Function GetOrthogonalPol( processSignalsParams, incidentSignal )
      Dim outPol
      Set outPol = Nothing
      
      Dim incPol
      Set incPol = incidentSignal.Polarization
      
      If(Not incPol is Nothing) Then               
         Set outPol = processSignalsParams.ConstructOrthogonalPolarization(incPol)                  
         outPol.ReferenceAxis = 3         
      End If
     
      Set GetOrthogonalPol = outPol
      
End Function

Function Log10(X)
   Log10 = Log(X) / Log(10)
End Function

Sub Compute( computeParams )
   
   computeParams.PrimaryChannelRcs = m_constantRCS
   computeParams.PrimaryChannelRcsCross = 1.0e-20
   computeParams.OrthoChannelRcs = m_constantRCS
   computeParams.OrthoChannelRcsCross = 1.0e-20
   
End Sub

Sub PostCompute( )

End Sub

Sub Free( )

End Sub

Function PreCompute( )

   PreCompute = true
   m_showMsg = true

End Function

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( attrBuilder )

   If( m_AgAttrScope is Nothing ) Then
   
      Set m_AgAttrScope = attrBuilder.NewScope()
      Call attrBuilder.AddQuantityDispatchProperty2( m_AgAttrScope, "ConstantRCS", "ConstantRCS", "ConstantRCS", "Rcs", "dBsm", "sqm", 0)
      Call attrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "Verbose", "Verbose", "Verbose", 0)
      Call attrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "EnablePolarization", "EnablePolarization", "EnablePolarization", 0)

   End If

   Set GetPluginConfig = m_AgAttrScope

End Function  

'===========================
' VerifyPluginConfig method
'===========================
Function VerifyPluginConfig(verifyResult)
   
    Dim Result
    Dim Message

    Result = true
    Message = "Ok"

    verifyResult.Result  = Result
    verifyResult.Message = Message

End Function 

Function GetConstantRCS()

     GetConstantRCS = m_constantRCS

End Function

Function SetConstantRCS(value)

     m_constantRCS = value

End Function

Function GetVerbose()

     GetVerbose = m_verbose
	
End Function

Function SetVerbose(value)

     m_verbose = value
     
End Function

Function GetEnablePolarization()

     GetEnablePolarization = m_enablePol
	
End Function

Function SetEnablePolarization(value)

     m_enablePol = value
     
End Function


'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================
