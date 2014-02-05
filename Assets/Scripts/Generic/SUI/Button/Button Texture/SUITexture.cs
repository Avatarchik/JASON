using UnityEngine;
using System;

[Serializable]
public class SUITexture {
	[SerializeField] private Texture texture;

	/** Draw the texture */
	public void Draw(Vector2 position) {
		if(texture == null) {
			Debug.LogWarning("A SUI Texture is missing a texture");
			return;
		}

		GUI.DrawTexture(new Rect(position.x, position.y, texture.width, texture.height), texture);
	}

	/** Set or get the texture */
	public Texture Texture {
		set { texture = value; }
		get { return texture; }
	}
}
