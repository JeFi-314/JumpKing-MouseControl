using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

namespace MouseControl.Menu;
public class ToggleShowCursor : ITextToggle
{
    public ToggleShowCursor() : base(ModEntry.Prefs.isShowCursor)
    {
    }

    protected override string GetName() => "Show Cursor";

    protected override void OnToggle()
    {
        ModEntry.Prefs.isShowCursor = toggle;
        CursorManager.SetVisible(toggle);
    }

    protected override bool CanChange()
    {
        return ModEntry.Prefs.isEnable;
    }
}
