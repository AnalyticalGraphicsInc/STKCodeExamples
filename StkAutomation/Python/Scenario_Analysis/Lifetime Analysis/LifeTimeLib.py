# coding: utf-8

import numpy as np
import pandas as pd
import scipy as sp
from scipy import stats
import os
import time
from datetime import timedelta
import datetime
import pickle
from comtypes.client import CreateObject
from comtypes.client import GetActiveObject
from comtypes.gen import STKObjects
from comtypes.gen import STKUtil
import asyncio
from threading import Thread
import concurrent.futures
import logging
import sys
import seaborn as sns
import pythoncom
from pyDOE2 import (
    fullfact,
    lhs,
)  # Will need to install pyDOE2: pip install pyDOE2    OR    conda install -c conda-forge pydoe2
from poliastro.core.elements import (
    rv2coe,
    coe2rv,
)  # Will need to install poliastro: conda install -c conda-forge poliastro
from poliastro.constants import GM_earth


class configSettings:
    def __init__(
        self,
        fileName="LifeTimeAnalysis.csv",
        howToVary="LatinHyperCube",
        numCores=2,
        runHPOP=False,
        maxDur=100,
        decayAlt=65,
        epoch=20001,
        a=6778,
        e=0,
        i=45,
        AoP=0,
        RAAN=0,
        TA=0,
        Cd=2.2,
        Cr=1.0,
        DragArea=13.65,
        SunArea=15.43,
        Mass=1000,
        AtmDen="Jacchia 1971",
        SolFlxFile="SolFlx_CSSI.dat",
        SigLvl=0,
        OrbPerCal=10,
        GaussQuad=2,
        SecondOrderOblateness="Off",
        numberOfRuns=1,
        varyCols=[],
        varyValues=[],
        setSunAreaEqualToDragArea=True,
    ):

        propertiesDict = {
            "fileName": fileName,
            "numCores": numCores,
            "runHPOP": runHPOP,
            "maxDur": maxDur,
            "decayAlt": decayAlt,
            "epoch": epoch,
            "a": a,
            "e": e,
            "i": i,
            "AoP": AoP,
            "RAAN": RAAN,
            "TA": TA,
            "Cd": Cd,
            "Cr": Cr,
            "DragArea": DragArea,
            "SunArea": SunArea,
            "Mass": Mass,
            "AtmDen": AtmDen,
            "SolFlxFile": SolFlxFile,
            "SigLvl": SigLvl,
            "OrbPerCal": OrbPerCal,
            "GaussQuad": GaussQuad,
            "SecondOrderOblateness": SecondOrderOblateness,
            "howToVary": howToVary,
            "numberOfRuns": numberOfRuns,
            "varyCols": varyCols,
            "varyValues": varyValues,
            "setSunAreaEqualToDragArea": setSunAreaEqualToDragArea,
        }

        for key, val in propertiesDict.items():
            setattr(self, key, val)
        self.propertiesDict = propertiesDict

    def properties(self):
        for attr in dir(self):
            if (
                not attr.startswith("__")
                and attr != "properties"
                and attr != "propertiesDict"
            ):
                self.propertiesDict.update({attr: getattr(self, attr)})
        return self.propertiesDict


####
class TradeStudy:
    def __init__(
        self,
        fileName="LifeTimeAnalysis.csv",
        howToVary="LatinHyperCube",
        numCores=2,
        runHPOP=False,
        maxDur=100,
        decayAlt=65,
        epoch=20001,
        a=6778,
        e=0,
        i=45,
        AoP=0,
        RAAN=0,
        TA=0,
        Cd=2.2,
        Cr=1.0,
        DragArea=13.65,
        SunArea=15.43,
        Mass=1000,
        AtmDen="Jacchia 1971",
        SolFlxFile="SolFlx_CSSI.dat",
        SigLvl=0,
        OrbPerCal=10,
        GaussQuad=2,
        SecondOrderOblateness="Off",
        numberOfRuns=1,
        varyCols=[],
        varyValues=[],
        setSunAreaEqualToDragArea=True,
    ):

        propertiesDict = {
            "fileName": fileName,
            "numCores": numCores,
            "runHPOP": runHPOP,
            "maxDur": maxDur,
            "decayAlt": decayAlt,
            "epoch": epoch,
            "a": a,
            "e": e,
            "i": i,
            "AoP": AoP,
            "RAAN": RAAN,
            "TA": TA,
            "Cd": Cd,
            "Cr": Cr,
            "DragArea": DragArea,
            "SunArea": SunArea,
            "Mass": Mass,
            "AtmDen": AtmDen,
            "SolFlxFile": SolFlxFile,
            "SigLvl": SigLvl,
            "OrbPerCal": OrbPerCal,
            "GaussQuad": GaussQuad,
            "SecondOrderOblateness": SecondOrderOblateness,
            "howToVary": howToVary,
            "numberOfRuns": numberOfRuns,
            "varyCols": varyCols,
            "varyValues": varyValues,
            "setSunAreaEqualToDragArea": setSunAreaEqualToDragArea,
        }

        for key, val in propertiesDict.items():
            setattr(self, key, val)
        self.propertiesDict = propertiesDict

    def properties(self):
        for attr in dir(self):
            if (
                not attr.startswith("__")
                and attr != "properties"
                and attr != "propertiesDict"
            ):
                self.propertiesDict.update({attr: getattr(self, attr)})
        return self.propertiesDict


###########################################################################################################################################


