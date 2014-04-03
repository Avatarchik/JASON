using UnityEngine;
using System.Collections;

public class ScrollingCombatText:MonoBehaviour {
	private TextMesh text;

	private Color color;

	private float textAlpha = 1;

	void Start() {
		text = GetComponent<TextMesh>();
		color = text.color;

		transform.localEulerAngles = new Vector3(0, 0, 0);

		Destroy(gameObject, 1.5f);
	}
	
	void FixedUpdate() {
		textAlpha -= 0.03f;

		text.color = new Color(color.r, color.g, color.b, textAlpha);

		transform.LookAt(Camera.main.transform.position);
		transform.rotation = Camera.main.transform.rotation;
		transform.Translate(new Vector3(0, 0.02f, 0));

		text.renderer.enabled = true;
	}
}
