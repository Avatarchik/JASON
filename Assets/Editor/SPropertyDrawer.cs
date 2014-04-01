using UnityEngine;
using UnityEditor;
using System.Collections;

public class SPropertyDrawer:PropertyDrawer {
	protected float height;

	protected int oldIndent;

	protected Rect position;
	protected Rect oldPosition;

	protected SerializedProperty property;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		position.height = 16f;
		
		label = EditorGUI.BeginProperty(position, label, property);
		property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
		EditorGUI.EndProperty();

		position = EditorGUI.IndentedRect(position);
		oldIndent = EditorGUI.indentLevel;

		EditorGUI.indentLevel = 1;

		this.position = position;
		this.oldPosition = position;
		this.property = property;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return property.isExpanded ? height : 16f;	
	}

	protected void DrawProperty(string name) {
		DrawProperty(name, new GUIContent(char.ToUpper(name[0]) + name.Substring(1)));
	}

	protected void DrawProperty(string name, GUIContent label) {
		position.x = oldPosition.x;
		position.y += 19f;
		position.width = oldPosition.width;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative(name), label);
	}

	protected void DrawProperties(string[] names) {
		GUIContent[] labels = new GUIContent[names.Length];

		for(int i = 0; i < names.Length; i++)
			labels[i] = new GUIContent(char.ToUpper(names[i][0]) + names[i].Substring(1));

		DrawProperties(names, labels);
	}

	protected void DrawProperties(string[] names, GUIContent[] labels) {
		position.x = oldPosition.x;
		position.y += 19f;
		position.width = oldPosition.width / names.Length;

		for (int i = 0; i < names.Length; i++) {
			EditorGUI.PropertyField(position, property.FindPropertyRelative(names[i]), labels[i]);
			position.x += position.width;
		}
	}

	protected bool DrawFoldout(string label, int content, bool status, out bool target) {
		position.x = oldPosition.x;
		position.y += 19f;
		position.width = oldPosition.width;

		target = EditorGUI.Foldout(position, status, new GUIContent(label), true);

		if(target)
			height += (content * 19f);

		return target;
	}

	protected void DrawLabel(string label) {
		position.x = oldPosition.x;
		position.y += 19f;
		position.width = oldPosition.width;

		EditorGUI.LabelField(position, new GUIContent(label), EditorStyles.boldLabel);
	}
}