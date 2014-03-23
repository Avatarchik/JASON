using UnityEngine;
using System.Collections;

public class ScrollingCombatText:MonoBehaviour {
	void Start() {
		transform.localEulerAngles = new Vector3(0, 0, 0);
		Destroy(gameObject, 1.5f);
	}
	
	void FixedUpdate() {
		transform.LookAt(Camera.main.transform.position);
		transform.rotation = Camera.main.transform.rotation;
		transform.Translate(new Vector3(0, 0.02f, 0));
	}
}
