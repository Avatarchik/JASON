using UnityEngine;
using System.Collections;

public class VolumeManager:MonoBehaviour {
	[SerializeField] private bool isMusic;
	[SerializeField] private bool isSound;
	
	void Start() {
		if(isMusic)
			audio.volume = AudioManager.Instance.MusicVolume;
		
		if(isSound)
			audio.volume = AudioManager.Instance.SoundVolume;
	}
}
