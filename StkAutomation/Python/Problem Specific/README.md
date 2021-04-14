# Python Problem Specific Samples

## [AnnotationsOnAVehicle2D.py](AnnotationsOnAVehicle2D.py)

This script allows you to add a series of annotations to the path of a moving STK object. The user specifies an STK version, the desired object name and type, the accuracy of the annotations (defined by time step), the event times in UTCG, the event titles, and the event colors. These are all located in "Main" at the bottom of the script. 

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---

## [DutyCycleMultiSat.py](DutyCycleMultiSat.py)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm?Highlight=python%20api) available in STK 12.1+ 


This script utilizes a Duty Cycle allotment (a total pointing time limitation) per orbit to determine access times to area targets. Sensors are utilized for spatial awareness purposes. The script can utilize a named "special" target as a primary target for one of the satellites in the scenario. The satellite will use as much time as possible on this target. The remaining allotted time on each satellite will be split evenly amongst the remaining targets.   
The user specifies the names of the satellites, the names of the sensors on the satellites, the primary targets, then the time allotment for each of the satellites. 

### Dependencies

* Licenses: Free, Integration, Pro
* Other Scripts: N/A
* Scenario: N/A

---

## [EditIntervalFile.py](EditIntervalFile.py)
Allows the user to take an interval file ([.int](https://help.agi.com/stk/index.htm#stk/importfiles-04.htm)) from STK and insert time gaps between the defined intervals that have "Show on default attributes" as the setting. The name of the initial interval file and the path to its containing folder are required in the beginning of the script, and the scenario start and stop times are required further down. This script is useful when loading an interval file that does not cover the whole time span of the object it is made for, as it prohibits any gaps in the trajectory.

### Dependencies

* Licenses: Free, Analysis Workbench
* Other Scripts: N/A
* Scenario: N/A