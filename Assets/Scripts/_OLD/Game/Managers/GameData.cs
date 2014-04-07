using UnityEngine;
using System.Collections;

public class GameData:Singleton<GameData> {
	public float contrast;

	public bool normalDungeonCleared;
	public bool fireDungeonCleared;

	public bool lightEnabled;
	public bool particlesEnabled;

	public bool musicEnabled;
	public bool sfxEnabled;

	public bool tutorialFinished;

	void Start() {
		LoadData();
	}

	/** Save the data */
	public void SaveData() {
		PlayerPrefsX.SetBool("Normal Dungeon", normalDungeonCleared);
		PlayerPrefsX.SetBool("Fire Dungeon", fireDungeonCleared);

		PlayerPrefsX.SetBool("Lights", lightEnabled);
		PlayerPrefsX.SetBool("Particles", particlesEnabled);

		PlayerPrefsX.SetBool("Music", musicEnabled);
		PlayerPrefsX.SetBool("SFX", sfxEnabled);

		PlayerPrefsX.SetBool("Tutorial Finished", tutorialFinished);

		PlayerPrefs.SetFloat("Contrast", contrast);

		PlayerPrefs.Save();
	}

	/** Load the data */
	public void LoadData() {
		normalDungeonCleared = PlayerPrefsX.GetBool("Normal Dungeon", false);
		fireDungeonCleared = PlayerPrefsX.GetBool("Fire Dungeon", false);

		lightEnabled = PlayerPrefsX.GetBool("Lights", false);
		particlesEnabled = PlayerPrefsX.GetBool("Particles", true);

		musicEnabled = PlayerPrefsX.GetBool("Music", true);
		sfxEnabled = PlayerPrefsX.GetBool("SFX", true);

		tutorialFinished = PlayerPrefsX.GetBool("Tutorial Finished", false);

		contrast = PlayerPrefs.GetFloat("Contrast", 0);
	}
}
