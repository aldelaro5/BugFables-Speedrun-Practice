using UnityEngine;

namespace SpeedrunPractice.Extensions
{
    public class MainManager_Ext : MonoBehaviour
    {
        public static bool drawInfo = false;
        public static bool toggleInfJump = false;
        public static int pp_TeleportIndex = 0;
        public static Vector3[] pp_TeleportArray = null;
        public static bool showInputDisplay = false;
        public static bool showPracticeMenu = false;
        public static bool toggleCollision = false;

        public static void ResetState()
        {
            drawInfo = false;
            toggleInfJump = false;
            pp_TeleportIndex = 0;
            pp_TeleportArray = null;
            showInputDisplay = false;
            showPracticeMenu = false;
            toggleCollision = false;
            PlayerControl_Ext.speed = 5;
        }
    }
}
