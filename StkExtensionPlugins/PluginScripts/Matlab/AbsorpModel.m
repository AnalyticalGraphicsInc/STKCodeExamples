function [output] = Matlab_AbsorpModel(input)

% NOTE: The outputs that are returned MUST be in the same order as registered

switch input.method

    case 'register'

        % register output variables       
        absorpLoss= {'ArgumentName','AbsorpLoss',...
                     'Name','AbsorpLoss',...
                     'ArgumentType','Output'};

        noiseTemp= {'ArgumentName','NoiseTemp',...
                    'Name','NoiseTemp',...
                    'ArgumentType','Output'};

        %register input variables       

        date = {'ArgumentName','DateUTC',...
                'Name','DateUTC',...
                'ArgumentType','Input'};

        epochSec = {'ArgumentName','EpochSec',...
                    'Name','EpochSec',...
                    'ArgumentType','Input'};

        frequency = {'ArgumentName','Frequency',...
                     'Name','Frequency',...
                     'ArgumentType','Input'};

        cbName = {'ArgumentName','CbName',...
                  'Name','CbName',...
                  'ArgumentType','Input'};

        xmtrPath = {'ArgumentName','XmtrPath',...
                    'Name','XmtrPath',...
                    'ArgumentType','Input'};

        rcvrPath = {'ArgumentName','RcvrPath',...
                    'Name','RcvrPath',...
                    'ArgumentType','Input'};

        xmtrPosCBF = {'ArgumentName','XmtrPosCBF',...
                      'Name','XmtrPosCBF',...
                      'ArgumentType','Input'};

        rcvrPosCBF= {'ArgumentName','RcvrPosCBF',...
                     'Name','RcvrPosCBF',...
                     'ArgumentType','Input'};

        output = {absorpLoss, noiseTemp, date, epochSec, cbName, frequency,...
                  xmtrPath, rcvrPath, xmtrPosCBF, rcvrPosCBF};

    case 'compute'

        computeData = input.methodData;

        %Model for testing
        %Absorption Loss is about 10% of the free space loss (in dBs) and must be less than one.
        %NoiseTemp is the noise temprature in Kelvin.
        %
        %NOTE:  Return Loss is in Linear Scale, STK will convert to dBs

        %USER ABSORPTION LOSS MODEL AREA.

        freq = computeData.Frequency;
        time = computeData.EpochSec;

        xmtrPos = [ 0 0 0 ];
        rcvrPos = [ 0 0 0 ];
        xmtrPos = computeData.XmtrPosCBF;
        rcvrPos = computeData.RcvrPosCBF;

        range = norm(xmtrPos  - rcvrPos);

        freeSpace = (4 * 3.141592 * range * freq) / 299792458.0
        loss = 10^(log10(freeSpace * freeSpace)/10)

        output.AbsorpLoss= 1.0/loss;
        output.NoiseTemp= 273.15 * (1 - 1.0/loss);

        %END OF USER MODEL AREA

    otherwise
        output = [];
end

