using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using SGUI;

[CustomPropertyDrawer(typeof(SGUIButton))]
public class SGUIButtonEditor:SPropertyDrawer {
	public const float BASE_HEIGHT = 70f;
	public const int LABEL_WIDTH = 80;

	private bool foldoutGeneral;
	private bool foldoutTextures;
	private bool foldoutLabel;
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {		
		base.OnGUI(position, property, label);
		
		if(!property.isExpanded)
			return;
		
		EditorGUIUtility.labelWidth = LABEL_WIDTH;
		height = BASE_HEIGHT;

		// General Settings
		if(DrawFoldout("General", 4, foldoutGeneral, out foldoutGeneral)) {
			DrawProperty("activated");

			DrawLabel("Position & Size");
			DrawProperties(new string[] { "bounds.x", "bounds.y" }, new GUIContent[] { new GUIContent("X"), new GUIContent("Y") });
			DrawProperties(new string[] { "bounds.width", "bounds.height" }, new GUIContent[] { new GUIContent("Width"), new GUIContent("Height") });
		}

		// Texture Settings
		if(DrawFoldout("Textures", 3, foldoutTextures, out foldoutTextures)) {
			DrawProperty("textureNormal", new GUIContent("Normal"));
			DrawProperty("textureHover", new GUIContent("Hover"));
			DrawProperty("textureActive", new GUIContent("Active"));
		}	
		
		// Label Settings
		if(DrawFoldout("Text", 4, foldoutLabel, out foldoutLabel)) {
			DrawProperty("text");
			DrawProperty("textFont", new GUIContent("Font"));
			DrawProperty("textSize", new GUIContent("Size"));
			DrawProperty("textAnchor", new GUIContent("Anchor"));
		}
	}
}