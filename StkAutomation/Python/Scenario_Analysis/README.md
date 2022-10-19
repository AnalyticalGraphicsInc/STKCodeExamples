# Python Scenario Analysis Samples

## [ForceComparison.py](ForceComparison.py)

This script will create a number of force model vectors on satellites at different altitudes and then evaluate the vector magnitudes. The user should edit the scenario start and stop times, as well as the semimajor axis and inclination of the satellites. HPOP is used to propagate the satellites.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [Aviator_ObjectModel_FuelConsumptionStudy.py](Aviator_ObjectModel_FuelConsumptionStudy.py)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm) available in STK 12.1+

This script is an example of computing trade studies using the Aviator Object Model. No changes are needed for this script to run, as it constructs its own scenario with an Aviator aircraft defined by the Advanced Fixed Wing Tool. As is, the script iterates through flight altitudes and plots the fuel consumed, time of flight and altitude in a 3D graph.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Aviator](https://www.agi.com/products/stk-specialized-modules/stk-aviator)
* Other Scripts: N/A
* Scenario: N/A

---

## [Lifetime Analysis](./Lifetime%20Analysis)

Run trade studies with the Lifetime Tool. Grid searches, Latin Hyper Cube sampling and Monte Carlo analyses are possible. HPOP can also be run to compare to the Lifetime Tool. Multiple instances of STK can be started to speed up the trade studies. Results are saved to a csv file and trade study configurations are saved so they can be loaded and modified later.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [SatPro](https://www.agi.com/products/stk-specialized-modules/stk-satpro)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: pyDOE2, poliastro

---

## [Constellation and Network Modeling](./ConstellationAndNetworkRouting)

All of the notebooks are built to attach to an open instance of STK. In this case STK 12 is used, although the user can specify the version to be STK 11. All of the inputs are at the top of the script. Inputs include chain names or constellation names, analysis start and stop time (there is also a step size a bit further down), either distance or timeDelay for finding the shortest path, and a dictionary of node processing delays which will be applied to every object in that constellation.

More information on each of the notebooks can be found in the readme within the project folder.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Communications](https://www.agi.com/products/stk-systems-bundle/stk-communications), [Pro](https://www.agi.com/products/stk-systems-bundle/stk-professional)
* Other Scripts: N/A
* Scenario: [Constellation and Network Routing Example](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/674cb49a-f67e-40f5-b7c5-0fd57abbb879)

---

## [EOIR_Synthetic_Scene_and_Data_Generation](./EOIR_Synthetic_Scene_and_Data_Generation)

Automate generating EOIR Synthetic Scene data. Define the analysis interval and the EOIR Atmosphere & Cloud settings. The notebook cycles through the scene, with each setting, and outputs data for each frame. Resulting data (bitmap and/or raw text files) is saved to unique directories for making videos or post processing. 

More information on each of the notebooks can be found in the readme within the project folder.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [EOIR](https://www.agi.com/products/stk-specialized-modules/stk-eoir)
* Other Scripts: N/A
* Scenario: [EOIR Cloud Air To Air Scenario](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/6102d7ad-b7e7-4b1a-a406-5f4927a8651c)

--- 

## [Volumetric_Percent_Satisfied](Volumetric_Percent_Satisfied.py)

Currently the "Percent Satisfied" report for a voluemtric object in STK reports the percentage of points that are "Active". This script allows the user to assign a satisfaction threshold and computes the volumetric's percent satisfaction from that value.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench), [Coverage](https://www.agi.com/products/stk-systems-bundle/stk-coverage)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: Numpy

--- 