using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using SGUI;

[CustomPropertyDrawer(typeof(SGUISpriteButton))]
public class SGUISpriteButtonEditor:SPropertyDrawer {
	public const float BASE_HEIGHT = 228f;
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
		DrawProperties(new string[] {"bounds.x", "bounds.y"}, new GUIContent[] {new GUIContent("X"), new GUIContent("Y")});						
		DrawProperties(new string[] {"bounds.width", "bounds.height"}, new GUIContent[] {new GUIContent("Width"), new GUIContent("Height")});
		
		// Spritesheet
		DrawLabel("Spritesheet");
		DrawProperty("spriteSheet", new GUIContent("Sprite Sheet"));
		DrawProperties(new string[] {"spriteSheetSize.x", "spriteSheetSize.y"}, new GUIContent[] {new GUIContent("Rows"), new GUIContent("Cols")});

		// Sprites
		DrawLabel("Sprites");
		DrawProperties(new string[] {"spriteNormal.x", "spriteNormal.y"}, new GUIContent[] {new GUIContent("X Normal"), new GUIContent("Y Normal")});
		DrawProperties(new string[] {"spriteHover.x", "spriteHover.y"}, new GUIContent[] {new GUIContent("X Hover"), new GUIContent("Y Hover")});
		DrawProperties(new string[] {"spriteActive.x", "spriteActive.y"}, new GUIContent[] {new GUIContent("X Active"), new GUIContent("Y Active")});
	}
}