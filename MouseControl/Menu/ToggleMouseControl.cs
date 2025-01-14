using System.IO;
using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

namespace MouseControl.Menu;
public class ToggleMouseControl : ITextToggle
{
    public ToggleMouseControl() : base(MouseControl.Prefs.isEnable)
    {
    }

    protected override string GetName() => "Enable Mouse Control";

    protected override void OnToggle()
    {
        MouseControl.Prefs.isEnable = toggle;
        CursorManager.SetVisible(toggle && MouseControl.Prefs.isShowCursor);
        CursorManager.SetBoundCursor(toggle && MouseControl.Prefs.isBoundCursor);
        if (toggle) {
            CursorManager.TryLoadTexture(Path.Combine(MouseControl.AssemblyPath, MouseControl.IconsFolder));
        }
    }
}