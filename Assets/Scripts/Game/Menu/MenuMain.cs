using UnityEngine;
using System.Collections;
using SGUI;

public class MenuMain:GUIBehaviour {
	public SGUITextureButton[] buttons;
	
	void Start() {
		StartCoroutine(WaitForGlobalManager());
	}

	protected override void OnGUI() {
		base.OnGUI();

		if(buttons[0].Click) {
			buttons[0].Destroy();

			Application.LoadLevel("Game");
		}
	}
	
	/** Wait until the Global Manager has been loaded */
	private IEnumerator WaitForGlobalManager() {
		while(GameObject.FindGameObjectWithTag("Global Manager") == null)
			yield return new WaitForSeconds(0.3f);

		foreach(SGUITextureButton button in buttons)
			button.Create();
	}
}
