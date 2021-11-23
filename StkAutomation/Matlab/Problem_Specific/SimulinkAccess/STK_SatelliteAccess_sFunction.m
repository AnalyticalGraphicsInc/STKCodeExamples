function STK_SatelliteAccess_sFunction(block)
%STK_SatelliteAccess_sFunction 
%Copyright 2013 Analytical Graphics, Inc.
%Author: Daniel Honaker
%
% This is a simple demonstration of how to integrate STK with 
% Simulink through the use of a Level-2 S-Function block.
% 
% The STK scenario is created and configured with code that has 
% been placed in the InitFcn callback for this block.  The important
% objects created in STK are stored in the UserData parameter for the
% block with a cell array called StkParameters.  These handles include
% the IAgStkObjectRoot object, the AgUiApplication object, and the
% three access objects 
% 
%   sunAccess  - One point access from the satellite to the Sun, used to 
%                 determine if the satellite is in sunlight.
%   
%   atAccess   - One point access from the sensor to the greenland area
%                 target, used to determine if the satellite is currently
%                 collecting data or running an experiment.
%   
%   commAccess - One point access from the satellite transmitter to the 
%                 ground station's receiver, used to determine if the 
%                 satellite is downlinking/communicating data.  There is 
%                 a constraint on this link of BER < 1e-8, ensuring only
%                 high quality links are considered.
%                 
% This function expects time to be passed as the input on Port 1.  It will
% store the time in the DWork vector for use in the Outputs callback.  In 
% the Outputs callback the StkParameters cell array will be retrieved from
% the UserData and each of the access objects will be calculated for the
% current time.  If access is satisfied, a value of 1 will be output to the
% port representing that calculation (output1 = sun, output2 = experiment
% output3 = comms).


%
% The setup method is used to setup the basic attributes of the
% S-function such as ports, parameters, etc. Do not add any other
% calls to the main body of the function.  
%   
setup(block);
  
%endfunction

% Function: setup ===================================================
% Abstract:
%   Set up the S-function block's basic characteristics such as:
%   - Input ports
%   - Output ports
%   - Dialog parameters
%   - Options
% 
%   Required         : Yes
%   C-Mex counterpart: mdlInitializeSizes
%
function setup(block)

  % Register the number of ports.
  block.NumInputPorts  = 1;
  block.NumOutputPorts = 3;
  
  % Set up the port properties to be inherited or dynamic.
  block.SetPreCompInpPortInfoToDynamic;
  block.SetPreCompOutPortInfoToDynamic;

  % Override the input port properties.
  block.InputPort(1).DatatypeID  = 0;  % double
  block.InputPort(1).Complexity  = 'Real';
  
  % Override the output port properties.
  block.OutputPort(1).DatatypeID  = 0; % double
  block.OutputPort(1).Complexity  = 'Real';
  
  % Override the output port properties.
  block.OutputPort(2).DatatypeID  = 0; % double
  block.OutputPort(2).Complexity  = 'Real';
  
  % Override the output port properties.
  block.OutputPort(3).DatatypeID  = 0; % double
  block.OutputPort(3).Complexity  = 'Real';

  % Register the sample times.
  %  [0 offset]            : Continuous sample time
  %  [positive_num offset] : Discrete sample time
  %
  %  [-1, 0]               : Inherited sample time
  %  [-2, 0]               : Variable sample time
  block.SampleTimes = [1 0];
  
  % -----------------------------------------------------------------
  % Options
  % -----------------------------------------------------------------
  
  % Specify the block simStateCompliance. The allowed values are:
  %    'UnknownSimState', < The default setting; warn and assume DefaultSimState
  %    'DefaultSimState', < Same SimState as a built-in block
  %    'HasNoSimState',   < No SimState
  %    'CustomSimState',  < Has GetSimState and SetSimState methods
  %    'DisallowSimState' < Errors out when saving or restoring the SimState
  block.SimStateCompliance = 'DefaultSimState';
  
  % -----------------------------------------------------------------
  % Register the methods called during update diagram/compilation.
  % -----------------------------------------------------------------

  %
  % PostPropagationSetup:
  %   Functionality    : Set up the work areas and the state variables. You can
  %                      also register run-time methods here.
  %   C-Mex counterpart: mdlSetWorkWidths
  %
  block.RegBlockMethod('PostPropagationSetup', @DoPostPropSetup);

  % SetInputPortSamplingMode:
  %   Functionality    : Check and set input and output port 
  %                      attributes specifying if port is operating 
  %                      in sample-based or frame-based mode
  %   C-Mex counterpart: mdlSetInputPortFrameData
  %   (Signal Processing Blockset is required in order to set a port
  %    to be frame-based)
  %
  block.RegBlockMethod('SetInputPortSamplingMode', @SetInpPortFrameData);
  
  % 
  % Start:
  %   Functionality    : Call to initialize the state and the work
  %                      area values.
  %   C-Mex counterpart: mdlStart
  %
  block.RegBlockMethod('Start', @Start);

  % 
  % Outputs:
  %   Functionality    : Call to generate the block outputs during a
  %                      simulation step.
  %   C-Mex counterpart: mdlOutputs
  %
  block.RegBlockMethod('Outputs', @Outputs);

  % 
  % Update:
  %   Functionality    : Call to update the discrete states
  %                      during a simulation step.
  %   C-Mex counterpart: mdlUpdate
  %
  block.RegBlockMethod('Update', @Update);

  % 
  % Terminate:
  %   Functionality    : Call at the end of a simulation for cleanup.
  %   C-Mex counterpart: mdlTerminate
  %
  block.RegBlockMethod('Terminate', @Terminate);

