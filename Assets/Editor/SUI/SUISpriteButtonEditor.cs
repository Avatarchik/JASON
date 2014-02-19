using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;

[CustomPropertyDrawer(typeof(SUISpriteButton))]
public class SUISpriteButtonEditor:PropertyDrawer {
	private float height = 144f;

	private bool labelFoldout;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		height = 144f;
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
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("id"));
		
		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;
		position.width /= 2f;
		
		EditorGUIUtility.labelWidth = 80f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.x"), new GUIContent("X Position"));
		position.x += position.width;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("position.y"), new GUIContent("Y Position"));
		
		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;

		EditorGUI.PropertyField(position, property.FindPropertyRelative("spriteSheet"));

		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;

		EditorGUI.indentLevel = 1;
		EditorGUIUtility.labelWidth = 80f;

		EditorGUI.PropertyField(position, property.FindPropertyRelative("spriteNormal"));
		if(property.FindPropertyRelative("spriteNormal").isExpanded) {
			position.y += 51f;
			height += 51f;
		}

		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;

		EditorGUI.PropertyField(position, property.FindPropertyRelative("spriteHover"));
		if(property.FindPropertyRelative("spriteHover").isExpanded) {
			position.y += 51f;
			height += 51f;
		}
		
		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;

		EditorGUI.PropertyField(position, property.FindPropertyRelative("spriteClick"));
		if(property.FindPropertyRelative("spriteClick").isExpanded) {
			position.y += 51f;
			height += 55f;
		}

		position.x = oldPosition.x;
		position.width = oldPosition.width;
		position.y += 19f;

		labelFoldout = EditorGUI.Foldout(position, labelFoldout, new GUIContent("Label"), true);
		if(labelFoldout) {
			height += 95f;
			
			position.x = oldPosition.x;
			position.width = oldPosition.width;
			position.y += 16f;
			
			EditorGUI.PropertyField(position, property.FindPropertyRelative("label.activated"));
			
			position.x = oldPosition.x;
			position.width = oldPosition.width;
			position.y += 19f;
			position.width /= 2f;
			
			EditorGUI.PropertyField(position, property.FindPropertyRelative("label.position.x"), new GUIContent("X Position"));
			position.x += position.width;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("label.position.y"), new GUIContent("Y Position"));
			
			position.x = oldPosition.x;
			position.width = oldPosition.width;
			position.y += 19f;
			
			EditorGUI.PropertyField(position, property.FindPropertyRelative("label.text"));
			
			position.x = oldPosition.x;
			position.width = oldPosition.width;
			position.y += 19f;
			
			EditorGUI.PropertyField(position, property.FindPropertyRelative("label.size"));
			
			position.x = oldPosition.x;
			position.width = oldPosition.width;
			position.y += 19f;
			
			EditorGUI.PropertyField(position, property.FindPropertyRelative("label.font"));
		}
		
		EditorGUI.indentLevel = oldIndent;
	}
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return property.isExpanded ? height : 16f;	
	}
}
