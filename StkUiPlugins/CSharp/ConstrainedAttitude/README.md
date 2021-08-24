# Constrained Attitude UI Plugin

## **Overview**
   
This plugin allows you to implement angle limit constraints on your vehicle's attitude. Select a vector to align a body axis with, then constrain another body axis to a specified constraint vector. Finally, specify an angle offset limit from the constraint vector.

![UserInterface](https://user-images.githubusercontent.com/60355211/130655866-5df9e4e6-920a-4ea0-a1e1-2159a04f102a.png)

After creating the attitude, the vehicle will remain aligned with the alignment vector until reaching the angle offset limit. Once at the limit, the attitude will hold there while still attempting to stay with the alignment vector. Once the angle offset is below the limit, the attitude returns to normal.

![AttitudeExample](https://user-images.githubusercontent.com/60355211/130655920-50bb1ede-f7a9-4997-994d-79cc67bc26a7.png)

## **Download**  
 
To use this plugin directly, follow the instructions [here](https://agiweb.secure.force.com/code/articles/Custom_Solution/Constrained-Attitude-UI-Plugin)

## **Dependencies**

* Licenses: Free, [Analysis Workbench](https://www.agi.com/products/stk-systems-bundle/stk-analysis-workbench)
* Other Scripts: N/A
* Scenario: N/A