def generateTradeStudy(tradeStudy):
    fileName = os.getcwd() + "\\Results\\" + tradeStudy.fileName
    runHPOP = tradeStudy.runHPOP
    epoch = tradeStudy.epoch
    a = tradeStudy.a
    e = tradeStudy.e
    i = tradeStudy.i
    RAAN = tradeStudy.RAAN
    AoP = tradeStudy.AoP
    TA = tradeStudy.TA
    Cd = tradeStudy.Cd
    Cr = tradeStudy.Cr
    DragArea = tradeStudy.DragArea
    SunArea = tradeStudy.SunArea
    Mass = tradeStudy.Mass
    OrbPerCal = tradeStudy.OrbPerCal
    GaussQuad = tradeStudy.GaussQuad
    SigLvl = tradeStudy.SigLvl
    SolFlxFile = tradeStudy.SolFlxFile
    AtmDen = tradeStudy.AtmDen
    SecondOrderOblateness = tradeStudy.SecondOrderOblateness
    numberOfRuns = tradeStudy.numberOfRuns
    howToVary = tradeStudy.howToVary
    varyCols = tradeStudy.varyCols
    varyValues = tradeStudy.varyValues
    setSunAreaEqualToDragArea = tradeStudy.setSunAreaEqualToDragArea
    np.random.seed(seed=1)

    # Generate Additional Columns
    Rp = a * (1 - e)
    Ra = a * (1 + e)
    p = a * (1 - e**2)
    rs, vs = coe2rv(
        GM_earth * 1e-9,
        p,
        e,
        i * np.pi / 180,
        RAAN * np.pi / 180,
        AoP * np.pi / 180,
        TA * np.pi / 180,
    )
    x = rs[0]
    y = rs[1]
    z = rs[2]
    Vx = vs[0]
    Vy = vs[1]
    Vz = vs[2]
    CdAM = Cd * DragArea / Mass
    CrAM = Cr * SunArea / Mass

    # Generate Dataframe to store all of the runs
    data = [
        epoch,
        a,
        e,
        i,
        RAAN,
        AoP,
        TA,
        Rp,
        Ra,
        p,
        x,
        y,
        z,
        Vx,
        Vy,
        Vz,
        Cd,
        Cr,
        DragArea,
        SunArea,
        Mass,
        CdAM,
        CrAM,
        OrbPerCal,
        GaussQuad,
        SigLvl,
        SolFlxFile,
        AtmDen,
        SecondOrderOblateness,
    ]

    columns = [
        "epoch",
        "a",
        "e",
        "i",
        "RAAN",
        "AoP",
        "TA",
        "Rp",
        "Ra",
        "p",
        "x",
        "y",
        "z",
        "Vx",
        "Vy",
        "Vz",
        "Cd",
        "Cr",
        "Drag Area",
        "Sun Area",
        "Mass",
        "Cd*Drag Area/Mass",
        "Cr*Sun Area/Mass",
        "Orb Per Calc",
        "Gaussian Quad",
        "Flux Sigma Level",
        "SolarFluxFile",
        "Density Model",
        "2nd Order Oblateness",
    ]

    df = pd.DataFrame(data=data, index=columns).T
    df[df.columns[:-3]] = df[df.columns[:-3]].astype(float)

    # Grid Search
    if howToVary.lower() == "gridsearch":
        # Create grid search of parameters and update the Dataframe
        numOfLevels = [len(val) for val in varyValues]
        runs = fullfact(numOfLevels).astype(int)
        paramdf = pd.DataFrame()

        for ii in range(len(runs.T)):
            paramdf[varyCols[ii]] = varyValues[ii][runs[:, ii]]

        df = pd.concat([df] * len(runs), ignore_index=True)

        for col in paramdf.columns:
            df[col] = paramdf[col]

    # Latin Hypercube
    elif howToVary.lower() == "latinhypercube":
        # Generate runs
        lhd = lhs(len(varyCols), samples=numberOfRuns)
        #     lhd = stats.norm(loc=0, scale=1).ppf(lhd) # Convert to a normal distribution
        lhd = pd.DataFrame(lhd)

        adjustEpoch = False
        if "epoch" in varyCols:
            date1 = yydddToDatetime(varyValues[0][0])
            date2 = yydddToDatetime(varyValues[0][1])
            deltaDays = lhd[0] * (date2 - date1).days
            minDate = [varyValues[0][0] for i in range(numberOfRuns)]
            varyCols.remove("epoch")
            varyValues = varyValues[1:]
            lhd = lhd.drop(0, axis=1)
            adjustEpoch = True

        lhd.columns = varyCols

        # Replace string columns with categories
        strii = [
            ii for ii in range(len(varyValues)) if isinstance(varyValues[ii][0], str)
        ]
        for ii in strii:
            lhd.iloc[:, ii] = pd.cut(
                lhd.iloc[:, ii], len(varyValues[ii]), labels=varyValues[ii]
            )
        # Replace float columns with values in range
        varyValues = np.array(varyValues)
        floatii = lhd.dtypes == float
        lhsMinMax = varyValues[floatii]
        lhsMinMax = np.concatenate(lhsMinMax, axis=0).reshape(-1, 2)
        lhd.loc[:, floatii] = (
            lhd.loc[:, floatii] * (lhsMinMax[:, 1] - lhsMinMax[:, 0]) + lhsMinMax[:, 0]
        )

        indxs = [
            ii
            for ii in range(len(varyCols))
            if varyCols[ii] in ["Orb Per Calc", "Gaussian Quad", "Flux Sigma Level"]
        ]
        lhd.iloc[:, indxs] = lhd.iloc[:, indxs].round()

        # Create df
        df = pd.concat([df] * len(lhd), ignore_index=True)

        if adjustEpoch == True:
            df["epoch"] = [
                adjustDate(yyddd, deltaDay)
                for yyddd, deltaDay in zip(minDate, deltaDays)
            ]

        for col in lhd.columns:
            df[col] = lhd[col]

    # Perturb
    elif howToVary.lower() == "perturb":
        # Sample from a normal distribution
        rv = stats.norm()
        rvVals = rv.rvs((numberOfRuns, len(varyCols)))

        # Create perturbation df
        pertdf = pd.DataFrame(rvVals) * varyValues
        pertdf.columns = varyCols

        # Duplicate original df by the number of runs
        df = pd.concat([df] * numberOfRuns, ignore_index=True)

        if "epoch" in varyCols:
            df["epoch"] = [
                adjustDate(yyddd, deltaDay)
                for yyddd, deltaDay in zip(df["epoch"], pertdf["epoch"])
            ]
            varyCols.remove("epoch")
            varyValues = varyValues[1:]

        for col in varyCols:
            df[col] = df[col] + pertdf[col]

    # Update dependant values
    df = updateDf(df, runHPOP, varyCols, setSunAreaEqualToDragArea)

    # Convert all columns to floats and round to the 10th decimal, otherwise there are some numerical rounding issues.
    cols = [
        col
        for col in df.columns
        if col not in ["SolarFluxFile", "Density Model", "2nd Order Oblateness"]
    ]
    df[cols] = df[cols].astype(float)
    for col in cols:
        df[col] = np.round(df[col], 10)

    # Add results
    df["LT Orbits"] = np.nan
    df["LT Years"] = np.nan
    df["LT Runtime"] = np.nan
    if runHPOP == True:
        df["HPOP Years"] = np.nan
        df["HPOP Runtime"] = np.nan

    df.index.name = "Run ID"
    df = df.reset_index()

    df.to_csv(fileName)  # Create a new csv to store the results

    return df


