using UnityEngine;
using System.Collections;

public class Room:MonoBehaviour {
	void Start() {
		renderer.material.color = Color.black;
	}
	
	public void Show() {
		renderer.material.color = Color.white;
	}
	
	public void Hide() {
		renderer.material.color = Color.black;
	}
}
