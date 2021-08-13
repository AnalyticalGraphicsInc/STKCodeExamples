# VbScript Problem Specific Samples

## [ASCOM Telescope Utility_STK11](ASCOM%20Telescope%20Utility_STK11)

This utility is used as an example for integration between STK and third party applications such as the AStronomy Common Object Model (ASCOM).  This utility will allow users to take advantage of STK’s system modeling capability by creating satellites, or other celestial orbiting bodies, and determine pointing geometry to specified geographic locations on the ground by using STK’s ability to model ‘Facility’ locations and advanced propagation algorithms to create orbiting bodies.

**Note**: For more information about using this utility refer to the full [documentation](ASCOM%20Telescope%20Utility_STK11/ASCOM%20Telescope%20Utility%20README.docx)

**Note**: This is a VB.NET project and not VbScript

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---

## [dataProviderToScalarFile.vbs](dataProviderToScalarFile.VBS)

This script will take a custom report with just one data provider and a set of time steps and turn it into a scalar file.

### Dependencies

* Licenses: Free, Integration, Analysis Workbench
* Other Scripts: N/A
* Scenario: N/A

---

## [panorama.vbs](panorama.vbs)

This script will create a 5 3D Windows so that these can be combine and pieced together to make a panorama view.

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---

## [AdvCatAutomation.xlsm](AdvCatAutomation.xlsm)

This tool shows the ins and outs of automating the AdvCAT module of STK using Excel. The code provides examples of creating satellites from files, setting thresholds in AdvCAT, and manually setting orbital elements/covariance. The code also shows how to to manipulate the HPOP propagator. This tool was originally designed to follow along with [this tutorial](https://help.agi.com/stk/index.htm#training/AdvCatTool.htm). Open an instance of STK using the controls in the upper left corner of the page. The lower left hand box provides all the requirements for the conjunction analysis and the variable shown are the same as the ones shown in the STK GUI version. The file paths for the primary and secondary satellites can be either a TLE/TCE (which will import all assets in the file) or an ephemeris file. Note that you need to change the filepaths before usage. If the checkboxes are selected on the right, then it allows the user to override the file option and hand-define the satellite position and covariance. This was included to show how custom covariance can be added and how a user can manipulate the HPOP propagator using scripting. All of the main code is located on the "Compute Conjunction Analysis" button. To access the code, right-click on the button and click on “Assign Macro”. On the new page click “Edit”, which will open up the VBA code editor and bring you to the code behind the button.

### Dependencies

* Licenses: Free, Integration, Pro, CAT
* Other Scripts: N/A
* Scenario: N/A

---
