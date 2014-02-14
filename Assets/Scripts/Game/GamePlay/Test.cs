using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Test:PoolObject {
	[SerializeField] public AudioClip clip;
	[SerializeField] public AudioSource source;

	public override void Update() {
		if(!source.isPlaying)
			available = true;
	}

	public void Play() {
		if(forceStopped) {
			source.Stop();
			forceStopped = false;
		}

		source.clip = clip;
		source.Play();
		available = false;
	}
}
