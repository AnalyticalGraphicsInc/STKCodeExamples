%InitFcn_TestCode.m
%Copyright 2021, Analytical Graphics Inc.
%Author: Daniel Honaker, 2013
%Last Updated: Alexander Lam, 2021
%---------------------------------------------

%create a new instance of STK12
uiapp = actxserver('STK12.application');                                                                                                                       
root = uiapp.Personality2;                                                                                                                                     
uiapp.visible = 1;    

%create a new scenario and specify the time
root.NewScenario('SimulinkRun');                                                                                                                                                                                                                                               
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');                                                                                                
root.CurrentScenario.SetTimePeriod('1 Jul 2013 12:00:00.000', '2 Jul 2013 12:00:00.000');                                                                      
root.CurrentScenario.AnalysisEpoch.SetExplicitTime('1 Jul 2013 12:00:00.000');      

%create a new satellite and propagate
satObj = root.CurrentScenario.Children.New('eSatellite', 'SpySat');                                                                                              
satObj.Propagator.InitialState.Representation.AssignClassical('eCoordinateSystemJ2000', 7045.635754, 0.000859, 98.093, 255.746, 213.045, 251.982);                                                                                                        
satObj.Propagator.Propagate();  

%create a new facility
facObj = root.CurrentScenario.Children.New('eFacility', 'GroundStation');  
facObj.Position.AssignPlanetodetic(30.4794, -86.5234, 0);

%add a sensor to the facility and target it at the satellite
pointingSensor = facObj.Children.New('eSensor', 'Targeted');
pointingSensor.Pattern.ConeAngle = 3.0;
pointingSensor.SetPointingType('eSnPtTargeted');
pointingSensor.Pointing.Targets.AddObject(satObj);

%add a reciever to the ground station
rx = pointingSensor.Children.New('eReceiver', 'DownlinkRx');
root.ExecuteCommand(['Receiver ' rx.Path ' SetValue Model Complex_Receiver_Model']);
root.ExecuteCommand(['Receiver ' rx.Path ' SetValue Model.AntennaControl.Antenna Parabolic']);
root.ExecuteCommand(['Receiver ' rx.Path ' SetValue Model.AntennaControl.Antenna.Diameter 3.0']);

result = root.ExecuteCommand(['Receiver_RM ' rx.Path ' GetValue Model']);
for i = 0:result.Count - 1
    result.Item(i)
end

%add a transmitter to the satellite
tx = satObj.Children.New('eTransmitter', 'DownlinkTx');
root.ExecuteCommand(['Transmitter ' tx.Path ' SetValue Model Complex_Transmitter_Model']);
root.ExecuteCommand(['Transmitter ' tx.Path ' SetValue Model.AntennaControl.Antenna Parabolic']);
root.ExecuteCommand(['Transmitter ' tx.Path ' SetValue Model.AntennaControl.Antenna.Diameter 0.25']);
root.ExecuteCommand(['Transmitter ' tx.Path ' SetValue Model.Power 150']);
root.ExecuteCommand(['Transmitter ' tx.Path ' SetValue Model.Frequency 4040000000.00']);

result = root.ExecuteCommand(['Transmitter_RM ' tx.Path ' GetValue Model']);
for i = 0:result.Count - 1
    result.Item(i)
end

%Constrain the receiver for max BER 1e-8
ber = rx.AccessConstraints.AddConstraint('eCstrBitErrorRate');
ber.EnableMax = 1;
ber.Max = 0.00000001;

% create the Sun
sun = root.CurrentScenario.Children.New('ePlanet', 'Sun');

% add a science sensor on the satellite, define as rectangular 5x5
sensorObj = satObj.Children.New('eSensor', 'scienceSensor');
sensorObj.SetPatternType('eSnRectangular');
sensorObj.Pattern.HorizontalHalfAngle = 5.0;
sensorObj.Pattern.VerticalHalfAngle = 5.0;

% create an area target for greenland
result = root.ExecuteCommand('GetDirectory / STKHome');
installDir = result.Item(0);
root.ExecuteCommand(['GIS * Import "' installDir '\Data\Shapefiles\Countries\Greenland\Greenland.shp" AreaTarget']);

%need to remove all those extra area targets
root.ExecuteCommand('UnloadMulti / */AreaTarget/Greenland_*');
greenland = root.CurrentScenario.Children.Item('Greenland');

%set an elevation constraint on the area target, otherwise the one point
%access will not work correctly for the sensor.
elevation = greenland.AccessConstraints.AddConstraint('eCstrElevationAngle');
elevation.Angle = 90.0;

% create an access object from the science sensor to the area target
atAccess = sensorObj.CreateOnePointAccess(greenland.Path);
% create an access object from the satellite to the sun
sunAccess = satObj.CreateOnePointAccess(sun.Path);
% create an access object from the rx to the tx
commAccess = rx.CreateOnePointAccess(tx.Path);

% reset the animation 
root.Rewind;

% set STK to expect times in epoch seconds
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');                                                                                                

stkParameters = cell(5,1);
stkParameters{1} = uiapp;
stkParameters{2} = root;
stkParameters{3} = sunAccess;
stkParameters{4} = atAccess;
stkParameters{5} = commAccess;

%assign the structure we'll use later to the UserData
set_param(gcb,'UserData', stkParameters);   
                                                                                                                                                   