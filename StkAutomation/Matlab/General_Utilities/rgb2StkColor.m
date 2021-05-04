function [ colorValue ] = rgb2StkColor( RGB )
%rgb2stkColor Converts a 1 by 3 R G B vector to the equivalent value expected 
%through the Matlab COM interface to STK
%
%   rgb2stkColor(V) returns a double value which represents the color value
%   expected by STK through the COM interface
%
%   Example
%       rgb2stkColor([14   255   255]) returns 16776974.
    if RGB(1) < 16
        R = ['0' dec2hex(RGB(1))];
    else
        R = dec2hex(RGB(1));
    end
    if RGB(2) < 16
        G = ['0' dec2hex(RGB(2))];
    else
        G = dec2hex(RGB(2));
    end
    if RGB(3) < 16
        B = ['0' dec2hex(RGB(3))];
    else
        B = dec2hex(RGB(3));
    end
    
    colorValue = hex2dec([B G R]);
end

