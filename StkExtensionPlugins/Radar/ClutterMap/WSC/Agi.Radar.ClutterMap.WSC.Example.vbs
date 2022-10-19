'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================
Dim m_AgAttrScope
Dim m_pluginSite
Dim m_constantClutterCoef
Dim m_verbose
Dim m_showMsg
Dim m_Rad2Deg
Dim m_coefDict
Dim m_pi
Dim m_halfPi
Dim m_applyGrazeMask

Set m_AgAttrScope = Nothing
m_pi = 3.1415926535897932384626433832795
m_halfPi = m_pi / 2.0

Sub Initialize( pluginSite )

   Set m_pluginSite = pluginSite
   m_constantClutterCoef = 1.0
   m_verbose = false
   m_applyGrazeMask = false
   m_showMsg = true
   m_Rad2Deg = 180.0 / m_pi
   
End Sub

Function PreCompute( )

   PreCompute = true	
   m_showMsg = true
   
   If (m_verbose) Then
   
      Set m_coefDict = CreateObject("Scripting.Dictionary")

   End If
   
End Function

Sub Compute( computeParams )
	   
   Dim radarLink
   Set radarLink = computeParams.RadarLink
   
   Dim radarLinkGeom
   Set radarLinkGeom = radarLink.Geometry
   
   Dim tgtPosVel
   Set tgtPosVel = radarLinkGeom.TargetPosVelProvider
   
   Dim tgtTime
   tgtTime = tgtPosVel.CurrentTime
   
   Dim tgtPos
   tgtPos = tgtPosVel.PositionCBFArray
   
   Dim tgtLLA
   tgtLLA = tgtPosVel.PositionLLAArray
   
   Dim tgtVel
   tgtVel = tgtPosVel.VelocityCBFArray
   
   Dim xmtPosVel
   Set xmtPosVel = radarLinkGeom.TransmitRadarPosVelProvider
   
   Dim xmtTime
   xmtTime = xmtPosVel.CurrentTime

   Dim xmtPos
   xmtPos = xmtPosVel.PositionCBFArray

   Dim xmtLLA
   xmtLLA = xmtPosVel.PositionLLAArray
     
   Dim xmtVel
   xmtVel = xmtPosVel.VelocityCBFArray
   
   Dim rcvPosVel
   Set rcvPosVel = radarLinkGeom.ReceiveRadarPosVelProvider
   
   Dim rcvTime
   rcvTime = rcvPosVel.CurrentTime

   Dim rcvPos
   rcvPos = rcvPosVel.PositionCBFArray

   Dim rcvLLA
   rcvLLA = rcvPosVel.PositionLLAArray
     
   Dim rcvVel
   rcvVel = rcvPosVel.VelocityCBFArray
   
   Dim rcvRange
   rcvRange = radarLinkGeom.ReceiveRadarRange

   Dim rcvAngleRate
   rcvAngleRate = radarLinkGeom.ReceiveRadarAngleRate

   Dim rcvRangeRate
   rcvRangeRate = radarLinkGeom.ReceiveRadarRangeRate

   Dim rcvConeAngle
   rcvConeAngle = radarLinkGeom.ReceiveRadarConeAngle

   Dim rcvPropTime
   rcvPropTime = radarLinkGeom.ReceiveRadarPropTime

   Dim xmtRange
   xmtRange = radarLinkGeom.TransmitRadarRange

   Dim xmtAngleRate
   xmtAngleRate = radarLinkGeom.TransmitRadarAngleRate

   Dim xmtRangeRate
   xmtRangeRate = radarLinkGeom.TransmitRadarRangeRate

   Dim xmtConeAngle
   xmtConeAngle = radarLinkGeom.TransmitRadarConeAngle

   Dim xmtPropTime
   xmtPropTime = radarLinkGeom.TransmitRadarPropTime

   Dim rangeSum
   rangeSum = radarLinkGeom.RangeSum

   Dim closure
   closure = radarLinkGeom.Closure

   Dim mlcVel
   mlcVel = radarLinkGeom.MLCVelocity

   Dim bistatAngle
   bistatAngle = radarLinkGeom.BistaticAngle

   Dim incAz
   incAz = radarLinkGeom.IncidentAzimuth

   Dim incEl
   incEl = radarLinkGeom.IncidentElevation

   Dim refAz
   refAz = radarLinkGeom.ReflectedAzimuth

   Dim refEl
   refEl = radarLinkGeom.ReflectedElevation

   Dim xyAngleRate
   xyAngleRate = radarLinkGeom.XYAngleRate   
      
   Dim clutterPatch
   Set clutterPatch = computeParams.ClutterPatch
      
   Dim clutterPatchPosVel
   Set clutterPatchPosVel = clutterPatch.PosVelProvider
   
   Dim clutterPatchPosCBF
   clutterPatchPosCBF = clutterPatchPosVel.PositionCBFArray
   
   Dim clutterPatchPosLLA
   clutterPatchPosLLA = clutterPatchPosVel.PositionLLAArray
   
   Dim signal
   Set signal = computeParams.Signal
   
   Dim polarization
   Set polarization = signal.Polarization
   
   If ( m_verbose And m_showMsg) Then
         
      Dim patchCbfX, patchCbfY, patchCbfZ
      patchCbfX = FormatNumber(clutterPatchPosCBF(0) / 1000, 2)
      patchCbfY = FormatNumber(clutterPatchPosCBF(1) / 1000, 2)
      patchCbfZ = FormatNumber(clutterPatchPosCBF(2) / 1000, 2)
   
      Dim patchLatDeg, patchLonDeg, patchAlt
      patchLatDeg = FormatNumber(clutterPatchPosLLA(0) * m_Rad2Deg, 2)
      patchLonDeg = FormatNumber(clutterPatchPosLLA(1) * m_Rad2Deg, 2)
      patchAlt = FormatNumber(clutterPatchPosLLA(2) / 1000, 2)  
      
      Dim tgtCbfX, tgtCbfY, tgtCbfZ
      tgtCbfX = FormatNumber(tgtPos(0) / 1000, 2)
      tgtCbfY = FormatNumber(tgtPos(1) / 1000, 2)
      tgtCbfZ = FormatNumber(tgtPos(2) / 1000, 2)
   
      Dim tgtLatDeg, tgtLonDeg, tgtAlt
      tgtLatDeg = FormatNumber(tgtLLA(0) * m_Rad2Deg, 2)
      tgtLonDeg = FormatNumber(tgtLLA(1) * m_Rad2Deg, 2)
      tgtAlt = FormatNumber(tgtLLA(2) / 1000, 2) 
      
      Dim xmtCbfX, xmtCbfY, xmtCbfZ
      xmtCbfX = FormatNumber(xmtPos(0) / 1000, 2)
      xmtCbfY = FormatNumber(xmtPos(1) / 1000, 2)
      xmtCbfZ = FormatNumber(xmtPos(2) / 1000, 2)
   
      Dim xmtLatDeg, xmtLonDeg, xmtAlt
      xmtLatDeg = FormatNumber(xmtLLA(0) * m_Rad2Deg, 2)
      xmtLonDeg = FormatNumber(xmtLLA(1) * m_Rad2Deg, 2)
      xmtAlt = FormatNumber(xmtLLA(2) / 1000, 2) 
      
      Dim rcvCbfX, rcvCbfY, rcvCbfZ
      rcvCbfX = FormatNumber(rcvPos(0) / 1000, 2)
      rcvCbfY = FormatNumber(rcvPos(1) / 1000, 2)
      rcvCbfZ = FormatNumber(rcvPos(2) / 1000, 2)
   
      Dim rcvLatDeg, rcvLonDeg, rcvAlt
      rcvLatDeg = FormatNumber(rcvLLA(0) * m_Rad2Deg, 2)
      rcvLonDeg = FormatNumber(rcvLLA(1) * m_Rad2Deg, 2)
      rcvAlt = FormatNumber(rcvLLA(2) / 1000, 2) 
      
      Dim tgtTerrainHeigh
      tgtTerrainHeigh = tgtPosVel.GetTerrainHeight(1)
      
      Dim terrainHeight
      terrainHeight = clutterPatchPosVel.GetTerrainHeight(1)
      
      Dim rcvTerrainHeigh
      rcvTerrainHeigh = rcvPosVel.GetTerrainHeight(1)
      
      Dim xmtTerrainHeigh
      xmtTerrainHeigh = xmtPosVel.GetTerrainHeight(1)
      
      Dim msgBoxStr
      msgBoxStr =  "Patch Parameters:" & vbCrLf & _
                   "Pos CBF: x = " & patchCbfX & " km, y = " & patchCbfY & " km, z = " & patchCbfZ & " km" & vbCrLf & _
                   "Pos LLA: lat = " & patchLatDeg & " deg, lon = " & patchLonDeg & " deg, alt = " & patchAlt & " km" & vbCrLf & _
                   "Area = " & clutterPatch.Area & vbCrLf & _
                   "Terrain Height = " & terrainHeight & " m" & vbCrLf & _
                   vbCrLf & "Target Parameters:" & vbCrLf & _
                   "Time = " & tgtTime & " EpSec" & vbCrLf & _
                   "Pos CBF: x = " & tgtCbfX & " km, y = " & tgtCbfY & " km, z = " & tgtCbfZ & " km" & vbCrLf & _
                   "Pos LLA: lat = " & tgtLatDeg & " deg, lon = " & tgtLonDeg & " deg, alt = " & tgtAlt & " km" & vbCrLf & _
                   "Terrain Height = " & tgtTerrainHeigh & " m" & vbCrLf & _
                   vbCrLf & "Transmit Radar Parameters:" & vbCrLf & _
                   "Time = " & xmtTime & " EpSec" & vbCrLf & _
                   "Pos CBF: x = " & xmtCbfX & " km, y = " & xmtCbfY & " km, z = " & xmtCbfZ & " km" & vbCrLf & _
                   "Pos LLA: lat = " & xmtLatDeg & " deg, lon = " & xmtLonDeg & " deg, alt = " & xmtAlt & " km" & vbCrLf & _
                   "Terrain Height = " & xmtTerrainHeigh & " m" & vbCrLf & _
                   vbCrLf & "Receive Radar Parameters:" & vbCrLf & _
                   "Time = " & rcvTime & " EpSec" & vbCrLf & _
                   "Pos CBF: x = " & rcvCbfX & " km, y = " & rcvCbfY & " km, z = " & rcvCbfZ & " km" & vbCrLf & _
                   "Pos LLA: lat = " & rcvLatDeg & " deg, lon = " & rcvLonDeg & " deg, alt = " & rcvAlt & " km" & vbCrLf & _
                   "Terrain Height = " & rcvTerrainHeigh & " m" & vbCrLf
                   
      MsgBox msgBoxStr             
                   
      msgBoxStr =  vbCrLf & "Link Geometry:" & vbCrLf & _
                   "Receive Range = " & rcvRange & vbCrLf & _
                   "Receive Angle Rate = " & rcvAngleRate & vbCrLf & _
                   "Receive Range Rate = " & rcvRangeRate & vbCrLf & _
                   "Receive Cone Angle = " & rcvConeAngle & vbCrLf & _
                   "Receive Prop Time = " & rcvPropTime & vbCrLf & _
                   "Transmit Range = " & xmtRange & vbCrLf & _
                   "Transmit Angle Rate = " & xmtAngleRate & vbCrLf & _
                   "Transmit Range Rate = " & xmtRangeRate & vbCrLf & _
                   "Transmit Cone Angle = " & xmtConeAngle & vbCrLf & _
                   "Transmit Prop Time = " & xmtPropTime & vbCrLf & _
                   "Range Sum = " & rangeSum & vbCrLf & _
                   "Closure = " & closure & vbCrLf & _
                   "MLC Velocity = " & mlcVel & vbCrLf & _
                   "Bistatic Angle = " & bistatAngle & vbCrLf & _
                   "Incident Azimuth = " & incAz & vbCrLf & _
                   "Incident Elevation = " & incEl & vbCrLf & _
                   "Reflected Azimuth = " & refAz & vbCrLf & _
                   "Reflected Elevation = " & refEl & vbCrLf & _
                   "XY Angle Rate = " & xyAngleRate & vbCrLf
                   
     MsgBox msgBoxStr
     
     msgBoxStr =   vbCrLf & "Signal Parameters:" & vbCrLf & _
                   "Time = " & signal.Time & " EpSec" & vbCrLf & _
                   "Frequency = " & FormatNumber(signal.Frequency / 1.0e9, 2) & " GHz" & vbCrLf & _
                   "Upper Band Limit = " & FormatNumber(signal.UpperBandLimit / 1.0e9, 2) & " GHz" & vbCrLf & _
                   "Lower Band Limit = " & FormatNumber(signal.LowerBandLimit / 1.0e9, 2) & " GHz" & vbCrLf & _
                   "Power = " & FormatNumber(10 * Log10(signal.Power), 2) & " dBW"                  
      
      If(Not polarization is Nothing) Then
         
         Dim polTypeStr
         polTypeStr = "Unknown"
         
         Dim polType
         polType = polarization.Type
         If ( polType = 1 ) Then
            polTypeStr = "Linear"
         ElseIf (polType = 2) Then
            polTypeStr = "LHC"
         ElseIf (polType = 3) Then
            polTypeStr = "RHC"
         ElseIf (polType = 4) Then
            polTypeStr = "Elliptical"
         End If
      
    	 Dim tiltAngle
    	 tiltAngle = FormatNumber(polarization.TiltAngle * m_Rad2Deg, 2)
    	 
    	 Dim axialRatio
    	 axialRatio = FormatNumber(polarization.AxialRatio, 2)
    	 
    	 Dim refAxis
    	 refAxis = polarization.ReferenceAxis
    	 
    	 Dim refAxisStr
    	 refAxisStr = "Unknown"
    	 
    	 If ( refAxis = 1 ) Then
    	     refAxisStr = "X"
    	 ElseIf ( refAxis = 2 ) Then
    	     refAxisStr = "Y"
    	 ElseIf ( refAxis = 3 ) Then
    	     refAxisStr = "Z"
    	 End If

         msgBoxStr = msgBoxStr & vbCrLf & _
                     "Pol Type = " & polTypeStr & vbCrLf & _
                     "Pol Tilt Angle = " & tiltAngle & " deg" & vbCrLf & _
                     "Pol Axial Ratio = " & axialRatio & vbCrLf & _
                     "Pol Ref Axis = " & refAxisStr
      End If
                 
      MsgBox msgBoxStr
                
      m_showMsg = false
      
   End If
   
   Dim relPosCbf(3)
   relPosCbf(0) = clutterPatchPosCBF(0) - rcvPos(0)
   relPosCbf(1) = clutterPatchPosCBF(1) - rcvPos(1)
   relPosCbf(2) = clutterPatchPosCBF(2) - rcvPos(2)
   
   Dim surfNorm
   surfNorm = clutterPatchPosVel.SurfaceNormalDeticArray
   
   Dim grazAngle
   grazAngle = AngleBetween(surfNorm, relPosCbf) - m_halfPi
   
   Dim clutterCoef
   clutterCoef = m_constantClutterCoef
   
   If ( m_applyGrazeMask ) Then
   
       clutterCoef = clutterCoef * (grazAngle / m_halfPi)
       
   End If
   
   If (m_verbose) Then
   
      Dim dictVal(2)
      dictVal(0) = grazAngle * m_Rad2Deg
      dictVal(1) = clutterCoef
   
      If( Not m_coefDict.Exists(signal.Time) ) Then
         m_coefDict.Add signal.Time, dictVal
      End If
      
   End If
   
   signal.Power = signal.Power * clutterCoef * clutterPatch.Area
   
   If(Not polarization is Nothing) Then
   
      signal.Polarization = computeParams.ConstructPolarization(1)
      'signal.Polarization = Nothing
   
   End If
  
