# Daniel Huynh

# NOTE: The arguments are as follows: name_of_mag_file name_of_phase_file [polarization_flag (optional, -V for vertical, -H for horizontal, -L for lefthand circular, or -R for righthand circular)]
# An example call may be: "python ConvertLKtoFFD.py testLK1.lk testLK2.lk -V" to convert and merge testLK1.lk testLK2.lk into a single FFD file using vertical polarization
# If no polarization flag is passed, default polarization used is vertical.
# One can also call -h or -help as the lone argument to the script for the help message and description of the script



# Import basic utilities
import math
import sys


# Parses each LK file, and combines both LK files' data into a single 2D list, with each index of the 2D list corresponding to a pairing of (Phi, Theta) corresponding to
# the order of (th1, ph1), (th1, ph2), (th1, ph3),…,(th1, phN), (th2, ph1), (th2, ph2)
# Output: values, a 2D list that contains as each element a list containing each pairing of (Phi, Theta)'s data in the form [Phi, Theta, Magnitude, Phase].
# Also returns the angle units in use, the total number of points, the start and stop values for each angle (Phi and Theta), as well as the number of considered points for each angle
def parse_data(LK1, LK2):
    values = [] # Initiating as an empty list
    namemag = ""
    namephase = ""
    unitsmag = ""
    unitsphase = ""
    mag_num_points = -1
    phase_num_points = -1
    phi_count = 0
    try:
        magfile = open(LK1, "r")
        phasefile = open(LK2, "r")
    except:
        exit("ERROR: One of the files not found.")

    try:
        magfile.readline() # Skip line (stk version)
        magfile.readline() # Skip line (stk version)
        phasefile.readline() # Skip line
        phasefile.readline() # Skip line
        namemag = magfile.readline() # Skip line "PhiThetaPattern"
        namephase = phasefile.readline() # Skip line "PhiThetaPattern"
        unitsmag = str(magfile.readline().upper().split()[1]) # assume the units are the same between both files, get the angle units in use
        unitsphase = str(phasefile.readline().upper().split()[1]) # assume the units are the same between both files, use mag_file's
        mag_num_points = int(magfile.readline().split()[1]) # assume the number of points are the same between both files, get the total number of points, or pairings of (phi, theta)
        phase_num_points = int(phasefile.readline().split()[1]) # assume the number of points are the same between both files, use mag_file's
        magfile.readline() # skip line "PatternData"
        phasefile.readline() # skip line "PatternData"
    except:
        exit("ERROR: Invalid file format.")

    start_phi = -1
    stop_phi = -1
    start_theta = -1
    stop_theta = -1
    reset_found = False
    reset_value = -1
    resets_found = 0
    for i in range(mag_num_points):
        line = magfile.readline().split() # Assume [Phi, Theta, Magnitude]
        line_phase = phasefile.readline().split() # Assume [Phi, Theta, Phase]
        line.append(line_phase[2]) # line list: [Phi, Theta, Magnitude, Phase]
        if(i == 0):
            reset_value = line[0]
            start_phi = reset_value
            start_theta = line[1]
            phi_count += 1
        else:
            if(i == mag_num_points-1):
                stop_theta = line[1]
            if(line[0] == reset_value):
                resets_found += 1
                if(resets_found == 1):
                    stop_phi = values[len(values)-1][0]
                    reset_found = True
            if(reset_found == False):
                phi_count += 1
        
        values.append(line)

    return values, unitsmag, mag_num_points, phi_count, [start_phi, stop_phi], [start_theta, stop_theta]


# Calculate the E_theta_real E_theta_imag E_phi_real E_phi_imag values from our combined file values using trigonometry, depending on the polarization chosen.
# Output: append the E_theta_real E_theta_imag E_phi_real E_phi_imag values to the end of the each element in the "values" 2D list such that each element in values should now be in the format [Phi, Theta, Magnitude, Phase, E_theta_real, E_theta_imag, E_phi_real, E_phi_imag]
# As a note, math.cos() and math.sin() use radians. If the units in use are degrees, we must convert them to radians first, as is performed in this code.
def calculate_ffd_values(values, polarization, units):
    
    if(polarization == "vertical"):
        for line in values:
            mag = float(line[2])
            phase = float(line[3])
            
            real = -1
            imag = -1
            if(units == "DEGREES"):
                real = mag * math.cos(math.radians(phase))
                imag = mag * math.sin(math.radians(phase))
            elif(units == "RADIANS"):
                real = mag * math.cos(phase)
                imag = mag * math.sin(phase)
            else:
                exit("ERROR: Invalid units indicated in LK file.")

            line.append(real) # E_theta_real
            line.append(imag) # E_theta_imag
            line.append(0) # E_phi_real
            line.append(0) # E_phi_imag
    elif(polarization == "horizontal"):
        for line in values:
            mag = float(line[2])
            phase = float(line[3])
            
            real = -1
            imag = -1
            if(units == "DEGREES"):
                real = mag * math.cos(math.radians(phase))
                imag = mag * math.sin(math.radians(phase))
            elif(units == "RADIANS"):
                real = mag * math.cos(phase)
                imag = mag * math.sin(phase)
            else:
                exit("ERROR: Invalid units indicated in LK file.")

            line.append(0) # E_theta_real
            line.append(0) # E_theta_imag
            line.append(real) # E_phi_real
            line.append(imag) # E_phi_imag
    elif(polarization == "rhcp"):
        for line in values:
            mag = float(line[2])
            phase = float(line[3])
            
            real = -1
            imag = -1
            if(units == "DEGREES"):
                real = mag * math.cos(math.radians(phase))
                imag = mag * math.sin(math.radians(phase))
            elif(units == "RADIANS"):
                real = mag * math.cos(phase)
                imag = mag * math.sin(phase)
            else:
                exit("ERROR: Invalid units indicated in LK file.")

            h_r = float(real)/math.sqrt(2)
            h_i = float(imag)/math.sqrt(2)
            v_r = -1*float(imag)/math.sqrt(2)
            v_i = float(real)/math.sqrt(2)

            line.append(v_r) # E_theta_real
            line.append(v_i) # E_theta_imag
            line.append(h_r) # E_phi_real
            line.append(h_i) # E_phi_imag
    elif(polarization == "lhcp"):
        for line in values:
            mag = float(line[2])
            phase = float(line[3])
            
            real = -1
            imag = -1
            if(units == "DEGREES"):
                real = mag * math.cos(math.radians(phase))
                imag = mag * math.sin(math.radians(phase))
            elif(units == "RADIANS"):
                real = mag * math.cos(phase)
                imag = mag * math.sin(phase)
            else:
                exit("ERROR: Invalid units indicated in LK file.")

            h_r = float(real)/math.sqrt(2)
            h_i = float(imag)/math.sqrt(2)
            v_r = float(imag)/math.sqrt(2)
            v_i = -1*float(real)/math.sqrt(2)

            line.append(v_r) # E_theta_real
            line.append(v_i) # E_theta_imag
            line.append(h_r) # E_phi_real
            line.append(h_i) # E_phi_imag

    return values



