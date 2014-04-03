using UnityEngine;
using System.Collections;

public class LightFlicker:MonoBehaviour {
	[SerializeField] private Color color0 = Color.red;
	[SerializeField] private Color color1 = new Color(1, 0.0647f, 0);

	[SerializeField] private float duration = 1;

	private Light target;

	void Start() {
		target = GetComponent<Light>();

		StartCoroutine("SetLight");
	}

	void Update () {
		float t = Mathf.PingPong(Time.time, duration) / duration;

		target.color = Color.Lerp(color0, color1, t);
	}

	/** Set the light */
	private IEnumerator SetLight() {
		while(true) {
			float randomTime = Random.Range(0.01f, 0.1f);
			float random = Random.Range(1.0f, 1.5f);

			yield return new WaitForSeconds(randomTime);

			target.intensity = random;
		}
	}
}