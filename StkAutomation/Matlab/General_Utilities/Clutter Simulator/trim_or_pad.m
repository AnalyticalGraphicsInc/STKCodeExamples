function [output] = trim_or_pad(input,new_sz, method)
    sz = size(input) ;
    output = zeros(new_sz) ;
    
    offset = floor((new_sz-sz)/2) ;
    
    output_start = max(offset,0) ;
    output_stop = min(new_sz,offset+sz) ;
    
    for i=output_start(1):output_stop(1)-1
        for j=output_start(2):output_stop(2)-1
            output(i+1,j+1) = input(i-offset(1)+1,j-offset(2)+1) ;
        end
    end
    
    replicate = 0 ;
    wrap = 0 ;
    mirror = 0 ;
    if (nargin < 3)
        method = 0 ;
    elseif method == 1
        replicate = 1 ;
    elseif method == 2
        wrap = 1 ;
    elseif method == 3
        mirror = 1 ;
    end
    
    if (new_sz(1) > sz(1))
        % Pad along the top: first dimension (rows)
        for i=0:offset(1)-1
            for j=output_start(2):output_stop(2)-1
                if replicate
                    % Replicate the first valid entry
                    output(i+1,j+1) = input(0+1,j-offset(2)+1) ;
                elseif wrap || mirror
                    wrap_i = i-offset(1) ;
                    wrap_i_count = 0 ;
                    while wrap_i < 0
                        wrap_i = wrap_i+sz(1) ;
                        wrap_i_count = wrap_i_count+1 ;
                    end
                    if mirror
                        if mod(wrap_i_count,2)
                            wrap_i = sz(1)-1-wrap_i ;
                        end
                    end
                    output(i+1,j+1) = input(wrap_i+1,j-offset(2)+1) ;
                end     
            end
        end
        
        % Pad along the bottom: first dimension (rows)
        for i=output_stop(1):new_sz(1)-1
            for j=output_start(2):output_stop(2)-1
                if replicate
                    % Replicate the last valid entry
                    output(i+1,j+1) = input(sz(1)-1+1,j-offset(2)+1) ;
                elseif wrap || mirror
                    wrap_i = i-offset(1) ;
                    wrap_i_count = 0 ;
                    while wrap_i >= sz(1)
                        wrap_i = wrap_i-sz(1) ;
                        wrap_i_count = wrap_i_count+1 ;
                    end
                    if mirror
                        if mod(wrap_i_count,2)
                            wrap_i = sz(1)-1-wrap_i ;
                        end
                    end
                    output(i+1,j+1) = input(wrap_i+1,j-offset(2)+1) ;
                end
            end
        end
    end
    
    if (new_sz(2) > sz(2))
        % Pad along the left: second dimension (columns)
        for i=0:new_sz(1)-1
            for j=0:offset(2)-1
                if replicate
                    % Replicate the first valid entry
                    if (i-offset(1) < 0)
                        output(i+1,j+1) = input(0+1,0+1) ;
                    elseif (i-offset(1) < sz(1))
                        output(i+1,j+1) = input(i-offset(1)+1,0+1) ;
                    else
                        output(i+1,j+1) = input(sz(1)-1+1,0+1) ;
                    end
                elseif wrap || mirror
                    wrap_i = i-offset(1) ;
                    wrap_i_count = 0 ;
                    wrap_j_count = 0 ;
                    while wrap_i >= sz(1) ;
                        wrap_i = wrap_i-sz(1) ;
                        wrap_i_count = wrap_i_count+1 ;
                    end
                    while wrap_i < 0;
                        wrap_i = wrap_i+sz(1) ;
                        wrap_i_count = wrap_i_count+1 ;
                    end
                    
                    wrap_j = j-offset(2) ;
                    while wrap_j < 0
                        wrap_j = wrap_j+sz(2) ;
                        wrap_j_count = wrap_j_count+1 ;
                    end
                    
                    if mirror
                        if mod(wrap_i_count,2)
                            wrap_i = sz(1)-1-wrap_i ;
                        end
                        if mod(wrap_j_count,2)
                            wrap_j = sz(2)-1-wrap_j ;
                        end
                    end
                    
                    output(i+1,j+1) = input(wrap_i+1,wrap_j+1) ;
                end
            end
        end
        
        % Pad along the right: second dimension (columns)
        for i=0:new_sz(1)-1
            for j=output_stop(2):new_sz(2)-1
                if replicate
                    % Replicate the last valid entry
                    if (i-offset(1) < 0)
                        output(i+1,j+1) = input(0+1,sz(2)-1+1) ;
                    elseif (i-offset(1) < sz(1))
                        output(i+1,j+1) = input(i-offset(1)+1,sz(2)-1+1) ;
                    else
                        output(i+1,j+1) = input(sz(1)-1+1,sz(2)-1+1) ;
                    end
                elseif wrap || mirror
                    wrap_i = i-offset(1) ;
                    wrap_i_count = 0 ;
                    wrap_j_count = 0 ;
                    while wrap_i >= sz(1) ;
                        wrap_i = wrap_i-sz(1) ;
                        wrap_i_count = wrap_i_count+1 ;
                    end
                    while wrap_i < 0;
                        wrap_i = wrap_i+sz(1) ;
                        wrap_i_count = wrap_i_count+1 ;
                    end
                    
                    wrap_j = j-offset(2) ;
                    while wrap_j >= sz(2)
                        wrap_j = wrap_j-sz(2) ;
                        wrap_j_count = wrap_j_count+1 ;
                    end
                    
                    if mirror
                        if mod(wrap_i_count,2)
                            wrap_i = sz(1)-1-wrap_i ;
                        end
                        if mod(wrap_j_count,2)
                            wrap_j = sz(2)-1-wrap_j ;
                        end
                    end
                    
                    output(i+1,j+1) = input(wrap_i+1,wrap_j+1) ;
                end
            end
        end
    end
end