# EOIR Scenario Analysis

Problem Description:

Modeling clouds and understanding their affects on a mission is imperative for mission modeling. EOIR provides the capabilities to model these effects, Python & Matlab allow you process and analyze the results.

## Generating EOIR Cloud data

[EOIR Cloud Air To Air Scenario](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/6102d7ad-b7e7-4b1a-a406-5f4927a8651c)
In this specific scenario two aircraft flights were modeled, each flying in a different direction. One aircraft has a Visible and a LWIR camera on board (Southbound). The cameras point and image the cross flight (Eastbound). The cross flight has a Mesh File: Boeing-757.obj; Max Dimensions: 70 m; Temperature 273 K.

Optionally, users can design their own missions in STK and use this FAQ [How to Model clouds in EOIR](https://analyticalgraphics.force.com/faqs/articles/Keyword/How-to-model-clouds-in-EOIR)

### Dependencies

* Licenses: [STK Premium Space](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-space-brochure.pdf) or [STK Premium Air](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-air-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

## [EOIR_Image_Generation.ipynb](EOIR_Image_Generation.ipynb)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm) available in STK 12.1+

Once the scenario is built, a python script with connect commands was used to automate data generation. The script connects to an open scenario. It has a few functions, see comments in script. The specified cloud and atmosphere properties are used to define the file location for the generated data. The script cycles through clouds and atmosphere properties and generates a bmp file and a text file for each time step (using the predefined animation times/timesteps). Once the data is generated it can be processed.

### Dependencies

* Licenses: [STK Premium Space](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-space-brochure.pdf) or [STK Premium Air](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-air-brochure.pdf)
* Other Scripts: N/A
* Scenario: [EOIR Cloud Air To Air Scenario](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/6102d7ad-b7e7-4b1a-a406-5f4927a8651c)

## [Images_to_video.ipynb](Images_to_video.ipynb)

This is a DIY solution to stack all the images to a video. Since this action had to be repeated it was automated. The script reads in a directory of images, resizes them, reorders them (only needed if stacking the bmp files), and then writes them to a video file.

### Dependencies

* Licenses: N/A
* Other Scripts: [EOIR_Image_Generation.ipynb](EOIR_Image_Generation.ipynb)
* Scenario: N/A

## [Generalized_EOIR_Image_Data_Generation](Generalized_EOIR_Image_Data_Generation)

This script is a geneneralized version of [EOIR_Image_Generation.ipynb](EOIR_Image_Generation.ipynb). The previous script allowed you to change atmosphere and cloud settings and generate the synthetic scene and raw sensor data. It was specific to that example, but there is a need for a generalied solution. This new script allows you to connect to any open scenario on your machine and generate the synthetic scene and raw sensor data.

### Dependencies

* Licenses: [STK Premium Space](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-space-brochure.pdf) or [STK Premium Air](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-air-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A