###########################################################################################################################################
def yydddToDatetime(yyddd):

    yyyy = 2000 + int(str(yyddd)[:2])
    dd = int(str(yyddd)[2:5])
    if len(str(yyddd)) > 5:
        fracdd = float(str(yyddd)[5:])
    else:
        fracdd = 0

    date = datetime.datetime(yyyy, 1, 1) + timedelta(dd + fracdd - 1)

    return date


def adjustDate(yyddd, deltaDay):
    years = float(str(yyddd)[:2])
    days = float(str(yyddd)[2:5]) + deltaDay
    years = years + np.floor(days / 365.25)
    yydddNew = years * 1000 + (days % 365.25)
    return yydddNew


def lifetimeVariations(fileName, tradeStudy):
    varyCols = tradeStudy.varyCols
    varyValues = tradeStudy.varyValues

    numOfLevels = [len(val) for val in varyValues]
    runs = fullfact(numOfLevels).astype(int)
    paramdf = pd.DataFrame()

    for ii in range(len(runs.T)):
        paramdf[varyCols[ii]] = varyValues[ii][runs[:, ii]]

    dfAllTemp = pd.read_csv(fileName, index_col=0).reset_index(
        drop=True
    )  # Read previously run Lifetime results
    df = pd.DataFrame()
    for jj in range(len(paramdf)):
        for ii in range(len(varyCols)):
            dfAllTemp[varyCols[ii]] = paramdf.iloc[jj, ii]
        df = df.append(dfAllTemp)
    df = df.drop_duplicates().reset_index(drop=True)

    # Convert all columns to floats and round to the 10th decimal, otherwise there are some numerical rounding issues.
    cols = [
        col
        for col in df.columns
        if col not in ["SolarFluxFile", "Density Model", "2nd Order Oblateness"]
    ]
    df[cols] = df[cols].astype(float)
    for col in cols:
        df[col] = np.round(df[col], 10)

    # Add results
    df["LT Orbits"] = np.nan
    df["LT Years"] = np.nan
    df["LT Runtime"] = np.nan

    df["Run ID Old"] = df["Run ID"]
    df = df.drop("Run ID", axis=1)
    df.index.name = "Run ID"
    df = df.reset_index()

    df.to_csv(
        os.getcwd() + "\\Results\\" + tradeStudy.fileName
    )  # Create a new csv to store the results

    return df


def updateDf(df, runHPOP, varyCols, setSunAreaEqualToDragArea):

    if any(col in ["a", "e", "i", "AoP", "RAAN", "TA", "Rp", "Ra"] for col in varyCols):

        # absolute e
        df["e"] = abs(df["e"])

        # Update dependant values, based on selection of Rp,Ra,a,e
        # If 2 are varied
        if "Rp" in varyCols and "e" in varyCols:
            df["a"] = df["Rp"] / (1 - df["e"])
            df["Ra"] = df["a"] * (1 + df["e"])
            df["p"] = df["a"] * (1 - df["e"] ** 2)

        elif "Ra" in varyCols and "e" in varyCols:
            df["a"] = df["Ra"] / (1 + df["e"])
            df["Rp"] = df["a"] * (1 - df["e"])
            df["p"] = df["a"] * (1 - df["e"] ** 2)

        elif "Rp" in varyCols and "a" in varyCols:
            df["e"] = 1 - df["Rp"] / df["a"]
            df["Ra"] = df["a"] * (1 + df["e"])
            df["p"] = df["a"] * (1 - df["e"] ** 2)

        elif "Ra" in varyCols and "a" in varyCols:
            df["e"] = df["Ra"] / df["a"] - 1
            df["Rp"] = df["a"] * (1 - df["e"])
            df["p"] = df["a"] * (1 - df["e"] ** 2)
        # If a,e pair or Rp,Ra pair or just 1 is varied
        else:
            if any(col in ["a", "e"] for col in varyCols):
                df["Rp"] = df["a"] * (1 - df["e"])
                df["Ra"] = df["a"] * (1 + df["e"])
                df["p"] = df["a"] * (1 - df["e"] ** 2)

            if any(col in ["Rp", "Ra"] for col in varyCols):
                switchRaRp = df["Ra"] < df["Rp"]
                tempRp = df.loc[switchRaRp, "Rp"]
                df.loc[switchRaRp, "Rp"] = df.loc[switchRaRp, "Ra"]
                df.loc[switchRaRp, "Ra"] = tempRp
                df["a"] = (df["Rp"] + df["Ra"]) / 2
                df["e"] = (df["Ra"] - df["Rp"]) / (df["Ra"] + df["Rp"])
                df["p"] = df["a"] * (1 - df["e"] ** 2)

        # Wrap values between 0 and 180 or 0 and 360
        if any(col in ["i", "AoP", "RAAN", "TA"] for col in varyCols):
            df.loc[df["i"] > 180, "i"] = 180 - (
                df.loc[df["i"] > 180, "i"] - 180
            )  # if > 180, subtract the amount over from 180
            df.loc[df["i"] < 180, "i"] = abs(df.loc[df["i"] < 180, "i"])
            df.loc[df["RAAN"] < 360, "RAAN"] = df.loc[df["RAAN"] < 360, "RAAN"] + 360
            df.loc[df["RAAN"] > 360, "RAAN"] = df.loc[df["RAAN"] > 360, "RAAN"] - 360
            df.loc[df["AoP"] < 360, "AoP"] = df.loc[df["AoP"] < 360, "AoP"] + 360
            df.loc[df["AoP"] > 360, "AoP"] = df.loc[df["AoP"] > 360, "AoP"] - 360
            df.loc[df["TA"] < 360, "TA"] = df.loc[df["TA"] < 360, "TA"] + 360
            df.loc[df["TA"] > 360, "TA"] = df.loc[df["TA"] > 360, "TA"] - 360

        # Update Cartesian
        rs = np.zeros((len(df), 3))
        vs = np.zeros((len(df), 3))
        for ii in range(len(df)):
            rs[ii], vs[ii] = coe2rv(
                GM_earth * 1e-9,
                df["p"].iloc[ii],
                df["e"].iloc[ii],
                df["i"].iloc[ii] * np.pi / 180,
                df["RAAN"].iloc[ii] * np.pi / 180,
                df["AoP"].iloc[ii] * np.pi / 180,
                df["TA"].iloc[ii] * np.pi / 180,
            )
        df["x"] = rs[:, 0]
        df["y"] = rs[:, 1]
        df["z"] = rs[:, 2]
        df["Vx"] = vs[:, 0]
        df["Vy"] = vs[:, 1]
        df["Vz"] = vs[:, 2]

    # Update Cartesian and dependant variables
    if any(col in ["x", "y", "z", "Vx", "Vy", "Vz"] for col in varyCols):
        # Convert back to classical
        rs = df[["x", "y", "z"]].to_numpy()
        vs = df[["Vx", "Vy", "Vz"]].to_numpy()
        oes = np.zeros((len(df), 6))
        for ii in range(len(df)):
            (
                oes[ii, 0],
                oes[ii, 1],
                oes[ii, 2],
                oes[ii, 3],
                oes[ii, 4],
                oes[ii, 5],
            ) = rv2coe(GM_earth * 1e-9, rs[ii], vs[ii])
        df["p"] = oes[:, 0]
        df["e"] = oes[:, 1]
        df["i"] = oes[:, 2] * 180 / np.pi
        df["RAAN"] = oes[:, 3] * 180 / np.pi
        df["AoP"] = oes[:, 4] * 180 / np.pi
        df["TA"] = oes[:, 5] * 180 / np.pi
        df["a"] = df["p"] / (1 - df["e"] ** 2)
        df["Rp"] = df["a"] * (1 - df["e"])
        df["Ra"] = df["a"] * (1 + df["e"])
        df.loc[df["TA"] < 0, "TA"] = df.loc[df["TA"] < 0, "TA"] + 360
        df.loc[df["TA"] > 360, "TA"] = df.loc[df["TA"] > 360, "TA"] - 360

    # Update satellite characteristics
    if any(col in ["Cd", "Cr", "Drag Area", "Sun Area", "Mass"] for col in varyCols):
        if setSunAreaEqualToDragArea == True:
            df["Sun Area"] = df["Drag Area"]
        df["Cd*Drag Area/Mass"] = df["Cd"] * df["Drag Area"] / df["Mass"]
        df["Cr*Sun Area/Mass"] = df["Cr"] * df["Sun Area"] / df["Mass"]

    # Replace flux sigma with 0
    df.loc[df["SolarFluxFile"] == "SpaceWeather-v1.2.txt", "Flux Sigma Level"] = 0
    # Get rid of -0 values
    df.loc[df["Flux Sigma Level"] == -0, "Flux Sigma Level"] = 0

    return df


