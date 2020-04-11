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
}
