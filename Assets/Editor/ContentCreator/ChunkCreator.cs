using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ChunkCreator:EditorWindow {
	public const string savePath = "Editor/Content Creator/Saved Chunks/";

	[MenuItem("Content Creator/Chunk Creator")]
	static void Init() {
		ChunkCreator window = (ChunkCreator)EditorWindow.GetWindow(typeof(ChunkCreator));
		
		window.title = "Chunk Creator";
		
		window.Show();
		window.Focus();
	}
}
