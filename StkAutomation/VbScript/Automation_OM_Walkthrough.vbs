' This script walks through the basic functions of the STK Astrogator Object
' Model by building the Hohmann Transfer Using a Targeter tutorial
' exercise, found in the STK Help. A version of this code using C# can be
' found in <STK Install>\CodeSamples\CustomApplications\CSharp\HohmannTransferUsingTargeter


' Declare variables to be used to access for accessing the application and its objects
Dim uiapp, root, sat, ASTG

' Launch a new instance of STK
Set uiapp = CreateObject("STK12.Application")
' or to grab an already open instance of STK
'Set uiapp = GetObject(,"STK12.Application")

' VBScript Note: When a variable will be a handle to an object (as opposed to just holding a
' value), you must use the Set command

' Attach to the STK Object Model
Set root = uiapp.Personality2

' Create a new scenario
root.NewScenario("ASTG_OM_Test")
' if one already exists, you can access it as in the examples below

' Create a new satellite. See STK Programming Interface Help to see that
' the enumeration for a Satellite object is "eSatellite" with a value of 18
Dim eSatellite
eSatellite = 18
Set sat = root.CurrentScenario.Children.New(eSatellite, "ASTG_Sat")
' or connect to an already existing satellite
'Set sat = root.CurrentScenario.Children.Item("Satellite1")

' Set the new Satellite to use Astrogator as the propagator
Dim ePropagatorAstrogator
ePropagatorAstrogator = 12
sat.SetPropagatorType(ePropagatorAstrogator)
' Note that Astrogator satellites by default start with one Initial State
' and one Propagate segment

' Create a handle to the Astrogator portion of the satellites object model
' for convenience
Set ASTG = sat.Propagator


'''
' Adding and Removing segments
'''

' Collections
' In the OM, groupings of the same kind of object are referred to as
' Collections. Examples include Sequences (including the MainSequence and
' Target Sequences) which hold groups of segments, Segments which may hold
' groups of Results, and Propagate Segments which may hold groups of
' Stopping Conditions.
' In general, all Collections have some similar properties and methods and
' will be interacted with the same way. The most common elements of a
' Collection interface are
'   Item(argument) - returns a handle to a particular element of
'   the collection
'   Count - the number of elements in this collection
'   Add(argument) or Insert(argument) - adds new elements to the collection
'   Remove, RemoveAll - removes elements from the collection
' Other methods like Cut, Copy, and Paste may be available depending on the
' kind of collection

' Declare some variables for working with the MCS
Dim MCS, initstate, propagate

' Create a handle to the MCS and remove all existing segments
Set MCS = ASTG.MainSequence
MCS.RemoveAll

' Functions can also be called directly without needing to create a
' separate handle. This will also work:
' ASTG.MainSequence.RemoveAll

''' Define the Initial State '''

' Use the Insert method to add a new Initial State to the MCS. The Insert
' method requires an enumeration as one of its arguments. Enumerations are
' a set of pre-defined options for certain methods and can be found in the
' Help for that given method.
Dim eVASegmentTypeInitialState, eVASegmentTypePropagate, eVASegmentTypeManeuver, eVASegmentTypeTargetSequence
eVASegmentTypeInitialState = 0
eVASegmentTypeManeuver = 2
eVASegmentTypePropagate = 5
eVASegmentTypeTargetSequence = 8

MCS.Insert eVASegmentTypeInitialState,"Inner Orbit","-"

' The Insert command will also return a handle to the segment it creates

Set propagate = MCS.Insert(eVASegmentTypePropagate,"Propagate","-")

'''
' Configuring Segment properties
'''

' Create a handle to the Initial State Segment, set it to use Modified
' Keplerian elements and assign new initial values
Set initstate = MCS.Item("Inner Orbit")
Dim eVAElementTypeModKeplerian
eVAElementTypeModKeplerian = 2
initstate.SetElementType(eVAElementTypeModKeplerian)
initstate.Element.RadiusOfPeriapsis = 6700
initstate.Element.Eccentricity = 0
initstate.Element.Inclination = 0
initstate.Element.RAAN = 0
initstate.Element.ArgOfPeriapsis = 0
initstate.Element.TrueAnomaly = 0

''' Propagate the Parking Orbit '''

' Change Propagate segment color, propagator, and stopping condition trip
' value

' Object Model colors must be set with decimal values, but can be easily
' converted from hex values. Here table with some example values for use within this script.
' Name     RGB            BGR            Hex      Decimal
' Red     255, 0, 0      0, 0, 255      0000ff    255
' Green   0, 255, 0      0, 255, 0      00ff00    65280
' Blue    0, 0, 255      255, 0, 0      ff0000    16711680
' Cyan    0, 255, 255    255, 255, 0    ffff00    16776960
' Yellow  255, 255, 0    0, 255, 255    00ffff    65535
' Magenta 255, 0, 255    255, 0, 255    ff00ff    16711935
' Black   0, 0, 0        0, 0, 0        000000    0
' White   255, 255, 255  255, 255, 255  ffffff    16777215

