using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using SGUI;

[CustomPropertyDrawer(typeof(SGUISlowWriteLabel))]
public class SGUISlowWriteLabelEditor:SPropertyDrawer {
	public const float BASE_HEIGHT = 266f;
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
		DrawProperties(new string[] { "bounds.x", "bounds.y" }, new GUIContent[] { new GUIContent("X"), new GUIContent("Y") });
		DrawProperties(new string[] { "bounds.width", "bounds.height" }, new GUIContent[] { new GUIContent("Width"), new GUIContent("Height") });

		// Text
		DrawLabel("Text");
		DrawProperty("text", new GUIContent("Text"));
		DrawProperty("textColor", new GUIContent("Text Color"));
		DrawProperty("font", new GUIContent("Font"));
		DrawProperty("fontSize", new GUIContent("Font Size"));

		// Slow write
		DrawLabel("Slow Write");
		DrawProperty("minDelay", new GUIContent("Min Delay"));
		DrawProperty("maxDelay", new GUIContent("Max Delay"));		
	}
}