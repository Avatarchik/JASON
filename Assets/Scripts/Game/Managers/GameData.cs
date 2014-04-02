using UnityEngine;
using System.Collections;

public class GameData:Singleton<GameData> {
	public bool normalDungeonCleared;
	public bool fireDungeonCleared;

	public bool lightEnabled;
	public bool particlesEnabled;

	private bool musicEnabled;
	private bool sfxEnabled;

	public float contrast;

	void Start() {
		LoadData();
	}

	public void SaveData() {
		PlayerPrefsX.SetBool("Normal Dungeon", normalDungeonCleared);
		PlayerPrefsX.SetBool("Fire Dungeon", fireDungeonCleared);

		PlayerPrefsX.SetBool("Lights", lightEnabled);
		PlayerPrefsX.SetBool("Particles", particlesEnabled);

		PlayerPrefsX.SetBool("Music", musicEnabled);
		PlayerPrefsX.SetBool("SFX", sfxEnabled);

		PlayerPrefs.SetFloat("Contrast", contrast);

		PlayerPrefs.Save();
	}

	public void LoadData() {
		normalDungeonCleared = PlayerPrefsX.GetBool("Normal Dungeon", false);
		fireDungeonCleared = PlayerPrefsX.GetBool("Fire Dungeon", false);

		lightEnabled = PlayerPrefsX.GetBool("Lights", false);
		particlesEnabled = PlayerPrefsX.GetBool("Particles", false);

		musicEnabled = PlayerPrefsX.GetBool("Music", true);
		sfxEnabled = PlayerPrefsX.GetBool("SFX", true);

		contrast = PlayerPrefs.GetFloat("Contrast", 0);
	}
}
