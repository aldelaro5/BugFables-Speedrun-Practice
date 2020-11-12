using BepInEx;
using System.Reflection;
using UnityEngine;

namespace SpeedrunPractice.Extensions
{
  public class StartMenu_Ext : MonoBehaviour
  {
		private void OnGUI()
		{
			GUIStyle guistyle = new GUIStyle();
			guistyle.fontSize = 16;
			guistyle.font = MainManager.fonts[0];
			guistyle.normal.textColor = new Color(0.65f, 0.65f, 1f);
			guistyle.padding = new RectOffset(8, 0, 8, 0);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 0.45f));
			texture2D.Apply();
			guistyle.normal.background = texture2D;
			var pluginMetadata = MetadataHelper.GetMetadata(typeof(SpeedrunPracticePlugin));
			GUI.Box(new Rect(0f, (float)Screen.height - 98f, 400f, 54f), 
				"Practice DLL v" + pluginMetadata.Version + 
				"\nby Benjee, wataniyob, Wintar, Lyght, Cyan627 and aldelaro5", guistyle);
		}
	}
}
