using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpeedrunPractice.Extensions
{
  public class PlayerControl_Ext : MonoBehaviour
  {
    public int guiInfoCount = 0;
    public string guiInfoMessage = "";
    public int pdllMenuCursorPos = 0;
		// This is a dead variable...with a cute name!
    public bool meowBenjee = false;
		public static int speed = 5;
		public void PracticeFKeys(PlayerControl __instance)
		{
			if (Input.GetKeyDown(KeyCode.F1))
			{
				MainManager_Ext.drawInfo = !MainManager_Ext.drawInfo;
			}
			if (Input.GetKeyDown(KeyCode.F2))
			{
				MainManager_Ext.showInputDisplay = !MainManager_Ext.showInputDisplay;
			}
			if (Input.GetKeyDown(KeyCode.F3))
			{
				MainManager.Heal();
			}
			if (Input.GetKeyDown(KeyCode.F4))
			{
				MainManager_Ext.toggleInfJump = !MainManager_Ext.toggleInfJump;
				MainManager.PlaySound("Scroll", -1);
				this.guiInfoMessage = "Inf. Jump : " + (MainManager_Ext.toggleInfJump ? "On" : "Off");
				this.guiInfoCount = 1;
			}
			if (Input.GetKeyDown(KeyCode.F5))
			{
				__instance.basespeed = ((__instance.basespeed == 5) ? 10 : 5);
				speed = __instance.basespeed;
				this.guiInfoMessage = "Speed : " + __instance.basespeed;
				this.guiInfoCount = 1;
			}
			if (Input.GetKeyDown(KeyCode.F6) && !MainManager.instance.pause)
			{
				MainManager.Save(new Vector3?(MainManager.player.transform.position));
				MainManager.PlaySound("Save", -1);
				this.guiInfoMessage = "Game saved .";
				this.guiInfoCount = 1;
			}
			if (Input.GetKeyDown(KeyCode.F7) && !MainManager.instance.pause)
			{
				MainManager.ReloadSave();
			}
			if (Input.GetKeyDown(KeyCode.F8))
			{
				SceneManager.LoadScene(0);
				//Application.LoadLevel(0);
			}
			if (Input.GetKeyDown(KeyCode.F9))
			{
				if (MainManager_Ext.pp_TeleportIndex == 0)
				{
					MainManager_Ext.pp_TeleportIndex = 4;
				}
				else
				{
					MainManager_Ext.pp_TeleportIndex--;
				}
				this.guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " selected .";
				this.guiInfoCount = 1;
			}
			if (Input.GetKeyDown(KeyCode.F10))
			{
				if (MainManager_Ext.pp_TeleportIndex == 4)
				{
					MainManager_Ext.pp_TeleportIndex = 0;
				}
				else
				{
					MainManager_Ext.pp_TeleportIndex++;
				}
				this.guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " selected .";
				this.guiInfoCount = 1;
			}
			if (Input.GetKeyDown(KeyCode.F11))
			{
				MainManager_Ext.pp_TeleportArray[MainManager_Ext.pp_TeleportIndex] = MainManager.player.transform.position;
				MainManager.PlaySound("Confirm", -1);
				this.guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " saved .";
				this.guiInfoCount = 1;
			}
			if (Input.GetKeyDown(KeyCode.F12))
			{
				MainManager.player.transform.position = MainManager_Ext.pp_TeleportArray[MainManager_Ext.pp_TeleportIndex];
				MainManager.PlaySound("Confirm", -1);
				this.guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " loaded .";
				this.guiInfoCount = 1;
			}
			if (Input.GetKeyDown(KeyCode.Delete))
			{
				MainManager_Ext.toggleCollision = !MainManager_Ext.toggleCollision;
				__instance.entity.detect.enabled = !MainManager_Ext.toggleCollision;
				__instance.entity.ccol.enabled = !MainManager_Ext.toggleCollision;
				MainManager_Ext.toggleInfJump = MainManager_Ext.toggleCollision;
				MainManager.PlaySound("Scroll", -1);
				this.guiInfoMessage = "Collision : " + (MainManager_Ext.toggleCollision ? "Off" : "On");
				this.guiInfoCount = 1;
			}
			// This is dead code from leftovers on working on a practice menu
			if (this.meowBenjee)
			{
				if (Input.GetKeyDown(KeyCode.Home))
				{
					MainManager_Ext.showPracticeMenu = !MainManager_Ext.showPracticeMenu;
					MainManager.PlaySound("Jump", -1);
					MainManager.instance.pause = !MainManager.instance.pause;
				}
				if (MainManager.GetKey(1, false) && MainManager_Ext.showPracticeMenu)
				{
					if (this.pdllMenuCursorPos == 4)
					{
						this.pdllMenuCursorPos = 0;
					}
					else
					{
						this.pdllMenuCursorPos++;
					}
					MainManager.PlaySound("Scroll", -1);
				}
				if (MainManager.GetKey(0, false) && MainManager_Ext.showPracticeMenu)
				{
					if (this.pdllMenuCursorPos == 0)
					{
						this.pdllMenuCursorPos = 4;
					}
					else
					{
						this.pdllMenuCursorPos--;
					}
					MainManager.PlaySound("Scroll", -1);
				}
				if (MainManager.GetKey(4, false) && MainManager_Ext.showPracticeMenu)
				{
					MainManager.PlaySound("Confirm", -1);
				}
				if (MainManager.GetKey(5, false) && MainManager_Ext.showPracticeMenu)
				{
					MainManager.PlaySound("Cancel", -1);
				}
			}
		}

		public void GUI_DrawInfoBox(PlayerControl __instance, GUIStyle guiStyle)
		{
			if (this.guiInfoCount <= 0)
			{
				return;
			}
			GUI.Box(new Rect(0f, (float)Screen.height - 245f, 115f, 32f), this.guiInfoMessage, guiStyle);
			this.guiInfoCount++;
			if (this.guiInfoCount > 180)
			{
				this.guiInfoCount = 0;
				this.guiInfoMessage = "";
			}
		}

		public void GUI_DrawInputDisplay(PlayerControl __instance, GUIStyle guiStyle)
		{
			if (!MainManager_Ext.showInputDisplay)
			{
				return;
			}
			guiStyle.fontSize = 22;
			guiStyle.fontStyle = FontStyle.Bold;
			float num = (float)Screen.height - 42f;
			GUI.Box(new Rect(0f, num, 220f, 42f), "", guiStyle);
			guiStyle.normal.background = null;
			if (MainManager.GetKey(2, true))
			{
				GUI.Box(new Rect(0f, num, 32f, 42f), "←", guiStyle);
			}
			if (MainManager.GetKey(0, true))
			{
				GUI.Box(new Rect(26f, num, 32f, 42f), "↑", guiStyle);
			}
			if (MainManager.GetKey(1, true))
			{
				GUI.Box(new Rect(42f, num, 32f, 42f), "↓", guiStyle);
			}
			if (MainManager.GetKey(3, true))
			{
				GUI.Box(new Rect(58f, num, 32f, 42f), "→", guiStyle);
			}
			if (MainManager.GetKey(4, true))
			{
				GUI.Box(new Rect(106f, num, 32f, 42f), "J", guiStyle);
			}
			if (MainManager.GetKey(5, true))
			{
				GUI.Box(new Rect(128f, num, 32f, 42f), "A", guiStyle);
			}
			if (MainManager.GetKey(6, true))
			{
				GUI.Box(new Rect(154f, num, 32f, 42f), "S", guiStyle);
			}
			if (MainManager.GetKey(9, true))
			{
				GUI.Box(new Rect(178f, num, 32f, 42f), "En", guiStyle);
			}
		}

		public void GUI_DrawPMAInfo(PlayerControl __instance, GUIStyle guiStyle)
		{
			if (!MainManager_Ext.drawInfo)
			{
				return;
			}
			float x = guiStyle.CalcSize(new GUIContent("Area ID : " + MainManager.map.areaid.ToString())).x;
			float x2 = guiStyle.CalcSize(new GUIContent("Map ID : " + MainManager.map.mapid.ToString())).x;
			float num = Math.Max(x, x2) + 8f;
			if (num < 180f)
			{
				num = 180f;
			}

			var getAngleRef = AccessTools.Method(typeof(PlayerControl), "GetAngle");

			GUI.Box(new Rect(0f, (float)Screen.height - 205f, num, 145f), 
				string.Format("Area ID : {0}\nMap ID : {1}\n\nX : {2}\nY : {3}\nZ : {4}\nAngle: {5}", new object[]
					{
						MainManager.map.areaid,
						MainManager.map.mapid,
						MainManager.player.transform.position.x,
						MainManager.player.transform.position.y,
						MainManager.player.transform.position.z,
						getAngleRef.Invoke(__instance, null).ToString()
					}
				), guiStyle);
		}

		// Dead code, this menu will never show up as it's leftovers from a WIP
		// on making a menu
		public void GUI_DrawPracticeMenu(PlayerControl __instance, GUIStyle guiStyle)
		{
			if (!MainManager_Ext.showPracticeMenu)
			{
				return;
			}
			GUIStyle guistyle = new GUIStyle();
			guistyle.fontSize = 16;
			guistyle.font = MainManager.fonts[0];
			guistyle.normal.textColor = Color.white;
			guistyle.padding = new RectOffset(40, 0, 8, 0);
			float num = (float)Screen.width - 180f;
			float num2 = (float)Screen.height - 300f;
			string text = "\n\n";
			for (int i = 0; i < this.pdllMenuCursorPos; i++)
			{
				if (i == 3)
				{
					text += "\n";
				}
				text += "\n";
			}
			text += "►";
			GUI.Label(new Rect(num, num2, 180f, 169f), "- Practice Menu -" + text, guiStyle);
			GUI.Box(new Rect(num, num2, 180f, 169f), "\n\nParty Stats\nItems\nMedals\nPosition\n\nHelp", guistyle);
		}

		public void OnGUI()
		{
			var playerControl = this.gameObject.GetComponent<PlayerControl>();
			GUIStyle guistyle = new GUIStyle();
			guistyle.fontSize = 16;
			guistyle.font = MainManager.fonts[0];
			guistyle.normal.textColor = Color.white;
			guistyle.padding = new RectOffset(8, 0, 8, 0);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 0.45f));
			texture2D.Apply();
			guistyle.normal.background = texture2D;
			this.GUI_DrawInfoBox(playerControl, guistyle);
			this.GUI_DrawPMAInfo(playerControl, guistyle);
			this.GUI_DrawPracticeMenu(playerControl, guistyle);
			this.GUI_DrawInputDisplay(playerControl, guistyle);
		}
	}
}
