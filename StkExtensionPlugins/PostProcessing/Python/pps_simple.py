#
# this is the temp file used by STK
#
import sys

inputfile = sys.argv[1]

#
# open/read the file
#
content = []
with open(inputfile) as f:
    content = f.readlines()

#
# open the new file (overwite input file) for writing
#
outfile = open(inputfile, "w")
count = 1
for s in content:
    outfile.write(str(count) + " " + s),
    count += 1

#
# close the output file
#
outfile.close()
