# EOIRandImageProcessing.py
import numpy as np
import pandas as pd
import cv2
import os
import imageio
import matplotlib.image as mpimg 
import matplotlib.pyplot as plt 
import time
from sklearn.cluster import KMeans
from sklearn.metrics import silhouette_score
from skimage.color import rgb2gray
from scipy.spatial.transform import Rotation as R
from skimage.feature import peak_local_max
from astropy import units as u
from astropy.coordinates import Angle
# Attach to STK
# New way of connectings
# !pip install "C:\Program Files\AGI\STK 12\bin\AgPythonAPI\agi.stk12-12.1.0-py3-none-any.whl"

# Set up your Python workspace
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.stkutil import *
# from agi.stk12.stkobjects.astrogator import *
from agi.stk12.utilities.colors import Color, Colors


## Automation

# do this after updating the pointing direction, otherwise you would need to use the observation vector
def getPointingDirection(sensor,tstart,tstop,tstep,axes=''):
    vecAxesDP = sensor.DataProviders.Item('Vector Choose Axes')
    vecObs = vecAxesDP.Group.Item('PointingDirection')
    if axes == '':
        vecObs.PreData = sensorPath.split('/')[1]+'/'+sensorPath.split('/')[2]+' Body'
    else:
        vecObs.PreData = axes
    elems = ['Time','RightAscension','Declination']
    results = vecObs.ExecElements(tstart,tstop,tstep,elems)
    return results.DataSets.ToArray()[0]

def getSensorFOVAndPixels(sensor):
    dataProvider = sensor.DataProviders.Item('EOIR Sensor Performance')
    elems = ['Horizontal pixels','Vertical pixels','Horizontal geometric FOV','Vertical geometric FOV']
    results = dataProvider.ExecElements(elems)
    data = np.array(results.DataSets.ToArray())[0]
    horizontalPixels,verticalPixels,horizontalAngle,verticalAngle  = data
    return horizontalPixels,verticalPixels,horizontalAngle,verticalAngle 

def getEOIRImages(root,sensorPath,imageName='',textName='',reuseFiles=True):
    if imageName != '':
        if not os.path.exists(imageName.replace('"','')) or reuseFiles == False:
            cmd = 'EOIRDetails {} SaveSceneImage {}'.format(sensorPath,imageName)
            root.ExecuteCommand(cmd)
            
    if textName != '':
        if not os.path.exists(textName.replace('"','')) or reuseFiles == False:
            cmd = 'EOIRDetails {} SaveSceneRawData {}'.format(sensorPath,textName)
            root.ExecuteCommand(cmd)
    return

def computeTrueSensorAzElError(sensor,tstart,tstop,tstep,target='Stage_1'):
    dataProvider = sensor.DataProviders.Item('Vectors(Body)').Group.Item(target)
    elems = ['Time','x','y','z']
    results = dataProvider.ExecElements(tstart,tstop,tstep,elems)
    data = np.array(results.DataSets.ToArray())
    norms = np.linalg.norm(data[:,1:],axis=1)
    data[:,1] = data[:,1]/norms
    data[:,2] = data[:,2]/norms
    data[:,3] = data[:,3]/norms
    # totalAngleOffset = [np.arccos(np.dot([0,0,1],data[ii,1:]))*180/np.pi for ii in range(len(data))]
#     trueMeasurements = np.array([(data[ii,0],np.arcsin(np.cross(data[ii,1:],[0,0,1]))[0:2]*180/np.pi) for ii in range(len(data))])
    
    trueMeasurements = []
    for ii in range(len(data)):
        az,el = np.arcsin(np.cross(data[ii,1:],[0,0,1]))[0:2]*180/np.pi
        trueMeasurements.append((data[ii,0],az,el))
    
    return trueMeasurements


    
