using UnityEngine;
using System;

[Serializable]
public class SUISprite {
	[SerializeField]private Vector2 texCoords;
	[SerializeField]private Vector2 size;

	/** Draw the texture */
	public void Draw(Vector2 position, Texture texture, Vector2 sheetSize) {
		if(texture == null) {
			Debug.LogWarning("A SUI Sprite is missing a texture");
			return;
		}
		
		float xOffset = 1;
		float yOffset = 1;
		
		if(texCoords.x.Equals(sheetSize.x) && sheetSize.x > 1)
			xOffset = 2;
			
		if(texCoords.y.Equals(sheetSize.y) && sheetSize.y > 1)
			yOffset = 2;
			
		GUI.DrawTextureWithTexCoords(
			new Rect(position.x, position.y, size.x, size.y),
		    texture,
			new Rect((texCoords.x - 1) / sheetSize.x, (texCoords.y - 1) / sheetSize.y, texCoords.x / sheetSize.x / xOffset, texCoords.y / sheetSize.y / yOffset)
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
