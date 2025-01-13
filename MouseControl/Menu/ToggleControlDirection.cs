using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

namespace MouseControl.Menu;
public class ToggleControlDirection : ITextToggle
{
    public ToggleControlDirection() : base(ModEntry.Prefs.isRLControl)
    {
    }

    protected override string GetName() => "Enable R/L Control";

    protected override void OnToggle()
    {
        ModEntry.Prefs.isRLControl = toggle;
    }

    protected override bool CanChange()
    {
        return ModEntry.Prefs.isEnable;
    }
}
