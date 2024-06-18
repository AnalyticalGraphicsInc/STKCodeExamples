# -*- coding: utf-8 -*-
"""
Created on Tue Jul 28 19:04:46 2020

@authors: Arien Sligar
         Shawn Carpenter
         
Python Script: HFSS_RCS_Complex_Export_AEDTLib.py

 Date: 29 July, 2020

  The purpose of this script is to write a FullComplexScatterMatrix data file for AGI STK.
  This script extracts HFSS SBR+ monostatic RCS data and exports it for import to AGI STK.
  The SBR+ monostatic RCS simulation MUST include:
    1. A target, aligned with X-axis "forward" and Z-axis "up"
    2. Plane wave incident definition over (theta, phi) for Vertical (theta) pol.
        Name this source "V".
        AGI requires theta span to be defined over (0,180) and phi over (-180,180).
    3. Plane wave incident definition over (theta, phi) for Vertical (phi) pol.
        Name this source "H".
    4. One frequency of simulation. (Have not expanded this for multi-freq use).
    5. Completed simulation results
    6. Make the RCS simulation project the active Design in your 2020 R2 AEDT interface
"""
import numpy as np
import sys
from pyaedt import Hfss
from pyaedt import get_pyaedt_app
from pyaedt import settings
#settings.use_grpc_api=False

#from pyaedt import *

#
# USER: Set the following variables for output:
#
#setup_name = "30GHz"
setup_name = "Setup1"
sources = ["Hinc","Vinc"] #incident wave polarization
#outfile_name = "D:/test1.rcs"
outfile_name = "D:/rcs_gulfstream_exton.rcs"
#outfile_name = "C:/Drone_22r2b.rcs"
#outfile_name = "C:/GulfStream_23r2.rcs"
#outfile_name = "D:/Designs/Electronics_Desktop_2023/HFSS/Plane/F-16/F-16.rcs"
#desktopVersion = '2022.2' # Update to reflect the AEDT version you are using
desktopVersion = '2023.2' # Update to reflect the AEDT version you are using#
#
# Copyright Ansys, Inc., 2020
########## Do not change code below ##################################

with Hfss(projectname='rcs_gulfstream_23R2',designname='HFSSDesign1',specified_version=desktopVersion, non_graphical=False, new_desktop_session=False) as hfss:
#with Hfss(projectname='F-16_RCS',designname='High_Density',specified_version=desktopVersion, non_graphical=False, new_desktop_session=False) as hfss:
#with Hfss(projectname='Drone_22r2',designname='HFSSDesign1',specified_version=desktopVersion, non_graphical=False, new_desktop_session=False) as hfss:

    oModule =hfss.odesign.GetModule("Solutions")
    oModule.SetSourceContexts(["V", "H"])
    oModule = hfss.odesign.GetModule("ReportSetup")
    
    trace_phi = 'ComplexMonostaticRCSPhi'
    trace_theta = 'ComplexMonostaticRCSTheta'
    
    traces = [trace_theta,trace_phi]
    
    all_data = {}

    for source in sources:
        for trace_name in traces:
            print(trace_name)
            ctxt = ["SourceContext:=" , source]
            sweeps = ["IWaveTheta:=", ["All"],"IWavePhi:=", ["All"],"Freq:=", ["1.0GHz"]]
#            sweeps = ["IWaveTheta:=", ["All"],"IWavePhi:=", ["All"],"Freq:=", ["6.0GHz"]]
#            sweeps = ["IWaveTheta:=", ["All"],"IWavePhi:=", ["All"],"Freq:=", ["30GHz"]]
            solnData = oModule.GetSolutionDataPerVariation("Monostatic RCS", setup_name + " : Sweep", ctxt, sweeps, trace_name)
            
            values_real = []
            values_imag = []
    
            for each in solnData: #I think this is if more than one solution variation exists, should only be one for this project
                values_real.append(each.GetRealDataValues(trace_name))
                values_imag.append(each.GetImagDataValues(trace_name))
    
            iwavetheta_values = solnData[0].GetSweepValues('IWaveTheta')
            iwavephi_values = solnData[0].GetSweepValues('IWavePhi')
    
            data = [values_real[0],values_imag[0]]
    
            if (source=="Hinc" and trace_name=="ComplexMonostaticRCSPhi"):
                    name='HH'
            elif (source=="Hinc"):
                    name='HV'
            elif (source=="Vinc" and trace_name=="ComplexMonostaticRCSTheta"):
                    name='VV'
            else:
                    name='VH'
                    
            all_data[name] = data
        
        
#all data is now in a list in the format all_data[n][m])
#where n is equal to polarization, ie 'HH' 'HV' 'VV' 'VH'
#and m=0 is for real part, m=1 is imagainary part 
#so you can get the column of data swept over all theta and all phi by iterating over
#all_data['HH']

#pi = 3.14159265359


length_of_data = len(iwavetheta_values)
num_phi_points = len(iwavephi_values)
num_theta_points = int(len(iwavetheta_values)/num_phi_points)

ph = 0 # phi_values counter for output value indexing

file_out = open(outfile_name, "w")
# Write header information
file_out.writelines('stk.v.9.0\n')
file_out.writelines('BEGIN rcs\n')
file_out.writelines('    RCSPatternType   FullComplexScatterMatrix\n')
file_out.writelines(' RhoAxis   RhoFromZ\n')
file_out.writelines('    RCSValuesLinearScale\n')
file_out.writelines('    RCSScatterMatrixBasis Horizontal_Vertical_OrothogonalPair\n')
file_out.writelines('    RhoPoints  ' + str(num_theta_points) + '\n')
file_out.writelines('    ThetaPoints  ' + str(num_phi_points) + '\n')
file_out.writelines('    BEGIN rcsdata\n')

for n in range(length_of_data):
    hh_re = str(all_data['HH'][0][n])
    hh_im = str(all_data['HH'][1][n])
    hv_re = str(all_data['HV'][0][n])
    hv_im = str(all_data['HV'][1][n])
    vv_re = str(all_data['VV'][0][n])
    vv_im = str(all_data['VV'][1][n])
    vh_re = str(all_data['VH'][0][n])
    vh_im = str(all_data['VH'][1][n])
    if ((n%(num_theta_points))==0):
        if (n != 0): ph = ph + 1
    string_line = '      ' + str(iwavetheta_values[n]*180.0/np.pi) + ' ' + str(iwavephi_values[ph]*180.0/np.pi) + ' ' + vv_re + ' ' + vv_im + ' ' + vh_re + ' ' + vh_im + ' ' + hv_re + ' ' + hv_im + ' ' + hh_re + ' '+ hh_im + '\n'
    file_out.writelines(string_line)

file_out.writelines('    END rcsdata\n')
file_out.writelines('END RCS\n')


file_out.flush()
file_out.close()

