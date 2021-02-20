using HarmonyLib;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(MainManager), "Start")]
    public class PatchMainManagerStart
    {
        static bool Prefix(PlayerControl __instance)
        {
            __instance.gameObject.AddComponent<MainManager_Ext>();
            return true;
        }

        static void Postfix() => MainManager.MainCamera.gameObject.AddComponent<FreeCam>();
    }

    [HarmonyPatch(typeof(MainManager), "RefreshCamera")]
    public class PatchMainManagerRefreshCamera
    {
        static bool Prefix() => !MainManager_Ext.toggleFreeCam;
    }
}