###########################################################################################################################################


def RunSTK(dfSlice, tradeStudy, processNum, showSTK=False, saveEveryNIter=15):
    pythoncom.CoInitialize()
    fileNamekk = (
        str.split(tradeStudy.fileName, ".")[0]
        + str(processNum)
        + "."
        + str.split(tradeStudy.fileName, ".")[-1]
    )
    fileName = os.getcwd() + "\\Results\\" + fileNamekk
    maxDur = tradeStudy.maxDur
    decayAlt = tradeStudy.decayAlt
    runHPOP = tradeStudy.runHPOP
    df = dfSlice.reset_index(drop=True)

    if "epoch" in df.columns:
        epoch1 = df["epoch"].iloc[0]
        epoch2 = epoch1 + 1
        epochs = df["epoch"].to_numpy()
        if (epochs[0] == epochs[1:]).all():
            updateEachEpoch = False
        else:
            updateEachEpoch = True
            updateState = True
    else:
        epoch1 = 19253.166667
        epoch2 = 19254.166667
        updateEachEpoch = False

    # Connect to STK
    start = time.time()
    app = CreateObject("STK11.Application")
    app.Visible = showSTK
    app.UserControl = showSTK
    root = app.Personality2
    root.NewScenario("LifeTimeAnalysis" + str(processNum))

    print("LifeTimeAnalysis" + str(processNum) + " has started.")
    epoch1 = str(epoch1)
    epoch2 = str(epoch2)
    root.ExecuteCommand("Units_Set * All DateFormat YYDDD")
    root.UnitPreferences.SetCurrentUnit("DateFormat", "YYDDD")
    sc = root.CurrentScenario
    sc2 = root.CurrentScenario.QueryInterface(STKObjects.IAgScenario)
    sc2.SetTimePeriod(epoch1, epoch2)
    root.ExecuteCommand("Units_Set * All DateFormat EpYr")
    root.ExecuteCommand("Units_SetConnect / Distance km")
    root.UnitPreferences.SetCurrentUnit("DateFormat", "EpYr")
    epoch1 = root.ConversionUtility.ConvertDate("YYDDD", "EpYr", epoch1)
    satName = "Satellite1"

    # Convert Dateframe to string
    dfstr = df.astype(str)

    # Figureout what needs to be updated at each iteration
    def updateFlags(tradeStudy):
        updateLTtradeStudy = False
        updateSatProp = False
        updateState = False
        varyCols = tradeStudy.varyCols

        if any(
            col
            in [
                "epoch",
                "x",
                "y",
                "z",
                "Vx",
                "Vy",
                "Vz",
                "a",
                "e",
                "i",
                "Aop",
                "RAAN",
                "TA",
                "Rp",
                "Ra",
            ]
            for col in varyCols
        ):
            updateState = True

        if any(
            col in ["Cd", "Cr", "Drag Area", "Sun Area", "Mass"] for col in varyCols
        ):
            updateSatProp = True

        if any(
            col
            in [
                "Orb Per Calc",
                "Gaussian Quad",
                "Flux Sigma Level",
                "SolarFluxFile",
                "Density Model",
                "2nd Order Oblateness",
            ]
            for col in varyCols
        ):
            updateLTtradeStudy = True

        if tradeStudy.howToVary.lower() == "perturb":
            updateLTtradeStudy = False

        if tradeStudy.howToVary.lower() == "lifetimevariations":
            updateLTtradeStudy = True
            updateSatProp = True
            updateState = True

        if tradeStudy.runHPOP == True:
            updateLTtradeStudy = True
            updateSatProp = True
            updateState = True

        return updateLTtradeStudy, updateSatProp, updateState

    # Convert drag model equivalent and HPOP solar flux file
    def atmDenEnu(atmDenStr):
        return {
            "1976 Standard": 0,
            "CIRA 1972": 1,
            "Harris-Priester": 3,
            "Jacchia-Roberts": 4,
            "Jacchia70Lifetime": 6,
            "Jacchia 1970": 6,
            "Jacchia 1971": 7,
            "NRLMSISE 2000": 8,
            "MSIS 1986": 9,
            "MSISE 1990": 10,
            "DTM 2012": STKObjects.eDTM2012,
        }[atmDenStr]

    def SolFluxSigEnu(SolarFluxLevel):
        return {
            -2: "SolFlx_CSSI_Minus2.dat",
            -1: "SolFlx_CSSI_Minus1.dat",
            0: "SolFlx_CSSI.dat",
            1: "SolFlx_CSSI_Plus1.dat",
            2: "SolFlx_CSSI_Plus2.dat",
        }[SolarFluxLevel]

    # tradeStudyure the updates
    updateLTtradeStudy, updateSatProp, updateState = updateFlags(tradeStudy)

    if runHPOP == True:
        if len(df) > 0:
            for i in range(len(df)):
                if np.isnan(df["HPOP Years"][i]):  # Not previously run

                    # Create or get satellite
                    if sc.Children.Contains(STKObjects.eSatellite, satName) == True:
                        sat = root.GetObjectFromPath("Satellite/" + satName)
                        sat.Unload()
                    sat = sc.Children.New(STKObjects.eSatellite, satName)
                    sat2 = sat.QueryInterface(STKObjects.IAgSatellite)
                    prop = sat2.SetPropagatorType(STKObjects.ePropagatorHPOP)
                    prop = sat2.Propagator.QueryInterface(
                        STKObjects.IAgVePropagatorHPOP
                    )

                    # Initialize satellite
                    if updateEachEpoch == True:
                        epYr = root.ConversionUtility.ConvertDate(
                            "YYDDD", "EpYr", dfstr["epoch"].iloc[i]
                        )
                    else:
                        epYr = epoch1

                    prop.EphemerisInterval.SetExplicitInterval(
                        float(epYr), float(epYr) + 1e-5
                    )
                    prop.InitialState.OrbitEpoch.SetExplicitTime(epYr)
                    prop.InitialState.Representation.AssignCartesian(
                        STKUtil.eCoordinateSystemICRF,
                        df["x"].iloc[i],
                        df["y"].iloc[i],
                        df["z"].iloc[i],
                        df["Vx"].iloc[i],
                        df["Vy"].iloc[i],
                        df["Vz"].iloc[i],
                    )
                    prop.Propagate()

                    # tradeStudyure Lifetime and satellite
                    cmd = (
                        "SetLifetime */Satellite/"
                        + satName
                        + " LimitType Duration DurationLimit "
                        + str(maxDur * 365.25)
                        + " DecayAltitude "
                        + str(decayAlt)
                    )
                    root.ExecuteCommand(cmd)

                    if updateLTtradeStudy == True:
                        cmd = (
                            "SetLifetime */Satellite/"
                            + satName
                            + ' DensityModel "'
                            + dfstr["Density Model"].iloc[i]
                            + '"'
                            + " FluxSigmaLevel "
                            + dfstr["Flux Sigma Level"].iloc[i]
                            + " OrbPerCalc "
                            + dfstr["Orb Per Calc"].iloc[i]
                            + " GaussianQuad "
                            + dfstr["Gaussian Quad"].iloc[i]
                            + " 2ndOrder "
                            + dfstr["2nd Order Oblateness"].iloc[i]
                        )
                        root.ExecuteCommand(cmd)
                        cmd = (
                            "SetLifetime */Satellite/"
                            + satName
                            + ' SolarFluxFile "C:\\ProgramData\\AGI\\STK 11 (x64)\\DynamicEarthData\\'
                            + dfstr["SolarFluxFile"].iloc[i]
                            + '"'
                        )
                        root.ExecuteCommand(cmd)

                    if updateSatProp == True:
                        cmd = (
                            "SetLifetime */Satellite/"
                            + satName
                            + " DragCoeff "
                            + dfstr["Cd"].iloc[i]
                            + " ReflectCoeff "
                            + dfstr["Cr"].iloc[i]
                            + " DragArea "
                            + dfstr["Drag Area"].iloc[i]
                            + " SunArea "
                            + dfstr["Sun Area"].iloc[i]
                            + " Mass "
                            + dfstr["Mass"].iloc[i]
                        )
                        root.ExecuteCommand(cmd)

                    # Run LT
                    cmd = "Lifetime */Satellite/" + satName
                    start2 = time.time()
                    res = root.ExecuteCommand(cmd)
                    stop2 = time.time()

                    if res.Item(0).split()[1] == "decay":
                        df.loc[i, "LT Orbits"] = res.Item(0).split()[9]
                        df.loc[i, "LT Years"] = float(res.Item(0).split()[7]) - float(
                            epYr
                        )
                    elif res.Item(0).split()[-1] == "begin.":
                        df.loc[i, "LT Orbits"] = 0
                        df.loc[i, "LT Years"] = 0
                    elif res.Item(0).split()[-1] == "limit.":
                        df.loc[i, "LT Orbits"] = 999999
                        df.loc[i, "LT Years"] = maxDur
                    else:
                        print("i = " + str(i) + ". " + res.Item(0))
                    df.loc[i, "LT Runtime"] = stop2 - start2

                    # tradeStudy HPOP

                    prop.EphemerisInterval.SetExplicitInterval(
                        float(epYr), float(epYr) + maxDur
                    )
                    prop.Step = 60
                    prop.Integrator.ReportEphemOnFixedTimeStep = False
                    if dfstr["Density Model"].iloc[i] in [
                        "NRLMSISE 2000",
                        "MSISE 1990",
                    ]:
                        prop.Integrator.UseVOP = False
                    else:
                        prop.Integrator.UseVOP = True
                    drag = prop.ForceModel.Drag.DragModel.QueryInterface(
                        STKObjects.IAgVeHPOPDragModelSpherical
                    )
                    srp = prop.ForceModel.SolarRadiationPressure.SRPModel.Model.QueryInterface(
                        STKObjects.IAgSRPModelSpherical
                    )
                    prop.ForceModel.Drag.SetSolarFluxGeoMagType(
                        STKObjects.eSolarFluxGeoMagUseFile
                    )
                    dragFile = prop.ForceModel.Drag.SolarFluxGeoMag.QueryInterface(
                        STKObjects.IAgVeSolarFluxGeoMagUseFile
                    )
                    prop.ForceModel.MoreOptions.Static.SatelliteMass = df["Mass"].iloc[
                        i
                    ]
                    drag.Cd = df["Cd"].iloc[i]
                    drag.AreaMassRatio = df["Drag Area"].iloc[i] / df["Mass"].iloc[i]
                    srp.Cr = df["Cr"].iloc[i]
                    srp.AreaMassRatio = df["Sun Area"].iloc[i] / df["Mass"].iloc[i]
                    prop.ForceModel.Drag.AtmosphericDensityModel = atmDenEnu(
                        dfstr["Density Model"].iloc[i]
                    )
                    if dfstr["SolarFluxFile"].iloc[i] == "SolFlx_CSSI.dat":
                        dragFile.File = (
                            "C:\\ProgramData\\AGI\\STK 11 (x64)\\DynamicEarthData\\"
                            + SolFluxSigEnu(df["Flux Sigma Level"].iloc[i])
                        )
                    else:
                        dragFile.File = (
                            "C:\\ProgramData\\AGI\\STK 11 (x64)\\DynamicEarthData\\"
                            + dfstr["SolarFluxFile"].iloc[i]
                        )

                    # Run HPOP
                    startHPOP = time.time()
                    try:
                        prop.Propagate()
                    except:
                        pass
                    stopHPOP = time.time()

                    # Record Results
                    df.loc[i, "HPOP Runtime"] = stopHPOP - startHPOP
                    startEph = float(
                        sat.Vgt.Events.Item("EphemerisStartTime").FindOccurrence().Epoch
                    )
                    stopEph = float(
                        sat.Vgt.Events.Item("EphemerisStopTime").FindOccurrence().Epoch
                    )
                    HPOPLT = stopEph - startEph
                    df.loc[i, "HPOP Years"] = HPOPLT

                    stop = time.time()

                    print(
                        "LifeTimeAnalysis"
                        + str(processNum)
                        + " "
                        + str(i + 1)
                        + " of "
                        + str(len(df))
                        + ". Total Time: "
                        + str(stop - start)
                    )
                    print(
                        "LifeTimeAnalysis"
                        + str(processNum)
                        + " HPOP Lifetime: "
                        + df.loc[i, "HPOP Years"].astype(str)
                        + ". Runtime "
                        + str(stopHPOP - startHPOP)
                    )
                    print(res.Item(0))

                    # Save as you go
                    df.to_csv(fileName)  # Save the file for future use
                    print("\t")
                    print(
                        "LifeTimeAnalysis" + str(processNum) + " wrote to " + fileName
                    )
                    print("\t")

                    # Unload satellite because HPOP properties sometimes don't get reset properly
                    sat.Unload()
    else:

        # Create or get satellite
        if sc.Children.Contains(STKObjects.eSatellite, satName) == False:
            sat = sc.Children.New(STKObjects.eSatellite, satName)
            sat2 = sat.QueryInterface(STKObjects.IAgSatellite)
            prop = sat2.SetPropagatorType(STKObjects.ePropagatorJ4Perturbation)
            prop = sat2.Propagator.QueryInterface(
                STKObjects.IAgVePropagatorJ4Perturbation
            )
        else:
            sat = root.GetObjectFromPath("Satellite/" + satName)
            sat2 = sat.QueryInterface(STKObjects.IAgSatellite)
            prop = sat2.Propagator.QueryInterface(
                STKObjects.IAgVePropagatorJ4Perturbation
            )

        # tradeStudyure Lifetime and satellite
        cmd = (
            "SetLifetime */Satellite/"
            + satName
            + " LimitType Duration DurationLimit "
            + str(maxDur * 365.25)
            + " DecayAltitude "
            + str(decayAlt)
        )
        root.ExecuteCommand(cmd)

        ii = 0
        prop.InitialState.Representation.AssignCartesian(
            STKUtil.eCoordinateSystemICRF,
            df["x"].iloc[ii],
            df["y"].iloc[ii],
            df["z"].iloc[ii],
            df["Vx"].iloc[ii],
            df["Vy"].iloc[ii],
            df["Vz"].iloc[ii],
        )
        prop.Step = 1e-3
        epYr = epoch1
        prop.InitialState.OrbitEpoch.SetExplicitTime(epYr)
        prop.StartTime = float(epYr)
        prop.StopTime = float(epYr) + 1e-10
        prop.Propagate()

        cmd = (
            "SetLifetime */Satellite/"
            + satName
            + ' DensityModel "'
            + dfstr["Density Model"].iloc[ii]
            + '"'
            + " FluxSigmaLevel "
            + dfstr["Flux Sigma Level"].iloc[ii]
            + " OrbPerCalc "
            + dfstr["Orb Per Calc"].iloc[ii]
            + " GaussianQuad "
            + dfstr["Gaussian Quad"].iloc[ii]
            + " 2ndOrder "
            + dfstr["2nd Order Oblateness"].iloc[ii]
        )
        root.ExecuteCommand(cmd)
        cmd = (
            "SetLifetime */Satellite/"
            + satName
            + ' SolarFluxFile "C:\\ProgramData\\AGI\\STK 11 (x64)\\DynamicEarthData\\'
            + dfstr["SolarFluxFile"].iloc[ii]
            + '"'
        )
        root.ExecuteCommand(cmd)

        cmd = (
            "SetLifetime */Satellite/"
            + satName
            + " DragCoeff "
            + dfstr["Cd"].iloc[ii]
            + " ReflectCoeff "
            + dfstr["Cr"].iloc[ii]
            + " DragArea "
            + dfstr["Drag Area"].iloc[ii]
            + " SunArea "
            + dfstr["Sun Area"].iloc[ii]
            + " Mass "
            + dfstr["Mass"].iloc[ii]
        )
        root.ExecuteCommand(cmd)

        # Run Lifetime
        k = 0

        if len(df) > 0:
            for i in range(len(df)):
                if np.isnan(df["LT Years"][i]):  # Not previously run
                    if updateLTtradeStudy == True:
                        cmd = (
                            "SetLifetime */Satellite/"
                            + satName
                            + ' DensityModel "'
                            + dfstr["Density Model"].iloc[i]
                            + '"'
                            + " FluxSigmaLevel "
                            + dfstr["Flux Sigma Level"].iloc[i]
                            + " OrbPerCalc "
                            + dfstr["Orb Per Calc"].iloc[i]
                            + " GaussianQuad "
                            + dfstr["Gaussian Quad"].iloc[i]
                            + " 2ndOrder "
                            + dfstr["2nd Order Oblateness"].iloc[i]
                        )
                        root.ExecuteCommand(cmd)
                        cmd = (
                            "SetLifetime */Satellite/"
                            + satName
                            + ' SolarFluxFile "C:\\ProgramData\\AGI\\STK 11 (x64)\\DynamicEarthData\\'
                            + dfstr["SolarFluxFile"].iloc[i]
                            + '"'
                        )
                        root.ExecuteCommand(cmd)

                    if updateSatProp == True:
                        cmd = (
                            "SetLifetime */Satellite/"
                            + satName
                            + " DragCoeff "
                            + dfstr["Cd"].iloc[i]
                            + " ReflectCoeff "
                            + dfstr["Cr"].iloc[i]
                            + " DragArea "
                            + dfstr["Drag Area"].iloc[i]
                            + " SunArea "
                            + dfstr["Sun Area"].iloc[i]
                            + " Mass "
                            + dfstr["Mass"].iloc[i]
                        )
                        root.ExecuteCommand(cmd)

                    if updateEachEpoch == True:
                        epYr = root.ConversionUtility.ConvertDate(
                            "YYDDD", "EpYr", dfstr["epoch"].iloc[i]
                        )
                        prop.InitialState.OrbitEpoch.SetExplicitTime(epYr)

                    if updateState == True:
                        prop.InitialState.Representation.AssignCartesian(
                            STKUtil.eCoordinateSystemICRF,
                            df["x"].iloc[i],
                            df["y"].iloc[i],
                            df["z"].iloc[i],
                            df["Vx"].iloc[i],
                            df["Vy"].iloc[i],
                            df["Vz"].iloc[i],
                        )
                        prop.Propagate()

                    cmd = "Lifetime */Satellite/" + satName
                    start2 = time.time()
                    res = root.ExecuteCommand(cmd)
                    stop2 = time.time()

                    if res.Item(0).split()[1] == "decay":
                        df.loc[i, "LT Orbits"] = res.Item(0).split()[9]
                        df.loc[i, "LT Years"] = float(res.Item(0).split()[7]) - float(
                            epYr
                        )
                    elif res.Item(0).split()[-1] == "begin.":
                        df.loc[i, "LT Orbits"] = 0
                        df.loc[i, "LT Years"] = 0
                    elif res.Item(0).split()[-1] == "limit.":
                        df.loc[i, "LT Orbits"] = 999999
                        df.loc[i, "LT Years"] = maxDur
                    else:
                        print("i = " + str(i) + ". " + res.Item(0))
                    df.loc[i, "LT Runtime"] = stop2 - start2
                    stop = time.time()

                    # Print results
                    if (i + 1) % 5 == 0:
                        print(
                            "LifeTimeAnalysis"
                            + str(processNum)
                            + " "
                            + str(i + 1)
                            + " of "
                            + str(len(df))
                            + ". Total Time: "
                            + str(stop - start)
                        )
                    #             print('\t'+res.Item(0))

                    # Save as you go
                    if (i + 1) % saveEveryNIter == 0:
                        df.to_csv(fileName)  # Save the file for future use
                        print("\t")
                        print(
                            "LifeTimeAnalysis"
                            + str(processNum)
                            + " wrote to "
                            + fileName
                        )
                        print("\t")

    # Save Results
    df.to_csv(fileName)  # Save the file for future use

    app.Quit()
    del app
    print("\t")
    print("LifeTimeAnalysis" + str(processNum) + " done. Wrote to " + fileName)
    print("\t")
    return str(processNum) + " Done"


