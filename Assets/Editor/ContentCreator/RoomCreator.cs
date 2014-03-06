using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RoomCreator:EditorWindow {
	public const string savePath = "Editor/Content Creator/Saved Rooms/";
	
	[MenuItem("Content Creator/Room Creator")]
	static void Init() {
		RoomCreator window = (RoomCreator)EditorWindow.GetWindow(typeof(RoomCreator));
		
		window.title = "Room Creator";
		
		window.Show();
		window.Focus();
	}
}
