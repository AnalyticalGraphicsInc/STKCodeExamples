# Python Problem Specific Samples

## [AnnotationsOnAVehicle2D.py](AnnotationsOnAVehicle2D.py)

This script allows you to add a series of annotations to the path of a moving STK object. The user specifies an STK version, the desired object name and type, the accuracy of the annotations (defined by time step), the event times in UTCG, the event titles, and the event colors. These are all located in "Main" at the bottom of the script.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [ChainTimingDelay.py](ChainTimingDelay.py)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm?) available in STK 12.1+

Complete chain access in STK means that each link in the chain simultaneously has access. This script allows a maximum delay between target/asset access and the rest of the chain. This is useful for scenarios such as an imaging system with onboard storage, where a target can be imaged, then downlinked at a later time specified by the maximum delay.

To use the script, just modify the user inputs then run.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [DutyCycleMultiSat.py](DutyCycleMultiSat.py)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm) available in STK 12.1+

This script utilizes a Duty Cycle allotment (a total pointing time limitation) per orbit to determine access times to area targets. Sensors are utilized for spatial awareness purposes. The script can utilize a named "special" target as a primary target for one of the satellites in the scenario. The satellite will use as much time as possible on this target. The remaining allotted time on each satellite will be split evenly amongst the remaining targets.
The user specifies the names of the satellites, the names of the sensors on the satellites, the primary targets, then the time allotment for each of the satellites.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [EditIntervalFile.py](EditIntervalFile.py)

