using UnityEngine;
using System.Collections;

public class GameManager:MonoBehaviour {
	void OnLevelWasLoaded(int level) {
		if(level == 2) {
			PlayerData.RequestReset();
		}
	}
}
