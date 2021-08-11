# MATLAB Scenario Building Samples

## [ImportFacilities](ImportFacilities)

This script attaches to an open instance of STK12 and imports facilities with position data from an Excel spreadsheet. Units are assumed to be degrees and meters with a header row in the Excel file for ID, LAT, LON, ALT. There is an example facility spreadsheet in the ImportFacilities folder.

Example:

```matlab
ImportFacilities('GroundSites.xlsx')
```

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [RealTimePropagation](RealTimePropagation)

This folder contains four scripts that demonstrate use cases for the realtime propagator in aircraft, missile, and satellite objects. See the ReadMe.txt file included in the RealTimePropagation folder for more information about each script. The data needed to run these scripts is also included in txt files.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [SensorSweepSOLIS](SensorSweepSOLIS)

This script creates a sweeping/raster scan over a list of area targets. The scan speed, width and direction are controllable and will be inserted as a ground vehicle called 'Scan#'. A SOLIS target sequence file can also be created, which can be used to command a spacecraft's attitude to point along the scan pattern. SOLIS can be automatically run with the generated list of scans. A preconfigured Aviator aircraft can alternatively be used to generate the scan pattern. This script relies on GenerateScan.m and LatLonNewPoint.m to create the scan pattern which are included in the SensorSweepSOLIS folder.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [SOLIS](https://www.agi.com/products/stk-specialized-modules/stk-solis)
* Other Scripts: N/A
* Scenario: N/A

---

## [astgSlewToManeuvers.m](astgSlewToManeuvers.m)

When an Astrogator satellite maneuvers by default the manuever will adjust the attitude to align with the thrust direction, but it will not slew. This script allows the satellite to slew into and out of the maneuver. The user specifies the name of the satellite, the pointing during thrusting intervals, the pointing outside of thrusting intervals, and the slew length. These inputs are set on lines 8-24.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://www.agi.com/products/stk-specialized-modules/stk-astrogator), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: [FilterSegmentsByType.m](../General_Utilities/FilterSegmentsByType.m)
* Scenario: N/A

---

## [FigureEight.m](FigureEight.m)

This script creates waypoints for any GreatArc propagated STK vehicle in the shape of a figure eight. The user specifies the name of the object, the major and minor axes of the figure eight, the center latitude and longitude, and a few other basic parameters on lines 10-19.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [FigureEightCircle.m](FigureEightCircle.m)

This script creates an ephemeris file for any GreatArc propagated STK vehicle in the shape of a figure eight constructed of two circles. The user specifies the name of the object, the radius of the two circles, the center latitude and longitude, and a few other basic parameters on lines 10-19.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [makeEllipticalSensor.m](makeEllipticalSensor.m)

This function creates an elliptical sensor pattern and saves it off as a .pattern file. The function when run will prompt the user for the semi-major and semi-minor axes of the ellipse in degrees as well as the rotation angle of the semi-major axis relative to the horizontal.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [MissilePropagation.m](MissilePropagation.m)

This is a simple script to propagate a missile with the basic ballistic propagator. The user specifies the launch and impact locations as well as the propagation times in the input section on lines 5-12. The script also pulls out ground range and altitude from the ground range data provider.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [solisCreateSequenceFromAccess.m](solisCreateSequenceFromAccess.m)

To run this script you must have a Scenario loaded with a propagated satellite, have SOLIS open for this satellite (to create sequence folder directories), have a ground station of any type (facility, target or place). These ground stations can be constrained to provide accurate access metrics. The script will prompt the user for the required inputs.

This sample is used to generate a simple sequence for communicating with a satellite. The Satellite and Uplink Site (place/target/facility) will have access to eachother. At AOS (Acquisition of Signal), the satellite will execute a Communications_ON sequence which readies the vehicle for communicating with the satellite. At LOS (Loss of Signal) the satellite will execute a Communications_OFF sequence which stops the vehicle from communicating. These additional sequences can include any commands to the vehicle and are left as .seq files which allow for anything to be added to them.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [SOLIS](https://www.agi.com/products/stk-specialized-modules/stk-solis)
* Other Scripts: N/A
* Scenario: N/A

---

## [makeEllipticalSensor.m](makeEllipticalSensor.m)

This script creates an elliptical sensor .pattern file for a given semi major axis, semi minor axis and a rotation angle from horizontal. The user is prompted in the console to enter the inputs, and the output file is written to the user's desktop. An alternative approach is to use the sensor Pattern Tool with an elliptical area target.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [Analysis_Workbench_Components.m](Analysis_Workbench_Components.m)

This script serves as an example of how to create each component type within Analysis Workbench. A new STK instance is created with a notional satellite, and one of each component available in the Vector Geometry Tool, Calculation Tool, and Time Tool is initialized.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [MultiSegmentAttitude.m](MultiSegmentAttitude.m)

This script demonstrates the use of the [SetAttitude](https://help.agi.com/stkdevkit/Subsystems/connectCmds/connectCmds.htm#cmd_SetAttitudeProfile.htm) and [AddAttitude](https://help.agi.com/stkdevkit/Subsystems/connectCmds/connectCmds.htm#cmd_AddAttitudeProfile.htm) Connect commands to create different attitude segments. Several different options to set a multi-segment attitude profile are available, including external attitude files, targeted pointing, and built-in profiles.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro)
* Other Scripts: N/A
* Scenario: A scenario with the default satellite and facility as well as an external [attitude (*.a) file](https://help.agi.com/stk/index.htm#stk/importfiles-01.htm).

---

## [MultiSegmentAttitudeLoop.m](MultiSegmentAttitudeLoop.m)

This function demonstrates the ability to store bulk attitude profile data and add multiple segments in a loop. It creates ten sample attitude segments of various types and definitions to a satellite within a scenario.

### Dependencies

* Licenses: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro)
* Other Scripts: N/A
* Scenario: Scenario with a satellite called 'MySatellite'

---
