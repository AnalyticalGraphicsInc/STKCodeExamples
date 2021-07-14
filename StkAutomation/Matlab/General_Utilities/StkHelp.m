% Function to launch programming documentation given an STK object/properties
% handle. Use can choose to launch online or offline documentation.
%
%      Inputs:
%              stkHandle - STK object or property handle
%              'offline' - Optional input to launch offline documentation
%
%     Outputs: None
%
%      Syntax: StkHelp(stkHandle)
%              StkHelp(stkHandle,'offline')
%
%   Example 1: Launch online documentation for Satellite > 2D Graphics > Attributes:
%              satellite - Handle to satellite
%              StkHelp(satellite.Graphics.Attributes);
%
%   Example 2: Launch offline documentation for Satellite > VGT > Calc Scalars:
%              satellite - Handle to satellite
%              calcScalars = satellite.Vgt.CalcScalars;
%              StkHelp(calcScalars,'offline');
%

function StkHelp(stkHandle, varargin)

    % Pull class, replace double-underscore with single-underscore:
    classString = replace(class(stkHandle),'__','_');
    
    % Handle STK application case:
    if strcmp(classString,'COM.STK12_application')
        
        urlLibrary = 'AgUiApplicationLib~';
        className = 'IAgUiApplication';
        
    % Other non-application cases:    
    else
    
        % Separate components of class string:
        classComponents = split(classString,'_');

        % Get class library and name:
        classLib = strcat(classComponents{2},'_',classComponents{3});
        className = classComponents{end};

        % Associate class library to URL component (incomplete, but has common libraries):
        switch classLib

            case 'STK_Objects'
                urlLibrary = 'STKObjects~';

            case 'STK_Astrogator'
                urlLibrary = 'AgSTKGatorLib~';

            case 'STK_VGT'
                urlLibrary = 'AgSTKVgtLib~';

            case 'STK_Util'
                urlLibrary = 'STKUtil~';

            case 'AGI_AgAccessConstraintPlugin'
                urlLibrary = 'AgAccessConstraintPlugin~';

            case 'STK_Graphics'
                urlLibrary = 'AgSTKGraphicsLib~';

            case 'Ui_Plugins'
                urlLibrary = 'AgUiPluginsLib~';

            case 'STK_X'
                urlLibrary = 'STKXLib~';

            case 'Ui_Application'
                urlLibrary = 'AgUiApplicationLib~';
                
            case 'Ui_Core'
                urlLibrary = 'AgUiCoreLib~';
        end 
    end
    
    % Using offline help:
    if nargin == 2 && strcmpi(varargin,'offline')
        
        % Get STK root handle:
        if strcmp(classString,'COM.STK12_application')
            root = stkHandle.Personality2;
        else
            root = stkHandle.Root;
        end
        
        % Get STK install directory:
        installDirectory = root.ExecuteCommand('GetSTKHomeDir /').Item(0);
        
        % Build URL base for offline help using the install directory:
        installDirectory = strrep(strrep(installDirectory,' ','%20'),'\','/');
        urlBase = ['file:///' installDirectory 'Help/Programming/Content/DocX/'];
        
    % Using online help:
    else
        
        % URL base uses online help:
        urlBase = 'http://help.agi.com/stkdevkit/#DocX/';
    end
    
    % Build URL string and launch in browser:
    urlString = strcat([urlBase, urlLibrary, className, '.html']);
    web(urlString,'-browser');
end