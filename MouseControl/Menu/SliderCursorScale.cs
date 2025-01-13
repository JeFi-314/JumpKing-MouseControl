using System;
using System.Reflection;
using HarmonyLib;
using JumpKing;
using JumpKing.PauseMenu.BT.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using MouseControl.Controller;

namespace MouseControl.Menu;
public class SliderCursorScale : ISlider
{
    const int steps = 9;
    public SliderCursorScale() : base((ModEntry.Prefs.CursorScale-1)/9f)
    {
        FieldInfo STEPS = AccessTools.Field(typeof(ISlider), "STEPS");
        STEPS.SetValue(this, steps);
    }

    protected override void IconDraw(float p_value, int x, int y, out int new_x)
    {
        string text = "Resize";
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
            "x"+Convert(p_value).ToString(),
            new Vector2(new_x + 65, y - offset.Y / 4),
            Color.White);
    }

    protected override void OnSliderChange(float p_value)
    {
        ModEntry.Prefs.CursorScale = Convert(p_value);
        CursorManager.TryLoadTexture(Path.Combine(ModEntry.AssemblyPath, ModEntry.IconsFolder));
        CursorManager.SetCursor("Normal", force: true);
    }

    private int Convert(float p_value) {
        return (int)Math.Round(p_value*9)+1;
    }
}