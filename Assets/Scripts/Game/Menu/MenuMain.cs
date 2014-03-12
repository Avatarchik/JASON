using UnityEngine;
using SGUI;

public class MenuMain:GUIBehaviour {
	//public SGUITextureButton fap;
	public SGUISpriteButton fap2;

	protected override void OnGUI() {
		base.OnGUI();

		if(fap2.OnClick) {
			fap2.Destroy();
			Application.LoadLevel("Game");
		}
	}
}
