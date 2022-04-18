% NOTE: for this example to work, this m-file must be on your Matlab path
%
% Use SetPath in Matlab to set the path or copy this m-file to your m-file
% working area

function theta = example1Hpop(xSat, ySat, zSat, xVec, yVec, zVec)

% Compute angle between satellite position and vector

sat = [xSat; ySat; zSat];
vec = [xVec; yVec; zVec];

theta = sat'*vec;

theta = acos(theta / ( norm(sat) * norm(vec) ));
