using System.Reflection;
using HarmonyLib;
using JumpKing;
using JumpKing.PauseMenu.BT.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MouseControl.Menu;
public class SliderSideRatio : ISlider
{
    const int steps = 16;
    public SliderSideRatio() : base((ModEntry.Pref.SideRatio-0.1f)/0.8f)
    {
        FieldInfo STEPS = AccessTools.Field(typeof(ISlider), "STEPS");
        STEPS.SetValue(this, steps);
    }

    protected override void IconDraw(float p_value, int x, int y, out int new_x)
    {
        string text = "SideArea";
        SpriteFont font = Game1.instance.contentManager.font.MenuFont;
        Point offset = font.MeasureString(text).ToPoint();
        Game1.spriteBatch.DrawString(
            font,
            text,
            new Vector2(x, y - offset.Y / 4),
            Color.White);
        new_x = x + offset.X + 5;
        Game1.spriteBatch.DrawString(
            font,
            (10+(int)(80*p_value)).ToString()+"%",
            new Vector2(new_x + 65, y - offset.Y / 4),
            Color.White);
    }

    protected override void OnSliderChange(float p_value)
    {
        ModEntry.Pref.SideRatio = 0.1f+0.8f*p_value;
    }
}