###########################################################################################################################################


def main(df, tradeStudy, showSTK=False, saveEveryNIter=15):
    numCores = tradeStudy.numCores
    logging.basicConfig(
        level=logging.INFO,
        format="%(threadName)10s %(name)18s: %(message)s",
        stream=sys.stderr,
    )
    executor = concurrent.futures.ThreadPoolExecutor(max_workers=numCores)

    event_loop = asyncio.get_event_loop()
    try:
        event_loop.run_until_complete(
            runTasks(executor, df, tradeStudy, showSTK, saveEveryNIter)
        )
    finally:
        event_loop.close()


async def runTasks(executor, df, tradeStudy, showSTK=False, saveEveryNIter=15):
    pythoncom.CoInitialize()
    log = logging.getLogger("run_blocking_tasks")
    log.info("starting")
    log.info("creating executor tasks")

    blocking_tasks = []
    df = df.reset_index(drop=True)
    indPerDF = np.array_split(df.index, tradeStudy.numCores)
    loop = asyncio.get_event_loop()
    for kk in range(tradeStudy.numCores):
        dfSlice = df.iloc[indPerDF[kk]]
        blocking_tasks.append(
            loop.run_in_executor(
                executor, RunSTK, dfSlice, tradeStudy, kk, showSTK, saveEveryNIter
            )
        )
    log.info("waiting for executor tasks")
    completed, pending = await asyncio.wait(blocking_tasks)
    results = [t.result() for t in completed]
    log.info("results: {!r}".format(results))
    log.info("exiting")


