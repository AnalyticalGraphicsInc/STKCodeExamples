function [output] = Matlab_RadarCrossSection(input)
        % NOTE: the outputs that are returned
        %       MUST be in the same order as registered

        % NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
        % 
        % This example shows static output parameters, but these parameters
        % may change value at each time step.

switch input.method
    
    case 'register'
        % register output variables

        RCSMatrixReal00    = {'ArgumentType', 'Output', 'Name', 'RCSMatrixReal00', 'ArgumentName', 'RCSMatrixReal00'}
        RCSMatrixImg00     = {'ArgumentType', 'Output', 'Name', 'RCSMatrixImg00', 'ArgumentName', 'RCSMatrixImg00'}
        RCSMatrixReal01    = {'ArgumentType', 'Output', 'Name', 'RCSMatrixReal01', 'ArgumentName', 'RCSMatrixReal01'}
        RCSMatrixImg01     = {'ArgumentType', 'Output', 'Name', 'RCSMatrixImg01', 'ArgumentName', 'RCSMatrixImg01'}
        RCSMatrixReal10    = {'ArgumentType', 'Output', 'Name', 'RCSMatrixReal10', 'ArgumentName', 'RCSMatrixReal10'}
        RCSMatrixImg10     = {'ArgumentType', 'Output', 'Name', 'RCSMatrixImg10', 'ArgumentName', 'RCSMatrixImg10'}
        RCSMatrixReal11    = {'ArgumentType', 'Output', 'Name', 'RCSMatrixReal11', 'ArgumentName', 'RCSMatrixReal11'}
        RCSMatrixImg11     = {'ArgumentType', 'Output', 'Name', 'RCSMatrixImg11', 'ArgumentName', 'RCSMatrixImg11'}
        ScatterMatrixBasis = {'ArgumentType', 'Output', 'Name', 'ScatterMatrixBasis', 'ArgumentName', 'ScatterMatrixBasis'}
        IsDynamic =          {'ArgumentType', 'Output', 'Name', 'IsDynamic', 'ArgumentName', 'IsDynamic'}

    % register input variables   
        EpochSec                 = {'ArgumentType', 'Input', 'Name', 'EpochSec', 'ArgumentName', 'EpochSec'}
        Frequency                = {'ArgumentType', 'Input', 'Name', 'Frequency', 'ArgumentName', 'Frequency'}
        IncidentRho              = {'ArgumentType', 'Input', 'Name', 'IncidentRho', 'ArgumentName', 'IncidentRho'}
        IncidentTheta            = {'ArgumentType', 'Input', 'Name', 'IncidentTheta', 'ArgumentName', 'IncidentTheta'}
        ReflectedRho             = {'ArgumentType', 'Input', 'Name', 'ReflectedRho', 'ArgumentName', 'ReflectedRho'}
        ReflectedTheta           = {'ArgumentType', 'Input', 'Name', 'ReflectedTheta', 'ArgumentName', 'ReflectedTheta'}
        IncidentBodyFixedVector  = {'ArgumentType', 'Input', 'Name', 'IncidentBodyFixedVector', 'ArgumentName', 'IncidentBodyFixedVector'}
        ReflectedBodyFixedVector = {'ArgumentType', 'Input', 'Name', 'ReflectedBodyFixedVector', 'ArgumentName', 'ReflectedBodyFixedVector'}

        output = {RCSMatrixReal00, RCSMatrixImg00, RCSMatrixReal01, RCSMatrixImg01, ...
                  RCSMatrixReal10, RCSMatrixImg10, RCSMatrixReal11, RCSMatrixImg11, ...
                  ScatterMatrixBasis, IsDynamic, ...
                  EpochSec, Frequency, IncidentRho, IncidentTheta, ReflectedRho, ReflectedTheta, ...
                  IncidentBodyFixedVector, ReflectedBodyFixedVector};

    case 'compute'

        computeData = input.methodData;

        % compute the Test Model : 
        % Example Model for testing only
        % 

        % USER MODEL AREA.

           time = computeData.EpochSec;
           freq = computeData.Frequency;
           incRho = computeData.IncidentRho;
           incTheta = computeData.IncidentTheta;
           refRho = computeData.ReflectedRho;
           refTheta = computeData.ReflectedTheta;
           incBodyFixedVector = computeData.IncidentBodyFixedVector;
           refBodyFixedVector = computeData.ReflectedBodyFixedVector;

           output.RCSMatrixReal00 = 0.0;
           output.RCSMatrixImg00  = 0.0;
           output.RCSMatrixReal01 = 0.0;
           output.RCSMatrixImg01  = -1.0;
           output.RCSMatrixReal10 = 1.0;
           output.RCSMatrixImg10  = 0.0;
           output.RCSMatrixReal11 = 0.0;
           output.RCSMatrixImg11  = 0.0;
           output.ScatterMatrixBasis = 0;
           output.IsDynamic = 0;

        % END OF USER MODEL AREA

    otherwise
        output = [];
end
