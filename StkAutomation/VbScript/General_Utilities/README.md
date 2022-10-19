# VbScript General Utilities Samples

## [AttitudeEuler.vbs](AttitudeEuler.vbs)

This script will take the "Euler Angles" report and generate an attitude file from it. The script will set the epoch to be the date of the first data point.

**Note**: Gregorian date format is the expected input

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [AttitudeYPR.vbs](AttitudeYPR.vbs)

This script will take the "Yaw Pitch Roll" report and generate an attitude file from it. The script will set the epoch to be the date of the first data point.

**Note**: Gregorian date format is the expected input

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [Excel_Command_Sender_Example.xlsb](Excel_Command_Sender_Example.xlsb)

This excel workbook will allow you to define a column with the header "CMDS_HERE" and send all valid connect commands in that column by pressing the "Send CON Cmds" button.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [STK_Excel.xlsm](STK_Excel.xlsm)

This excel workbook is a basic example of integrating excel and STK.  There are a collection of buttons that exectue various tasks such as starting STK, creating objects from data in the excel sheet, and grabbing data from STK to add to the excel sheet.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration)
* Other Scripts: N/A
* Scenario: N/A

---

## [TerrainConverter](TerrainConverter)

This script allows users to convert multiple terrain files of different formats at once. Once an input directory containing terrain data is chosen, clicking "Get Files" populates the list and allows files to be converted to .pdtt files. 


### Dependencies

* Capabilities: Free
* Other Scripts: N/A
* Scenario: N/A

---

## [AviatorWaypointsConnect.vbs](AviatorWaypointsConnect.vbs)

This script will take the waypoints located in the file "C:/temp/waypoints.txt" and convert it into an Aviator route with waypoints specified by the file. 

**Note**: If there is an Aircraft1 already, it will rewrite any existing waypoints.

### Dependencies

* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Aviator](https://www.agi.com/products/stk-specialized-modules/stk-aviator)
* Other Scripts: N/A
* Scenario: N/A

---

## [CovDP_PlusComments.vbs](CovDP_PlusComments.vbs)

Sample VBscript to connect to STK and pull Coverage Data Providers

### Dependencies


* Capabilities: Free, [Integration](https://www.agi.com/products/stk-systems-bundle/stk-integration), [Coverage](https://www.agi.com/products/stk-systems-bundle/stk-coverage)
* Other Scripts: N/A
* Scenario: Any scenario open with a coverage definition. 

---
