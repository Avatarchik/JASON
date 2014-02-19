using UnityEngine;
using System;

[Serializable]
public class SUISprite {
	[SerializeField]private Vector2 texCoords;
	[SerializeField]private Vector2 size;
	[SerializeField]private Vector2 sheetSize;

	/** Draw the texture */
	public void Draw(Vector2 position, Texture texture) {
		if(texture == null) {
			Debug.LogWarning("A SUI Sprite is missing a texture");
			return;
		}

		Debug.Log(new Rect(texCoords.x / sheetSize.x, texCoords.y / sheetSize.y, size.x / (size.x * 2), size.y / (size.y * 2)));

		GUI.DrawTextureWithTexCoords(
			new Rect(position.x, position.y, size.x, size.y),
		    texture,
			new Rect(texCoords.x / sheetSize.x, texCoords.y / sheetSize.y, size.normalized.x, size.normalized.y)
		);
	}

	/** Set or get the texture coordinates of the sprite */
	public Vector2 TexCoords {
		set { texCoords = value; }
		get { return texCoords; }
	}

	/** Set or get the size of the sprite */
	public Vector2 Size {
		set { size = value; }
		get { return size; }
	}
}
