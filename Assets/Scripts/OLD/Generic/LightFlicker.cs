using UnityEngine;
using System.Collections;

public class LightFlicker:MonoBehaviour {
	[SerializeField] private float duration;

	[SerializeField] private float minIntensity;
	[SerializeField] private float maxIntensity;

	[SerializeField] private Color[] colors;

	void Start() {
		StartCoroutine("SwitchLightColor");
	}

	void Update () {
		int color1 = Random.Range(0, colors.Length);
		int color2 = Random.Range(0, colors.Length);

		light.color = Color.Lerp(colors[color1], colors[color2], Mathf.PingPong(Time.time, duration) / duration);
	}

	/** <summary>Switch between the available light colors every cycle</summary> */
	private IEnumerator SwitchLightColor() {
		while(true) {
			float intensity = Random.Range(minIntensity, maxIntensity);
			float delay = Random.Range(0.01f, 0.1f);

			yield return new WaitForSeconds(delay);

			light.intensity = intensity;
		}
	}
}