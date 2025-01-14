using System.Diagnostics;
using BehaviorTree;
using MouseControl.Controller;

namespace MouseControl.Nodes;
public class SaveMouseBind : IBTnode
{
    public SaveMouseBind(): base()
    {
    }

    protected override BTresult MyRun(TickData p_data)
    {
        MouseControl.Prefs.MouseBinding = MousePad.Binding;
        SaveTextButton.SetNotifer(false);
        return BTresult.Success;
    }
}
