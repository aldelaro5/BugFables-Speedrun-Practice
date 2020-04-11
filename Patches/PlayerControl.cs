using HarmonyLib;
using SpeedrunPractice.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace SpeedrunPractice.Patches
{
  [HarmonyPatch(typeof(PlayerControl), "Start")]
  public class PatchPlayerControlStart
  {
    static bool Prefix(PlayerControl __instance)
    {
      __instance.gameObject.AddComponent<PlayerControl_Ext>();

      return true;
    }
  }

  [HarmonyPatch(typeof(PlayerControl), "Update")]
  public class PatchPlayerControlUpdate
  {
    static bool Prefix(PlayerControl __instance)
    {
      if (!MainManager.instance.minipause && !MainManager.instance.message)
      {
        var playerControlExt = __instance.gameObject.GetComponent<PlayerControl_Ext>();
        playerControlExt.PracticeFKeys(__instance);
      }

      return true;
    }
  }

  [HarmonyPatch(typeof(PlayerControl), "GetInput")]
  public class PatchPlayerControlGetInput
  {
    // This patches an if condition that determines if the player is allowed to jump.
    // Instead of being dependant on onground, it adds an OR condition with our custom
    // toggleInfJump field to allow jumping if it's true regardless of onground
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
      bool patternFound = false;
      var instructionsList = instructions.ToList();
      int indexToInsertPatch = -1;
      for (int i = 0; i < instructionsList.Count; i++)
      {
        var inst = instructionsList[i];
        if (patternFound)
        {
          if (inst.operand == typeof(EntityControl).GetField("onground"))
          {
            // Here, we have found the spot to patch, it's one instruction below the ldfld
            indexToInsertPatch = i + 1;
            break;
          }
        }
        // There is only one case where the field MainManger.inevent is read and it's
        // in the outer if of the one we want to patch. What we want to do is look for
        // our if AFTER this outer if
        else if (inst.operand == typeof(MainManager).GetField("inevent"))
        {
          patternFound = true;
        }
      }

      // The IL looks like this normally:
      // IL_0379: ldfld     bool EntityControl::onground
      // IL_037E: brfalse IL_04C7 (this means to not enter the if block)
      // IL_0383: ldc.i4.4
      // What we do is add a jump to IL_0383 if onground is true, but if it's not, this is 
      // where we load our custom field and the instruction at IL_037E will now be dependant
      // on our custom field.
      var lbl = generator.DefineLabel();
      instructionsList.Insert(indexToInsertPatch, new CodeInstruction(OpCodes.Brtrue, lbl));
      instructionsList.Insert(indexToInsertPatch + 1, new CodeInstruction(OpCodes.Ldsfld, typeof(MainManager_Ext).GetField("toggleInfJump")));
      instructionsList[indexToInsertPatch + 3].labels.Add(lbl);

      return instructionsList;
    }
  }
}
