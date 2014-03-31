using UnityEngine;
using System.Collections;
using SGUI;

public class MenuMain:GUIBehaviour {
	public SGUIButton[] buttons;
	public SGUITexture[] textures;

	private bool optionsOpened;
	private bool creditsOpened;
	private bool mainMenuOpened = true;

	[SerializeField]private GUIStyle sliderStyle;
	[SerializeField]private GUIStyle thumbStyle;

	void Start() {
		foreach(SGUIButton button in buttons)
			button.Create();

		foreach(SGUITexture texture in textures)
			texture.Create();
	}

	protected override void OnGUI() {
		base.OnGUI();
		RenderGUI();
		if(buttons[0].Click) {
			if(optionsOpened){
				optionsOpened = false;
				mainMenuOpened = true;
			}else{
				optionsOpened = true;
				mainMenuOpened = false;
				creditsOpened = false;
			}
		}
		if(optionsOpened){
			OptionsHandling();
		}
	}

	void RenderGUI(){
		/** Option Items*/
		textures[0].Activated = optionsOpened;
		buttons[1].Activated = optionsOpened;
		buttons[2].Activated = optionsOpened;
		buttons[3].Activated = optionsOpened;
		buttons[4].Activated = optionsOpened;
		buttons[5].Activated = optionsOpened;
		buttons[6].Activated = optionsOpened;
		buttons[7].Activated = optionsOpened;
		buttons[8].Activated = optionsOpened;
		buttons[9].Activated = mainMenuOpened;
		buttons[10].Activated = mainMenuOpened;
		buttons[11].Activated = mainMenuOpened;
		if(optionsOpened){
			GameData.Instance.musicVolume = GUI.HorizontalSlider(new Rect(1200,300,600,150),GameData.Instance.musicVolume,0,1,sliderStyle,thumbStyle);
			GameData.Instance.audioVolume = GUI.HorizontalSlider(new Rect(1200,600,600,150),GameData.Instance.audioVolume,0,1,sliderStyle,thumbStyle);
			GameData.Instance.contrast = GUI.HorizontalSlider(new Rect(1200,900,600,150),GameData.Instance.contrast,0,1,sliderStyle,thumbStyle);
		}

	}

	void OptionsHandling(){
		if(buttons[4].Click){
			//Dynamic Lights
			if(GameData.Instance.lightEnabled){
				GameData.Instance.lightEnabled = false;
			}else{
				GameData.Instance.lightEnabled = true;
			}
		}
		if(buttons[5].Click) {
			//Particles
			if(GameData.Instance.particlesEnabled){
				GameData.Instance.particlesEnabled = false;
			}else{
				GameData.Instance.particlesEnabled = true;
			}
		}

		if(buttons[6].Click) {
			//Left Handed
			if(GameData.Instance.leftHanded){
				GameData.Instance.leftHanded = false;
			}else{
				GameData.Instance.leftHanded = true;
			}
		}

		if(buttons[7].Click){
			GameData.Instance.SaveData();
		}
		if(buttons[8].Click){
			GameData.Instance.fireDungeonCleared = false;
			GameData.Instance.normalDungeonCleared = false;
			GameData.Instance.SaveData();
		}
	}
}
