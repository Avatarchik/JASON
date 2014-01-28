using UnityEngine;
using System.Collections;

public class GUITest:MonoBehaviour {
	public Texture2D[] textures;

	void Start () {
		new Button(new Vector2(100, 100), textures[0], textures[1], textures[2], Button => Debug.Log ("Clicked"));
		new Label(new Vector2(100, 10), new GUIContent("Fapfapfap"));
		
		Debug.Log(SingletonTest.Instance.I);
	}
}
