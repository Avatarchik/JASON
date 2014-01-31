using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;

[CustomPropertyDrawer(typeof(Button))]
class ButtonCreator:PropertyDrawer {
	private bool isToggle;

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
		position.width /= 2f;
		
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.labelWidth = 55f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.x"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.y"));
		
		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;
		position.width /= 2f;
		
		EditorGUIUtility.labelWidth = 55f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("text"), new GUIContent("Text"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("tooltip"), new GUIContent("Tooltip"));
		
		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;
		position.width /= 3f;

		EditorGUIUtility.labelWidth = 55f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("textureNormal"), new GUIContent("Normal"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("textureActive"), new GUIContent("Active"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("textureToggle"), new GUIContent("Toggle"));
		
		EditorGUI.indentLevel = oldIndent;
	}
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return property.isExpanded ? 70f : 16f;	
	}
}
