using UnityEngine;
using System.Collections;

public class FireGrid:TimedTrap {
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private Renderer lavaRenderer;

	[SerializeField] private float colorAlpha;
	
	[SerializeField] private bool precise;
	
	private bool warmingUp;

	void Start() {
		particles.enableEmission = false;

		StartCoroutine("Cycle");
	}

	void FixedUpdate() {
		if(warmingUp) {
			if(colorAlpha <= 1.0f) {
				colorAlpha += 0.005f;
			} else {
				warmingUp = false;
				activated = true;
			}
		}

		if(!activated && !warmingUp)
			if(colorAlpha >= 0.5f)
				colorAlpha -= 0.01f;

		lavaRenderer.material.color = new Color(colorAlpha, colorAlpha, colorAlpha);
	}

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Player"))
			return;

		entity = col.GetComponent<Entity>();

		StartCoroutine("DamageEntity");
	}

	void OnTriggerExit(Collider col) {
		if(!col.CompareTag("Player"))
			return;

		StopCoroutine("DamageEntity");

		entity = null;
	}
	
	/** <summary>Cycle between the on and off state</summary> */
	private IEnumerator Cycle() {
		yield return new WaitForSeconds(startDelay);

		while(true) {
			float random = 1;

			if(!precise)
				random = Random.Range(2.5f, 7.5f);

			yield return new WaitForSeconds(delay * random);

			warmingUp = true;

			while(warmingUp)
				yield return new WaitForSeconds(0.01f);

			particles.enableEmission = true;

			yield return new WaitForSeconds(duration);

			particles.enableEmission = false;
			activated = false;
		}
	}
}
