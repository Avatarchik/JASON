using UnityEngine;

public class GUITest:MonoBehaviour {
	public Button button;
	
	void OnGUI() {
		//if(button.Clicked) {
		//	Debug.Log("Button was clicked!");
		//}
		
		if(button.Toggled) {
			Debug.Log("Button was turned on!");
		}
	}
}
