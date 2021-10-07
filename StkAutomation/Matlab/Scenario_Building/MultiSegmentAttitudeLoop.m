% Example Matlab code to add profiles to a Multi-Segment attitude type.
% This script allows the user to add multiple attitude segments to a
% satellite with KNOWN start times. 
%
% Attitude segment names, types and start times are set in the "Inputs"
% section, and segment specific data is set in the "Additional Segment
% Data" section. The "Additional Segment Data" section is broken out based
% on attitude profile type, and notes regarding available input types and
% values are included.
%
% The script loops through each attitude profile, gathers the appropriate
% profile data, builds the "Connect Command string" and adds the attitude
% segments using the AddAttitude Connect Command.
%
% The current script takes an example satellite "MySatellite" and adds 10
% attitude segments of various types.

clc; clear all; format long g; format compact

%% Inputs

    % Set satellite name:
    satelliteName = 'MySatellite';
    
    % Set desired time unit for attitude segment. This could be 'UTCG',
    % 'EpSec', 'GPS', 'YYDDD', etc.
    dateUnit = 'EpSec';
    
    % Set attitude profile names and types. Available profile types are
    % listed at: http://help.agi.com/stkdevkit/#../Subsystems/connectCmds/Content/cmd_SetAttitudeProfile.htm
    
    profileNames = {      'NadirSun', 'AlignConstrain', 'AlignConstrain2', 'Spinning', 'SpinNadir', ...
                    'PrecessingSpin',       'InertFix',          'Fixed1',   'Fixed2',       'GPS'};
                
    profileTypes = {      'NadirSun', 'AlignConstrain', 'AlignConstrain', 'Spinning', 'SpinNadir', ...
                    'PrecessingSpin',       'InertFix',          'Fixed',    'Fixed',       'GPS'};
    
    % Set start times for attitude segments as strings, in desired units:
    startTimes = {'100', '200', '300', '400', '500', '600', '700', '800', '900', '1000'};
    
