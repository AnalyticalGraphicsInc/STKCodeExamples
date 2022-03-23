% NOTE: for this example to work, this m-file must be on your Matlab path
%
% Use SetPath in Matlab to set the path or copy this m-file to your m-file
% working area

function [yaw, pitch] = example1AttCtrl(deltaT, argOfLat)

% declare some variables that were put into the global workspace by the plugin
global attCtrlY0 attCtrlY1 attCtrlY2 attCtrlYS attCtrlYC;
global attCtrlP0 attCtrlP1 attCtrlP2 attCtrlPS attCtrlPC;

% Compute 			

yaw = attCtrlY0 + attCtrlY1 * deltaT + attCtrlY2 * deltaT^2;
yaw  = yaw + attCtrlYS * sin(argOfLat) + attCtrlYC * cos(argOfLat);
			
pitch = attCtrlP0 + attCtrlP1 * deltaT + attCtrlP2 * deltaT^2; 
pitch = pitch + attCtrlPS * sin(argOfLat) + attCtrlPC * cos(argOfLat);