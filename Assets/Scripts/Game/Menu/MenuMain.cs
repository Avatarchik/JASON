using UnityEngine;

public class MenuMain:GUIBehaviour {


	void OnGUI(){
		base.OnGUI();
		if(GUI.Button(new Rect(0,0,400,100),"Start")){
			Application.LoadLevel("Game");
		}
	}
}
