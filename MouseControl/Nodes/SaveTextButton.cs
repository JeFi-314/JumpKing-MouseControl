using BehaviorTree;
using HarmonyLib;
using JumpKing.PauseMenu.BT;
using Microsoft.Xna.Framework.Graphics;

namespace MouseControl.Nodes;
public class SaveTextButton : TextButton
{
    private static bool notify = false;
    private static SaveTextButton _instance;
    public SaveTextButton(IBTnode p_child, SpriteFont p_font)
        : base("Save"+(notify ? "*" : ""), p_child, p_font)
    {
        _instance = this;
    }

    public static void SetNotifer(bool value) {
        var m_text = Traverse.Create(_instance).Field<string>("m_text");
        if (value && !notify) {
            m_text.Value = m_text.Value + "*";
            notify = true;
        }
        else if (!value && notify) {
            m_text.Value = m_text.Value.Substring(0, m_text.Value.Length-1);
            notify = false;
        }
    }
}