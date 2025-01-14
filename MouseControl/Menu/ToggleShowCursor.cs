using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

namespace MouseControl.Menu;
public class ToggleShowCursor : ITextToggle
{
    public ToggleShowCursor() : base(MouseControl.Prefs.isShowCursor)
    {
    }

    protected override string GetName() => "Show Cursor";

    protected override void OnToggle()
    {
        MouseControl.Prefs.isShowCursor = toggle;
        CursorManager.SetVisible(toggle);
    }

    protected override bool CanChange()
    {
        return MouseControl.Prefs.isEnable;
    }
}