###########################################################################################################################################


def loadSats(df, maxSats=100, maxDur=100):
    # Load satellites
    try:
        app = GetActiveObject("STK11.Application")
        root = app.Personality2
    except:
        app = CreateObject("STK11.Application")
        app.Visible = True
        app.UserControl = True
        root = app.Personality2
        root.NewScenario("LifeTimeRuns")
    root.UnitPreferences.SetCurrentUnit("DateFormat", "EpDay")
    sc = root.CurrentScenario

    # Color by Lifetime
    colors = ((df["LT Years"] - 0) * (1 / (maxDur - 0) * 255)).astype("uint8")
    colorsInt = []
    for ii in range(len(colors)):
        colorsInt.append(
            int(
                "00"
                + "{0:#0{1}x}".format(colors.iloc[ii], 4)[2:]
                + "{0:#0{1}x}".format(255 - colors.iloc[ii], 4)[2:],
                16,
            )
        )

    # Loop through the df
    if len(df) <= maxSats:
        for ii in range(len(df)):
            satName = str(int(df["Run ID"].iloc[ii]))
            if sc.Children.Contains(STKObjects.eSatellite, satName) == False:
                sat = sc.Children.New(STKObjects.eSatellite, satName)
                sat2 = sat.QueryInterface(STKObjects.IAgSatellite)

            else:
                sat = root.GetObjectFromPath("Satellite/" + satName)
                sat2 = sat.QueryInterface(STKObjects.IAgSatellite)

            graphics = sat2.Graphics.Attributes.QueryInterface(
                STKObjects.IAgVeGfxAttributesBasic
            )
            graphics.Color = colorsInt[ii]

            prop = sat2.SetPropagatorType(STKObjects.ePropagatorJ4Perturbation)
            prop = sat2.Propagator.QueryInterface(
                STKObjects.IAgVePropagatorJ4Perturbation
            )
            prop.UseScenarioAnalysisTime = False
            prop.InitialState.Epoch = root.ConversionUtility.ConvertDate(
                "YYDDD", "EpDay", str(df["epoch"].iloc[ii])
            )
            prop.InitialState.Representation.AssignCartesian(
                STKUtil.eCoordinateSystemICRF,
                df["x"].iloc[ii],
                df["y"].iloc[ii],
                df["z"].iloc[ii],
                df["Vx"].iloc[ii],
                df["Vy"].iloc[ii],
                df["Vz"].iloc[ii],
            )
            prop.Step = 600
            prop.StopTime = 1
            prop.Propagate()
    else:
        print(
            "Number of satellites is too large, "
            + str(len(df))
            + ". Please reduce the number of satellites to beneath "
            + str(maxSats)
        )


