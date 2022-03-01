function [output] = Matlab_AntMultiBeamSelStrat(input)
	% NOTE: the outputs that are returned
	%       MUST be in the same order as registered

	% NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
	% 
	% This example shows static output parameters, but these parameters
      % may change value at each time step.

switch input.method
    
    case 'register'
 	% register output variables

        BeamNumber = {'ArgumentName','BeamNumber',...
                     'Name','BeamNumber',...
                     'ArgumentType','Output'};

 	% register input variables       
   
        DateUTC= {'ArgumentName','DateUTC',...
                'Name','DateUTC',...
                'ArgumentType','Input'};
 
        EpochSec= {'ArgumentName','EpochSec',...
                   'Name','EpochSec',...
                   'ArgumentType','Input'};
                  
        CbName = {'ArgumentName','CbName',...
                  'Name','CbName',...
                  'ArgumentType','Input'};

        AntennaPosLLA = {'ArgumentName','AntennaPosLLA',...
               'Name','AntennaPosLLA',...
               'ArgumentType','Input'};

        NumberOfBeams = {'ArgumentName','NumberOfBeams',...
               'Name','NumberOfBeams',...
               'ArgumentType','Input'};

        BeamIDsArray = {'ArgumentName','BeamIDsArray',...
               'Name','BeamIDsArray',...
               'ArgumentType','Input'};

        Frequency = {'ArgumentName','Frequency',...
               'Name','Frequency',...
               'ArgumentType','Input'};
 
        Power = {'ArgumentName','Power',...
               'Name','Power',...
               'ArgumentType','Input'};
   
        IsActive = {'ArgumentName','IsActive',...
                    'Name','IsActive',...
                    'ArgumentType','Input'};
   
        output = {BeamNumber, DateUTC, EpochSec, CbName, AntennaPosLLA, NumberOfBeams, ...
                  BeamIDsArray, Frequency, Power, IsActive};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% Receiver input & output parameter usage is shown
	% 

	% USER Receiver MODEL AREA.
        
	numBeams =    computeData.NumberOfBeams;
        epochSec =    computeData.EpochSec;
	  antPos  = computeData.AntennaPosLLA;
	  beamIDs = computeData.BeamIDsArray;
        freqs   = computeData.Frequency;
        powers  = computeData.Power;
        activeFlags = computeData.IsActive;

        %return the first active beam found
        output.BeamNumber = 1;
	for i = 1:numBeams
	   if activeFlags(i) == 1
              output.BeamNumber= i;
              break;
           end
        end

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

