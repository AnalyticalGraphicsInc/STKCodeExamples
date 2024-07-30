import agi.core.AgCoreException;
import agi.core.AgCore_JNI;
public class Main {
    public static void main(String[] args) {
        try {
            STKEngine stkEngine = new STKEngine();
            AgCore_JNI.xInitThreads();

            stkEngine.compute();
        }
        catch (AgCoreException e) {
            e.printHexHresult();
            e.printStackTrace();
        }
    catch (Throwable t) {
            t.printStackTrace();
        }

    }
}