def computeSensorBodyToParentRotations(sensor,tstart,tstop,tstep,axes = ''):
    if not axes:
        dataProvider = sensor.DataProviders.Item('Body Axes Orientation').Group.Item('Parent Body Axes')
        elems = ['Time','Euler323 precession','Euler323 nutation','Euler323 spin']
        results = dataProvider.ExecElements(tstart,tstop,tstep,elems)
        data = np.array(results.DataSets.ToArray()[:])
        if len(data) == 0:
            print('Transformation could not be completed. Check the sensor point is defined at this time')

    if axes:
        dataProvider = sensor.DataProviders.Item('Axes Choose Axes').Group.Item('Body')
        elems = ['Time','Euler323 precession','Euler323 nutation','Euler323 spin']
        dataProvider.PreData = axes
        results = dataProvider.ExecElements(tstart,tstop,tstep,elems)
        data = np.array(results.DataSets.ToArray()[:])
        if len(data) == 0:
            print('Transformation could not be completed. Check the sensor point is defined at this time')
    return data


def updatePointingDir(azError,elError,timeRotationZYX):
    # Get target vector based on az and el errors
    boresight = [0,0,1]
    r = R.from_euler('yx',[-elError,-azError],degrees=True)
    targetVec = np.matmul(r.as_matrix(),boresight)

    # Transform vector into satellite frame, convert to Az El in body frame.  
    r = R.from_euler('zyz', [timeRotationZYX[3] ,timeRotationZYX[2],timeRotationZYX[1]], degrees=True)
    v = np.matmul(r.as_matrix(),targetVec)
    az = float(np.degrees(np.arctan2(v[1],v[0])))
    el = float(np.degrees(np.arctan2(v[2],np.sqrt(v[0]**2+v[1]**2))))
    return az, el, v

# do this after updating the pointing direction, otherwise you would need to use the observation vector
def getRADECMeasurements(sensor,tstart,tstop,tstep,useMeasurementDirection=True):
    vecAxesDP = sensor.DataProviders.Item('Vectors(ICRF)')
    if useMeasurementDirection == True:
        vecObs = vecAxesDP.Group.Item('MeasurementDirection')
    else:
        vecObs = vecAxesDP.Group.Item('Boresight')
    elems = ['Time','RightAscension','Declination']
    results = vecObs.ExecElements(tstart,tstop,tstep,elems)
    return results.DataSets.ToArray()[0]




def RADECToMeasurementFileLine(root,t,ra,dec,targetID=1001,sensorID=1000,RAstd=6.0,DECstd=6.0):
    
    # Convert time
    yyddd,fractionOfDay = root.ConversionUtility.ConvertDate('EpSec','YYDDD',str(t)).split('.')
    year = int(yyddd[0:2])
    dayOfYear = int(yyddd[2:])
    secs = np.round(float('.'+fractionOfDay)*86400,3)
    secondsOfDay,fractionsOfSeconds = str(secs).split('.')
    secondsOfDay = int(secondsOfDay)
    fractionsOfSeconds = int(fractionsOfSeconds)
    
    # Convert to hms and dms from degrees
    RAhh,RAmm,RAsec = Angle(str(ra)+'d').wrap_at('360d').hms
    RAhh = int(RAhh)
    RAmm =int(RAmm)
    RAsec = float(RAsec)
    DECsign,DECdd,DECmm,DECsec = Angle(str(dec)+'d').signed_dms
    DECdd = int(DECdd)
    DECmm = int(DECmm)
    DECsec = float(DECsec)
    DECsign = ' ' if DECsign > 0 else '-'
    
    # Settings
    frame = 3
    rotationMethod = 4 
    annualAberrationApplied = 0
    diurnalAberrationApplied = 0

    line = '{:7d}1203     {:0>2d}{:0>3d}{:0>5d}{:0<6d}0{}{} {:>2d}{:0>2d}{:06.3f}{}{:0>2d}{:0>2d}{:05.2f}{}0{}{:5.2f}{:5.2f}   {:5d}\n'.format(targetID,year,dayOfYear,secondsOfDay,fractionsOfSeconds,frame,rotationMethod,RAhh,RAmm,RAsec,DECsign,DECdd,DECmm,DECsec,annualAberrationApplied,diurnalAberrationApplied,RAstd,DECstd,sensorID).replace('.','')
    return line

