function WX = buildCross(w)

w1 = w(1);
w2 = w(2);
w3 = w(3);

WX = [ 0  -w3   w2
       w3  0   -w1
      -w2  w1   0 ];
    
