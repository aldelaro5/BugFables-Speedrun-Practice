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
  }


    [HarmonyPatch(typeof(MainManager), "Reset")]
    public class PatchMainManagerReset
    {
        static void Prefix() => MainManager_Ext.ResetState();

    }
}
