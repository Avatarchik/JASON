using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;

[CustomPropertyDrawer(typeof(Button))]
class ButtonCreator:PropertyDrawer {
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		position.height = 16f;
		
		Rect foldoutPosition = position;
		
		label = EditorGUI.BeginProperty(position, label, property);
		property.isExpanded = EditorGUI.Foldout(foldoutPosition, property.isExpanded, label, true);
		EditorGUI.EndProperty();
		
		if(!property.isExpanded)
			return;
		
		position = EditorGUI.IndentedRect(position);
		
		int oldIndent = EditorGUI.indentLevel;
		Rect oldPosition = position;
		
		position.y += 16f;
		position.width /= 3f;
		
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.labelWidth = 12f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("textureNormal"), new GUIContent("N"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("textureHover"), new GUIContent("H"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("textureActive"), new GUIContent("A"));
		
		position.x = oldPosition.x;
		
		position.y += 19f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.x"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.y"));
		
		EditorGUI.indentLevel = oldIndent;
	}
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return property.isExpanded ? 51f : 16f;	
	}
}
