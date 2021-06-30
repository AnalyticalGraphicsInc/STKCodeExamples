# MATLAB Scenario Analysis Samples

## [accessAtEveryCoveragePoint.m](accessAtEveryCoveragePoint.m)

This script will iterate through every point in a coverage definition and find the lat, lon, and C/N between the transmitter and a dummy facility/receiver on a coverage definition point. It will write this information to a file located in the working directory.

**NOTE**: Object names need to be changed on lines 22-27

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Coverage](https://p.widencdn.net/hml3i2/Coverage-Product-Specsheet), [Communications](https://p.widencdn.net/nvindd/Communications--Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [AccessByMetric.m](AccessByMetric.m)

This is a script to select access from an object to multiple other objects based off of a specifed condition metric, such as minimum range or maximum elevation angle.

Once the script generates the file, create a sensor on the object and assign its pointing type to Targeted and move over all the to objects. Change the target times to load in the created interval file. Remember to change the cone angle to something small so only the singular object that meets your metric is selected.

Inputs:
 fromObjectType: type of object to compute access from
 fromObjectName: name of object
 toObjectTypes: type of object to compute access to, it can be multiple types
 optionalToObjectName: optionally, only use objects that contain this string in their name
 fileName: the filename you want the data to be exported to (without the extension)
 conditionMetric: the conditional metric you want to extract (Range and Elevation are the only two metrics for now)
 timeStep: this is how often the data is sampled from STK. By default it is at 1 second which does take some time to run, so change with caution
 extrema: this is what determines if you're looking for minimum or maximum

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [addArrowsAlongPath.m](addArrowsAlongPath.m)

This function uses graphics primitives and Analysis Workbench components to draw evenly spaced arrows along an object's trajectory. This is useful for many Earth-based objects to show direction of travel along the trajectory. Run the script to quickly place directional arrows along your object routes!

**NOTE**: This script only works with Earth-bound objects (i.e. aircraft, ground vehicles, ships, and missiles)*

Inputs:
 objectPath (char array): Specify object path of vehicle
 numArrows (int): Set number of equally-spaced arrows along the path
 arrowLegLength_mi (int/double): Set leg length for each arrow in miles
 arrowLegAngle (int/double): Set angle between arrow leg and neg. direction vector
 arrowLineWidth (int): Set arrow line width (1-10)
 clearExistingPrimitives (bool): Do you want to clear any existing primitives?

Example:

```matlab
addDirectionalArrows('Aircraft/testAircraft',10,20,45,3,true)
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Analysis Workbench](https://p.widencdn.net/cdz693/Analysis-Workbench-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [astgCheckPassiveSafety.m](astgCheckPassiveSafety.m)

This function will take in an Astrogator satellite and check each maneuver in that satellite's MCS for passive safety relative to another reference satellite. The user specifies a minimum range between the two satellites below which a maneuver will be labeled as unsafe. After each maneuver, a sample satellite is created and propagated without any further maneuvers for the specified amount of time. Range between the two satellites is checked throughout that propagation to determine safety.

Inputs:
 root: STK Object Model root
 SatName: Name of Astrogator satellite
 TargetName: Name of reference satellite
 userTrip: Length of time each maneuver is propagated after to check for passive safety (sec)
 userMinRange: Range threshold between two satellites for safety violation (km)

Example:

```matlab
[passiveCheck,minRangeP,badManeuverTimes] = astgCheckPassiveSafety(root, 'Satellite1', 'Satellite2', 1000, 10)
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Astrogator](https://p.widencdn.net/1ozbgh/Astrogator-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [astgVisualizeDeltaVVectors.m](astgVisualizeDeltaVVectors.m)

Adds delta V vectors along the trajectory of an Astrogator satellite. The delta Vs come from the maneuver summary report. The maneuver location and vector are also created in Analysis Workbench.

Open a STK scenario with a propagated Astrogator satellite which has maneuvers. In the Input section of the Matlab code, update the satellite name, desired coordinate system to display the dV Vectors and graphics options. Then click Run in Matlab. The code will need to be rerun to update the maneuvers if the trajectory changes.

When selecting the 'displayAxes', ensure both a coordinate axes and a coordinate system exist in Analysis Workbench with the same name.

For finite maneuvers the delta V vector is placed at the beginning of the maneuver and appears after the maneuver occurs. Change Line 193 to put the dV vector at the start of the burn:

```matlab
ephDP = sat.DataProviders.Item('Astrogator Maneuver Ephemeris Block Initial');
```

Ensure the spacecraft does not run out of fuel during a maneuver.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Astrogator](https://p.widencdn.net/1ozbgh/Astrogator-Product-Specsheet), [Analysis Workbench](https://p.widencdn.net/cdz693/Analysis-Workbench-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [automateLifetimeTool.m](automateLifetimeTool.m)

This script automates a satellite lifetime tool analysis, generates a text file of the results, and shows an example of looping through paraemters to run a trade study on the satellite's orbit lifetime.

To run the script, have STK open with a scenario containing the satellite that you would like to perform that lifetime analysis on. Ensure the satellite has been propagated. Change the satellite's name on line 13 and the lifetime parameters on lines 16-19

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [SatPro](https://p.widencdn.net/xzzsyk/SatPro--Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [CreateGrazingAngle.m](CreateGrazingAngle.m)

This script finds the Earth horizon grazing angle along a given direction from an object. In this case, a bearing angle rotated CCW from North is used to specify the direction.

Inputs:
 objectPath: Object path in STK (string)
 angleCCWFromNorth: Angle CCW from North (Westward) to specify the reference direction (double)

Outputs:
 Analysis Workbench components: a reference direction vector, the grazing point, the vector pointing to the grazing point, the grazing angle relative to Nadir(detic), and a calc scalar for the grazing angle

Example:

```matlab
GrazingAngle('Satellite/Satellite1', -45)
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Analysis Workbench](https://p.widencdn.net/cdz693/Analysis-Workbench-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [quiverPlotRelativeGroundVelocity.m](quiverPlotRelativeGroundVelocity.m)

Quiver plots in Matlab are a great way of visualizing vector fields. In this example, STK integration is used to bring data in from STK to Matlab. The data is then used to create a quiver plot showing the relative velocity of points on the ground that are covered by a satellite's sensor.
This script is made for a scenario with the format specified in QuiverExample.vdf. More information on quiver plots can be found in the [Matlab help](https://www.mathworks.com/help/matlab/ref/quiver.html).

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Coverage](https://p.widencdn.net/hml3i2/Coverage-Product-Specsheet)
* Other Scripts: N/A
* Scenario: [QuiverExample.vdf](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/fe8f63d5-77f1-40e7-975a-2319d78ae18a)

---

## [regroupByAsset.m](regroupByAsset.m)

Report accesses by asset from an "Export Accesses as Text" .cvaa file using a Coverage Definition. This code determines the access intervals for each asset to a coverage definition. Conceptually similar to Deck Access, but for a coverage definiition with multiple assets. If an asset does not have access no interval file will be created.

**NOTE**: The inputs section contains absolute paths on lines 9 and 11 that will need to be changed for each machine

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Coverage](https://p.widencdn.net/hml3i2/Coverage-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [valueByGridPointAtTime.m](valueByGridPointAtTime.m)

This script captures Instantaneous FOM Values by Grid point for an STK Coverage Definition utilizing a Navigation Accuracy Type Figure of Merit. The user must first create an STK Scenario consisting of a Coverage Definition and Figure of Merit of Type Naviation Accuracy. Before running this script the user must first Compute Coverage Definition.

**IMPORTANT**: The User must specify the name of the Coverage Definition and Figure of Merit Objects flawlessly (no spelling errors or mistakes with capitalising letters). The user must also specify the time step for the generated FOM report.

Inputs:
 Cov_Def_Name: User specified name of Coverage Definition Object in STK
 FOM_Name: User specified name of Coverage Definition Object in STK
 Time_Step: Number of seconds between each time step

Outputs:
 T1: Table one showing Instantaneous FOM Value for each Lat/Lon point at each time step
 T2: Table one showing Percentile FOM value for each time step valueByGridPointAtTime: Exported Excel spreadsheet with Sheet 1 contatining T1 and Sheet 2 containing T2

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Coverage](https://p.widencdn.net/hml3i2/Coverage-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---
