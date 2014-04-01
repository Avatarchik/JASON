using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using SGUI;

[CustomPropertyDrawer(typeof(SGUILabel))]
public class SGUILabelEditor:SPropertyDrawer {
	public const float BASE_HEIGHT = 57f;
	public const int LABEL_WIDTH = 80;

	private bool foldoutGeneral;
	private bool foldoutText;

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

		// Text Settings
		if(DrawFoldout("Text", 4, foldoutText, out foldoutText)) {
			DrawProperty("text", new GUIContent("Text"));
			DrawProperty("textColor", new GUIContent("Text Color"));
			DrawProperty("font", new GUIContent("Font"));
			DrawProperty("fontSize", new GUIContent("Font Size"));
		}
	}
}