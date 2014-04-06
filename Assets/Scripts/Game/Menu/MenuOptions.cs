using UnityEngine;
using System.Collections;
using SGUI;

public class MenuOptions:GUIBehaviour {
	[SerializeField] private Texture logo;
	
	[SerializeField] private GUIStyle resetProgress;
	[SerializeField] private GUIStyle back;
	[SerializeField] private GUIStyle particles;
	[SerializeField] private GUIStyle lights;
	[SerializeField] private GUIStyle label;

	[SerializeField] private GUIStyle sliderBar;
	[SerializeField] private GUIStyle sliderButton;

	private MenuMain menuMain;

	private bool opened = false;

	void Start() {
		menuMain = GetComponent<MenuMain>();

		Texture2D temp = particles.normal.background;

		if(!GameData.Instance.particlesEnabled) {
			particles.normal.background = particles.active.background;
			particles.active.background = temp;
		}

		if(!GameData.Instance.lightEnabled) {
			temp = lights.normal.background;

			lights.normal.background = lights.active.background;
			lights.active.background = temp;
		}
	}

	protected override void OnGUI() {
		if(!opened)
			return;

		base.OnGUI();

		GameData data = GameData.Instance;

		GUI.DrawTexture(new Rect(707, 25, 496.5f, 512), logo);

		// Reset Save Data
		if(GUI.Button(new Rect(20, 915, 517.5f, 158.25f), new GUIContent("Reset Progress"), resetProgress)) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			data.fireDungeonCleared = false;
			data.normalDungeonCleared = false;

			data.SaveData();
		}

		// Back
		if(GUI.Button(new Rect(1381, 915, 517.5f, 158.25f), new GUIContent("Back"), back)) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			menuMain.Open();
		}

		// Particles
		if(GUI.Button(new Rect(220, 650, 387.75f, 118.6875f), new GUIContent("Particles"), particles)) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			data.particlesEnabled = !data.particlesEnabled;

			Texture2D temp = particles.normal.background;

			particles.normal.background = particles.active.background;
			particles.active.background = temp;

			data.SaveData();
		}

		// Dynamic Lights
		if(GUI.Button(new Rect(1270, 650, 387.75f, 118.6875f), new GUIContent("Lights"), lights)) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			data.lightEnabled = !data.lightEnabled;

			Texture2D temp = lights.normal.background;

			lights.normal.background = lights.active.background;
			lights.active.background = temp;

			data.SaveData();
		}

		//GUI.Label(new Rect(820, 650, 500, 50), new GUIContent("Contrast"), label);
		//data.contrast = GUI.HorizontalSlider(new Rect(745, 710, 419.625f, 69.75f), data.contrast, 0, 1, sliderBar, sliderButton);
	}

	/** Open this GUI */
	public void Open() {
		menuMain.Close();

		opened = true;
	}

	/** Close this GUI */
	public void Close() {
		opened = false;
	}

	public bool IsOpen {
		get { return opened; }
	}
}
