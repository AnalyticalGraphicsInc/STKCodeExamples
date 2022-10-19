function [output] = Matlab_NullDirectionProvider(input)

% SAMPLE FOR MATLAB BASED CUSTOM PHASED ARRAY NULL DIRECTION PROVIDER 
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
					
	% The Number of Null Directions
       NumDirections = {'ArgumentName','NumDirections',...
                   'Name','NumDirections',...
                   'ArgumentType','Output'};
        
	% Directions - Azimuth Angles in antenna's coordinate system 
		Azimuths = {  'ArgumentName','Azimuths',...
                   'Name','Azimuths',...
                   'ArgumentType','Output'};
				   
	% Directions - Elevation Angles in antenna's coordinate system 
		Elevations  = {  'ArgumentName','Elevations',...
                   'Name','Elevations',...
                   'ArgumentType','Output'};
				   
	% register inputs
		
	% The Object Path is a String variable. 
	   ObjectPath = {'ArgumentName','ObjectPath',...
                 'Name','ObjectPath',...
                 'ArgumentType','Input'};
				
	% The scenario simulation epoch time is a double in seconds.
	    EpochSec = {'ArgumentName','EpochSec',...
                 'Name','EpochSec',...
                 'ArgumentType','Input'};
				
	%  The position is a String variable, representing latitude, longitude and 
	%  altitude of the antenna above the surface of the Earth.
		PosLLA = {'ArgumentName','PosLLA',...
                 'Name','PosLLA',...
                 'ArgumentType','Input'};
				 
	%  The position is a String variable, representing X, Y and Z.
		PosCBF = {'ArgumentName','PosCBF',...
                 'Name','PosCBF',...
                 'ArgumentType','Input'};	 
				 				
	% The Member Position Format is an integer
		MemberPositionFormat = { 'ArgumentName','MemberPositionFormat',...
                'Name','MemberPositionFormat',...
                'ArgumentType','Input'};
				 
	% The Number of Members is an integer
		NumberOfMembers = { 'ArgumentName','NumberOfMembers',...
                'Name','NumberOfMembers',...
                'ArgumentType','Input'};
			
	% The Member Positions is an array of doubles
		MemberPositions = { 'ArgumentName','MemberPositions',...
                'Name','MemberPositions',...
                'ArgumentType','Input'};
				
	% The Member Frequencies is an array of doubles
		MemberFrequencies = { 'ArgumentName','MemberFrequencies',...
                'Name','MemberFrequencies',...
                'ArgumentType','Input'};
				
	% The Member Powers is an array of doubles
		MemberPwrs = { 'ArgumentName','MemberPwrs',...
                'Name','MemberPwrs',...
                'ArgumentType','Input'};

	% The Member Ids is an array of integers
		MemberIds = { 'ArgumentName','MemberIds',...
                'Name','MemberIds',...
                'ArgumentType','Input'};

	% The Member Categories is an array of integers
		MemberCategories = { 'ArgumentName','MemberCategories',...
                'Name','MemberCategories',...
                'ArgumentType','Input'};


        output = {IsDynamic, NumDirections, Azimuths, Elevations, ObjectPath, EpochSec, PosLLA, PosCBF, MemberPositionFormat, NumberOfMembers, MemberPositions, MemberFrequencies, MemberPwrs, MemberIds, MemberCategories};
    
    case 'compute'

	computeData = input.methodData;

	PosLLA = [ 0.0 0.0 0.0 ];
	PosCBF = [ 0.0 0.0 0.0 ];
	
	ObjectPath = computeData.ObjectPath;
	EpochSec = computeData.EpochSec;
	PosLLA = computeData.PosLLA;
	PosCBF = computeData.PosCBF;
	NumberOfMembers = computeData.NumberOfMembers;
	MemberPositionFormat = computeData.MemberPositionFormat;
	MemberPositions = computeData.MemberPositions;
	MemberFrequencies = computeData.MemberFrequencies;
	MemberPwrs = computeData.MemberPwrs;
    MemberIds = computeData.MemberIds;
	MemberCategories = computeData.MemberCategories;
		
	Azimuths(1) = 0;
	Elevations(1) = 0;

	%############################################################################################
	% USER PLUGIN BEAM DIRECTION PROVIDER MODEL AREA.
	% PLEASE REPLACE THE CODE BELOW WITH YOUR DIRECTION PROVIDER COMPUTATION MODEL
	%
	% This sample demonstrates how to dynamically return nulling directions.  This script looks 
	% at each member to determine it's category (i.e. Aircraft, Facility, Satellite, etc.) to 
	% determine if it should null the member.  If the member is not an aircraft, it will return
	% the member's direction in order to be nulled.
	% This is just a simplistic example to demonstrate how to dynamically return null directions.
	%
	% All input and out paramters have been mapped to variables described below.
	%############################################################################################
	% NOTE: the outputs that are returned MUST be in the same order as registered
	% If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	% will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	% at every timestep.
	%
	% All directions specified as Azimuth and Elevation angles (see STK help) in degrees and 
	% relative to the entity's body coordinate system.
	%
	% Script input variables available to user:
	%		ObjectPath - Path of the object, i.e. objects fully qualified name.   string
	%		EpochSec   - Current simulation epoch seconds.                        double  
	%		PosLLA	   - Position the object in LLA.                              string
	%		PosCBF	   - Position the object in CBF.                              string
	%		NumberOfMembers - Number of members in view at this time step. Used
    %                         to define size of input field arrays.  Max 100
	%                         WARNING: Always check this field since, for efficency, 
	%                                  STK may provide old data for 
	%                                  other fields and should be considered stale
    %                                  if this field is 0.                       	 int
    %       MemberPositionFormat - Defines if memberPositions array will be 
	%                              relative position in Theta/Phi/Range (rad/rad/m)
	%                              or X/Y/Z (m/m/m)                               int  
	%		MemberPositions      - Member positions in format specified by
	%                              MemberPositionFormat.                          double(3)
    %       MemberFrequencies   -  Member frequencies (-1 for non-RF members)     double(100)
    %       MemberPwrs          -  Member eirp (-3000dBW for non-emitter members) double(100)
    %       MemberIds           -  Member ids, 0-based as listed in antenna.         int(100)
	%       MemberCategories    -  Member object category (Aircraft, Facility, etc.) int(100)
	%
	% Script outputs which must be filled in by the user:
	%       IsDynamic           - Indicates if script is time-dynamic (see above).   int
	%       NumDirections       - Currently, beam only supports 1 direction           int
	%		Azimuths            - Az in antenna's coordinate system (rad)           double(100)
	%       Elevations          - El in antenna's coordinate system (rad)           double(100)	%############################################################################################
        
	% Initialize Output values
	IsDynamic     = 1; 
	NumDirections = 0;
	
	% For each object, identify it...
	for i = 0:NumberOfMembers - 1
	   if MemberCategories(i+1) ~= 1  % if it is not an aircraft...
	      %...treat it as a jammer and null it
	      Azimuths(NumDirections+1) = MemberPositions(3*i+1);
	      Elevations(NumDirections+1) = MemberPositions(3*i+2);
		  NumDirections = NumDirections+1;
	   end
	 end  
		
	
	% END OF USER MODEL AREA
    % ###########################################
	
    % Transfer to return structure...
    output.IsDynamic      = IsDynamic;
	output.NumDirections  = NumDirections;
	if (NumDirections < 1) % The output memory MUST be initialized in the script or STK will turn off the script
		output.Azimuths(1) = Azimuths(1);
		output.Elevations(1) = Elevations(1);
	else
		for I = 1:1:NumDirections
			output.Azimuths(I) = Azimuths(I);
			output.Elevations(I) = Elevations(I);
	    end
	end

    otherwise
        output = [];
end

