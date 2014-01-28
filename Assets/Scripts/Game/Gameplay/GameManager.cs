using UnityEngine;
using System.Collections;

public class GameManager:MonoBehaviour {
	void OnGUI() {
		Button.Update();
		Label.Update();
	}
	
	void OnApplicationQuit() {
		Button.Clear();
		Label.Clear();
	}
}
