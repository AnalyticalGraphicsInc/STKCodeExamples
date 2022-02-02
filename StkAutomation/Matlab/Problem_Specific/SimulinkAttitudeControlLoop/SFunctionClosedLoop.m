function SFunctionClosedLoop(block)

  setup(block);

function setup(block)
  
  %% Register number of input and output ports
  block.NumInputPorts  = 1;
  block.NumOutputPorts = 6;
 
  % Override input port properties
  block.InputPort(1).Dimensions  = 3;
  block.InputPort(1).DatatypeID  = 0;  % double
  block.InputPort(1).Complexity  = 'Real';
  block.InputPort(1).DirectFeedthrough = true;
  
  % Override output port properties
  block.OutputPort(1).Dimensions  = 4;
  block.OutputPort(1).DatatypeID  = 0;  % double
  block.OutputPort(1).Complexity  = 'Real';
  block.OutputPort(2).Dimensions  = 3;
  block.OutputPort(2).DatatypeID  = 0;  % double
  block.OutputPort(2).Complexity  = 'Real';
  block.OutputPort(3).Dimensions  = 3;
  block.OutputPort(3).DatatypeID  = 0;  % double
  block.OutputPort(3).Complexity  = 'Real';
  block.OutputPort(4).Dimensions  = 4;
  block.OutputPort(4).DatatypeID  = 0;  % double
  block.OutputPort(4).Complexity  = 'Real';
  block.OutputPort(5).Dimensions  = 3;
  block.OutputPort(5).DatatypeID  = 0;  % double
  block.OutputPort(5).Complexity  = 'Real';
  block.OutputPort(6).Dimensions  = 3;
  block.OutputPort(6).DatatypeID  = 0;  % double
  block.OutputPort(6).Complexity  = 'Real';
  
  %% Set block sample time
  block.SampleTimes = [0.1 0];
  
  %% Register methods
  block.RegBlockMethod('Start', @Start);
  block.RegBlockMethod('Outputs', @Output); 
  block.RegBlockMethod('SetInputPortSamplingMode', @SetInpPortFrameData);
  block.RegBlockMethod('PostPropagationSetup',    @DoPostPropSetup);
  
function DoPostPropSetup(block)

block.NumDworks = 1;

block.Dwork(1).Name            = 'Euler321';
block.Dwork(1).Dimensions      = 3;
block.Dwork(1).DatatypeID      = 0;      % double
block.Dwork(1).Complexity      = 'Real'; % real
block.Dwork(1).UsedAsDiscState = true;

% block.Dwork(2).Name            = 'Quaternion';
% block.Dwork(2).Dimensions      = 4;
% block.Dwork(2).DatatypeID      = 0;      % double
% block.Dwork(2).Complexity      = 'Real'; % real
% block.Dwork(2).UsedAsDiscState = true;


function SetInpPortFrameData(block, idx, fd)
  
  block.InputPort(idx).SamplingMode = fd;
  block.OutputPort(1).SamplingMode  = fd;
  block.OutputPort(2).SamplingMode  = fd;
  block.OutputPort(3).SamplingMode  = fd;
  block.OutputPort(4).SamplingMode  = fd;
  block.OutputPort(5).SamplingMode  = fd;
  block.OutputPort(6).SamplingMode  = fd;
  
function Start(block)

block.Dwork(1).Data = [0, 0, 0];
%block.Dwork(2).Data = [0, 0, 0, 1];

function Output(block)

%retreive the stkParameters array from the UserData
stkParameters = get_param(block.BlockHandle, 'UserData');

%get the root and the access objects from the array
root = stkParameters{2};
sat = stkParameters{3};
ref = stkParameters{4};

%update the feedback
block.Dwork(1).Data = block.InputPort(1).Data;
yaw = block.Dwork(1).Data(1);
pitch = block.Dwork(1).Data(2);
roll = block.Dwork(1).Data(3);
% q1 = block.Dwork(2).Data(1);
% q2 = block.Dwork(2).Data(2);
% q3 = block.Dwork(2).Data(3);
% q4 = block.Dwork(2).Data(4);

%change the animation to the next step
root.CurrentTime = block.CurrentTime;

%set the satellite attitude
sat.Attitude.AddEuler(root.CurrentTime, '321', yaw, pitch, roll);
%sat.Attitude.AddQuaternion(root.CurrentTime, q1, q2, q3, q4);

%get the Euler attitude of the body frames respect to the ICRF frame
satBodyAxes = sat.Vgt.Axes.Item('Body');
satAttitudeEuler = satBodyAxes.FindInAxes(root.CurrentTime, sat.Vgt.Axes.Item('ICRF')).Orientation.QueryEulerAnglesArray('e321');
refBodyAxes = ref.Vgt.Axes.Item('Body');
refAttitudeEuler = refBodyAxes.FindInAxes(root.CurrentTime, ref.Vgt.Axes.Item('ICRF')).Orientation.QueryEulerAnglesArray('e321');

