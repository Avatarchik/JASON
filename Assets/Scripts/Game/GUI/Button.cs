using UnityEngine;
using System;

[Serializable]
public class Button {
	[SerializeField] private Vector2 position;
	
	[SerializeField] private Texture2D textureNormal;
	[SerializeField] private Texture2D textureHover;
	[SerializeField] private Texture2D textureActive;
	
	[SerializeField] private string text;
	[SerializeField] private string tooltip;
	
	[SerializeField] private Texture2D textureToggle;

	private GUIContent content;
	private GUIStyle style;
	
	private bool toggled;
	
	/** Check if the button is clicked, return true if it is */
	public bool Clicked {
		get {
			if(style == null)
				InitializeStyle();
				
			if(content == null)
				InitializeContent();
			
			if(!DebugSettings.Instance.DisableGUI) {
				uint width = 0;
				uint height = 0;
			
				if(style.normal.background != null) {
					width = (uint)style.normal.background.width;
					height = (uint)style.normal.background.height; 
				} else if(style.hover.background != null) {
					width = (uint)style.hover.background.width;
					height = (uint)style.hover.background.height; 
				} else if(style.active.background != null) {
					width = (uint)style.active.background.width;
					height = (uint)style.active.background.height; 
				}
			
				if(GUI.Button(new Rect(position.x, position.y, width, height), content, style)) {
					return true;
				} else {
					return false;
				}
			}
			
			return false;
		}
	}
	
	public bool Toggled {
		get {
			if(Clicked) {
				if(textureToggle != null && style.normal.background != textureToggle) { 
					style.normal.background = textureToggle;
				} else if(style.normal.background == textureToggle) {
					style.normal.background = textureNormal;
				}
				
				toggled = !toggled;
			}
							
			return toggled;
		}
	}
	
	/** Return the content of the button */
	public GUIContent Content {
		get {
			if(content == null)
				InitializeContent();
		
			return content;
		}
	}
	
	/** Return the style of the button */
	public GUIStyle Style {
		get {
			if(style == null)
				InitializeStyle();
		
			return style;
		}
	}	
	
	/** Return the position of the button */
	public Vector2 Position { get { return position; } }
	
	/** Initialize the GUIStyle */
	private void InitializeStyle() {
		style = new GUIStyle();
		
		style.normal.background = textureNormal;
		style.hover.background = textureHover;
		style.active.background = textureActive;
	}
	
	/** Initialize the GUIContent */
	private void InitializeContent() {
		if(text == null)
			text = "";
		
		if(tooltip == null)
			tooltip = "";
	
		content = new GUIContent(text, tooltip);
	}
}