% -------------------------------------------------------------------
% The local functions below are provided to illustrate how you may implement
% the various block methods listed above.
% -------------------------------------------------------------------

function Start(block)
  block.Dwork(1).Data = 0;
%endfunction

function SetInpPortFrameData(block, idx, fd)
  
  block.InputPort(idx).SamplingMode = fd;
  block.OutputPort(1).SamplingMode  = fd;
  block.OutputPort(2).SamplingMode  = fd;
  block.OutputPort(3).SamplingMode  = fd;
  
%endfunction

function DoPostPropSetup(block)
  block.NumDworks = 1;
  
  block.Dwork(1).Name            = 'time';
  block.Dwork(1).Dimensions      = 1;
  block.Dwork(1).DatatypeID      = 0;      % double
  block.Dwork(1).Complexity      = 'Real'; % real
  block.Dwork(1).UsedAsDiscState = true;
%endfunction

function Outputs(block)

%retreive the stkParameters array from the UserData
stkParameters = get_param(block.BlockHandle, 'UserData');

%get the root and the access objects from the array
root = stkParameters{2};
sunAccess = stkParameters{3};
atAccess = stkParameters{4};
commAccess = stkParameters{5};

%update the current time in STK
root.CurrentTime = block.Dwork(1).Data; 

%calculate the sun access 
sunAccess.StartTime = block.Dwork(1).Data; 
sunAccess.StopTime = block.Dwork(1).Data; 
results = sunAccess.Compute;
light = results.Item(0).AccessSatisfied;

%calculate the area target access 
atAccess.StartTime = block.Dwork(1).Data;
atAccess.StopTime = block.Dwork(1).Data;
results = atAccess.Compute;
experiment = results.Item(0).AccessSatisfied;

%calculate the comm access 
commAccess.StartTime = block.Dwork(1).Data; 
commAccess.StopTime = block.Dwork(1).Data; 
results = commAccess.Compute;
comms = results.Item(0).AccessSatisfied;

if light
    block.OutputPort(1).Data = 1;
else
    block.OutputPort(1).Data = 0;
end

if experiment
    block.OutputPort(2).Data = 1;
else
    block.OutputPort(2).Data = 0;
end

if comms
    block.OutputPort(3).Data = 1;
else
    block.OutputPort(3).Data = 0;
end
  
%endfunction

function Update(block)
  
  block.Dwork(1).Data = block.InputPort(1).Data;
  
%endfunction
    
function Terminate(block)

disp(['Terminating the block with handle ' num2str(block.BlockHandle) '.']);

%attempt to release the connection to STK
stkParameters = get_param(block.BlockHandle, 'UserData');
uiapp = stkParameters{1};
uiapp.Quit;
%endfunction
