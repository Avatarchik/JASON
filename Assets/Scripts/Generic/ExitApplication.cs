using UnityEngine;
using System.Collections;

public class ExitApplication:Singleton<ExitApplication> {
	private bool gameStarted;

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(!gameStarted) {
				Application.Quit();
			} else {
				gameStarted = false;

				GameObject.Find("SGUI Manager").GetComponent<SGUIManager>().RemoveAll();

				Application.LoadLevel("Menu");
			}
		}
	}

	public bool GameStarted {
		set { gameStarted = value; }
	}
}
