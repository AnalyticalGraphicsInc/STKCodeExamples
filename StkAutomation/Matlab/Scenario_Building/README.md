# MATLAB Scenario Building Samples

## [ImportFacilities](ImportFacilities)

This script attaches to an open instance of STK12 and imports facilities with position data from an Excel spreadsheet. Units are assumed to be degrees and meters with a header row in the Excel file for ID, LAT, LON, ALT. There is an example facility spreadsheet in the ImportFacilities folder.

Example:

```matlab
ImportFacilities('GroundSites.xlsx')
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [RealTimePropagation](RealTimePropagation)

This folder contains four scripts that demonstrate use cases for the realtime propagator in aircraft, missile, and satellite objects. See the ReadMe.txt file included in the RealTimePropagation folder for more information about each script. The data needed to run these scripts is also included in txt files.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [SensorSweepSOLIS](SensorSweepSOLIS)

This script creates a sweeping/raster scan over a list of area targets. The scan speed, width and direction are controllable and will be inserted as a ground vehicle called 'Scan#'. A SOLIS target sequence file can also be created, which can be used to command a spacecraft's attitude to point along the scan pattern. SOLIS can be automatically run with the generated list of scans. A preconfigured Aviator aircraft can alternatively be used to generate the scan pattern. This script relies on GenerateScan.m and LatLonNewPoint.m to create the scan pattern which are included in the SensorSweepSOLIS folder.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [SOLIS](https://p.widencdn.net/oxspcl/SOLIS-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [astgSlewToManeuvers.m](astgSlewToManeuvers.m)

When an Astrogator satellite maneuvers by default the manuever will adjust the attitude to align with the thrust direction, but it will not slew. This script allows the satellite to slew into and out of the maneuver. The user specifies the name of the satellite, the pointing during thrusting intervals, the pointing outside of thrusting intervals, and the slew length. These inputs are set on lines 8-24.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Astrogator](https://p.widencdn.net/1ozbgh/Astrogator-Product-Specsheet), [Analysis Workbench](https://p.widencdn.net/cdz693/Analysis-Workbench-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [FigureEight.m](FigureEight.m)

This script creates waypoints for any GreatArc propagated STK vehicle in the shape of a figure eight. The user specifies the name of the object, the major and minor axes of the figure eight, the center latitude and longitude, and a few other basic parameters on lines 10-19.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [FigureEightCircle.m](FigureEightCircle.m)

This script creates an ephemeris file for any GreatArc propagated STK vehicle in the shape of a figure eight constructed of two circles. The user specifies the name of the object, the radius of the two circles, the center latitude and longitude, and a few other basic parameters on lines 10-19.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [makeEllipticalSensor.m](makeEllipticalSensor.m)

This function creates an elliptical sensor pattern and saves it off as a .pattern file. The function when run will prompt the user for the semi-major and semi-minor axes of the ellipse in degrees as well as the rotation angle of the semi-major axis relative to the horizontal.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Pro](https://p.widencdn.net/3ezpjj/STK-Pro-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [MissilePropagation.m](MissilePropagation.m)

This is a simple script to propagate a missile with the basic ballistic propagator. The user specifies the launch and impact locations as well as the propagation times in the input section on lines 5-12. The script also pulls out ground range and altitude from the ground range data provider.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [solisCreateSequenceFromAccess.m](solisCreateSequenceFromAccess.m)

To run this script you must have a Scenario loaded with a propagated satellite, have SOLIS open for this satellite (to create sequence folder directories), have a ground station of any type (facility, target or place). These ground stations can be constrained to provide accurate access metrics. The script will prompt the user for the required inputs.

This sample is used to generate a simple sequence for communicating with a satellite. The Satellite and Uplink Site (place/target/facility) will have access to eachother. At AOS (Acquisition of Signal), the satellite will execute a Communications_ON sequence which readies the vehicle for communicating with the satellite. At LOS (Loss of Signal) the satellite will execute a Communications_OFF sequence which stops the vehicle from communicating. These additional sequences can include any commands to the vehicle and are left as .seq files which allow for anything to be added to them.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [SOLIS](https://p.widencdn.net/oxspcl/SOLIS-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---
