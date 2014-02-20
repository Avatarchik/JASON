using UnityEngine;
using UnityEditor;

public class HelpBox {
	private string message;
	private MessageType type;

	public HelpBox(string message, MessageType type) {
		this.message = message;
		this.type = type;
	}

	public void Render() {
		EditorGUILayout.HelpBox(message, type);
	}
}
