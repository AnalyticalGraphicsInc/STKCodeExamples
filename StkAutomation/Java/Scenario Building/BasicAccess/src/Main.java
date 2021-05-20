import javax.swing.*;
import agi.core.*;

public class Main
{
    public static void main(String[] args)
    {
        Runnable r = new Runnable()
        {
            public void run()
            {
                try
                {
                    StkExample example = new StkExample();
                    example.initialize();
                    example.createScenario();
                    example.computeAccess();
                    //example.closeSTK();
                    example.uninitialize();
                }
                catch(AgCoreException ce)
                {
                    ce.printHexHresult();
                    ce.printStackTrace();
                }
                catch(Throwable t)
                {
                    t.printStackTrace();
                }
            }
        };
        SwingUtilities.invokeLater(r);
    }
}
