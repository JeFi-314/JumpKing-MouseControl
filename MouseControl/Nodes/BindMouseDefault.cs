using BehaviorTree;
using MouseControl.Controller;

namespace MouseControl.Nodes;
internal class BindMouseDefault : IBTnode
{
    public BindMouseDefault()
    {
    }

    protected override BTresult MyRun(TickData p_data)
    {
        MousePad.Binding = new MouseBinding();
        // SaveTextButton.SetNotifer(true);
        return BTresult.Success;
    }
}
