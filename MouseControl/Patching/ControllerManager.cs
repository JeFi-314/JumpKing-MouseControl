using System;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib;
using JumpKing.Controller;
using MouseControl.Controller;
using JK = JumpKing.Controller;

namespace MouseControl.Patching;

public class ControllerManager {
    public ControllerManager(Harmony harmony) {
        Type type = typeof(JK.ControllerManager);

        MethodInfo Update = type.GetMethod(nameof(JK.ControllerManager.Update));
        harmony.Patch(
            Update,
            prefix: new HarmonyMethod(typeof(ControllerManager), nameof(preUpdate))
        );

        MethodInfo GetPadState = type.GetMethod(nameof(JK.ControllerManager.GetPadState));
        harmony.Patch(
            GetPadState,
            postfix: new HarmonyMethod(typeof(ControllerManager), nameof(postGetPadState))
        );

        MethodInfo GetPressedPadState = type.GetMethod(nameof(JK.ControllerManager.GetPressedPadState));
        harmony.Patch(
            GetPressedPadState,
            postfix: new HarmonyMethod(typeof(ControllerManager), nameof(postGetPressedPadState))
        );
    }

    private static void preUpdate() {
        MousePad.Update();
    }

    private static void postGetPadState(JK.ControllerManager __instance, ref PadState __result) {
        __result = Traverse.Create(__instance).Method("Combine", new PadState[] {__result, MousePad.GetState()}).GetValue<PadState>();
    }

    private static void postGetPressedPadState(JK.ControllerManager __instance, ref PadState __result) {
        __result = Traverse.Create(__instance).Method("Combine", new PadState[] {__result, MousePad.GetPressed()}).GetValue<PadState>();
    }
}