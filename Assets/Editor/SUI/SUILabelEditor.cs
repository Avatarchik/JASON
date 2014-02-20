using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;

[CustomPropertyDrawer(typeof(SUILabel))]
public class SUILabelEditor:PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
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
		EditorGUIUtility.labelWidth = 80f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("activated"));
		
		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;
		position.width /= 2f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.x"), new GUIContent("X Position"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.y"), new GUIContent("Y Position"));
		
		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("text"));

		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("size"));

		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("font"));
		
		EditorGUI.indentLevel = oldIndent;
	}
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return property.isExpanded ? 108f : 16f;	
	}
}
