import comtypes.gen.STKObjects
from comtypes.client import CreateObject, GetActiveObject
import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits import mplot3d

def StartSTK():
    try:
        uiApp = GetActiveObject('STK12.Application')

        stkRoot = uiApp.Personality2
        checkEmpty = stkRoot.Children.Count

        if checkEmpty == 0:
            uiApp.visible = 1
            uiApp.userControl = 1
            stkRoot.NewScenario('AviatorParametricDemo')
            scenario = stkRoot.CurrentScenario.QueryInterface(STKObjects.IAgScenario)
        else:
            ## Implement checking to see if I should close the scenario
            pass
    except:
        uiApp = CreateObject('STK12.Application')
        stkRoot = uiApp.Personality2
        uiApp.visible = 1
        uiApp.userControl = 1
        stkRoot.NewScenario('AviatorParametricDemo')
        scenario = stkRoot.CurrentScenario.QueryInterface(STKObjects.IAgScenario)

    stkRoot.UnitPreferences.SetCurrentUnit('DateFormat', 'EpHr')
    return stkRoot

def AviatorParametricDemo(stkRoot):
    aircraft = stkRoot.CurrentScenario.Children.New(STKObjects.eAircraft, 'AvtrAircraft')
    aircraftObject = aircraft.QueryInterface(STKObjects.IAgAircraft)
    aircraftObject.SetRouteType(STKObjects.ePropagatorAviator)
    avtrProp = aircraftObject.Route.QueryInterface(STKObjects.IAgVePropagatorAviator).AvtrPropagator
    avtrProp = avtrProp.QueryInterface(AgStkAvtrLib.IAgAvtrPropagator)

    avtrMission = avtrProp.AvtrMission
    phases = avtrMission.Phases
    phase = phases.Item(0)
    procedures = phase.Procedures

    ## Get the runways from the catalog
    runwayCategory = avtrProp.AvtrCatalog.RunwayCategory
    runwayCategory.ARINC424Runways.MasterDataFilepath = r'C:\Program Files\AGI\STK 12\Data\Resources\stktraining\samples\FAANFD18'
    runwayList = runwayCategory.ARINC424Runways.QueryInterface(AgStkAvtrLib.IAgAvtrCatalogSource).ChildNames
    JFK = runwayCategory.ARINC424Runways.GetARINC424Item('JOHN F KENNEDY INTL 04L 22R')
    LAX = runwayCategory.ARINC424Runways.GetARINC424Item('LOS ANGELES INTL 06L 24R')

    ac = avtrProp.AvtrCatalog.AircraftCategory

    if ac.AircraftModels.QueryInterface(AgStkAvtrLib.IAgAvtrCatalogSource).Contains('Advanced Airliner') > 0:
        ac.AircraftModels.QueryInterface(AgStkAvtrLib.IAgAvtrCatalogSource).RemoveChild('Advanced Airliner')

    basicAirliner = ac.AircraftModels.GetAircraft('Basic Airliner')
    advAirliner = basicAirliner.QueryInterface(AgStkAvtrLib.IAgAvtrCatalogItem).Duplicate();
    advAirliner = advAirliner.QueryInterface(AgStkAvtrLib.IAgAvtrAircraft)
    advAirliner.Name = 'Advanced Airliner'
    advTool = advAirliner.AdvFixedWingTool
    advTool.MaxMach = 0.88
    advTool.AeroStrategy = AgStkAvtrLib.eSubsonicAero
    advTool.PowerplantStrategy = AgStkAvtrLib.eTurbofanHighBypass
    engine = advTool.PowerplantModeAsEmpiricalJetEngine
    engine.MaxSeaLevelStaticThrust = 200000
    engine.DesignPointAltitude = 39000;
    advTool.CreateAllPerfModels('AdvancedModel', 1, 1)

    avtrMission.Vehicle = advAirliner

    takeoff = procedures.Add(AgStkAvtrLib.eSiteRunway, AgStkAvtrLib.eProcTakeoff)
    jfkRunway = takeoff.Site
    jfkRunway.QueryInterface(AgStkAvtrLib.IAgAvtrSiteRunway).CopyFromCatalog(JFK)

    enroute = procedures.Add(AgStkAvtrLib.eSiteRunway, AgStkAvtrLib.eProcEnroute)
    laxRunway = enroute.Site
    laxRunway.QueryInterface(AgStkAvtrLib.IAgAvtrSiteRunway).CopyFromCatalog(LAX)
    laxRunway.LowEndHeading = 35
    laxRunway.Name = 'LAX Alternate Runway'
    laxRunway.QueryInterface(AgStkAvtrLib.IAgAvtrSiteRunway).AddToCatalog(1)
    enroute.QueryInterface(AgStkAvtrLib.IAgAvtrProcedureEnroute).AltitudeMSLOptions.UseDefaultCruiseAltitude = 0

    landing = procedures.Add(AgStkAvtrLib.eSiteRunway, AgStkAvtrLib.eProcLanding)
    landing.Name = 'Landing'
    landingRunway = landing.Site
    laxAlternate = runwayCategory.UserRunways.GetUserRunway('LAX Alternate Runway')
    landingRunway.QueryInterface(AgStkAvtrLib.IAgAvtrSiteRunway).CopyFromCatalog(laxAlternate)

    altitudes = np.linspace(20000, 45000, 6)
    totalFuel = np.array([])
    totalTime = np.array([])

    for altitude in altitudes:
        print(f'Setting Altitude to {altitude} ft')
        enroute.QueryInterface(AgStkAvtrLib.IAgAvtrProcedureEnroute).AltitudeMSLOptions.MSLAltitude = altitude
        avtrProp.Propagate()
        flightDP = aircraft.DataProviders.Item('Flight Profile By Time').QueryInterface(STKObjects.IAgDataPrvTimeVar).Exec(4.5, 6.5, 3600)
        fuelUsed = flightDP.DataSets.GetDataSetByName('Fuel Consumed').GetValues()
        totalFuel = np.append(totalFuel, fuelUsed[-1])
        time = flightDP.DataSets.GetDataSetByName('Time').GetValues()
        totalTime = np.append(totalTime, time[-1])

    fig = plt.subplots(figsize = (12, 8))
    gridsize = (3,2)
    ax1 = plt.subplot2grid(gridsize, (0,0), colspan = 2, rowspan = 2, projection='3d')
    ax2 = plt.subplot2grid(gridsize, (2,0))
    ax3 = plt.subplot2grid(gridsize, (2,1))

    ax1.plot3D(altitudes, totalTime, totalFuel)
    ax1.set_title('Fuel Consumed and Time of Flight at Varying Altitudes')
    ax1.set_xlabel('Altitude (ft)')
    ax1.set_ylabel('Time of Flight (hrs)')
    ax1.set_zlabel('Fuel Consumed (lbs)')

    ax2.plot(altitudes, totalFuel)
    ax2.set_title('Fuel Consumed at Each Altitude')
    ax2.set_xlabel('Altitude (ft)')
    ax2.set_ylabel('Fuel Consumed (lbs)')

    ax3.plot(altitudes, totalTime)
    ax3.set_title('Total Time of Flight at Each Altidue')
    ax3.set_xlabel('Altitude (ft)')
    ax3.set_ylabel('Time of Flight (hrs)')

    plt.show()



if __name__ == "__main__":
    stkRoot = StartSTK()
    AviatorParametricDemo(stkRoot)