%% Additional Segment Data

    % ---------------------------------------------------------------------
    % Data for INSTALLED "Aligned and Constrained" attitude profiles:
    
        % Installed profiles include:
        %
        % NadirECIVel,  NadirECFVel,      NadirSun,        NadirOrbit,  ECIVelNadir,
        % ECFVelNadir, ECFVelRadial, NorthEastDown,          SunNadir,  SunEcliptic,
        %     SunECIZ,    SunOccult,     CBIVelSun, SunPointingZOrbit, XPOPInertial.
        %
        % These profiles require a single offset angle as input.

        % Offset angles:
        offsetAngles = {'10'};

    % ---------------------------------------------------------------------
    % Data for CUSTOM "Aligned and Constrained" attitude profiles:
    
        % Ref types/values for "Aligned and Constrained" profiles can be:
        %
        %    Axis  / <x> <y> <z>
        %    Euler / <angle 1> <angle 2> <sequence> (12, 21, 31, 32)
        %    PR    / <pitch> <roll>
        %    RaDec / <raan> <dec>

        % Aligned vector data:
          alignVectors = {'Nadir(Centric)', 'CentralBody/Earth Moon'};
         alignRefTypes = {          'Axis',                  'Euler'};
        alignRefValues = {         '0 0 1',             '1.2 2.5 31'};

        % Constrained vector data:
          constrainVectors = {'Orbit_Normal', 'Orbit_Normal'};
         constrainRefTypes = {          'PR',        'RaDec'};
        constrainRefValues = {       '15 20',        '11 22'};

    % ---------------------------------------------------------------------
    % Data for "Fixed" and "Inertially Fixed" attitude profiles:

        % Ref types/values for "Fixed" and "Inertially Fixed" profiles can be:
        %
        %    YPR   / <yaw> <pitch> <roll> <sequence> (RPY, RYP, PRY, PYR, YRP, YPR)
        %    Quat  / <q1> <q2> <q3> <q4>
        %    Euler / <angle 1> <angle 2> <angle 3> <sequence>
        %            (121, 123, 131, 132, 212, 213, 231, 232, 312, 313, 321, 323)
        
        % Fixed data:
        fixedRefFrames = {'CentralBody/Earth Inertial', 'Satellite/MySatellite LVLH'};
         fixedRefTypes = {                       'YPR',                      'Euler'};
        fixedRefValues = {              '10 20 30 YPR',               '15 20 25 321'};

        % Inertial data:
         inertialRefTypes = {   'Quat'};
        inertialRefValues = {'0 0 0 1'};

    % ---------------------------------------------------------------------
    % Data for "Spin about nadir" and "Spin about Sun vector" attitude profiles:

        % Spin rates are in revs/min, offsets are in deg.
        %
        % Spin epochs are set in above units. The reference axes are
        % computed at the spin epoch and are used as the basis for the
        % spin. The spin epoch is important if the reference frame/vector
        % rotates in time (e.g. LVLH, Body frame, Nadir vector, etc.):

        % Nadir data:
          nadirSpinRates = {'0.1'};
        nadirSpinOffsets = { '15'};
         nadirSpinEpochs = {'500'};

        % Sun data:
        %   sunSpinRates = {'0.1',  '0.2',  '0.5'};
        % sunSpinOffsets = {  '0',    '0',   '15'};
        %  sunSpinEpochs = {  '0', '1800', '3600'};

    % ---------------------------------------------------------------------
    % Data for "Spinning" and "Precessing Spin" attitude profiles:

        % Ref types/values for "Spinning" and "Precessing Spin" profiles can be:
        %
        %    Axis  / <x> <y> <z>
        %    Euler / <angle 1> <angle 2> <sequence> (12, 21, 31, 32)
        %    PR    / <pitch> <roll>
        %    RaDec / <raan> <dec>
        %
        % Spin rates are in revs/min, offsets are in deg.
        %
        % Spin epochs are set in above units. The reference axes are
        % computed at the spin epoch and are used as the basis for the
        % spin. The spin epoch is important if the reference frame/vector
        % rotates in time (e.g. LVLH, Body frame, Nadir vector, etc.):

        % Spinning data:

            % Spin data:
              spinningRates = {'0.1'};
            spinningOffsets = {  '1'};
             spinningEpochs = {'400'};

            % Pointing data:
                 spinningBodyTypes = { 'Axis'};
                spinningBodyValues = {'0 0 1'};
             spinningInertialTypes = {   'PR'};
            spinningInertialValues = {'15 20'};

        % Precessing spin data:

            % Spin/precession data:
                    precessingSpinRates = {'0.2'};
                  precessingSpinOffsets = {  '2'};
              precessingPrecessionRates = {'0.3'};
            precessingPrecessionOffsets = {  '3'};

            % Epochs/nutations:
                    precessingEpochs = {'600'};
            precessingNutationValues = { '30'};

            % Pointing data:
                   precessingBodyTypes = { 'Axis'};
                  precessingBodyValues = {'0 0 1'};
             precessingPrecessionTypes = {   'PR'};
            precessingPrecessionValues = {'15 20'};
            
            % Reference frames:
            precessingRefFrames = {'Satellite/MySatellite LVLH'};

    % ---------------------------------------------------------------------
    % Data for "GPS" attitude profiles:

        % GPS model types can be:
        %
        %   IIANominal, IIFNominal, IIRNominal, IIA, IIF, IIR, QZSS, GalileoIOV
        
        % Types:
        gpsTypes = {'IIRNominal'};

    % ---------------------------------------------------------------------
    % Data for "Yaw to nadir" attitude profiles:

        % Ref types/values for "Yaw to Nadir" profiles can be:
        %
        %    Axis  / <x> <y> <z>
        %    Euler / <angle 1> <angle 2> <sequence> (12, 21, 31, 32)
        %    PR    / <pitch> <roll>
        %    RaDec / <raan> <dec>
        
        % Pointing data:
         yawRefTypes = {'RaDec'};
        yawRefValues = {'15 20'};
            
%% Initialization

    % Get current app, root and scenario handles:
    GetCurrentScenario;

    % Get satellite handle:
    try
        satellite = scenario.Children.Item(satelliteName);
    catch
        satellite = scenario.Children.New('eSatellite','MySatellite');
    end

    % Change connect command date units to desired units:
    command = strcat(['Units_Set * Connect Date "',dateUnit,'"']);
    root.ExecuteCommand(command);
    
    % Set profile type flags to 1:
    alignConstrainIndex = 1;        installedIndex = 1;
             fixedIndex = 1;         inertialIndex = 1;
          spinningIndex = 1;   precessingSpinIndex = 1;
         spinNadirIndex = 1;          spinSunIndex = 1;
               gpsIndex = 1;         yawNadirIndex = 1;
              xpopIndex = 1;

