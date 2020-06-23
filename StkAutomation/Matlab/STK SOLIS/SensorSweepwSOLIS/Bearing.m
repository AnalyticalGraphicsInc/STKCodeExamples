function [b] = Bearing(lat1,lon1,lat2,lon2)
    lat1 = lat1*pi/180;
    lon1 = lon1*pi/180;
    lat2 = lat2*pi/180;
    lon2 = lon2*pi/180;
    b = atan2(sin(lon2-lon1)*cos(lat2),cos(lat1)*sin(lat2)-sin(lat1)*cos(lat2)*cos(lon2-lon1));
    b = b*180/pi;
end