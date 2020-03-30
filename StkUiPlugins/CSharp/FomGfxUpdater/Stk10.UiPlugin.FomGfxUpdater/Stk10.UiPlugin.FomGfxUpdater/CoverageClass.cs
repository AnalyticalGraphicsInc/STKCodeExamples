using System;
using System.Collections.Generic;
using System.Text;
using AGI.STKObjects;

namespace Stk10.UiPlugin.FomGfxUpdater
{
    static class CoverageClass
    {
        public static AgStkObjectRootClass root;

        

        public static void AutoContour(string fomPath, string staticOrAnimation)
        {
		    IAgStkObject figureofMerit = root.GetObjectFromPath(fomPath);

		    IAgDataPrvFixed fomDP = (IAgDataPrvFixed)figureofMerit.DataProviders["Overall Value"];
		    //Execute the data providers over the scenario interval - scen.StartTime to scen.StopTime
		    IAgDrResult fomResult = fomDP.Exec();

		    Array mini = fomResult.DataSets.GetDataSetByName("Minimum").GetValues();
		    Array maxi = fomResult.DataSets.GetDataSetByName("Maximum").GetValues();
				
		    double min = .99 * (double)mini.GetValue(0);
		    double max = 1.01 * (double)maxi.GetValue(0);
		
		    double step = (max - min) / 99.0;

            string staticOnOff;
            string animationOnOff;
            if (staticOrAnimation.ToLower() == "static")
            {
                staticOnOff = "On";
                animationOnOff = "Off";                
            }
            else
            {
                staticOnOff = "Off";
                animationOnOff = "On";
            }

            root.ExecuteCommand("BatchGraphics * On");
            root.ExecuteCommand("Graphics " + fomPath + " Contours " + staticOrAnimation + " ColorRamp red blue StartStop " + min.ToString() + " " + max.ToString() + " " + step.ToString());
            root.ExecuteCommand("Graphics " + fomPath + " Contours Static Show " + staticOnOff);
            root.ExecuteCommand("Graphics " + fomPath + " Static " + staticOnOff);
            root.ExecuteCommand("Graphics " + fomPath + " Contours Animation Show " + animationOnOff);
            root.ExecuteCommand("Graphics " + fomPath + " Animation " + animationOnOff);
            root.ExecuteCommand("BatchGraphics * Off");

        }

        public static void GenerateGridStatsReport(string fomPath)
        {
            root.ExecuteCommand("ReportCreate " + fomPath + " Type Display Style \"Grid Stats\"");
        }


    }
}
