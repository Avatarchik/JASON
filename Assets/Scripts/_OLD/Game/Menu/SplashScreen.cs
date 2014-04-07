using UnityEngine;
using System.Collections;
using SGUI;

public class SplashScreen:GUIBehaviour {
	[SerializeField] private Transform normalCamera;

	[SerializeField] private SGUITexture[] textures;

	private float alpha;

	private bool fadingOut;

	private bool isDone;
	
	void Start() {
		foreach(SGUITexture texture in textures)
			texture.Create();

		alpha = 0;
	}

	protected override void OnGUI() {
		if(isDone)
			return;

		textures[0].Color = new Color(1, 1, 1, alpha);

		if(fadingOut && alpha <= 0) {
			isDone = true;
			textures[0].Activated = false;

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
