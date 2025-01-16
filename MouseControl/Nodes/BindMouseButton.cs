using BehaviorTree;
using JumpKing.Controller;
using MouseControl.Controller;
using System;
using System.Diagnostics;
using System.Linq;

namespace MouseControl.Nodes;
public class BindMouseButton : IBTnode
{
    private string bindName;
    private bool[] lastPressed = new bool[6];
	public bool[] LastPressed
	{
		get => lastPressed;
	}

    public BindMouseButton(string p_bindName) {
        bindName = p_bindName;
    }

    protected override BTresult MyRun(TickData p_data)
    {
        BTresult result = BTresult.Running;
        foreach (int i in Enum.GetValues(typeof(MouseButtons))) {
            if (result == BTresult.Running && MousePad.PressedButtons[i] && !LastPressed[i]) {
                MousePad.Binding.SaveButton(bindName, (MouseButtons)i);
                // SaveTextButton.SetNotifer(true);
                result = BTresult.Success;
#if DEBUG
                Debug.WriteLine($"[DEBUG] Binded '{bindName}' with {(MouseButtons)i}!");
#endif
            }
            lastPressed[i] = MousePad.PressedButtons[i];
        }
        if (result == BTresult.Running && MenuController.instance.GetPadState().ToArray().Contains(value: true)) {
            MousePad.Binding.SaveButton(bindName, MouseButtons.None);
            MenuController.instance.ConsumePadPresses();
            // SaveTextButton.SetNotifer(true);
            result = BTresult.Success;
#if DEBUG
            Debug.WriteLine($"[DEBUG] Binded '{bindName}' with {MouseButtons.None}!");
#endif
        }
        return result;
    }
}