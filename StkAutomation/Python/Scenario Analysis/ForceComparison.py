# this script will create a number of force model vectors on satellites at 
# different altitudes and then evaluate the vector magnitudes
# author: jens ramrath, agi
# date: 22 july 2020

import numpy as np
import comtypes
from comtypes.client import CreateObject
from comtypes.client import GetActiveObject

from comtypes.gen import STKUtil
from comtypes.gen import STKObjects
from comtypes.gen import AgSTKVgtLib



def ForceComparison():
    if True:
        # create new scenario
        uiApp = CreateObject('STK12.Application')
        uiApp.Visible = True
        uiApp.UserControl = True
    
        root = uiApp.Personality2
        root.NewScenario("PerturbingForceComparison")
    else:
        # connect to running scenario
        uiApp = GetActiveObject('STK12.Application')
        uiApp.UserControl = True
        uiApp.Visible = True
        root = uiApp.Personality2
    
    ####################
    ##### SCENARIO #####
    ####################
    sc = root.CurrentScenario
    iagSc = sc.QueryInterface(STKObjects.IAgScenario)
    iagSc.AnalysisInterval.SetStartAndStopTimes("1 Jan 2020 00:00:00.00", "2 Jan 2020 00:00:00.00")
    root.Rewind
    
    
    #############################
    ##### CREATE SATELLITES #####
    #############################
    sats = np.array(["LEO_300km", "LEO_400km", "LEO_600km", "LEO_800km", "GPS", "GEO"])
    sma  = np.array([6678, 6778, 6978, 7178, 26600, 42165])
    inc  = np.array([98.0, 98.0, 98.0, 98.0, 55.0, 0.0])
    
    for thisSat in sats:
        print("Creating */Satellite/" + thisSat)
    
        oSat = sc.Children.New(STKObjects.eSatellite, thisSat)
        sat = oSat.QueryInterface(STKObjects.IAgSatellite)
        
        sat.SetPropagatorType(0) # ePropagatorHPOP
        prop = sat.Propagator.QueryInterface(STKObjects.IAgVePropagatorHPOP)
        prop.Step = 60
        prop.InitialState.Representation.AssignClassical(11, sma[np.where(sats == thisSat)[0]], 0.0, inc[np.where(sats == thisSat)[0]], 0.0, 0.0, 0.0)
    
        forceModel = prop.ForceModel
        forceModel.CentralBodyGravity.File = 'C:\Program Files\AGI\STK 12\STKData\CentralBodies\Earth\WGS84_EGM96.grv'
        forceModel.CentralBodyGravity.MaxDegree = 21
        forceModel.CentralBodyGravity.MaxOrder = 21
        forceModel.Drag.Use=1
        forceModel.Drag.DragModel.Cd=0.01
        forceModel.Drag.DragModel.AreaMassRatio=0.01
        forceModel.SolarRadiationPressure.Use=1
    
        prop.Propagate()
    
    
    
    
    ######################################
    ##### CREATE FORCE MODEL VECTORS #####
    ######################################
    # can't create ForceModel vectors with the OM so connect all the way
    vectors = []
    
    #######################
    ### GRAVITY VECTORS ###
    #######################
    # Point Mass
    GravityVector(root, "PointMass", 0, 0)
    vectors.append("PointMass")
    
    # J2
    GravityVector(root, "J2", 2, 0)
    vectors.append("J2")
    
    # J4
    GravityVector(root, "J4", 4, 0)
    vectors.append("J4")
    
    # J2/2
    GravityVector(root, "J2-2", 2, 2)
    vectors.append("J2-2")
    
    # J4/4
    GravityVector(root, "J4-4", 4, 4)
    vectors.append("J4-4")
    
    # J8/8
    GravityVector(root, "J8-8", 8, 8)
    vectors.append("J8-8")
    
    # J12/12
    GravityVector(root, "J12-12", 12, 12)
    vectors.append("J12-12")
    
    # J24/24
    GravityVector(root, "J24-24", 24, 24)
    vectors.append("J24-24")
    
    # J70/70
    GravityVector(root, "J70-70", 70, 70)
    vectors.append("J70-70")    
    
    
    ######################
    ### CENTRAL BODIES ###
    ######################
    # Sun
    thisVector = "SunForce"
    print("Creating vector: " + thisVector)
    vectors.append(thisVector)
    root.ExecuteCommand("VectorTool * Satellite Create VectorTemplate SunForce \"Force Model\" Scale 1.0 CentralBody Earth")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SunForce \"Force Model\" Force UseCBGravity Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SunForce \"Force Model\" Drag Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SunForce \"Force Model\" Force SRP Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SunForce \"Force Model\" Force ThirdBodyGravity Sun On FromCB")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SunForce \"Force Model\" Force ThirdBodyGravity Moon Off")
    
    # Moon
    thisVector = "MoonForce"
    print("Creating vector: " + thisVector)
    vectors.append(thisVector)
    root.ExecuteCommand("VectorTool * Satellite Create VectorTemplate MoonForce \"Force Model\" Scale 1.0 CentralBody Earth")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate MoonForce \"Force Model\" Force UseCBGravity Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate MoonForce \"Force Model\" Drag Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate MoonForce \"Force Model\" Force SRP Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate MoonForce \"Force Model\" Force ThirdBodyGravity Sun Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate MoonForce \"Force Model\" Force ThirdBodyGravity Moon On FromCB")
    
    # Mars
    CentralBodyForce(root, "Mars")
    vectors.append("MarsForce")
    
    # Jupiter
    CentralBodyForce(root, "Jupiter")
    vectors.append("JupiterForce")
    
    # Venus
    CentralBodyForce(root, "Venus")
    vectors.append("VenusForce")
    
    # drag
    thisVector = "Drag"
    print("Creating vector: " + thisVector + " using 1000 kg and 20 m^2 area")
    vectors.append(thisVector)
    root.ExecuteCommand("VectorTool * Satellite Create VectorTemplate Drag \"Force Model\" Scale 1.0 CentralBody Earth")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate Drag \"Force Model\" Force UseCBGravity Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate Drag \"Force Model\" Drag On 2.2 0.02 \"Jacchia 1970\" Manual 150 150 3.0")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate Drag \"Force Model\" Force SRP Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate Drag \"Force Model\" Force ThirdBodyGravity Sun Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate Drag \"Force Model\" Force ThirdBodyGravity Moon Off")
    
    
    # srp
    thisVector = "SRP"
    print("Creating vector: " + thisVector + " using 1000 kg and 20 m^2 area")
    vectors.append(thisVector)
    root.ExecuteCommand("VectorTool * Satellite Create VectorTemplate SRP \"Force Model\" Scale 1.0 CentralBody Earth")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SRP \"Force Model\" Force UseCBGravity Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SRP \"Force Model\" Drag Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SRP \"Force Model\" Force SRP On")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SRP \"Force Model\" Force ThirdBodyGravity Sun Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate SRP \"Force Model\" Force ThirdBodyGravity Moon Off")
    
    
    ####################
    ##### ANALYSIS #####
    ####################
    for thisSat in sats:
        print("Analyzing */Satellite/" + thisSat)
        
        oSat = root.GetObjectFromPath("*/Satellite/" + thisSat)

        # loop through vectors and vector differences of interest
        m = GetAverageMagnitudeNewton(root, oSat, "PointMass")
        m = GetAverageMagnitudeNewton(root, oSat, "J2")
        m = GetAverageMagnitudeNewton(root, oSat, "J2-2")
        m = GetAverageMagnitudeNewton(root, oSat, "J4")
        m = GetAverageMagnitudeNewton(root, oSat, "J4-4")
        m = GetAverageMagnitudeNewton(root, oSat, "J8-8")
        m = GetAverageMagnitudeNewton(root, oSat, "J12-12")
        m = GetAverageMagnitudeNewton(root, oSat, "J24-24")
        m = GetAverageMagnitudeNewton(root, oSat, "J70-70")
        
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J2")
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J2-2")
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J4")
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J4-4")
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J8-8")
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J12-12")
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J24-24")
        m = GetAverageDifferenceNewton(root, oSat, "PointMass", "J70-70")
        
        m = GetAverageDifferenceNewton(root, oSat, "J2-2", "J2")
        m = GetAverageDifferenceNewton(root, oSat, "J2", "J4")
        m = GetAverageDifferenceNewton(root, oSat, "J4-4", "J2-2")
        m = GetAverageDifferenceNewton(root, oSat, "J8-8", "J4-4")
        m = GetAverageDifferenceNewton(root, oSat, "J12-12", "J8-8")
        m = GetAverageDifferenceNewton(root, oSat, "J24-24", "J12-12")
        m = GetAverageDifferenceNewton(root, oSat, "J70-70", "J24-24")
        
        m = GetAverageMagnitudeNewton(root, oSat, "SunForce")
        m = GetAverageMagnitudeNewton(root, oSat, "MoonForce")
        m = GetAverageMagnitudeNewton(root, oSat, "MarsForce")
        m = GetAverageMagnitudeNewton(root, oSat, "JupiterForce")
        m = GetAverageMagnitudeNewton(root, oSat, "VenusForce")
        
        m = GetAverageMagnitudeNewton(root, oSat, "Drag")
        m = GetAverageMagnitudeNewton(root, oSat, "SRP")
    
    
    
    
    
    ####################
    ##### CLEAN-UP #####
    ####################
    # delete vectors and satellites
    if False:
        for thisVector in vectors:
            root.ExecuteCommand("VectorTool * Satellite Delete VectorTemplate " + thisVector)
            
        for thisSat in sats:
            oThisSat = root.GetObjectFromPath("*/Satellite/" + thisSat)
            oThisSat.Unload()
    


        
        
