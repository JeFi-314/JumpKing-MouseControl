using System.IO;
using JumpKing.PauseMenu.BT.Actions;
using MouseControl.Controller;

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
        CursorManager.SetVisible(toggle && ModEntry.Prefs.isShowCursor);
        CursorManager.SetBoundCursor(toggle && ModEntry.Prefs.isClipCursor);
        CursorManager.TryLoadTexture(Path.Combine(ModEntry.AssemblyPath, ModEntry.IconsFolder));
    }
}