%%get the quaternion attitude of the body frames respect to the ICRF frame
%satAttitudeQuaternions = bodyAxes.FindInAxes(root.CurrentTime, sat.Vgt.Axes.Item('ICRF')).Orientation.QueryQuaternionArray();

%%%% get the reference attitude state %%%%%%%%
% define a data provider and the elements to get from it
referenceState = ref.DataProviders.Item('Attitude Quaternions');
Elems = {'q1';'q2';'q3';'q4';'wx';'wy';'wz'};
% execute the query and get back the results
Results1 = referenceState.ExecElements(root.CurrentTime, root.CurrentTime, 10, Elems);
q1 = cell2mat(Results1.DataSets.GetDataSetByName('q1').GetValues);
q2 = cell2mat(Results1.DataSets.GetDataSetByName('q2').GetValues);
q3 = cell2mat(Results1.DataSets.GetDataSetByName('q3').GetValues);
q4 = cell2mat(Results1.DataSets.GetDataSetByName('q4').GetValues);
wx = cell2mat(Results1.DataSets.GetDataSetByName('wx').GetValues);
wy = cell2mat(Results1.DataSets.GetDataSetByName('wy').GetValues);
wz = cell2mat(Results1.DataSets.GetDataSetByName('wz').GetValues);
%refAttitudeState = [q1(1,1), q2(1,1), q3(1,1), q4(1,1), wx(1,1), wy(1,1), wz(1,1)];
refQuaternion = [q1(1,1), q2(1,1), q3(1,1), q4(1,1)];
refAngularSpeed = [wx(1,1), wy(1,1), wz(1,1)];


%%%% get the current attitude state %%%%%%%%
% define a data provider and the elements to get from it
currentState = sat.DataProviders.Item('Attitude Quaternions');
Elems = {'q1';'q2';'q3';'q4';'wx';'wy';'wz'};
% execute the query and get back the results
Results2 = currentState.ExecElements(root.CurrentTime, root.CurrentTime, 10, Elems);
q1 = cell2mat(Results2.DataSets.GetDataSetByName('q1').GetValues);
q2 = cell2mat(Results2.DataSets.GetDataSetByName('q2').GetValues);
q3 = cell2mat(Results2.DataSets.GetDataSetByName('q3').GetValues);
q4 = cell2mat(Results2.DataSets.GetDataSetByName('q4').GetValues);
wx = cell2mat(Results2.DataSets.GetDataSetByName('wx').GetValues);
wy = cell2mat(Results2.DataSets.GetDataSetByName('wy').GetValues);
wz = cell2mat(Results2.DataSets.GetDataSetByName('wz').GetValues);
%satAttitudeState = [q1(1,1), q2(1,1), q3(1,1), q4(1,1), wx(1,1), wy(1,1), wz(1,1)];
satQuaternion = [q1(1,1), q2(1,1), q3(1,1), q4(1,1)];
satAngularSpeed = [wx(1,1), wy(1,1), wz(1,1)];

%%%%%%%% modify and use this code if you need additional vectors from STK (attitude determination from vectors) %%%%%%%% 
% %get the x, y, z Sun vector components respect to the body frame
% sunElems = {'Time';'x/Magnitude';'y/Magnitude';'z/Magnitude'};
% satSun = sat.DataProviders.Item('Vectors(Body)').Group.Item('Sun').ExecElements(root.CurrentTime, root.CurrentTime, 1, sunElems);
% xSun = cell2mat(satSun.DataSets.Item(1).GetValues);
% ySun = cell2mat(satSun.DataSets.Item(2).GetValues);
% zSun = cell2mat(satSun.DataSets.Item(3).GetValues);
% sunMag = [xSun(1,1), ySun(1,1), zSun(1,1)];
% %get the x, y, z Magnetic vector components
% magElems = {'Time';'x/Magnitude';'y/Magnitude';'z/Magnitude'};
% satMag = sat.DataProviders.Item('Vectors(Body)').Group.Item('MagField(IGRF)').ExecElements(root.CurrentTime, root.CurrentTime, 1, magElems);
% xMag = cell2mat(satMag.DataSets.Item(1).GetValues);
% yMag = cell2mat(satMag.DataSets.Item(2).GetValues);
% zMag = cell2mat(satMag.DataSets.Item(3).GetValues);
% magMag = [xMag(1,1), yMag(1,1), zMag(1,1)];


%%%%%%%%%%%%%%%% set the outputs %%%%%%%%%%%%%%%%%%%%%%%
block.OutputPort(1).Data = refQuaternion;
block.OutputPort(2).Data = refAngularSpeed;
block.OutputPort(3).Data = cell2mat(refAttitudeEuler);
block.OutputPort(4).Data = satQuaternion;
block.OutputPort(5).Data = satAngularSpeed;
block.OutputPort(6).Data = cell2mat(satAttitudeEuler);

