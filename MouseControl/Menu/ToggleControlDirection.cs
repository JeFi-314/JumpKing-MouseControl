using JumpKing.PauseMenu.BT.Actions;

namespace MouseControl.Menu;
public class ToggleControlDirection : ITextToggle
{
    public ToggleControlDirection() : base(ModEntry.Pref.isControlDirection)
    {
    }

    protected override string GetName() => "Control Direction";

    protected override void OnToggle()
    {
        ModEntry.Pref.isControlDirection = toggle;
    }

    protected override bool CanChange()
    {
        return ModEntry.Pref.isEnable;
    }
}
