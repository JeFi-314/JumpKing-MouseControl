using BehaviorTree;

namespace MouseControl.Nodes;
public class CheckEnable : IBTnode
{
    protected override BTresult MyRun(TickData p_data)
    {
        return MouseControl.Prefs.isEnable ? BTresult.Success : BTresult.Failure;
    }
}