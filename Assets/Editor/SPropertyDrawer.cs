using UnityEngine;
using UnityEditor;
using System.Collections;

public class SPropertyDrawer:PropertyDrawer {
	protected float height;

	protected Rect position;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

	}

	protected void DrawProperty(string name) {
		DrawProperty(name, new GUIContent(name));
	}

	protected void DrawProperty(string name, GUIContent label) {

	}

	protected void DrawProperties(string[] names) {
		GUIContent[] labels = new GUIContent[names.Length];

		DrawProperties(names, labels);
	}

	protected void DrawProperties(string[] names, GUIContent[] labels) {

	}

	protected void DrawLabel(string label) {

	}
}
