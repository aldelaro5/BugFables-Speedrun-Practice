using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(GroundDetector), "OnTriggerStay")]
    public class PatchGroundDetectorOnTriggerStay
    {
        static bool Prefix(GroundDetector __instance) => __instance.enabled;
    }
}
