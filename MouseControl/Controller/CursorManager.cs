using Microsoft.Xna.Framework.Input;
using JumpKing;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


namespace MouseControl.Controller;

public static class CursorManager
{
	private static string currentIcon;
	private static readonly Dictionary<string, MouseCursor> DefaultIcons;
	private static readonly Dictionary<string, MouseCursor> CustomIcons;
	private static bool isCliped;
	static CursorManager() {
		currentIcon = "normal";
		DefaultIcons = new Dictionary<string, MouseCursor>
        {
            {"Left", MouseCursor.SizeNWSE },
            {"Normal", MouseCursor.SizeNS },
            {"Right", MouseCursor.SizeNESW },
            {"LeftWalk", MouseCursor.SizeWE },
            {"RightWalk", MouseCursor.SizeWE },
            {"LeftJump", MouseCursor.Wait },
            {"NormalJump", MouseCursor.WaitArrow },
            {"RightJump", MouseCursor.Wait }
        };
		CustomIcons = new Dictionary<string, MouseCursor>();
	}

	public static void SetVisible(bool value) {
		Game1.instance.IsMouseVisible = value;
	}

	public static bool SetCursor(string name, bool force=false) {
		if (!force && currentIcon==name) return true;
		if (CustomIcons.ContainsKey(name)) {
			Mouse.SetCursor(CustomIcons[name]);
			currentIcon = name;
			return true;
		}
		else if (DefaultIcons.ContainsKey(name)) {
			Mouse.SetCursor(DefaultIcons[name]);
			currentIcon = name;
			return true;
		}
		return false;
	}

	public static void AddTexture(string name, Texture2D texture, int x, int y) {
		CustomIcons[name] = MouseCursor.FromTexture2D(
			texture: texture,
			originx: x,
			originy: y
		);
	}
	public static bool RemoveTexture(string name) {
		return CustomIcons.Remove(name);
	}
	public static void TryLoadTexture(string folder) {
		if (!Directory.Exists(folder)) {
			Debug.WriteLine($"[DEBUG] Folder not found: {folder}");
			return;
		}

		JKContentManager contentManager = Game1.instance.contentManager;
		foreach (var icon in DefaultIcons)
		{
			string filePath = Path.Combine(folder, icon.Key + ".xnb");

			try
			{
				if (File.Exists(filePath))
				{
					Texture2D texture = contentManager.Load<Texture2D>(Path.Combine(folder, icon.Key));

					int centerX = texture.Width / 2;
					int centerY = texture.Height / 2;

					AddTexture(icon.Key, texture, centerX, centerY);
				}
				else
				{
					Debug.WriteLine($"[DEBUG] Texture file not found: {filePath}");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[DEBUG] Error loading texture '{icon.Key}': {ex.Message}");
			}
		}
	}

    public static void SetClipCursor(bool value)
    {
        if (value && !isCliped) {
			onWindowSizeChange(null, EventArgs.Empty);
			Game1.instance.Window.ClientSizeChanged += onWindowSizeChange;
			isCliped = true;
			Debug.WriteLine($"[DEBUG] Cursor clipped)");
        }
        else if (!value && isCliped)
        {
			Cursor.Clip = System.Drawing.Rectangle.Empty;
			Game1.instance.Window.ClientSizeChanged -= onWindowSizeChange;
			isCliped = false;
			Debug.WriteLine($"[DEBUG] Cursor unclipped)");
        }
    }

    public static void onWindowSizeChange(object sender, EventArgs e)
    {
        var bound = Game1.instance.Window.ClientBounds;

        System.Drawing.Rectangle drawingBounds = new System.Drawing.Rectangle(
            bound.X, bound.Y, bound.Width, bound.Height
        );

        Cursor.Clip = drawingBounds;

        Debug.WriteLine($"[DEBUG] Window bounds: ({drawingBounds.X}, {drawingBounds.Y})~({drawingBounds.X+drawingBounds.Width}, {drawingBounds.Y+drawingBounds.Height})");
    }
}
