using JumpKing.PauseMenu.BT.Actions;

namespace MouseControl.Menu;
public class ToggleMouseControl : ITextToggle
{
    public ToggleMouseControl() : base(ModEntry.Prefs.isEnable)
    {
    }

    protected override string GetName() => "Enable Mouse Control";

    protected override void OnToggle()
    {
        ModEntry.Prefs.isEnable = toggle;
    }
}