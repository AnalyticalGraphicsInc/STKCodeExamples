# MATLAB Scenario Building Samples

## [ImportFacilities](ImportFacilities)

This script attaches to an open instance of STK12 and imports facilities with position data from an Excel spreadsheet. Units are assumed to be degrees and meters with a header row in the Excel file for ID, LAT, LON, ALT. There is an example facility spreadsheet in the ImportFacilities folder.

Example:

```matlab
ImportFacilities('GroundSitesExamples.xlsx')
```

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [RealTimePropagation](RealTimePropagation)

This folder contains four scripts that demonstrate use cases for the realtime propagator in aircraft, missile, and satellite objects. See the ReadMe.txt file included in the RealTimePropagation folder for more information about each script. The data needed to run these scripts is also included in txt files.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [SensorSweepSOLIS](SensorSweepSOLIS)

This script creates a sweeping/raster scan over a list of area targets. The scan speed, width and direction are controllable and will be inserted as a ground vehicle called 'Scan#'. A SOLIS target sequence file can also be created, which can be used to command a spacecraft's attitude to point along the scan pattern. SOLIS can be automatically run with the generated list of scans. A preconfigured Aviator aircraft can alternatively be used to generate the scan pattern. This script relies on GenerateScan.m and LatLonNewPoint.m to create the scan pattern which are included in the SensorSweepSOLIS folder.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [SOLIS](https://www.agi.com/products/stk-specialized-modules/stk-solis)
* Other Scripts: N/A
* Scenario: N/A

---

## [SimpleSimulinkExample](SimpleSimulinkExample)

This is a simple tutorial walkthrough of setting up the connection between Simulink and STK using the S-function block.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [astgSlewToManeuvers.m](astgSlewToManeuvers.m)

When an Astrogator satellite maneuvers by default the manuever will adjust the attitude to align with the thrust direction, but it will not slew. This script allows the satellite to slew into and out of the maneuver. The user specifies the name of the satellite, the pointing during thrusting intervals, the pointing outside of thrusting intervals, and the slew length. These inputs are set on lines 8-24.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://www.agi.com/products/stk-specialized-modules/stk-astrogator), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: [FilterSegmentsByType.m](../General_Utilities/FilterSegmentsByType.m)
* Scenario: N/A

---

## [FigureEight.m](FigureEight.m)

This script creates waypoints for any GreatArc propagated STK vehicle in the shape of a figure eight. The user specifies the name of the object, the major and minor axes of the figure eight, the center latitude and longitude, and a few other basic parameters on lines 10-19.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [FigureEightCircle.m](FigureEightCircle.m)

This script creates an ephemeris file for any GreatArc propagated STK vehicle in the shape of a figure eight constructed of two circles. The user specifies the name of the object, the radius of the two circles, the center latitude and longitude, and a few other basic parameters on lines 10-19.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [makeEllipticalSensor.m](makeEllipticalSensor.m)

This function creates an elliptical sensor pattern and saves it off as a .pattern file. The function when run will prompt the user for the semi-major and semi-minor axes of the ellipse in degrees as well as the rotation angle of the semi-major axis relative to the horizontal.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [MissilePropagation.m](MissilePropagation.m)

This is a simple script to propagate a missile with the basic ballistic propagator. The user specifies the launch and impact locations as well as the propagation times in the input section on lines 5-12. The script also pulls out ground range and altitude from the ground range data provider.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [solisCreateSequenceFromAccess.m](solisCreateSequenceFromAccess.m)

To run this script you must have a Scenario loaded with a propagated satellite, have SOLIS open for this satellite (to create sequence folder directories), have a ground station of any type (facility, target or place). These ground stations can be constrained to provide accurate access metrics. The script will prompt the user for the required inputs.

