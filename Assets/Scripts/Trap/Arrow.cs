using UnityEngine;
using System.Collections;

public class Arrow:MonoBehaviour {
	[SerializeField] private float damage;
	[SerializeField] private float speed;
	[SerializeField] private float fadePerUpdate;

	[SerializeField] private ParticleSystem[] particles;

	private bool fired;
	private bool fading;

	void Update() {
		if(fired)
			transform.Translate(Vector3.right * speed);

		if(fading) {
			Color oldColor = renderer.material.color;

			renderer.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - fadePerUpdate);
		}
	}

	void OnCollisionEnter(Collision col) {
		collider.enabled = false;
		fired = false;

		transform.parent = col.transform;

		if(col.collider.CompareTag("Player"))
			col.collider.GetComponent<Player>().Damage(damage);

		StartFade();
	}

	/** <summary>Fire the arrow</summary>
	 * <param name="origin">The transform of the object that fires the arrow</param> */
	public void Fire(Transform origin) {
		transform.position = origin.position;
		transform.rotation = origin.rotation;
		transform.parent = origin;

		foreach(ParticleSystem particle in particles)
			particle.renderer.enabled = true;

		collider.enabled = true;
		fired = true;

		ResetFade();
	}

	/** <summary>Start fading the arrow out</summary> */
	private void StartFade() {
		foreach(ParticleSystem particle in particles)
			particle.renderer.enabled = false;

		fading = true;
	}

	/** <summary>Reset the alpha of the arrow</summary> */
	private void ResetFade() {
		Color oldColor = renderer.material.color;

		renderer.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1.0f);
		
		fading = false;
	}
}
