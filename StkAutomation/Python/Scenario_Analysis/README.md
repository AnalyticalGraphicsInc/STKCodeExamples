# Python Scenario Analysis Samples

## [ForceComparison.py](ForceComparison.py)

This script will create a number of force model vectors on satellites at different altitudes and then evaluate the vector magnitudes. The user should edit the scenario start and stop times, as well as the semimajor axis and inclination of the satellites. HPOP is used to propagate the satellites.

### Dependencies

* Licenses: Free, Integration, SatPro, Analysis Workbench
* Other Scripts: N/A
* Scenario: N/A

---

## [Aviator_ObjectModel_FuelConsumptionStudy.py](Aviator_ObjectModel_FuelConsumptionStudy.py)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm?Highlight=python%20api) available in STK 12.1+

This script is an example of computing trade studies using the Aviator Object Model. No changes are needed for this script to run, as it constructs its own scenario with an Aviator aircraft defined by the Advanced Fixed Wing Tool. As is, the script iterates through flight altitudes and plots the fuel consumed, time of flight and altitude in a 3D graph.

### Dependencies

* Licenses: Free, Integration, Aviator
* Other Scripts: N/A
* Scenario: N/A

---

## [Lifetime Analysis](Lifetime Analysis)

Run trade studies with the Lifetime Tool. Grid searches, Latin Hyper Cube sampling and Monte Carlo analyses are possible. HPOP can also be run to compare to the Lifetime Tool. Multiple instances of STK can be started to speed up the trade studies. Results are saved to a csv file and trade study configurations are saved so they can be loaded and modified later.

### Dependencies

* Licenses: Free, Integration, SatPro
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: pyDOE2, poliastro

---

## [Constellation and Network Modeling](Constellation and Network Modeling)

All of the notebooks are built to attach to an open instance of STK. In this case STK 12 is used, although the user can specify the version to be STK 11. All of the inputs are at the top of the script. Inputs include chain names or constellation names, analysis start and stop time (there is also a step size a bit further down), either distance or timeDelay for finding the shortest path, and a dictionary of node processing delays which will be applied to every object in that constellation.

More information on each of the notebooks can be found in the readme within the project folder.

### Dependencies

* Licenses: Free, Integration, Communications, Pro
* Other Scripts: N/A
* Scenario: [Constellation and Network Routing Example](https://agiweb.secure.force.com/code/articles/Custom_Solution/Constellation-and-Network-Routing)

---
