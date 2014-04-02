using UnityEngine;
using System.Collections;

public class GameStarter:MonoBehaviour {
	[SerializeField] private string sceneToLoad;

	void Start() {
		AudioManager.Instance.SetAudio(AudioManager.AudioFiles.NormalMusic,true);
		Application.LoadLevel(sceneToLoad);
	}
}