Dim Red, Green, Blue, Cyan, Yellow, Magenta, Black, White
Red = "0000ff"
Green = "00ff00"
Blue = "ff0000"
Cyan = "ffff00"
Yellow = "00ffff"
Magenta = "ff00ff"
Black = "000000"
White = "ffffff"

propagate.Properties.Color = CLng("&h" & Cyan)

' Change the propagator
propagate.PropagatorName = "Earth Point Mass"

'''
' Configure Stopping Conditions
'''

' Recall Stopping Conditions are also stored as a collection of items
propagate.StoppingConditions.Item("Duration").Properties.Trip = 7200

''' Maneuver into the Transfer Ellipse
''' Define a Target Sequence

' Create variables for working with the target sequence, maneuvers and other segments
Dim ts, dv1, impulsive, thrustVector, dc, xControlParam, roaResult, transferEllipse, dv2, eccResult, outerorbit

' Insert a Target Sequence with a nested Maneuver segment
Set ts = MCS.Insert(eVASegmentTypeTargetSequence,"Start Transfer","-")
' Sequences (including Target and Backward) have their own collection of
' segments
Set dv1 = ts.Segments.Insert(eVASegmentTypeManeuver,"DV1","-")
dv1.Properties.Color = CLng("&h" & Red)

''' Select Variables
Dim eVAManeuverTypeImpulsive
eVAManeuverTypeImpulsive = 0
dv1.SetManeuverType(eVAManeuverTypeImpulsive)
' Create a handle to the impulsive properties of the maneuver
Set impulsive = dv1.Maneuver
Dim eVAAttitudeControlThrustVector
eVAAttitudeControlThrustVector = 4
impulsive.SetAttitudeControlType(eVAAttitudeControlThrustVector)
' Create a handle to the Attitude Control - Thrust Vector properties of the
' maneuver and set the appropriate axes
Set thrustVector = impulsive.AttitudeControl
thrustVector.ThrustAxesName = "Satellite VNC(Earth)"


'''
' Turn on Controls for Search Profiles
'''

' To enable the targeter to vary a given segment property, it must be
' enabled as a control parameter. This is done by the
' EnableControlParameter method which is available on each segment inside a
' target sequence. 
Dim eVAControlManeuverImpulsiveCartesianX
eVAControlManeuverImpulsiveCartesianX = 408
dv1.EnableControlParameter(eVAControlManeuverImpulsiveCartesianX)


'''
' Configure Results
'''

' Segment Results, which can be used as targeter goals, are also stored in a collection
dv1.Results.Add("Keplerian Elems/Radius of Apoapsis")


''' Set up the Targeter
'''
' Configure Targeting
'''

' Targter Profiles are also stored as a collection
Set dc = ts.Profiles.Item("Differential Corrector")

' Create a handle to the targeter control and set its properties
Set xControlParam = dc.ControlParameters.GetControlByPaths("DV1", "ImpulsiveMnvr.Cartesian.X")
xControlParam.Enable = true
xControlParam.MaxStep = 0.3

' Create a handle to the targeter results and set its properties
Set roaResult = dc.Results.GetResultByPaths("DV1", "Radius Of Apoapsis")
roaResult.Enable = true
roaResult.DesiredValue = 42238
roaResult.Tolerance = 0.1

' Set final DC and targeter properties and run modes
dc.MaxIterations = 50
dc.EnableDisplayStatus = true

Dim eVAProfileModeIterate
eVAProfileModeIterate = 0
dc.Mode = eVAProfileModeIterate

Dim eVATargetSeqActionRunActiveProfiles
eVATargetSeqActionRunActiveProfiles = 1
ts.Action = eVATargetSeqActionRunActiveProfiles

''' Propagate the Transfer Orbit to Apogee
Set transferEllipse = MCS.Insert(eVASegmentTypePropagate,"Transfer Ellipse","-")
transferEllipse.PropagatorName = "Earth Point Mass"
'Add an Apoapsis Stopping Condition and remove the Duration Stopping
'Condition
transferEllipse.StoppingConditions.Add("Apoapsis")
transferEllipse.StoppingConditions.Remove("Duration")

''' Maneuver into the Outer Orbit

''' Define another Target Sequence

' Starting here, we will overwrite some existing variables (ts, dc, etc...)
' with a handle to elements in the new target sequence
Set ts = MCS.Insert(eVASegmentTypeTargetSequence,"Finish Transfer","-")
Set dv2 = ts.Segments.Insert(eVASegmentTypeManeuver,"DV2","-")
dv2.Properties.Color = CLng("&h" & Red)

''' Select Variables
dv2.SetManeuverType(eVAManeuverTypeImpulsive)
Set impulsive = dv1.Maneuver
impulsive.SetAttitudeControlType(eVAAttitudeControlThrustVector)
Set thrustVector = impulsive.AttitudeControl
thrustVector.ThrustAxesName = "Satellite VNC(Earth)"
dv2.EnableControlParameter(eVAControlManeuverImpulsiveCartesianX)
dv2.Results.Add("Keplerian Elems/Eccentricity")

