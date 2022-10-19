function [output] = Matlab_DynamicModulator_IdealPSD(input)
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
					 
	% ModulationEfficiency is a double in Hz/bits per sec.
        ModulationEfficiency= {'ArgumentName','ModulationEfficiency',...
                     'Name','ModulationEfficiency',...
                     'ArgumentType','Output'};
					 
	% CodeRate is the ratio of uncoded to coded bits (i.e. 1/2 Code Rate is 0.5) and is a double
       CodeRate= {'ArgumentName','CodeRate',...
                    'Name','CodeRate',...
                    'ArgumentType','Output'};
					
	% PSDShape is used to generate the Power Spectral Density if Use Signal PSD is enabled.
       PSDShape= {'ArgumentName','PSDShape',...
                    'Name','PSDShape',...
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
    
	%	The ChipsPerBit is a integer.
        ChipsPerBit= {'ArgumentName','ChipsPerBit',...
                    'Name','ChipsPerBit',...
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
   
        output = {IsDynamic, ModulationName, ModulationEfficiency, CodeRate, PSDShape,...
				  SpectrumLimitLow, SpectrumLimitHi, UsePSD, ChipsPerBit,...  
                  date, CbName, ObjectPath, EpochSec, RFCarrierFreq, ObjectPosLLA, DataRate};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% Transmitter input & outpur parameter usage is shown
	% 

	% USER DynamicModulator_IdealPSD AREA.
        
	objLLA = [ 0.0 0.0 0.0 ];
	
	objLLA = computeData.ObjectPosLLA;
	time = computeData.EpochSec;
	DataRate = computeData.DataRate;
		
	IsDynamic        = 1; 
	ModulationName   = 'BPSK'; % Only used by STK for proper demodulation
	PSDShape         = 'BPSK';
	ModulationEfficiency = 2.0; 
	CodeRate         = 1.0;
    ChipsPerBit      = 1.0;
	SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0;
	SpectrumLimitHi  = -SpectrumLimitLow;
    UsePSD           = 0;
	
	waveformSelector = mod(floor(time),4);
	
    % First waveform is...
    % QAM256
	if ( waveformSelector == 0)
		ModulationName   = 'QAM256';
		PSDShape         = 'QAM256';
		ModulationEfficiency = 0.25;
		CodeRate = 1.0;
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0;
		SpectrumLimitHi  = -SpectrumLimitLow;
    % Second waveform is...
    % QAM16
	elseif ( waveformSelector == 1)
		ModulationName   = 'QAM16';
		PSDShape         = 'QAM16';
		ModulationEfficiency = 0.5;
		CodeRate = 1.0;
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0;
		SpectrumLimitHi  = -SpectrumLimitLow;
    % Third waveform is...
    % 8PSK
	elseif (waveformSelector == 2)
		ModulationName   = '8PSK'; 
		PSDShape         = '8PSK';
		ModulationEfficiency =  0.6666666666;
		CodeRate = 1.0;
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0;
		SpectrumLimitHi  = -SpectrumLimitLow;
    % Fourth waveform is...
    % BPSK
	else
		ModulationName   = 'BPSK';
        PSDShape         = 'BPSK';
		ModulationEfficiency = 2.0;
		CodeRate = 1.0;
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0;
		SpectrumLimitHi  = -SpectrumLimitLow;
	end
	
    output.IsDynamic = IsDynamic;
    output.ModulationName = ModulationName;
	output.ModulationEfficiency = ModulationEfficiency;
	output.CodeRate = CodeRate;
	output.PSDShape = PSDShape;
	output.SpectrumLimitLow = SpectrumLimitLow;
	output.SpectrumLimitHi = SpectrumLimitHi;
	output.UsePSD = UsePSD;
    output.ChipsPerBit = ChipsPerBit;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

