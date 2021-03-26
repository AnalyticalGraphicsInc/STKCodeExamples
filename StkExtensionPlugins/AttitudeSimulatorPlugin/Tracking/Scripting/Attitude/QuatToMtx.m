function dcm = QuatToMtx(q)

   q1Sq = q(1) * q(1);
   q1x2 = q(1) * q(2);
                        q1x3 = q(1) * q(3);
                        q1x4 = q(1) * q(4);
                        q2Sq = q(2) * q(2);
                        q2x3 = q(2) * q(3);
                        q2x4 = q(2) * q(4);
                        q3Sq = q(3) * q(3);
                        q3x4 = q(3) * q(4);
                        q4Sq = q(4) * q(4);

    dcm(1,1) = q1Sq - q2Sq - q3Sq + q4Sq;
    dcm(1,2) = 2.0 * (q1x2 + q3x4);
    dcm(1,3) = 2.0 * (q1x3 - q2x4);

    dcm(2,1) = 2.0 * (q1x2 - q3x4);
    dcm(2,2) = -q1Sq + q2Sq - q3Sq + q4Sq;
    dcm(2,3) = 2.0 * (q2x3 + q1x4);

    dcm(3,1) = 2.0 * (q1x3 + q2x4);
    dcm(3,2) = 2.0 * (q2x3 - q1x4);
    dcm(3,3) = -q1Sq - q2Sq + q3Sq + q4Sq;

 