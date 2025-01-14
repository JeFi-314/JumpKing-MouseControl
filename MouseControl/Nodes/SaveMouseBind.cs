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
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Walk {MousePad.Binding.Walk}!");
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Jump {MousePad.Binding.Jump}!");
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Pause {MousePad.Binding.Pause}!");
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Confirm {MousePad.Binding.Confirm}!");
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Cancel {MousePad.Binding.Cancel}!");
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Boots {MousePad.Binding.Boots}!");
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Snake {MousePad.Binding.Snake}!");
        Debug.WriteLine($"[DEBUG] MousePad.Binding.Restart {MousePad.Binding.Restart}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Walk {MouseControl.Prefs.MouseBinding.Walk}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Jump {MouseControl.Prefs.MouseBinding.Jump}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Pause {MouseControl.Prefs.MouseBinding.Pause}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Confirm {MouseControl.Prefs.MouseBinding.Confirm}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Cancel {MouseControl.Prefs.MouseBinding.Cancel}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Boots {MouseControl.Prefs.MouseBinding.Boots}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Snake {MouseControl.Prefs.MouseBinding.Snake}!");
        Debug.WriteLine($"[DEBUG] MouseControl.Prefs.MouseBinding.Restart {MouseControl.Prefs.MouseBinding.Restart}!");
        MouseControl.Prefs.MouseBinding = MousePad.Binding;
        SaveTextButton.SetNotifer(false);
        return BTresult.Success;
    }
}
