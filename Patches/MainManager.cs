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

        static void Postfix(MainManager __instance) => MainManager.MainCamera.gameObject.AddComponent<FreeCam>();
    }

    [HarmonyPatch(typeof(MainManager), "Reset")]
    public class PatchMainManagerReset
    {
        static bool Prefix()
        {
            MainManager_Ext.drawInfo = false;
            MainManager_Ext.pp_TeleportIndex = 0;
            MainManager_Ext.showInputDisplay = false;
            MainManager_Ext.showPracticeMenu = false;
            MainManager_Ext.toggleCollision = false;
            MainManager_Ext.toggleInfJump = false;
            PlayerControl_Ext.speed = 5;
            return true;
        }
    }

    [HarmonyPatch(typeof(MainManager), "RefreshCamera")]
    public class PatchMainManagerRefreshCamera
    {
        static bool Prefix()
        {
            return !MainManager_Ext.toggleFreeCam;
        }
    }
  }


    [HarmonyPatch(typeof(MainManager), "Reset")]
    public class PatchMainManagerReset
    {
        static void Prefix() => MainManager_Ext.ResetState();

    }
}
