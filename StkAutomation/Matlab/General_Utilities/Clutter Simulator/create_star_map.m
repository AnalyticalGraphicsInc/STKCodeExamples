function [star_map] = create_star_map(sz,star_positions,star_brightness_levels)
    star_map = zeros(sz) ;
    
    num_stars = length(star_brightness_levels) ;
    
    for i=1:num_stars
        x = round(star_positions(i,1)) ;
        y = round(star_positions(i,2)) ;
        
        if (x >= 1 && y >= 1 && x <= sz(2) && y <= sz(1))
            star_map(y,x) = star_map(y,x)+star_brightness_levels(i) ;
        end
    end
end