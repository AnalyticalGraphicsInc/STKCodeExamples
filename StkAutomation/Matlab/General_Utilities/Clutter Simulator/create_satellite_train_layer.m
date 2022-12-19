function [satellite_positions] = create_satellite_train_layer(num_satellites,unit_velocity_vector,spacing,std_dev_along,std_dev_across,display)
    if nargin < 6
        display = 1;
    end
    
    magnitude_squared = unit_velocity_vector*unit_velocity_vector' ;
    if magnitude_squared ~= 1.0
        unit_velocity_vector = unit_velocity_vector/sqrt(magnitude_squared) ;
    end
    
    %std_dev_along = sqrt(along_variability) ;
    %std_dev_across = sqrt(across_variability) ;
    
    unit_across_velocity_vector = [-1*unit_velocity_vector(2),unit_velocity_vector(1)] ;
    
    if 0
        figure(1) ;
        for i=1:num_satellites
            sample_pos = unit_velocity_vector*(randn()*std_dev_along+spacing*i)+randn(1,2).*std_dev_across.*unit_across_velocity_vector ;

            plot(sample_pos(1),sample_pos(2),'*') ;

            if (i == 1)
                hold on ;
            end
        end
        hold off ;
    else
        count_list = [1:num_satellites]' ;
        true_positions = count_list*unit_velocity_vector*spacing ;
        along_offsets = randn(num_satellites,1)*unit_velocity_vector*std_dev_along ;
        across_offsets = randn(num_satellites,1)*unit_across_velocity_vector*std_dev_across ;

        satellite_positions = true_positions + along_offsets + across_offsets ;
        
        if display
            figure(1) ;
            plot(satellite_positions(:,1),satellite_positions(:,2),'*') ;
            hold on ;
            plot(true_positions(:,1),true_positions(:,2),'-x') ;
            hold off ;
            axis tight equal ;
        end
    end
end