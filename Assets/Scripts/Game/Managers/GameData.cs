using UnityEngine;
using System.Collections;

public class GameData:Singleton<GameData>{
	public bool normalDungeonCleared;
	public  bool fireDungeonCleared;

	public bool lightEnabled;
	public bool particlesEnabled;
	public bool leftHanded;
	public float contrast;
	void Start () {

	}
	public void GetData(){
		normalDungeonCleared = PlayerPrefsX.GetBool("NormalDungeon",false);
		fireDungeonCleared = PlayerPrefsX.GetBool("FireDungeon",false);
		lightEnabled = PlayerPrefsX.GetBool("Lights",false);
		particlesEnabled = PlayerPrefsX.GetBool("Particles",false);
		leftHanded = PlayerPrefsX.GetBool("LeftHanded",false);
		contrast = PlayerPrefs.GetFloat("Contrast",0);
	}
	// Update is called once per frame
	public void SaveData(){
		PlayerPrefs.Save();
	}
}
