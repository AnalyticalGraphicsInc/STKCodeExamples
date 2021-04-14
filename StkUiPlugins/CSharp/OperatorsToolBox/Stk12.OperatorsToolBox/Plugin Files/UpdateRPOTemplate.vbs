dim uiApp, root, scenario, target, rpo, prop, mainSeq, pr_prm1
set uiApp = GetObject(,"STK12.Application")
set root = uiApp.Personality2
set scenario = root.CurrentScenario
set target = root.CurrentScenario.Children("Target")
set rpo = root.CurrentScenario.Children("RPO")
set prop = target.Propagator
prop.RunMCS()
set prop = rpo.Propagator
set mainSeq = prop.MainSequence
set pr_prm1 = mainSeq("Set_Initial_State").ScriptingTool.Parameters("Initial_Epoch")
pr_prm1.paramvalue = scenario.StartTime
prop.RunMCS()
