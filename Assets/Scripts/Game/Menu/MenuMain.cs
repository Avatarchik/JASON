using UnityEngine;
using System.Collections;
using SGUI;

public class MenuMain:GUIBehaviour {
	public enum MenuOpen {
		Main,
		Options,
		Credits
	}

	[SerializeField] private SGUITexture[] textures;

	[SerializeField] private SGUIButton[] buttons;

	[SerializeField] private GUIStyle sliderStyle;
	[SerializeField] private GUIStyle thumbStyle;

	private MenuOpen openMenu = MenuOpen.Main;

	void Start() {
		foreach(SGUIButton button in buttons)
			button.Create();

		foreach(SGUITexture texture in textures)
			texture.Create();
	}

	protected override void OnGUI() {
		base.OnGUI();

		if(buttons[0].OnClick)
			openMenu = (openMenu == MenuOpen.Main) ? MenuOpen.Options : MenuOpen.Main;

		RenderMainGUI();
		RenderOptionsGUI();
	}

	private void RenderMainGUI() {
		bool isOpen = (openMenu == MenuOpen.Main);

		for(int i = 9; i < 12; i++)
			buttons[i].Activated = isOpen;
	}

	private void RenderOptionsGUI() {
		bool isOpen = (openMenu == MenuOpen.Options);

		textures[0].Activated = isOpen;

		for(int i = 1; i < 9; i++)
			buttons[i].Activated = isOpen;

		if(isOpen) {
			GameData.Instance.musicVolume = GUI.HorizontalSlider(new Rect(1200, 300, 600, 150), GameData.Instance.musicVolume, 0, 1, sliderStyle, thumbStyle);
			GameData.Instance.audioVolume = GUI.HorizontalSlider(new Rect(1200, 600, 600, 150), GameData.Instance.audioVolume, 0, 1, sliderStyle, thumbStyle);
			GameData.Instance.contrast = GUI.HorizontalSlider(new Rect(1200, 900, 600, 150), GameData.Instance.contrast, 0, 1, sliderStyle, thumbStyle);
		}
	}

	void OptionsHandling(){
		// Dynamic Lights
		if(buttons[4].OnClick)
			GameData.Instance.lightEnabled = !GameData.Instance.lightEnabled;
		
		// Particles
		if(buttons[5].OnClick)
			GameData.Instance.particlesEnabled = !GameData.Instance.particlesEnabled;

		// Left Handed
		if(buttons[6].OnClick)
			GameData.Instance.leftHanded = !GameData.Instance.leftHanded;

		// Save Data
		if(buttons[7].OnClick)
			GameData.Instance.SaveData();
		
		// Reset Save Data
		if(buttons[8].OnClick) {
			GameData.Instance.fireDungeonCleared = false;
			GameData.Instance.normalDungeonCleared = false;
			GameData.Instance.SaveData();
		}
	}
}
