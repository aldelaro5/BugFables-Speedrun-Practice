using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(EntityControl), "Unfix", new Type[] { typeof(bool) })]
    public class PatchEntityControlUnfix
    {
        static void Postfix(EntityControl __instance)
        {
            if (__instance.playerentity)
                __instance.ccol.enabled = !MainManager_Ext.toggleCollision;
        }
    }
}
