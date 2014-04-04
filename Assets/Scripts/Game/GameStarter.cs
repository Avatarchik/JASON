using UnityEngine;
using System.Collections;

public class GameStarter:MonoBehaviour {
	[SerializeField] private string sceneToLoad;

	void Start() {
		Application.LoadLevel(sceneToLoad);
	}
}
