using UnityEngine;
using System.Collections;
using SGUI;

public class MenuOptions:GUIBehaviour {
	[SerializeField] private SGUITexture[] textures;
	[SerializeField] private SGUIButton[] buttons;
	[SerializeField] private SGUILabel[] labels;

	[SerializeField] private GUIStyle sliderBar;
	[SerializeField] private GUIStyle sliderButton;

	private MenuMain menuMain;

	private bool opened = false;

	void Start() {
		foreach(SGUITexture texture in textures) {
			texture.Create();
			texture.Activated = false;
		}

		foreach(SGUIButton button in buttons) {
			button.Create();
			button.Activated = false;
		}

		foreach(SGUILabel label in labels) {
			label.Create();
			label.Activated = false;
		}

		menuMain = GetComponent<MenuMain>();

		buttons[2].TextColor = GameData.Instance.particlesEnabled ? Color.yellow : Color.white;
		buttons[3].TextColor = GameData.Instance.lightEnabled ? Color.yellow : Color.white;
	}

	protected override void OnGUI() {
		if(!opened)
			return;

		base.OnGUI();

		GameData data = GameData.Instance;

		// Reset Save Data
		if(buttons[0].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			data.fireDungeonCleared = false;
			data.normalDungeonCleared = false;
		}

		// Back
		if(buttons[1].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			menuMain.Open();
		}

		// Particles
		if(buttons[2].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			data.particlesEnabled = !data.particlesEnabled;

			buttons[2].TextColor = data.particlesEnabled ? Color.yellow : Color.white;
		}

		// Dynamic Lights
		if(buttons[3].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			data.lightEnabled = !data.lightEnabled;

			buttons[3].TextColor = data.lightEnabled ? Color.yellow : Color.white;
		}

		data.contrast = GUI.HorizontalSlider(new Rect(745, 710, 419.625f, 69.75f), data.contrast, 0, 1, sliderBar, sliderButton);
		data.SaveData();
	}

	/** Open this GUI */
	public void Open() {
		menuMain.Close();

		opened = true;

		StartCoroutine(ButtonDelay());
	}

	/** Close this GUI */
	public void Close() {
		opened = false;

		foreach(SGUITexture texture in textures)
			texture.Activated = false;

		foreach(SGUIButton button in buttons)
			button.Activated = false;

		foreach(SGUILabel label in labels)
			label.Activated = false;
	}

	private IEnumerator ButtonDelay() {
		yield return new WaitForSeconds(0.001f);

		foreach(SGUITexture texture in textures)
			texture.Activated = true;

		foreach(SGUIButton button in buttons)
			button.Activated = true;

		foreach(SGUILabel label in labels)
			label.Activated = true;
	}
}
