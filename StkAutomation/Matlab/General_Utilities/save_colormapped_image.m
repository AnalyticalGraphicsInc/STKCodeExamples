function [] = save_colormapped_image(image_data, c_map, image_file_to_write, bounds)
    if nargin < 4
        bounds(1) = min(image_data(:)) ;
        bounds(2) = max(image_data(:)) ;
    end
    
    ncolors = size(c_map, 1);
    scaled_image = (image_data-bounds(1))/(bounds(2)-bounds(1));
    mapped_image = uint8(scaled_image.*(ncolors-1));
    
    imwrite(mapped_image,c_map,image_file_to_write);
end