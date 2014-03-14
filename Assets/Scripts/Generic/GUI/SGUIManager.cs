using UnityEngine;
using System.Collections.Generic;
using SGUI;

public class SGUIManager:Singleton<SGUIManager> {
	[SerializeField] private Vector2 nativeGuiSize = new Vector2(1920, 1080);

	private List<SGUITexture> sguiTextures = new List<SGUITexture>();
	private List<SGUISprite> sguiSprites = new List<SGUISprite>();
	private List<SGUIButton> sguiButtons = new List<SGUIButton>();
	
	void OnGUI() {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / nativeGuiSize.x, Screen.height / nativeGuiSize.y, 1));
	
		foreach(SGUITexture texture in sguiTextures)
			texture.Update(nativeGuiSize);
			
		foreach(SGUISprite sprite in sguiSprites)
			sprite.Update(nativeGuiSize);
			
		foreach(SGUIButton button in sguiButtons)
			button.Update(nativeGuiSize);		
	}
	
	public bool IsAnyButtonClicked() {
		foreach(SGUIButton button in sguiButtons)
			if(button.Click)
				return true;
	
		return false;
	}
	
	internal void RegisterTexture(SGUITexture texture) {
		foreach(SGUITexture other in sguiTextures)
			if(texture.Equals(other))
				other.Destroy();
		
		sguiTextures.Add(texture);
	}
	
	internal void RegisterSprite(SGUISprite sprite) {
		foreach(SGUISprite other in sguiSprites)
			if(sprite.Equals(other))
				other.Destroy();
		
		sguiSprites.Add(sprite);
	}
	
	internal void RegisterButton(SGUIButton button) {
		foreach(SGUIButton other in sguiButtons)
			if(button.Equals(other))
				other.Destroy();

		sguiButtons.Add(button);
	}
	
	internal void RemoveTexture(SGUITexture texture) {
		sguiTextures.Remove(texture);
	}
	
	internal void RemoveSprite(SGUISprite sprite) {
		sguiSprites.Remove(sprite);
	}
	
	internal void RemoveButton(SGUIButton button) {
		sguiButtons.Remove(button);
	}
}
