﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using SGUI;

[CustomPropertyDrawer(typeof(SGUITexture))]
public class SGUITextureEditor:SPropertyDrawer {
	public const float BASE_HEIGHT = 57f;
	public const int LABEL_WIDTH = 80;

	private bool foldoutGeneral;
	private bool foldoutTextures;

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
		if(DrawFoldout("Texture", 1, foldoutTextures, out foldoutTextures)) {
			DrawProperty("texture", new GUIContent("Texture"));
		}
	}
}