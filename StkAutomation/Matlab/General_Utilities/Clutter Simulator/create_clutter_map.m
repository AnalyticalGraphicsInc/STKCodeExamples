function [clutter_map] = create_clutter_map(sz,clutter_positions,clutter_brightness_levels, clutter_motion_vector)
    pad_x = ceil(abs(clutter_motion_vector(1))) ;
    pad_y = ceil(abs(clutter_motion_vector(2))) ;
    
    padded_sz = [sz(1)+pad_y*2,sz(2)+pad_x*2] ;
    
    padded_clutter_map = zeros(padded_sz) ;
    
    sz_clutter_pos = size(clutter_positions) ;
    num_clutter_objs = sz_clutter_pos(1) ;
    
    apply_psf = 0 ;
    
    for i=1:num_clutter_objs
        x = round(clutter_positions(i,1))+pad_x ;
        y = round(clutter_positions(i,2))+pad_y ;
        
        if (x >= 1 && y >= 1 && x <= padded_sz(2) && y <= padded_sz(1))
            apply_psf = 1 ;
            if length(clutter_brightness_levels) > 1
                padded_clutter_map(y,x) = padded_clutter_map(y,x)+clutter_brightness_levels(i) ;
            else
                padded_clutter_map(y,x) = padded_clutter_map(y,x)+clutter_brightness_levels ;
            end
        end
    end
    
    % Check if any of the clutter objects need to be applied
    if apply_psf
        x_range = [-pad_x,pad_x] ;
        y_range = [-pad_y,pad_y] ;
        clutter_psf = draw_antialias_line(0,clutter_motion_vector(1),0,clutter_motion_vector(2),x_range,y_range) ;
        
        padded_clutter_map = conv2(padded_clutter_map,clutter_psf,'same') ;
    end
    
    % Trim the paddded clutter map to capture the relevant edge effects
    clutter_map = trim_or_pad(padded_clutter_map,sz) ;
end