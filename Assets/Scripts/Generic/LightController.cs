using UnityEngine;
using System.Collections;

public class LightController:MonoBehaviour {
	void Start () {
		if(!GameData.Instance.lightEnabled) {
			GetComponent<Light>().enabled = true;
		} else {
			GetComponent<Light>().enabled = false;
		}
	}
}
