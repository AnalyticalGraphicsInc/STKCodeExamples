function [output] = Matlab_GaussianAntennaGain(input)

% SAPMLE FOR MATLAB BASED CUSTOM ANTENNA GAIN PLUGIN SCRIPT PROVIDED 
% BY THE USER
% PLEASE ADD YOUR MODEL IN THE USER ANTENNA GAIN MODEL AREA BELOW.
% DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT

switch input.method
    
    case 'register'
	
	% register outputs
        
	% The antenna gain value in the direction given by the azimuth and elevation angles of the antenna boresight is 
	% a double in dB or dBi.
		AntennaGain  = {  'ArgumentName','AntennaGain',...
                   'Name','AntennaGain',...
                   'ArgumentType','Output'};
    %  The 3dB beamwidth of the antenna gain pattern is a double in radians. 
        Beamwidth = {'ArgumentName','Beamwidth',...
                     'Name','Beamwidth',...
                     'ArgumentType','Output'};
        
	% The maximum gain of the antenna beam is a double in dB or dBi. This value may be at the boresight.
        AntennaMaxGain = {'ArgumentName','AntennaMaxGain',...
                      'Name','AntennaMaxGain',...
                      'ArgumentType','Output'};
   
    % The antenna integrated gain over a noise source is a double in dB or dBi. 
	% This value is currently estimated by STK using internal numerical methods. 
        IntegratedGain = { 'ArgumentName','IntegratedGain',...
                    'Name','IntegratedGain',...
                    'ArgumentType','Output'};
					
	% DynamicGain is a flag (0 or 1, default = 0) to indicate whether the plugin is time-based or otherwise contains dynamic data,
	% in which case STK must be notified to recalculate the data as necessary. which is Boolean.
        DynamicGain= {'ArgumentName','DynamicGain',...
                    'Name','DynamicGain',...
                    'ArgumentType','Output'};
					
	%	This is a flag for Antenna Coordinate System (0 for Azimuth-Elevation Polar & 1 for Azimuth-Elevation Rectangular).
       AntennaCoordSystem = {'ArgumentName','AntennaCoordSystem',...
                   'Name','AntennaCoordSystem',...
                   'ArgumentType','Output'};

	% register inputs
		
	% The current date and time is a String variable. 
		date = {'ArgumentName','Date',...
                'Name','DateUTC',...
                'ArgumentType','Input'};

    % The scenario central body is a String variable.     
		cbName = {'ArgumentName','CbName',...
                'Name','CbName',...
                'ArgumentType','Input'};
				
	% The scenario simulation epoch time is a double in seconds.
	   EpochSec = {'ArgumentName','EpochSec',...
                'Name','EpochSec',...
                'ArgumentType','Input'};

	% The frequency of the transmitter carrier is a double in Hz.
       Frequency = { 'ArgumentName','Frequency',...
                'Name','Frequency',...
                'ArgumentType','Input'};
 
	%	The azimuth angle is a double in radians that is measured from the antenna boresight in the antenna rectangular 
	% 	coordinate system used by STK. In combination with ElevationAngle, represents the direction of the comm. link where
	%	the gain value is required.     
		AzimuthAngle = {'ArgumentName','AzimuthAngle',...
                  'Name','AzimuthAngle',...
                  'ArgumentType','Input'};

	%	The elevation angle is a double in radians that is measured from the antenna boresight in the antenna rectangular 
	%	coordinate system used by STK. In combination with AzimuthAngle, represents the direction of the comm. link where 
	%	the gain value is required.
       ElevationAngle = { 'ArgumentName','ElevationAngle',...
                  'Name','ElevationAngle',...
                  'ArgumentType','Input'};

    %  The antenna position is a vector of doubles, of length 3, representing latitude (radians), longitude (radians) and 
	%  altitude of the antenna above the surface of the Earth (meters).
	   AntennaPosLLA = {'ArgumentName','AntennaPosLLA',...
                 'Name','AntennaPosLLA',...
                 'ArgumentType','Input'};

	%	This is a flag for Antenna Coordinate System (0 for Azimuth-Elevation Polar & 1 for Azimuth-Elevation Rectangular).
       AntennaCoordSystem = {'ArgumentName','AntennaCoordSystem',...
                   'Name','AntennaCoordSystem',...
                   'ArgumentType','Input'};
   
        output = {AntennaGain, Beamwidth, AntennaMaxGain, IntegratedGain, DynamicGain, AntennaCoordSystem, date, cbName, EpochSec, Frequency, AzimuthAngle, ElevationAngle, AntennaPosLLA, AntennaCoordSystem};
    
    case 'compute'

        computeData = input.methodData;

	% USER ANTENNA GAIN MODEL AREA
        
	  dia  = 1.0;
	  eff  = 0.55;
	  antLLA = [ 0.0 0.0 0.0 ];

        freq = computeData.Frequency;
        az   = computeData.AzimuthAngle;
        el   = computeData.ElevationAngle;
		antLLA = computeData.AntennaPosLLA;

	  lambda = 299792458.0 / freq;
	  thetab = lambda / (dia * sqrt(eff));
	  x = 3.141592 * dia / lambda;

	  gmax = eff * x * x;
	  expParm = -2.76 * el * el / (thetab * thetab);
	  if expParm < -700
		 expParm = -700;
	  end
	  gain = gmax * exp(expParm);
	  gain = 10.0*log(gain)/log(10.0);

        output.AntennaGain    = gain;
        output.Beamwidth      = thetab;
        output.AntennaMaxGain = 10.0 * log(gmax)/log(10.0);
        output.IntegratedGain = 0.5;
        output.DynamicGain = 0;
        
        %AntennaCoordSystem return 0 for Polar and 1 for Rectangular
        output.AntennaCoordSystem = 0;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