End Sub

Sub PostCompute( )

   If (m_verbose) Then
   
      Dim time
      Dim times
      Dim dictVal
      Dim msgBoxStr
      msgBoxStr = "Computed Clutter Coefficients:" & vbCrLf
      msgBoxStr = msgBoxStr & "Time (EpSec), Grazing Angle (deg), Clutter Coefficient" & vbCrLf
   
      times = m_coefDict.Keys
   
      For Each time in times
   	    dictVal = m_coefDict.Item(time)
            msgBoxStr = msgBoxStr & time & ", " & dictVal(0) & ", " & dictVal(1) & vbCrLf
      Next
   
      MsgBox msgBoxStr
      
   End If
   
End Sub

Sub Free( )

End Sub

Function Log10(X)
   Log10 = Log(X) / Log(10)
End Function

Function Magnitude(vec)
   Magnitude = Sqr(vec(0) * vec(0) + vec(1) * vec(1) + vec(2) *  vec(2))
End Function

Function CrossProduct(vec1, vec2)
 
   Dim crossProdVec(3)
   crossProdVec(0) = vec1(1) * vec2(2) - vec1(2) * vec2(1)
   crossProdVec(1) = vec1(2) * vec2(0) - vec1(0) * vec2(2)
   crossProdVec(2) = vec1(0) * vec2(1) - vec1(1) * vec2(0)
   
   CrossProduct = crossProdVec

