###-------------------------------------------------------------------------------------###
#---------------------Height and Width of Rectangular Sensor Footprint--------------------#
###-------------------------------------------------------------------------------------###

# Scenario Variables
satName = "Satellite1"
sensorName = "Sensor1"
reportTimeStep = 1 #sec
# Ensure double slashes
filepathForTxt = "FILE\\PATH\\HERE"

###-------------------------------------------------------------------------------------###

from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.utilities.colors import *
from agi.stk12.vgt import *

stk = STKDesktop.AttachToApplication()
root = stk.Root
scenario = root.CurrentScenario

sat = scenario.Children.Item(satName)
sens = sat.Children.Item(sensorName)

rectPattern = sens.Pattern
horHalfAngle = rectPattern.HorizontalHalfAngle
vertHalfAngle = rectPattern.VerticalHalfAngle

topBotDec = 90 - vertHalfAngle
leftRightDec = 90 - horHalfAngle

# Delete any previously created vectors and points
delete = []
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Vector Top'.format(satName,sensorName))
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Vector Bot'.format(satName,sensorName))
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Vector Left'.format(satName,sensorName))
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Vector Right'.format(satName,sensorName))

delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Point TopPoint'.format(satName,sensorName))
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Point BotPoint'.format(satName,sensorName))
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Point LeftPoint'.format(satName,sensorName))
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Point Right'.format(satName,sensorName))

delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Vector TopToBot'.format(satName,sensorName))
delete.append('VectorTool * Satellite/{}/Sensor/{} Delete Vector LeftToRight'.format(satName,sensorName))

for i in range(len(delete)):
    try:
        root.ExecuteCommand(delete[i])
    except:
        pass

vecFact = sens.Vgt.Vectors.Factory

top = vecFact.Create('Top','',AgECrdnVectorType.eCrdnVectorTypeFixedInAxes)
top.ReferenceAxes.SetPath('Satellite/{}/Sensor/{} Body'.format(satName,sensorName))
top.Direction.AssignRADec(90, topBotDec)

bot = vecFact.Create('Bot','',AgECrdnVectorType.eCrdnVectorTypeFixedInAxes)
bot.ReferenceAxes.SetPath('Satellite/{}/Sensor/{} Body'.format(satName,sensorName))
bot.Direction.AssignRADec(-90, topBotDec)

left = vecFact.Create('Left','',AgECrdnVectorType.eCrdnVectorTypeFixedInAxes)
left.ReferenceAxes.SetPath('Satellite/{}/Sensor/{} Body'.format(satName,sensorName))
left.Direction.AssignRADec(0, leftRightDec)

right = vecFact.Create('Right','',AgECrdnVectorType.eCrdnVectorTypeFixedInAxes)
right.ReferenceAxes.SetPath('Satellite/{}/Sensor/{} Body'.format(satName,sensorName))
right.Direction.AssignRADec(180, leftRightDec)

pointFact = sens.Vgt.Points.Factory

topPoint = pointFact.Create('TopPoint','',AgECrdnPointType.eCrdnPointTypeCentralBodyIntersect)
topPoint.CentralBody = 'Earth'
topPoint.IntersectionSurface = AgECrdnIntersectionSurface.eCrdnIntersectionSurfaceAtCentralBodyEllipsoid
topPoint.DirectionVector = top

botPoint = pointFact.Create('BotPoint','',AgECrdnPointType.eCrdnPointTypeCentralBodyIntersect)
botPoint.CentralBody = 'Earth'
botPoint.IntersectionSurface = AgECrdnIntersectionSurface.eCrdnIntersectionSurfaceAtCentralBodyEllipsoid
botPoint.DirectionVector = bot

leftPoint = pointFact.Create('LeftPoint','',AgECrdnPointType.eCrdnPointTypeCentralBodyIntersect)
leftPoint.CentralBody = 'Earth'
leftPoint.IntersectionSurface = AgECrdnIntersectionSurface.eCrdnIntersectionSurfaceAtCentralBodyEllipsoid
leftPoint.DirectionVector = left

rightPoint = pointFact.Create('RightPoint','',AgECrdnPointType.eCrdnPointTypeCentralBodyIntersect)
rightPoint.CentralBody = 'Earth'
rightPoint.IntersectionSurface = AgECrdnIntersectionSurface.eCrdnIntersectionSurfaceAtCentralBodyEllipsoid
rightPoint.DirectionVector = right

# Show Points
sat3D = sat.VO
satVectors = sat3D.Vector.RefCrdns
satVectors.Add(AgEGeometricElemType.ePointElem, 'Satellite/{}/Sensor/{} TopPoint'.format(satName,sensorName))
satVectors.Add(AgEGeometricElemType.ePointElem, 'Satellite/{}/Sensor/{} BotPoint'.format(satName,sensorName))
satVectors.Add(AgEGeometricElemType.ePointElem, 'Satellite/{}/Sensor/{} LeftPoint'.format(satName,sensorName))
satVectors.Add(AgEGeometricElemType.ePointElem, 'Satellite/{}/Sensor/{} RightPoint'.format(satName,sensorName))

add = []
add.append('VectorTool * Satellite/{}/Sensor/{} Create Vector TopToBot "Displacement on Surface" OriginPoint "Satellite/{}/Sensor/{} TopPoint" DestinationPoint "Satellite/{}/Sensor/{} BotPoint Point" SurfaceCB Earth'.format(satName,sensorName,satName,sensorName,satName,sensorName))
add.append('VectorTool * Satellite/{}/Sensor/{} Create Vector LeftToRight "Displacement on Surface" OriginPoint "Satellite/{}/Sensor/{} LeftPoint" DestinationPoint "Satellite/{}/Sensor/{} RightPoint" SurfaceCB Earth'.format(satName,sensorName,satName,sensorName,satName,sensorName))
for j in range(len(add)):
    root.ExecuteCommand(add[j])

topToBotInit = sens.DataProviders.Item('Vectors(Fixed)').Group.Item('TopToBot')
topToBot = topToBotInit.Exec(scenario.StartTime, scenario.StopTime, reportTimeStep)
times = list(topToBot.DataSets.Item(0).GetValues())
topToBotMagnitude = list(topToBot.DataSets.Item(4).GetValues())

leftToRightInit = sens.DataProviders.Item('Vectors(Fixed)').Group.Item('LeftToRight')
leftToRight = leftToRightInit.Exec(scenario.StartTime, scenario.StopTime, reportTimeStep)
leftToRightMagnitude = list(leftToRight.DataSets.Item(4).GetValues())

# Save to .txt
file = open(filepathForTxt, 'w+')
file.write('Sensor Footprint Width (km)\n\n')
file.write('           Time                   Top to Bottom (Vertical)   Left to Right (Horizontal)\n\n')
for k in range(len(times)):
    time = times[k]
    ttbMag = topToBotMagnitude[k]
    ltrMag = leftToRightMagnitude[k]
    line = str(time) + "       " + str(ttbMag) + ("          ") + str(ltrMag) + "\n"
    file.write(line)
file.close()


