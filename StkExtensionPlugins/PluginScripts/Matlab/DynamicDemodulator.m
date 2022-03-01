function [output] = Matlab_DynamicDemodulator(input)
	% NOTE: the outputs that are returned
	%       MUST be in the same order as registered

	% NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
	% 
	% This example shows static output parameters, but these parameters
      % may change value at each time step.

switch input.method
    
    case 'register'
 	% register output variables       
					 
	% OutBER is a double
       OutBER = {'ArgumentName','OutBER',...
                    'Name','OutBER',...
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
			   
	% The RFFreq is a double in Hz.
	   RFFreq = {'ArgumentName','RFFreq',...
                'Name','RFFreq',...
                'ArgumentType','Input'};
			   
	% The DataRate is a double in bits per second.
	   DataRate = {'ArgumentName','DataRate',...
                'Name','DataRate',...
                'ArgumentType','Input'}; 
				
	% SpectrumLimitLo is the Lower band limit used to compute a PSD over in Hz and 
	% relative to RF carrier frequency (-100GHz to 0) and is a double.
	   SpectrumLimitLo= {'ArgumentName','SpectrumLimitLo',...
                     'Name','SpectrumLimitLo',...
                     'ArgumentType','Input'};
					 
	% SpectrumLimitHi is the Upper band limit used to compute a PSD over in Hz and 
	% relative to RF carrier frequency (-100GHz to 0) and is a double.
	   SpectrumLimitHi= {'ArgumentName','SpectrumLimitHi',...
                     'Name','SpectrumLimitHi',...
                     'ArgumentType','Input'};
					 
	% The type of modulation used by the transmitter is a String. 
	% This must be one of the STK registered modulation types. However,
	% users can add their own modulation types, and STK will register them 
	%(see online Help for the Comm module)( max 32 characters ).
       SignalModulationName = {'ArgumentName','SignalModulationName',...
                     'Name','SignalModulationName',...
                     'ArgumentType','Input'};
					 
	% The SignalEbNo is a double in dB.
	   SignalEbNo = {'ArgumentName','SignalEbNo',...
                'Name','SignalEbNo',...
                'ArgumentType','Input'}; 
   
        output = {OutBER, date, CbName, ObjectPath, EpochSec, ObjectPosLLA,...
				  RFFreq, DataRate, SpectrumLimitLo, SpectrumLimitHi,...
				  SignalModulationName, SignalEbNo};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% Transmitter input & outpur parameter usage is shown
	% 

	% USER DynamicDemodulator AREA.
        
	objLLA = [ 0.0 0.0 0.0 ];
	
	objLLA = computeData.ObjectPosLLA;
	time = computeData.EpochSec;
	DataRate = computeData.DataRate;
	ModulationName = computeData.SignalModulationName;
	
	% Initialize Output values
	% NOTE: Your doppler resolution will be limited to FreqStepSize, so be sure to set number of 
	%       NumPSDPts to achieve adequate doppler resolution.
    OutBER           = 0.5;
	
	waveformSelector = mod(floor(time),3);
	
	if ( waveformSelector == 0)
		OutBER = 0.5;
	elseif ( waveformSelector == 1)
		OutBER = 1.1111111e-1;
	else
	     if (ModulationName == 'BPSK')
		     OutBER = 2.222222e-2;
	     elseif (ModulationName == 'OQPSK')
		     OutBER = 3.333333e-3;
	     elseif (ModulationName == 'MyOQPSK') 
		     OutBER = 4.444444e-4;
	     else
		     OutBER = 5.555555e-5;
	     end
    end
	
    output.OutBER = OutBER;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

