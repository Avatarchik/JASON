using UnityEngine;
using System;

[Serializable]
public class Button {
	public Texture2D textureNormal;
	public Texture2D textureHover;
	public Texture2D textureActive;
	public Vector2 position;

	private GUIContent content;
	private GUIStyle style;
	
	private bool isClicked;
	
	void Start() {
		content = new GUIContent("", "");
		style = new GUIStyle();
	
		style.normal.background = textureNormal;
		style.hover.background = textureHover;
		style.active.background = textureActive;
	}
	
	void OnGUI() {		
		if(!DebugSettings.Instance.DisableGUI) {
			if(GUI.Button(new Rect(position.x, position.y, style.normal.background.width, style.normal.background.height), content, style)) {
				isClicked = true;
			} else {
				isClicked = false;
			}
		}
	}
	
	public GUIContent Content { get { return content; } }
	
	public GUIStyle Style { get { return style; } }
	
	public bool IsClicked { get { return isClicked; } }
}
