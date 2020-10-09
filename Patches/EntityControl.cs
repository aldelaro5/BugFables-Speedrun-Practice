using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedrunPractice.Patches
{
  [HarmonyPatch(typeof(EntityControl), "Unfix", new Type[] { typeof(bool) })]
  public class PatchEntityControlUnfix
  {
    static bool Prefix(EntityControl __instance, bool force)
    {
      if (!MainManager_Ext.toggleCollision && __instance.name == "Player 0")
        return true;

      return false;
    }
  }
}