# Referencing: https://help.agi.com/stk/index.htm#comm/complexANSYSffdSamples.htm
# Writes the values to an "output.ffd" file
def write_to_ffd(values, num_points, phi_count, startstopphi, startstoptheta):
    phi_count = int(phi_count)
    theta_count = int(num_points/phi_count)
    output_file = open("output.ffd", "w")
    output_file.write(f"{startstoptheta[0]} {startstoptheta[1]} {theta_count}\n") # Theta Min Max NumPts
    output_file.write(f"{startstopphi[0]} {startstopphi[1]} {phi_count}\n") # Phi Min Max NumPts

    for line in values:
        output_file.write(f"{line[4]} {line[5]} {line[6]} {line[7]}\n")
    
    print("Finished writing FFD file.")


# NOTE: The arguments are as follows: name_of_mag_file name_of_phase_file [polarization_flag (optional, -V for vertical or -H for horizontal)]
# If no polarization flag is passed, default polarization used is vertical.
# One can also call -h or -help as the lone argument to the script for the help message and description of the script
# Main takes in the file names and parses the arguments provided. It will run the entire script pipeline when the script is called without need for the user to call anything or intervene.
def main():
    if(len(sys.argv) == 2 and (sys.argv[1] == "-help" or sys.argv[1] == "-h")):
        print("This script will take in two LK files-- a magnitude LK file and a phase LK file, and merge and convert them into a single FFD file that allows STK to work with both magnitude and phase in a single antenna file.")
        print("The arguments for this program are as follows: name_of_mag_file name_of_phase_file [polarization_flag (optional, -V for vertical, -H for horizontal, -L for lefthand circular, or -R for righthand circular)]")
        print("An example call may be: \"python ConvertLKtoFFD.py testLK1.lk testLK2.lk -V\" to convert and merge testLK1.lk testLK2.lk into a single FFD file using vertical polarization")
        print("If no polarization flag is passed, the default polarization used is vertical.")
        print("One can also call -h or -help as the lone argument to the script for this help message.")
        exit(0)
    elif(len(sys.argv) < 3):
        exit("ERROR: Invalid Arguments. Please pass required arguments:  name_of_mag_file name_of_phase_file [polarization_flag (optional, -V for vertical, -H for horizontal, -L for lefthand circular, or -R for righthand circular)]\nIf no polarization flag is passed, default polarization used is vertical.\nOne can also call -h or -help as the lone argument to the script for this help message.")

    polarization = "vertical"

    LK_file_1 = sys.argv[1]  # NOTE: ARGUMENT ONE: name of the MAGNITUDE LK file as a string
    LK_file_2 = sys.argv[2]  # NOTE: ARGUMENT TWO: name of the PHASE LK file as a string

    if(len(sys.argv) == 4):
        polarization_flag = sys.argv[3] # NOTE: ARGUMENT THREE (Optional): flag indicating the desired polarization
        if(polarization_flag == "-V"):
            polarization = "vertical"
        elif(polarization_flag == "-H"):
            polarization = "horizontal"
        elif(polarization_flag == "-R"):
            polarization = "rhcp"
        elif(polarization_flag == "-L"):
            polarization = "lhcp"
        else:
            exit("ERROR: Invalid optional polarization flag passed.")

    combined_file_values, units, num_points, phi_count, startstopphi, startstoptheta = parse_data(LK_file_1, LK_file_2)
    final_values = calculate_ffd_values(combined_file_values, polarization, units)
    write_to_ffd(final_values, num_points, phi_count, startstopphi, startstoptheta)

if __name__ == "__main__":
    main()