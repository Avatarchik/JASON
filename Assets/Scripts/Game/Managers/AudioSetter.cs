using UnityEngine;
using System.Collections;

public class AudioSetter:MonoBehaviour {
	public int factor;

	public bool isMusic;

	void FixedUpdate () {
		if(isMusic) {
			audio.volume = AudioManager.Instance.MusicVolume / factor;
		} else {
			audio.volume = AudioManager.Instance.SoundVolume / factor;
		}
	}
}
