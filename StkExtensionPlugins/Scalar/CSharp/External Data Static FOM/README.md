## Bring External Data into a Figure of Merit

This folder contains the External Data Plugin C# project as well as the necessary .xml file and an example data file for a Figure of Merit over the United States.
The following is a step by step procedure for building and using a scalar calculation plug in to bring external data into a Figure of Merit (FOM)

### Plug in
1. Move ExternalFOMScalarPlugin.xml to C:/ProgramData/AGI/STK 12/Plugins.
2. Open Visual Studio as administrator.
3. Open ExternalFOMScalarPlugin.sln.
4. Rebuild soltuion.
5. Run STK.

### Scenario Set up
1. Create 2 Facility objects, one for constraints (Cnstr) and one to be an asset(Asset).
2. In the Constraints Basic page for both, disable the Line of Sight Constraints.
3. Create a Coverage object. For this example, ensure that it covers the United States.
4. Create a Figure of Merit (FOM)

### Create a Scalar Calculation
1. Open Analysis Workbench.
2. Select the Calculation tab.
3. Click the creat a new Scalar Calculation component.
4. Set the Type to Plugin and give it a name.
5. Select Scalar External FOM from the Select Plugin Name drop-down list.
6. Click OK to close and create the new scalar calc component. The component is placed under the facility’s My Components directory in Analysis Workbench Calculation Tool.

### Load the FOM Values Data File
1. Re-open the scalar calculation.
2. Select the Plugin Settings option.
3. Double-click on the FilePath option to browse to the location of the saved lat,lon, combined FOM values data file.
4. Click OK. You may need to adjust the Tolerance field if your data file lat/lon points do not exactly match the coverage grid points. For the included example data set the tolerance around 1 degree.

### Use the Constraint Object for Coverage
1. Open the Coverage object's properties.
2. Select the Basic - Grid page.
3. Click the Grid Constraint Options button.
4. Enable the Use Object Instance option. 
5. Select the Cnstr facility.
6. Enable the Use Actual Object on the Grid Points option. 
7. Click OK to accept changes and close the Grid Constraint Options page.
8. Select the Basic - Assets page. Ensure you only have selected/Assigned one object (Asset) that is always visible to the coverage grid at all times (Line of Sight constraint disabled).
9. Select the Basic - Interval page.
10. Set the Access Interval to a one (1) second duration since only a static FOM contour display is being generated (this saves on computational time).
11. Click OK on the coverage properties to accept changes and close the panel.

### Figure of Merit
1. Open FOM's Properties
2. Select the Basic - Definition page.
3. Select the Scalar Calculation option as Type. 
4. Browse to select the facility object’s scalar plugin calculation which you had created.
5. Ensure that the time step is set to one (1) sec. 
6. Click OK to accept changes and close the FOM properties panel.

### Compute Access
1. Right-click on the coverage object in the Object Browser.
2. Extend the CoverageDefintion menu and select Compute Accesses.
3. When the coverage computation has completed, right-click on the FOM and select the Report & Graph Manager.
4. Generate a Value By Grid Point report to ensure that the combined FOM values from the external data file are present.
5. Generate the 2D Graphics - Static combined FOM value contour scale based on the range of combined FOM values present in the Value By Grid Point report 