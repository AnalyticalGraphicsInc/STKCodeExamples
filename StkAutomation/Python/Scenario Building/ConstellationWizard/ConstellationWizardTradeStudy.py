
# %% Constellation Wizard
import numpy as np
# Define the constellation and inputs
constellationList = ['Oneweb','SpaceX','Telesat'] # List of different constellations to run analysis
satTemplateList = ['OneWeb','StarLink1','TeleSat1'] # Used for deck access constraints and child objects when loading satellites. This can be an empty string '' 
transmittersList = ['OneWebTransmitter','StarLinkTransmitter','TeleSatTransmitter']
accessObjPath = '*/Facility/AGI/Sensor/FOV' # Used for deck access
startTimes = np.arange(0,3600,600) # Deck Access and analysis start time. Relative to the scenario start time [EpSec]
stopTimes = np.arange(600,4200,600) # Deck Access and analysis stop time. Relative to the scenario start time [EpSec]

# %% Loop through each constellation
# Import libraries
from DeckAccessReader import *
import pandas as pd
import os
cwd = os.getcwd()
cwdFiles = cwd+'\\Files'
from comtypes.client import CreateObject
from comtypes.client import GetActiveObject
from comtypes.gen import STKObjects

# Connect To STK
scenarioPath = cwd+'\\ConstellationWizardExampleScenario'
scenarioName = 'ConstellationWizardExample'
root = ConnectToSTK(scenarioPath = scenarioPath,scenarioName=scenarioName) # Tries to connect to open scenario,then Load scenario,then create new scenario
sc = root.CurrentScenario
sc2 = root.CurrentScenario.QueryInterface(STKObjects.IAgScenario)

for constellationName,satTemplateName,transmitters in zip(constellationList,satTemplateList,transmittersList):
    for startTime,stopTime in zip(startTimes,stopTimes):
        # Create the constellation if needed
        TLEFileName = cwdFiles+'\\Constellations\\'+constellationName+'.tce'

        # Look at TLEs
        tleList = getTLEs(TLEFileName)
        dfTLE = tleListToDF(tleList)

        # Run deck Access, using a constraint object if it exists
        startTime = sc2.StartTime+startTime
        stopTime = sc2.StartTime+stopTime
        NumOfSC,deckAccessFileName,deckAccessTLEFileName = runDeckAccess(root,startTime,stopTime,TLEFileName,accessObjPath,constraintSatName=satTemplateName)

        # Read deck access report file
        dfAccess = deckAccessReportToDF(deckAccessFileName)

        # Look at TLEs with access
        tleList = getTLEs(deckAccessTLEFileName)
        dfTLEwAccess = tleListToDF(tleList)
        NewTLEFileName = TLEFileName.split('.')[0] + 'DeckAccess.tce'
        dfToTLE(dfTLEwAccess,NewTLEFileName) # Example of how to save DeckAccess satellites to a TCE file

        # Satellites to load
        # dfLoad = dfTLE # Load all satellites in TLE files
        # dfLoad = dfTLE[dfTLE['i']>45] # Use subset based on filtering
        # dfLoad = dfTLEwAccess[(dfTLEwAccess['i']>45) & (dfTLEwAccess['RAAN'].astype(float)>180)] # Use subset based on deck access and filtering
        dfLoad = dfTLEwAccess # Use subset based on deck access

        # Load satellites using a satellite template
        LoadSatsUsingTemplate(root,dfLoad,startTime,stopTime,TLEFileName,satTemplateName,color='green')

        chainPath = '*/Chain/AGIToConstellation'
        accessReceiver = '*/Facility/AGI/Sensor/FOV/Receiver/GroundReceiver'
        objsToAdd = ['*/Constellation/'+transmitters+'s',accessReceiver]
        exportFileName = cwdFiles+'\\AnalysisResults\\'+constellationName+'ChainCommStart'+str(int(startTime))+'Stop'+str(int(stopTime))+'.csv'
        dfChainData = chainAnalysis(root,chainPath,objsToAdd,startTime,stopTime,exportFileName)

        covDefPath = '*/CoverageDefinition/CovUS'
        coverageAssets = ['Constellation/'+constellationName]
        exportFileName = cwdFiles+'\\AnalysisResults\\'+constellationName+'NAssetStart'+str(int(startTime))+'Stop'+str(int(stopTime))+'.csv'
        dfCovAnalysis = covAnalysis(root,covDefPath,coverageAssets,startTime,stopTime,exportFileName)

        commSysPath = '*/CommSystem/InterferanceAnalysis'
        accessReceiver = 'Facility/AGI/Sensor/FOV/Receiver/GroundReceiver'
        interferanceTransmitters = ['*/Constellation/'+transmitters+'s']
        exportFileName = cwdFiles+'\\AnalysisResults\\'+constellationName+'InterferanceLinkBudgetStart'+str(int(startTime))+'Stop'+str(int(stopTime))+'.csv'
        dfCommSys = commSysAnalysis(root,commSysPath,accessReceiver,interferanceTransmitters,startTime,stopTime,exportFileName)

        # Unload Satellites and Constellations
        UnloadObjs(root,'Satellite',pattern='tle-*')
        UnloadObjs(root,'Constellation',pattern=satTemplateName[0:2]+'*')
        UnloadObjs(root,'Constellation',pattern=constellationName+'*')
