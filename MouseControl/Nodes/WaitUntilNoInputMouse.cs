using System.Linq;
using BehaviorTree;
using JumpKing.Controller;
using MouseControl.Controller;

public class WaitUntilNoInputAllMouse : IBTnode
{
    protected override BTresult MyRun(TickData p_data)
    {
        if (ControllerManager.instance.GetPadState().ToArray().Contains(value: true)
        || MousePad.PressedButtons.Contains(value: true))
        {
            return BTresult.Running;
        }

        return BTresult.Success;
    }
}
