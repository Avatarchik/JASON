using UnityEngine;
using System.Collections;

public class ParticleController:MonoBehaviour {
	void Start() {
		if(!GameData.Instance.particlesEnabled)
			GetComponent<ParticleSystem>().renderer.enabled = false;
	}
}
