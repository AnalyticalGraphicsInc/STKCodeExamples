# VbScript Access Constraint Plugins

## [UProp_LOS_Check](VB_UProp_LOS_Check.vbs)

Check for Line of Sight access in urban environments. This script is used as a comm access constraint on the receiver. When a simple transmitter is used and all other losses are turned off, the comm link is computed considering losses caused by the urban environment. This function is intended for use with the attached scenario, where it is used as a comm access constraint for the grid constraint receiver for a coverage definition.

Uprop returns approximately zero for atmospheric loss when it determines LOS is available. This script computes free space path loss and subtracts it from RIP. With a 0dBW transmitter the difference should be approx zero since atmospheric loss is near zero when LOS exists. Function returns 100 when LOS available and 0 otherwise.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: [SingaporeLOSplugin.zip](https://sdf.agi.com/share/page/site/agi-support/document-details?nodeRef=workspace://SpacesStore/f29a6489-e855-438b-8876-1cf75648b50f)

---

## [NIIRS_Constraint](NIIRS_Constraint.vbs)

This constraint calculates a modified form of the National Imagery Interpretability Rating Scale (NIIRS) image quality metric. The first step is to calculate the Ground Sample Distance (GSD) for a sensor viewing another object. This version of the GSD equation is parameterized in terms of Q, the optical ratio.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---
