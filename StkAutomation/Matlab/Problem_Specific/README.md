# MATLAB Problem Specific Samples

## [DynamicSensorMask](DynamicSensorMask)

Sensor AzEl masks are computed at a momment in time meaning that they cannot account for obstructions that move in the sensor's frame. This script allows sensor field of view obscurations to be considered over time by moving objects, such as solar panels or other obscuring vehichles.

The script computes sensor body AzElMasks over a series of times. After the masks have been created, they can be reused for subsequent access calculations. Access is calculated to a list of objects from each sensor. Access is computed using the most recent AzEl mask until the time of the next available mask and then the mask is updated and the process is repeated. In this way dynamic field of view obstructions can be considered. The switching of the AzElMasks is handled by the script, with the result being access interval files and AWB intervals which can be loaded back into STK.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: [filterObjectsByType.m](../General_Utilities/filterObjectsByType.m)
* Scenario: [DynamicSensorAzElMask.zip](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/ea415e04-233e-4d2d-870b-a105724d5361)

---

## [SolarEnergySimulink](SolarEnergySimulink)

This example shows how satellite power system designed in Simulink can be integrated with STK for testing and analysis of that system.  An STK scenario is used to generate inputs to the Simulink power model by determining sunlight exposure, experiment times, and downlink times.  A level-2 s-function block is used to host the STK application.  When the Simulink model is run, an STK scenario will be built that includes a satellite, an area target to be imaged (experiment), and a ground station for downlink (communications).  As Simulink executes the model through time, it will query STK for updates on the state of the satellite.  STK will return 1 or 0 for these three states, In Sunlight, Conducting Experiment (access to area target), Conducting Downlink (link budget with ground station where BER < 1e-8).  These values will feed into the power model to determine the current state of the battery.

**NOTE**: This example requires a Simulink license, see the detailed ReadMe within the SolarEnergySimulink folder

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [advcatTradeStudy.m](advcatTradeStudy.m)

