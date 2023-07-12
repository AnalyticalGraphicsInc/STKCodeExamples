# LKtoFFDConverter

This folder contains two scripts that were used in a case where a customer had two LK files to represent an antenna on STK: there was one LK file representing magnitude, and one LK file representing phase, but STK will only accept a single LK file. The customer wanted the antenna on STK to take into account both the magnitude and phase file. As a result, I wrote a converter to take in two LK files representing magnitude and phase, and combines them into a single FFD file that will allow such an antenna on STK to consider both. I had to use some math to convert it within the script, but I built in functionality such that you can specify the type of polarization for the resulting FFD file.

In addition, since we were unsure of how the customer was able to obtain such LK files, and we did not have access to LK files on hand, I wrote a script to generate two LK files for testing, with random values. One can change the type of distribution for the random number generator to get more realistic values.

* ConvertLKtoFFD.py :
The arguments are as follows: name_of_mag_file name_of_phase_file [polarization_flag (optional, -V for vertical, -H for horizontal, -L for lefthand circular, or -R for righthand circular)]
An example call may be: "python ConvertLKtoFFD.py testLK1.lk testLK2.lk -V" to convert and merge testLK1.lk testLK2.lk into a single FFD file using vertical polarization If no polarization flag is passed, default polarization used is vertical.
One can also call -h or -help as the lone argument to the script for the help message and description of the script

* GenerateTestLK.py :
This script generates two LK files to be used for testing purposes. The two LK files are simply generated with random numbers, and one can change the distribution of the random number generator to their own preferred distribution. However, I did not implement command line argument functionality in this script as I had done for the converter script.