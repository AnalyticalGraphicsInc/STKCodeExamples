function [map,x,y] = draw_antialias_line(x1,x2,y1,y2,x_range,y_range)
    min_x = floor(min(x1,x2)) ;
    max_x = ceil(max(x1,x2)) ;
    min_y = floor(min(y1,y2)) ;
    max_y = ceil(max(y1,y2)) ;
    
    total_len = sqrt((x1-x2)^2+(y1-y2)^2) ;
    
    if nargin >= 6
        [x,y] = meshgrid([x_range(1):x_range(2)],[y_range(1):y_range(2)]) ;
    else
        [x,y] = meshgrid([min_x:max_x],[min_y:max_y]) ;
    end
    sz = size(x) ;
    map = zeros(sz) ;
    
    if abs(x1-x2) < abs(y1-y2)
        steep = 1 ;
        
        t1 = x1 ;
        t2 = x2 ;
        x1 = y1 ;
        x2 = y2 ;
        y1 = t1 ;
        y2 = t2 ;
        
        t = min_x ;
        min_x = min_y ;
        min_y = t ;
        
        t = max_x ;
        max_x = max_y ;
        max_y = t ;
        
        t = x ;
        x = y ;
        y = t ;
    else
        steep = 0 ;
    end
    
    if x1 > x2
        t1 = x1 ;
        x1 = x2 ;
        x2 = t1 ;
        t1 = y1 ;
        y1 = y2 ;
        y2 = t1 ;
    end
    
    if (x2-x1) == 0
        m = 0 ;
    else
        m = (y2-y1)/(x2-x1) ;
    end
    
    for x_ind = max(min_x,min(x(:))):min(max_x,max(x(:)))
        if x_ind < x1
            x_start = x1 ;
        else
            x_start = x_ind ;
        end
        
        y_start = m*(x_start-x1)+y1 ;
        
        if x_ind > x2
            x_stop = x_start ;
        elseif x_ind+1 > x2
            x_stop = x2 ;
        else
            x_stop = x_ind+1 ;
        end
        
        y_stop = m*(x_stop-x1)+y1 ;
        
        if y_start <= y_stop
            inc = 1 ;
            y_ind_start = max(floor(y_start),min(y(:))) ;
            y_ind_stop = min(ceil(y_stop),max(y(:))) ;
        else
            inc = -1 ;
            y_ind_start = min(ceil(y_start),max(y(:))) ;
            y_ind_stop = max(floor(y_stop),min(y(:))) ;
        end
        
        if y_ind_start == y_ind_stop
            y_ind_stop = y_ind_stop + inc ;
        end
        
        for y_ind = y_ind_start:inc:y_ind_stop-inc
            if (inc > 0 && y_ind < y_start) || (inc < 0 && y_ind > y_start)
                y_box_start = y_start ;
            else
                y_box_start = y_ind ;
            end
            
            if (inc > 0 && y_ind+1 > y_stop)||(inc < 0 && y_ind-1 < y_stop)
                y_box_stop = y_stop ;
            else
                y_box_stop = y_ind+inc ;
            end
            
            if m == 0
                x_box_start = x_start ;
                x_box_stop = x_stop ;
            else
                x_box_start = (y_box_start-y1)/m+x1 ;
                x_box_stop = (y_box_stop-y1)/m+x1 ;
            end
            
            % Calculate the length within this y box
            len = sqrt((x_box_start-x_box_stop)^2+...
                (y_box_start-y_box_stop)^2) ;
            
            if total_len == 0
                map(x==x_ind & y==y_ind) = 1 ;
            else
                map(x==x_ind & y==y_ind) = len/total_len ;
            end
        end
    end
end