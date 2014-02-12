using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager:Singleton<AudioManager> {
	[SerializeField] [Range(0.0f, 1.0f)] private float musicVolume = 1.0f;
	[SerializeField] [Range(0.0f, 1.0f)] private float soundVolume = 1.0f;

	public float MusicVolume {
		set { musicVolume = value; }
		get { return musicVolume; }
	}
	
	public float SoundVolume {
		set { soundVolume = value; }
		get { return soundVolume; }
	} 
}
