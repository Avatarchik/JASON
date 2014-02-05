using UnityEngine;
using System.Collections;

public class XperiaPlayer:MonoBehaviour {
	public Vector2 movement;

	void Update() {
		movement = new Vector2(0, 0);
		
		if(Input.GetKey(KeyCode.UpArrow)) {
			movement.y -= 0.1f;
		} else if(Input.GetKey(KeyCode.DownArrow)) {
			movement.y += 0.1f;
		}
		
		if(Input.GetKey(KeyCode.LeftArrow)) {
			movement.x += 0.1f;
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			movement.x -= 0.1f;
		}
		
		transform.Translate(movement);
	}
}
