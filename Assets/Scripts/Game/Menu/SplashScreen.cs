using UnityEngine;
using System.Collections;
using SGUI;

public class SplashScreen:GUIBehaviour {
	[SerializeField] private Transform normalCamera;

	[SerializeField] private Texture logo;

	private float alpha = 0;

	private bool fadingOut;

	private bool isDone;

	void Update() {
		if(isDone)
			return;

		if(Input.touchCount > 0 || Input.GetMouseButton(0)) {
			isDone = true;

			Camera.main.transform.position = normalCamera.position;
			Camera.main.transform.rotation = normalCamera.rotation;

			GetComponent<MenuMain>().Open();
		}
	}

	protected override void OnGUI() {
		if(isDone)
			return;

		Color originalColor = GUI.color;

		GUI.color = new Color(1, 1, 1, alpha);
		GUI.DrawTexture(new Rect(180, 0, 435.25f, 448.5f), logo);
		GUI.color = originalColor;

		if(fadingOut && alpha <= 0) {
			isDone = true;

			Camera.main.transform.position = normalCamera.position;
			Camera.main.transform.rotation = normalCamera.rotation;

			GetComponent<MenuMain>().Open();
		} else if(!fadingOut && alpha >= 1) {
			StartCoroutine(Wait());
		}

		if(fadingOut) {
			alpha -= 0.005f;
		} else {
			alpha += 0.005f;
		}
	}

	private IEnumerator Wait() {
		yield return new WaitForSeconds(1);

		alpha = 1;
		fadingOut = true;
	}
}
