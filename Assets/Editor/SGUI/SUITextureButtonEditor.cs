using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using SGUI;

[CustomPropertyDrawer(typeof(SUITextureButton))]
public class SUITextureButtonEditor:SPropertyDrawer {
	public const float BASE_HEIGHT = 149f;
	public const int LABEL_WIDTH = 80;
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {		
		base.OnGUI(position, property, label);
		
		if(!property.isExpanded)
			return;
		
		EditorGUIUtility.labelWidth = LABEL_WIDTH;
		height = BASE_HEIGHT;
		
		// Activated
		DrawProperty("activated");
		
		// Bounds
		DrawProperties(	new string[] {"bounds.x", "bounds.y"}, new GUIContent[] {new GUIContent("X"), new GUIContent("Y")});						
		DrawProperties( new string[] {"bounds.width", "bounds.height"}, new GUIContent[] {new GUIContent("Width"), new GUIContent("Height")});
		
		// Textures
		DrawLabel("Textures");
		this.position = EditorGUI.IndentedRect(this.position);
		DrawProperty("textureNormal", new GUIContent("Normal"));
		DrawProperty("textureHover", new GUIContent("Hover"));
		DrawProperty("textureActive", new GUIContent("Active"));
	}
}