This example runs a trade study in which the inclination and semi-major axis parameters of a satellite are varied to determine the number of conjuctions between the satellite and the entire TLE catalog at each configuration. This script requires a scenario with a satellite called Satellite1, an AdvCat object called AdvCAT1. The AdvCAT object needs to be set up with Satellite1 as Primary and any TLE database as the secondary object.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [CAT](https://www.agi.com/products/stk-specialized-modules/stk-conjunction-analysis-tool-cat)
* Other Scripts: N/A
* Scenario: Create or open a scenario with a Satellite named Satellite1 and an AdvCAT object named AdvCAT1

---

## [ChangeSTMComponents.m](ChangeSTMComponents.m)

With STK 11.4 there is now the ability to report out the state transition matrix components for an astrogator propagate segment.  You must first create a custom propagator in the component browser and add the State Transition Matrix as a propagator function.  Then, for any propagate segment that uses a propagator with the STM added, you can report out the components.  In the component browser there are already template components for the STM under Calculation Objects > Cartesian STM.  If you are orbiting Earth, these components will work just fine, but if you are at a different central body, you will need to duplicate each of these components and set the reference frame to a frame for the central body that you are orbiting.  

This script connects to the linked scenario with a satellite orbiting Mars.  The satellite is already configured with a propagate segment that uses a custom propagator called Mars HPOP with STM that has the state transition matrix added to it.  The script creates duplicates of all the STM components and changes the reference frame to Mars J2000.  It then reports out the STM at a given instant in time.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: [MarsSatSTM.vdf](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/edc984d2-8757-49ce-bfcc-a01fa764064b)

---

## [High_Altitude_Balloon.m](High_Altitude_Balloon.m)

This example models the path of a high altitude balloon object using the STK aircraft object type. The user defines the balloons starting location and some basic performance metrics. These inputs are used to generate a balloon flight path with random noise to account for wind variation.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [LambertSolver_EarthToMars.m](LambertSolver_EarthToMars.m)

A short example demonstrating the connect command interface to the Lambert Solver built into STK. The Lambert Solver is used to get an estimate for the deltaV required for an Earth to Mars transfer.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://www.agi.com/products/stk-specialized-modules/stk-astrogator)
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

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: Create or open a scenario with at least one facility and one satellite

---

## [SatelliteMonteCarlo.m](SatelliteMonteCarlo.m)

An example of setting up a Monte Carlo analysis in STK. This example perturbs a satellite's initial state with a Gaussian or uniform distribution. At each iteration the perturbations and the RIC state relative to the nominal satellite are recorded. Please feel free to modify this snippet as necessary for your particular analysis, alternatively, you can perform more detailed analyses with STK Analyzer.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: Create or open a scenario with at least a satellite object

---

## [TETK_Automation_Tutorial.m](TETK_Automation_Tutorial.m)

An example of setting up a TETK scenario based on the data files included in the STK install. The TETK interface is primary connect commands. This script creates a new scenario, populates it with flight segments and tracking files, performs track comparisons, and creates quick looks. This servers as an example of TETK integration to be customized by the user.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [TETK](https://www.agi.com/products/tetk)
* Other Scripts: N/A
* Scenario: N/A

---

## [TETK_SatisfactionIntervalsExample.m](TETK_SatisfactionIntervalsExample.m)

This code allows you to import TSPI data files and create a flight segment .txt file based on intervals that satisfy TWO user input metric conditions, which is then loaded into TETK via Connect Commands to visualize. In this example, we are looking at the intervals during our Ownship's flight that meet a Cal Air Speed condition (from 200 to 300 knots) and a roll angle condition (from -50 to 0 degrees).

**NOTE**: This code is taking a .csv TSPI file with a time format in ISO-YD and converting it to the correct format in order to import flight segments via .txt file. Will require time format manipulation for importing flight time segments.
Required format for ingest to TETK: ddd:HH:mm:ss.sss

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [TETK](https://www.agi.com/products/tetk)
* Other Scripts: N/A
* Scenario: N/A

---

## [TLE_Deorbit_Estimation.m](TLE_Deorbit_Estimation.m)

Satellite deorbit prediction is difficult due to its sensitivity on the forces experienced during atmospheric reentry and TLEs are often not sufficient to capture these forces. This script is meant to better estimate where a satellite may deorbit by propagating with a higher fidelity force model seeded from set of TLE states.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://www.agi.com/products/stk-specialized-modules/stk-astrogator)
* Other Scripts: N/A
* Scenario: N/A

---

## [AttitudeUsingAScheduledVector.m](AttitudeUsingAScheduledVector.m)

Sets up a scheduled vector in STK and builds all of the conditions.  The satellite will slew the Body X to the pointingAng defined below when the lat rate is positive and the satellite is above the aboveDeg parameter set below.

**Note**: Works only in STK 12.0+

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [ClosestGrazingAngle.m](ClosestGrazingAngle.m)

Finds the closest* Earth horizon/limb/edge/grazing angle from a specifed vector/boresight.

The direction of the closest edge is assumed to be in the same plane as the Nadir vector and vector of interest. This assumption is true for a perfectly spherical Earth, but may be slightly different when accounting for Earth's oblateness. If Nadir is selected as the vector of interest, the closest edge of the Earth is assumed to be Northward.

**Note**: Works only in STK 12.0+

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [SatelliteAttitudeControl](SatelliteAttitudeControl)
	
This example shows how a simple satellite with 3-axis attitude control built in Simulink can be integrated with STK.  An STK scenario is used to generate pointing data for the Simulink satellite model (truth data).  A level-2 s-function block is used to host the STK application.  The attitude is controlled by 3 rate integrating gyros built in Simulink, including the effects of band limited white noise to the gyros input position.   The gyro models are developed after the examples found in *Atmospheric and Space Flight Dynamics*, Ashish Tewari, Birkhauser Boston 2007.
 
The gains for this model are very loose to exaggerate the dampening time and overshoot.  Users can change the gains in the rate gyro blocks and adjust the transfer functions to match the inertia of their satellite.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro)
* Other Scripts: N/A
* Scenario: N/A

---

## [SimulinkAccess](SimulinkAccess)
	
This is a simple example that shows how Simulink can integrate with STK, where Simulink controls the simulation clock and pulls analysis data from STK. The STK analysis derives from a scenario containing a LEO satellite with an attached sensor and transmitter, an area target, and a ground station with an attached receiver.  From this base scenario, access is calculated to determine when the satellite is in the sun, when the satellite’s sensor can view the area target, and when the satellite’s transmitter can communicate with the ground station w/ a Bit Error Rate less than 1e-8.  The Simulink model consists of a source clock block, a Level-2 S-Function block for the STK component, and three display blocks to view the output from STK.  Simulink passes the current simulation time to the s-function block, where STK is queried to determine the current state of access for the three analyses previously mentioned.  If access is true, a value of 1 is passed to the display block for that case (Sunlight, Experiment, or CommLink).  

Follow the instructions in the included ReadMe.docx

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [SimulinkAttitudeControlLoop](SimulinkAttitudeControlLoop)
	
Simulink is a time-driven, visual oriented simulation & analysis tool that allows to easily implement time dependent processes.
In this example we use it to emulate the AOCS (Attitude and Orbit Control System) subsystem of a LEO satellite. The Simulink plant gets attitude and angular speed information from STK at any time step and returns an update attitude state according with the implemented guidance law and the current settings (such as satellite inertia and actuators specification).

A fully working control law based both on quaternions and Euler angles has been implemented; it has been tested over a number of different attitude profiles and works pretty nice. Some disturbance torques have been also added, so the controller is continuously commanding a small torque to maintain the satellite in the right attitude profile.

Once launched, the Simulink model starts an STK scenario that contains two satellites, one used for reference and the other one that is feeded by Simulink itself. In the scenario there is an HTML page that contains some textboxes to allow  the user to define any inertially fixed attitude profile, plus a spinning Sun pointing and an Earth pointing profile. By inserting any new attitude profile, the satellite will change its attitude accordingly.

A complete tutorial is also provided for your reference (UsingSimulink_v4.pdf)

Important:  you need to change the STK scenario path into the initialization callback function of the S-Function accordingly with your installation directory (otherwise the STK scenario cannot be found).

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro)
* Other Scripts: N/A
* Scenario: [SimulinkAttitudeControlLoop.zip](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/0aaeb6cb-7e91-499b-836c-0c35cd51e183)

---

## [SimulinkAttitudeFile](SimulinkAttitudeFile)
	
This MATLAB script will create a simple scenario with a satellite set to target a ground control station. The satellite’s YPR angles will be written to an external attitude file (.a) where the yaw angle will be constrained by a user specified amount. That .a file will be loaded in for a cloned satellite object.
Here is a link to learn more about .a files and how they are created: [Attitude File Format (*.a)](http://help.agi.com/stk/index.htm#stk/importfiles-01.htm)

Open the script in MATLAB, change lines 7-9 to match the user’s desired output file location and yaw constraints. Run the script. 

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [sensorPointingAndAER.m](sensorPointingAndAER.m)
	
This is a standalone script that demonstrates the use of sensor pointing and retrieval of AER data using the Object Model. A new STK scenario is created with 2 aircraft and a sensor on the first aircraft. The sensor is set to target the second aircraft, and access is computed. Time, Az, and El data of the access is loaded into Matlab matrices.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [coverageAccumPercent.m](coverageAccumPercent.m)
	
This script allows you to specify a % of coverage accumulation over the course of your analysis interval. For example, if you want to know the time it takes for you coverage grid to be 90% covered, it will calculate the time it takes starting at each time step. The script will open STK, create a new scenario with a satellite and coverage definition assigned to a country area target, use the grid inspector data provider to compute accumulated coverage, and display the results in a Matlab table. A Matlab progress bar is created to show computation progress.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [Coverage](https://www.agi.com/products/stk-systems-bundle/stk-coverage)
* Other Scripts: N/A
* Scenario: N/A

---

## [ConvertAERtoLLA](ConvertAERtoLLA)

This script allows for easy conversion of a AER data from a Radar to LLA for a launch vehicle in STK. This same code can be easily modified to be used for any vehicles like Aircrafts/UAVs/GroundVehicles/Satellites. It requires Azimuth Elevation Range (AER) Data from tracker to that vehicle in a .csv as well as the location of the tracker. This script allows you to visualize trajectory data by reading in AER data from a .csv, converting it to LLA, and adding it as a waypoint in the vehicle's trajectory. The data should be formatted in columns of Time(EpSec), Azimuth(deg), Elevation(deg), and Range(km).

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [DebrisModel.m](DebrisModel.m)

This Debris model is a framework that can be augmented to simulate different types of debris-generating events.  The model uses a maneuver with a Normal Distributed random value for velocity in all directions.  It gives normally (Gaussian) distributed random numbers whose values theoretically lies between -Infinity to Infinity having 0 mean and 1 variance but most fall within +/-3 meters  per second. 50 debris objects are generated and propagated for a 100 minute period.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://www.agi.com/products/stk-specialized-modules/stk-astrogator)
* Other Scripts: N/A
* Scenario: N/A

---

## [AST_output_Acceleration_Data.m](AST_output_Acceleration_Data.m)

This short script allows the user to input an Astrogator satellite name, a finite maneuver name, and a time step and then export the acceleration history and mass rate into a .accelhist file that is usable by ODTK. More information on the .accelhist file can be found in the [ODTK Help](https://help.agi.com/ODTK/index.htm#od/ODObjectsSatelliteManeuversAccelerationFiles.htm). 

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://www.agi.com/products/stk-specialized-modules/stk-astrogator)
* Other Scripts: N/A
* Scenario: N/A

---

## [LinkBudgetLatLonChain_Rev4.m](LinkBudgetLatLonChain_Rev4.m)

This script takes the TransponderLB STK scenario and outputs the Eb/No, rain loss, and atm loss at the aircraft and facility's latitude and longitude. The facility is used as a 'dummy' object for the ground vehicle. The script works by moving the facility to each point in the ground vehicle's ephemeris file then outputting the LLA information of facility and aircraft. At each facility position, the aircraft flies its entire route and comm parameters are exported to an Excel spreadsheet.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [Communications](https://www.agi.com/products/stk-systems-bundle/stk-communications)
* Other Scripts: N/A
* Scenario: [TransponderLB.vdf](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/048cbff3-4567-4409-acdf-3dbb1830615c)

---

## [AccumulatedPower_CreateUserSuppliedData.m](AccumulatedPower_CreateUserSuppliedData.m)

This script will calculate access from your satellite to any one of the facilities in your scenario or that you specify. It will use a Chain object to determine times at which any access is calculated between the satellite and any one of the facilities. When access valid, the ‘connectedConsumptionRate’ is used. When there is no access, the ‘defaultConsumptionRate’ is used. These values are summed at every time step (based on the value you specified in the script) and then put into a custom ‘External Data’ report file that STK can then use as a Data Provider and displayed using the data display feature. The data display must be set up in the GUI.
Some high-level inputs are required: constants for power accumulation, selections for ground sites you’ll be using, the name of the satellite, and the time step you’ll be checking for connection at. You’ll also need to specify an intermediary file location.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [EOIRTextureMaps](EOIRTextureMaps)

This folder contains examples of converting NASA Earthdata HDF4 and HDF5 files into a format that can be imported into STK as a texture map. This contains examples of navigating the HDF standard format to locate corner point metadata required by STK and as well as detailing the conversion of data to usable information. This information is then written to a csv file that can be natively read by STK's EOIR capability. Check out [this FAQ](https://analyticalgraphics.force.com/faqs/articles/Knowledge/Loading-EOIR-Texture-Maps-From-NASA-s-Earthdata) for more detailed workflow information.

### Dependencies
* Capabilities: Free
* Other Scripts: N/A
* Scenario: N/A

---