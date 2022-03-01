function [output] = Matlab_DynamicModulator_CustomPSD(input)
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

	% The type of modulation used by the transmitter is a String. This must be one of the STK registered modulation types. However,
	% users can add their own modulation types, and STK will register them (see online Help for the Comm module)( max 32 characters ).
        ModulationName = {'ArgumentName','ModulationName',...
                     'Name','ModulationName',...
                     'ArgumentType','Output'};
					 
	% SpectrumLimitLow is the Lower band limit used to compute a PSD over in Hz and relative to RF carrier frequency (-100GHz to 0) and is a double.
	   SpectrumLimitLow= {'ArgumentName','SpectrumLimitLow',...
                     'Name','SpectrumLimitLow',...
                     'ArgumentType','Output'};
					 
	% SpectrumLimitHi is the Upper band limit used to compute a PSD over in Hz and relative to RF carrier frequency (-100GHz to 0) and is a double.
	   SpectrumLimitHi= {'ArgumentName','SpectrumLimitHi',...
                     'Name','SpectrumLimitHi',...
                     'ArgumentType','Output'};
					 
	%  UsePSD is a flag (0 or 1) that specifies whether or not to enable the PSD is a Boolean.
		UsePSD= {'ArgumentName','UsePSD',...
                    'Name','UsePSD',...
                    'ArgumentType','Output'};  
					
	% NumPSDPoints is the Number of PSD points being returned (max 100,000) and is an integer.
       NumPSDPoints= {'ArgumentName','NumPSDPoints',...
                    'Name','NumPSDPoints',...
                    'ArgumentType','Output'};
					
	%  PSDData is a double array of Psd values [ NumPSDPoints x 1 ].
		PSDData= {'ArgumentName','PSDData',...
                    'Name','PSDData',...
                    'ArgumentType','Output'};  
 
	%	The FreqStepSize is the Frequency step size of Psd data, in Hz and is a double.
        FreqStepSize= {'ArgumentName','FreqStepSize',...
                    'Name','FreqStepSize',...
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
				
	% The RFCarrierFreq is a double in Hz.
	   RFCarrierFreq = {'ArgumentName','RFCarrierFreq',...
                'Name','RFCarrierFreq',...
                'ArgumentType','Input'};
				  
	% The object position in Latitude, Longitude, Altitude coordinates is a vector of doubles, of length 3, in radians, radians, meters.
        ObjectPosLLA = {'ArgumentName','ObjectPosLLA',...
               'Name','ObjectPosLLA',...
               'ArgumentType','Input'};
			   
	% The DataRate is a double in bits per second.
	   DataRate = {'ArgumentName','DataRate',...
                'Name','DataRate',...
                'ArgumentType','Input'}; 
   
        output = {IsDynamic, ModulationName, SpectrumLimitLow, SpectrumLimitHi,...
				  UsePSD, NumPSDPoints, PSDData, FreqStepSize,...  
                  date, CbName, ObjectPath, EpochSec, RFCarrierFreq, ObjectPosLLA, DataRate};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% Transmitter input & outpur parameter usage is shown
	% 

	% USER DynamicModulator_CustomPSD AREA.
        
	objLLA = [ 0.0 0.0 0.0 ];
	
	objLLA = computeData.ObjectPosLLA;
	time = computeData.EpochSec;
	DataRate = computeData.DataRate;
		
	% Initialize Output values
	% NOTE: Your doppler resolution will be limited to FreqStepSize, so be sure to 
	%        set NumPSDPts to achieve adequate doppler resolution.
	
	IsDynamic        = 1;
	ModulationName   = 'BPSK';   % Only used by STK for proper demodulation
	modulationEfficiency = 2.0;
	codeRate         = 1.0;
    chipsPerBit      = 1;
	SpectrumLimitLow = -(DataRate * modulationEfficiency / codeRate * chipsPerBit)/2.0;
	SpectrumLimitHi  = -SpectrumLimitLow;
    UsePSD           = 1;
    NumPSDPoints     = 50001; %NumPSDPoints is odd to account for a center frequency value
	
	FreqStepSize     = (SpectrumLimitHi - SpectrumLimitLow)/NumPSDPoints;
	
	waveformSelector = mod(floor(time),4);
	
	% First waveform is...
	% DataRate Hz wide w/ peak magnitude of 0dB at carrier frequency and
    % -30 dB down at edges.
	if ( waveformSelector == 0)
		for I = 1:1:NumPSDPoints/2+1
			PsdData(I) = -30 + (I-1)*(30/NumPSDPoints*2);
			PsdData(NumPSDPoints-I+1) = PsdData(I);
		end
    % Second waveform is...
	% DataRate Hz wide w/ peak magnitude of 0dB at carrier frequency and
    % -10 dB down at edges.
	elseif ( waveformSelector == 1)
		for I = 1:1:NumPSDPoints/2+1
			PsdData(I) = -10 + (I-1)*(10/NumPSDPoints*2);
			PsdData(NumPSDPoints-I+1) = PsdData(I);
		end
    % Third waveform is...
	% similar to second waveform but is 4 times more bandwidth efficient
	elseif (waveformSelector == 2)
	    modulationEfficiency = 0.25;
    	SpectrumLimitLow = -(DataRate * modulationEfficiency / codeRate * chipsPerBit)/2.0;
	    SpectrumLimitHi  = -SpectrumLimitLow;
	    FreqStepSize     = (SpectrumLimitHi - SpectrumLimitLow)/NumPSDPoints;
	
		for I = 1:1:NumPSDPoints/2+1
			PsdData(I) = -10 + (I-1)*(10/NumPSDPoints*2);
			PsdData(NumPSDPoints-I+1) = PsdData(I);
		end
    % Fourth waveform is...
	% similar to third waveform but 2x wider than 3rd
	else
	    modulationEfficiency = 0.25;
		chipsPerBit = 2;
    	SpectrumLimitLow = -(DataRate * modulationEfficiency / codeRate * chipsPerBit)/2.0;
	    SpectrumLimitHi  = -SpectrumLimitLow;
	    FreqStepSize     = (SpectrumLimitHi - SpectrumLimitLow)/NumPSDPoints;
	
		for I = 1:1:NumPSDPoints/2+1
			PsdData(I) = -10 + (I-1)*(10/NumPSDPoints*2);
			PsdData(NumPSDPoints-I+1) = PsdData(I);
		end
	end
	
    output.IsDynamic = IsDynamic;
    output.ModulationName = ModulationName;
	output.SpectrumLimitLow = SpectrumLimitLow;
	output.SpectrumLimitHi = SpectrumLimitHi;
	output.UsePSD = UsePSD;
	output.NumPSDPoints  = NumPSDPoints;
	for I = 1:1:NumPSDPoints
	output.PSDData(I) = PsdData(I);
	end
    output.FreqStepSize = FreqStepSize;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

