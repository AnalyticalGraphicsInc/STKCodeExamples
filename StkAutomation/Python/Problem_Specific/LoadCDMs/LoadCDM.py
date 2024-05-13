# this script will 
# 1. connect to a running instance of STK
#    - it expects the STK Python API to be installed
#    - ensure the scenario interval covers the conjunctions
# 2. parse the specified CDM
# 3. generate satellites using the data from the CDM
# 4. if desired, load satellites into AdvCAT
#
# author: jens ramrath, agi
# date: 29 april 2024

##### INIT #####
cdmPath = r'C:\temp\1year\LoadCDMs\SomeCDM.xml'
addAdvCat = True


##### RUN #####
# parse cdm
from ParseCdm import *
allCdmData = ParseCdm.ParseCdmFromFile(cdmPath)



# connect to running instance of STK
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkutil import *
from agi.stk12.stkobjects import *


stk = STKDesktop.AttachToApplication()
root = stk.Root
scenario = root.CurrentScenario

print('connected to ' + scenario.InstanceName)



# loop through conjunctions in CDM file
from AddSatellite import *

for cdmData in allCdmData:
    # set up AdvCat
    if addAdvCat:
        aCatName = cdmData.ID.replace('CDM_ID:', '')
        aCat = root.CurrentScenario.Children.New(AgESTKObjectType.eAdvCat, aCatName)
        
        currentTimeUnits = root.UnitPreferences.GetCurrentUnitAbbrv('dateFormat')
        tcaEpSec = root.ConversionUtility.ConvertDate('ISO-YMD', 'EpSec', cdmData.TCA)
        aCatStartTime = root.ConversionUtility.ConvertDate('EpSec', currentTimeUnits, str(float(tcaEpSec) - 7200))
        aCatStopTime  = root.ConversionUtility.ConvertDate('EpSec', currentTimeUnits, str(float(tcaEpSec) + 7200))
        aCat.TimePeriod.SetExplicitInterval(aCatStartTime, aCatStopTime)

    # loop through satellites in each conjunction
    addedPrimary = False
    for segment in cdmData.Segment:
        
        #validName = cdmData.ID.replace('CDM_ID:', '') + '_' + segment.OBJECT_NAME.replace(' ', '_').replace("(", "").replace(")", "").replace("/", "")

        sat = AddSatellite(segment, root, cdmData.TCA)

        if addAdvCat:
            if not addedPrimary:
                root.ExecuteCommand('ACAT */AdvCAT/' + aCatName + ' Primary Add "Satellite/' + segment.UniqueName + '" Cov')
                addedPrimary = True
            else:
                root.ExecuteCommand('ACAT */AdvCAT/' + aCatName + ' Secondary Add "Satellite/' + segment.UniqueName + '" Cov')
                aCat.Compute()