## Image Processing


# Uses the raw text files not the image anymore.
def normalizeImage(data,k=1,convertToInt=False,plotImage=False):
    # Linear (k=1),Cubic Root(k=1/3), Square transformation(k=2)
    # Linear just converts pixel counts to the range 0 to 255
    # Cubic Root suppresses the bright spots and enhances the dark spots
    # Square suppresses the dark spots and enhances the bright spots
    # Formula found here: https://eeweb.engineering.nyu.edu/~yao/EL5123/lecture3_contrast_enhancement.pdf
    
    t1 = 0
    t2 = 255
    data[data<0] = 0 # Remove negative valuesa replaces with 0
    data3 = data**k
#     data3[np.isnan(data3)] = 0 # Remove nans, negative valuesa replaces with 0
    s1 = min(data3.reshape(-1,1))
    s2 = max(data3.reshape(-1,1))
    a = (t2-t1)/(s2-s1)
    b = t1-s1*a
    image = a*data3+b
    if convertToInt == True:
        image = image.astype(int) # Convert to ints?

    # Plotting
    if plotImage == True:
        plt.figure(figsize=(8,8))
        plt.imshow(image)
        plt.show()
    return image

def processImage(pic):
#     meanpic = np.mean(pic,axis=2) # average across RGB
    meanpic = rgb2gray(pic)
    X,Y = meanpic.shape
#     meanpic = rgb2gray(pic)
#     every4thPixels = []
    # Get every 4th pixel
    for offset in range(0,1):
        every4thPixel = np.zeros((int(X/4),int(Y/4)))
        for ii in np.arange(0+offset,X,4):
            for jj in np.arange(0+offset,Y,4):
                # average 4 pixels at a time
#                 every4thPixel[int((ii-offset)/4),int((jj-offset)/4)] = meanpic[ii,jj]+meanpic[ii+1,jj+1]+meanpic[ii+2,jj+2]+meanpic[ii+3,jj+3]
                every4thPixel[int((ii-offset)/4),int((jj-offset)/4)] = meanpic[ii,jj]

#         every4thPixels.append(every4thPixel)
    return every4thPixel


def getMaxSNR(image):
    std = np.std(image.reshape(-1,1)) # pixel values to variance, signal/variance
    SNRs = image/std
    return np.max(SNRs)

def sortBySNR(image,objectCenters):
    standardDev = np.std(image.reshape(-1,1))
    objectSNRS = np.array([image[objectCenter[0],objectCenter[1]]/standardDev for objectCenter in objectCenters])
    order = np.argsort(objectSNRS)
    objectSNRS = objectSNRS[order]
    objectCenters = objectCenters[order]
    return objectCenters,objectSNRS


def getObjectCenters(image,method='localpeaks',minSNR=10,percentofmax=0.25,maxObjects=4):

    xcount,ycount = image.shape
    

    if getMaxSNR(image) >=minSNR:
        if method.lower() =='localpeaks':
            objectCenters = peak_local_max(image,min_distance=1,threshold_rel=percentofmax,num_peaks=maxObjects)
        else:

            if method.lower() == 'kmeans':
                # Group into 2 colors, object or not an object
                X = image.reshape(-1,1)
                kmeans = KMeans(n_clusters = 2).fit(X)
                groupedImage = kmeans.cluster_centers_[kmeans.labels_]
                groupedImage = groupedImage.reshape(image.shape)
                objectColor = kmeans.cluster_centers_[-1][0]

            elif method.lower() == 'percentofmax':
                groupedImage = image
                groupedImage = groupedImage/np.max(groupedImage)
                percentofmax = 0.25 # percent of max signal
                groupedImage[groupedImage<percentofmax] = 0
                groupedImage[groupedImage>=percentofmax] = 1
                objectColor = 1

            elif method.lower() == 'minSNR':
                groupedImage = image
                std = np.std(groupedImage.reshape(-1,1)) # pixel values to variance, signal/variance
                SNRs = groupedImage/std
                groupedImage[SNRs < minSNR] = 0
                groupedImage[SNRs >= minSNR] = 1
                objectColor = 1
            else:
                # Group into 2 colors, object or not an object
                X = image.reshape(-1,1)
                kmeans = KMeans(n_clusters = 2).fit(X)
                groupedImage = kmeans.cluster_centers_[kmeans.labels_]
                groupedImage = groupedImage.reshape(image.shape)
                objectColor = kmeans.cluster_centers_[-1][0]

            # get the points of the object clusters
            points = np.array([(x,y) for x in range(xcount) for y in range(ycount) if groupedImage[x,y] == objectColor])

            if len(points) > 0:
                # get the center of each object cluster
                # kmeans1 = KMeans(n_clusters=2).fit(points)
                kmeans1 = findNumberOfObjects(points,maxObjects=maxObjects)
                objectCenters = kmeans1.cluster_centers_
            else:
                objectCenters=[]
    else:
        objectCenters=[]


    return objectCenters



