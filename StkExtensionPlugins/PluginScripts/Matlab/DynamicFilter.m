function [output] = Matlab_DynamicFilter(input)
	% NOTE: the outputs that are returned
	%       MUST be in the same order as registered

	% NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
	% 
	% This example shows static output parameters, but these parameters
      % may change value at each time step.

switch input.method
    
    case 'register'
 	% register output variables       
	
	% IsDynamic indicates if script is time-dynamic and is an integer.
       IsDynamic = {'ArgumentName','IsDynamic',...
                     'Name','IsDynamic',...
                     'ArgumentType','Output'};
					 
	% LowerBandlimit is the Lower band limit used to compute a PSD over in Hz and relative to RF carrier frequency (-100GHz to 0) and is a double.
	   LowerBandlimit= {'ArgumentName','LowerBandlimit',...
                     'Name','LowerBandlimit',...
                     'ArgumentType','Output'};
					 
	% UpperBandlimit is the Upper band limit used to compute a PSD over in Hz and relative to RF carrier frequency (-100GHz to 0) and is a double.
	   UpperBandlimit= {'ArgumentName','UpperBandlimit',...
                     'Name','UpperBandlimit',...
                     'ArgumentType','Output'};

	% NumPoints is the Number of attenuation points being returned (max 100,000) and is an integer.
       NumPoints= {'ArgumentName','NumPoints',...
                    'Name','NumPoints',...
                    'ArgumentType','Output'};
 
	% The Attenuation values of the filter, in dB (0 to -3000 dB) and is a double array [NumPoints x 1].
        Attenuation= {'ArgumentName','Attenuation',...
                    'Name','Attenuation',...
                    'ArgumentType','Output'};

 	% register input variables       
   
    % The current date and time is a String variable. 
        date = {'ArgumentName','DateUTC',...
                'Name','DateUTC',...
                'ArgumentType','Input'};
 
    % The scenario central body is a String variable. 
	   CbName = {'ArgumentName','CbName',...
                  'Name','CbName',...
                  'ArgumentType','Input'};
				  
	% The ObjectPath is a String variable. 
	   ObjectPath  = {'ArgumentName','ObjectPath',...
                  'Name','ObjectPath',...
                  'ArgumentType','Input'};
				  
	% The scenario simulation epoch time is a double in seconds.
	   EpochSec = {'ArgumentName','EpochSec',...
                'Name','EpochSec',...
                'ArgumentType','Input'};
				  
	% The object position in Latitude, Longitude, Altitude coordinates is a vector of doubles, of length 3, in radians, radians, meters.
        ObjectPosLLA = {'ArgumentName','ObjectPosLLA',...
               'Name','ObjectPosLLA',...
               'ArgumentType','Input'};
			   
	% The CenterFreq is a double in Hz.
	   CenterFreq = {'ArgumentName','CenterFreq',...
                'Name','CenterFreq',...
                'ArgumentType','Input'};
			   
	% The FreqStepSize is a double in Hz.
	   FreqStepSize = {'ArgumentName','FreqStepSize',...
                'Name','FreqStepSize',...
                'ArgumentType','Input'}; 
   
        output = {IsDynamic, LowerBandlimit, UpperBandlimit,...
				  NumPoints, Attenuation,...  
                  date, CbName, ObjectPath, EpochSec, ObjectPosLLA, CenterFreq, FreqStepSize};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% Transmitter input & outpur parameter usage is shown
	% 

	% USER DynamicFilter AREA.
        
	objLLA = [ 0.0 0.0 0.0 ];
	
	objLLA = computeData.ObjectPosLLA;
	time = computeData.EpochSec;
	
	IsDynamic        = 1;
	%FreqStepSize = 10000;
	
	FreqStepSize = computeData.FreqStepSize;
	
	filterSelector = mod(floor(time),3);
	
	% First filter is...
	% 60 MHz wide w/ zero attenuation at center frequency
	% and -60 dB down at edges
	if(filterSelector == 0) 
	   LowerBandlimit = -30e6;
       UpperBandlimit = 30e6;
	% Second filter is...
	% 30 MHz wide w/ zero attenuation at center frequency
	% and -60 dB down at edges
	elseif (filterSelector == 1)
	   LowerBandlimit = -15e6;
       UpperBandlimit = 15e6;
	% Third filter is...
	% 10 MHz wide w/ zero attenuation at center frequency
	% and -60 dB down at edges
	elseif (filterSelector == 2)
	   LowerBandlimit = -5e6;
       UpperBandlimit = 5e6;
	end
	
	NumPoints = 1+(UpperBandlimit - LowerBandlimit)/FreqStepSize;
	
	for I = 1:1:(NumPoints-1)/2+1
	  attenDB = -60 + (I-1)*2*60/(NumPoints-1);
	  Attenuation(I) = attenDB;
	  Attenuation(NumPoints-I+1) = attenDB;
	end
	
    output.IsDynamic = IsDynamic;
	output.LowerBandlimit = LowerBandlimit;
	output.UpperBandlimit = UpperBandlimit;
	output.NumPoints  = NumPoints;
	for I = 1:1:NumPoints
	output.Attenuation(I) = Attenuation(I);
	end
	
	% END OF USER MODEL AREA

    otherwise
        output = [];
end

