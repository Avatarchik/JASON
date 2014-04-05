using UnityEngine;
using System.Collections;

public class ScrollingTexture:MonoBehaviour {
	[SerializeField] private Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);

	[SerializeField] private string textureName = "_MainTex";

	[SerializeField] private float lavaHeight = 0;

	[SerializeField] private int materialIndex = 0;
	
	private Vector2 uvOffset = Vector2.zero;
	
	private bool lavaState = false;

	void LateUpdate() {
		if(lavaState) {
			lavaHeight -= 0.001f;
		} else {
			lavaHeight += 0.001f;
		}

		if(lavaHeight >= 0.05f)
			lavaState = true;

		if(lavaHeight <= 0.01f)
			lavaState = false;

		uvOffset += (uvAnimationRate * Time.deltaTime);

		if(renderer.enabled) {
			renderer.materials[materialIndex].SetTextureOffset(textureName, uvOffset );
			renderer.materials[materialIndex].SetFloat("_Parallax", lavaHeight);
		}
	}
}