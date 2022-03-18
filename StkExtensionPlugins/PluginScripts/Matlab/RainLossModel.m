function [output] = Matlab_RainLossModel(input)
        % NOTE: the outputs that are returned
        %       MUST be in the same order as registered

        % Noise Temperature (K) is computed by STK from the rain loss.

switch input.method

    case 'register'
        % register output variables

        rainLoss= {'ArgumentName','RainLoss',...
                   'Name','RainLoss',...
                   'ArgumentType','Output'};

        % register input variables

        date = {'ArgumentName','DateUTC',...
                'Name','DateUTC',...
                'ArgumentType','Input'};

        frequency = {'ArgumentName','Frequency',...
                     'Name','Frequency',...
                     'ArgumentType','Input'};

        cbName = {'ArgumentName','CbName',...
                  'Name','CbName',...
                  'ArgumentType','Input'};

        elevAngle = {'ArgumentName','ElevAngle',...
                  'Name','ElevAngle',...
                  'ArgumentType','Input'};

        outagePercentage = {'ArgumentName','OutagePercentage',...
                            'Name','OutagePercentage',...
                            'ArgumentType','Input'};

        rcvrPosLLA = {'ArgumentName','RcvrPosLLA',...
                      'Name','RcvrPosLLA',...
                      'ArgumentType','Input'};

        xmtrPosLLA = {'ArgumentName','XmtrPosLLA',...
                      'Name','XmtrPosLLA',...
                      'ArgumentType','Input'};

        output = {rainLoss, date, cbName, frequency,...
                  elevAngle, outagePercentage, rcvrPosLLA, xmtrPosLLA};

    case 'compute'

        computeData = input.methodData;

        % Compute the Test Model
        % NOTE:  Loss should be returned as a positive dB value.

        % USER RAIN LOSS MODEL AREA.

        freq = computeData.Frequency;
        elev = computeData.ElevAngle;
        outPcnt = computeData.OutagePercentage;

        rcvrLLA = [ 0 0 0 ];
        xmtrLLA = [ 0 0 0 ];
        rcvrLLA = computeData.RcvrPosLLA;
        xmtrLLA = computeData.XmtrPosLLA;

        loss = 1/20;

        output.RainLoss= loss;

        % END OF USER MODEL AREA

    otherwise
        output = [];
end
