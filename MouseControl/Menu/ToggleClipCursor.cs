using JumpKing.PauseMenu.BT.Actions;

namespace MouseControl.Menu;
public class ToggleClipCursor : ITextToggle
{
    public ToggleClipCursor() : base(ModEntry.Prefs.isClipCursor)
    {
    }

    protected override string GetName() => "Bound Cursor";

    protected override void OnToggle()
    {
        ModEntry.Prefs.isClipCursor = toggle;
    }

    protected override bool CanChange()
    {
        return ModEntry.Prefs.isEnable;
    }
}
