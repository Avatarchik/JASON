using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;

/*[CustomPropertyDrawer(typeof(SUITexture))]
public class SUITextureEditor:PropertyDrawer {
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
		
		position.y += 16f;
		
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.labelWidth = 80f;
		
		EditorGUI.PropertyField(position, property.FindPropertyRelative("texture"));
		
		EditorGUI.indentLevel = oldIndent;
	}
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return property.isExpanded ? 32f : 16f;	
	}
}*/
