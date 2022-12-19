function [psf] = generate_2d_gaussian_psf(std_dev,x,y)
    if nargin < 2
        x = [-10:1/8:10] ;
    end
    
    if nargin < 3
        y = [-10:1/8:10] ;
    end
    
    [mx,my] = meshgrid(x,y) ;
    
    if std_dev > 0
        psf = exp(-(mx.^2 + my.^2)/(2*std_dev^2)) ;
    else
        dist_squared = mx.^2+my.^2 ;
        psf = double(dist_squared==min(dist_squared)) ;
    end
    
    psf = psf/sum(psf(:)) ;
end