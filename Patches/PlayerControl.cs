using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;
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
    // Instead of being dependant on onground and coyote time, it will first
    // check a custom field and it will take priority over the in game checks
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
            // Here, we have found the spot to patch, it's 2 instruction before the ldfld of onground
            indexToInsertPatch = i - 2;
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

      // What we do is add a jump before onground is checked that will skip its check 
      // (as well as the coyote time checks) so if our custom field is true, 
      // the jump will go through
      Console.WriteLine(instructionsList[indexToInsertPatch].opcode);
      var lbl = generator.DefineLabel();
      instructionsList.Insert(indexToInsertPatch, new CodeInstruction(OpCodes.Ldsfld, typeof(MainManager_Ext).GetField("toggleInfJump")));
      instructionsList.Insert(indexToInsertPatch + 1, new CodeInstruction(OpCodes.Brtrue, lbl));
      Console.WriteLine(instructionsList[indexToInsertPatch].opcode);
      Console.WriteLine(instructionsList[indexToInsertPatch + 1].opcode);
      Console.WriteLine(instructionsList[indexToInsertPatch + 2].opcode);

      for (int i = indexToInsertPatch; i < instructionsList.Count; i++)
      {
        var inst = instructionsList[i];
        if (inst.operand == typeof(MainManager).GetField("jumpcooldown"))
        {
          instructionsList[i + 3].labels.Add(lbl);
          Console.WriteLine(instructionsList[i + 3].opcode);
          break;
        }
      }

      return instructionsList;
    }
  }
}
