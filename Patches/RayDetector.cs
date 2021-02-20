using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(RayDetector), "OnTriggerStay")]
    public class PatchRayDetectorOnTriggerStay
    {
        static bool Prefix(RayDetector __instance) => !MainManager_Ext.toggleCollision;
    }
}
