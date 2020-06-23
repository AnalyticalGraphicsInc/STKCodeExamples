package stkengine

import agi.core.AgCoreException
import agi.core.ct.AgCt_JNI
import agi.core.logging.AgFormatter
import agi.stkengine.AgStkCustomApplication_JNI
import agi.stkobjects.*
import agi.stkx.AgEFeatureCodes
import agi.stkx.AgSTKXApplicationClass
import java.util.logging.ConsoleHandler
import java.util.logging.Level
import java.util.logging.Logger
import kotlin.system.exitProcess

class StkEngine {
    private lateinit var stkXApplication: AgSTKXApplicationClass
    private lateinit var stkXRoot: AgStkObjectRootClass

    fun compute()
    {
        println("Starting...")
        initializeStkX()
        println("STKX Initialized Successfully...")
        val scenario = createScenario()
        println("Scenario Created...")
        val facility = createFacility(scenario)
        println("Facility Created...")
        val aircraft = createAircraft(scenario)
        println("Aircraft Created...")
        computeAccess(scenario, facility, aircraft)
        uninitializeStkX()
    }

    private fun initializeStkX() {
        val ch = ConsoleHandler()
        ch.level = Level.OFF
        ch.formatter = AgFormatter()
        Logger.getLogger("agi").level = Level.OFF
        Logger.getLogger("agi").addHandler(ch)

        AgCt_JNI.initialize_CtDelegate()
        AgStkCustomApplication_JNI.initialize(true)

        this.stkXApplication = AgSTKXApplicationClass()
        this.stkXApplication.noGraphics = true

        if(!this.stkXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
        {
            val message = "STK Engine Runtime license is required to run this sample. Exiting!"
            println(message)
            exitProcess(0)
        }

        if(!this.stkXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL)){
            val message = "You do not have the required STK Globe license. Exiting!"
            println(message)
            exitProcess(0)
        }
        this.stkXRoot = AgStkObjectRootClass()
    }


    @Throws(AgCoreException::class)
    private fun computeAccess(scenario: AgScenarioClass, object1: IAgStkObject, object2: IAgStkObject)
    {
        val startTime = scenario.startTime_AsObject
        val stopTime = scenario.stopTime_AsObject

        val access = object1.getAccessToObject(object2)
        access.computeAccess()

        val dataProviderCollection = access.dataProviders
        val dataProviderItem = dataProviderCollection.getItem("Access Data")

        val dataProviderInterval = dataProviderItem as IAgDataPrvInterval
        val dataProviderResults = dataProviderInterval.exec(startTime, stopTime)

        val dataSetCollection = dataProviderResults.dataSets

        // Print column names
        val elementNames = dataSetCollection.elementNames_AsObject as Array<*>
        val nameCount = elementNames.size

        elementNames.forEach {
            print(it)
            print("\t")
        }
        println()

        val rowCount = dataSetCollection.rowCount

        for (i in 0 until rowCount)
        {
            val row = dataSetCollection.getRow_AsObject(i) as Array<*>
            row.forEach {
                print(it)
                print("\t")
            }
            println()
        }
    }

    @Throws(AgCoreException::class)
    private fun createFacility(scenario: AgScenarioClass): IAgStkObject
    {
        return scenario.children._new(AgESTKObjectType.E_FACILITY, "FacilityObject")
    }

    @Throws(AgCoreException::class)
    private fun createAircraft(scenario: AgScenarioClass): IAgStkObject
    {
        val aircraft = scenario.children._new(AgESTKObjectType.E_AIRCRAFT, "AircraftObject")
        val aircraftObject = aircraft as AgAircraftClass

        aircraftObject.routeType = AgEVePropagatorType.E_PROPAGATOR_GREAT_ARC.value
        val propagator = aircraftObject.route as IAgVePropagatorGreatArc
        val waypoints = propagator.waypoints

        val waypoint1 = waypoints.add()
        waypoint1.altitude = 2.0
        waypoint1.setLatitude(39.842)
        waypoint1.setLongitude(-75.596)

        val waypoint2 = waypoints.add()
        waypoint2.altitude = 2.0
        waypoint2.setLatitude(40.393)
        waypoint2.setLongitude(-75.632)

        propagator.propagate()
        return aircraft
    }

    @Throws(AgCoreException::class)
    private fun createScenario(): AgScenarioClass
    {
        stkXRoot.newScenario("NoGraphicsScenario")
        return stkXRoot.currentScenario as AgScenarioClass
    }

    private fun uninitializeStkX() {
        stkXRoot.closeScenario()
        stkXRoot.release()

        System.runFinalization()
        System.gc()

        AgStkCustomApplication_JNI.uninitialize()
        AgCt_JNI.uninitialize_CtDelegate()
    }
}