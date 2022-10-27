function [star_positions, star_brightness_levels] = create_star_info(sz, star_density, max_star_brightness)
    pixel_area = sz(1)*sz(2) ;
    num_stars = round(pixel_area*star_density) ;
    
    star_positions = rand(num_stars,2) ;
    star_positions(:,1) = star_positions(:,1)*sz(2) ;
    star_positions(:,2) = star_positions(:,2)*sz(1) ;
    
    star_brightness_levels = rand(num_stars,1) ;
    star_brightness_levels = star_brightness_levels-min(star_brightness_levels) ;
    star_brightness_levels = star_brightness_levels/max(star_brightness_levels) ;
    star_brightness_levels = (1 - star_brightness_levels).^2 ;
    star_brightness_levels = star_brightness_levels*max_star_brightness ;
end