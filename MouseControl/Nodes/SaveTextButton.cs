using BehaviorTree;
using JumpKing.PauseMenu.BT;
using Microsoft.Xna.Framework.Graphics;

namespace MouseControl.Nodes;
public class SaveTextButton : TextButton
{
    private static bool notify = false;
    private static SaveTextButton _instance;
    public SaveTextButton(IBTnode p_child, SpriteFont p_font)
        : base("Save", p_child, p_font)
    {
        if (_instance != null) return;
        _instance = this;
    }

    public static void SetNotifer(bool value) {
        if (value && !notify) {
            _instance.Text = _instance.Text + "*";
            notify = true;
        }
        else if (!value && notify) {
            _instance.Text = _instance.Text.Substring(0, _instance.Text.Length-1);
            notify = false;
        }
    }
}