Allows the user to take an interval file ([.int](https://help.agi.com/stk/index.htm#stk/importfiles-04.htm)) from STK and insert time gaps between the defined intervals that have "Show on default attributes" as the setting. The name of the initial interval file and the path to its containing folder are required in the beginning of the script, and the scenario start and stop times are required further down. This script is useful when loading an interval file that does not cover the whole time span of the object it is made for, as it prohibits any gaps in the trajectory.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [ExternalWindModel](ExternalWindModel)

This code example shows how to integrate external wind models for use with STK. This script uses the [Horizontal Wind Model 93](https://ccmc.gsfc.nasa.gov/modelweb/atmos/hwm.html) in STK scenarios. HWM93 is a popular empirical wind model based on satellite and ground-based instrument data.

Step 1. Install the HWM93 model from [PyPi](https://pypi.org/project/hwm93/) or [Github](https://github.com/space-physics/hwm93)
Step 2. Explore the sample code in HWM93STKpy notebook. Filepaths will need to be changed to run the example.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [EOIRTrackingInTheLoop](EOIRTrackingInTheLoop)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm) available in STK 12.1+

Notebooks and libraries to automate STK, take EOIR images, process images, generate measurements, update pointing direction and optionally run ODTK in the loop. Additionally includes a tool to help convert images into reflectance, emissivitiy and temperature maps to use with EOIR.

### Dependencies

* Licenses: [STK Premium Space](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-space-brochure.pdf) or [STK Premium Air](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-air-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: numpy, pandas, cv2, shutil, imageio, matplotlib, sklearn, skimage, scipy, astropy, PIL

---

## [LKtoFFDConverter](LKtoFFDConverter)

Two scripts: 1. A converter that takes in two LK files, one representing magnitude and one representing phase, and combining and converting the data into a single FFD file with a specified polarization (through command line arguments). Allows STK import into an antenna that will consider both magnitude and phase of an LK file. 2. A generator script to generate test LK files using a specified random distribution.

### Dependencies

* Licenses: N/A
* Other Scripts: N/A
* Scenario: N/A

---

## [SatelliteConflictFreePassesUsingIntervalTree](SatelliteConflictFreePassesUsingIntervalTree)

This script was used in a specific case for a customer: there is a ground facility that is attempting to calculate line of sight access to a constellation of satellites. The customer wants all the passes (pass number, time intervals, satellite name) that are "conflict free", the definition of conflict free being so:
    - A pass is conflict free if for the entire pass through the ground facility's line of sight access no other satellite enters that line of sight access region of the ground facility such that the facility now has line of sight access to two satellites. Even if a satellite is halfway through its pass without conflict, the moment another satellite enters the region, BOTH PASSES ARE REMOVED.
    - We are not looking for non-conflicting time intervals. We are looking for PASSES. For example, for a satellite that is halfway through its pass before another satellite enters the region, we do not care about the time interval such that it was the only satellite in the region. We will delete the entire pass including the time interval where it was initially unconflicted. We only keep time intervals and pass data in which the ENTIRE pass was valid and unconflicted.
    - If the region is unsymmetrical, generating reports on STK will recognize them as two different accesses even if they were on the same pass. We will merge any two accesses that are a part of the same pass as such.
    - The time between the valid pass of one satellite and the valid pass of the next valid satellite must be at least 3 minutes or else both will also be invalid.

This script implements an "Interval Tree" as a data structure to use to mark and remove conflicting intervals of times. It can be used to check for conflicts between intervals in logarithmic O(logn) time, which is especially important since the customer generated reports on a whole constellation of satellites, leading to huge amounts of access intervals in the resulting generated reports. As such, we needed an efficient way to find conflicts. We use the Object Model to pull in generated access report data, populate data structures, and add them to an interval tree. We then linearly iterate through all intervals and mark each as conflicted or not, and then delete conflicted intervals.

This script was utilized for a very specific case, but many elements of this can definitely be reused for any conflict-resolution type case that requires an efficient way to remove conflicts with similar guidelines (especially with cases that are more concerned about entire full passes of a satellite rather than just valid time intervals).

### Dependencies

* Licenses: N/A
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: numpy

---

## [ConstellationWizard](ConstellationWizard)

Python code and a user inteface to allow subsets of large satellite constellations to quickly be built and loaded into STK, perform analysis, and then unloaded. In this way analysis at different times or with different constellations can be performed without loading in thousands of satellites. The readme within the project folder contains specific requirements for using the tool, as well as explanations for each of the notebooks and how the tool generally works.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [DeckAccess](DeckAccess)

When the question comes up "What satellites can I see?", STK has a tool to answer this question called Deck Access. Deck Access can be accesses through the GUI or scripted via Connect Command to consider access to a list of many objects. This Jupyter notebook builds a scenario, creates an observer with a constraint and then runs deck access to the TLE satellite database at the current time. Then the subset of visible satellites are imported into STK as an MTO object for visualization and further investigation of the deck access report and TLE data is done with Python.

Rerunning the script will update the current time and the set of visible satellites will be updated.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [TargetedSensorPointingClosestFacility.py](TargetedSensorPointingClosestFacility.py)

This script computes access between a series of sensors on satellites and place objects and creates a pointing algorithm based on the place object that is the closest to the satellite at a given time. The script uses satellite names, place names and a time step for the algorithm to use as inputs. It also assumes that a scenario with these places and satellites already exists and that the satellites already have sensor objects.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---


## [createCovariancePoints.py](createCovariancePoints.py)

This script will take the prinicple axes of a covariance matrix and turn it into point. These can be used for access computations to determine when a satellite may be visible accounting for the orbit uncertainty. It will write 6 ephemris file and add points and satellites for the positive and negative direction of the major, intermediate and minor axis.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: numpy

---

## [EOIRTextureMaps](EOIRTextureMaps)

This folder contains examples of converting NASA Earthdata HDF4 and HDF5 files into a format that can be imported into STK as a texture map. This contains examples of navigating the HDF standard format to locate corner point metadata required by STK and as well as detailing the conversion of data to usable information. This information is then written to a csv file that can be natively read by STK's EOIR capability. Check out [this FAQ](https://analyticalgraphics.force.com/faqs/articles/Knowledge/Loading-EOIR-Texture-Maps-From-NASA-s-Earthdata) for more detailed workflow information.
### Dependencies

* Licenses: N/A
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: numpy, pyhdf, h5py, matplotlib

---
