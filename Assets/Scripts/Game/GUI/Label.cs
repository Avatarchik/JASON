using UnityEngine;
using System.Collections;

public class Label {
	private static ArrayList labels = new ArrayList();

	private GUIContent content;
	private GUIStyle style;

	private Rect position;
	
	/** Create a new label */
	public Label(Vector2 position, string text):this(position, new GUIContent(text, "")) { }
	
	/** Create a new label */
	public Label(Vector2 position, GUIContent content):this(position, content, new GUIStyle()) { }
	
	/** Create a new label */
	public Label(Vector2 position, GUIContent content, GUIStyle style) {
		this.position = new Rect(position.x, position.y, 0, 0);
		this.content = content;
		this.style = style;
		
		labels.Add(this);
	}
	
	public GUIContent Content { get { return content; } }
	
	public GUIStyle Style { get { return style; } }
	
	public Rect Position { get { return position; } }
	
	/** Draw all labels that are registered */
	public static void Update() {
		if(!DebugSettings.Instance.DisableGUI) {
			foreach(Label label in labels) {
				GUI.Label(label.Position, label.Content, label.Style);
			}
		}
	}
	
	/** Dispose of all labels */
	public static void Clear() {
		labels.Clear();
	}
}
