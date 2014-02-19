using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;

[CustomPropertyDrawer(typeof(SUISprite))]
public class SUISpriteEditor:PropertyDrawer {
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
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("texCoords.x"), new GUIContent("Sprite row"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("texCoords.y"), new GUIContent("Sprite col"));
		
		position.x = oldPosition.x;
		position.y += 19f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("size.x"), new GUIContent("Width"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("size.y"), new GUIContent("Height"));

		position.x = oldPosition.x;
		position.y += 19f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("sheetSize.x"), new GUIContent("Sheet rows"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("sheetSize.y"), new GUIContent("Sheet cols"));
		
		EditorGUI.indentLevel = oldIndent;
	}
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return property.isExpanded ? 89f : 16f;	
	}
}
