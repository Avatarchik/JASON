using UnityEngine;
using System.Collections;

public class LightController:MonoBehaviour {
	void Start () {
		if(!GameData.Instance.lightEnabled)
			GetComponent<Light>().enabled = false;
	}
}
