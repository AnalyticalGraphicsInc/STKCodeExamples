# EOIR Scenario Analysis 

	
Problem Description:  

Modeling clouds and understanding their affects on a mission is imperative for mission modeling. EOIR provides the capabilities to model these effects, Python & Matlab allow you process and analyze the results. 

## Generating EOIR Cloud data 
 
[EOIR Cloud Air To Air Scenario](https://sdf.agi.com/share/page/context/mine/document-details?nodeRef=workspace://SpacesStore/19b625b1-839d-40fc-9774-41ca6ef43f9f)
In this specific scenario two aircraft flights were modeled, each flying in a different direction. One aircraft has a Visible and a LWIR camera on board (Southbound). The cameras point and image the cross flight (Eastbound). The cross flight has a Mesh File: Boeing-757.obj; Max Dimensions: 70 m; Temperature 273 K.

Optionally, users can design their own missions in STK and use this FAQ to model clouds: How to Model clouds in EOIR https://agiweb.secure.force.com/faqs/articles/Keyword/How-to-model-clouds-in-EOIR. 

 
## [EOIR_Image_Generation.ipynb](EOIR_Image_Generation.ipynb)

Made with [Python API](https://help.agi.com/stkdevkit/index.htm#python/pythonGettingStarted.htm?Highlight=python%20api) available in STK 12.1+

Once the scenario is built, a python script with connect commands was used to automate data generation. The script connects to an open scenario. It has a few functions, see comments in script. The specified cloud and atmosphere properties are used to define the file location for the generated data. The script cycles through clouds and atmosphere properties and generates a bmp file and a text file for each time step (using the predefined animation times/timesteps). Once the data is generated it can be processed.

### Dependencies

* Capabilities: Free, Integration, EOIR
* Other Scripts: N/A
* Scenario: [EOIR Cloud Air To Air Scenario](https://sdf.agi.com/share/page/context/mine/document-details?nodeRef=workspace://SpacesStore/19b625b1-839d-40fc-9774-41ca6ef43f9f)
 

## [Images_to_video.ipynb](Images_to_video.ipynb)
This is a DIY solution to stack all the images to a video. Since this action had to be repeated it was automated. The script reads in a directory of images, resizes them, reorders them (only needed if stacking the bmp files), and then writes them to a video file.

### Dependencies
* Capabilities: N/A 
* Other Scripts: [EOIR_Image_Generation.ipynb](EOIR_Image_Generation.ipynb)
* Scenario: N/A 