def GravityVector(root, vectorName, degree, order):
    print("Creating vector: " + vectorName)
    root.ExecuteCommand("VectorTool * Satellite Create VectorTemplate " + vectorName + " \"Force Model\" Scale 1.0 CentralBody Earth")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Force Model\" Force UseCBGravity On")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Force Model\" Force Gravity \"C:\\Program Files\\AGI\\STK 11\\STKData\\CentralBodies\\Earth\\GGM03C.grv\" " + str(degree) + " " + str(order))
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Force Model\" Drag Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Force Model\" Force SRP Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Force Model\" Force ThirdBodyGravity Sun Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Force Model\" Force ThirdBodyGravity Moon Off")


def VectorDifference(root, vectorName, vector1Name, vector2Name):
    print("Creating vector: " + vectorName)
    root.ExecuteCommand("VectorTool * Satellite Create VectorTemplate " + vectorName + " \"Linear Combination\"")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Linear Combination\" VectorA \"Satellite " + vector1Name + "\" ScaleFactorA 1.0 NormalizeA No")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Linear Combination\" VectorB \"Satellite " + vector2Name + "\" ScaleFactorB -1.0 NormalizeB No")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + vectorName + " \"Linear Combination\" InheritDimension FromA")
    
    
def CentralBodyForce(root, centralBodyName):
    print("Creating vector: " + centralBodyName + "Force")
    root.ExecuteCommand("VectorTool * Satellite Create VectorTemplate " + centralBodyName + "Force \"Force Model\" Scale 1.0 CentralBody Earth")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + centralBodyName + "Force \"Force Model\" Force UseCBGravity Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + centralBodyName + "Force \"Force Model\" Drag Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + centralBodyName + "Force \"Force Model\" Force SRP Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + centralBodyName + "Force \"Force Model\" Force ThirdBodyGravity Sun Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + centralBodyName + "Force \"Force Model\" Force ThirdBodyGravity Moon Off")
    root.ExecuteCommand("VectorTool * Satellite Modify VectorTemplate " + centralBodyName + "Force \"Force Model\" Force ThirdBodyGravity " + centralBodyName + " On FromCB")


