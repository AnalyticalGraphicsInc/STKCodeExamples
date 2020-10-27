function [epsec, x_pos, y_pos, z_pos, x_vel, y_vel, z_vel] = get_posvel_data(fid)

data_line = fgetl(fid);

spLine = regexp(data_line, '\s', 'split');

epsec = spLine{1};
x_pos = spLine{2};
y_pos = spLine{3};
z_pos = spLine{4};
x_vel = spLine{5};
y_vel = spLine{6};
z_vel = spLine{7};


 