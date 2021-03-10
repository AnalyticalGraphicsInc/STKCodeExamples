import agi.core.AgCoreException;
import agi.core.*;
import agi.core.awt.AgAwt_JNI;
import agi.stk.ui.AgStkAutomation_JNI;
import agi.stk.ui.AgStkUi;
import agi.stkobjects.*;
import agi.stkutil.*;

public class StkExample
{
    // Object Names
    String m_scenarioName = "JavaExample";
    String m_satelliteName = "MySatellite";
    String m_facilityName = "MyFacility";

    // Member variables
    AgStkUi m_stkUi;
    AgStkObjectRootClass m_stkRoot;
    AgScenarioClass m_scenario;
    AgSatelliteClass m_satellite;
    AgFacilityClass m_facility;

    public void initialize()
    throws AgException
    {
        // Must be called once before using STK API library
        AgAwt_JNI.initialize_AwtDelegate();
        AgStkAutomation_JNI.initialize(true);
        AgAwt_JNI.initialize_AwtComponents();

        startSTK();
    }

    private void startSTK()
    {
        // Launch STK Application and get handle to STK Object Root
        try
        {
            m_stkUi = new AgStkUi();
            if( m_stkUi != null)
            {
                m_stkUi.setUserControl(true);
                m_stkUi.setVisible(true);
                m_stkRoot = (AgStkObjectRootClass)m_stkUi.getIAgStkObjectRoot();
            }
        }
        catch(AgCoreException ex)
        {
            ex.printHexHresult();
            ex.printStackTrace();
        }
    }

    public void createScenario()
    {
        // Create new scenario
        m_stkRoot.newScenario(m_scenarioName);
        m_scenario = (AgScenarioClass)m_stkRoot.getCurrentScenario();

        // Set units
        m_stkRoot.getUnitPreferences().setCurrentUnit("DateFormat", "UTCG");
        m_stkRoot.getUnitPreferences().setCurrentUnit("Time", "min");

        addSatellite();
        addFacility();
    }

    private void addSatellite()
    {
        // Create satellite object
        m_satellite = (AgSatelliteClass)m_scenario.getChildren()._new(AgESTKObjectType.E_SATELLITE, m_satelliteName);

        // Get orbit representation in classical elements
        AgVePropagatorTwoBodyClass propagator = (AgVePropagatorTwoBodyClass)m_satellite.getPropagator();
        IAgOrbitState representation = propagator.getInitialState().getRepresentation();
        IAgOrbitStateClassical keplerian = (IAgOrbitStateClassical)representation.convertTo(AgEOrbitStateType.E_ORBIT_STATE_CLASSICAL);

        // Set semi-major axis and eccentricity
        keplerian.setSizeShapeType(AgEClassicalSizeShape.E_SIZE_SHAPE_SEMIMAJOR_AXIS);
        IAgClassicalSizeShapeSemimajorAxis sizeShape = (IAgClassicalSizeShapeSemimajorAxis)keplerian.getSizeShape();
        sizeShape.setSemiMajorAxis(7500);
        sizeShape.setEccentricity(0.1);

        // Set inclination, argument or perigee and RAAN
        keplerian.getOrientation().setInclination(45);
        keplerian.getOrientation().setArgOfPerigee(0);
        ((IAgOrientationAscNodeRAAN)keplerian.getOrientation().getAscNode()).setValue(90);

        // Propagate satellite
        representation.assign(keplerian);
        propagator.propagate();
    }

    private void addFacility()
    {
        // Create a facility at AGI HQ
        m_facility = (AgFacilityClass)m_scenario.getChildren()._new(AgESTKObjectType.E_FACILITY, m_facilityName);
        m_facility.getPosition().assignGeodetic(40.0386, -75.5966, 0.0);
    }

    public void computeAccess()
    {
        // Compute access from the satellite to the facility
        IAgStkAccess access = m_facility.getAccessToObject(m_satellite);
        access.computeAccess();

        printAccessIntervals(access);
    }

    private void printAccessIntervals(IAgStkAccess access)
    {
        // Get access intervals
        IAgDataPrvInterval dataProvider = access.getDataProviders().getDataPrvIntervalFromPath("Access Data");
        IAgDrResult results = dataProvider.exec(m_scenario.getStartTime(), m_scenario.getStopTime());

        // Get arrays of access data
        IAgDrDataSetCollection dataSets = results.getDataSets();
        AgSafeArray accessNumberArray = dataSets.getDataSetByName("Access Number").getValues();
        AgSafeArray startTimeArray = dataSets.getDataSetByName("Start Time").getValues();
        AgSafeArray stopTimeArray = dataSets.getDataSetByName("Stop Time").getValues();
        AgSafeArray durationArray = dataSets.getDataSetByName("Duration").getValues();

        // Print the data
        String headers = String.format("Access Number\tStart Time (UTCG)\t\t\t\t\tStop Time (UTCG)\t\t\t\t\tDuration (min)");
        System.out.println(headers);

        int rowCount = accessNumberArray.getColumnCount();
        for(int row = 0; row < rowCount; row++)
        {
            int accessNumber = (int) accessNumberArray.getVariant(row).getObject();
            String startTime = (String) startTimeArray.getVariant(row).getObject();
            String stopTime = (String) stopTimeArray.getVariant(row).getObject();
            Double duration = (Double) durationArray.getVariant(row).getObject();

            String dataLine = String.format("%d\t\t\t\t%s\t\t%s\t\t%1.3f", accessNumber, startTime, stopTime, duration);
            System.out.println(dataLine);
        }
    }

    public void closeSTK()
    {
        m_stkRoot.closeScenario();
        m_stkRoot.release();
        m_stkUi.quit();
        m_stkUi.release();
    }

    public void uninitialize()
    throws AgException
    {
        // Tell the JVM it should finalize classes
        // and garbage collect.  However, there is
        // no guarantee of this.
        System.runFinalization();
        System.gc();

        AgAwt_JNI.uninitialize_AwtComponents();
        AgStkAutomation_JNI.uninitialize();
        AgAwt_JNI.uninitialize_AwtDelegate();
    }
}
