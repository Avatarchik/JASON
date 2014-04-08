using UnityEngine;
using System.Collections;

public class MenuSplash:GUIBehaviour {
	[SerializeField] private Texture2D splashScreen;
	[SerializeField] private Rect splashScreenPosition;

	private float alpha;

	private bool fadingOut;
	private bool done;

	protected override void OnGUI() {
		if(done)
			return;

		base.OnGUI();

		if(fadingOut && alpha <= 0) {
			done = true;
		} else if(!fadingOut && alpha >= 1.0f) {
			StartCoroutine("FadeDelay");
		}

		Color originalColor = GUI.color;

		GUI.color = new Color(1, 1, 1, alpha);
		GUI.DrawTexture(splashScreenPosition, splashScreen);
		GUI.color = originalColor;

		alpha = fadingOut ? alpha - 0.005f : alpha + 0.005f;
	}

	private IEnumerator FadeDelay() {
		yield return new WaitForSeconds(1.0f);
		
		alpha = 1;
		fadingOut = true;
	}
}
