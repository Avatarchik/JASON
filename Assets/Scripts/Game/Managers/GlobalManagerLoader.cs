using UnityEngine;
using System.Collections;

public class GlobalManagerLoader:MonoBehaviour {
	void Start() {
		if(GameObject.Find("Global Managers") == null)
			Application.LoadLevelAdditive("Global Manager");
	}
}
