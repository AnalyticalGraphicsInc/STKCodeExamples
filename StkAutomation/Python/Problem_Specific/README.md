# Python Problem Specific Samples

## [AnnotationsOnAVehicle2D.py](AnnotationsOnAVehicle2D.py)

This script allows you to add a series of annotations to the path of a moving STK object. The user specifies an STK version, the desired object name and type, the accuracy of the annotations (defined by time step), the event times in UTCG, the event titles, and the event colors. These are all located in "Main" at the bottom of the script.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [DutyCycleMultiSat.py](DutyCycleMultiSat.py)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm?Highlight=python%20api) available in STK 12.1+

This script utilizes a Duty Cycle allotment (a total pointing time limitation) per orbit to determine access times to area targets. Sensors are utilized for spatial awareness purposes. The script can utilize a named "special" target as a primary target for one of the satellites in the scenario. The satellite will use as much time as possible on this target. The remaining allotted time on each satellite will be split evenly amongst the remaining targets.
The user specifies the names of the satellites, the names of the sensors on the satellites, the primary targets, then the time allotment for each of the satellites.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [EditIntervalFile.py](EditIntervalFile.py)

Allows the user to take an interval file ([.int](https://help.agi.com/stk/index.htm#stk/importfiles-04.htm)) from STK and insert time gaps between the defined intervals that have "Show on default attributes" as the setting. The name of the initial interval file and the path to its containing folder are required in the beginning of the script, and the scenario start and stop times are required further down. This script is useful when loading an interval file that does not cover the whole time span of the object it is made for, as it prohibits any gaps in the trajectory.

### Dependencies

* Capabilities: Free, [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [ExternalWindModel](ExternalWindModel)

This code example shows how to integrate external wind models for use with STK. This script uses the [Horizontal Wind Model 93](https://ccmc.gsfc.nasa.gov/modelweb/atmos/hwm.html) in STK scenarios. HWM93 is a popular empirical wind model based on satellite and ground-based instrument data.

Step 1. Install the HWM93 model from [PyPi](https://pypi.org/project/hwm93/) or [Github](https://github.com/space-physics/hwm93)
Step 2. Explore the sample code in HWM93STKpy notebook. Filepaths will need to be changed to run the example.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [EOIRTrackingInTheLoop](EOIRTrackingInTheLoop)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm?Highlight=python%20api) available in STK 12.1+

Notebooks and libraries to automate STK, take EOIR images, process images, generate measurements, update pointing direction and optionally run ODTK in the loop. Additionally includes a tool to help convert images into reflectance, emissivitiy and temperature maps to use with EOIR. 

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [EOIR](https://www.agi.com/products/stk-specialized-modules/stk-eoir)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: numpy, pandas, cv2, shutil, imageio, matplotlib, sklearn, skimage, scipy, astropy, PIL

---

## [ConstellationWizard](ConstellationWizard)

Python code and a user inteface to allow subsets of large satellite constellations to quickly be built and loaded into STK, perform analysis, and then unloaded. In this way analysis at different times or with different constellations can be performed without loading in thousands of satellites. The readme within the project folder contains specific requirements for using the tool, as well as explanations for each of the notebooks and how the tool generally works.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional), [Communications](https://www.agi.com/products/stk-systems-bundle/stk-communications)
* Other Scripts: N/A
* Scenario: N/A

---

## [DeckAccess](DeckAccess)

When the question comes up "What satellites can I see?", STK has a tool to answer this question called Deck Access. Deck Access can be accesses through the GUI or scripted via Connect Command to consider access to a list of many objects. This Jupyter notebook builds a scenario, creates an observer with a constraint and then runs deck access to the TLE satellite database at the current time. Then the subset of visible satellites are imported into STK as an MTO object for visualization and further investigation of the deck access report and TLE data is done with Python.

Rerunning the script will update the current time and the set of visible satellites will be updated.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---

## [TargetedSensorPointingClosestFacility.py](TargetedSensorPointingClosestFacility.py)

This script computes access between a series of sensors on satellites and place objects and creates a pointing algorithm based on the place object that is the closest to the satellite at a given time. The script uses satellite names, place names and a time step for the algorithm to use as inputs. It also assumes that a scenario with these places and satellites already exists and that the satellites already have sensor objects. 

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: N/A

---
