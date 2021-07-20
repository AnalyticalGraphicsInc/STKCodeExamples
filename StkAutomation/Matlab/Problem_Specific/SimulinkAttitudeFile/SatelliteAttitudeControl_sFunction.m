function SatelliteAttitudeControl_sFunction(block)
%MSFUNTMPL_BASIC A Template for a Level-2 MATLAB S-Function
%   The MATLAB S-function is written as a MATLAB function with the
%   same name as the S-function. Replace 'msfuntmpl_basic' with the
%   name of your S-function.
%
%   It should be noted that the MATLAB S-function is very similar
%   to Level-2 C-Mex S-functions. You should be able to get more
%   information for each of the block methods by referring to the
%   documentation for C-Mex S-functions.
%
%   Copyright 2003-2010 The MathWorks, Inc.

%%
%% The setup method is used to set up the basic attributes of the
%% S-function such as ports, parameters, etc. Do not add any other
%% calls to the main body of the function.
%%
setup(block);

%endfunction

%% Function: setup ===================================================
%% Abstract:
%%   Set up the basic characteristics of the S-function block such as:
%%   - Input ports
%%   - Output ports
%%   - Dialog parameters
%%   - Options
%%
%%   Required         : Yes
%%   C-Mex counterpart: mdlInitializeSizes
%%
function setup(block)

% Register number of ports
block.NumInputPorts  = 3;
block.NumOutputPorts = 3;

% Setup port properties to be inherited or dynamic
block.SetPreCompInpPortInfoToDynamic;
block.SetPreCompOutPortInfoToDynamic;

% Override input port properties
block.InputPort(1).Dimensions  = 1;
block.InputPort(1).DatatypeID  = 0;  % double
block.InputPort(1).Complexity  = 'Real';
block.InputPort(1).DirectFeedthrough = false;

block.InputPort(2).Dimensions  = 1;
block.InputPort(2).DatatypeID  = 0; % double
block.InputPort(2).Complexity  = 'Real';
block.InputPort(2).DirectFeedthrough = false;

block.InputPort(3).Dimensions = 1;
block.InputPort(3).DatatypeID = 0; % double
block.InputPort(3).Complexity = 'Real';
block.InputPort(3).DirectFeedthrough = false;

% Override output port properties
block.OutputPort(1).Dimensions       = 1;
block.OutputPort(1).DatatypeID  = 0; % double
block.OutputPort(1).Complexity  = 'Real';

block.OutputPort(2).Dimensions       = 1;
block.OutputPort(2).DatatypeID  = 0; % double
block.OutputPort(2).Complexity  = 'Real';

block.OutputPort(3).Dimensions       = 1;
block.OutputPort(3).DatatypeID  = 0; % double
block.OutputPort(3).Complexity  = 'Real';

% Register parameters
block.NumDialogPrms     = 0;

% Register sample times
%  [0 offset]            : Continuous sample time
%  [positive_num offset] : Discrete sample time
%
%  [-1, 0]               : Inherited sample time
%  [-2, 0]               : Variable sample time
block.SampleTimes = [0.5 0];

% Specify the block simStateCompliance. The allowed values are:
%    'UnknownSimState', < The default setting; warn and assume DefaultSimState
%    'DefaultSimState', < Same sim state as a built-in block
%    'HasNoSimState',   < No sim state
%    'CustomSimState',  < Has GetSimState and SetSimState methods
%    'DisallowSimState' < Error out when saving or restoring the model sim state
block.SimStateCompliance = 'DefaultSimState';

%% -----------------------------------------------------------------
%% The MATLAB S-function uses an internal registry for all
%% block methods. You should register all relevant methods
%% (optional and required) as illustrated below. You may choose
%% any suitable name for the methods and implement these methods
%% as local functions within the same file. See comments
%% provided for each function for more information.
%% -----------------------------------------------------------------

  % SetInputPortSamplingMode:
  %   Functionality    : Check and set input and output port 
  %                      attributes specifying if port is operating 
  %                      in sample-based or frame-based mode
  %   C-Mex counterpart: mdlSetInputPortFrameData
  %   (Signal Processing Blockset is required in order to set a port
  %    to be frame-based)
  %
block.RegBlockMethod('SetInputPortSamplingMode', @SetInpPortFrameData);
block.RegBlockMethod('PostPropagationSetup',    @DoPostPropSetup);
block.RegBlockMethod('Start', @Start);
block.RegBlockMethod('Outputs', @Outputs);     % Required
block.RegBlockMethod('Update', @Update);
block.RegBlockMethod('Terminate', @Terminate); % Required

%end setup

%%
%% PostPropagationSetup:
%%   Functionality    : Setup work areas and state variables. Can
%%                      also register run-time methods here
%%   Required         : No
%%   C-Mex counterpart: mdlSetWorkWidths
%%
function DoPostPropSetup(block)

