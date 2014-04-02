using UnityEngine;
using System.Collections.Generic;
using SGUI;

public class SGUIManager:Singleton<SGUIManager> {
	[SerializeField] private Vector2 nativeGuiSize = new Vector2(1920, 1080);

	private List<SGUITexture> sguiTextures = new List<SGUITexture>();
	private List<SGUIButton> sguiButtons = new List<SGUIButton>();
	private List<SGUILabel> sguiLabels = new List<SGUILabel>();
	
	void OnGUI() {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / nativeGuiSize.x, Screen.height / nativeGuiSize.y, 1));
	
		foreach(SGUITexture texture in sguiTextures)
			texture.Update(nativeGuiSize);
			
		foreach(SGUIButton button in sguiButtons)
			button.Update(nativeGuiSize);

		foreach(SGUILabel label in sguiLabels)
			label.Update(nativeGuiSize);
	}

	public void RemoveAll() {
		for(int i = sguiTextures.Count - 1; i >= 0; i--)
			sguiTextures[i].Destroy();

		for(int i = sguiButtons.Count - 1; i >= 0; i--)
			sguiButtons[i].Destroy();

		for(int i = sguiLabels.Count - 1; i >= 0; i--)
			sguiLabels[i].Destroy();
	}
	
	public bool AnyButtonClicked {
		get {
			foreach(SGUIButton button in sguiButtons)
				if(button.Hover || button.Click)
					if(!button.IsManual)
						return true;

			return false;
		}
	}
	
	internal void RegisterTexture(SGUITexture texture) {
		for(int i = 0; i < sguiTextures.Count; i++)
			if(texture.Equals(sguiTextures[i]))
				sguiTextures[i].Destroy();

		sguiTextures.Add(texture);
	}
	
	internal void RegisterButton(SGUIButton button) {
		for(int i = 0; i < sguiButtons.Count; i++)
			if(button.Equals(sguiButtons[i]))
				sguiButtons[i].Destroy();

		sguiButtons.Add(button);
	}

	internal void RegisterLabel(SGUILabel label) {
		for(int i = 0; i < sguiLabels.Count; i++)
			if(label.Equals(sguiLabels[i]))
				sguiLabels[i].Destroy();

		sguiLabels.Add(label);
	}
	
	internal void RemoveTexture(SGUITexture texture) {
		sguiTextures.Remove(texture);
	}
	
	internal void RemoveButton(SGUIButton button) {
		sguiButtons.Remove(button);
	}

	internal void RemoveLabel(SGUILabel label) {
		sguiLabels.Remove(label);
	}
}
