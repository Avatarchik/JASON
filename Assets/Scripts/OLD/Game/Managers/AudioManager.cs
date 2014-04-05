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
		MoanMusic = 2,
		MenuMusic = 3,
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

	void OnLevelWasLoaded(int level) {
		SetAudio(AudioFiles.BossMusic, false);
		SetAudio(AudioFiles.MoanMusic, false);
		SetAudio(AudioFiles.NormalMusic, false);
		SetAudio(AudioFiles.MenuMusic, false);

		if(level == 3 || level == 4)
			SetAudio(AudioFiles.NormalMusic, true);

		if(level == 2)
			SetAudio(AudioFiles.MenuMusic, true);

		if(level == 1)
			SetAudio(AudioFiles.MoanMusic, true);
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
