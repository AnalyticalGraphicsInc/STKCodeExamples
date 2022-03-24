'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================
Dim m_AgAttrScope
Set m_AgAttrScope = Nothing
Dim m_pluginSite
Dim m_verbose
Dim m_showMsg
Dim m_patchArea
Dim m_offsetAngle
Dim m_sinOffset
Dim m_cosOffset
Dim m_intersectParams
Dim m_pi
m_pi = 3.1415926535897932384626433832795
Dim m_Rad2Deg
m_Rad2Deg = 180.0 / m_pi

Sub Register( regInfo )

   regInfo.ValidRadarSystems = 1
   
End Sub

Sub Initialize( pluginSite )

   Set m_pluginSite = pluginSite
   m_verbose = false
   m_showMsg = true
   m_patchArea = 1.0
   m_offsetAngle = 0.01745329251994329576923690768489
   m_sinOffset = Sin(1.5707963267948966192313216916398 - m_offsetAngle)
   m_cosOffset = Cos(1.5707963267948966192313216916398 - m_offsetAngle)
   
End Sub

Function PreCompute( )

   m_showMsg = true
   Set m_intersectParams = CreateObject("AgStkRadarPlugin12.AgSTKRadarCBIntersectComputeParams")
   PreCompute = true	
   
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
   
   Dim signal
   Set signal = computeParams.Signal
   
   Dim polarization
   Set polarization = signal.Polarization
    
    
   Dim clutterPatch1
   Dim clutterPatch2
   Dim clutterPatch3
   Dim clutterPatch4
   
   Set clutterPatch1 = Nothing
   Set clutterPatch2 = Nothing
   Set clutterPatch3 = Nothing
   Set clutterPatch4 = Nothing

   Call m_intersectParams.SetBasePositionCBF(xmtPos(0), xmtPos(1), xmtPos(2))
   
   Dim pt1Cbf
   pt1Cbf = xmtPosVel.ConvertBodyCartesianToCBFCartesianArray(m_cosOffset, 0.0, m_sinOffset)
   
   Call m_intersectParams.SetDirectionCBF(pt1Cbf(0), pt1Cbf(1), pt1Cbf(2))
   
   Dim p1Intersect
   Set p1Intersect = xmtPosVel.ComputeCentralBodyIntersect(m_intersectParams)
   
   If (p1Intersect.IntersectionFound) Then
   
      Set clutterPatch1 = computeParams.ClutterPatches.Add()
      
      Dim interceptP1
      interceptP1 = p1Intersect.Intercept1CBFArray
      Call clutterPatch1.SetPositionCBF(interceptP1(0), interceptP1(1), interceptP1(2))
      clutterPatch1.Area = m_patchArea   
   
   End If
   
   Dim pt2Cbf
   pt2Cbf = xmtPosVel.ConvertBodyCartesianToCBFCartesianArray(-m_cosOffset, 0.0, m_sinOffset)
   
   Call m_intersectParams.SetDirectionCBF(pt2Cbf(0), pt2Cbf(1), pt2Cbf(2))
   
   Dim p2Intersect
   Set p2Intersect = xmtPosVel.ComputeCentralBodyIntersect(m_intersectParams)
   
   If (p2Intersect.IntersectionFound) Then
   
      Set clutterPatch2 = computeParams.ClutterPatches.Add()
      
      Dim interceptP2
      interceptP2 = p2Intersect.Intercept1CBFArray
      Call clutterPatch2.SetPositionCBF(interceptP2(0), interceptP2(1), interceptP2(2))
      clutterPatch2.Area = m_patchArea   
   
   End If
   
   Dim pt3Cbf
   pt3Cbf = xmtPosVel.ConvertBodyCartesianToCBFCartesianArray(0.0, m_cosOffset, m_sinOffset)
   
   Call m_intersectParams.SetDirectionCBF(pt3Cbf(0), pt3Cbf(1), pt3Cbf(2))
   
   Dim p3Intersect
   Set p3Intersect = xmtPosVel.ComputeCentralBodyIntersect(m_intersectParams)
   
   If (p3Intersect.IntersectionFound) Then
   
      Set clutterPatch3 = computeParams.ClutterPatches.Add()
      
      Dim interceptP3
      interceptP3 = p3Intersect.Intercept1CBFArray
      Call clutterPatch3.SetPositionCBF(interceptP3(0), interceptP3(1), interceptP3(2))
      clutterPatch3.Area = m_patchArea   
   
   End If
   
   Dim pt4Cbf
   pt4Cbf = xmtPosVel.ConvertBodyCartesianToCBFCartesianArray(0.0, -m_cosOffset, m_sinOffset)
   
   Call m_intersectParams.SetDirectionCBF(pt4Cbf(0), pt4Cbf(1), pt4Cbf(2))
   
   Dim p4Intersect
   Set p4Intersect = xmtPosVel.ComputeCentralBodyIntersect(m_intersectParams)
   
   If (p4Intersect.IntersectionFound) Then
   
      Set clutterPatch4 = computeParams.ClutterPatches.Add()
      
      Dim interceptP4
      interceptP4 = p4Intersect.Intercept1CBFArray
      Call clutterPatch4.SetPositionCBF(interceptP4(0), interceptP4(1), interceptP4(2))
      clutterPatch4.Area = m_patchArea   
   
   End If
   
   If ( m_verbose And m_showMsg) Then
               
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
            
      Dim rcvTerrainHeigh
      rcvTerrainHeigh = rcvPosVel.GetTerrainHeight(1)
      
      Dim xmtTerrainHeigh
      xmtTerrainHeigh = xmtPosVel.GetTerrainHeight(1)
      
      Dim msgBoxStr
      msgBoxStr =  "Target Parameters:" & vbCrLf & _
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
      
      msgBoxStr = "Clutter Patch Parameters:" & vbCrLf
      
      If ( Not clutterPatch1 is Nothing) Then
      
          Dim clPt1Pos
          clPt1Pos = clutterPatch1.PosVelProvider.PositionLLAArray
          
	  Dim clPt1Pos_X
	  clPt1Pos_X = FormatNumber(clPt1Pos(0) * m_Rad2Deg, 2)
	  Dim clPt1Pos_Y
	  clPt1Pos_Y = FormatNumber(clPt1Pos(1) * m_Rad2Deg, 2)
	  Dim clPt1Pos_Z
	  clPt1Pos_Z = FormatNumber(clPt1Pos(2) * m_Rad2Deg, 2)

          
          msgBoxStr = msgBoxStr & "   Patch1 Position: " & clPt1Pos_X & _
                            ", " & clPt1Pos_Y & ", " & clPt1Pos_Z & vbCrLf
        
       End If
                 
      If ( Not clutterPatch2 is Nothing) Then
      
          Dim clPt2Pos
          clPt2Pos = clutterPatch2.PosVelProvider.PositionLLAArray
          
	  Dim clPt2Pos_X
	  clPt2Pos_X = FormatNumber(clPt2Pos(0) * m_Rad2Deg, 2)
	  Dim clPt2Pos_Y
	  clPt2Pos_Y = FormatNumber(clPt2Pos(1) * m_Rad2Deg, 2)
	  Dim clPt2Pos_Z
	  clPt2Pos_Z = FormatNumber(clPt2Pos(2) * m_Rad2Deg, 2)

          
          msgBoxStr = msgBoxStr & "   Patch2 Position: " & clPt2Pos_X & _
                            ", " & clPt2Pos_Y & ", " & clPt2Pos_Z & vbCrLf
        
       End If
      
      If ( Not clutterPatch3 is Nothing) Then
      
          Dim clPt3Pos
          clPt3Pos = clutterPatch3.PosVelProvider.PositionLLAArray
          
	  Dim clPt3Pos_X
	  clPt3Pos_X = FormatNumber(clPt3Pos(0) * m_Rad2Deg, 2)
	  Dim clPt3Pos_Y
	  clPt3Pos_Y = FormatNumber(clPt3Pos(1) * m_Rad2Deg, 2)
	  Dim clPt3Pos_Z
	  clPt3Pos_Z = FormatNumber(clPt3Pos(2) * m_Rad2Deg, 2)

          
          msgBoxStr = msgBoxStr & "   Patch3 Position: " & clPt3Pos_X & _
                            ", " & clPt3Pos_Y & ", " & clPt3Pos_Z & vbCrLf
        
       End If
       
      If ( Not clutterPatch4 is Nothing) Then
      
          Dim clPt4Pos
          clPt4Pos = clutterPatch4.PosVelProvider.PositionLLAArray
          
	  Dim clPt4Pos_X
	  clPt4Pos_X = FormatNumber(clPt4Pos(0) * m_Rad2Deg, 2)
	  Dim clPt4Pos_Y
	  clPt4Pos_Y = FormatNumber(clPt4Pos(1) * m_Rad2Deg, 2)
	  Dim clPt4Pos_Z
	  clPt4Pos_Z = FormatNumber(clPt4Pos(2) * m_Rad2Deg, 2)

          
          msgBoxStr = msgBoxStr & "   Patch4 Position: " & clPt4Pos_X & _
                            ", " & clPt4Pos_Y & ", " & clPt4Pos_Z & vbCrLf
        
       End If
       
       MsgBox msgBoxStr

      m_showMsg = false
      
   End If
  
End Sub

Sub PostCompute( )
   
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

      Call attrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "Verbose", "Verbose", "Verbose", 0)
      Call attrBuilder.AddQuantityDispatchProperty2( m_AgAttrScope, "PatchArea", "PatchArea", "PatchArea", "Rcs", "sqm", "sqm", 0)
      Call attrBuilder.AddQuantityDispatchProperty2( m_AgAttrScope, "OffsetAngle", "OffsetAngle", "OffsetAngle", "Angle", "deg", "rad", 0)

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

Function GetVerbose()

     GetVerbose = m_verbose
	
End Function

Function SetVerbose(value)

     m_verbose = value
     
End Function

Function GetPatchArea()
  
   GetPatchArea = m_patchArea
   
End Function

Function SetPatchArea( area )

   m_patchArea = area
   
End Function

Function GetOffsetAngle()
  
   GetOffsetAngle = m_offsetAngle
   
End Function

Function SetOffsetAngle( offsetAngle )

   m_offsetAngle = offsetAngle
   m_sinOffset = Sin(1.5707963267948966192313216916398 - m_offsetAngle)
   m_cosOffset = Cos(1.5707963267948966192313216916398 - m_offsetAngle)
   
End Function
'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================
