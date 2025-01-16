using HarmonyLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using JumpKing.Mods;
using JumpKing.PauseMenu;
using MouseControl.Menu;
using MouseControl.Nodes;
using MouseControl.Controller;
using JumpKing.PauseMenu.BT;

namespace MouseControl;
[JumpKingMod(IDENTIFIER)]
public static class MouseControl
{
    const string IDENTIFIER = "JeFi.MouseControl";
    const string HARMONY_IDENTIFIER = "JeFi.MouseControl.Harmony";
    const string SETTINGS_FILE = "JeFi.MouseControl.Preferences.xml";

    public static string AssemblyPath { get; set; }
    public static string IconsFolder = "icons";
    public static Preferences Prefs { get; private set; }

    /// <summary>
    /// Called by Jump King before the level loads
    /// </summary>
    [BeforeLevelLoad]
    public static void BeforeLevelLoad()
    {
        AssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
#if DEBUG
        Debugger.Launch();
        Debug.WriteLine("------");
        Harmony.DEBUG = true;
        Environment.SetEnvironmentVariable("HARMONY_LOG_FILE", $@"{AssemblyPath}\harmony.log.txt");
#endif
        try
        {
            Prefs = XmlSerializerHelper.Deserialize<Preferences>($@"{AssemblyPath}\{SETTINGS_FILE}");
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[ERROR] [{IDENTIFIER}] {e.Message}");
            Prefs = new Preferences();
        }
        Prefs.PropertyChanged += SaveSettingsOnFile;
        Prefs.SideRatio = Prefs.SideRatio;

        CursorManager.SetVisible(Prefs.isEnable && Prefs.isShowCursor);
		CursorManager.SetBoundCursor(Prefs.isEnable && Prefs.isBoundCursor);
        CursorManager.TryLoadTexture(Path.Combine(AssemblyPath, IconsFolder));
		CursorManager.SetCursor("Normal", force: true);
        MousePad.Binding = Prefs.MouseBinding;

        Harmony harmony = new Harmony(HARMONY_IDENTIFIER);

        try {
            new Patching.ControllerManager(harmony);
        }
        catch (Exception e) {
            Debug.WriteLine(e.ToString());

            // Debug.WriteLine($"Message: {e.Message}");
            // Debug.WriteLine($"Stack Trace: {e.StackTrace}");

            // if (e.InnerException != null)
            // {
            //         Debug.WriteLine("Inner Exception:");
            //         Debug.WriteLine(e.InnerException.ToString());
            // }
            throw e;
        }

#if DEBUG
        Environment.SetEnvironmentVariable("HARMONY_LOG_FILE", null);
#endif
    }

    #region Menu Items
    [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static ToggleMouseControl ToggleMouseControl(object factory, GuiFormat format)
    {
        return new ToggleMouseControl();
    }

    [PauseMenuItemSetting]
    [MainMenuItemSetting]
    public static TextButton BindControls(object factory, GuiFormat format)
    {
        return new TextButton("Controls", new MenuBindControls(factory));
    }

    [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static ToggleShowCursor ToggleShowCursor(object factory, GuiFormat format)
    {
        return new ToggleShowCursor();
    }

    [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static ToggleBoundCursor ToggleClipCursor(object factory, GuiFormat format)
    {
        return new ToggleBoundCursor();
    }

    // [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static ToggleControlDirection ToggleControlDirection(object factory, GuiFormat format)
    {
        return new ToggleControlDirection();
    }

    // [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static SliderSideRatio SliderSideRatio(object factory, GuiFormat format)
    {
        return new SliderSideRatio();
    }

    [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static SliderCursorScale SliderCursorScale(object factory, GuiFormat format)
    {
        return new SliderCursorScale();
    }
    #endregion

    private static void SaveSettingsOnFile(object sender, System.ComponentModel.PropertyChangedEventArgs args)
    {
        try
        {
            XmlSerializerHelper.Serialize($@"{AssemblyPath}\{SETTINGS_FILE}", Prefs);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[DEBUG] [{IDENTIFIER}] {e.Message}");
        }
    }
}
