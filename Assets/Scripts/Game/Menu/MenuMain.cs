using UnityEngine;
using SGUI;

public class MenuMain:GUIBehaviour {
	public SGUITextureButton[] buttons;
	
	void Start() {
		foreach(SGUITextureButton button in buttons)
			button.Create();
	}

	protected override void OnGUI() {
		base.OnGUI();

		if(buttons[0].Click) {
			buttons[0].Destroy();

			Application.LoadLevel("Game");
		}
	}
}
