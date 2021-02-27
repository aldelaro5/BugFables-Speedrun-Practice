using BepInEx;
using HarmonyLib;

namespace SpeedrunPractice
{
  [BepInPlugin("com.aldelaro5.BugFables.plugins.SpeedrunPractice", "Speedrun Practice", "3.0.1")]
  [BepInProcess("Bug Fables.exe")]
  public class SpeedrunPracticePlugin : BaseUnityPlugin
  {
    void Awake()
    {
      var harmony = new Harmony("com.aldelaro5.BugFables.harmony.SpeedrunPractice");
      harmony.PatchAll();
    }
  }
}
