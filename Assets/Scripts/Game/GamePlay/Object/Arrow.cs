using UnityEngine;
using System.Collections;

public class Arrow:MonoBehaviour {
	[SerializeField] Shader alphaShader;

	private Shader normalShader;

	private Renderer model;

	private bool isTraveling;
	private bool fadeOut;
	public ParticleSystem[] particleEffect;
	void Start() {
		model = transform.GetChild(0).renderer;
		normalShader = renderer.material.shader;
		particleEffect[0].renderer.enabled = false;
		particleEffect[1].renderer.enabled = false;
	}

	void FixedUpdate () {
		if(isTraveling)
			transform.Translate(Vector3.forward * 0.8f);
	}

	void LateUpdate() {
		if(fadeOut) {
			Color newColor = model.material.color;

			newColor.a -= 0.01f;

			model.material.color = newColor;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		isTraveling = false;
		collider.enabled = false;

		transform.parent = collision.transform;

		if(collision.gameObject.CompareTag("Player"))
			collision.gameObject.GetComponent<Player>().Damage(1, 0, false);

		StartFade();
	}

	/** Fire the arrow from the specified arrow trap */
	public void Fire(ArrowTrap arrowTrap) {
		transform.position = arrowTrap.transform.position;
		transform.rotation = arrowTrap.transform.rotation;
		particleEffect[0].renderer.enabled = true;
		particleEffect[1].renderer.enabled = true;
		isTraveling = true;
		collider.enabled = true;
		
		transform.parent = arrowTrap.transform;

		ResetFade();
	}

	/** Start fading out the arrow */
	private void StartFade() {
		model.material.shader = alphaShader;
		particleEffect[0].renderer.enabled = false;
		particleEffect[1].renderer.enabled = false;
		fadeOut = true;
	}

	/** Reset the fade of the arrow */
	private void ResetFade() {
		if(normalShader != null) {
			model.material.shader = normalShader;
			
			Color newColor = model.material.color;
			
			newColor.a = 1.0f;
			
			model.material.color = newColor;

			fadeOut = false;
		}
	}
}