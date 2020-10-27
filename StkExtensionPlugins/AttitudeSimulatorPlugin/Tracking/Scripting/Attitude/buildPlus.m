function WP = buildPlus(w)

w1 = w(1);
w2 = w(2);
w3 = w(3);

WP = [ w1  w2   w3   0   0   0
        0  w1   0    w2  w3  0
        0   0   w1   0   w2  w3 ];
    
