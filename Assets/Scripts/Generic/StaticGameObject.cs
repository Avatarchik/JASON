using UnityEngine;
using System.Collections;

public class StaticGameObject:MonoBehaviour {
	void Start() {
		DontDestroyOnLoad(gameObject);
	}
}
