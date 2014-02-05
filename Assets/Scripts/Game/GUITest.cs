using UnityEngine;

public class GUITest:MonoBehaviour {
	public SUISpriteButton spriteButton;
	public SUITextureButton textureButton;

	public SUILabel label;
	
	void OnGUI() {
		GUIManager.UpdateGUIMatrix();

		textureButton.Update(1920, 1080);

		if(textureButton.OnNormal) {
			Debug.Log("On Normal");
		}

		if(textureButton.OnHover) {
			Debug.Log("On Hover");
		}

		if(textureButton.OnClick) {
			Debug.Log("On Click");
		}

		label.Draw();
	}
}