###########################################################################################################################################
def readResults(tradeStudy):
    fileName = os.getcwd() + "\\Results\\" + tradeStudy.fileName
    # Load dataframe from previous runs
    # Check for the existence of csv files from different cores
    fileCount = 0
    for kk in range(tradeStudy.numCores):
        fileNamekk = (
            str.split(fileName, ".")[0] + str(kk) + "." + str.split(fileName, ".")[-1]
        )
        if os.path.isfile(fileNamekk):
            fileCount += 1
    # Merge into one csv if all of the cores are there
    if fileCount == tradeStudy.numCores:
        df = mergeResults(tradeStudy)
        for kk in range(tradeStudy.numCores):
            fileNamekk = (
                str.split(fileName, ".")[0]
                + str(kk)
                + "."
                + str.split(fileName, ".")[-1]
            )
            os.remove(fileNamekk)
    # Otherwise just read from the final csv
    else:
        df = csvToDf(tradeStudy.fileName)
    return df


def mergeResults(tradeStudy):
    fileName = os.getcwd() + "\\Results\\" + tradeStudy.fileName
    for kk in range(tradeStudy.numCores):
        fileNamekk = (
            str.split(tradeStudy.fileName, ".")[0]
            + str(kk)
            + "."
            + str.split(tradeStudy.fileName, ".")[-1]
        )
        fileNamekk = os.getcwd() + "\\Results\\" + fileNamekk
        dfTemp = pd.read_csv(
            fileNamekk, index_col=0
        )  # Read previously run Lifetime results
        if kk == 0:
            dfTemp.to_csv(fileName)
        else:
            dfTemp.to_csv(fileName, mode="a", header=False)
    df = pd.read_csv(fileName, index_col=0).sort_values("Run ID").reset_index(drop=True)
    return df


