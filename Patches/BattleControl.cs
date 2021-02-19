using HarmonyLib;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(BattleControl), "Update")]
    public class PatchBattleControlUpdate
    {
        static bool Prefix(BattleControl __instance)
        {
            if (__instance.GetComponent<BattleControl_Ext>() == null)
                MainManager.battle.gameObject.AddComponent<BattleControl_Ext>();

            if (!__instance.cancelupdate && MainManager.pausemenu == null && MainManager.instance.inbattle)
                __instance.GetComponent<BattleControl_Ext>().PracticeFKeys();

            return true;
        }
    }
}
