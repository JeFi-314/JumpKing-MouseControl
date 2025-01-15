using System.Linq;
using BehaviorTree;
using MouseControl.Controller;

public class WaitUntilNoInputMouse : IBTnode
{
    protected override BTresult MyRun(TickData p_data)
    {
        if (MousePad.PressedButtons.Contains(value: true))
        {
            return BTresult.Running;
        }

        return BTresult.Success;
    }
}
