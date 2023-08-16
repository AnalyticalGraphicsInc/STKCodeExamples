# Python Scenario Building Samples

## [Aviator_ObjectModel_CarrierLanding.py](Aviator_ObjectModel_CarrierLanding.py)

Creates a scenario that demonstrates an aircraft carrier landing using two Aviator aircraft with custom procedures. All inputs are available for editing, though this is better used as a reference for planning Aviator missions programmatically.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Aviator](https://www.agi.com/products/stk-specialized-modules/stk-aviator)
* Other Scripts: N/A
* Scenario: N/A

---

## [STK_OM_Tutorial](STK_OM_Tutorial)

STK Object Model Walkthrough using Python with comtypes. In the incomplete version, the user is meant to write lines where "Action" is requested in a comment. The comtypes interface is no longer the recommended way of interacting with STK via python (see the [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm) available in STK 12.1+), but this tutorial is helpful for understanding how the STK object model works. More tutorial content for the STK object model is available in the [Level 2 Integration Certification](https://register.agi.com/training/certification/?cert=integration).

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A

---

## [AstrogatorObjectModel](AstrogatorObjectModel)

These notebooks walk you through an introduction to [STK Astrogator](https://help.agi.com/stk/index.htm#astrogator.htm) object model. The tutorial builds a LEO to GEO transfer using a combined inclination and apogee raise maneuver at GEO. The walkthrough demonstrates using a target sequence for a Hohmann Transfer. The comtypes interface is no longer the recommended way of interacting with STK via python (see the [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm?Highlight=python%20api) available in STK 12.1+), but this tutorial is helpful for understanding how the STK Astrogator object model works. More tutorial content for the STK object model is available in the [Level 2 Integration Certification](https://register.agi.com/training/certification/?cert=integration).

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Astrogator](https://www.agi.com/products/stk-specialized-modules/stk-astrogator)
* Other Scripts: N/A
* Scenario: N/A

---

## [PythonAPI12.2_Demo](PythonAPI12.2_Demo)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm) available in STK 12.1+

This script is a demo of the STK Python API that creates a complex search and rescue scenario using custom Aviator models and mission procedures. The only user input required is a Cesium Ion access code, but Cesium Ion buildings can be turned off in the variable list at the top of the script. This script should prove a useful tool for learning to use the Python API for creating Aviator and Communications objects. A custom 3D model of a Cessna 206 aircraft is included with this demo.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Aviator](https://www.agi.com/products/stk-specialized-modules/stk-aviator), [Communications](https://www.agi.com/capabilities/communications)
* Other Scripts: N/A
* Scenario: N/A

---
## [VolumetricSensorCoverage.py](VolumetricSensorCoverage.py)

This script performs an end-to-end volumetric coverage analysis with sensors. It will build a scenario with three GEO satellites, a volume grid that covers the GEO belt, and a volumetric object to perform the analysis. This script leverages the comtypes interface.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---
## [VolumetricSensorCoverage_pythonAPI.py](VolumetricSensorCoverage_pythonAPI.py)

This script performs an end-to-end volumetric coverage analysis with sensors. It will build a scenario with three GEO satellites, a volume grid that covers the GEO belt, and a volumetric object to perform the analysis. This script leverages the python cross-platform API.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---
