using UnityEngine;
using System;

[Serializable]
public class SUILabel:SUI {
	[SerializeField] private string text;
	[SerializeField] private int size;
	[SerializeField] private Font font;

	private GUIStyle style;

	private bool initialized;

	private void Initialize() {
		style = new GUIStyle();

		initialized = true;
	}

	/** Draw the label */
	public void Draw() {
		if(!activated)
			return;

		if(!initialized)
			Initialize();

		style.font = font;
		style.fontSize = size;

		if(text == null) {
			Debug.LogWarning("A SUI Label is null");
			return;
		}

		if(text.Equals("")) {
			Debug.LogWarning("A SUI Label has an empty value. Deactivate it instead");
			return;
		}

		GUI.Label(new Rect(position.x, position.y, 0, 0), new GUIContent(text), style);
	}

	public string Text {
		set { text = value; }
		get { return text; }
	}
}
