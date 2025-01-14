using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

namespace MouseControl.Menu;
public class ToggleControlDirection : ITextToggle
{
    public ToggleControlDirection() : base(MouseControl.Prefs.isRLControl)
    {
    }

    protected override string GetName() => "Enable R/L Control";

    protected override void OnToggle()
    {
        MouseControl.Prefs.isRLControl = toggle;
    }

    protected override bool CanChange()
    {
        return MouseControl.Prefs.isEnable;
    }
}
