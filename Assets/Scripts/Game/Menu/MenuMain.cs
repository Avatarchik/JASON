using UnityEngine;
using System.Collections;
using SGUI;

public class MenuMain:GUIBehaviour {
	[SerializeField] private SGUITexture[] textures;
	[SerializeField] private SGUIButton[] buttons;

	private MenuOptions menuOptions;

	private bool opened = true;

	void Start() {
		if(textures.Length > 0)
			foreach(SGUITexture texture in textures)
				texture.Create();

		if(buttons.Length > 0)
			foreach(SGUIButton button in buttons)
				button.Create();

		menuOptions = GetComponent<MenuOptions>();
	}

	protected override void OnGUI() {
		if(!opened)
			return;

		base.OnGUI();

		if(buttons[0].OnClick) {
			GameObject.Find("SGUI Manager").GetComponent<SGUIManager>().RemoveAll();
			Application.LoadLevel("Normal Dungeon");
		}

		if(buttons[1].OnClick)
			;

		if(buttons[2].OnClick)
			menuOptions.Open();
	}

	public void Open() {
		menuOptions.Close();

		opened = true;

		foreach(SGUITexture texture in textures)
			texture.Activated = true;

		foreach(SGUIButton button in buttons)
			button.Activated = true;
	}

	public void Close() {
		opened = false;

		foreach(SGUITexture texture in textures)
			texture.Activated = false;

		foreach(SGUIButton button in buttons)
			button.Activated = false;
	}
}
