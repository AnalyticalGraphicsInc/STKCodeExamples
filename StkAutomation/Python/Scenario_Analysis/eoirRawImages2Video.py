import glob
import os
import pandas as pd
import numpy as np
from PIL import Image
import cv2

def normalizeFrameSequence(inputFolder, outputFolder, k = 0.3):
    # Suggested gamma encoding k
    # Linear (k=1),Cubic Root(k=1/3), Square transformation(k=2)
    # Linear just converts pixel counts to the range 0 to 255
    # Cubic Root suppresses the bright spots and enhances the dark spots
    # Square suppresses the dark spots and enhances the bright spots

    # Get all raw files
    os.chdir(inputFolder)
    rawFiles = []
    for file in glob.glob("*.txt"):
        rawFiles.append(file)

    # Get global min and max to normalize
    globalMax = float('-inf')
    for file in rawFiles:
        print(f'Processing {file}')
        with open(file, 'r') as f:
            for line in f:
                lineNums = [float(num) for num in line.split()]
                globalMax = max(globalMax, max(lineNums))

    # Set max bound with gamma encoding
    os.chdir(outputFolder)
    maxBound = globalMax ** k
    for index, file in enumerate(rawFiles):
        # Read image, then clip to 0, then apply gamma, then normalize, then write to file
        print(f'Writing normalized {file}')
        data = pd.read_csv(os.path.join(inputFolder, file), sep= '\\s+', header=None).clip(lower=0) ** k
        normalizedData = data/maxBound*255 # Max value of 255 for uint8
        imageArray = normalizedData.values.astype(np.uint8)
        image = Image.fromarray(imageArray)
        image.save(f'normalizedFrame_{index:06d}.jpg')

def images2Video(imageFolder, videoName, fps):
    # Get video frames
    os.chdir(imageFolder)
    files = []
    for file in os.listdir("."):
        files.append(file)
    numImages = len(files)

    # Find mean image size to resize
    print('Computing video frame size')
    mean_width = 0
    mean_height = 0
    for file in files:
        im = Image.open(file)
        width, height = im.size
        mean_width += width
        mean_height += height
    mean_width = int(mean_width / numImages)
    mean_height = int(mean_height / numImages)

    # Must resize to consistent size for cv2 video writer
    print('Resizing images')
    for file in files:
        im = Image.open(file)
        imResize = im.resize((mean_width, mean_height), Image.Resampling.LANCZOS)
        imResize.save(file)

    # Write video
    print('Writing video')
    frame = cv2.imread(files[0])
    height, width, _ = frame.shape
    video = cv2.VideoWriter(os.path.join(os.path.dirname(imageFolder), videoName+'.mp4'), cv2.VideoWriter.fourcc(*'avc1'), fps, (width, height))
    for file in files:
        print(file)
        video.write(cv2.imread(file))
    cv2.destroyAllWindows()
    video.release()

def main():
    # INPUTS
    rawFolder = 'C:\\RAW'
    normalizedFolder = 'C:\\Compressed'
    videoName = 'EOIR_Video'
    fps = 10
    
    # Run
    normalizeFrameSequence(rawFolder, normalizedFolder, k=1)
    images2Video(normalizedFolder, videoName, fps)
    print('Done')

if __name__ == "__main__":
    main()