block.NumDworks = 5;

block.Dwork(1).Name            = 'time';
block.Dwork(1).Dimensions      = 1;
block.Dwork(1).DatatypeID      = 0;      % double
block.Dwork(1).Complexity      = 'Real'; % real
block.Dwork(1).UsedAsDiscState = true;

block.Dwork(2).Name            = 'A';
block.Dwork(2).Dimensions      = 1;
block.Dwork(2).DatatypeID      = 0;      % double
block.Dwork(2).Complexity      = 'Real'; % real
block.Dwork(2).UsedAsDiscState = true;

block.Dwork(3).Name            = 'B';
block.Dwork(3).Dimensions      = 1;
block.Dwork(3).DatatypeID      = 0;      % double
block.Dwork(3).Complexity      = 'Real'; % real
block.Dwork(3).UsedAsDiscState = true;

block.Dwork(4).Name            = 'C';
block.Dwork(4).Dimensions      = 1;
block.Dwork(4).DatatypeID      = 0;      % double
block.Dwork(4).Complexity      = 'Real'; % real
block.Dwork(4).UsedAsDiscState = true;

block.Dwork(5).Name            = 'lastTime';
block.Dwork(5).Dimensions      = 1;
block.Dwork(5).DatatypeID      = 0;      % double
block.Dwork(5).Complexity      = 'Real'; % real
block.Dwork(5).UsedAsDiscState = true;

function SetInpPortFrameData(block, idx, fd)
  
  block.InputPort(idx).SamplingMode = fd;
  block.OutputPort(1).SamplingMode  = fd;
  block.OutputPort(2).SamplingMode  = fd;
  block.OutputPort(3).SamplingMode  = fd;
  
%endfunction

%%
%% Start:
%%   Functionality    : Called once at start of model execution. If you
%%                      have states that should be initialized once, this
%%                      is the place to do it.
%%   Required         : No
%%   C-MEX counterpart: mdlStart
%%
function Start(block)

block.Dwork(1).Data = 0;
block.Dwork(2).Data = 0;
block.Dwork(3).Data = 0;
block.Dwork(4).Data = 0;
block.Dwork(5).Data = 0;

%endfunction

%%
%% Outputs:
%%   Functionality    : Called to generate block outputs in
%%                      simulation step
%%   Required         : Yes
%%   C-MEX counterpart: mdlOutputs
%%
function Outputs(block)

%retreive the stkParameters array from the UserData
stkParameters = get_param(block.BlockHandle, 'UserData');

%get the root and the access objects from the array
root = stkParameters{2};
realSat = stkParameters{3};
perfectPointingSat = stkParameters{4};
perfectBodyAxes = stkParameters{5};

%update the current time in STK
%root.CurrentTime = block.Dwork(1).Data;
root.CurrentTime = block.CurrentTime;

%find the new desired attitude
perfectAttitude = perfectBodyAxes.FindInAxes(root.CurrentTime + 2, perfectPointingSat.Vgt.Axes.Item('ICRF')).Orientation.QueryEulerAnglesArray('e321');

block.OutputPort(1).Data = perfectAttitude{1};
block.OutputPort(2).Data = perfectAttitude{2};
block.OutputPort(3).Data = perfectAttitude{3};

block.InputPort(1).Data;
block.InputPort(2).Data;
block.InputPort(3).Data;

%end Outputs

%%
%% Update:
%%   Functionality    : Called to update discrete states
%%                      during simulation step
%%   Required         : No
%%   C-MEX counterpart: mdlUpdate
%%
function Update(block)

%update the time and feedback C
block.Dwork(5).Data = block.Dwork(1).Data;  %Set lastTime before updating time
block.Dwork(1).Data = block.CurrentTime;
block.Dwork(2).Data = block.InputPort(1).Data;
block.Dwork(3).Data = block.InputPort(2).Data;
block.Dwork(4).Data = block.InputPort(3).Data;

%update STK so that the satellite's attitude is using the output from the
%feedback loop
%retreive the stkParameters array from the UserData
stkParameters = get_param(block.BlockHandle, 'UserData');

%get the root and satellite object
root = stkParameters{2};
realSat = stkParameters{3};

A = block.Dwork(2).Data;
B = block.Dwork(3).Data;
C = block.Dwork(4).Data;
realSat.Attitude.AddEuler(block.Dwork(5).Data, '321', A, B, C);

%end Update

%%
%% Terminate:
%%   Functionality    : Called at the end of simulation for cleanup
%%   Required         : Yes
%%   C-MEX counterpart: mdlTerminate
%%
function Terminate(block)

stkParameters = get_param(block.BlockHandle, 'UserData');

%get the root 
root = stkParameters{2};
uiapp = stkParameters{1};

%end Terminate

