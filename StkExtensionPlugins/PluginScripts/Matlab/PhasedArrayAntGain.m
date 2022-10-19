function [output] = Matlab_PhasedArrayAntGain(input)

% SAMPLE STK ANTENNA GAIN PLUGIN TO MODEL PHASED ARRAY GAIN (WRITTEN IN MATLAB)
% TO MODIFY/REPLACE THE SIMPLE COSINE LOSS MODEL, EDIT CODE IN THE -USER GAIN MODEL AREA-
% DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT

switch input.method
    
    case 'register'
	
%******************************************************************
%******************************************************************
%************************Output Parameters*************************
%******************************************************************
%******************************************************************
        
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
					
	% This is a flag for Antenna Coordinate System (0 for Azimuth-Elevation Polar & 1 for Azimuth-Elevation Rectangular).
       AntennaCoordSystem = {'ArgumentName','AntennaCoordSystem',...
                   'Name','AntennaCoordSystem',...
                   'ArgumentType','Output'};
                 
%******************************************************************
%******************************************************************
%************************Input Parameters*************************
%******************************************************************
%******************************************************************

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

	% This is a flag for Antenna Coordinate System (0 for Azimuth-Elevation Polar & 1 for Azimuth-Elevation Rectangular).
       AntennaCoordSystem = {'ArgumentName','AntennaCoordSystem',...
                   'Name','AntennaCoordSystem',...
                   'ArgumentType','Input'};
   
        output = {AntennaGain, Beamwidth, AntennaMaxGain, IntegratedGain, DynamicGain, AntennaCoordSystem, date, cbName, EpochSec, Frequency, AzimuthAngle, ElevationAngle, AntennaPosLLA, AntennaCoordSystem};
    
    case 'compute'

        computeData = input.methodData;

	% USER ANTENNA GAIN MODEL AREA
    
    % TO MODIFY/REPLACE THE SIMPLE COSINE LOSS MODEL, EDIT CODE IN THIS SECTION OF THE SCRIPT!!!

	% DESCRIPTION OF SIMPLE COSINE LOSS MODEL
	% ----------------------------------------
	%
	% Overview:
	% Simple approximation of mainbeam line-of-sight gain assuming ideal cosine scan loss behavior
	% over a limited scan range (e.g. 60 degrees). Estimates first-order gain reduction from 
	% distortion and spreading introduced as mainbeam is scanned away from the array's normal 
	% vector. The model is intended as a simple symmetrical planar phased array model for link
	% budgets and radar analysis.
	%
	% 
	% Applicability:
	% As shipped, this plugin script is intended for use in communication link and radar anlaysis
	% invloving two rf objects with a constant system noise temperature.  This plugin as shipped 
	% is NOT appropriate for use with a calculated system temperature or in an STK CommSystem 
	% analysis with constellations of tranmitters, receivers and interferers. This is because 
	% integrated gain, non-symmetrical arrays, mainbeam shape, sidelobe structure, adaptive nulling, and
	% other gain pattern details are not modeled. Nor will this plug-in provide dynamic gain graphics
	% in the STK 2-D or 3-D windows. If such capabilities are of interest to you, please ask AGI
	% Support about additional phased array modeling options for STK.  You may contact us
	% by phone at 1.800.924.7244 or via email at support@agi.com.
	%
	% Assumptions:
	% 1) Planar array of ideal elements --> gain reduction proportional to projected aperture area
	% 2) Half-wavelength or less element spacing --> minimal gain reduction from grating lobes
	% 3) Symmetrical element layout --> equivalent scan loss behavior over all line-of-site azimuths
	% 4) Scan limited --> beyond a certain scan angle (e.g. 60 deg), no useful gain provided
	%
	% Mathematics:
	% A maximum gain value representing the broadside on-axis mainbeam (i.e. line-of-sight along 
	% array's normal vector) is reduced by multipliying by the cosine of the scan angle, where 
	% scan angle is defined as the angle between the array normal vector and the line-of-sight 
	% to the intended communication node or radar target. 
	%
	% The user is presented two options to model the maximum gain:
	% 1) a user-specified hardcoded value
	% 2) a calculation based on user-specified circular aperture area, aperture efficiency, and
	% operating frequency. The user selects the model they wish to use by setting the gainModel
	% script parameter.
	% As shipped, the script is set to use a hardcoded maximum gain of 41 dB.
	% A placeholder is also provided for the user to introduce their own maximum gain calculation.
	%
	% END DESCRIPTION
	%

	% Overview of inputs and variable types provided by the STK antenna gain plug-in interface
	%   Test Model Phased Array Antenna
	%	Script inputs
	%		Date, Time				string
	%		Central Body Name			string
	%		Frequency(Hz)				double
	%		Azimuth Angle(Rad)			double
	%		Elevation Angle(Rad)			double
	%		AntennaPosLLA(Deg,Deg,m)		double(3)
	%		AntennaCoordSystem			integer 
	%							(0: az-el-polar,
	%							 1: az-el-rectangular,
	%							 2: az-el-other)
	%		EpochSec (seconds)			double
	%
	
	% Declaring and Initializing inputs, constants and other variables
	  
     % Get input values (those not used by simple cosine loss model are commented out)

        %date = computeData.DateUTC;
        %cb = computeData.CbName;
        freq = computeData.Frequency;
        az   = computeData.AzimuthAngle;
        el   = computeData.ElevationAngle;
        %antLLA = [ 0.0 0.0 0.0 ];
		%antLLA = computeData.AntennaPosLLA;
        %AntennaCoordSystem = computeData.AntennaCoordSystem;
        %EpochSec = computeData.EpochSec;
        
        % Initialize return gain value and internally calculated scan angle value
        AntennaGain = -999999.9;
    	scanLoss    = 0.0;
    	
    %Maximum Allowable Scan Range - User defined
	%-------------------------------------------
	%Scan angles larger than this hardcoded limit will result in zero gain returned to STK
	%For the simple cosine loss model, max scan set to 60 degrees (Pi/3)
    	maxScan = pi/3;
        
    %Maximum Gain Model
	%------------------
	%Represents broadside on-axis mainbeam boresight gain (i.e. boresight along array's
	%normal vector). As shipped, the script is set to use a hardcoded maximum gain of 
	%41 dB (equivalent to 1 meter parabolic at 14.5 GHz and 55% efficiency). The user may
	%change this harcoded value or use the calculation-based model provided by setting
	%the gainModel script parameter and providing the appropriate inputs. A placeholder is
	%also provided for the user to introduce their own max gain calculation.

	gainModel = 0;
	
	if gainModel == 0
	  % Hardcoded maximum gain value--user can set value as desired
    	  gmax = 41.0;
    elseif gainModel == 1
      % Max gain calculated based on circular aperture area, aperture efficiency, and operating frequency.
	  % This model is useful when the array will be used over a variety of operating frequencies.
	  % Assumes half-wavelength or less element spacing!!!
	  % Reference: Handbook of Electrical Engineering Calculations, Phadke, 1999

      % USER-SPECIFIED DESIGN FREQUENCY IN HZ -- used to enforce half-wavelength or less 
	  % element spacing assumption (operating frequencies above design frequency will result
	  % in a return value of -999999.9 dB gain)
	  designFreq = 14500000000;
        
      % USER-SPECIFIED APERTURE AREA IN SQUARE-METERS
	  area = pi;

	  % USER-SPECIFIED APERTURE EFFICIENCY (UNITLESS)
	  eff = 0.6;

      % Calculate max gain in dB
	  if freq <= designFreq
            lambda = 299792458.0 / freq;
            gmax = eff*4*pi*area/(lambda*lambda);
            gmax = 10*log(gmax)/log(10.0);
      else
            gmax = -999999.9;
      end
      
    else
	  % Placeholder for user-supplied maximum on-axis gain calculation.
	  gmax = 0.0;
    end

    % Scan Angle Computation
    % ----------------------
	
	% Computing Scan Loss and Antenna Gain under the condition of less than or equal to Maximum Allowable Scan
	% Otherwise Antenna Gain = -999999.9 dB

	if el <= maxScan

      % When not using a hardcoded max gain, only compute antenna gain if operating frequency
	  % is less than design frequency that way the same error/null value (-999999.9 dB) is
	  % always returned from the plugin.

	    if gainModel == 0
	      scanLoss = cos(el);
	      scanLoss = 10.0*log(scanLoss)/log(10.0);
	      AntennaGain = gmax + scanLoss;

	    elseif gainModel == 1
	      if freq <= designFreq	
	        scanLoss = cos(el);
	        scanLoss = 10.0*log(scanLoss)/log(10.0);
	        AntennaGain = gmax + scanLoss;
	      end

	    else
	     % Placeholder for use with user-supplied maximum on-axis gain calculation.

        end

	end
	

	% Return of Outputs to STK
	% ------------------------

	% NOTE: Simple cosine loss model only returns AntennaGain and AntennaMaxGain
	% NOTE: Simple cosine loss model does not compute integrated gain and thus should only be used
	%       with a constant system noise temperature when used in Rx and Radar objects.

	% NOTE: All outputs MUST be returned and in the same order that they were registered.
	%       STK will interpret return values with the units shown below.

	% AntennaGain (dB), gain of the antenna at time and in the Azi-Elev direction off the boresight.
	% Beamwidth (Rad) is the 3-dB beamwidth of the antenna.
	% AntennaMaxGain (dB) is the maximum ( possibly boresight gain of the antenna)
	% IntegratedGain of the antenna (range 0-1) used for antenna Noise computation.
	% Dynamic Gain is a flag (value = 0 or 1) depending on whether the graphics get updated at each timestep.

        output.AntennaGain    = AntennaGain;
        output.Beamwidth      = 0;
        output.AntennaMaxGain = gmax;
        output.IntegratedGain = 0;
        output.AntennaCoordSystem = 0;
        output.DynamicGain = 0;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

