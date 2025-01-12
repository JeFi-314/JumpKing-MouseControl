using HarmonyLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;


using JumpKing.Mods;
using JumpKing.PauseMenu;
using JumpKing.Controller;
using MouseControl.Menu;

// using MouseControl.Menu;

namespace MouseControl;
[JumpKingMod(IDENTIFIER)]
public static class ModEntry
{
    public static int OffsetX {get; private set; }
    public static int OffsetY {get; private set; }
    const string IDENTIFIER = "JeFi.MouseControl";
    const string HARMONY_IDENTIFIER = "JeFi.MouseControl.Harmony";
    const string SETTINGS_FILE = "JeFi.MouseControl.Preferences.xml";

    public static string AssemblyPath { get; set; }
    public static Preferences Pref { get; private set; }

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
            Pref = XmlSerializerHelper.Deserialize<Preferences>($@"{AssemblyPath}\{SETTINGS_FILE}");
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[ERROR] [{IDENTIFIER}] {e.Message}");
            Pref = new Preferences();
        }
        Pref.PropertyChanged += SaveSettingsOnFile;
        Pref.SideRatio = Pref.SideRatio;

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

    [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static ToggleControlDirection ToggleControlDirection(object factory, GuiFormat format)
    {
        return new ToggleControlDirection();
    }

    [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static SliderSideRatio SliderSideRatio(object factory, GuiFormat format)
    {
        return new SliderSideRatio();
    }
    #endregion

        private static void SaveSettingsOnFile(object sender, System.ComponentModel.PropertyChangedEventArgs args)
    {
        try
        {
            XmlSerializerHelper.Serialize($@"{AssemblyPath}\{SETTINGS_FILE}", Pref);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[ERROR] [{IDENTIFIER}] {e.Message}");
        }
    }
}
