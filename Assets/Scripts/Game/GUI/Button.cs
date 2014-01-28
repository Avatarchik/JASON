using UnityEngine;
using System.Collections;

public delegate void Callback(Button button);

public class Button {
	private static ArrayList buttons = new ArrayList();

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
		
		buttons.Add(this);
	}
	
	/** Create a button */
	public Button(Vector2 position, GUIContent content, GUIStyle style, Callback callback) {
		this.position = new Rect(position.x, position.y, style.normal.background.width, style.normal.background.height);
		this.callback += callback;
		this.content = content;
		this.style = style;
		
		buttons.Add(this);
	}
	
	public GUIContent Content { get { return content; } }
	
	public GUIStyle Style { get { return style; } }
	
	public Rect Position { get { return position; } }
	
	/** Draw all buttons that are registered */
	public static void Update() {
		if(!DebugSettings.Instance.DisableGUI) {
			foreach(Button button in buttons) {
				if(button.callback == null)
					throw new System.ArgumentException("The callback of a button can\'t be null");
				
				if(!DebugSettings.Instance.DisableGUI) {
					if(GUI.Button(button.position, button.content, button.style)) {
						button.callback(button);
					}
				}
			}
		}
	}
	
	/** Dispose of all buttons */
	public static void Clear() {
		buttons.Clear();
	}
}
