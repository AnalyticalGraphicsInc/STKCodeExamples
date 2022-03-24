% NOTE: for this example to work, this m-file must be on your Matlab path
%
% Use SetPath in Matlab to set the path or copy this m-file to your m-file
% working area

function thrust = example1EngineModel(deltaT, argOfLat)

% declare some variables that were put into the global workspace by the plugin
global engMdlT0 engMdlT1 engMdlT2 engMdlTS engMdlTC;

thrust = engMdlT0 + engMdlT1 * deltaT + engMdlT2 * deltaT^2;
thrust = thrust + engMdlTS * sin(argOfLat) + engMdlTC * cos(argOfLat);