# MATLAB Scenario Building Samples

## [getDCM.m](getDCM.m)

Allows users to output a quaternion from the Axes Choose Axes data provider and convert that quaternion into a Direction Cosine Matrix (DCM). The user first specifies and object and coordinate system that will be the system of interest or the "to" system in the transformation. Then specifies a coordinate system that will be the reference or "from" system and finally specifies a time for the DCM to be computed at. Note that this time must be within the scenario time period.

ex. `getSTKDCM('Satellite1','Body','CentralBody/Earth J2000','15 Apr 2021 18:00:30.000')`

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---