# compute average magnitude over scenario duration
def GetAverageMagnitudeNewton(root, sat, vectorName):
    sc = root.CurrentScenario
    iagSc = sc.QueryInterface(STKObjects.IAgScenario)
    
    dp = sat.DataProviders.GetDataPrvTimeVarFromPath("Vectors(ICRF)/" + vectorName)
    dpResultLong = dp.Exec(iagSc.StartTime, iagSc.StopTime, 60.0)
            
    mLong = dpResultLong.DataSets.GetDataSetByName("Magnitude").GetValues()
            
            
    mSum = 0
    counter = 0
    for thisM in mLong:
        # convert thisM km/s^2 to m/s^2 and multiply by 1000 for 1000 kg satellite
        mSum += thisM*1000.0*1000.0
        counter += 1

    mAverage = mSum/counter
    print("   " + "{:18}".format(vectorName) + " " + str(mAverage) + " N")
    return mAverage



# copute average difference in vector over scenario duration
def GetAverageDifferenceNewton(root, sat, vectorName1, vectorName2):
    sc = root.CurrentScenario
    iagSc = sc.QueryInterface(STKObjects.IAgScenario)
    
    dp1 = sat.DataProviders.GetDataPrvTimeVarFromPath("Vectors(ICRF)/" + vectorName1)
    dpResult1 = dp1.Exec(iagSc.StartTime, iagSc.StopTime, 60.0)
    x1 = dpResult1.DataSets.GetDataSetByName("x").GetValues()
    y1 = dpResult1.DataSets.GetDataSetByName("y").GetValues()
    z1 = dpResult1.DataSets.GetDataSetByName("z").GetValues()
    
    dp2 = sat.DataProviders.GetDataPrvTimeVarFromPath("Vectors(ICRF)/" + vectorName2)
    dpResult2 = dp2.Exec(iagSc.StartTime, iagSc.StopTime, 60.0)
    x2 = dpResult2.DataSets.GetDataSetByName("x").GetValues()
    y2 = dpResult2.DataSets.GetDataSetByName("y").GetValues()
    z2 = dpResult2.DataSets.GetDataSetByName("z").GetValues()

    # get average over scenario
    mDiffSum = 0
    counter = 0
    for thisX in x1:
        # convert accelerations from km/s^2 to m/s^2 and multiply by 1000 for 1000 kg satellite
        thisXDiff = x1[counter]*1000.0*1000.0 - x2[counter]*1000.0*1000.0
        thisYDiff = y1[counter]*1000.0*1000.0 - y2[counter]*1000.0*1000.0
        thisZDiff = z1[counter]*1000.0*1000.0 - z2[counter]*1000.0*1000.0
        
        thisMDiff = np.sqrt(thisXDiff*thisXDiff + thisYDiff*thisYDiff + thisZDiff*thisZDiff)
        
        mDiffSum += thisMDiff
        counter += 1

    mDiffAverage = mDiffSum/counter
    print("   " + "{:18}".format(vectorName1 + " - " + vectorName2) + " " + str(mDiffAverage) + " N")
    return mDiffSum/counter




ForceComparison()