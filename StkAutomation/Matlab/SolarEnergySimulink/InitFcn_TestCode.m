%create a new instance of STK12                                                                                                                   
uiapp = actxserver('STK12.application');                                                                                                          
root = uiapp.Personality2;                                                                                                                        
uiapp.visible = 1;             
                                                                                                                   
%create a new scenario and specify the time                                                                                                       
root.NewScenario('SimulinkRun');   
scenario = root.CurrentScenario;                                                                                                               
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');                                                                                   
scenario.SetTimePeriod('1 Jul 2013 12:00:00.000', '2 Jul 2013 12:00:00.000');                                                         
scenario.Epoch = '1 Jul 2013 12:00:00.000';        
root.Rewind();
                                                                                   
%create a new satellite and propagate                                                                                                             
satellite = root.CurrentScenario.Children.New('eSatellite', 'ScienceSat');                                                                           
satellite.Propagator.InitialState.Representation.AssignClassical('eCoordinateSystemJ2000', 7045.635754, 0.000859, 98.093, 255.746, 213.045, 251.982);
satellite.Propagator.EphemerisInterval.SetStartAndStopTimes(scenario.StartTime, scenario.StopTime);                                                                                           
satellite.Propagator.Propagate();
                                                                                                                      
%create a new facility                                                                                                                            
facility = root.CurrentScenario.Children.New('eFacility', 'GroundStation');                                                                         
facility.Position.AssignPlanetodetic(30.4794, -86.5234, 0);   
                                                                                      
%add a sensor to the facility and target it at the satellite                                                                                      
pointingSensor = facility.Children.New('eSensor', 'Targeted');                                                                                      
pointingSensor.Pattern.ConeAngle = 3.0;                                                                                                           
pointingSensor.SetPointingType('eSnPtTargeted');                                                                                                  
pointingSensor.Pointing.Targets.AddObject(satellite);
                                                                                            
%add a reciever to the ground station                                                                                                             
receiver = pointingSensor.Children.New('eReceiver', 'DownlinkRx');                                                                                      
receiver.SetModel('Complex Receiver Model');
receiverAntenna = receiver.Model.AntennaControl;
receiverAntenna.SetEmbeddedModel('Parabolic');
receiverAntenna.EmbeddedModel.Diameter = 3;  % m
                                                                                                                                            
%add a transmitter to the satellite                                                                                                               
transmitter = satellite.Children.New('eTransmitter', 'DownlinkTx');
transmitter.SetModel('Complex Transmitter Model');
transmitterAntenna = transmitter.Model.AntennaControl;
transmitterAntenna.SetEmbeddedModel('Parabolic');
transmitterAntenna.EmbeddedModel.Diameter = 0.25;  % m

transmitter.Model.Power = 150;  % dbW
transmitter.Model.Frequency = 4.04;  % GHz
                                                                                                                                       
%Constrain the receiver for max BER 1e-8                                                                                                          
berConstraint = receiver.AccessConstraints.AddConstraint('eCstrBitErrorRate');                                                                                    
berConstraint.EnableMax = 1;                                                                                                                                
berConstraint.Max = 0.00000001;   

% create the Sun                                                                                                                                  
sun = root.CurrentScenario.Children.New('ePlanet', 'Sun');        

% add a science sensor on the satellite, define as rectangular 5x5                                                                                
scienceSensor = satellite.Children.New('eSensor', 'scienceSensor');                                                                                      
scienceSensor.SetPatternType('eSnRectangular');                                                                                                       
scienceSensor.Pattern.HorizontalHalfAngle = 5.0;                                                                                                      
scienceSensor.Pattern.VerticalHalfAngle = 5.0;      

% create an area target for greenland                                                                                                             
result = root.ExecuteCommand('GetDirectory / STKHome');                                                                                           
installDirectory = result.Item(0);                                                                                                                      
root.ExecuteCommand(['GIS * Import "' installDirectory '\Data\Shapefiles\Countries\Greenland\Greenland.shp" AreaTarget']);                              

%need to remove all those extra area targets                                                                                                      
root.ExecuteCommand('UnloadMulti / */AreaTarget/Greenland_*');                                                                                    
greenland = root.CurrentScenario.Children.Item('Greenland'); 

%set an elevation constraint on the area target, otherwise the one point                                                                          
%access will not work correctly for the sensor.                                                                                                   
elevationConstraint = greenland.AccessConstraints.AddConstraint('eCstrElevationAngle');                                                                     
elevationConstraint.Angle = 90.0;         

% create an access object from the science sensor to the area target                                                                              
greenlandAccess = scienceSensor.CreateOnePointAccess(greenland.Path);  

% create an access object from the satellite to the sun                                                                                           
sunAccess = satellite.CreateOnePointAccess(sun.Path); 

% create an access object from the rx to the tx                                                                                                   
communicationsAccess = receiver.CreateOnePointAccess(transmitter.Path);      

% set STK to expect times in epoch seconds                                                                                                        
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');                                                                                  
stkParameters = cell(5,1);                                                                                                                      
stkParameters{1} = uiapp;                                                                                                                         
stkParameters{2} = root;                                                                                                                          
stkParameters{3} = sunAccess;                                                                                                                     
stkParameters{4} = greenlandAccess;                                                                                                                      
stkParameters{5} = communicationsAccess;  

%assign the structure we'll use later to the UserData                                                                                             
set_param(gcb,'UserData', stkParameters);         