''' Set up the Targeter
Set dc = ts.Profiles.Item("Differential Corrector")
Set xControlParam = dc.ControlParameters.GetControlByPaths("DV2", "ImpulsiveMnvr.Cartesian.X")
xControlParam.Enable = true
xControlParam.MaxStep = 0.3
Set eccResult = dc.Results.GetResultByPaths("DV2", "Eccentricity")
eccResult.Enable = true
eccResult.DesiredValue = 0
eccResult.Tolerance = 0.01

' Set final DC and targeter properties and run modes
dc.EnableDisplayStatus = true
dc.Mode = eVAProfileModeIterate
ts.Action = eVATargetSeqActionRunActiveProfiles

''' Propagate the Outer Orbit
Set outerOrbit = MCS.Insert(eVASegmentTypePropagate,"Outer Orbit","-")
outerOrbit.PropagatorName = "Earth Point Mass"
outerOrbit.Properties.Color = CLng("&h" & Yellow)
outerOrbit.StoppingConditions.Item("Duration").Properties.Trip = 86400

''' Running and Analyzing the MCS

' Execute the MCS. This is the equivalent of clicking the "Run" arrow
' button on the MCS toolbar.
ASTG.RunMCS

' Single Segment Mode. There are times when, due to complex mission
' requirements or even the designers preference, the Astrogator MCS
' graphical interface may not be the most efficient solution. For these
' times, Astrogator also supports executing segments and sequences individually, in any
' order specified by your code. Between running segments you can evaluate
' results and change segment properties. This allows the mission designer
' to model trajectories or algorithms which would be impractical in the
' GUI. Note that if executing a sequence or target sequences, the entire
' sequence will run to completion. Implementing custom targeting algorithms
' is usually best done with a Search Plugin.

' Initialize the MCS for Single Segment Mode
ASTG.BeginRun

' Execute a single segment. Note that some kind of initial state segment
' (Initial State, Launch, or Follow)
initstate.Run
propagate.Run
Set ts1 = MCS.Item("Start Transfer")
ts1.Run
transferEllipse.Run
ts.Run
outerOrbit.Run

' Ends the MCS rus
ASTG.EndRun


' Segments consists of three structures which are useful for examining your
' satellite and orbit parameters:
'   Initial State -  The orbit and spacecraft state at the beginning epoch
'   of the segment
'   Final State   -  The orbit and spacecraft state ate the ending epoch of
'   the segment
'   Results       -  The value of any Calc Object selected by the user,
'   evaluated at the ending epoch of the segment

' Create variables for working with reporting and the Component Browser
Dim transferEllipse_IS_Sun_Inertial, compCollection, CalcObjs, CartElems, x, Xmars

' Report the TA at the beginning of the Transfer Ellipse segment
transferEllipse.InitialState.SetElementType(eVAElementTypeModKeplerian)
transferEllipse.FinalState.SetElementType(eVAElementTypeModKeplerian)
MsgBox("Transfer Ellipse True Anomaly: " & transferEllipse.FinalState.Element.TrueAnomaly & " deg" )

' Report the TA at the beginning of the Transfer Ellipse segment, in the
' Sun Inertial frame
Set transferEllipse_IS_Sun_Inertial = transferEllipse.InitialState.GetInFrameName("CentralBody/Sun Inertial")
transferEllipse_IS_Sun_Inertial.SetElementType(eVAElementTypeModKeplerian)
MsgBox("Transfer Ellipse True Anomaly (Sun Inertial): " & transferEllipse_IS_Sun_Inertial.Element.TrueAnomaly & " deg" )

' Add and report a Duration Result on the Transfer Ellipse segment
transferEllipse.Results.Add("Time/Duration")
ASTG.RunMCS
MsgBox("Transfer Ellipse Duration: " & transferEllipse.GetResultValue("Duration") & " sec" )

''' Accessing the Component Browser
Dim eComponentAstrogator
eComponentAstrogator = 2
Set compCollection = root.CurrentScenario.ComponentDirectory.GetComponents(eComponentAstrogator)
Set CalcObjs = compCollection.GetFolder("Calculation Objects")

' Create a copy of the Cartesian Element X that is with respect to Mars
Set CartElems = CalcObjs.GetFolder("Cartesian Elems")
Set X = CartElems.Item("X")
X.CloneObject
CartElems.Item("X").CloneObject
' When using the CloneObject method, the new name will simply be the old name with
' a "1" added to the end
Set Xmars = CartElems.Item("X1")
Xmars.Name = "X Mars"
Xmars.CoordSystemName = "CentralBody/Mars Inertial"

MsgBox("Click OK to close STK")

Set root = Nothing
Set uiapp = Nothing