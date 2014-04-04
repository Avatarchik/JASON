using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager:Singleton<AudioManager> {
	[SerializeField] [Range(0.0f, 1.0f)] private float musicVolume = 1.0f;
	[SerializeField] [Range(0.0f, 1.0f)] private float soundVolume = 1.0f;

	[SerializeField] private AudioSource[] sources;

	public enum AudioFiles {
		NormalMusic = 0,
		BossMusic = 1,
		DoorOpen = 11,
		BlockMove = 12,
		SwitchActivate = 13,
		FootSteps = 21,
		SwordSlash = 22,
		Bull1 = 30,
		Bull2 = 31,
		Bull3 = 32,
		ButtonClick = 50
	}

	/** Set the active audio source */
	public void SetAudio(AudioFiles filetype, bool state) {
		if(state) {
			if(!sources[(int)filetype].isPlaying)
				sources[(int)filetype].Play();
		} else {
			sources[(int)filetype].Stop();
		}
	}

	/** Set and/or get the music volume */
	public float MusicVolume {
		set { musicVolume = value; }
		get { return musicVolume; }
	}
	
	/** Set and/or get the SFX volume */
	public float SoundVolume {
		set { soundVolume = value; }
		get { return soundVolume; }
	} 
}