def findNumberOfObjects(points,maxObjects=4):
    scoresInertia = []
    scoresSilhouette = []
    kmeansfits = []
    if len(points) < maxObjects:
        maxObjects = len(points)

    for clusterCount in range(1,maxObjects+1):
        kmeans1 = KMeans(n_clusters=clusterCount).fit(points)
        objectCenters = kmeans1.cluster_centers_
        score = kmeans1.inertia_
        scoresInertia.append(score)


        if clusterCount >= 2 and len(points) >= 3:
            score = silhouette_score(points,kmeans1.labels_)
            scoresSilhouette.append(score)

        kmeansfits.append(kmeans1)

    objectCount1 = np.argmin(np.diff(scoresInertia))+2
    if len(scoresSilhouette) > 0:
        objectCount2 = np.argmax(scoresSilhouette)+2

        if objectCount1!=objectCount2:
            print('Inertia Predicted {}'.format(objectCount1))
            print('Silhouette Predicted {}'.format(objectCount2))
            print('Going with Inertia score')
        objectCount = objectCount1
    else:
        objectCount = objectCount1

    if scoresInertia[1] - scoresInertia[0] > 0:
        objectCount = 1
        print('Adding more clusters made the inertia score worse, setting the object count to 1')
    
    return kmeansfits[objectCount-1]



## Helper functions

# # Doesn't actually set the environment variables, turns out it only temporarily changes it
# def useImageinEOIR(imagePath,latHighLatLowLonLowLonHigh):

#     # Process Image
#     data = mpimg.imread(imagePath)
#     image = normalizeImage(data,k=1,convertToInt=False,plotImage=False)/255
#     imageGrayScale = rgb2gray(image)
#     csvPath = imagePath[:-4]+'.csv'
#     np.savetxt(csvPath, imageGrayScale, delimiter=",")
    
#     # set Environment Variables
#     os.environ['AGI_EOIR_CUSTOM_REFLECTANCE_TEXTURE_FILE'] = csvPath
#     os.environ['AGI_EOIR_CUSTOM_TEMPERATURE_TEXTURE_FILE'] = csvPath
#     os.environ['AGI_EOIR_CUSTOM_TEXTUREMAP_FILE'] = csvPath
#     os.environ['AGI_EOIR_CUSTOM_TEMPERATURE_TEXTURE_COORDINATES'] = latHighLatLowLonLowLonHigh
#     os.environ['AGI_EOIR_CUSTOM_REFLECTANCE_TEXTURE_COORDINATES'] = latHighLatLowLonLowLonHigh
#     os.environ['AGI_EOIR_CUSTOM_TEXTUREMAP_COORDINATES'] = latHighLatLowLonLowLonHigh
#     print('You must restart STK to see these effects')
    
#     return 

