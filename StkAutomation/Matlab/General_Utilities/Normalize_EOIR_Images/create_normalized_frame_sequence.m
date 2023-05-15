function [] = create_normalized_frame_sequence(input_folder,output_folder,normalize_to_first,colormap_to_use,scale_up_factor)
    if (nargin < 3)
        normalize_to_first = 1;
    end
    
    if (nargin < 4)
        colormap_to_use = colormap;
    end
    
    if (nargin < 5)
        scale_up_factor = 1;
    end
    
    files = dir(input_folder);
    num_files = length(files)-2;
    if (length(files) <= 2)
        disp('Error, no data files found.') ;
        return;
    end
    
    [~, reindex] = sort( str2double( regexp( {files.name}, '\d+', 'match', 'once' )));
    files = files(reindex) ;
    
    i = 1;
    file_name = sprintf('%s\\%s',files(i).folder,files(i).name);
    a = load(file_name);
    
    if (normalize_to_first || num_files == 1)
        min_value = min(a(:))
        max_value = max(a(:))
    else
        min_value = min(a(:))
        max_value = max(a(:))
        
        for i=1:num_files
            file_name = sprintf('%s\\%s',files(i).folder,files(i).name);
            a = load(file_name);
            
            temp_min_value = min(a(:));
            temp_max_value = max(a(:));
            
            if (temp_min_value < min_value)
                min_value = temp_min_value
            end
            
            if (temp_max_value > max_value)
                max_value = temp_max_value
            end
        end
    end
    
    %disp(min_value);
    %disp(max_value);
    
    %min_value = 0.0e+10; #manually setting min
    %max_value = 7.0e+10; #manually setting max
      
    disp(min_value);
    disp(max_value);

    full_value_range = max_value-min_value
    bounds(1) = min_value;%+0.001*full_value_range
    bounds(2) = max_value-0.1*full_value_range
    for i=1:num_files
        file_name = sprintf('%s\\%s',files(i).folder,files(i).name);
        disp(file_name)
        a = load(file_name);
        figure(2);
        subplot(1,2,1);
        imagesc(a);
        subplot(1,2,2);
        imagesc(a,bounds);
        axis tight equal off;
        output_file = sprintf('%s\\output_frame_%06d.jpg',output_folder,i-1)
        
        % Processing image here
        powerscale = .3;
        a(a<0) = 0;
        a = a.^powerscale;
               
        bounds(1) = 0;
        bounds(2) = max_value^powerscale;
        
        if (scale_up_factor == 1)
            save_colormapped_image(a, colormap_to_use, output_file, bounds);
        elseif (scale_up_factor > 1)
            save_colormapped_image(a(1:1/scale_up_factor:end,1:1/scale_up_factor:end), colormap_to_use, output_file, bounds);
        end
            
        % pause(0.1);
    end
end