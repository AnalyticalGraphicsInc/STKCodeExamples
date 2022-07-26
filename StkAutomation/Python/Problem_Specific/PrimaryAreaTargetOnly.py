# this script will load all regions of a selected .shp file, 
# find the largest AreaTarget and remove all others that start 
# with the same name
#
# author: jens ramrath, agi
# date: 26 june 2022

# connect to running instance of STK
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
stk = STKDesktop.AttachToApplication()

root = stk.Root 

# import shapefile
countryName = "Spain"
root.ExecuteCommand('GIS * Import \"C:\\Program Files\\AGI\\STK 12\\Data\\Shapefiles\\Countries\\' + countryName + '\\' + countryName + '.shp\" AreaTarget')

# get all AreaTarget objects in scenario
areaTargets = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eAreaTarget)

maxSizeArea = 0
maxSizeName = ''

# loop through all AreaTargets to find largest one
for thisAT in areaTargets:

    # check if it's the correct country
    if countryName in thisAT.InstanceName:

        # get size
        dp = thisAT.DataProviders.GetDataPrvFixedFromPath('Bounding Rectangle/Geometry')
        dpResults = dp.Exec()
        thisArea = dpResults.DataSets.GetDataSetByName('Area')

        # check if it's largets one
        if thisArea.GetValues()[0] > maxSizeArea:
            maxSizeArea = thisArea.GetValues()[0]
            maxSizeName = thisAT.InstanceName


# loop through all AreaTargets to remove all but largest one. It also keeps AreaTargets that are not associates with this country
for thisAT in areaTargets:

    # check if it's the correct country
    if countryName in thisAT.InstanceName:

        # remove if not largest
        if not thisAT.InstanceName == maxSizeName:
            thisAT.Unload()
    

# rename larget AreaTarget to countryName
root.ExecuteCommand('Rename */AreaTarget/' + maxSizeName + ' ' + countryName)

