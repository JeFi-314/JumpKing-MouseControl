using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

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
        CursorManager.SetBoundCursor(toggle);
    }

    protected override bool CanChange()
    {
        return ModEntry.Prefs.isEnable;
    }
}
