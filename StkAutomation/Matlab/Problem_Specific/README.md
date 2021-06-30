# MATLAB Problem Specific Samples

## [DynamicSensorMask](DynamicSensorMask)

Sensor AzEl masks are computed at a momment in time meaning that they cannot account for obstructions that move in the sensor's frame. This script allows sensor field of view obscurations to be considered over time by moving objects, such as solar panels or other obscuring vehichles.

The script computes sensor body AzElMasks over a series of times. After the masks have been created, they can be reused for subsequent access calculations. Access is calculated to a list of objects from each sensor. Access is computed using the most recent AzEl mask until the time of the next available mask and then the mask is updated and the process is repeated. In this way dynamic field of view obstructions can be considered. The switching of the AzElMasks is handled by the script, with the result being access interval files and AWB intervals which can be loaded back into STK.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Pro](https://p.widencdn.net/3ezpjj/STK-Pro-Product-Specsheet)
* Other Scripts: [FilterObjectsByType.m](../General_Utilities/FilterObjectsByType.m)
* Scenario: [DynamicSensorAzElMask.zip](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/ea415e04-233e-4d2d-870b-a105724d5361)

---

## [SolarEnergySimulink](SolarEnergySimulink)

This example shows how satellite power system designed in Simulink can be integrated with STK for testing and analysis of that system.  An STK scenario is used to generate inputs to the Simulink power model by determining sunlight exposure, experiment times, and downlink times.  A level-2 s-function block is used to host the STK application.  When the Simulink model is run, an STK scenario will be built that includes a satellite, an area target to be imaged (experiment), and a ground station for downlink (communications).  As Simulink executes the model through time, it will query STK for updates on the state of the satellite.  STK will return 1 or 0 for these three states, In Sunlight, Conducting Experiment (access to area target), Conducting Downlink (link budget with ground station where BER < 1e-8).  These values will feed into the power model to determine the current state of the battery.

**NOTE**: This example requires a Simulink license, see the detailed ReadMe within the SolarEnergySimulink folder

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [advcatTradeStudy.m](advcatTradeStudy.m)

This example runs a trade study in which the inclination and semi-major axis parameters of a satellite are varied to determine the number of conjuctions between the satellite and the entire TLE catalog at each configuration. This script requires a scenario with a satellite called Satellite1, an AdvCat object called AdvCAT1. The AdvCAT object needs to be set up with Satellite1 as Primary and any TLE database as the secondary object.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [CAT](https://p.widencdn.net/es6zld/Conjunction-Analysis--Product-Specsheet)
* Other Scripts: N/A
* Scenario: Create or open a scenario with a Satellite named Satellite1 and an AdvCAT object named AdvCAT1

---

## [High_Altitude_Balloon.m](High_Altitude_Balloon.m)

This example models the path of a high altitude balloon object using the STK aircraft object type. The user defines the balloons starting location and some basic performance metrics. These inputs are used to generate a balloon flight path with random noise to account for wind variation.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [LambertSolver_EarthToMars.m](LambertSolver_EarthToMars.m)

A short example demonstrating the connect command interface to the Lambert Solver built into STK. The Lambert Solver is used to get an estimate for the deltaV required for an Earth to Mars transfer.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Astrogator](https://p.widencdn.net/1ozbgh/Astrogator-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [multiSegmentAttitude.m](multiSegmentAttitude.m)

This function is written to address a specific "problem" however it serves as an example of custom Analysis Workbench components as well as multi segment attitude through STK Integration. The specific "problem" that this script addresses is a satellite that has multiple attitude segments: a non-operational profile, a pre-operational attitude profile, and an operational profile. The  definition and timing of each profile is as follows:

Non-Operational Profile -- Satellite Body X aligned with the velocity vector and body Z constrained by the Nadir vector. This is the default state for the satellite

Pre-Operational Profile -- Satellite Body Y aligned to the Sun vector and body Z constrained by the Nadir vector. This is to simulate an operational constraint that the satellite must point its solar panels at the sun for a specified amount of time before each access (or Operational segment) to the facility.

Operational Profile -- Satellite Boxy X aligned to the vector from the satellite to the facility and body Z constrained to the Nadir vector. This is to simulate a transmit/receive attitude between the satellite and the facility.

Each profile switch is accompanied by a fixed time slew. The fixed time for these slews is an input to the function. In this hypothetical situation, the satellite must be in sunlight to be eligible for access and an interval list is created that contains each sunlight interval for the satellite minus the first x amount of seconds in each period corresponding to the required pre-pass time. The functionality demonstrated in this script is primarily custom time components through Analysis Workbench and custom attitude segments through the Multi-Segment attitude type.

Called with: [] = multiSegmentAttitude(satName,facName, prePassTime, SlewLength)

Example:

```matlab
multiSegmentAttitude("Satellite1","Facility1",600,300)
```

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [Analysis Workbench](https://p.widencdn.net/cdz693/Analysis-Workbench-Product-Specsheet)
* Other Scripts: N/A
* Scenario: Create or open a scenario with at least one facility and one satellite

---

## [SatelliteMonteCarlo.m](SatelliteMonteCarlo.m)

An example of setting up a Monte Carlo analysis in STK. This example perturbs a satellite's initial state with a Gaussian or uniform distribution. At each iteration the perturbations and the RIC state relative to the nominal satellite are recorded. Please feel free to modify this snippet as necessary for your particular analysis, alternatively, you can perform more detailed analyses with STK Analyzer.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet)
* Other Scripts: N/A
* Scenario: Create or open a scenario with at least a satellite object

---

## [TETK_Automation_Tutorial.m](TETK_Automation_Tutorial.m)

An example of setting up a TETK scenario based on the data files included in the STK install. The TETK interface is primary connect commands. This script creates a new scenario, populates it with flight segments and tracking files, performs track comparisons, and creates quick looks. This servers as an example of TETK integration to be customized by the user.

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [TETK](https://p.widencdn.net/cvrh1r/TE-Tool-Kit-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---

## [TETK_SatisfactionIntervalsExample.m](TETK_SatisfactionIntervalsExample.m)

This code allows you to import TSPI data files and create a flight segment .txt file based on intervals that satisfy TWO user input metric conditions, which is then loaded into TETK via Connect Commands to visualize. In this example, we are looking at the intervals during our Ownship's flight that meet a Cal Air Speed condition (from 200 to 300 knots) and a roll angle condition (from -50 to 0 degrees).

**NOTE**: This code is taking a .csv TSPI file with a time format in ISO-YD and converting it to the correct format in order to import flight segments via .txt file. Will require time format manipulation for importing flight time segments.
Required format for ingest to TETK: ddd:HH:mm:ss.sss

### Dependencies

* Licenses: Free, [Integration](https://p.widencdn.net/wums3s/Integration-Product-Specsheet), [TETK](https://p.widencdn.net/cvrh1r/TE-Tool-Kit-Product-Specsheet)
* Other Scripts: N/A
* Scenario: N/A

---
