import sys
try:
    from agi.stk12.stkdesktop import STKDesktop
    from agi.stk12.stkobjects import *
    from agi.stk12.stkobjects.aviator import *
    from agi.stk12.utilities.colors import *
except:
    print("Failed to import stk modules. Make sure you have installed the STK Python API wheel (agi.stk<..ver..>-py3-none-any.whl) from the STK Install bin directory")
try:
    from mpl_toolkits import mplot3d
    import matplotlib.pyplot as plt
    import numpy as np
except:
    print("**** Error: Failed to import one of the required modules (mpl_toolkits, matplotlib, numpy). Make sure you have them installed. If you are using anaconda python, make sure you are running the sample from an anaconda command prompt.")
    sys.exit(1)

def StartSTK():
    try:
        uiApp = STKDesktop.AttachToApplication()

        stkRoot = uiApp.Root
        checkEmpty = stkRoot.Children.Count

        if checkEmpty == 0:
            uiApp.visible = True
            uiApp.userControl = True
            stkRoot.NewScenario('AviatorParametricDemo')
            scenario = AgScenario(stkRoot.CurrentScenario)
        else:
            ## Implement checking to see if I should close the scenario
            pass
    except:
        uiApp = STKDesktop.StartApplication(visible=True, userControl=True)
        stkRoot = uiApp.Root
        stkRoot.NewScenario('AviatorParametricDemo')
        scenario = AgScenario(stkRoot.CurrentScenario)

    stkRoot.UnitPreferences.SetCurrentUnit('DateFormat', 'EpHr')
    return stkRoot

def AviatorParametricDemo(stkRoot):
    aircraft = AgAircraft(stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, 'AvtrAircraft'))
    aircraft.SetRouteType(AgEVePropagatorType.ePropagatorAviator)
    avtrProp = AgAvtrPropagator(AgVePropagatorAviator(aircraft.Route).AvtrPropagator)

    avtrMission = avtrProp.AvtrMission
    phases = avtrMission.Phases
    phase = phases[0]
    procedures = phase.Procedures

    ## Get the runways from the catalog
    runwayCategory = avtrProp.AvtrCatalog.RunwayCategory
    runwayCategory.ARINC424Runways.MasterDataFilepath = r'C:\Program Files\AGI\STK 12\Data\Resources\stktraining\samples\FAANFD18'
    JFK = runwayCategory.ARINC424Runways.GetARINC424Item('JOHN F KENNEDY INTL 04L 22R')
    LAX = runwayCategory.ARINC424Runways.GetARINC424Item('LOS ANGELES INTL 06L 24R')

    ac = avtrProp.AvtrCatalog.AircraftCategory

    if ac.AircraftModels.Contains('Advanced Airliner') > 0:
        ac.AircraftModels.RemoveChild('Advanced Airliner')

    basicAirliner = ac.AircraftModels.GetAircraft('Basic Airliner')
    advAirliner = basicAirliner.Duplicate();
    advAirliner.Name = 'Advanced Airliner'
    advTool = advAirliner.AdvFixedWingTool
    advTool.MaxMach = 0.88
    advTool.AeroStrategy = AgEAvtrAdvFixedWingAeroStrategy.eSubsonicAero
    advTool.PowerplantStrategy = AgEAvtrAdvFixedWingPowerplantStrategy.eTurbofanHighBypass
    engine = advTool.PowerplantModeAsEmpiricalJetEngine
    engine.MaxSeaLevelStaticThrust = 200000
    engine.DesignPointAltitude = 39000;
    advTool.CreateAllPerfModels('AdvancedModel', 1, 1)

    avtrMission.Vehicle = advAirliner

    takeoff = procedures.Add(AgEAvtrSiteType.eSiteRunway, AgEAvtrProcedureType.eProcTakeoff)
    jfkRunway = takeoff.Site
    AgAvtrSiteRunway(jfkRunway).CopyFromCatalog(JFK)
    

    enroute = procedures.Add(AgEAvtrSiteType.eSiteRunway, AgEAvtrProcedureType.eProcEnroute)
    laxRunway = enroute.Site
    AgAvtrSiteRunway(laxRunway).CopyFromCatalog(LAX)
    laxRunway.LowEndHeading = 35
    laxRunway.Name = 'LAX Alternate Runway'
    AgAvtrSiteRunway(laxRunway).AddToCatalog(1)
    AgAvtrProcedureEnroute(enroute).AltitudeMSLOptions.UseDefaultCruiseAltitude = 0

    landing = procedures.Add(AgEAvtrSiteType.eSiteRunway, AgEAvtrProcedureType.eProcLanding)
    landing.Name = 'Landing'
    landingRunway = landing.Site
    laxAlternate = runwayCategory.UserRunways.GetUserRunway('LAX Alternate Runway')
    AgAvtrSiteRunway(landingRunway).CopyFromCatalog(laxAlternate)

    altitudes = np.linspace(20000, 45000, 6)
    totalFuel = np.array([])
    totalTime = np.array([])

    for altitude in altitudes:
        print(f'Setting Altitude to {altitude} ft')
        AgAvtrProcedureEnroute(enroute).AltitudeMSLOptions.MSLAltitude = altitude
        avtrProp.Propagate()
        flightDP = AgDataPrvTimeVar(aircraft.DataProviders['Flight Profile By Time']).Exec(4.5, 6.5, 3600)
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
    