function [output] = Matlab_BeamDirectionProvider(input)

% SAMPLE FOR MATLAB BASED CUSTOM PHASED ARRAY BEAM DIRECTION PROVIDER 
% PLUGIN SCRIPT PROVIDED BY THE USER
% PLEASE ADD YOUR MODEL IN THE USER ANTENNA GAIN MODEL AREA BELOW.
% DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
	PiOver2 = 1.5707963268;
    degToRadians = 0.01745329252;
    radiansToDeg = 57.29577951308;	
	
	gScanAzStepSize = 4*degToRadians;  
	gScanElStepSize = 5*degToRadians;
	gScanMinAz = -30*degToRadians; 
	gScanMaxAz = 30*degToRadians;    
	gScanMinEl = -20*degToRadians;   
	gScanMaxEl = 5*degToRadians;
	
	global gBdpScanAz;
	global gBdpScanEl;
	
	if isempty(gBdpScanAz) || isempty(gBdpScanEl)
		gBdpScanAz = gScanMinAz;
		gBdpScanEl = gScanMinEl;	
	end
	
switch input.method
    
    case 'register'

	% register outputs
	
	% IsDynamic is a flag (0 or 1, default = 0) to indicate whether the plugin is time-based or otherwise contains dynamic data,
	% in which case STK must be notified to recalculate the data as necessary. which is Boolean.
       IsDynamic= {'ArgumentName','IsDynamic',...
                    'Name','IsDynamic',...
                    'ArgumentType','Output'};
					
	% The Number of Beam Directions - Currently, beam only supports 1 direction 
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


        output = {IsDynamic, NumDirections, Azimuths, Elevations, ...
                  ObjectPath, EpochSec, PosLLA, PosCBF, ...
				  MemberPositionFormat, NumberOfMembers, MemberPositions, ...
				  MemberFrequencies, MemberPwrs, MemberIds, MemberCategories};
    
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
	% This simple sample demonstrates how to dynamically return beam directions.  This script 
	% defines the antenna's field of regard (FOR) and then scans the FOR.  If any aircraft fly   
	% within the FOR and within effective range it will switch to tracking mode.  It will switch 
	% it's target if another member becomes closer.  If all objects are out of the FOR, it will 
	% switch back to track mode. This is just a simplistic example to demonstrate how to 
	% dynamically return direction.
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
	%       Elevations          - El in antenna's coordinate system (rad)           double(100)
	%############################################################################################
        
	% Initialize Output values
	IsDynamic     = 1
    NumDirections = 1;
	
	radarRange = 100000;

	% If any object is in radar range, use track mode determine who to track
	minRange = 1e300;
	for i = 0:NumberOfMembers - 1   
		objAz = MemberPositions(3*i+1);
		objEl = MemberPositions(3*i+2);
		objRange = MemberPositions(3*i+3);
	
	    % Track the closest object within range of the radar
	    if objRange < radarRange
	       if objRange < minRange
	           % Only target it if it's in front hemisphere
		       if objAz > -PiOver2
					if objAz < PiOver2
						minAz = objAz;
						minEl = objEl;
						minRange = objRange;
					end
			   end
		   end
	   end
	end

    % If nothing is inside radar range, continue scan mode
	if minRange == 1e300
        gBdpScanAz = gBdpScanAz + gScanAzStepSize;
        gBdpScanAz = gBdpScanAz + gScanAzStepSize;
	    if EpochSec <= 0.00001		  
	      gBdpScanAz = gScanMinAz;
	      gBdpScanEl = gScanMinEl;
	    end
    	Azimuths(1) = gBdpScanAz;
	    Elevations(1) = gBdpScanEl;
		
        % Check for end of scan pattern and reset to begin scan pattern
		if gBdpScanAz > gScanMaxAz
			gBdpScanEl = gBdpScanEl + gScanElStepSize;
			gBdpScanAz = gScanMinAz;
		end
		
		if gBdpScanEl > gScanMaxEl
			gScanElStepSize = gScanElStepSize;
			gBdpScanEl = gScanMinEl;
		end
    else
	    Azimuths(1) = minAz
		Elevations(1) = minEl
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