def csvToDf(fileName):
    fileName = os.getcwd() + "\\Results\\" + fileName
    dfRes = (
        pd.read_csv(fileName, index_col=0).sort_values("Run ID").reset_index(drop=True)
    )
    cols = [
        col
        for col in dfRes.columns
        if col not in ["SolarFluxFile", "Density Model", "2nd Order Oblateness"]
    ]
    dfRes[cols] = dfRes[cols].astype(float)
    return dfRes


def loadTradeStudy(fileName):
    fileName = os.getcwd() + "\\TradeStudyFiles\\tradeStudy" + fileName + ".pkl"
    tradeStudy = pickle.load(open(fileName, "rb"))
    return tradeStudy


def saveTradeStudy(tradeStudy):
    tradeStudyFileName = (
        os.getcwd()
        + "\\TradeStudyFiles\\"
        + "tradeStudy"
        + tradeStudy.fileName.split(".")[0]
        + ".pkl"
    )
    pickle.dump(tradeStudy, open(tradeStudyFileName, "wb"))
    return


def solFluxVals():
    fp = "C:\ProgramData\AGI\STK 11 (x64)\DynamicEarthData\SolFlx_CSSI.dat"
    df = pd.read_csv(fp, "\t")
    df = df[df.columns[0]].str.split(expand=True).astype(float)
    df = df.iloc[32:]
    df[1] = range(282)
    df[1] = df[1].astype(float) / 12
    df = df[[1, 3]]
    df.columns = ["EpYears", "F10_7"]
    df = df.reset_index(drop=True)
    return df
