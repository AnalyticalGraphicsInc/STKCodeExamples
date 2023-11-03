# C# General Utility Samples

## [DataProviderExplorer](DataProviderExplorer)

This utility will allow you to connect to an open instance of STK, choose a specific data provider and export to an [Ansys](https://ansys.com) Library File.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [PullDataFromSTKExample](PullDataFromSTKExample)

This example utility shows how to stream AER data between a satellite and facility as a scenario animates.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [SendEphemerisToSTK](SendEphemerisToSTK)

This utility will allow you to add a new option to your Windows right-click > Send To menu for you to send files directly to STK.

This was originally designed to send Satellite Ephemeris files to STK (by the [ComSpOC](https://comspoc.com/) Operators) but has been expanded to include TLE and TCE files.

Recent updates have been added to now include GPX files imported as waypoints on a ground vehicle.  This utility can continue to be extended with additional file types/extensions.

#### Installation

* Build the project to create the executable
* Move the built executable "SendEphemerisToSTK.exe" to a permanent location of your choice
* Place a shortcut to that executable here: %AppData%/Microsoft/Windows/SendTo
* Rename that shortcut "STK"
* Right click on any Ephemeris File and on the Send To menu you will see an STK option.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [GetObjectsByType.cs](GetObjectsByType.cs)

Function to grab all instances of `<object type>` STK objects. Returns a list containing the handles to all `<object type>` objects.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---
  
## [GetSegmentsByType.cs](GetSegmentsByType.cs)

  Function to grab all instances of `<segment type>` Astrogator segments. Returns a list containing the handles to all `<segment type>` segments.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf), [STK Premium Space](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-space-brochure.pdf)
* Other Scripts: N/A
* Scenario: A scenario with at least one propagated Astrogator satellite

---
