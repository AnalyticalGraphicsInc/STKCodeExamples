
import agi.core.AgCoreException;
import agi.core.AgSafeArray;
import agi.core.AgVariant;
import agi.core.ct.AgCt_JNI;
import agi.core.logging.AgFormatter;
import agi.stkengine.AgStkCustomApplication_JNI;
import agi.stkobjects.*;
import agi.stkx.AgEFeatureCodes;
import agi.stkx.AgSTKXApplicationClass;
import agi.stkvgt.*;
import java.util.logging.ConsoleHandler;
import java.util.logging.Level;
import java.util.logging.Logger;


public class STKEngine {
    AgSTKXApplicationClass stkXApplication;
    AgStkObjectRootClass stkXRoot;

    public void compute()
    {
        System.out.println("Starting...");
        initializeStkX();
        System.out.println("STKX Initialized Successfully...");
        AgScenarioClass scenario = createScenario();
        System.out.println("Scenario Created...");
        AgFacilityClass facility = createFacility(scenario);
        System.out.println("Facility Created...");
        AgAircraftClass aircraft = createAircraft(scenario);
        System.out.println("Aircraft Created...");
        xPositionData(aircraft);
        computeAccess(scenario,facility,aircraft);
        uninitializeStkX();
    }

    private void initializeStkX() {
        ConsoleHandler ch = new ConsoleHandler();
        ch.setLevel(Level.OFF);
        ch.setFormatter(new AgFormatter());
        Logger.getLogger("agi").setLevel(Level.OFF);
        Logger.getLogger("agi").addHandler(ch);

        AgCt_JNI.initialize_CtDelegate();
        AgStkCustomApplication_JNI.initialize(true);

        this.stkXApplication = new AgSTKXApplicationClass();
        this.stkXApplication.setNoGraphics(true);

        if(!this.stkXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
        {
            String message = "STK Engine Runtime license is required to run this sample. Exiting!";
            System.out.println(message);
            System.exit(0);
        }

        if(!this.stkXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL)){
            String message = "You do not have the required STK Globe license. Exiting!";
            System.out.println(message);
            System.exit(0);
        }
        this.stkXRoot = new AgStkObjectRootClass();
    }

    private void xPositionData(IAgStkObject aircraft){
        // object2 is an aircraft object
        //Get Time interval of Aircraft availability(propagated timespan) and it's Start/Stop times
        IAgCrdnEventInterval aircraftInterval = aircraft.getVgt().getEventIntervals().getItemByName("AvailabilityTimeSpan");
        var aircraftStart= aircraftInterval.findInterval().getInterval().getStart_AsObject();
        var aircraftStop= aircraftInterval.findInterval().getInterval().getStop_AsObject();
        // Gets the data providers available to the aircraft object
        IAgDataProviderCollection dataProviderCollection = aircraft.getDataProviders();
        // Get the Vectors(Inertial) data provider and cast the dataProviderGroup interface
        IAgDataProviderGroup dataProviderGroup = (IAgDataProviderGroup)dataProviderCollection.getItem("Vectors(Inertial)");
        // Gets the group(reference coordinates) for the data provider
        IAgDataProviderInfo dataProviderItem = dataProviderGroup.getGroup().getItem("Nadir(Centric)");
        // Gets the results of the Vectors(Inertial)-Nadir(Centric) data provider for the time interval of the aircraft at 60 sec time steps
        IAgDrResult dataProviderResults = ((IAgDataPrvTimeVar) dataProviderItem).exec(aircraftStart,aircraftStop,60);
        // Gets data from the Result
        IAgDrDataSetCollection dataSetCollection = dataProviderResults.getDataSets();
        // Getting all values for "X" vector value
        AgSafeArray array = dataSetCollection.getDataSetByName("x").getValues();
        // Get the first value in Array
        AgVariant variant = array.getVariant(1);
        // Print the value. Remove the "getDouble()" method to see the data contained in AgVariant
        System.out.println();
        System.out.print(variant.getDouble());
        System.out.println();

    }

    private void computeAccess(AgScenarioClass scenario,IAgStkObject object1, IAgStkObject object2) throws AgCoreException
    {
        var startTime = scenario.getStartTime_AsObject();
        var stopTime = scenario.getStartTime_AsObject();

        IAgStkAccess access = object1.getAccessToObject(object2);
        access.computeAccess();

        IAgDataProviderCollection dataProviderCollection = access.getDataProviders();
        IAgDataProviderInfo dataProviderItem = dataProviderCollection.getItem("Access Data");

        IAgDataPrvInterval dataProviderInterval = (IAgDataPrvInterval)dataProviderItem;
        IAgDrResult dataProviderResults = dataProviderInterval.exec(startTime, stopTime);

        IAgDrDataSetCollection dataSetCollection = dataProviderResults.getDataSets();

        // Print column names

        Object[] elementNames = (Object[])dataSetCollection.getElementNames_AsObject();

        for (var i : elementNames) {
            System.out.print(i);
            System.out.print("\t");
        }
        System.out.println();

        int rowCount = dataSetCollection.getRowCount();

        for (int i =0;i<rowCount;i++)
        {
            Object[] row = (Object[])dataSetCollection.getRow_AsObject(i);
            for (var j:row) {
                System.out.print(j);
                System.out.print("\t");
            }
            System.out.println();
        }
    }

    private AgFacilityClass createFacility(AgScenarioClass scenario) throws AgCoreException
    {
        return (AgFacilityClass) scenario.getChildren()._new(AgESTKObjectType.E_FACILITY, "FacilityObject");
    }

    private AgAircraftClass createAircraft(AgScenarioClass scenario) throws AgCoreException
    {
        AgAircraftClass aircraft = (AgAircraftClass) scenario.getChildren()._new(AgESTKObjectType.E_AIRCRAFT, "AircraftObject");

        aircraft.setRouteType(AgEVePropagatorType.E_PROPAGATOR_GREAT_ARC);
        IAgVePropagatorGreatArc propagator = (IAgVePropagatorGreatArc) aircraft.getRoute();
        IAgVeWaypointsCollection waypoints = propagator.getWaypoints();

        IAgVeWaypointsElement waypoint1 = waypoints.add();
        waypoint1.setAltitude(2.0);
        waypoint1.setLatitude(39.842);
        waypoint1.setLongitude(-75.596);

        IAgVeWaypointsElement waypoint2 = waypoints.add();
        waypoint2.setAltitude(2.0);
        waypoint2.setLatitude(40.393);
        waypoint2.setLongitude(-75.632);

        propagator.propagate();
        return aircraft;
    }

    private AgScenarioClass createScenario() throws AgCoreException
    {
        stkXRoot.newScenario("NoGraphicsScenario");
        return (AgScenarioClass) stkXRoot.getCurrentScenario();
    }

    private void uninitializeStkX() {
        stkXRoot.closeScenario();
        stkXRoot.release();

        System.runFinalization();
        System.gc();

        AgStkCustomApplication_JNI.uninitialize();
        AgCt_JNI.uninitialize_CtDelegate();
    }
}
