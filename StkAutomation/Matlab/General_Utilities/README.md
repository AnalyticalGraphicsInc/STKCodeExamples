# MATLAB General Utility Samples

## [Clutter Simulator](Clutter%20Simulator)

The clutter simulator folder contains the SSA_Clutter_Interference_Visualizer.m demo tool. This is meant to be a simple and fast simulator for showing a target object, a randomly generated star like background, and a series of clutter objects traveling along in a constellation group.

Please reference [this readme](./Clutter Simulator/README.md) for more information

### Dependencies

* Licenses: N/A
* Other Scripts: [trim_or_pad.m](./Clutter Simulator/trim_or_pad.m), [create_clutter_map.m](./Clutter Simulator/create_clutter_map.m), [create_satellite_train_layer.m](./Clutter Simulator/create_satellite_train_layer.m), [create_star_info.m](./Clutter Simulator/create_star_info.m), [create_star_map.m](./Clutter Simulator/create_star_map.m),
[draw_antialias_line.m](./Clutter Simulator/draw_antialias_line.m), [generate_2d_gaussian_psf.m](./Clutter Simulator/generate_2d_gaussian_psf.m), [mouse_figure.m](./Clutter Simulator/mouse_figure.m)
* Scenario: N/A

---

## [convertTLEState.m](convertTLEState.m)

This simple function requires an open STK 12 scenario with a satellite that has been propagated with the SGP4 propagator. This function will pull out the initial state of the SGP4 satellite in the ICRF frame then create and propagate a new satellite of the specified propagator type using that initial state. Place this file in your MATLAB working directory and then you can call it within other MATLAB scripts or directly from the command window.

**NOTE**: You must use single quotations to specify these string inputs, this is due to limitations of the SetPropagatorType method

Example:

```matlab
convertTLEState('Satellite1','ePropagatorHPOP')
```

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
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

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
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

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [generateSensorPointingFile.m](generateSensorPointingFile.m)

This function will generate an attitude pointing file (.sp) for the specified sensor, using quaternions. For targeted sensors and/or sensors with multiple access intervals, this script will create multiple pointing files for each interval, and name them according to their start and stop times in EpSec.

Example

```matlab
generateSensorPointingFile('Satellite/LEOSat/Sensor/LEOsensor', 'LEOSensorAttitude', 60)
```

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [getAllObjectHandles.m](getAllObjectHandles.m)

Connects to an existing STK instance and loops through every object in the scenario while assigning a local MATLAB variable with the same name as each object pointing to that object. This is commonly used for debugging getting more comfortable with the object model. Users can create complex scenarios manually and then run getAllObjectHandles to navigate through the object model and inspect properties.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: Any scenario open with at least one object

---

## [getDCM.m](getDCM.m)

Allows users to output a quaternion from the Axes Choose Axes data provider and convert that quaternion into a Direction Cosine Matrix (DCM). The user first specifies and object and coordinate system that will be the system of interest or the "to" system in the transformation. Then specifies a coordinate system that will be the reference or "from" system and finally specifies a time for the DCM to be computed at. Note that this time must be within the scenario time period.

Example:

```matlab
getSTKDCM('Satellite1','Body','CentralBody/Earth J2000','15 Apr 2021 18:00:30.000')
```

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
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

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
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

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
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

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [StkHelp.m](StkHelp.m)

This function will launch the Programming Help documentation page for a given STK handle. The 'offline' flag can be used to open the local Programming Help (optional).

Example:

```matlab
% satellite: IAgSatellite
StkHelp(satellite, 'offline')
```

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [StkHelp.m](StkHelp.m)

This function will launch the Programming Help documentation page for a given STK handle. The 'offline' flag can be used to open the local Programming Help (optional).

Example:

```matlab
% satellite: IAgSatellite
StkHelp(satellite, 'offline')
```

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
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

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [persistentAccessLines.m](persistentAccessLines.m)

This script will connect to a running instance of STK and generate persistent access lines over time. You must specify the from_objPath, the to_ObjPath, timestep, and option variables. With an option equal to 1, an MTO will be used to statically visualize the access lines between the two objects. With option equal to 2, primitives will be used to dynamically create persistent access lines throughout an access. Note that MTOs will take significantly longer to create than primitives. When the script has completed, you will receive a popup message saying 'Done.'

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: Any scenario with at least two objects that have access to each other at some point in the scenario analysis interval.

---

## [FilterSegmentsByType.m](FilterSegmentsByType.m)

For an Astrogator satellite, grabs all segments of a specified type and returns a list of handles to the segments.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf), [STK Premium Space](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-space-brochure.pdf)
* Other Scripts: N/A
* Scenario: Any scenario with a propagated Astrogator satellite.

---

## [intervalListFromTimeArray.m](intervalListFromTimeArray.m)

Create a new time interval list given a time array or an STK Object with an offset time in seconds.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: Any scenario with a STK object.

---

## [FOMDataPrv.m](FOMDataPrv.m)

Get the "Figure of Merit: Time Value by Point" data provider values: Latitude, Longitude and FOM Value. This snippet assumes a scenario has already been created with a Coverage Definition and a Figure of Merit. To use the snippet the user must edit the script to redefine the path to the scenario file (the ScenarioPath variable), the Coverage Definition and FOM object names/paths, and the Time of interest (dataPrv.PreData).

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: Any scenario open with Coverage definition and Figure of Merit

---

## [Normalize_EOIR_Images](Normalize_EOIR_Images)

This script accepts a directory of raw sensor output data from EOIR and normalizes the entire frame sequence. This is useful for stitching the frames together in a video so that any color variation between frames is eliminated. This can be used in conjunction with the image generation and movie-making Python scripts linked below.

### Dependencies

* Licenses: N/A
* Other Scripts: [EOIR Synthetic Scene and Data Generation](../../Python/Scenario_Analysis/EOIR_Synthetic_Scene_and_Data_Generation)
* Scenario: N/A
