using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

namespace MouseControl.Menu;
public class ToggleBoundCursor : ITextToggle
{
    public ToggleBoundCursor() : base(ModEntry.Prefs.isBoundCursor)
    {
    }

    protected override string GetName() => "Bound Cursor";

    protected override void OnToggle()
    {
        ModEntry.Prefs.isBoundCursor = toggle;
        CursorManager.SetBoundCursor(toggle);
    }

    protected override bool CanChange()
    {
        return ModEntry.Prefs.isEnable;
    }
}
