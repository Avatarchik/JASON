using UnityEngine;
using System.Collections;

public class GameData:Singleton<GameData>{
	public bool normalDungeonCleared;
	public  bool fireDungeonCleared;

	public bool lightEnabled;
	public bool particlesEnabled;
	public bool leftHanded;
	public float contrast;

	public float musicVolume;
	public float audioVolume;
	void Start () {
		GetData();
	}
	public void GetData(){
		normalDungeonCleared = PlayerPrefsX.GetBool("NormalDungeon",false);
		fireDungeonCleared = PlayerPrefsX.GetBool("FireDungeon",false);
		lightEnabled = PlayerPrefsX.GetBool("Lights",false);
		particlesEnabled = PlayerPrefsX.GetBool("Particles",false);
		leftHanded = PlayerPrefsX.GetBool("LeftHanded",false);
		contrast = PlayerPrefs.GetFloat("Contrast",0);
		musicVolume = PlayerPrefs.GetFloat("Music",1);
		audioVolume = PlayerPrefs.GetFloat("Audio",1);
	}
	// Update is called once per frame
	public void SaveData(){
		PlayerPrefsX.SetBool("NormalDungeon",normalDungeonCleared);
		PlayerPrefsX.SetBool("FireDungeon",fireDungeonCleared);
		PlayerPrefsX.SetBool("Lights",lightEnabled);
		PlayerPrefsX.SetBool("Particles",particlesEnabled);
		PlayerPrefsX.SetBool("LeftHanded",leftHanded);
		PlayerPrefs.SetFloat("Music",musicVolume);
		PlayerPrefs.SetFloat("Audio",audioVolume);
		PlayerPrefs.SetFloat("Contrast",contrast);
		PlayerPrefs.Save();
	}
}