This sample is used to generate a simple sequence for communicating with a satellite. The Satellite and Uplink Site (place/target/facility) will have access to eachother. At AOS (Acquisition of Signal), the satellite will execute a Communications_ON sequence which readies the vehicle for communicating with the satellite. At LOS (Loss of Signal) the satellite will execute a Communications_OFF sequence which stops the vehicle from communicating. These additional sequences can include any commands to the vehicle and are left as .seq files which allow for anything to be added to them.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [SOLIS](https://www.agi.com/products/stk-specialized-modules/stk-solis)
* Other Scripts: N/A
* Scenario: N/A

---

## [makeEllipticalSensor.m](makeEllipticalSensor.m)

This script creates an elliptical sensor .pattern file for a given semi major axis, semi minor axis and a rotation angle from horizontal. The user is prompted in the console to enter the inputs, and the output file is written to the user's desktop. An alternative approach is to use the sensor Pattern Tool with an elliptical area target.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [Analysis_Workbench_Components.m](Analysis_Workbench_Components.m)

This script serves as an example of how to create each component type within Analysis Workbench. A new STK instance is created with a notional satellite, and one of each component available in the Vector Geometry Tool, Calculation Tool, and Time Tool is initialized.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [MultiSegmentAttitude.m](MultiSegmentAttitude.m)

This script demonstrates the use of the [SetAttitude](https://help.agi.com/stkdevkit/Subsystems/connectCmds/connectCmds.htm#cmd_SetAttitudeProfile.htm) and [AddAttitude](https://help.agi.com/stkdevkit/Subsystems/connectCmds/connectCmds.htm#cmd_AddAttitudeProfile.htm) Connect commands to create different attitude segments. Several different options to set a multi-segment attitude profile are available, including external attitude files, targeted pointing, and built-in profiles.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro)
* Other Scripts: N/A
* Scenario: A scenario with the default satellite and facility as well as an external [attitude (*.a) file](https://help.agi.com/stk/index.htm#stk/importfiles-01.htm).

---

## [MultiSegmentAttitudeLoop.m](MultiSegmentAttitudeLoop.m)

This function demonstrates the ability to store bulk attitude profile data and add multiple segments in a loop. It creates ten sample attitude segments of various types and definitions to a satellite within a scenario.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro)
* Other Scripts: N/A
* Scenario: Scenario with a satellite called 'MySatellite'

---

## [ObjectLines.m](ObjectLines.m)

This script will draw lines from an existing object to another existing object at various specified times in the object's path.

How to use:
    1. Create two objects in an existing scenario.
    2. Specify the final animation time in the script.
    3. Specify the list of time instants in seconds relative to the final animation time that you'd like to draw lines from the path of one object to the other.
    4. Set the names of your two objects in the script.
    5. Run the script.  It will create a duplicate object for each time instant and draw lines from it to the other object.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [chainAutomationExample.m](chainAutomationExample.m)

This script is a short demonstration of how to use the STK Object Model to create a Chain and Constellation object, then compute Chain access. This script also creates a cell array that contains all of the AER data for the Chain access computation.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [AstrogatorAPI_Training](AstrogatorAPI_Training)

These scripts are from a training that walked through using MATLAB to create an Astrogator MCS from scratch. The satellite completes a Hohmann transfer from LEO to GEO and also preforms an inclination change. In the version that is not complete, users are required to fill in lines that have headers stating "ACTION REQUIRED". The completed version contains the full script and can be run without any changes.  

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://help.agi.com/stk/index.htm#astrogator.htm)
* Other Scripts: N/A
* Scenario: N/A

---

## [ImportAreaTarget](ImportAreaTarget)

A sample Excel file is included to show you the format for the area target information that you want to import into STK. The script will read the Excel file and create an area target object in STK based on the lat/lon points defined in the spreadsheet. Type 'help ImportAreaTarget' in the Matlab command window for a description on how to use the function.

Example:

```matlab
ImportFacilities('USA.xlsx', 'UnitedStates')
```

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [ObjectModelTutorial.m](ObjectModelTutorial.m)

A basic example of using the object model and connect syntax to control STK from MATLAB

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [Coverage_FOM_GridInspector_CodeHelp.m](Coverage_FOM_GridInspector_CodeHelp.m)

This MATLAB COM script will create a simple satellite scenario and calculate the revisit time for that satellite using Coverage. The data will be returned and parsed in MATLAB. This script uses Object Model commands to create the scenario, it's objects, and return the values to MATLAB. Additionally this uses Grid Inspector Tool. 

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Coverage](https://www.agi.com/products/stk-systems-bundle/stk-coverage)
* Other Scripts: N/A
* Scenario: Any open scenario

---

## [PNT_and_Object_Coverage_Automation.m](PNT_and_Object_Coverage_Automation.m)

This MATLAB script populates and automates an example position, navigation, and timing (PNT) scenario containing a test aircraft and several other objects which we'll call pseudolites. These other objects consist of two aircraft and six ground stations which 'observe' the test aircraft and seek to determine its position. Dilution of Precision (DOP) and Navigation Accuracy calculations are completed using single-object coverage. The data is then exported into MATLAB where a plot is generated for each solution.

### Dependencies

* Capabilities: Free, [Pro](https://www.agi.com/products/stk/pro), [Integration](https://www.agi.com/capabilities/integration), [Coverage](https://www.agi.com/capabilities/coverage)
* Other Scripts: N/A
* Scenario: N/A

---

## [Aviator_ObjectModel_CarrierLanding.py](Aviator_ObjectModel_CarrierLanding.py)

Creates a scenario that demonstrates an aircraft carrier landing using two Aviator aircraft with custom procedures. All inputs are available for editing, though this is better used as a reference for planning Aviator missions programmatically.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Aviator](https://www.agi.com/products/stk-specialized-modules/stk-aviator)
* Other Scripts: N/A
* Scenario: N/A

---