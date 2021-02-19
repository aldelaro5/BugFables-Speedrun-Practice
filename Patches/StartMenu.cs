using HarmonyLib;
using SpeedrunPractice.Extensions;
using UnityEngine;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(StartMenu), "Start")]
    public class PatchStartMenuStart
    {
        static bool Prefix(PlayerControl __instance)
        {
            MainManager_Ext.pp_TeleportArray = new Vector3[5];
            __instance.gameObject.AddComponent<StartMenu_Ext>();

            return true;
        }
    }
}
