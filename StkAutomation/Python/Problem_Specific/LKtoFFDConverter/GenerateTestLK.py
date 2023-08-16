import random

def generate():
    file1 = open("testlk1.lk", "w")
    file2 = open("testlk2.lk", "w")
    files = [file1, file2]

    for file in files:
        file.write("stk.v.6.0\n")
        file.write("\n")
        file.write("PhiThetaPattern\n")
        file.write("AngleUnits Degrees\n")
        file.write("NumberOfPoints 16471\n")
        file.write("PatternData\n")
        for theta in range(0, 181, 2):
            for phi in range(0, 361, 2):
                file.write(f"{phi}\t{theta}\t{random.gauss(-6, 5)}\n")

def main():
    generate()

if __name__ == "__main__":
    main()