%% Add Attitude Profiles
    
    % Loop over number of profiles:
    nProfile = numel(profileNames);
    for i = 1:nProfile
        
        % Get profile name, type, start time:
        profileName = profileNames{i};
        profileType = profileTypes{i};
          startTime = startTimes{i};
        
        % Start building command string:
        baseString = strcat(['AddAttitude */Satellite/',satelliteName,' Profile ',profileName,' "',startTime,'" ',profileType]);
        
        % Gather attitude data based on profile type:
        switch profileType
            
            % Custom "Aligned and Constrained" profile type:
            case 'AlignConstrain'
                
                % Set align/constrain index:
                index = alignConstrainIndex;

                % Aligned vector data:
                  alignVector = alignVectors{index};
                 alignRefType = alignRefTypes{index};
                alignRefValue = alignRefValues{index};
                  alignString = strcat([alignRefType,' ',alignRefValue,' "',alignVector,'"']);
                
                % Constrained vector data:
                  constrainVector = constrainVectors{index};
                 constrainRefType = constrainRefTypes{index};
                constrainRefValue = constrainRefValues{index};
                  constrainString = strcat([constrainRefType,' ',constrainRefValue,' "',constrainVector,'"']);
                
                % Build profile string:
                profileString = strcat([alignString,' ',constrainString]);
                
                % Increment align/constrain index by 1:
                alignConstrainIndex = alignConstrainIndex + 1;
                
            % Inertially fixed profile:
            case 'InertFix'

                % Set inertial index:
                index = inertialIndex;
                
                % Inertial data:
                 inertialRefType = inertialRefTypes{index};
                inertialRefValue = inertialRefValues{index};
                  inertialString = strcat([inertialRefType,' ',inertialRefValue]);                
                
                % Build profile string:
                profileString = inertialString;
                
                % Increment inertial index by 1:
                inertialIndex = inertialIndex + 1;
            
            % Fixed in axes profile:
            case 'Fixed'
                
                % Set fixed index:
                index = fixedIndex;
                
                % Fixed data:
                 fixedRefType = fixedRefTypes{index};
                fixedRefValue = fixedRefValues{index};
                fixedRefFrame = fixedRefFrames{index};
                  fixedString = strcat([fixedRefType,' ',fixedRefValue,' "',fixedRefFrame,'"']);                
                
                % Build profile string:
                profileString = fixedString;
                
                % Increment fixed index by 1:
                fixedIndex = fixedIndex + 1;
                
            % Spinning profile:
            case 'Spinning'
            
                % Set spinning index:
                index = spinningIndex;

                % Pointing data:
                      spinningBodyType = spinningBodyTypes{index};
                     spinningBodyValue = spinningBodyValues{index};
                  spinningInertialType = spinningInertialTypes{index};
                 spinningInertialValue = spinningInertialValues{index};
                spinningPointingString = strcat(['Inertial ',spinningInertialType,' ',spinningInertialValue,' ', ...
                                                     'Body ',    spinningBodyType,' ',    spinningBodyValue]);
                
                % Spin data:
                      spinningRate = spinningRates{index};
                    spinningOffset = spinningOffsets{index};
                     spinningEpoch = spinningEpochs{index};
                spinningSpinString = strcat([spinningRate,' ',spinningOffset,' "',spinningEpoch,'"']);
                
                % Build profile string:
                profileString = strcat([spinningPointingString,' ',spinningSpinString]);
                
                % Increment spinning index by 1:
                spinningIndex = spinningIndex + 1;
            
            % Precessing spin profile:
            case 'PrecessingSpin'    
            
                % Set precessing index:
                index = precessingSpinIndex;
                
                % Pointing data:
                       precessingBodyType = precessingBodyTypes{index};
                      precessingBodyValue = precessingBodyValues{index};
                 precessingPrecessionType = precessingPrecessionTypes{index};
                precessingPrecessionValue = precessingPrecessionValues{index};
                 precessingPointingString = strcat(['Inertial ',precessingPrecessionType,' ',precessingPrecessionValue,' ', ...
                                                        'Body ',      precessingBodyType,' ',      precessingBodyValue]);
                
                % Spin/precession data:
                        precessingSpinRate = precessingSpinRates{index};
                      precessingSpinOffset = precessingSpinOffsets{index};
                  precessingPrecessionRate = precessingPrecessionRates{index};
                precessingPrecessionOffset = precessingPrecessionOffsets{index};
                   precessingNutationValue = precessingNutationValues{index};
                      precessingSpinString = strcat([     precessingSpinRate,' ',precessingPrecessionRate,' ', ...
                                                     precessingNutationValue,' ',    precessingSpinOffset,' ',precessingPrecessionOffset]);

                % Auxiliary data:
                    precessingEpoch = precessingEpochs{index};
                 precessingRefFrame = precessingRefFrames{index};
                precessingAuxString = strcat(['"',precessingEpoch,'" "',precessingRefFrame,'"']);
                
                % Build profile string:
                profileString = strcat([precessingPointingString,' ',precessingSpinString,' ',precessingAuxString]);
                
                % Increment precessing index by  1:
                precessingSpinIndex = precessingSpinIndex + 1;
                
            % Spin about nadir profile:
            case 'SpinNadir'
                
                % Set spin nadir index:
                index = spinNadirIndex;
                
                % Spinning data:
                  nadirSpinRate = nadirSpinRates{index};
                nadirSpinOffset = nadirSpinOffsets{index};
                 nadirSpinEpoch = nadirSpinEpochs{index};
                nadirSpinString = strcat([nadirSpinRate,' ',nadirSpinOffset,' "',nadirSpinEpoch,'"']);
                
                % Build profile string:
                profileString = nadirSpinString;
                
                % Increment spin nadir index by 1:
                spinNadirIndex = spinNadirIndex + 1;
                
            % Spin about Sun vector profile:
            case 'SpinSun'
                
                % Set spin Sun index:
                index = spinSunIndex;

                % Spinning data:
                  sunSpinRate = nadirSpinRates{index};
                sunSpinOffset = nadirSpinOffsets{index};
                 sunSpinEpoch = nadirSpinEpochs{index};
                sunSpinString = strcat([sunSpinRate,' ',sunSpinOffset,' "',sunSpinEpoch,'"']);
                
                % Build profile string:
                profileString = sunSpinString;
                
                % Increment spin Sun index by 1:
                spinSunIndex = spinSunIndex + 1;
                
            % GPS profile:
            case 'GPS'
                
                % Set GPS index:
                index = gpsIndex;
                
                % Model type:
                  gpsType = gpsTypes{index};
                gpsString = strcat(['ModelType ',gpsType]);
                
                % Build profile string:
                profileString = gpsString;
            
                % Increament GPS index by 1:
                gpsIndex = gpsIndex + 1;
                
            % Yaw to nadir profile:
            case 'YawNadir'
                
                % Set yaw nadir index:
                index = yawNadirIndex;
                
                % Pointing data:
                 yawRefType = yawRefTypes{index};
                yawRefValue = yawRefValues{index};
                  yawString = strcat([yawRefType,' ',yawRefValue]);
                
                % Build profile string:
                profileString = yawString;
                
                % Increment yaw nadir index by 1:
                yawNadirIndex = yawNadirIndex + 1;
                
            % One of the 'installed' "Aligned and Constrained" profiles:
            otherwise
            
                % Set installed index:
                index = installedIndex;
                
                % Get offset angle:
                 offsetAngle = offsetAngles{index};
                offsetString = strcat(['Offset ',offsetAngle]);
                
                % Build profile string:
                profileString = offsetString;
                
                % Increment installed index by 1:
                installedIndex = installedIndex + 1;
        end
        
        % Build command string and execute command:
        commandString = strcat([baseString,' ',profileString]);
        root.ExecuteCommand(commandString);
    end

%% Subroutines

% Function to get current app, root, scenario handles. Assigns three
% variables to the Workspace: "app", "root", and "scenario".
%
% Example 1: Return STK application handle.
%
%   [app,~,~] = GetCurrentScenario;
%
% Example 2: Return STK application, root and scenario handles.
%
%   GetCurrentScenario;  |OR|  [app,root,scenario] = GetCurrentScenario;
%
function varargout = GetCurrentScenario

    % Get STK application, return root and scenario:
    app = actxGetRunningServer('STK12.application');
    root = app.Personality2;
    scenario = root.CurrentScenario;
    
    % No outputs requested:
    if nargout == 0
        
        % Assign as Matlab Workspace variables:
        assignin('base','app',app);
        assignin('base','root',root);
        assignin('base','scenario',scenario);
        
    % Outputs requested:
    elseif nargout == 3
        
        % Assign specific outputs:
        varargout{1} = app;
        varargout{2} = root;
        varargout{3} = scenario;
    end
end

