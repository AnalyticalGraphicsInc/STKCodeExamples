# STK Plugins (Engine and Plugin Scripts)

Example engine plugins and plugin scripts for STK Desktop and STK Engine.

Instructions for using plugin scripts can be found in the [STK help](https://help.agi.com/stkdevkit/index.htm#../Subsystems/pluginScripts/Content/pluginScriptsinstall.htm).

## [Access Constraints](AccessConstraints)

Implement a custom access constraint metric that can be used in Access for determining visibility intervals and as a Figure of Merit in Coverage. The access contraint plugin interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgAccessConstraintPlugin~IAgAccessConstraintPlugin.html).

---

## [Astrogator Attitude Controllers](Astrogator.AttitudeControllers)

Customize the attitude of an engine or thruster set (i.e., creating a steering law for an engine). The astrogator attitude controller interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgPropagatorWrappers~IAgGatorPluginAttCtrl.html).

---

## [Astrogator Engine Models](Astrogator.EngineModels)

Model engine thrust, mass flow rate, and Isp. The astrogator engine model interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgPropagatorWrappers~IAgGatorPluginEngineModel.html).

---

## [Astrogator EOM Functions](Astrogator.EOMFunctions)

Add additional equations of motion with user variables and customize the force model when using STK Astrogator to numerically propagate an orbit. The astrogator EOM interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgAsHpopPlugin~IAgAsEOMFuncPlugin.html).

---

## [Astrogator Search](Astrogator.Search)

Implement a custom search algorithm when using a Target Sequence. The astrogator search interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgSearch~IAgPluginSearch.html).

---

## [Attitude Simulator](AttitudeSimulatorPlugin)

Scripts to drive the attitude simulator capability within STK. Documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#../Subsystems/pluginScripts/Content/attitudePoints.htm).

---

## [Aviator Strategy](AviatorStrategy)

Customize the basic maneuver strategy within the Aviator module. Documentation can be found [here](https://help.agi.com/stk/index.htm#aircraft/proc_basicManeuver.htm).

---

## [Density Models](DensityModels)

Customize the computation of atmospheric density for satellite propagation. Documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/stkPlugins_PG.html?TocPath=Library%2520Reference%257CSTK%2520Engine%2520Plugins%257C_____1).

---

## [Drag Models](DragModels)

Customize a drag force model used in the propagation of the satellite trajectory. The drag model interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgAsHpopPlugin~IAgAsDragModelPlugin.html).

---

## [Ephemeris File Reader](EphemerisFileReader)

Implement a custom ephemeris file reader that can be used with the STK External propagator. The ephemeris file reader interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgAsPlugin~IAgAsEphemFileReaderPlugin.html).

---

## [Graphics](Graphics)

Plugins that cover a variety of graphical customizations. Listings of plugin types can be found [here](https://help.agi.com/stkdevkit/index.htm#STKPlugins/enginePlugins_pluginPoints.htm).

---

## [HPOP Force Models](Hpop.ForceModels)

Customize the force model used when using HPOP and STK Astrogator to numerically propagate an orbit. The HPOP force model interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgAsHpopPlugin_P.html).

---

## [Plugin Scripts](PluginScripts)

Plugin points for tasks performed by an individual script. Types of plugin scripts are found [here](https://help.agi.com/stkdevkit/index.htm#STKPlugins/enginePlugins_scriptPluginPoints.htm).

---

## [Post Processing](PostProcessing)

Scripts to customize report formats. Documentation can be found [here](https://help.agi.com/stk/index.htm#stk/report-08.htm).

---

## [Radar](Radar)

Plugins that cover a variety of radar computations. Listings of plugin types can be found [here](https://help.agi.com/stkdevkit/index.htm#STKPlugins/enginePlugins_pluginPoints.htm).

---

## [RT3](RT3)

Extension plugins for the Real-Time Tracking (RT3) tool. Documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/Rt3_PG.html).

---

## [Scalar](Scalar)

Extension plugins for calc scalars within Analysis Workbench. Documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgSTKVgtLib~IAgCrdnCalcScalarPlugin.html)

---
## [SRP Light Reflection](SRP.LightReflection)

Customize a solar pressure force model used in the propagation of the satellite trajectory. The SRP light reflection interface documentation can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgAsHpopPlugin~IAgAsLightReflectionPlugin.html).

---

## [VGT Plugins](VGT.Plugins)

Plugins that cover the creation and editing of Analysis Workbench components. Listing of supported interfaces can be found [here](https://help.agi.com/stkdevkit/index.htm#DocX/AgCrdnPlugin_P.html).

---
