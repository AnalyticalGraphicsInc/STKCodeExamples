# MATLAB General Utility Samples

## [convertTLEState.m](convertTLEState.m)

This simple function requires an open STK 12 scenario with a satellite that has been propagated with the SGP4 propagator. This function will pull out the initial state of the SGP4 satellite in the ICRF frame then create and propagate a new satellite of the specified propagator type using that initial state. Place this file in your MATLAB working directory and then you can call it within other MATLAB scripts or directly from the command window.

**NOTE**: You must use single quotations to specify these string inputs, this is due to limitations of the SetPropagatorType method

Example:

```matlab
convertTLEState('Satellite1','ePropagatorHPOP')
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [createEphemerisFile.m](createEphemerisFile.m)

Creates an ephemeris file for an STK Object in an Analysis Workbench Coordinate System. The script can also automatically create a new object using the new ephemeris file.

**NOTE**: Be sure to update the input section of the script before running

Example objects and coordinate systems:

```matlab
objPath = 'Aircraft/Aircraft1';
objPath = 'Missile/Missile1';
objPath = 'LaunchVehicle/LaunchVehicle1';
coordSys = 'CentralBody/Earth ICRF';
coordSys = 'CentralBody/Moon L2';
coordSys = 'Satellite/Satellite2 RIC';
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Analysis Workbench](https://p.widencdn.net/cdz693/Analysis-Workbench-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [filterObjectsByType.m](filterObjectsByType.m)

This function grabs all objects in your STK scenario of a certain specified type and returns their paths. The user also has the option to specify a common string to filter objects by name.

Example: Find all satellites in the scenario

```matlab
FilterObjectsByType('Satellite', '')
```

Example: Find all satellites in the scenario that start with gps

```matlab
FilterObjectsByType('Satellite', 'gps')
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [getDCM.m](getDCM.m)

Allows users to output a quaternion from the Axes Choose Axes data provider and convert that quaternion into a Direction Cosine Matrix (DCM). The user first specifies and object and coordinate system that will be the system of interest or the "to" system in the transformation. Then specifies a coordinate system that will be the reference or "from" system and finally specifies a time for the DCM to be computed at. Note that this time must be within the scenario time period.

Example:

```matlab
getSTKDCM('Satellite1','Body','CentralBody/Earth J2000','15 Apr 2021 18:00:30.000')
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [printRootEvents.m](printRootEvents.m)

This function attaches to the root object of a scenario and prints root events to the MATLAB command window.

Example:

```matlab
uiapp = actxserver('STK12.application');
root = uiapp.Personality2;
root.registerevent('printRootEvents')
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [pullDataProvider.m](pullDataProvider.m)

This function takes in data provider parameters and outputs the desired data, skipping the setup that is usually needed. It automates the process without the user having to get into the semantics of how data providers work in object model.

It is also useful to have the report & graph manager open as if you were trying to create a custom report so you can see the possible data provider and proper subfolders to use as function inputs

**NOTE**: See the script header for input instructions and instructions for data providers requiring predata

Example:

```matlab
dataProvString = 'Axes Choose Axes';
dataProvElem = {'Time','q1','q2','q3','q4'};
times = {0,2400,60};
grouping = 'Body';
predata = [CentralBodies/Earth];
object = root.GetObjectFromPath('Satellite/Satellite1');
[outputData] = pullDataProvider(root,dataProvString,dataProvElem,times,grouping,predata,object);
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [rgb2StkColor.m](rgb2StkColor.m)

A simple utility to convert a standard RGB color vector to a double expected by STK through the COM interface.

Example:

```matlab
[stkColor] = rgb2stkColor([14   255   255])
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [vgtImporterExporter.m](vgtImporterExporter.m)

This script allows you to export many vgt components on one object and inserts them on another. Through the GUI, you can only do one at a time.

Example:

```matlab
vgtImporterExporter('AWB','Ship/Ship1','Satellite/Satellite1')
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---
