using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using SGUI;

[CustomPropertyDrawer(typeof(SGUITextureButton))]
public class SGUITextureButtonEditor:SPropertyDrawer {
	public const float BASE_HEIGHT = 168f;
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
		DrawLabel("Position & Size");
		DrawProperties(	new string[] {"bounds.x", "bounds.y"}, new GUIContent[] {new GUIContent("X"), new GUIContent("Y")});						
		DrawProperties( new string[] {"bounds.width", "bounds.height"}, new GUIContent[] {new GUIContent("Width"), new GUIContent("Height")});
		
		// Textures
		DrawLabel("Textures");
		DrawProperty("textureNormal", new GUIContent("Normal"));
		DrawProperty("textureHover", new GUIContent("Hover"));
		DrawProperty("textureActive", new GUIContent("Active"));
	}
}