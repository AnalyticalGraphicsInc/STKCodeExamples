# STK Engine (C#)

STK Engine programs using C#.

---
## [OrbitSwitcher](OrbitSwitcher)
An STK Engine 'No Graphics' utility using C# scripting. This project is meant to serve as a companion utility to help users convert their satellite's Cartesian coordinates in a fixed frame to Keplerian elements in an intertial frame. This function does not use a satellite object but instead utilizes the conversion utility within STK Engine. The Keplerian elements you would like to return can be specified by changing the enum values near the top of the Main function. The results are printed to the command line.

---
## [StkMetaDataExtractor](StkMetaDataExtractor)
This STK Engine app will read an STK scenario with an instance of STK Engine to extract metadata from that scenario about each of the objects. It will also export a Cesium display summary file. As a Code Example, this utility should demonstrate how users can iterate through all objects in a scenario and interrogate each object type for information related to that specific object type. Included in this project are STK 12 and STK 11 versions of this STK Engine function. The extracted data are exported to an XML file.

---

## [WindowsFormsStarter](WindowsFormsStarter)
This is an STK Engine 11 solution that provides the user with an example STK Engine application with 3D and 2D graphics embedded in a Windows Form. Both [StkEngineWindowsFormStarter](Stk11.EngineWindowsFormStarter) and [StkEngineWindowsFormStarterLight](Stk11.EngineWindowsFormStarterLight) create a windows form with the ability to create a new STK scenario using an embedded button, then use interactive 3D and 2D graphics. StkEngineWindowsFormStarter includes an animation toolbar overlay for the 3D graphics window of the application that allows the user to play the scenario as one might in the STK UI. StkEngineWindowsFormStarterLight does not include this toolbar.

---

