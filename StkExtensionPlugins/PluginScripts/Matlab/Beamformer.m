function [output] = Matlab_Beamformer(input)

% SAMPLE FOR MATLAB BASED CUSTOM PHASED ARRAY BEAM FORMER
% PLUGIN SCRIPT PROVIDED BY THE USER
% PLEASE ADD YOUR MODEL IN THE USER ANTENNA GAIN MODEL AREA BELOW.
% DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
	
switch input.method
    
    case 'register'
	
	% register outputs
	
	% IsDynamic is a flag (0 or 1, default = 0) to indicate whether the plugin is time-based or otherwise contains dynamic data,
	% in which case STK must be notified to recalculate the data as necessary. which is Boolean.
       IsDynamic= {'ArgumentName','IsDynamic',...
                    'Name','IsDynamic',...
                    'ArgumentType','Output'};
     
	% Weights - Complex values for each element.
		Weights = {'ArgumentName','Weights',...
                   'Name','Weights',...
                   'ArgumentType','Output'};
				   
	% register inputs
		
	% The scenario simulation epoch time is a double in seconds.
	    EpochSec = {'ArgumentName','EpochSec',...
                 'Name','EpochSec',...
                 'ArgumentType','Input'};
				 
	% The Number of Elements is an integer
		NumberOfElements = { 'ArgumentName','NumberOfElements',...
                'Name','NumberOfElements',...
                'ArgumentType','Input'};

	% The Design Frequency is an integer
		DesignFrequency = { 'ArgumentName','DesignFrequency',...
                'Name','DesignFrequency',...
                'ArgumentType','Input'};
				
	% The Operating Frequency is an integer
		OperatingFrequency = { 'ArgumentName','OperatingFrequency',...
                'Name','OperatingFrequency',...
                'ArgumentType','Input'};
								
	% The Number of Beam Directions is an integer
		NumberOfBeamDirections = { 'ArgumentName','NumberOfBeamDirections',...
                'Name','NumberOfBeamDirections',...
                'ArgumentType','Input'};
				
	% The Beam Directions is an array of doubles
		BeamDirections = { 'ArgumentName','BeamDirections',...
                'Name','BeamDirections',...
                'ArgumentType','Input'};
				
	% The Number of Null Directions is an integer
		NumberOfNullDirections = { 'ArgumentName','NumberOfNullDirections',...
                'Name','NumberOfNullDirections',...
                'ArgumentType','Input'};
	
	% The Null Directions is an array of doubles
		NullDirections = { 'ArgumentName','NullDirections',...
                'Name','NullDirections',...
                'ArgumentType','Input'};	

        output = {IsDynamic, Weights, EpochSec, NumberOfElements, DesignFrequency, OperatingFrequency, NumberOfBeamDirections, BeamDirections, NumberOfNullDirections, NullDirections};
    
    case 'compute'

        computeData = input.methodData;
		
	EpochSec = computeData.EpochSec;
	NumberOfElements = computeData.NumberOfElements;
	DesignFrequency = computeData.DesignFrequency;
	OperatingFrequency = computeData.OperatingFrequency;
	NumberOfBeamDirections = computeData.NumberOfBeamDirections;
	BeamDirections = computeData.BeamDirections;
	NumberOfNullDirections = computeData.NumberOfNullDirections;
	NullDirections = computeData.NullDirections;
		
	%############################################################################################
	% USER PLUGIN BEAMFORMER MODEL AREA.
	% PLEASE REPLACE THE CODE BELOW WITH YOUR BEAMFORMER COMPUTATION MODEL
	%
	% This sample demonstrates how to dynamically return weights.  Implements a static "deck" of
    % three weight sets to be applied at specific time steps. This is a very simplistic example
	% to demostrate how to dynamically return weights.
	% 
	%
	% All input and out paramters have been mapped to variables described below.
	%############################################################################################
	% NOTE: the outputs that are returned MUST be in the same order as registered
	% If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	% will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	% at every timestep.
	%
	% All weights are to be complex numbers(see STK help).
	%
	% Script input variables available to user:
	%		EpochSec   - Current simulation epoch seconds.                         double  
	%		NumberOfElements - Number of enabled antenna elements in the array.    int
	%		DesignFrequency - Design frequency of the antenna array (Hz).          double
	%		OperatingFrequency - Current operating frequency of the antenna (Hz).  double
    %       NumberOfBeamDirections -  The number of items in the BeamDirections 
	%                          input field described below. 	                   int
	%		BeamDirections     - Array of Az/El values (rad/rad) representing 
    %                          the direction "entities", where "entities" are  
    %                          defined by the specific selected Direction Provider. 
    %                          Currently an array length of one is supported.      double(1,2)
    %       NumberOfNullDirections -  The number of items in the NullDirections 
	%                          input field described below. 	                   int
	%		NullDirections     - Array of Az/El values (rad/rad) representing the 
    %                          direction "entities", where "entities" are defined  
    %                          by the specific selected beam-steering Direction Prvdr. 
	%                          Currently an array length of one is supported.      double(1,2)
	%
	% Script outputs which must be filled in by the user:
	%       IsDynamic      -   Indicates if script is time-dynamic (see above).    int
	%       Weights        -   Complex values for each element.  Format is 
	%                          linear array of real/imaginary interleaved values.
	%############################################################################################
        
	% Initialize Output values
	IsDynamic     = 1; 
	
	if EpochSec < 600
	   Weights = [-0.0727559 0.233901  0.0164913 0.215751  -0.0603389 0.244863  0.153553 0.128345  0.184669 0.227678  -0.0637141 0.202648  0.205931 0.0567225  0.160496 0.177382  0.147236 -0.0999877  0.313338 -1.85741e-013  0.147236 0.0999877  0.160496 -0.177382  0.205931 -0.0567225  -0.0637141 -0.202648  0.184669 -0.227678  0.153553 -0.128345  -0.0603389 -0.244863  0.0164913 -0.215751  -0.0727559 -0.233901]; 
    elseif (EpochSec >= 600) & (EpochSec < 1200)
	   Weights = [-0.0384998 0.242632  0.116201 0.175782  -0.0728582 0.22732  0.199626 -0.0118431  0.180923 0.201406  -0.124046 0.194012  0.217225 -0.00904073  0.127373 0.194714  0.0867985 -0.184947  0.281027 1.38456e-015  0.0867985 0.184947  0.127373 -0.194714  0.217225 0.00904073  -0.124046 -0.194012  0.180923 -0.201406  0.199626 0.0118431  -0.0728582 -0.22732  0.116201 -0.175782  -0.0384998 -0.242632]; 
	else
	   Weights = [-0.0160897 0.235592  0.195985 0.120679  -0.0770078 0.204931  0.186028 -0.139551  0.158855 0.171406  -0.147394 0.179345  0.217917 -0.0586559  0.102059 0.196255  0.0284807 -0.231605  0.232606 -7.91854e-014  0.0284807 0.231605  0.102059 -0.196255  0.217917 0.0586559  -0.147394 -0.179345  0.158855 -0.171406  0.186028 0.139551  -0.0770078 -0.204931  0.195985 -0.120679  -0.0160897 -0.235592];
    end

	% END OF USER MODEL AREA
    % ###########################################
	
    % Transfer to return structure...
	output.IsDynamic = IsDynamic;
	for I = 1:1:NumberOfElements*2
	   output.Weights(I) = Weights(I);
	end


    otherwise
        output = [];
end