def writeSensorPointingFile(sensorPointingHistory,fileName='SensorPointing.sp',axes=''):
    with open(fileName, 'w') as f:
        f.write('stk.v.12.0\n')
        if axes:
            f.write('CoordinateAxes AWB {} {}\n'.format(axes.split(' ')[1],axes.split(' ')[0]))
        f.write('Begin Attitude\n')
        f.write('NumberofAttitudePoints {}\n'.format(len(sensorPointingHistory)))
        f.write('Sequence 323\n')
        f.write('AttitudeTimeAzElAngles\n')
        for line in sensorPointingHistory:
            f.write("\t{} {} {}\n".format(line[0],line[1],line[2]))
        f.write('End Attitude\n')
    return


## custom rotation matrices used to verify rotations were working properly
def rot(ang,axis):
    ang = ang/180*np.pi
    if axis.lower() == 'z':
        rot = np.array([[np.cos(ang),-np.sin(ang),0],[np.sin(ang),np.cos(ang),0],[0,0,1]])
    elif axis.lower() == 'y':
        rot = np.array([[np.cos(ang),0,np.sin(ang)],[0,1,0],[-np.sin(ang),0,np.cos(ang)]])
    elif axis.lower() == 'x':
        rot = np.array([[1,0,0],[0,np.cos(ang),-np.sin(ang)],[0,np.sin(ang),np.cos(ang)]])
    else:
        print('please enter x y or z for the axis')
    return rot

def createVideo(sensorName,times,tstep,imageFolder,realTimeRate=1):
    video_name = sensorName+'.mp4'
    fps = (1/tstep)*realTimeRate
    images =  ['{}\\{}Time{}Tagged.png'.format(imageFolder,sensorName,str(float(t)).replace('.','_')) for t in times]
    frame = cv2.imread(images[0])
    height, width, layers = frame.shape

    video = cv2.VideoWriter(video_name, 0, fps, (width,height))

    for image in images:
        video.write(cv2.imread(image))

    cv2.destroyAllWindows()
    video.release()
    return


# Read state from ephemeris file
def getState(ephemerisFile,tPlus1):
    with open(ephemerisFile,'r') as f:
        data = False
    #     k=0
        for line in f:
            if data == True:
                vals = line.split(' ')
                if len(vals) > 1:
                    tVal = np.round(float(vals[1]),3)
                    if tVal == tPlus1:
                        stateLine =line
                        ## Read next line
                        break
            if 'EphemerisTimePosVel' in line:
                data = True
    #         k+=1
    splitVals = stateLine.replace('\n','').split(' ')
    state = np.array([splitVal for splitVal in splitVals if splitVal != '']).astype(float)

    return state

# rotMat = np.matmul(rot(-63,'z'),rot(37.5,'y'),rot(0,'x'))
# v = np.matmul(rotMat,[0,0,1])
# np.matmul(rotMat.T,v) # invert
# np.degrees(np.arctan2(v[1],v[0]))
# np.degrees(np.arctan2(v[2],np.sqrt(v[0]**2+v[1]**2)))


## Doesnt work currently, need to update
# def predictLocation(measurements):
#     if len(measurements) > 2:
#         dEl = measurements[-1][2]-measurements[-2][2]
#         dAz = measurements[-1][1]-measurements[-2][1]
#         azError = measurements[-1][1]+dAz
#         elError = measurements[-1][2]+dEl
#     else:
#         azError = measurements[-1][1]
#         elError = measurements[-1][2]  
#     return azError,elError

# trueAzElError = computeTrueSensorAzElError(sensor,tstart,tstart,tstep,target='Stage_1')
# trueAzElError
# timeRotationZYX = computeSenorBodyToParentBodyRotations(sensor,tstart,tstart,tstep)
# timeRotationZYX 
# t = tstart
# trueAzElError = computeTrueSensorAzElError(sensor,t,t,tstep,target='Stage_1')
# timeRotationZYX = computeSenorBodyToParentBodyRotations(sensor,t,t,tstep)
# # Calculate target direction from az el offsets
# boresight = [0,0,1]
# azError = trueAzElError[0][1]
# elError = trueAzElError[0][2]
# az,el = updatePointingDir(azError,elError,timeRotationZYX)
# az,el
