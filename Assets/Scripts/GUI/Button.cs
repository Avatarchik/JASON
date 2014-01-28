using UnityEngine;
using System.Collections;

public delegate void Callback(Button button);

public class Button {
	private event Callback callback;

	private GUIContent content;
	private GUIStyle style;
	
	private Rect position;

	/** Create a button */
	public Button(Vector2 position, Texture2D normal, Callback callback):this(position, normal, null, callback) { }
	
	/** Create a button */
	public Button(Vector2 position, Texture2D normal, Texture2D hover, Callback callback):this(position, normal, hover, null, callback) { }
	
	/** Create a button */
	public Button(Vector2 position, Texture2D normal, Texture2D hover, Texture2D down, Callback callback):this(position, normal, hover, down, new GUIContent("", ""), callback) { }
	
	/** Create a button */
	public Button(Vector2 position, Texture2D normal, Texture2D hover, Texture2D down, GUIContent content, Callback callback) {
		this.position = new Rect(position.x, position.y, normal.width, normal.height);
		this.callback += callback;
		this.content = content;
		
		style = new GUIStyle();
		style.normal.background = normal;
		style.hover.background = hover;
		style.active.background = down;
	}
	
	/** Create a button */
	public Button(Vector2 position, GUIContent content, GUIStyle style, Callback callback) {
		this.position = new Rect(position.x, position.y, style.normal.background.width, style.normal.background.height);
		this.callback += callback;
		this.content = content;
		this.style = style;
	}
	
	/** Update the button to check for clicks. Must be called from the OnGUI method */
	public void Update() {
		if(callback == null)
			throw new System.ArgumentException("The callback of the button can\'t be null");
			
		if(GUI.Button(position, content, style)) {
			callback(this);
		}
	}
	
	public GUIContent Content { get { return content; } }
	
	public GUIStyle Style { get { return style; } }
	
	public Rect Position { get { return position; } }
}