End Function

Function DotProduct(vec1, vec2)
   DotProduct = vec1(0) * vec2(0) + vec1(1) * vec2(1) + vec1(2) * vec2(2)
End Function

Function AngleBetween(vec1, vec2)

   Dim cross
   Dim sinTheta, cosTheta
   
   cross = CrossProduct(vec1, vec2)
   sinTheta = Magnitude(cross)
   cosTheta = DotProduct(vec1, vec2)
   
   AngleBetween = Abs(Atan2(sinTheta, cosTheta))

End Function

Function Atan2(ys, xs) 
    Dim theta 
    If xs <> 0 Then 
        theta = Atn(ys / xs) 
        If xs < 0 Then 
            theta = theta + m_pi 
        End If 
    Else 
        If ys < 0 Then 
            theta = 3 * m_pi / 2 '90 
        Else 
            theta = m_pi / 2 '270 
        End If 
    End If 
    Atan2 = theta 
End Function 

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( attrBuilder )

   If( m_AgAttrScope is Nothing ) Then
      Set m_AgAttrScope = attrBuilder.NewScope()

      Call attrBuilder.AddQuantityDispatchProperty2( m_AgAttrScope, "ConstantClutterCoefficient", "ConstantClutterCoefficient", "ConstantClutterCoefficient", "Ratio", "dB", "units", 0)
      Call attrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "ApplyGrazeMask", "ApplyGrazeMask", "ApplyGrazeMask", 0)
      Call attrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "Verbose", "Verbose", "Verbose", 0)

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
    Message = "OK"

    verifyResult.Result = Result
    verifyResult.Message = Message

End Function 

Function GetConstantClutterCoefficient()

     GetConstantClutterCoefficient = m_constantClutterCoef

End Function

Function SetConstantClutterCoefficient(value)

     m_constantClutterCoef = value

End Function

Function GetVerbose()

     GetVerbose = m_verbose
	
End Function

Function SetVerbose(value)

     m_verbose = value
     
End Function

Function GetApplyGrazeMask()

     GetApplyGrazeMask = m_applyGrazeMask
	
End Function

Function SetApplyGrazeMask(value)

     m_applyGrazeMask = value
     
End Function
'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================
