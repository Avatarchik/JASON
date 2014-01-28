using UnityEngine;
using System.Collections;

public class GUITest:MonoBehaviour {
	public Texture2D[] textures;

	private Button button;

	void Start () {
		button = new Button(new Vector2(100, 100), textures[0], textures[1], textures[2], Button => Debug.Log ("Clicked"));
		
		Debug.Log(SingletonTest.Instance.I);
	}
	
	void OnGUI() {
		button.Update();
	}
}
